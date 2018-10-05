using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Lagan.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MissingAnnotationAnalyzer : DiagnosticAnalyzer
    {
        private const string Category = "Design";

        private static readonly DiagnosticDescriptor MissingAnnotationRule = new DiagnosticDescriptor("LGN001",
                                     new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources)),
                                     new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources)),
                                     Category,
                                     DiagnosticSeverity.Warning,
                                     isEnabledByDefault: true,
                                     description: new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources)));


        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(MissingAnnotationRule);

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(AnalyzeParameter, SymbolKind.Parameter);
            context.RegisterSymbolAction(AnalyzeField, SymbolKind.Field);
        }

        private void AnalyzeParameter(SymbolAnalysisContext context)
        {
            var symbol = (IParameterSymbol)context.Symbol;

            if (!symbol.Type.ImplementsIDisposable())
            {
                return;
            }

            AnalyzeSymbol(context, symbol);
        }

        private void AnalyzeField(SymbolAnalysisContext context)
        {
            var symbol = (IFieldSymbol)context.Symbol;

            if (!symbol.Type.ImplementsIDisposable())
            {
                return;
            }

            AnalyzeSymbol(context, symbol);
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context, ISymbol target)
        {
            if (!target.GetAttributes().Any(attribute => attribute.AttributeClass.IsOwnedAttribute() || attribute.AttributeClass.IsBorrowedAttribute()))
            {
                var diagnostic = Diagnostic.Create(MissingAnnotationRule, target.Locations[0], target.Name);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
