using Furnace.Interfaces.ContentTypes;
using ServiceStack;

namespace Furnace.Models.ContentTypes
{
    using System.Collections.Generic;

    public class ContentType : IContentType
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public string FullName
        {
            get { return "{0}.{1}".FormatWith(Namespace, Name); }
        }

        public IList<IProperty> Properties { get; set; }

        public ContentType()
        {
            Properties = new List<IProperty>();
        }

    }
}
