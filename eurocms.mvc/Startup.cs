using Microsoft.Owin;
using Owin;
using CKSource.CKFinder.Connector.Config;
using CKSource.FileSystem.Local;
using EuroCMS.Admin.Models;
using EuroCMS.Admin.Common;
using CKSource.CKFinder.Connector.Host.Owin;

[assembly: OwinStartup(typeof(EuroCMS.Admin.Startup))]

namespace EuroCMS.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
            /*
             * If you installed CKSource.CKFinder.Connector.Logs.NLog you can start the logger:
             * LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();
             * Keep in mind that the logger should be initialized only once and before any other
             * CKFinder method is invoked.
             */
            /*
             * Register the "local" type backend file system.
             */
            //FileSystemFactory.RegisterFileSystem<LocalStorage>();
            /*
             * Map the CKFinder connector service under a given path. By default the CKFinder JavaScript
             * client expect the ASP.NET connector to be accessible under the "/ckfinder/connector" route.
             */

            Connector c = new Connector();

            app.Map("/ckfinder/connector", c.SetupConnector);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
           
        }
    }
}
