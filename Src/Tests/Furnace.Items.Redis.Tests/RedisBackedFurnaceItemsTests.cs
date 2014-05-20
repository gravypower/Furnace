using Furnace.Interfaces.Configuration;
using Furnace.Interfaces.ContentTypes;
using Furnace.Tests;

namespace Furnace.Items.Redis.Tests
{
    using Models.ContentTypes;
    using System.Globalization;
    using NSubstitute;

    using NUnit.Framework;

    using ServiceStack.Text;
    using ServiceStack.Redis;

    [TestFixture]
    public class RedisBackedFurnaceItemsTests
    {
        protected const string ContentTypeName = "SomeType";
        protected const string ContentTypeNamespace = "SomeNamespace";

        protected IRedisClient Client;

        public IContentType ContentType;
        protected RedisBackedFurnaceItems Sut;

        protected IFurnaceSiteConfiguration SiteConfiguration;
        protected IFurnaceContentTypes ContentTypes;

        [SetUp]
        public void RedisBackedFurnaceItemsTestsSetUp()
        {
            Client = Substitute.For<IRedisClient>();
            ContentTypes = Substitute.For<IFurnaceContentTypes>();
            ContentType = new ContentType { Name = ContentTypeName, Namespace = ContentTypeNamespace };

            SiteConfiguration = Substitute.For<IFurnaceSiteConfiguration>();
            SiteConfiguration.DefaultSiteCulture.Returns(new CultureInfo("en-AU"));

            Sut = new RedisBackedFurnaceItems(Client, SiteConfiguration, ContentTypes);
        }

        [Test]
        public void WhenStoreItemIsCalled_ThenClientReceives_CorrectObject()
        {
            //Assign
            const long id = 1L;
            ContentType.AddPropity("Test", "string");
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
