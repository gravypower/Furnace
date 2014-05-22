using System.Collections.Generic;
using Furnace.Boiler.Models.Play;
using Furnace.Boiler.Play.Models.Messages;
using Furnace.ContentTypes.Roslyn;
using Furnace.Interfaces.ContentTypes;
using ServiceStack;

namespace Furnace.Boiler.Play
{
    [ClientCanSwapTemplates]
    [DefaultView("Item")]
    public class ItemService : Service
    {
        private IFurnaceContentTypes contentTypes;

        public ItemService()
        {
            contentTypes = new RoslynContentTypes(@"E:\Dev\Src\Furnace.Boiler.Models.Play");
            contentTypes.GetContentTypes();
            contentTypes.CompileFurnaceContentTypes();

            var pages = new List<Page>
            {
                new Page {Title = "Page One"},
                new Page {Title = "Page Two"}
            };
        }

        public PageResponse Get(PageRequest request)
        {
            return new PageResponse();
        }
    }
}