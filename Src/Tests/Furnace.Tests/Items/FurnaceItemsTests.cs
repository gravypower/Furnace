using Furnace.Configuration;
using Furnace.Tests.Items.FurnaceItemsSpies;

namespace Furnace.Tests.Items
{
    using ServiceStack.Redis;
    using Furnace.Items;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture("AbstractFurnaceItems")]
    [TestFixture("RedisBackedFurnaceItems")]
    public abstract class FurnaceItemsTests
    {
        protected IFurnaceItems<long, string> Sut;

        private readonly string _furnaceItemsType;
        protected IFurnaceSiteConfiguration SiteConfiguration;

        protected FurnaceItemsTests(string furnaceItemsType)
        {
            _furnaceItemsType = furnaceItemsType;
        }

        [SetUp]
        protected void SetUp()
        {
            SiteConfiguration = Substitute.For<IFurnaceSiteConfiguration>();
            switch (_furnaceItemsType)
            {
                case "AbstractFurnaceItems":
                    Sut = new FurnaceItemsSpy(SiteConfiguration);
                    break;
                case "RedisBackedFurnaceItems":
                    {
                        var client = Substitute.For<IRedisClient>();
                        Sut = new RedisBackedFurnaceItemsSpy(client, SiteConfiguration);
                    }
                break;
            }
        }
    }
}
