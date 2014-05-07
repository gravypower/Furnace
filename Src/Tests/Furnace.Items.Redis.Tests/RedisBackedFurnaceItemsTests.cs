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

        public RedisBackedFurnaceItemsTests(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

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
            const long id = 1L;

            AddPropityToContentType("SomeName", "SomeType");

            //Act
            var result = Sut.GetItem(id, ContentType);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void WhenGetItemIsCalled_ThenTheReturnedItem_IsCorrect()
        {
            //Assign
            const long id = 1L;
            AddPropityToContentType("Test", "string");
            var item = Sut.CreateItem(ContentType);
            item.Id = id;

            const string propityValue = "NotDefaultValue";
            var returnJon = new Stub { Test = propityValue }.BuildSerialisedString();

            var key = RedisBackedFurnaceItems.CreateItemKey(id, ContentType);

            Client.GetValue(key).Returns(returnJon);

            //Act
            var result = Sut.GetItem(id, ContentType);

            //Assert
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result["Test"], Is.EqualTo(propityValue));
            Assert.That(result.ContentType.Name == ContentTypeName);
        }

        [Test]
        public void WhenTypedGetItemIsCalled_ThenTheReturnedItem_IsCorrect()
        {
            //Assign
            const long id = 1L;

            const string propityValue = "NotDefault Value";
            var returnJon = new Stub {Test = propityValue}.BuildSerialisedString();

            var key = RedisBackedFurnaceItems.CreateItemKey(id, typeof(Stub));

            Client.GetValue(key).Returns(returnJon);

            //Act
            var result = Sut.GetItem<Stub>(id);

            //Assert
            Assert.That(result.Test, Is.EqualTo(propityValue));
        }

        [Test]
        public void GivenNoItemWithID_WhenTypedGetItemIsCalled_ThenNullReturned()
        {
            //Assign
            const long id = 1L;

            //Act
            var result = Sut.GetItem<Stub>(id);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void WhenStoreItemIsCalled_ThenClientReceives_CorrectObject()
        {
            //Assign
            const long id = 1L;
            AddPropityToContentType("Test", "string");
            var item = Sut.CreateItem(ContentType);
            item.Id = id;

            //Act
            Sut.SetItem(id, item);

            //Assert
            var key = RedisBackedFurnaceItems.CreateItemKey(id, ContentType);
            Client.Received().Set(key, item.Propities);
        }

        public class Stub
        {
            public string Test { get; set; }

            public string BuildSerialisedString()
            {
                return "{Test:" + Test + "}";
            }
        }
    }
}
