namespace Furnace.Roslyn.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class SyntaxNodeExtensions
    {
        public static IEnumerable<PropertyDeclarationSyntax> GetPropertyDeclarationSyntax(this SyntaxNode syntaxNode)
        {
            return syntaxNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();
        }

        public static IEnumerable<ClassDeclarationSyntax> GetClassDeclarationSyntax(this SyntaxNode syntaxNode)
        {
            return syntaxNode.DescendantNodes().OfType<ClassDeclarationSyntax>();
        }
    }
}
