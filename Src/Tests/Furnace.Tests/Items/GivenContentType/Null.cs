namespace Furnace.Tests.Items.GivenContentType
{
    using Furnace.Items;

    using NUnit.Framework;

    public class Null : GivenContentTypeTests
    {
        [SetUp]
        public void WithNullSetUp()
        {
            ContentType = null;
        }

        [Test]
        public void WhenCreateItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            var ex = Assert.Throws<FurnaceItems.NullContentTypeException>(() => Sut.CreateItem(ContentType));
        }

        [Test]
        public void WhenGetItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            const string key = "SomeKey";
            var ex = Assert.Throws<FurnaceItems.NullContentTypeException>(() => Sut.GetItem(key, ContentType));
        }
    }
}
