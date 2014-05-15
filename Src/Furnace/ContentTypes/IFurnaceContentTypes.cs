using System.Collections.Generic;
using Furnace.Models.ContentTypes;

namespace Furnace.ContentTypes
{
    public interface IFurnaceContentTypes
    {
        IEnumerable<ContentType> GetContentTypes();
        void CompileFurnaceContentTypes();
    }
}
