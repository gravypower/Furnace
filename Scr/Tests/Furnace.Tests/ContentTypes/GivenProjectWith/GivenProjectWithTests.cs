using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith
{
    [TestFixture]
    public abstract class GivenProjectWithTests : ContentTypesTests
    {
        protected abstract string ExpectedNamespace { get; }
    }
}
