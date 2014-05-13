using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class CreatingFurnaceObjectTypeFactoryTests : FurnaceObjectTypeFactoryTests
    {
        [Test]
        public void GivenNullTemplatePath_WhenFurnaceObjectTypeFactoryCreated_TemplatePathExceptionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TemplatePathException>(() => new FurnaceObjectTypeFactory(null));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TemplatePathException.NullTemplatePath));
        }

        [Test]
        public void GivenEmptyTemplatePath_WhenFurnaceObjectTypeFactoryCreated_TemplatePathExceptionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TemplatePathException>(() => new FurnaceObjectTypeFactory(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TemplatePathException.EmptyTemplatePath));
        }

        [Test]
        public void GivenInvalidTemplatePath_WhenFurnaceObjectTypeFactoryCreated_TemplatePathExceptionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TemplatePathException>(() => new FurnaceObjectTypeFactory("InvalidPath"));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TemplatePathException.InvalidTemplatePath));
        }

        [Test]
        public void GivenValidTemplatePath_WhenFurnaceObjectTypeFactoryCreated_TemplatePathExceptionNotThrown()
        {
            var result = new FurnaceObjectTypeFactory(TemplteFilePath);
        }
    }
}
