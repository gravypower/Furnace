using System.Collections.Generic;
using System.Linq;
using Furnace.ContentTypes.TypeFinders;

namespace Furnace.ContentTypes
{
    public class FurnaceContentTypes : IFurnaceContentTypes
    {
        private readonly ITypeFinder _typeFinder;

        public FurnaceContentTypes(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public IEnumerable<FurnaceContentType> GetContentTypes()
        {
            return _typeFinder.FindTypes().Select(name => new FurnaceContentType {Name = name});
        }
    }
}
