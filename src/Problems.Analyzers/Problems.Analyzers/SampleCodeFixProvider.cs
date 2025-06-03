using System;
using System.Collections.Immutable;
using System.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace Problems.Analyzers;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SampleCodeFixProvider)), Shared]
public class SampleCodeFixProvider : CodeFixProvider
{
    private static readonly LocalizableString CodeFixTitle =
        new LocalizableResourceString(
            nameof(Resources.AB0001CodeFixTitle),
            Resources.ResourceManager,
            typeof(Resources));
    
    public sealed override ImmutableArray<string> FixableDiagnosticIds => [SampleSemanticAnalyzer.DiagnosticId];

    public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.Single();
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
        if (root is null)
            return;
        
        var token = root.FindToken(diagnostic.Location.SourceSpan.Start);
        var dto = token.Parent?.AncestorsAndSelf().OfType<RecordDeclarationSyntax>().FirstOrDefault();
        if (dto is null)
            return;
        
        var changes = GetChanges(context.Document, dto);
        if (changes is null)
            return;
        
        var renameDescription = string.Format(CodeFixTitle.ToString(), changes.Value.CurrentName, changes.Value.NewName);
        
        context.RegisterCodeFix(
            CodeAction.Create(renameDescription, t => RenameRecord(context.Document, dto, changes.Value.NewName, t)),
            diagnostic);
    }

    private async Task<Solution> RenameRecord(
        Document document,
        RecordDeclarationSyntax dto,
        string newName,
        CancellationToken token)
    {
        if (document is null)
            throw new ArgumentNullException(nameof(document));
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(newName));

        var semanticModel = await document.GetSemanticModelAsync(token);
        var symbol = semanticModel?.GetDeclaredSymbol(dto, token);
        if (symbol is null)
            throw new ApplicationException("Can not find symbol for the record declaration.");

        var solution = document.Project.Solution;
        var newSolution = await Renamer.RenameSymbolAsync(solution, symbol, new SymbolRenameOptions(), newName, token);
        return newSolution;
    }

    private static (string CurrentName, string NewName)? GetChanges(Document document, RecordDeclarationSyntax dto)
    {
        if (document is null)
            throw new ArgumentNullException(nameof(document));
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        var name = dto.Identifier.Text;
        var cleanName = name;
        var folders = (document.FilePath ?? string.Empty).Split(Path.DirectorySeparatorChar);
        var scanFolderIndex = Array.FindIndex(folders, f => f.Equals(SampleSemanticAnalyzer.ScanFolder));
        
        if (scanFolderIndex > 0 && scanFolderIndex + 2 <= folders.Length)
        {
            var prefix = folders[scanFolderIndex + 1];
            var suffix = folders[scanFolderIndex + 2];
        
            if (cleanName.EndsWith(suffix))
                cleanName = cleanName.Substring(0, cleanName.Length - suffix.Length);
            if (cleanName.StartsWith(prefix))
                cleanName = cleanName.Substring(prefix.Length);
        
            var newName = $"{prefix}{cleanName}{suffix}";

            return (name, newName);
        }

        return null;
    }
}