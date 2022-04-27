using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EuroCMS.FrontEnd.Startup))]

namespace EuroCMS.FrontEnd
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseHangfireDashboard("/hangfire");
        }
    }
}