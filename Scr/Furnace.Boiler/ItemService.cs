using Furnace.Boiler.Play.Models.Messages;
using Furnace.ContentTypes.Model;
using ServiceStack;

namespace Furnace.Boiler.Play
{
    [ClientCanSwapTemplates]
    [DefaultView("Item")]
    public class ItemService : Service
    {
        public ItemResponse Get(ItemRequest request)
        {
            return new ItemResponse
            {
               ContentType = new FurnaceContentType
               {
                   Name = "Test Name",
                   Namespace = "Test Namespace",
                   Properties = new []
                   {
                       new FurnaceContentTypeProperty
                       {
                           Name = "PropertyName",
                           Type = "PropertyType",
                           DefaultValue = "PropertyDefaultValue"
                       }
                   }
               }
            };
        }
    }
}