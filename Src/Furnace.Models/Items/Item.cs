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

        //public TKeyType Id {
        //get { return (TKeyType)Propities["Furance_ID"]; }
        //set { Propities["Furance_ID"] = value; } }

       // public IDictionary<string, object> Propities { get; set; }

        protected Item(ContentType contentType)
        {
            ContentType = contentType;
            //Propities = new Dictionary<string, object> {{"Furance_ID", "SOMEID"}};
            //foreach (var property in ContentType.Properties)
            //{
            //    Propities.Add(property.Name, property.DefaultValue);
            //}
            
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
