using System.Collections.Generic;

namespace Furnace.ContentTypes
{
    public class FurnaceContentType
    {
        public string Name { get; internal set; }

        public IList<string> Properties { get; set; }

        public FurnaceContentType()
        {
            Properties = new List<string>();
        }

    }
}
