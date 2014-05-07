using Furnace.Items.Redis;
using ServiceStack.Redis;

namespace Furnace.Tests.Items
{
    public class RedisBackedFurnaceItemsSpy : RedisBackedFurnaceItems
    {
        public IRedisClient Client { get; private set; }
        public RedisBackedFurnaceItemsSpy(IRedisClient client) : base(client)
        {
            Client = client;
        }
    }
}
