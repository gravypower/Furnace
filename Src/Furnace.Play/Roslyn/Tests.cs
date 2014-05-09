using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.MSBuild;
using NUnit.Framework;
using ServiceStack.Text;

namespace Furnace.Play.Roslyn
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SomeTest()
        {
            var tree = CSharpSyntaxTree.ParseFile("Roslyn/FurnaceItem.cs");
            var root = tree.GetRoot();
            var node = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            var tt = SyntaxFactory.ParseTypeName("TestClass");

            var typesList = new List<TypeSyntax> {tt};
            typesList.AddRange(node.BaseList.Types);

            var types = new SeparatedSyntaxList<TypeSyntax>();
            types = types.AddRange(typesList);
            
            //SyntaxFactory.BaseList();
            var updated = node.Update(node.AttributeLists,
                node.Modifiers,
                node.Keyword,
                node.Identifier,
                node.TypeParameterList,
                SyntaxFactory.BaseList(types),
                node.ConstraintClauses,
                node.OpenBraceToken,
                node.Members,
                node.CloseBraceToken,
                node.SemicolonToken).NormalizeWhitespace();

            System.Console.WriteLine(updated.ToFullString());
        }

        [Test]
        public void SomeOtherTest()
        {
            if(File.Exists(@"c:\temp\MyCompilation.dll"))
                return;

            var tree = CSharpSyntaxTree.ParseFile("Roslyn/FurnaceItem.cs");
            var oldRoot = tree.GetRoot();
            var rewriter = new BaseClassInserter("TestClass");
            var newRoot = rewriter.Visit(oldRoot);
            newRoot = newRoot.NormalizeWhitespace(); // fix up the whitespace so it is legible.

            System.Console.WriteLine(newRoot.ToFullString());

            var newTree = SyntaxFactory.SyntaxTree(newRoot, "MyCodeFile.cs");
            var compilation = CSharpCompilation.Create("MyCompilation")
                .AddSyntaxTrees(newTree)
                .AddReferences(new MetadataFileReference(typeof(object).Assembly.Location))
                .AddReferences(new MetadataFileReference(typeof(FurnaceItem).Assembly.Location))
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var file = new FileStream(@"c:\temp\MyCompilation.dll", FileMode.Create))
            {
                var result = compilation.Emit(file);

                Assert.That(result.Diagnostics, Is.Empty);
            }
        }

        [Test]
        public void MOreTests()
        {
            var myComp = Assembly.LoadFile(@"c:\temp\MyCompilation.dll");

            var type = myComp.GetType("Furnace.Play.Roslyn.FurnaceItem");

            System.Console.WriteLine(type);

            var json = "{TestString:123}";

            var result = (IFurnaceItem)TypeSerializer.DeserializeFromString(json, type);
            result.SetFurnaceItem(new FurnaceItemInformation());

            Assert.That(result, Is.AssignableTo<IFurnaceItem>());
            Assert.That(result, Is.AssignableTo<TestClass>());

            var testClass = (TestClass)result;

            Assert.That(testClass.TestString, Is.EqualTo("123"));

        }

        public class BaseClassInserter : CSharpSyntaxRewriter
        {
            private string _baseClass;

            public BaseClassInserter(string baseClass)
            {
                _baseClass = baseClass;
            }

            public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
            {
                var tt = SyntaxFactory.ParseTypeName(_baseClass);

                var typesList = new List<TypeSyntax> { tt };
                typesList.AddRange(node.BaseList.Types);

                var types = new SeparatedSyntaxList<TypeSyntax>();
                types = types.AddRange(typesList);

                //SyntaxFactory.BaseList();
                var updated = node.Update(node.AttributeLists,
                    node.Modifiers,
                    node.Keyword,
                    node.Identifier,
                    node.TypeParameterList,
                    SyntaxFactory.BaseList(types),
                    node.ConstraintClauses,
                    node.OpenBraceToken,
                    node.Members,
                    node.CloseBraceToken,
                    node.SemicolonToken).NormalizeWhitespace();

                return updated;
            }
        }
    }
}
