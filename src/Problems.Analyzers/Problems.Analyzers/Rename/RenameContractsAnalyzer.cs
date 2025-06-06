using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Problems.Analyzers.Rename;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RenameContractsAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "AB0001";

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

    private const string Category = "Naming";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: Description);

    // Keep in mind: you have to list your rules here.
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        if (!Debugger.IsAttached)
            Debugger.Launch();

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.RecordDeclaration);
    }

    private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var location = context.Node.GetLocation();
        var path = location.SourceTree?.FilePath;
        if (!IsAvailablePath(path))
            return;

        if (context.Node is not RecordDeclarationSyntax recordDeclarationSyntax)
            return;

        if (!IsCorrectRecordName(path!))
        {
            var diagnostic = Diagnostic.Create(Rule, location, recordDeclarationSyntax.Identifier.Text);
            context.ReportDiagnostic(diagnostic);
        }
    }

    private bool IsAvailablePath(string? path)
    {
        var requiredPath = $"Problems.Analyzers.Sample{Path.DirectorySeparatorChar}RenameContracts{
            Path.DirectorySeparatorChar}";

        return path?.Contains(requiredPath) is true;
    }

    private bool IsCorrectRecordName(string path)
    {
        var fileName = Path.GetFileNameWithoutExtension(path);

        var fileNameParts = path.Split(Path.DirectorySeparatorChar)
            .Reverse()
            .Skip(1)
            .Take(2)
            .ToArray();

        var startName = fileNameParts.Last();
        var endName = fileNameParts.First();

        return fileName.StartsWith(startName) && fileName.EndsWith(endName);
    }
}