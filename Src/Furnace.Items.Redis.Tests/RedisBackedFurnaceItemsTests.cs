namespace Furnace.Items.Redis.Tests
{
    using Furnace.Tests.Items.GivenContentType;
    using Furnace.Tests.Items.GivenContentType.WithNameAndNamespace;

    using Models.ContentTypes;
    using Models.Items;

    using NSubstitute;

    using NUnit.Framework;

    using ServiceStack;
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
            const string FullyQualifiedContentName = "Some.Test.Name";
            const string ID = "SomeId";
            var itemId = RedisBackedFurnaceItems.ItemsSetId.FormatWith(FullyQualifiedContentName, ID);

            AddPropityToContentType("SomeName", "SomeType");

            //Act
            var result = Sut.GetItem(itemId, ContentType);

            //Assert
            Assert.That(result, Is.Null);

        }

        //[Test]
        //public void WhenGetItemIsCalled_ThenTheReturnedItem_HasCorrectId()
        //{
        //    //Assign
        //    var id = 1L;
        //    AddPropityToContentType(PropertyName, PropertyType);
        //    var item = Sut.CreateItem(ContentType);
        //    item.Id = guid;

        //    ItemRepository.GetById(guid).Returns(string.Empty);

        //    //Act
        //    var result = Sut.GetItem(guid, ContentType);

        //    Assert.That(result.Id, Is.EqualTo(guid));
        //}

        //[Test]
        //public void WhenGetItemIsCalled_ThenTheReturnedItem_HasCorrectJSON()
        //{
        //    //Assign
        //    var guid = new Guid("0bc8a24e-467d-40b9-aed7-81cdcaffbdbe");
        //    AddPropityToContentType(PropertyName, PropertyType);
        //    var item = Sut.CreateItem(ContentType);
        //    item.Id = guid;

        //    ItemRepository.GetById(guid).Returns(string.Empty);

        //    //Act
        //    var result = Sut.GetItem(guid, ContentType);

        //    Assert.That(result.Id, Is.EqualTo(guid));
        //}
    }
}
