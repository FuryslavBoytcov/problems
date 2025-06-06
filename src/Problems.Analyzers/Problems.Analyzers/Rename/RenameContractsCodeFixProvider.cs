using System;
using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace Problems.Analyzers.Rename;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(RenameContractsCodeFixProvider)), Shared]
public class RenameContractsCodeFixProvider : CodeFixProvider
{
    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } =
        [RenameContractsAnalyzer.DiagnosticId];

    public RenameContractsCodeFixProvider()
    {
        if (!Debugger.IsAttached)
            Debugger.Launch();
    }

    public override FixAllProvider? GetFixAllProvider() => null;

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.Single();
        var diagnosticSpan = diagnostic.Location.SourceSpan;
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        var diagnosticNode = root?.FindNode(diagnosticSpan);

        if (diagnosticNode is not RecordDeclarationSyntax declaration)
            return;

        var path = declaration.GetLocation().SourceTree!.FilePath;
        var fixedFileName = GetFixedRecordName(path);

        context.RegisterCodeFix(
            CodeAction.Create(
                title: string.Format(Resources.AB0001CodeFixTitle, declaration.Identifier.Text, fixedFileName),
                createChangedSolution: c => SanitizeCompanyNameAsync(context.Document, declaration, c),
                equivalenceKey: nameof(Resources.AB0001CodeFixTitle)),
            diagnostic);
    }

    private async Task<Solution> SanitizeCompanyNameAsync(
        Document document,
        RecordDeclarationSyntax declaration,
        CancellationToken cancellationToken)
    {
        var path = declaration.GetLocation().SourceTree!.FilePath;
        var fixedFileName = GetFixedRecordName(path);

        var semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);

        var typeSymbol = semanticModel?.GetDeclaredSymbol(declaration, cancellationToken);
        if (typeSymbol == null)
            return document.Project.Solution;

        var newSolution = await Renamer
            .RenameSymbolAsync(
                document.Project.Solution,
                typeSymbol,
                new(),
                fixedFileName,
                cancellationToken)
            .ConfigureAwait(false);

        return newSolution;
    }


    private string GetFixedRecordName(string path)
    {
        var fileNameParts = path.Split(Path.DirectorySeparatorChar)
            .Reverse()
            .Skip(1)
            .Take(2)
            .ToArray();

        var startName = fileNameParts.Last();
        var endName = fileNameParts.First();
        var fileName = Path.GetFileNameWithoutExtension(path);

        var baseName = fileName.Replace(startName, String.Empty).Replace(endName, String.Empty);

        return $"{startName}{baseName}{endName}";
    }
}