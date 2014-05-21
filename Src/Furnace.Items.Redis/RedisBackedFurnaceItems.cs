namespace Furnace.Items.Redis
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Collections.Generic;

    using Interfaces.Configuration;
    using Interfaces.ContentTypes;
    using Interfaces.Items;
    using Models.Items;

    using ServiceStack;
    using ServiceStack.Redis;
    using ServiceStack.Text;

    public class RedisBackedFurnaceItems : FurnaceItems<long>
    {
        public const string ItemHashKey = "Item:{0}:{1}";

        public const string ItemChildrenSortedSetKey = "ItemChildren:{0}:{1}";

        private readonly IRedisClient _client;
        private readonly IFurnaceContentTypes _contentTypes;

        public RedisBackedFurnaceItems(IRedisClient client, IFurnaceSiteConfiguration siteConfiguration, IFurnaceContentTypes contentTypes)
            : base(siteConfiguration)
        {
            _contentTypes = contentTypes;
            _client = client;
        }

        protected override IItem<long> AbstractGetItem(long id, IContentType contentType, CultureInfo ci)
        {
            var key = CreateItemKey(id, contentType);

            var itemHash = _client.Hashes[key];
            var value = itemHash[SiteConfiguration.DefaultSiteCulture.Name];

            if (value.IsNullOrEmpty())
                return null;

            return new Item(contentType, value);
        }

        public IItem<long> GetItem(string key)
        {
            var itemHash = _client.Hashes[key];
            var value = itemHash[SiteConfiguration.DefaultSiteCulture.Name];

            if (value.IsNullOrEmpty())
                return null;

            return new Item(value, _contentTypes);
        }

        protected override IItem<long> NewItem(IContentType contentType)
        {
            return new Item(contentType);
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

        public override IEnumerable<IItem<long>> GetItemChildren(long id, Type type)
        {
            var key = CreateItemChildrenKey(id, type);
            return GetItemChildren(key);
        }

        protected IEnumerable<IItem<long>> GetItemChildren(string key)
        {
            foreach (var itemKey in _client.SortedSets[key])
            {
                yield return GetItem(itemKey);
            }
        }

        public override IEnumerable<IItem<long>> GetItemSiblings(long id, Type type)
        {
            var key = CreateItemKey(id, type);
            var item = GetItem(key);

            var parentKey = CreateItemChildrenKey(
                item.FurnaceItemInformation.ParentId,
                item.FurnaceItemInformation.ContentTypeFullName
                );

            return GetItemChildren(parentKey).Where(x=>x.Id != id);
        }

        public override IItem<long> GetItemParent(long id, Type type)
        {
            var key = CreateItemKey(id, type);
            var item = GetItem(key);

            var parentKey = CreateItemKey(
                item.FurnaceItemInformation.ParentId,
                item.FurnaceItemInformation.ContentTypeFullName
                );

            return GetItem(parentKey);
        }

        protected override void AbstractSetItem(long id, IItem<long> item)
        {
            var key = CreateItemKey(id, item.ContentType);
            var itemHash = _client.Hashes[key];
            var value = TypeSerializer.SerializeToString(item.Propities);
            itemHash.Add(SiteConfiguration.DefaultSiteCulture.Name, value);
        }

        public static string CreateItemKey(long id, string fullName)
        {
            return ItemHashKey.FormatWith(fullName, id);
        }

        public static string CreateItemKey(long id, IContentType contentType)
        {
            return ItemHashKey.FormatWith(contentType.FullName, id);
        }

        public static string CreateItemKey(long id, Type type)
        {
            return ItemHashKey.FormatWith(type.FullName, id);
        }

        public static string CreateItemChildrenKey(long id, Type type)
        {
            return ItemChildrenSortedSetKey.FormatWith(type.FullName, id);
        }

        public static string CreateItemChildrenKey(long id, string fullName)
        {
            return ItemChildrenSortedSetKey.FormatWith(fullName, id);
        }
    }
}
