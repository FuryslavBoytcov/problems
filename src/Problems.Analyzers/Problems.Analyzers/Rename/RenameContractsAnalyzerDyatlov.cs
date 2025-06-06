using System;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Problems.Analyzers.Rename;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RenameContractsAnalyzerDyatlov : DiagnosticAnalyzer
{
    public const string DiagnosticId = "AB0001";
    private const string Category = "Naming";
    public const string ScanFolder = "RenameContracts";

    private static readonly LocalizableString Title = new LocalizableResourceString(
        nameof(Resources.AB0001Title),
        Resources.ResourceManager,
        typeof(Resources));

    private static readonly LocalizableString MessageFormat =
        new LocalizableResourceString(
            nameof(Resources.AB0001MessageFormat),
            Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString Description =
        new LocalizableResourceString(
            nameof(Resources.AB0001Description),
            Resources.ResourceManager,
            typeof(Resources));

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeRecord, SyntaxKind.RecordDeclaration);
    }

    private void AnalyzeRecord(SyntaxNodeAnalysisContext context)
    {
        var dto = (RecordDeclarationSyntax) context.Node;
        var folders = context.Node.SyntaxTree.FilePath.Split(Path.DirectorySeparatorChar);
        var scanFolderIndex = Array.FindIndex(folders, f => f.Equals(ScanFolder));

        if (scanFolderIndex > 0 && scanFolderIndex + 2 <= folders.Length)
        {
            var prefix = folders[scanFolderIndex + 1];
            var suffix = folders[scanFolderIndex + 2];
            var name = dto.Identifier.Text;

            if (!name.StartsWith(prefix) || !name.EndsWith(suffix))
            {
                var diagnostic = Diagnostic.Create(
                    Rule,
                    dto.Identifier.GetLocation(),
                    name,
                    $"{prefix}...{suffix}");

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}