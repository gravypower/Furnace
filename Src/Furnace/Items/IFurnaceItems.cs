using System.Collections.Generic;
using System.Globalization;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceItems<in TKeyType, in TInternalKeyType>
    {
        Item CreateItem(ContentType contentType);

        Item GetItem(TInternalKeyType key);
        Item GetItem(TKeyType key, ContentType contentType);
        Item GetItem(TKeyType key, ContentType contentType, CultureInfo cultureInfo);

        TRealType GetItem<TRealType>(TKeyType key);
        TRealType GetItem<TRealType>(TKeyType key, CultureInfo cultureInfo);

        void SetItem(TKeyType key, Item item);
        IEnumerable<object> GetItemChildren<TRealType>(TKeyType key);
    }
}
