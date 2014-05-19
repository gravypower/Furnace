using System.Collections.Generic;
using System.Globalization;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceItems<in TKeyType>
    {
        Item CreateItem(ContentType contentType);

        Item GetItem(TKeyType key, ContentType contentType);
        Item GetItem(TKeyType key, ContentType contentType, CultureInfo cultureInfo);

        TRealType GetItem<TRealType>(TKeyType key);
        TRealType GetItem<TRealType>(TKeyType key, CultureInfo cultureInfo);

        void SetItem(TKeyType key, Item item);
        IEnumerable<Item> GetItemChildren<TRealType>(TKeyType key);
    }
}
