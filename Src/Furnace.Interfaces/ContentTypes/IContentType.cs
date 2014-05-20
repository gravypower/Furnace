using System.Collections.Generic;

namespace Furnace.Interfaces.ContentTypes
{
    public interface IContentType
    {
        string Name { get; set; }
        string Namespace { get; set; }
        string FullName { get; }
        IList<IProperty> Properties { get; set; }
    }
}
