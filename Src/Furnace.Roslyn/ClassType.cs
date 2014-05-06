using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Furnace.Roslyn
{
    public class ClassType
    {
        public SyntaxNode DocumentRoot { get; set; }
        public SemanticModel SemanticModel { get; set; }
        public ClassDeclarationSyntax ClassDeclarationSyntax { get; set; }
    }
}
