using System.Collections.Generic;

namespace Furnace.ContentTypes
{
    public interface IFurnaceContentTypes
    {
        IEnumerable<FurnaceContentType> GetContentTypes();
    }
}
