using System.Collections.Generic;
using Furnace.Boiler.Models.Play;
using Furnace.Boiler.Play.Models.Messages;
using ServiceStack;

namespace Furnace.Boiler.Play
{
    [ClientCanSwapTemplates]
    [DefaultView("Item")]
    public class ItemService : Service
    {
        public PageResponse Get(PageRequest request)
        {
            var responce = new PageResponse {Pages = new List<Page>()};
            //foreach (var itemChild in items.GetItemChildren<Page>(1L))
            //{
            //    responce.Pages.Add(itemChild.As<Page>());
            //}
            return responce;
        }
    }
}