using System.Net;
using Funq;
using Furnace.Boiler.Play.Models.Messages;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Razor;

namespace Furnace.Boiler.Play
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Furnace.Boiler.Play", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ConsoleLogFactory();

            Plugins.Add(new RazorFormat());

            CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/notfound");

            Routes.Add<PageRequest>("/item/");
        }
    }
}