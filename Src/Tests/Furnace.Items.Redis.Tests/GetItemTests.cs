using NSubstitute;
using NUnit.Framework;

namespace Furnace.Items.Redis.Tests
{
    [TestFixture]
    public class GetItemTests : RedisBackedFurnaceItemsTests
    {
        public GetItemTests(string furnaceItemsType) : base(furnaceItemsType)
        {
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
            var returnJon = new Stub { Test = propityValue }.BuildSerialisedString();

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
        public void SomeTEst()
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
            var result = Sut.GetItem(key);

            //Assert
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result["Test"], Is.EqualTo(propityValue));
            Assert.That(result.ContentType.Name == ContentTypeName);
        }
    }
}
