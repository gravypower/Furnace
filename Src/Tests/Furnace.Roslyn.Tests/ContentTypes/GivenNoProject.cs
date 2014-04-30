using NUnit.Framework;

namespace Furnace.Roslyn.Tests.ContentTypes
{
    [TestFixture]
    public class GivenNoProject
    {
        [Test]
        public void WhenCreatingNewRoslynContentTypes_EmptyStringAsProjectPath_ThenExceptionThrown()
        {
            Assert.That(() => new RoslynContentTypes(string.Empty), Throws.Exception.TypeOf<RoslynContentTypes.InvalidProjectPathException>());
        }

        [Test]
        public void WhenCreatingNewRoslynContentTypes_NullAsProjectPath_ThenExceptionThrown()
        {
            Assert.That(() => new RoslynContentTypes(null), Throws.Exception.TypeOf<RoslynContentTypes.InvalidProjectPathException>());
        }
    }
}
