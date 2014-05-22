using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Funq;
using Furnace.Boiler.Models.Play;
using Furnace.Boiler.Play.Models.Messages;
using Furnace.ContentTypes.Roslyn;
using Furnace.Interfaces.ContentTypes;
using Furnace.Interfaces.Items;
using Furnace.Items.Redis;
using Furnace.Models.Items;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Razor;
using ServiceStack.Redis;

namespace Furnace.Boiler.Play
{
    public class AppHost : AppHostBase
    {
        private IFurnaceContentTypes contentTypes;
        private IFurnaceItems<long> items;

        public AppHost()
            : base("Furnace.Boiler.Play", typeof(AppHost).Assembly)
        {

            contentTypes =
                new RoslynContentTypes(
                    @"C:\GitHub\Furnace\Src\Furnace.Boiler.Models.Play\Furnace.Boiler.Models.Play.csproj");

            contentTypes.GetContentTypes();
            contentTypes.CompileFurnaceContentTypes();
            var siteConfig = new FurnaceSiteConfiguration(new CultureInfo("en-AU"));

            var redisClient = new RedisClient("localhost");

            redisClient.FlushAll();

            items = new RedisBackedFurnaceItems(redisClient, siteConfig, contentTypes);

            var pageType = contentTypes.GetContentTypes().Single(x => x.FullName == typeof(Page).FullName);

            var pages = new List<Page>
            {
                new Page {Title = "Page One"},
                new Page {Title = "Page Two"}
            };

            var id = 2L;
            foreach (var page in pages)
            {
                var p = items.CreateItem(pageType);
                p.Propities.Add("Title", page.Title);
                p.Propities.Add("Body", page.Body);

                p.FurnaceItemInformation = new FurnaceItemInformation<long>
                {
                    ParentId = 1l,
                    ParentContentTypeFullName = typeof (Page).FullName,
                    ContentTypeFullName = typeof (Page).FullName,
                    Id = id
                };

                p.Save(redisClient);

                id++;
            }
        }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ConsoleLogFactory();

            Plugins.Add(new RazorFormat());

            CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/notfound");

            Routes.Add<PageRequest>("/");
        }
    }
}