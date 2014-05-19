using System.Linq;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
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
            var type = typeof (Stub);
            ContentType = new ContentType { Name = type.Name, Namespace = type.Namespace };
            var fi = new FurnaceItemInformation<long> {Id = Id, ContentTypeFullName = ContentType.FullName};
            var stub = AddStub(fi);
            Client.SortedSets[_key].Returns(_fakeRedisSortedSet);

            var returnJson = new Stub(fi) {Test = "Test"}.BuildSerialisedString();

            var key = RedisBackedFurnaceItems.CreateItemKey(fi.Id, type);

            Client.GetValue(key).Returns(returnJson);

            ContentTypes.GetContentTypes().Returns(new[] { ContentType });

            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ContentType, Is.EqualTo(ContentType));
        }

        private Stub AddStub(FurnaceItemInformation<long> furnaceItemInformation)
        {
            var stub = new Stub(furnaceItemInformation);
            var itemKey = RedisBackedFurnaceItems.CreateItemKey(furnaceItemInformation.Id, typeof(Stub));
            _fakeRedisSortedSet.Add(itemKey);
            Client.Hashes[itemKey][SiteConfiguration.DefaultSiteCulture.Name].Returns(stub.BuildSerialisedString());
            return stub;
        }
    }
}
