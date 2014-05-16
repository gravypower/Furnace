using System.Linq;
using Furnace.ContentTypes.Roslyn.Extensions;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class GivenValidTemplatePathTests : FurnaceObjectTypeFactoryTests
    {
        protected const string FurnaceObjectTypeInterface = "IFurnaceObjectType";

        [SetUp]
        public void GivenValidTemplatePathTestsSetUp()
        {
            Sut = new FurnaceObjectTypeFactorySpy(TemplateFilePath);
        }

        [Test]
        public void SyntaxTreeHasRoot()
        {
            //Assert
            Assert.That(Spy.TemplateClassRoot, Is.Not.Null);
        }

        [Test]
        public void GivenNullFullName_WhenCreateFurnaceTypeCalled_NullFullNameExceptionThrown()
        {
            //Act
            var ex = Assert.Throws<FurnaceObjectTypeFactory.FullNameException>(() => Sut.AddFurnaceType(null));

            //Assert
            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.FullNameException.NullFullName));
        }

        [Test]
        public void GivenEmptyFullName_WhenCreateFurnaceTypeCalled_NullFullNameExceptionThrown()
        {
            //Act
            var ex = Assert.Throws<FurnaceObjectTypeFactory.FullNameException>(() => Sut.AddFurnaceType(string.Empty));

            //Assert
            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.FullNameException.EmptyFullName));
        }

        [Test]
        public void GivenFullName_WhenAddingOneFurnaceType_ThenThereIsOneFurnaceTypeThatHasCorrectBaseTypes()
        {
            //Assign
            const string typeNamespace = "Some";
            const string typeName = "Name";
            const string fullName = typeNamespace + "." + typeName;

            //Act
            Sut.AddFurnaceType(fullName);

            //Assert
            Assert.That(Spy.FurnaceTypes.Count(), Is.EqualTo(1));
            AssertFurnaceType(Spy.FurnaceTypes.First().SyntaxTree, typeName, typeNamespace);
        }

        [Test]
        public void GivenFullName_WhenAddingTwoFurnaceType_ThenThereIsOneFurnaceTypeThatHasCorrectBaseTypes()
        {
            //Assign
            const string typeNamespace = "Some";
            const string typeNameOne = "Name";
            const string typeNameTwo = "Name";
            const string fullNameOne = typeNamespace + "." + typeNameOne;
            const string fullNameTwo = typeNamespace + "." + typeNameTwo;

            //Act
            Sut.AddFurnaceType(fullNameOne);
            Sut.AddFurnaceType(fullNameTwo);

            //Assert
            Assert.That(Spy.FurnaceTypes.Count(), Is.EqualTo(2));
            var types = Spy.FurnaceTypes.ToList();

            AssertFurnaceType(types[0].SyntaxTree, typeNameOne, typeNamespace);
            AssertFurnaceType(types[1].SyntaxTree, typeNameTwo, typeNamespace);
        }

        private static void AssertFurnaceType(SyntaxTree tree, string typeName, string typeNamespace)
        {
            var item = tree.GetRoot();
            var classDeclarationSyntax = item.DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            Assert.That(classDeclarationSyntax.BaseList.Types[0].ToString(), Is.EqualTo(typeNamespace + "." +typeName));
            Assert.That(classDeclarationSyntax.BaseList.Types[1].ToString(), Is.EqualTo(FurnaceObjectTypeInterface));
            Assert.That(classDeclarationSyntax.Identifier.Text, TypeNameIsCorrect(typeName));
            Assert.That(item.PropertyDeclarationNodes(), HasCorrectIdentifier());
        }

        private static EqualConstraint TypeNameIsCorrect(string fullNameOne)
        {
            return Is.EqualTo(Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + fullNameOne);
        }

        private static Constraint HasCorrectIdentifier()
        {
            return Has.Some.Matches<PropertyDeclarationSyntax>(x=>x.Identifier.Text == "FurnaceItemInformation");
        }
    }
}

