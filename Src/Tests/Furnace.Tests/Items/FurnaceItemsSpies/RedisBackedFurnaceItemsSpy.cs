using Furnace.Configuration;
using Furnace.Items.Redis;
using Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.Localisation;
using NSubstitute;
using ServiceStack.Redis;

namespace Furnace.Tests.Items.FurnaceItemsSpies
{
    public class RedisBackedFurnaceItemsSpy : RedisBackedFurnaceItems, IFurnaceItemsSpy
    {
        public IRedisClient Client { get; private set; }
        public RedisBackedFurnaceItemsSpy(IRedisClient client, IFurnaceSiteConfiguration siteConfiguration) : base(client, siteConfiguration)
        {
            Client = client;
        }

        public void AssertWhenGetItemIsCalled_ThenCorrectKey_IsUsed(LocalisationTests fixture)
        {
            var key = CreateItemKey(fixture.Id, fixture.ContentType);
            Client.Received().GetValue(key);
        }
    }
}
