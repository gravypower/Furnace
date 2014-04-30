using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.Play
{
    public class Item : DynamicObject
    {
        private readonly ContentType _contentType;
        private readonly Dictionary<string, Propity> _propities = new Dictionary<string, Propity>();

        public Item(ContentType contentType)
        {
            _contentType = contentType;
        }

        public bool AddPropity(string propityName, object value)
        {
            var furnacePropity = _contentType.Properties.SingleOrDefault(x => x.Name == propityName);

            if (furnacePropity == null)
                return false;

            if (!_propities.ContainsKey(propityName))
                _propities.Add(propityName, new Propity(furnacePropity) { Value = value });
            else
                _propities[propityName].Value = value;

            return true;
        }
   
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!_propities.ContainsKey(binder.Name))
                return base.TryGetMember(binder, out result);

            result = _propities[binder.Name].Value;
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _propities.Keys;
        }
    }
}
