using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.Models.Items
{
    using System.Collections.Generic;

    public class Item : Item<long>
    {
        public Item(ContentType contentType, IDictionary<string, object> propities)
           : base(contentType, propities)
        {
        }

        public Item(ContentType contentType)
            : base(contentType)
        {
        }
    }

    public abstract class Item<TKeyType>
    {
        public ContentType ContentType { get; set; }

        public TKeyType Id { get; set; }

        public IDictionary<string, object> Propities { get; set; }

        protected Item(ContentType contentType, IDictionary<string, object> propities)
        {
            Propities = propities;
            ContentType = contentType;
        }

        protected Item(ContentType contentType)
        {
            ContentType = contentType;
            Propities = new Dictionary<string, object>();
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
