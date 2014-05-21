using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Furnace.Interfaces.Configuration;
using Furnace.Interfaces.ContentTypes;
using Furnace.Interfaces.Items;
using Furnace.Models.Exceptions;
using ServiceStack;

namespace Furnace.Items
{
    public abstract class FurnaceItems<TIdType> : FurnaceItems, IFurnaceItems<TIdType>
    {
        protected FurnaceItems(IFurnaceSiteConfiguration siteConfiguration) : base(siteConfiguration)
        {
        }

        public IItem<TIdType> CreateItem(IContentType contentType)
        {
            var guard = new Guard();
            guard.GuardContenType(contentType);

            return NewItem(contentType);
        }

        protected abstract IItem<TIdType> NewItem(IContentType contentType);

        public IItem<TIdType> GetItem(TIdType id, IContentType contentType)
        {
            return GetItem(id, contentType, CultureInfo.DefaultThreadCurrentCulture);
        }

        public IItem<TIdType> GetItem(TIdType id, IContentType contentType, CultureInfo cultureInfo)
        {
            var guard = new Guard();
            guard.GuardContenType(contentType);
            return AbstractGetItem(id, contentType, cultureInfo);
        }

        public void SetItem(TIdType id, IItem<TIdType> item)
        {
            var guard = new Guard();
            guard.GuardContenType(item.ContentType);
            AbstractSetItem(id, item);
        }

        public IEnumerable<IItem<TIdType>> GetItemSiblings<T>(TIdType id)
        {
            return GetItemSiblings(id, typeof(T));
        }

        public IEnumerable<IItem<TIdType>> GetItemChildren<TRealType>(TIdType id)
        {
            return GetItemChildren(id, typeof(TRealType));
        }

        public IItem<TIdType> GetItemParent<T>(TIdType id)
        {
            return GetItemParent(id, typeof(T));
        }

        public abstract TRealType GetItem<TRealType>(TIdType id);
        public abstract TRealType GetItem<TRealType>(TIdType id, CultureInfo cultureInfo);

        public abstract IEnumerable<IItem<TIdType>> GetItemChildren(TIdType id, Type type);
        public abstract IEnumerable<IItem<TIdType>> GetItemSiblings(TIdType id, Type type);

        public abstract IItem<TIdType> GetItemParent(TIdType id, Type type);

        protected abstract IItem<TIdType> AbstractGetItem(TIdType id, IContentType contentType, CultureInfo ci);
        protected abstract void AbstractSetItem(TIdType id, IItem<TIdType> item);
    }

    public class FurnaceItems
    {
        protected IFurnaceSiteConfiguration SiteConfiguration;

        protected FurnaceItems(IFurnaceSiteConfiguration siteConfiguration)
        {
            SiteConfiguration = siteConfiguration;
        }

        protected class Guard
        {
            private readonly List<string> _reasons;

            public Guard()
            {
                _reasons = new List<string>();
            }

            public void GuardContenType(IContentType contentType)
            {
                if(contentType == null)
                    throw new NullContentTypeException();

                if (contentType.Namespace.IsNullOrEmpty()) _reasons.Add(InvalidContentTypeException.NoNamespace);

                if (contentType.Name.IsNullOrEmpty()) _reasons.Add(InvalidContentTypeException.NoName);

                if (!contentType.Properties.Any()) _reasons.Add(InvalidContentTypeException.NoProperties);
                else GuardProperties(contentType.Properties);

                if (_reasons.Any()) throw new InvalidContentTypeException(_reasons);
            }

            private void GuardProperties(IEnumerable<IProperty> properties)
            {
                foreach (var property in properties)
                {
                    if (property.Type == null) _reasons.Add(InvalidContentTypeException.PropertyHasNoType.FormatWith(property.Name));

                    if (property.Name.IsNullOrEmpty()) _reasons.Add(InvalidContentTypeException.PropertyHasNoName.FormatWith(property.Type));
                }
            }
        }

        public class NullContentTypeException : FurnaceException
        {
        }

        public class InvalidContentTypeException : ReasonsFurnaceException
        {
            public const string NoName = "ContentType had no name";

            public const string NoNamespace = "ContentType had no namespace";

            public const string NoProperties = "ContentType has no properties";

            public const string PropertyHasNoType = "Propity {0} has no type";

            public const string PropertyHasNoName = "Propity of type {0} has no name";


            public InvalidContentTypeException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }
    }
}
