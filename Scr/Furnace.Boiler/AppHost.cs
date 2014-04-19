using System;
using System.Net;
using System.Reflection;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Logging;
using ServiceStack.Razor;

namespace Furnace.Boiler
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Test Razor", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {

            LogManager.LogFactory = new ConsoleLogFactory();

            Plugins.Add(new RazorFormat());

            CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/notfound");
            CustomErrorHttpHandlers[HttpStatusCode.Unauthorized] = new RazorHandler("/login");
        }
    }
}