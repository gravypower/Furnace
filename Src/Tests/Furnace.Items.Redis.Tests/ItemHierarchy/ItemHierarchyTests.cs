using System.Linq;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;
using NSubstitute;
using NUnit.Framework;
using ServiceStack;

namespace Furnace.Items.Redis.Tests.ItemHierarchy
{
    [TestFixture]
    public class ItemHierarchyTests : RedisBackedFurnaceItemsTests
    {
        private string _key;
        private FakeRedisSortedSet _fakeRedisSortedSet;
        private const long Id = 100L;

        [SetUp]
        public void ItemHierarchyTestsSetUp()
        {
            _key = RedisBackedFurnaceItems.CreateItemChildrenKey(Id, typeof(Stub));
            _fakeRedisSortedSet = new FakeRedisSortedSet();
            
        }

        [Test]
        public void CanGenerateItemChildrenKey()
        {
            var type = typeof(Stub);
            var key = RedisBackedFurnaceItems.CreateItemChildrenKey(Id, type);

            Assert.That(key, Is.EqualTo(RedisBackedFurnaceItems.ItemChildrenSortedSetKey.FormatWith(type.Namespace, type.Name, Id)));
        }

        [Test]
        public void GiveItemHasOneChild_WhenGetItemChildresIsCalled_OneItemReturned()
        {
            //Assign
            var test = "Test";
            AddStub(Id, test);

            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ContentType.FullName, Is.EqualTo(typeof(Stub).FullName));
            Assert.That(result.First()["Test"], Is.EqualTo(test));
        }

        [Test]
        public void GiveItemHasOneChild_WhenGetItemChildresIsCalled_OneItemCanBeConvertedToCorrectType()
        {
            //Assign
            var test = "Test";
            AddStub(Id, test);
           
            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().As<Stub>(), Is.TypeOf<Stub>());
            Assert.That(result.First().As<Stub>().Test, Is.EqualTo("Test"));
        }

        private void AddStub(long id, string test)
        {
            var type = typeof(Stub);
            var contentType = new ContentType { Name = type.Name, Namespace = type.Namespace };

            var furnaceItemInformation = new FurnaceItemInformation<long> { Id = id, ContentTypeFullName = contentType.FullName };

            var stub = new Stub(furnaceItemInformation) {Test = test};

            var itemKey = RedisBackedFurnaceItems.CreateItemKey(furnaceItemInformation.Id, typeof(Stub));
            var key = RedisBackedFurnaceItems.CreateItemKey(Id, type);

            _fakeRedisSortedSet.Add(itemKey);

            var returnJson = stub.BuildSerialisedString();

            Client.Hashes[itemKey][SiteConfiguration.DefaultSiteCulture.Name].Returns(stub.BuildSerialisedString());
            Client.SortedSets[_key].Returns(_fakeRedisSortedSet);
            Client.GetValue(key).Returns(returnJson);
            ContentTypes.GetContentTypes().Returns(new[] { contentType });
        }
    }
}
