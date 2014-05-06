using Microsoft.CodeAnalysis;

namespace Furnace.ContentTypes.Roslyn.Extensions
{
    public static class ISymbolExtensions
    {
        public static string ToMinimallyQualified(this ISymbol symbol)
        {
            return symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        }

        public static string GetNamespace(this ISymbol symbol)
        {
            return symbol.ContainingNamespace.ToDisplayString();
        }
    }
}
