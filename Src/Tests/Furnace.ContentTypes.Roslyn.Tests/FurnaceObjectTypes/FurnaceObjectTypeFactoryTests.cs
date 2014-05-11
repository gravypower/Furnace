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
        protected string TemplteFilePath;
        protected string TempltePath;

        [SetUp]
        public void SetUp()
        {
            TempltePath = Path.GetDirectoryName(GetType().Assembly.Location);
            TemplteFilePath = TempltePath + @"\FurnaceObjectTypes\FurnaceObjectType.cs";
        }

        [Test]
        public void FurnaceObjectTypeTemplateExists()
        {            
            Assert.That(File.Exists(TemplteFilePath), Is.True);
        }

       
    }
}
