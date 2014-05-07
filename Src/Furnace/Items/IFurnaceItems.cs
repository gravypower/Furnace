using System.Globalization;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceItems<in TKeyType>
    {
        Item CreateItem(ContentType contentType);

        Item GetItem(TKeyType id, ContentType contentType);
        Item GetItem(TKeyType id, ContentType contentType, CultureInfo ci);

        TRealType GetItem<TRealType>(TKeyType id);

        void SetItem(TKeyType id, Item item);
    }
}
