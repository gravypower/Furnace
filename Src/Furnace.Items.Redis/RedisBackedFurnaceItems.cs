namespace Furnace.Items.Redis
{
    using Models.ContentTypes;
    using Models.Items;

    using ServiceStack.Redis;

    public class RedisBackedFurnaceItems : FurnaceItems<string>
    {
        public const string ContentTypeSetId = "ContentType:{0}";

        public const string ItemsSetId = "Items:{0}:{1}";

        private readonly IRedisClient client;

        public RedisBackedFurnaceItems(IRedisClient client)
        {
            this.client = client;
        }

        public override Item AbstractGetItem(string key, ContentType contentType)
        { 
            return null;
        }
    }
}
