using Furnace.Boiler.Play.Models.Messages;
using Furnace.ContentTypes.Model;
using ServiceStack;

namespace Furnace.Boiler.Play
{
    [ClientCanSwapTemplates]
    [DefaultView("Item")]
    public class BoilerService
    {
        public ItemResponse Get(ItemRequest request)
        {
            return new ItemResponse
            {
               ContentType = new FurnaceContentType()
            };
        }
    }
}