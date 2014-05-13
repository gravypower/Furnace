using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Furnace.ContentTypes.Roslyn.Extensions
{
    public static class SyntaxNodeExtensions
    {
        public static IEnumerable<PropertyDeclarationSyntax> PropertyDeclarationNodes(this SyntaxNode syntaxNode)
        {
            return syntaxNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();
        }

        public static IEnumerable<ClassDeclarationSyntax> ClassDeclarationNodes(this SyntaxNode syntaxNode)
        {
            return syntaxNode.DescendantNodes().OfType<ClassDeclarationSyntax>();
        }
    }
}
