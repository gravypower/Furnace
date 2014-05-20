using System.Linq;
using Furnace.Interfaces.ContentTypes;
using Furnace.Interfaces.Items;
using ServiceStack.Text;

namespace Furnace.Models.Items
{
    using System.Collections.Generic;

    public class Item : Item<long>
    {
        public Item(string serialisedObject, IFurnaceContentTypes furnaceContentTypes) 
            : base(serialisedObject, furnaceContentTypes)
        {
        }

        public Item(IContentType contentType, string serialisedObject) 
            : base(contentType, serialisedObject)
        {
        }

        public Item(IContentType contentType) 
            : base(contentType)
        {
        }
    }

    public abstract class Item<TKeyType> : IItem<TKeyType>
    {
        private string _serialisedObject;
        public const string FurnaceIteminformationName = "FurnaceItemInformation";

        public IContentType ContentType { get; set; }

        public TKeyType Id
        {
            get { return FurnaceItemInformation.Id; }
            set { FurnaceItemInformation.Id = value; }
        }

        public IDictionary<string, object> Propities { get; set; }

        public IFurnaceItemInformation<TKeyType> FurnaceItemInformation { get; set; }

        protected Item(string serialisedObject, IFurnaceContentTypes furnaceContentTypes)
        {
            _serialisedObject = serialisedObject;
            Propities = TypeSerializer.DeserializeFromString<IDictionary<string, object>>(serialisedObject);
            FurnaceItemInformation = GetFurnaceItemInformation(Propities);

            ContentType = furnaceContentTypes.GetContentTypes().Single(x => x.FullName == FurnaceItemInformation.ContentTypeFullName);
        }


        protected Item(IContentType contentType, string serialisedObject)
        {
            _serialisedObject = serialisedObject;
            Propities = TypeSerializer.DeserializeFromString<IDictionary<string, object>>(serialisedObject);
            FurnaceItemInformation = GetFurnaceItemInformation(Propities);
            ContentType = contentType;
        }


        protected Item(IContentType contentType)
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

        public T As<T>()
        {
            return TypeSerializer.DeserializeFromString<T>(_serialisedObject);
        }

        protected static IFurnaceItemInformation<TKeyType> GetFurnaceItemInformation(IDictionary<string, object> propities)
        {
            return (IFurnaceItemInformation<TKeyType>)propities[FurnaceIteminformationName];
        }
    }
}
