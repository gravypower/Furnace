namespace Furnace.Roslyn.Tests
{
    using System.Linq;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using NUnit.Framework;

    [TestFixture]
    public class InitializerTests
    {
        [Test]
        public void GivenAnAutoProperty_WithAutoPropertyInitialiser_ThenCanReadStringPropertyInitialValue()
        {
            var source = @"
            using System;
            public class Test
            {
                public string StringProperty { get; set; } = ""StringPropertyDefault"";
            }";

            var tree = SyntaxFactory.ParseSyntaxTree(source);

            var properties = tree.GetRoot().DescendantNodes()
                    .OfType<PropertyDeclarationSyntax>();

            foreach (var property in properties)
            {
                Assert.That(GetDefaultValue(property), Is.EqualTo("StringPropertyDefault"));
            }

        }

        [Test]
        public void GivenAnAutoProperty_WithAutoPropertyInitialiser_ThenCanReadDateTimePropertyInitialValue()
        {
            var source = @"
            using System;
            public class Test
            {
                public DateTime DateTimeProperty { get; set; } = new DateTime(2014, 4, 25);
            }";

            var tree = SyntaxFactory.ParseSyntaxTree(source);

            var properties = tree.GetRoot().DescendantNodes()
                    .OfType<PropertyDeclarationSyntax>();

            foreach (var property in properties)
            {
                Assert.That(GetDefaultValue(property), Is.EqualTo("new DateTime(2014, 4, 25)"));
            }
        }

        private static string GetDefaultValue(PropertyDeclarationSyntax property)
        {
            var childSyntaxList = property.Initializer.Value;

            if (property.Initializer.Value is LiteralExpressionSyntax)
            {
                var literalExpression = property.Initializer.Value as LiteralExpressionSyntax;
                return literalExpression.Token.ValueText;
            }
            return childSyntaxList.DescendantNodesAndSelf().OfType<ObjectCreationExpressionSyntax>().First().ToFullString();
        }
    }
}
