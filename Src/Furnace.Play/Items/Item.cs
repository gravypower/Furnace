using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Furnace.Interfaces.ContentTypes;
using Furnace.Models.ContentTypes;

namespace Furnace.Play
{
    public class Item : DynamicObject
    {
        private readonly IContentType _contentType;
        private readonly IDictionary<string, object> _propities = new Dictionary<string, object>();

        public string SOmeProp { get; set; }

        public Item(ContentType contentType)
        {
            SOmeProp = "fdafsda";
            _contentType = contentType;
        }

        public Item(dynamic expando)
        {
            SOmeProp = expando.SOmeProp;
            _propities = expando as IDictionary<string, object>;
        }

        public bool AddPropity(string propityName, object value)
        {
            var furnacePropity = _contentType.Properties.SingleOrDefault(x => x.Name == propityName);

            if (furnacePropity == null)
                return false;

            if (!_propities.ContainsKey(propityName))
                _propities.Add(propityName, value );
            else
                _propities[propityName] = value;

            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            _propities.Add("SOmeProp", this.SOmeProp);

            return _propities.GetEnumerator();
        }
    }
}
