using System;
using NSubstitute;
using NUnit.Framework;
using ServiceStack;

namespace Furnace.Items.Redis.Tests.ItemHierarchy
{
    [TestFixture]
    public class ItemHierarchyTests : RedisBackedFurnaceItemsTests
    {
        public ItemHierarchyTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [SetUp]
        public void ItemHierarchyTestsSetUp()
        {
            const long Id = 99L;
            var key = RedisBackedFurnaceItems.CreateItemKey(Id, typeof(Stub));
            var defultCultureStub = new Stub { Test = "Hello" };
            //Client.SortedSets[key]
        }

        [Test]
        public void CanGenerateItemChildrenKey()
        {
            const long id = 99L;
            var type = typeof(Stub);
            var key = RedisBackedFurnaceItems.CreateItemChridrenKey(id, type);

            Assert.That(key, Is.EqualTo(RedisBackedFurnaceItems.ItemChridrenSortedSetKey.FormatWith(type.Namespace, type.Name, id)));
        }
    }
}
