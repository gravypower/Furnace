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
        public IFurnaceItems<long> Items;

        public AppHost()
            : base("Furnace.Boiler.Play", typeof(AppHost).Assembly)
        {

            contentTypes =
                new RoslynContentTypes(
                    @"E:\Dev\Src\Furnace.Boiler.Models.Play\Furnace.Boiler.Models.Play.csproj",
                    @"E:\Dev\Src\Furnace.Boiler\bin\FurnaceObjectTypes\FurnaceObjectType.cs");

            contentTypes.GetContentTypes();
            contentTypes.CompileFurnaceContentTypes();
            var siteConfig = new FurnaceSiteConfiguration(new CultureInfo("en-AU"));

            var redisClient = new RedisClient("localhost");

            redisClient.FlushAll();

            Items = new RedisBackedFurnaceItems(redisClient, siteConfig, contentTypes);

            var pageType = contentTypes.GetContentTypes().Single(x => x.FullName == typeof(Page).FullName);

            for (var i = 2L; i < 100; i++)
            {
                var p = Items.CreateItem(pageType);
                p.Propities.Add("Title", "Page " + i);
                p.Propities.Add("Body", "Body " + i);

                p.FurnaceItemInformation = new FurnaceItemInformation<long>
                {
                    ParentId = 1L,
                    ParentContentTypeFullName = typeof (Page).FullName,
                    ContentTypeFullName = typeof (Page).FullName,
                    Id = i
                };

                p.Save(redisClient);
            }
        }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ConsoleLogFactory();

            Plugins.Add(new RazorFormat());

            CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/notfound");

            Routes.Add<PageRequest>("/Items");
            Routes.Add<PageRequest>("/Items/{PageId}");
        }
    }
}