using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.Models.Items
{
    public class Item : Item<string>
    {
        public Item(ContentType contentType)
            : base(contentType)
        {
        }
    }

    public abstract class Item<TKeyType>
    {
        public ContentType ContentType { get; set; }

        public TKeyType Id { get; set; }

        protected Item(ContentType contentType)
        {
            ContentType = contentType;
        }

        public object this[string propityName]
        {
            get
            {
                return ContentType.Properties.Single(x=>x.Name == propityName).DefaultValue;
            }
        }
    }
}
