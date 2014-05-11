using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class CreatingFurnaceObjectTypeFactoryTests : FurnaceObjectTypeFactoryTests
    {
        [Test]
        public void GivenNullTemplatePath_WhenFurnaceObjectTypeFactoryCreated_NullTemplateExcpetionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TempltePathException>(() => new FurnaceObjectTypeFactory(null));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TempltePathException.NullTempltePath));
        }

        [Test]
        public void GivenEmptyTemplatePath_WhenFurnaceObjectTypeFactoryCreated_NullTemplateExcpetionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TempltePathException>(() => new FurnaceObjectTypeFactory(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TempltePathException.EmptyTempltePath));
        }

        [Test]
        public void GivenInvalidTemplatePath_WhenFurnaceObjectTypeFactoryCreated_NullTemplateExcpetionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TempltePathException>(() => new FurnaceObjectTypeFactory("InvalidPath"));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TempltePathException.InvalidTempltePath));
        }

        [Test]
        public void GivenValidTemplatePath_WhenFurnaceObjectTypeFactoryCreated_NullTemplateExcpetionNotThrown()
        {
            var result = new FurnaceObjectTypeFactory(TemplteFilePath);
        }
    }
}
