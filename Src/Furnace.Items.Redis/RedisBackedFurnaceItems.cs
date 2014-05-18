using System;
using System.Globalization;
using Furnace.Configuration;

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

        public const string ItemHashKey = "Item:{0}.{1}:{2}";

        public const string ItemChridrenSortedSetKey = "ItemChridren:{0}.{1}:{2}";

        private readonly IRedisClient _client;

        public RedisBackedFurnaceItems(IRedisClient client, IFurnaceSiteConfiguration siteConfiguration)
            :base(siteConfiguration)
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
            var itemHash = _client.Hashes[key];
            var value = itemHash[SiteConfiguration.DefaultSiteCulture.Name];

            return TypeSerializer.DeserializeFromString<TRealType>(value);
        }

        public override TRealType GetItem<TRealType>(long id, CultureInfo cultureInfo)
        {
            var key = CreateItemKey(id, typeof(TRealType));

            var itemHash = _client.Hashes[key];
            var value = itemHash[cultureInfo.Name];

            return TypeSerializer.DeserializeFromString<TRealType>(value);
        }

        public override IEnumerable<object> GetItemChildren<TRealType>(long id)
        {
            var key = CreateItemChridrenKey(id, typeof(TRealType));
            foreach (var itemKey in _client.SortedSets[key])
            {
                yield return new object();
            }
        }

        public override void AbstractSetItem(long id, Item item)
        {
            var key = CreateItemKey(id, item.ContentType);
            var itemHash = _client.Hashes[key];
            var value = TypeSerializer.SerializeToString(item.Propities);
            itemHash.Add(SiteConfiguration.DefaultSiteCulture.Name, value);
        }

        public static string CreateItemKey(long id, ContentType contentType)
        {
            return ItemHashKey.FormatWith(contentType.Namespace, contentType.Name, id);
        }

        public static string CreateItemKey(long id, Type type)
        {
            return ItemHashKey.FormatWith(type.Namespace, type.Name, id);
        }

        public static string CreateItemChridrenKey(long id, Type type)
        {
            return ItemChridrenSortedSetKey.FormatWith(type.Namespace, type.Name, id);
        }
    }
}
