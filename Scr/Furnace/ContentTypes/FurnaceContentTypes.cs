using System.Collections.Generic;

namespace Furnace.ContentTypes
{
    public abstract class FurnaceContentTypes : IFurnaceContentTypes
    {
        public abstract IEnumerable<FurnaceContentType> GetContentTypes();
    }
}
