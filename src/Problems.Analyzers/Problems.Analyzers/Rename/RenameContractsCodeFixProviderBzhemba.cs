using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace Problems.Analyzers.Rename;

[ExportCodeFixProvider(LanguageNames.CSharp), Shared]
public partial class RenameContractsCodeFixProviderBzhemba : CodeFixProvider
{
    private const string _title = "Rename to match naming convention";

    public sealed override ImmutableArray<string> FixableDiagnosticIds
        => [RenameContractsAnalyzerBzhemba.DiagnosticId];

    public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public async sealed override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var recordDeclaration = root!.FindToken(diagnosticSpan.Start)
            .Parent.AncestorsAndSelf()
            .OfType<RecordDeclarationSyntax>()
            .First();

        context.RegisterCodeFix(
            CodeAction.Create(
                title: _title,
                createChangedSolution: c => RenameAsync(context.Document, recordDeclaration, c)),
            diagnostic);
    }

    private async Task<Solution> RenameAsync(
        Document document,
        RecordDeclarationSyntax recordDeclaration,
        CancellationToken cancellationToken)
    {
        var rootFolder = document.Folders.Reverse().Skip(1).FirstOrDefault();
        var baseName = recordDeclaration.Identifier.Text;
        var currentFolder = document.Folders.Last();

        var expectedName = CreateExpectedRecordName(rootFolder!, baseName, currentFolder);

        var semanticModel = await document.GetSemanticModelAsync(cancellationToken);

        if (semanticModel == null)
            return document.Project.Solution;

        var recordSymbol = semanticModel.GetDeclaredSymbol(recordDeclaration, cancellationToken);
        if (recordSymbol == null)
            return document.Project.Solution;

        var renameOptions = new SymbolRenameOptions
        {
            RenameOverloads = true,
            RenameInComments = true,
            RenameInStrings = true,
        };

        var newSolution = await Renamer.RenameSymbolAsync(
            document.Project.Solution,
            recordSymbol,
            renameOptions,
            expectedName,
            cancellationToken);

        return newSolution;
    }

    private string CreateExpectedRecordName(string rootFolder, string baseName, string directoryName)
    {
        var rootOverlap = FindOverlapLength(rootFolder, baseName);
        var combined = rootFolder + (rootOverlap > 0 ? baseName.Substring(rootOverlap) : baseName);

        var directoryOverlap = FindOverlapLength(combined, directoryName);
        var result = combined + (directoryOverlap > 0 ? directoryName.Substring(directoryOverlap) : directoryName);

        return result;
    }

    private int FindOverlapLength(string firstString, string secondString)
    {
        var maxOverlap = Math.Min(firstString.Length, secondString.Length);
        for (var i = maxOverlap; i >= 0; i--)
        {
            if (firstString.EndsWith(secondString.Substring(0, i)))
                return i;
        }

        return 0;
    }
}