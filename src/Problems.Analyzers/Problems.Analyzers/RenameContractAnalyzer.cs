using System;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Problems.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RenameContractsAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "AB0001";

    private static readonly LocalizableString _title = new LocalizableResourceString(
        nameof(Resources.AB0001Title),
        Resources.ResourceManager,
        typeof(Resources));

    private static readonly LocalizableString _messageFormat =
        new LocalizableResourceString(
            nameof(Resources.AB0001MessageFormat),
            Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString _description =
        new LocalizableResourceString(
            nameof(Resources.AB0001Description),
            Resources.ResourceManager,
            typeof(Resources));

    private const string _category = "Naming";

    private static readonly DiagnosticDescriptor _rule = new(
        DiagnosticId,
        _title,
        _messageFormat,
        _category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: _description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [_rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeRecordDeclaration, SyntaxKind.RecordDeclaration);
    }

    private static void AnalyzeRecordDeclaration(SyntaxNodeAnalysisContext context)
    {
        var recordDeclaration = (RecordDeclarationSyntax)context.Node;
        var filePath = recordDeclaration.SyntaxTree.FilePath;
        
        if (!filePath.Contains("RenameContracts"))
            return;
        
        var recordName = recordDeclaration.Identifier.Text;

        var directoryName = Path.GetFileName(Path.GetDirectoryName(filePath));
        var rootFolder = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(filePath)));
        
        if (string.IsNullOrEmpty(rootFolder) || string.IsNullOrEmpty(directoryName))
            return;
        
        var startsWithRootFolder = recordName.StartsWith(rootFolder, StringComparison.OrdinalIgnoreCase);
        var endsWithDirectoryName = recordName.EndsWith(directoryName, StringComparison.OrdinalIgnoreCase);

        if (startsWithRootFolder && endsWithDirectoryName)
            return;
        
        var diagnostic = Diagnostic.Create(
            _rule,
            recordDeclaration.Identifier.GetLocation(),
            recordName,
            rootFolder,
            directoryName
        );
        
        context.ReportDiagnostic(diagnostic);
    }
}