using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Redis;

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
            var key = RedisBackedFurnaceItems.CreateItemChridrenKey(Id, typeof(Stub));
            var a = Substitute.For<IRedisSortedSet>();

            Client.SortedSets[key].Returns(a);
        }

        [Test]
        public void CanGenerateItemChildrenKey()
        {
            const long id = 99L;
            var type = typeof(Stub);
            var key = RedisBackedFurnaceItems.CreateItemChridrenKey(id, type);

            Assert.That(key, Is.EqualTo(RedisBackedFurnaceItems.ItemChridrenSortedSetKey.FormatWith(type.Namespace, type.Name, id)));
        }

        [Test]
        public void SomeTest()
        {
            
        }
    }
}
