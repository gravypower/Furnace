using ServiceStack;

namespace Furnace.Models.ContentTypes
{
    using System.Collections.Generic;

    public class ContentType
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public string FullName
        {
            get { return "{0}.{1}".FormatWith(Namespace, Name); }
        }

        public IList<Property> Properties { get; set; }

        public ContentType()
        {
            Properties = new List<Property>();
        }

    }
}
