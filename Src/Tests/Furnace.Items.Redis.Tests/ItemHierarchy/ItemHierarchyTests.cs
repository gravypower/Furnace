using System;
using System.Collections.Generic;
using System.Linq;
using Furnace.Interfaces.ContentTypes;
using Furnace.Items.Redis.Tests.Stubs;
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
        private FakeRedisSortedSet _childSortedSet;

        private List<IContentType> _contentTypes;

        private const long Id = 100L;

        [SetUp]
        public void ItemHierarchyTestsSetUp()
        {
            _childSortedSet = new FakeRedisSortedSet();
            _contentTypes = new List<IContentType>();

        }

        [Test]
        public void CanGenerateItemChildrenKey()
        {
            var type = typeof(Stub);
            var key = RedisBackedFurnaceItems.CreateItemChildrenKey(Id, type);

            Assert.That(key, Is.EqualTo(RedisBackedFurnaceItems.ItemChildrenSortedSetKey.FormatWith(type.FullName, Id)));
        }

        [Test]
        public void GiveItemHasOneChild_WhenGetItemChildresIsCalled_OneItemReturned()
        {
            //Assign
            var test = "Test";
            AddChild<Stub>(Id, typeof(Stub), 100L, test);

            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].ContentType.FullName, Is.EqualTo(typeof(Stub).FullName));
            Assert.That(result[0]["Test"], Is.EqualTo(test));
        }

        [Test]
        public void GiveItemHasOneChild_WhenGetItemChildresIsCalled_OneItemCanBeConvertedToCorrectType()
        {
            //Assign
            var test = "Test";
            AddChild<Stub>(Id, typeof(Stub), 100L, test);
           
            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].As<Stub>(), Is.TypeOf<Stub>());
            Assert.That(result[0].As<Stub>().Test, Is.EqualTo("Test"));
        }

        [Test]
        public void GiveItemHasTwoChildren_WhenGetItemChildresIsCalled_OneItemReturned()
        {
            //Assign
            var test1 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 100L, test1);

            var test2 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 101L, test2);

            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].ContentType.FullName, Is.EqualTo(typeof(Stub).FullName));
            Assert.That(result[0]["Test"], Is.EqualTo(test1));
            Assert.That(result[1].ContentType.FullName, Is.EqualTo(typeof(Stub).FullName));
            Assert.That(result[1]["Test"], Is.EqualTo(test1));
        }

        [Test]
        public void GiveItemHasTwoChildren_WhenGetSiblingsIsCalled_TwoItemCanBeConvertedToCorrectType()
        {
            //Assign
            var test1 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 100L, test1);

            var test2 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 101L, test2);

            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].As<Stub>(), Is.TypeOf<Stub>());
            Assert.That(result[0].As<Stub>().Test, Is.EqualTo(test1));
            Assert.That(result[1].As<Stub>(), Is.TypeOf<Stub>());
            Assert.That(result[1].As<Stub>().Test, Is.EqualTo(test1));
        }

        [Test]
        public void GiveItemHasTwoChildrenOfDifferentTypes_WhenGetSiblingsIsCalled_TwoItemCanBeConvertedToCorrectType()
        {
            //Assign
            var test1 = "Test1";
            AddChild<Stub>(Id, typeof(Stub), 100L, test1);

            var test2 = "Test2";
            AddChild<AnotherStub>(Id, typeof(Stub), 101L, test2);

            //Act
            var result = Sut.GetItemChildren<Stub>(Id).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].As<Stub>(), Is.TypeOf<Stub>());
            Assert.That(result[0].As<Stub>().Test, Is.EqualTo(test1));
            Assert.That(result[1].As<AnotherStub>(), Is.TypeOf<AnotherStub>());
            Assert.That(result[1].As<AnotherStub>().Test, Is.EqualTo(test2));
        }


        [Test]
        public void GivenItemsParentHasTwoChildren_WhenGetItemSiblingsIsCalled_OneItemReturned()
        {
            //Assign
            var test1 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 900L, test1);

            var test2 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 901L, test2);

            //Act
            var result = Sut.GetItemSiblings<Stub>(900L).ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
           
        }

        [Test]
        public void GivenItemHasParent_WhenGetParentIsCalled_ParentReturned()
        {
            //Assign
            var test1 = "Test";
            AddChild<Stub>(Id, typeof(Stub), 900L, test1);

            var test2 = "Test";
            AddChild<Stub>(900L, typeof(Stub), 901L, test2);

            //Act
            var result = Sut.GetItemParent<Stub>(901L);

            //Assert
            Assert.That(result["Test"], Is.EqualTo(test1));

        }

        private void AddChild<T>(long parentId, Type parenType, long childId, string test)
            where T : BaseStub, new()
        {
            var type = typeof(T);
            var contentType = new ContentType { Name = type.Name, Namespace = type.Namespace };

            var furnaceItemInformation = new FurnaceItemInformation<long>
            {
                Id = childId,
                ContentTypeFullName = contentType.FullName,
                ParentId =  parentId,
                ParentContentTypeFullName = parenType.FullName
            };

            var stub = new T
            {
                FurnaceItemInformation = furnaceItemInformation,
                Test = test
            };

            var itemKey = RedisBackedFurnaceItems.CreateItemKey(furnaceItemInformation.Id, type);
            var setKey = RedisBackedFurnaceItems.CreateItemChildrenKey(parentId, parenType);

            _childSortedSet.Add(itemKey);

            Client.Hashes[itemKey][SiteConfiguration.DefaultSiteCulture.Name].Returns(stub.BuildSerialisedString());
            Client.SortedSets[setKey].Returns(_childSortedSet);

            if(_contentTypes.All(x => x.FullName != contentType.FullName))
                _contentTypes.Add(contentType);

            ContentTypes.GetContentTypes().Returns(_contentTypes);
        }
    }
}
