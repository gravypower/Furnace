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
        private string _templtePath;

        [SetUp]
        public void SetUp()
        {
            Sut = new FurnaceObjectTypeFactorySpy();
            _templtePath = Path.GetDirectoryName(GetType().Assembly.Location) + @"\FurnaceObjectTypes\FurnaceObjectType.cs";
        }

        [Test]
        public void FurnaceObjectTypeTemplateExists()
        {
            
            Assert.That(File.Exists(_templtePath), Is.True);
        }

        [Test]
        public void GiveNullTemplatePath_WhenParseFurnaceObjectTypeTemplateIsCalled_NullTemplateExcpetionThrown()
        {
            Assert.That(() => Sut.ParseFurnaceObjectTypeTemplate(null), Throws.TypeOf<FurnaceObjectTypeFactory.TempltePathException>() ); 
        }
    }
}
