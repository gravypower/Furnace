namespace Furnace.Items.Redis.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    using ServiceStack.Redis;

    [TestFixture]
    public class RedisBackedFurnaceItemsTests
    {
        protected IRedisClient Client;

        protected RedisBackedFurnaceItems Sut;

        [SetUp]
        public void RedisBackedFurnaceItemsTestsSetUp()
        {
            Client = Substitute.For<IRedisClient>();
            Sut = new RedisBackedFurnaceItems(this.Client);
        }

        //[Test]
        //public void SomeTest()
        //{
        //    //Assign
        //    const string FullyQualifiedContentName = "Some.Test.Name";
        //    var id = "SomeId";
        //    var itemId = RedisBackedFurnaceItems.ItemsSetId.FormatWith(FullyQualifiedContentName, id);

        //    AddPropityToContentType(PropertyName, PropertyType);
        //    var item = Sut.CreateItem(ContentType);

        //    Client.GetById<Item>(itemId).Returns(item);

        //    //Act
        //    var result = Sut.GetItem(id, ContentType);

        //    //Assert
        //    Assert.That(result, Is.EqualTo(item));

        //}

        //[Test]
        //public void WhenGetItemIsCalled_ThenTheReturnedItem_HasCorrectId()
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
