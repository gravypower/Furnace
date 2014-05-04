namespace Furnace.Items.Redis.Tests
{
    using Furnace.Tests.Items.GivenContentType.WithNameAndNamespace;

    using NSubstitute;

    using NUnit.Framework;

    using ServiceStack.Redis;

    [TestFixture]
    public class RedisBackedFurnaceItemsTests : WithNameAndNamespaceTests
    {
        protected IRedisClient Client;

        [SetUp]
        public void RedisBackedFurnaceItemsTestsSetUp()
        {
            Client = Substitute.For<IRedisClient>();
            Sut = new RedisBackedFurnaceItems(Client);
        }

        [Test]
        public void GivenNoItemWithID_WhenGetItemIsCalled_ThenNullReturned()
        {
            //Assign
            const long Id = 1L;

            AddPropityToContentType("SomeName", "SomeType");

            //Act
            var result = Sut.GetItem(Id, ContentType);

            //Assert
            Assert.That(result, Is.Null);

        }

        [Test]
        public void WhenGetItemIsCalled_ThenTheReturnedItem_IsCorrect()
        {
            //Assign
            const long Id = 1L;
            AddPropityToContentType("Test", "string");
            var item = Sut.CreateItem(ContentType);
            item.Id = Id;

            const string PropityValue = "NotDefaultValue";
            const string ReturnJon = "{\"Test\":\""+ PropityValue + "\"}";

            var key = RedisBackedFurnaceItems.CreateItemKey(Id, ContentType);

            Client.GetValue(key).Returns(ReturnJon);

            //Act
            var result = Sut.GetItem(Id, ContentType);

            //Assert
            Assert.That(result.Id, Is.EqualTo(Id));
            Assert.That(result["Test"], Is.EqualTo(PropityValue));
            Assert.That(result.ContentType.Name == ContentTypeName);
        }

        [Test]
        public void WhenStoreItemIsCalled_ThenJSON_IsCorrect()
        {
            //Assign
            const long Id = 1L;
            AddPropityToContentType("Test", "string");
            var item = Sut.CreateItem(ContentType);
            item.Id = Id;

            //Act
            Sut.SetItem(Id, item);

            //Assert
            var key = RedisBackedFurnaceItems.CreateItemKey(Id, ContentType);
            Client.Received().Set(key, item.Propities);
        }
    }
}
