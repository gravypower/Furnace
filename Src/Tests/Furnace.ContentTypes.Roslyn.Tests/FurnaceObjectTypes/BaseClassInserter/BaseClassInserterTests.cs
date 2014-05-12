using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.BaseClassInserter
{
    [TestFixture]
    public class BaseClassInserterTests
    {
        public Roslyn.FurnaceObjectTypes.BaseClassInserter Sut;
        public string BaseClass = "Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.BaseClassInserter.TestClasses.TestType";

        private const string TestClassPath = @"FurnaceObjectTypes\BaseClassInserter\TestClasses\";

        [SetUp]
        public void SetUp()
        {
            Sut = new Roslyn.FurnaceObjectTypes.BaseClassInserter(BaseClass);
        }

        [Test]
        public void GivenNullBaseClass_WhenBaseClassInserterCreated_BaseClassExceptionThrown()
        {
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException>(() => new Roslyn.FurnaceObjectTypes.BaseClassInserter(null));

            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException.NullBaseClassFullName));
        }

        [Test]
        public void GivenEmptyBaseClass_WhenBaseClassInserterCreated_TempltePathExceptionThrown()
        {
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException>(() => new Roslyn.FurnaceObjectTypes.BaseClassInserter(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException.EmptyBaseClassFullName));
        }

        [Test]
        public void GivenEmptyClass_WhenVisitingClassDeclarationSyntax_TempltePathExceptionThrown()
        {
            //Assign
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + "EmptyClass.cs");
            var root = tree.GetRoot();
            //Act
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException>(() => Sut.Visit(root));

            //Assert
            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException.EmptyBaseClass));
        }

        [Test]
        public void GivenTwoClasses_WhenVisitingClassDeclarationSyntax_TempltePathExceptionThrown()
        {
            //Assign
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + "TwoClasses.cs");
            var root = tree.GetRoot();
            //Act
            var ex = Assert.Throws<Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException>(() => Sut.Visit(root));

            //Assert
            Assert.That(ex.InvalidReasons, Contains.Item(Roslyn.FurnaceObjectTypes.BaseClassInserter.BaseClassException.MoreThanOneClass));
        }

        [Test]
        public void GivenOneClass_WhenVisitingClassDeclarationSyntax_TempltePathExceptionNotThrown()
        {
            //Assign
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + "OneClass.cs");
            var root = tree.GetRoot();

            //Act
            Sut.Visit(root);
        }

        [Test]
        public void GivenOneClass_WhenVisitingClassDeclarationSyntax_BaseTypeAdded()
        {
            //Assign
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + "OneClass.cs");
            var root = tree.GetRoot();

            //Act
            var result = Sut.Visit(root).DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            //Assert
            Assert.That(result.BaseList.Types.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GivenClassWithInterface_WhenVisitingClassDeclarationSyntax_BaseTypeAdded()
        {
            //Assign
            var tree = CSharpSyntaxTree.ParseFile(TestClassPath + "ClassWithInterface.cs");
            var root = tree.GetRoot();

            //Act
            var result = Sut.Visit(root).DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            //Assert
            Assert.That(result.BaseList.Types.Count(), Is.EqualTo(2));
        }
    }
}
