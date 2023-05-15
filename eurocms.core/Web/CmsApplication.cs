using EuroCMS.Core;
using EuroCMS.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;

namespace EuroCMS.Web
{
    public class CmsApplication : HttpApplication
    { 
        public override void Init()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new CmsTemplatePathProvider()); 

            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["eurocms.db"].ConnectionString;
            System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connectionString);
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connectionString, "cms_articles");

            base.Init();
        }

        public override string GetOutputCacheProviderName(HttpContext context)
        {
            // to-do FileBasedCacheProvider & MemoryBaseCacheProvider
            return base.GetOutputCacheProviderName(context);
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Contains("key"))
            {
                return string.Format("PATH:{0}_ISMOBILE:{1}_ISAUTHENTICATED:{2}",
                    HttpContext.Current.Request.Url.ToString(),
                    HttpContext.Current.Request.Browser.IsMobileDevice.ToString(), // (config path: C:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\Browsers)
                    HttpContext.Current.User.Identity.IsAuthenticated
                );
            }

            return base.GetVaryByCustomString(context, custom);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = base.Context.Request.Cookies[cookieName];

            if (authCookie == null)
            {
                return;
            }

            FormsAuthenticationTicket authTicket = null;

            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }

            if (authTicket == null)
            {
                return;
            }

            string[] roles = authTicket.UserData.Split(new char[] { '|' });
            FormsIdentity id = new FormsIdentity(authTicket);
            GenericPrincipal principal = new GenericPrincipal(id, roles);

            base.Context.User = principal;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //string domainKey = ConfigurationManager.AppSettings["EuroCMS.Key"] ?? "";
            //EuroCMS.Core.CmsHelper.UnlockCMS(domainKey, Request.Url.Host);
            //if (!EuroCMS.Core.CmsHelper.CheckLicence())
            //{
            //    Response.Write("Invalid CMS License!" + Environment.NewLine);
            //    Response.End();
            //}
        }
    }
}
