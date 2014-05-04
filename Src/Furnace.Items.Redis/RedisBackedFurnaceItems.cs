namespace Furnace.Items.Redis
{
    using System.Collections.Generic;

    using Models.ContentTypes;
    using Models.Items;

    using ServiceStack;
    using ServiceStack.Redis;
    using ServiceStack.Text;

    public class RedisBackedFurnaceItems : FurnaceItems<long>
    {
        public const string ContentTypeSetId = "ContentType:{0}";

        public const string ItemKey = "Item:{0}.{1}:{2}";

        private readonly IRedisClient client;

        public RedisBackedFurnaceItems(IRedisClient client)
        {
            this.client = client;
        }

        public override Item AbstractGetItem(long id, ContentType contentType)
        {
            var key = CreateItemKey(id, contentType);
            var value = client.GetValue(key);

            if (value.IsNullOrEmpty()) return null;

            var propities = TypeSerializer.DeserializeFromString<IDictionary<string, object>>(value);

            return new Item(contentType, propities) { Id = id };
        }

        public override void AbstractSetItem(long id, Item item)
        {
            var key = CreateItemKey(id, item.ContentType);
            client.Set(key, item.Propities);
        }

        public static string CreateItemKey(long id, ContentType contentType)
        {
            return ItemKey.FormatWith(contentType.Namespace, contentType.Name, id);
        }
    }
}
