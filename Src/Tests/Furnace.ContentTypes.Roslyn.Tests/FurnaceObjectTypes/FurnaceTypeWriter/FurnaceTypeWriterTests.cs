using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.FurnaceTypeWriter
{
    [TestFixture]
    public class FurnaceTypeWriterTests
    {
        public string BaseClass = "Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.FurnaceTypeWriter.TestClasses.";

        private const string TestClassPath = @"FurnaceObjectTypes\FurnaceTypeWriter\TestClasses\";

        [Test]
        public void GivenNullBaseClass_WhenBaseClassInserterCreated_BaseClassExceptionThrown()
        {
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.BaseClassException>(() => new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(null));

            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.BaseClassException.NullBaseClassFullName));
        }

        [Test]
        public void GivenEmptyBaseClass_WhenBaseClassInserterCreated_TempltePathExceptionThrown()
        {
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.BaseClassException>(() => new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.BaseClassException.EmptyBaseClassFullName));
        }

        [Test]
        public void GivenEmptyClass_WhenVisitingClassDeclarationSyntax_TempltePathExceptionThrown()
        {
            //Assign
            var sut = new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(BaseClass);
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + "EmptyClass.cs");
            var root = tree.GetRoot();
            //Act
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.BaseClassException>(() => sut.Visit(root));

            //Assert
            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.BaseClassException.EmptyBaseClass));
        }

        [Test]
        public void GivenOneClass_WhenVisitingClassDeclarationSyntax_TempltePathExceptionNotThrown()
        {
            //Assign
            const string className = "OneClass";
            var sut = new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(BaseClass + className);
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + className + ".cs");
            var root = tree.GetRoot();

            //Act
            sut.Visit(root);
        }

        [Test]
        public void GivenOneClass_WhenVisitingClassDeclarationSyntax_BaseTypeAdded()
        {
            //Assign
            const string className = "OneClass";
            var sut = new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(BaseClass + className);
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + className + ".cs");
            var root = tree.GetRoot();

            //Act
            var result = sut.Visit(root).DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            //Assert
            Assert.That(result.BaseList.Types.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GivenOneClass_WhenVisitingClassDeclarationSyntax_NewTypeNameIsCorrect()
        {
            //Assign
            const string className = "OneClass";
            var sut = new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(BaseClass + className);
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + className + ".cs");
            var root = tree.GetRoot();

            //Act
            var result = sut.Visit(root).DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            
            //Assert
            Assert.That(result.Identifier.Text, Is.EqualTo(Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + className));
        }

        [Test]
        public void GivenClassWithInterface_WhenVisitingClassDeclarationSyntax_BaseTypeAdded()
        {
            //Assign
            const string className = "ClassWithInterface";
            var sut = new Roslyn.FurnaceObjectTypes.FurnaceTypeWriter(BaseClass + className);
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + className + ".cs");
            var root = tree.GetRoot();

            //Act
            var result = sut.Visit(root).DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            //Assert
            Assert.That(result.BaseList.Types.Count(), Is.EqualTo(2));
        }
    }
}
