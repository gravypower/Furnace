using System.Linq;
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

        public ItemHierarchyTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [SetUp]
        public void ItemHierarchyTestsSetUp()
        {
            _key = RedisBackedFurnaceItems.CreateItemChridrenKey(Id, typeof(Stub));
            _fakeRedisSortedSet = new FakeRedisSortedSet();
        }

        [Test]
        public void CanGenerateItemChildrenKey()
        {
            var type = typeof(Stub);
            var key = RedisBackedFurnaceItems.CreateItemChridrenKey(Id, type);

            Assert.That(key, Is.EqualTo(RedisBackedFurnaceItems.ItemChridrenSortedSetKey.FormatWith(type.Namespace, type.Name, Id)));
        }

        //[Test]
        //public void GiveItemHasOneChild_WhenGetItemChildresIsCalled_OneItemReturned()
        //{
        //    //Assign
        //    var stub = AddStub(1);
        //    Client.SortedSets[_key].Returns(_fakeRedisSortedSet);

        //    //Act
        //    var result = Sut.GetItemChildren<Stub>(Id).ToList();
            
        //    //Assert
        //    Assert.That(result.Count(), Is.EqualTo(1));
        //    Assert.That(result.First(), Is.EqualTo(stub));
        //}

        private Stub AddStub(long id)
        {
            var stub = new Stub();
            _fakeRedisSortedSet.Add(RedisBackedFurnaceItems.CreateItemChridrenKey(Id, typeof(Stub)));
            var itemKey = RedisBackedFurnaceItems.CreateItemKey(id, typeof (Stub));
            Client.Hashes[itemKey][SiteConfiguration.DefaultSiteCulture.Name].Returns(stub.BuildSerialisedString());
            return stub;
        }
    }
}
