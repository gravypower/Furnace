using System.Collections.Generic;
using Furnace.ContentTypes.Model;

namespace Furnace.ContentTypes
{
    public interface IFurnaceContentTypes
    {
        IEnumerable<FurnaceContentType> GetContentTypes();
    }
}
