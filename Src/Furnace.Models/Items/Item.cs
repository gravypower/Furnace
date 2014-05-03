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
        private readonly ContentType _contentType;

        public TKeyType Id { get; set; }

        protected Item(ContentType contentType)
        {
            _contentType = contentType;
        }

        public object this[string propityName]
        {
            get
            {
                return _contentType.Properties.Single(x=>x.Name == propityName).DefaultValue;
            }
        }
    }
}
