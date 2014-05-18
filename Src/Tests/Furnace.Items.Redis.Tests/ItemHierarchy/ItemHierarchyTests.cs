using NSubstitute;
using NUnit.Framework;
using ServiceStack;

namespace Furnace.Items.Redis.Tests.ItemHierarchy
{
    [TestFixture]
    public class ItemHierarchyTests : RedisBackedFurnaceItemsTests
    {
        private const long Id = 100L;

        public ItemHierarchyTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [SetUp]
        public void ItemHierarchyTestsSetUp()
        {
            var key = RedisBackedFurnaceItems.CreateItemChridrenKey(Id, typeof(Stub));
            Client.SortedSets[key].Returns(new FakeRedisSortedSet());
        }

        [Test]
        public void CanGenerateItemChildrenKey()
        {
            var type = typeof(Stub);
            var key = RedisBackedFurnaceItems.CreateItemChridrenKey(Id, type);

            Assert.That(key, Is.EqualTo(RedisBackedFurnaceItems.ItemChridrenSortedSetKey.FormatWith(type.Namespace, type.Name, Id)));
        }

        [Test]
        public void SomeTest()
        {
            var result = Sut.GetItem<Stub>(Id);
        }
    }
}
