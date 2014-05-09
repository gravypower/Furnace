using System.IO;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class FurnaceObjectTypeFactoryTests
    {
        public FurnaceObjectTypeFactory Sut;
        public FurnaceObjectTypeFactorySpy Spy;
        private string _templteFilePath;
        private string _templtePath;

        [SetUp]
        public void SetUp()
        {
            _templtePath = Path.GetDirectoryName(GetType().Assembly.Location);
            _templteFilePath = _templtePath + @"\FurnaceObjectTypes\FurnaceObjectType.cs";
            Sut = new FurnaceObjectTypeFactorySpy(_templteFilePath);
        }

        [Test]
        public void FurnaceObjectTypeTemplateExists()
        {            
            Assert.That(File.Exists(_templteFilePath), Is.True);
        }

        [Test]
        public void GivenNullTemplatePath_WhenParseFurnaceObjectTypeTemplateIsCalled_NullTemplateExcpetionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TempltePathException>(() => new FurnaceObjectTypeFactory(null));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TempltePathException.NullTempltePath));
        }

        [Test]
        public void GivenEmptyTemplatePath_WhenParseFurnaceObjectTypeTemplateIsCalled_NullTemplateExcpetionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TempltePathException>(() => new FurnaceObjectTypeFactory(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TempltePathException.EmptyTempltePath));
        }

        [Test]
        public void GivenInvalidTemplatePath_WhenParseFurnaceObjectTypeTemplateIsCalled_NullTemplateExcpetionThrown()
        {
            var ex = Assert.Throws<FurnaceObjectTypeFactory.TempltePathException>(() => new FurnaceObjectTypeFactory("InvalidPath"));

            Assert.That(ex.InvalidReasons, Contains.Item(FurnaceObjectTypeFactory.TempltePathException.InvalidTempltePath));
        }

        [Test]
        public void GivenGoodTemplatePath_WhenParseFurnaceObjectTypeTemplateIsCalled_NullTemplateExcpetionNotThrown()
        {
            var result = new FurnaceObjectTypeFactory(_templteFilePath);
        }
    }
}
