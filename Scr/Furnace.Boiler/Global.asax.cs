using System;
using System.Web;
using ServiceStack.MiniProfiler;

namespace Furnace.Boiler.Play
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }

        protected void Application_BeginRequest(object src, EventArgs e)
        {
            if (Request.IsLocal)
                Profiler.Start();
        }

        protected void Application_EndRequest(object src, EventArgs e)
        {
            Profiler.Stop();
        }
    }
}