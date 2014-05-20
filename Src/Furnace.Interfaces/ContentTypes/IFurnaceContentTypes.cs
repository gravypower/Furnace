using System.Collections.Generic;

namespace Furnace.Interfaces.ContentTypes
{
    public interface IFurnaceContentTypes
    {
        IEnumerable<IContentType> GetContentTypes();
        void CompileFurnaceContentTypes();
    }
}
