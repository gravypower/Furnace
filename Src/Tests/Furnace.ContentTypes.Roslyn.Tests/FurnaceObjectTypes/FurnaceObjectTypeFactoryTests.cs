using System.IO;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public abstract class FurnaceObjectTypeFactoryTests
    {
        public FurnaceObjectTypeFactory Sut;
        public FurnaceObjectTypeFactorySpy Spy { get { return Sut as FurnaceObjectTypeFactorySpy; } }
        protected string TemplateFilePath;
        protected string TemplatePath;

        [SetUp]
        public void SetUp()
        {
            TemplatePath = Path.GetDirectoryName(GetType().Assembly.Location);
            TemplateFilePath = TemplatePath + @"\FurnaceObjectTypes\FurnaceObjectType.cs";
        }

        [Test]
        public void FurnaceObjectTypeTemplateExists()
        {            
            Assert.That(File.Exists(TemplateFilePath), Is.True);
        }
    }
}
