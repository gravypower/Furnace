using Furnace.Models.Items;
using Furnace.Tests;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Items.Redis.Tests
{
    [TestFixture]
    public class GetItemTests : RedisBackedFurnaceItemsTests
    {
        [Test]
        public void GivenNoItemWithID_WhenGetItemIsCalled_ThenNullReturned()
        {
            //Assign
            const long id = 1L;

            ContentType.AddPropity("SomeName", "SomeType");

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
            ContentType.AddPropity("Test", "string");
            var item = Sut.CreateItem(ContentType);
            item.Id = id;

            const string propityValue = "NotDefaultValue";
            var fi = new FurnaceItemInformation<long>() {Id = id};
            var returnJon = new Stub(fi) { Test = propityValue }.BuildSerialisedString();

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
            var fi = new FurnaceItemInformation<long>();
            var returnJon = new Stub(fi) { Test = propityValue }.BuildSerialisedString();

            var key = RedisBackedFurnaceItems.CreateItemKey(id, typeof(Stub));

            Client.Hashes[key][Arg.Any<string>()].Returns(returnJon);

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
        public void SomeTest()
        {
            //Assign
            const long id = 1L;

            const string propityValue = "NotDefaultValue";
            var fi = new FurnaceItemInformation<long> {Id = id, ContentTypeFullName = ContentType.FullName};
            var returnJson = new Stub(fi) { Test = propityValue }.BuildSerialisedString();

            var key = RedisBackedFurnaceItems.CreateItemKey(id, ContentType);

            Client.GetValue(key).Returns(returnJson);

            ContentTypes.GetContentTypes().Returns(new[] {ContentType});

            //Act
            var result = Sut.GetItem(key);

            //Assert
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result["Test"], Is.EqualTo(propityValue));
            Assert.That(result.ContentType.Name == ContentTypeName);
        }
    }
}
