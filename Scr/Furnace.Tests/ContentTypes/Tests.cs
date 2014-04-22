using Furnace.ContentTypes;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes
{
    [TestFixture]
    public class Tests
    {
        protected FurnaceContentTypes Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = Substitute.For<FurnaceContentTypes>();
        }
    }
}
