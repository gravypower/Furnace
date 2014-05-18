using System.Collections.Generic;
using System.Globalization;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceItems<in TKeyType>
    {
        Item CreateItem(ContentType contentType);

        Item GetItem(TKeyType id, ContentType contentType);
        Item GetItem(TKeyType id, ContentType contentType, CultureInfo cultureInfo);

        TRealType GetItem<TRealType>(TKeyType id);
        TRealType GetItem<TRealType>(TKeyType id, CultureInfo cultureInfo);

        void SetItem(TKeyType id, Item item);
        IEnumerable<object> GetItemChildren<TRealType>(TKeyType id);
    }
}
