using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceItems<in TKeyType>
    {
        Item CreateItem(ContentType contentType);
        Item GetItem(TKeyType guid, ContentType contentType);
    }
}
