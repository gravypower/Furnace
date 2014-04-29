namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject
{
    using NUnit.Framework;

    [TestFixture]
    public abstract class GivenProjectTests : ContentTypesTests
    {
        protected abstract string ExpectedNamespace { get; }
    }
}
