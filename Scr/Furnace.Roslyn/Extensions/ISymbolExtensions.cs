namespace Furnace.Roslyn.Extensions
{
    using Microsoft.CodeAnalysis;

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
