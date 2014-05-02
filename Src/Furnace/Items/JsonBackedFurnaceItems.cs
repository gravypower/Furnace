using System;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Items
{
    public class JsonBackedFurnaceItems : FurnaceItems<string>
    {
        public JsonBackedFurnaceItems(IFurnaceItemRepository<string> itemRepository) : base(itemRepository)
        {
        }

        public override Item GetItem(Guid guid, ContentType contentType)
        {
            var json = ItemRepository.GetById(guid);
            return null;
        }
    }
}
