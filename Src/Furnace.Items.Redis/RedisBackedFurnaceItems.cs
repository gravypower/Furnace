using System;
using System.Globalization;
using System.Linq;
using Furnace.Configuration;
using Furnace.ContentTypes;

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
        public const string ItemHashKey = "Item:{0}.{1}:{2}";

        public const string ItemChildrenSortedSetKey = "ItemChildren:{0}.{1}:{2}";

        public const string FurnaceIteminformationName = "FurnaceItemInformation";

        private readonly IRedisClient _client;
        private IFurnaceContentTypes _contentTypes;

        public RedisBackedFurnaceItems(IRedisClient client, IFurnaceSiteConfiguration siteConfiguration, IFurnaceContentTypes contentTypes)
            : base(siteConfiguration)
        {
            _contentTypes = contentTypes;
            _client = client;
        }

        public override Item AbstractGetItem(long id, ContentType contentType, CultureInfo ci)
        {
            var key = CreateItemKey(id, contentType);

            var propities = GetPropities(key);

            if (propities == null)
                return null;

            var furnaceItemInformation = GetFurnaceItemInformation(propities);

            return new Item(contentType, propities, furnaceItemInformation);
        }

        private IDictionary<string, object> GetPropities(string key)
        {
            var value = _client.GetValue(key);

            if (value.IsNullOrEmpty())
                return null;

            return TypeSerializer.DeserializeFromString<IDictionary<string, object>>(value);
        }

        public Item GetItem(string key)
        {
            var propities = GetPropities(key);

            if (propities == null)
                return null;

            var furnaceItemInformation = GetFurnaceItemInformation(propities);

            var contentType = _contentTypes.GetContentTypes().Single(x => x.FullName == furnaceItemInformation.ContentTypeFullName);

            return new Item(contentType, propities, furnaceItemInformation);
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

        public override IEnumerable<Item> GetItemChildren<TRealType>(long id)
        {
            var key = CreateItemChildrenKey(id, typeof(TRealType));
            foreach (var itemKey in _client.SortedSets[key])
            {
                yield return GetItem(itemKey);
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

        public static string CreateItemChildrenKey(long id, Type type)
        {
            return ItemChildrenSortedSetKey.FormatWith(type.Namespace, type.Name, id);
        }

        private static FurnaceItemInformation<long> GetFurnaceItemInformation(IDictionary<string, object> propities)
        {
            return TypeSerializer.DeserializeFromString<FurnaceItemInformation<long>>(propities[FurnaceIteminformationName] as string);
        }
    }
}
