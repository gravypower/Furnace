using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject
{
    [TestFixture]
    public abstract class GivenProjectTests : ContentTypesTests
    {
        protected const string BaseNamespace = "Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses.TestClasses.";

        protected abstract string ExpectedNamespace { get; }

        protected ITypeFinder TypeFinder = new TypeFinder();
    }
}
