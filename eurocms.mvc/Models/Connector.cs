using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem.Local;
using EuroCMS.Model;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace EuroCMS.Admin.Models
{
    public class Connector
    {
        public void SetupConnector(IAppBuilder app)
        {
            /*
             * Create a connector instance using ConnectorBuilder. The call to the LoadConfig() method
             * will configure the connector using CKFinder configuration options defined in Web.config.
             */
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            /*
             * Create an instance of authenticator implemented in the previous step.
             */
            var customAuthenticator = new CustomCKFinderAuthenticator();
            connectorBuilder
                /*
                 * Provide the global configuration.
                 *
                 * If you installed CKSource.CKFinder.Connector.Config you should load the static configuration
                 * from XML:
                 * connectorBuilder.LoadConfig();
                 */
                .LoadConfig()
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                        //config.LoadConfig();

                        var instanceId = request.QueryParameters["id"].FirstOrDefault() ?? string.Empty;
                        var root = GetRootByInstanceId(instanceId);
                        var baseUrl = GetBaseUrlByInstanceId(instanceId);
                        config.AddBackend(instanceId.ToString(), new LocalStorage(root));
                        config.AddResourceType(instanceId, builder => builder.SetBackend(instanceId, instanceId));
                        config.AddAclRule(new AclRule(
                        new StringMatcher("*"),
                        new StringMatcher("*"),
                        new StringMatcher("*"),
                        new Dictionary<Permission, PermissionType> { { Permission.All, PermissionType.Allow } }));
                        


                        //config.AddBackend(sayi.ToString(), new LocalStorage("~/i/assets/"));
                        //config.AddResourceType(sayi.ToString(), builder => builder.SetBackend(sayi.ToString(), sayi.ToString()));
                        //config.AddAclRule(new AclRule(
                        //new StringMatcher("*"),
                        //new StringMatcher("*"),
                        //new StringMatcher("*"),
                        //new Dictionary<Permission, PermissionType> { { Permission.All, PermissionType.Allow } }));



                        /*
                         * Configure settings per request.
                         *
                         * The minimal configuration has to include at least one backend, one resource type
                         * and one ACL rule.
                         *
                         * For example:
                         * config.LoadConfig()
                         * config.AddBackend("default", new LocalStorage(@"C:\files"));
                         * config.AddResourceType("images", builder => builder.SetBackend("default", "images"));
                         * config.AddAclRule(new AclRule(
                         *     new StringMatcher("*"),
                         *     new StringMatcher("*"),
                         *     new StringMatcher("*"),
                         *     new Dictionary<Permission, PermissionType> { { Permission.All, PermissionType.Allow } }));
                         *
                         * If you installed CKSource.CKFinder.Connector.Config, you should load the static configuration
                         * from XML:
                         * config.LoadConfig();
                         *
                         * If you installed CKSource.CKFinder.Connector.KeyValue.EntityFramework, you may enable caching:
                         * config.SetKeyValueStoreProvider(
                         *     new EntityFrameworkKeyValueStoreProvider("CKFinderCacheConnection"));
                         */
                    }
                );
            /*
             * Build the connector middleware.
             */
            var connector = connectorBuilder
                .Build(connectorFactory);
            /*
             * Add the CKFinder connector middleware to the web application pipeline.
             */
            app.UseConnector(connector);
        }

        private static string GetBaseUrlByInstanceId(string instanceId)
        {

            string rootUrl = WebConfigurationManager.AppSettings["FileRoot"];
            var pathMap = new Dictionary<string, string>();

            using (CmsDbContext dbContext = new CmsDbContext())
            {
                var sites = dbContext.Sites.ToList();

                foreach (var site in sites)
                {
                    if (!string.IsNullOrEmpty(site.FilePath))
                    {
                        pathMap.Add(site.FilePath, rootUrl);
                    }
                }
            }
            pathMap.Add("/", rootUrl);
            string root;
            if (pathMap.TryGetValue(instanceId, out root))
            {
                return root;
            }

            return "";
        }

        private static string GetRootByInstanceId(string instanceId)
        {
            string rootUrl = WebConfigurationManager.AppSettings["FileRoot"];
            var pathMap = new Dictionary<string, string>();

            using (CmsDbContext dbContext = new CmsDbContext())
            {
                var sites = dbContext.Sites.ToList();

                foreach (var site in sites)
                {
                    if (!string.IsNullOrEmpty(site.FilePath))
                    {
                        pathMap.Add(site.FilePath, rootUrl);
                    }
                }
            }
            pathMap.Add("/", rootUrl);
            string root;
            if (pathMap.TryGetValue(instanceId, out root))
            {
                return root;
            }

            return "";
        }
    }
}