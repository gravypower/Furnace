using System.Collections.Generic;
using Furnace.Interfaces.ContentTypes;

namespace Furnace.Interfaces.Items
{
    public interface IItem<TKeyType>
    {
        IContentType ContentType { get; set; }
        TKeyType Id { get; set; }
        IDictionary<string, object> Propities { get; set; }
        IFurnaceItemInformation<TKeyType> FurnaceItemInformation { get; set; }
        object this[string propityName] { get; }
        T As<T>();
    }
}
