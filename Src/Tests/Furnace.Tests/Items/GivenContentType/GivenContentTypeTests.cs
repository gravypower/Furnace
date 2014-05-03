using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType
{
    using Furnace.Items;

    public abstract class GivenContentTypeTests : FurnaceItemsTests
    {
        protected ContentType ContentType;

        public class WithNull : GivenContentTypeTests
        {
            [SetUp]
            public void WithNullSetUp()
            {
                ContentType = null;
            }

            [Test]
            public void WhenCreateItemIsCalled_ThenInvalidContentTypeException_IsThrown()
            {
                var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NullContentType));
            }

            [Test]
            public void WhenGetItemIsCalled_ThenInvalidContentTypeException_IsThrown()
            {
                var key = "SomeKey";
                var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.GetItem(key, ContentType));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NullContentType));
            }
        }

    }
}
