using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests
{
    [TestFixture]
    public class GivenNoProject
    {
        [Test]
        public void WhenCreatingNewRoslynContentTypes_EmptyStringAsProjectPath_ThenExceptionThrown()
        {
            Assert.That(() => new RoslynContentTypes(string.Empty, string.Empty), Throws.Exception.TypeOf<RoslynContentTypes.InvalidProjectPathException>());
        }

        [Test]
        public void WhenCreatingNewRoslynContentTypes_NullAsProjectPath_ThenExceptionThrown()
        {
            Assert.That(() => new RoslynContentTypes(null, null), Throws.Exception.TypeOf<RoslynContentTypes.InvalidProjectPathException>());
        }
    }
}
