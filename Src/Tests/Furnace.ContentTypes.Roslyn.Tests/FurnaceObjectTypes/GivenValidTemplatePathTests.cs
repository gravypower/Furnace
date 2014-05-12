using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class GivenValidTemplatePathTests : FurnaceObjectTypeFactoryTests
    {
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
        public void SomeTest()
        {
            Sut.CreateFurnaceType("Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.TestType");
        }
    }
}
