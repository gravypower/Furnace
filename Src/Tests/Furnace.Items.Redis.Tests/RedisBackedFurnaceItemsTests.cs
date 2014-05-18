namespace Furnace.Items.Redis.Tests
{
    using System.Globalization;

    using Furnace.Tests.Items.GivenContentType.WithNameAndNamespace;

    using NSubstitute;

    using NUnit.Framework;

    using ServiceStack.Text;
    using ServiceStack.Redis;

    [TestFixture]
    public class RedisBackedFurnaceItemsTests : WithNameAndNamespaceTests
    {
        protected IRedisClient Client;

        public RedisBackedFurnaceItemsTests(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

        [SetUp]
        public void RedisBackedFurnaceItemsTestsSetUp()
        {
            Client = Substitute.For<IRedisClient>();
            SiteConfiguration.DefaultSiteCulture.Returns(new CultureInfo("en-AU"));
            Sut = new RedisBackedFurnaceItems(Client, SiteConfiguration);
        }

        [Test]
        public void WhenStoreItemIsCalled_ThenClientReceives_CorrectObject()
        {
            //Assign
            const long id = 1L;
            AddPropityToContentType("Test", "string");
            var item = Sut.CreateItem(ContentType);
            item.Id = id;

            //Act
            Sut.SetItem(id, item);

            //Assert
            var key = RedisBackedFurnaceItems.CreateItemKey(id, ContentType);
            var value = TypeSerializer.SerializeToString(item.Propities);
            Client.Hashes[key].Received().Add(Arg.Any<string>(), value);
        }
    }
}
