using System.Collections.Generic;

namespace Furnace.ContentTypes.Model
{
    public class FurnaceContentType
    {
        public string Name { get; internal set; }

        public string Namespace { get; set; }

        public IList<FurnaceContentTypeProperty> Properties { get; set; }

        public FurnaceContentType()
        {
            Properties = new List<FurnaceContentTypeProperty>();
        }

    }
}
