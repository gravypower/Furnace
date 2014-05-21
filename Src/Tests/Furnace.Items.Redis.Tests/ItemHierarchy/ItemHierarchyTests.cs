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
        private FakeRedisSortedSet _childSortedSet;
        private const long Id = 100L;

        [SetUp]
        public void ItemHierarchyTestsSetUp()
        {
            _childSortedSet = new FakeRedisSortedSet();
            
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
            AddChildStub(Id, 100L, test);

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
            AddChildStub(Id, 100L, test);
           
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
            AddChildStub(Id, 100L, test1);

            var test2 = "Test";
            AddChildStub(Id, 101L, test2);

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
            AddChildStub(Id, 100L, test1);

            var test2 = "Test";
            AddChildStub(Id, 101L, test2);

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
        public void GivenItemsParentHasTwoChildren_WhenGetItemSiblingsIsCalled_OneItemReturned()
        {
            //Assign
            var test1 = "Test";
            AddChildStub(Id, 900L, test1);

            var test2 = "Test";
            AddChildStub(Id, 901L, test2);

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
            AddChildStub(Id, 900L, test1);

            var test2 = "Test";
            AddChildStub(900L, 901L, test2);

            //Act
            var result = Sut.GetItemParent<Stub>(901L);

            //Assert
            Assert.That(result["Test"], Is.EqualTo(test1));

        }

        private void AddChildStub(long parentId, long childId, string test)
        {
            var type = typeof(Stub);
            var contentType = new ContentType { Name = type.Name, Namespace = type.Namespace };

            var furnaceItemInformation = new FurnaceItemInformation<long>
            {
                Id = childId,
                ContentTypeFullName = contentType.FullName,
                ParentId =  parentId,
                ParentContentTypeFullName = contentType.FullName
            };

            var stub = new Stub(furnaceItemInformation) {Test = test};

            var itemKey = RedisBackedFurnaceItems.CreateItemKey(furnaceItemInformation.Id, typeof(Stub));
            var setKey = RedisBackedFurnaceItems.CreateItemChildrenKey(parentId, typeof(Stub));

            _childSortedSet.Add(itemKey);

            Client.Hashes[itemKey][SiteConfiguration.DefaultSiteCulture.Name].Returns(stub.BuildSerialisedString());
            Client.SortedSets[setKey].Returns(_childSortedSet);
            ContentTypes.GetContentTypes().Returns(new[] { contentType });
        }
    }
}
