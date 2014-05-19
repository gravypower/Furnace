using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.Models.Items
{
    using System.Collections.Generic;

    public class Item : Item<long>
    {
        public Item(ContentType contentType, IDictionary<string, object> propities, FurnaceItemInformation<long> furnaceItemInformation)
           : base(contentType)
        {
            FurnaceItemInformation = furnaceItemInformation;
            Propities = propities;
        }

        public Item(ContentType contentType)
            : base(contentType)
        {
        }
    }

    public abstract class Item<TKeyType>
    {
        public ContentType ContentType { get; set; }

        public TKeyType Id
        {
            get { return FurnaceItemInformation.Id; }
            set { FurnaceItemInformation.Id = value; }
        }

        public IDictionary<string, object> Propities { get; set; }

        protected FurnaceItemInformation<TKeyType> FurnaceItemInformation { get; set; }

        protected Item(IDictionary<string, object> propities)
        {
            Propities = propities;
        }

        protected Item(ContentType contentType)
        {
            Propities = new Dictionary<string, object>();
            ContentType = contentType;
            FurnaceItemInformation = new FurnaceItemInformation<TKeyType>();
        }

        public object this[string propityName]
        {
            get
            {
                if (Propities.ContainsKey(propityName))
                    return Propities[propityName];

                return ContentType.Properties.Single(x=>x.Name == propityName).DefaultValue;
            }
        }
    }
}
