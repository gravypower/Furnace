using System;
using System.Globalization;

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

        private readonly IRedisClient _client;

        public RedisBackedFurnaceItems(IRedisClient client)
        {
            _client = client;
        }

        public override Item AbstractGetItem(long id, ContentType contentType, CultureInfo ci)
        {
            var key = CreateItemKey(id, contentType);
            var value = _client.GetValue(key);

            if (value.IsNullOrEmpty())
                return null;

            var propities = TypeSerializer.DeserializeFromString<IDictionary<string, object>>(value);

            return new Item(contentType, propities) { Id = id };
        }

        public override TRealType GetItem<TRealType>(long id)
        {
            var key = CreateItemKey(id, typeof(TRealType));
            var value = _client.GetValue(key);

            return TypeSerializer.DeserializeFromString<TRealType>(value);
        }

        public override void AbstractSetItem(long id, Item item)
        {
            var key = CreateItemKey(id, item.ContentType);
            _client.Set(key, item.Propities);
        }

        public static string CreateItemKey(long id, ContentType contentType)
        {
            return ItemKey.FormatWith(contentType.Namespace, contentType.Name, id);
        }

        public static string CreateItemKey(long id, Type type)
        {
            return ItemKey.FormatWith(type.Namespace, type.Name, id);
        }
    }
}
