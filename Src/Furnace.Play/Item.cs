using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Furnace.Models.ContentTypes;

namespace Furnace.Play
{
    using System.Collections;

    public class Item : DynamicObject, IDictionary<string, object>
    {
        private readonly ContentType _contentType;
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
   
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!_propities.ContainsKey(binder.Name))
                return base.TryGetMember(binder, out result);

            result = _propities[binder.Name];
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _propities.Keys;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            _propities.Add("SOmeProp", this.SOmeProp);

            return _propities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        public bool ContainsKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Add(string key, object value)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            throw new System.NotImplementedException();
        }

        public object this[string key]
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public ICollection<string> Keys { get; private set; }

        public ICollection<object> Values { get; private set; }
    }
}
