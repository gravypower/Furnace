using Furnace.Models.ContentTypes;

namespace Furnace.Roslyn.Extensions
{
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal static class PropertyDeclarationSyntaxExtensions
    {
        public static Property GetFurnaceContentTypeProperty(
            this PropertyDeclarationSyntax propertySyntax)
        {
            return new Property
            {
                Name = propertySyntax.Identifier.Text,
                Type = propertySyntax.Type.ToString(),
                DefaultValue = GetDefaultValve(propertySyntax)
            };
        }

        private static string GetDefaultValve(PropertyDeclarationSyntax propertySyntax)
        {
            if (propertySyntax.Initializer == null)
            {
                return null;
            }

            if (propertySyntax.Initializer.Value is LiteralExpressionSyntax)
            {
                var literalExpression = propertySyntax.Initializer.Value as LiteralExpressionSyntax;
                return literalExpression.Token.ValueText;
            }

            //if (propertySyntax.Initializer.Value is ObjectCreationExpressionSyntax)

            var childSyntaxList = propertySyntax.Initializer.Value;

            return
                childSyntaxList.DescendantNodesAndSelf()
                    .OfType<ObjectCreationExpressionSyntax>()
                    .First()
                    .ToFullString();
        }
    }
}
