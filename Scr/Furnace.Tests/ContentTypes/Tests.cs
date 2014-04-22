using Furnace.ContentTypes;
using Furnace.ContentTypes.TypeFinders;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes
{
    [TestFixture]
    public class Tests
    {
        protected IFurnaceContentTypes Sut;
        protected ITypeFinder TypeFinder;

        [SetUp]
        public void SetUp()
        {
            TypeFinder = Substitute.For<ITypeFinder>();
            Sut = new FurnaceContentTypes(TypeFinder);
        }
    }
}
