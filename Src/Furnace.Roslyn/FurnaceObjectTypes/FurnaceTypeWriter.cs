using System.Collections.Generic;
using Furnace.Models.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public class FurnaceTypeWriter : CSharpSyntaxRewriter
    {
        private readonly TypeSyntax _baseTypeSyntax;
        private readonly string _baseClassFullName;

        public const string FurnaceTypeIdentifier = "FurnaceTypeIdentifier_";

        public FurnaceTypeWriter(string baseClassFullName)
        {
            _baseClassFullName = baseClassFullName;
            GuardBaseClass(baseClassFullName);
            _baseTypeSyntax = SyntaxFactory.ParseTypeName(baseClassFullName);
        }

        public override SyntaxNode Visit(SyntaxNode node)
        {
            GuardNode(node);
            return base.Visit(node);
        }

        private static void GuardNode(SyntaxNode node)
        {
            if (node != null && node.SyntaxTree.Length == 0)
                throw new BaseClassException(new[] {BaseClassException.EmptyBaseClass});
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var typesList = new List<TypeSyntax> { _baseTypeSyntax };

            if (node.BaseList != null)
                typesList.AddRange(node.BaseList.Types);

            var types = new SeparatedSyntaxList<TypeSyntax>();
            types = types.AddRange(typesList);

            var identifier = SyntaxFactory.Identifier(FurnaceTypeIdentifier + _baseClassFullName);

            var updatedNode = node.Update(node.AttributeLists,
                node.Modifiers,
                node.Keyword,
                identifier,
                node.TypeParameterList,
                SyntaxFactory.BaseList(types),
                node.ConstraintClauses,
                node.OpenBraceToken,
                node.Members,
                node.CloseBraceToken,
                node.SemicolonToken).NormalizeWhitespace();

            return updatedNode;
        }

        private static void GuardBaseClass(string baseClassPath)
        {
            if (baseClassPath == null)
                throw new BaseClassException(new[] { BaseClassException.NullBaseClassFullName });

            if (baseClassPath == string.Empty)
                throw new BaseClassException(new[] { BaseClassException.EmptyBaseClassFullName });
        }

        public class BaseClassException : ReasonsFurnaceException
        {
            public const string NullBaseClassFullName = "Base class full name was null";

            public const string EmptyBaseClassFullName = "Base class full name was Empty";

            public const string EmptyBaseClass = "Base class was Empty";

            public BaseClassException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }
    }
}
