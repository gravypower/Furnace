using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Furnace.Configuration;
using Furnace.Models.ContentTypes;
using Furnace.Models.Exceptions;
using Furnace.Models.Items;
using ServiceStack;
using Property = Furnace.Models.ContentTypes.Property;

namespace Furnace.Items
{
    public abstract class FurnaceItems<TIdType> : FurnaceItems, IFurnaceItems<TIdType>
    {
        protected FurnaceItems(IFurnaceSiteConfiguration siteConfiguration) : base(siteConfiguration)
        {
        }

        public Item CreateItem(ContentType contentType)
        {
            var guard = new Guard();
            guard.GuardContenType(contentType);

            return new Item(contentType);
        }

        public Item GetItem(TIdType id, ContentType contentType)
        {
            return GetItem(id, contentType, CultureInfo.DefaultThreadCurrentCulture);
        }

        public Item GetItem(TIdType id, ContentType contentType, CultureInfo cultureInfo)
        {
            var guard = new Guard();
            guard.GuardContenType(contentType);
            return AbstractGetItem(id, contentType, cultureInfo);
        }

        public void SetItem(TIdType id, Item item)
        {
            var guard = new Guard();
            guard.GuardContenType(item.ContentType);
            AbstractSetItem(id, item);
        }

        public abstract Item AbstractGetItem(TIdType id, ContentType contentType, CultureInfo ci);

        public abstract TRealType GetItem<TRealType>(TIdType id);
        public abstract TRealType GetItem<TRealType>(TIdType id, CultureInfo cultureInfo);

        public abstract void AbstractSetItem(TIdType id, Item item);
        

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
            private readonly List<string> reasons;

            public Guard()
            {
                reasons = new List<string>();
            }

            public void GuardContenType(ContentType contentType)
            {
                if(contentType == null)
                    throw new NullContentTypeException();

                if (contentType.Namespace.IsNullOrEmpty()) reasons.Add(InvalidContentTypeException.NoNamespace);

                if (contentType.Name.IsNullOrEmpty()) reasons.Add(InvalidContentTypeException.NoName);

                if (!contentType.Properties.Any()) reasons.Add(InvalidContentTypeException.NoProperties);
                else GuardProperties(contentType.Properties);

                if (reasons.Any()) throw new InvalidContentTypeException(reasons);
            }

            private void GuardProperties(IEnumerable<Property> properties)
            {
                foreach (var property in properties)
                {
                    if (property.Type == null) reasons.Add(InvalidContentTypeException.PropertyHasNoType.FormatWith(property.Name));

                    if (property.Name.IsNullOrEmpty()) reasons.Add(InvalidContentTypeException.PropertyHasNoName.FormatWith(property.Type));
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
