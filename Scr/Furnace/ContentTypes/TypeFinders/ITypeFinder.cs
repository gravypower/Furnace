using System.Collections.Generic;

namespace Furnace.ContentTypes.TypeFinders
{
    public interface ITypeFinder
    {
        IList<string> FindTypes();
    }
}
