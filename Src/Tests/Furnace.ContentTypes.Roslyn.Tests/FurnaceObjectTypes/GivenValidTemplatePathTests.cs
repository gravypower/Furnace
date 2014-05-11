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
    }
}
