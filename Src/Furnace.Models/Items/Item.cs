using System;
using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.Models.Items
{
    public class Item
    {
        private readonly ContentType _contentType;

        public Guid Id { get; set; }

        public Item(ContentType contentType)
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
