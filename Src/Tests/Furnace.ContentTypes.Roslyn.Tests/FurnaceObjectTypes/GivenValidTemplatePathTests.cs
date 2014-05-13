using System.Linq;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class GivenValidTemplatePathTests : FurnaceObjectTypeFactoryTests
    {
        protected const string FurnaceObjectTypeInterface = "IFurnaceObjectType";

        [SetUp]
        public void GivenValidTemplatePathTestsSetUp()
        {
            Sut = new FurnaceObjectTypeFactorySpy(TemplteFilePath);
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
            var ex = Assert.Throws<FurnaceObjectTypeFactory.FullNameException>(() => Sut.AddFurnaceType(null));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.FullNameException.NullFullName));
        }

        [Test]
        public void GivenEmptyFullName_WhenCreateFurnaceTypeCalled_NullFullNameExceptionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.FullNameException>(() => Sut.AddFurnaceType(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.FullNameException.EmptyFullName));
        }

        [Test]
        public void GivenFullName_WhenAddingOneFurnaceType_ThenThereIsOneFurnaceTypeThatHasCorrectBaseTypes()
        {
            const string fullName = "SomeFullName";
            Sut.AddFurnaceType(fullName);

            Assert.That(Spy.FurnaceTypes.Count(), Is.EqualTo(1));

            var assertItem = Spy.FurnaceTypes.First().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            AssertFurnaceType(assertItem, fullName);
        }

        [Test]
        public void GivenFullName_WhenAddingTwoFurnaceType_ThenThereIsOneFurnaceTypeThatHasCorrectBaseTypes()
        {
            const string fullNameOne = "SomeFullName";
            const string fullNameTwo = "SomeFullNameTwo";
            Sut.AddFurnaceType(fullNameOne);

            Sut.AddFurnaceType(fullNameTwo);

            Assert.That(Spy.FurnaceTypes.Count(), Is.EqualTo(2));
            var types = Spy.FurnaceTypes.ToList();

            var firstItem = types[0].DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            AssertFurnaceType(firstItem, fullNameOne);

            var secondItem = types[1].DescendantNodes().OfType<ClassDeclarationSyntax>().First();
            AssertFurnaceType(secondItem, fullNameTwo);

        }

        private static void AssertFurnaceType(BaseTypeDeclarationSyntax firstItem, string fullNameOne)
        {
            Assert.That(firstItem.BaseList.Types[0].ToString(), Is.EqualTo(fullNameOne));
            Assert.That(firstItem.BaseList.Types[1].ToString(), Is.EqualTo(FurnaceObjectTypeInterface));
        }
    }
}
