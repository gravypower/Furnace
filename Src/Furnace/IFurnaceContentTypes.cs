namespace Furnace
{
    using System.Collections.Generic;

    using ContentTypes.Model;

    public interface IFurnaceContentTypes
    {
        IEnumerable<FurnaceContentType> GetContentTypes();
    }
}
