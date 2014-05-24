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
            var appHost = (AppHost) ServiceStackHost.Instance;
            var responce = new PageResponse {Pages = new List<Page>()};

            if (request.PageId == 0L)
            {
                foreach (var itemChild in appHost.Items.GetItemChildren<Page>(1L))
                {
                    responce.Pages.Add(itemChild.As<Page>());
                }
            }
            else
            {
                responce.Pages.Add(appHost.Items.GetItem<Page>(request.PageId));
            }

            return responce;
        }
    }
}