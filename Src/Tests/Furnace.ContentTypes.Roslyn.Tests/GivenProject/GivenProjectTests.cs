using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject
{
    [TestFixture]
    public abstract class GivenProjectTests : ContentTypesTests
    {
        protected abstract string ExpectedNamespace { get; }
    }
}
