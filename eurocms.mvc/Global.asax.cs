
using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using EuroCMS.Admin;
using EuroCMS.Admin.entity;
using System.Data.Entity;
using EuroCMS.Model;
using EuroCMS.Core;


namespace EuroCMS.Admin
{
    public static class GlobalVars
    {

        public static List<string> build_logs = new List<string>();
        public static bool redirect_to_success = false;
    }

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Headers.Set("Server", "");
        //    HttpContext.Current.Response.Headers.Add("Set-Cookie", "HttpOnly;Secure;SameSite=Strict");

        //}
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            Database.SetInitializer<LanguageDbContext>(null);
            Database.SetInitializer<CssDbContext>(null);
            Database.SetInitializer<ZoneDbContext>(null);
            Database.SetInitializer<ArticleDbContext>(null);
            Database.SetInitializer<LayoutDbContext>(null);

            Database.SetInitializer<ZoneDbContext>(null);
            Database.SetInitializer<LayoutDbContext>(null);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ReadConfig();
        }

        public void Application_BeginRequest()
        {
            IPrincipal principal = Thread.CurrentPrincipal;

            if (GlobalVars.redirect_to_success)
            {
                GlobalVars.redirect_to_success = false;
                Response.Redirect("/Home/InstallSuccess");
            }
        }

        ConfigurationDbContext config_context = new ConfigurationDbContext();
        PublisherDbContext publisher_context = new PublisherDbContext();
        LanguageDbContext language_context = new LanguageDbContext();
        CssDbContext css_context = new CssDbContext();
        LayoutDbContext layout_context = new LayoutDbContext();


        public void ReadConfig()
        {
            bool config_done = false;

            var result = config_context.SelectAll();

            if (ConfigurationManager.AppSettings["EuroCMS.WS"].ToLower() == "local")
            {
                var config_local = result.Where(t => t.config_name == "CONFIG_DONE" && t.config_value_local == "YEAH").FirstOrDefault();
                if (config_local != null)
                    config_done = true;
            }
            else if (ConfigurationManager.AppSettings["EuroCMS.WS"].ToLower() == "remote")
            {
                var config_remote = result.Where(t => t.config_name == "CONFIG_DONE" && t.config_value_remote == "YEAH").FirstOrDefault();
                if (config_remote != null)
                    config_done = true;
            }

            if (!config_done)
                BuildConfig();

        }

        public void BuildConfig()
        {
            MembershipUser user;

            if (Membership.FindUsersByName("admin").Count < 1)
                user = Membership.CreateUser("admin", "changethis.?1", "it@euromsg.com");
            else
                user = Membership.GetUser("admin");


            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("PowerUser"))
                Roles.CreateRole("PowerUser");

            if (!Roles.RoleExists("Editor"))
                Roles.CreateRole("Editor");

            if (!Roles.RoleExists("Author"))
                Roles.CreateRole("Author");

            if (!Roles.RoleExists("ContentManager"))          //New System Role
                Roles.CreateRole("ContentManager");

            if (!Roles.RoleExists("ContentEntry"))          //New System Role
                Roles.CreateRole("ContentEntry");

            if (!Roles.RoleExists("UserCreator"))           //New System Role
                Roles.CreateRole(",UserCreator");

            if (!Roles.IsUserInRole("admin", "Administrator"))
                Roles.AddUserToRole("admin", "Administrator");

            string LIP = string.Format("{0} - {1}", System.Environment.MachineName, AppDomain.CurrentDomain.Id);
            bool ge = false;

            checkDBConfig("CHARSET", "utf-8", user.ProviderUserKey);
            checkDBConfig("ADMIN_CONTACT", "System Administrator", user.ProviderUserKey);
            checkDBConfig("CACHE_ACTIVE", "1", user.ProviderUserKey);
            checkDBConfig("APPROVE_LEVEL", "3", user.ProviderUserKey);
            checkDBConfig("APPROVE_FILE_WITH_ARTICLE", "N", user.ProviderUserKey);
            checkDBConfig("CHILKAT_CRYPT_KEY", "", user.ProviderUserKey);
            checkDBConfig("MAIL_CHARSET", "utf-8", user.ProviderUserKey);
            checkDBConfig("SEARCH_NEW_WINDOW", "0", user.ProviderUserKey);
            //checkDBConfig("SMTP_AUTHENTICATION", "0", user.ProviderUserKey);
            //checkDBConfig("SMTP_COMPONENT", "chilkat", user.ProviderUserKey);
            //checkDBConfig("SMTP_FROM_EMAIL", "cms@euromsg.com", user.ProviderUserKey);
            //checkDBConfig("SMTP_FROM_NAME", "EuroCMS System", user.ProviderUserKey);
            //checkDBConfig("SMTP_USERNAME", "", user.ProviderUserKey);
            //checkDBConfig("SMTP_PASSWORD", "", user.ProviderUserKey);
            //checkDBConfig("SMTP_PORT", "25", user.ProviderUserKey);
            //checkDBConfig("SMTP_SERVER", "localhost", user.ProviderUserKey);
            //checkDBConfig("SMTP_UNLOCK", "", user.ProviderUserKey);
            checkDBConfig("SMTP_USE_SSL", "0", user.ProviderUserKey);
            checkDBConfig("ZONE_PERMISSION_TYPE", "AND", user.ProviderUserKey);
            checkDBConfig("TITLE_PREFIX", "", user.ProviderUserKey);
            checkDBConfig("TITLE_SUFFIX", "", user.ProviderUserKey);
            checkDBConfig("CSRF_WARNING", "Y", user.ProviderUserKey);
            checkDBConfig("CSRF_WARNING_EMAILS", "it@euromsg.com", user.ProviderUserKey);
            checkDBConfig("FORCE_HTTPS", "N", user.ProviderUserKey);
            checkDBConfig("FORCE_HTTPS_ONLY_LOGIN", "N", user.ProviderUserKey);
            checkDBConfig("HTTPS_DETECTION", "HTTPS", user.ProviderUserKey);
            checkDBConfig("AUTO_RELOAD_CACHE", "N", user.ProviderUserKey);
            checkDBConfig("ADMIN_PATH", "/cms/", user.ProviderUserKey);
            checkDBConfig("DEBUG_MODE", "Y", user.ProviderUserKey);
            checkDBConfig("CLEAR_EMPTY_LINES", "N", user.ProviderUserKey);
            checkDBConfig("CLEAR_TABS_AND_SPACES", "N", user.ProviderUserKey);
            checkDBConfig("EDITOR_CSS", "", user.ProviderUserKey);
            checkDBConfig("ENABLE_CHECK_OUT", "0", user.ProviderUserKey);
            checkDBConfig("RC4_KEY", "", user.ProviderUserKey);
            checkDBConfig("ERRORS_ALLOWED_IPS", "", user.ProviderUserKey);
            checkDBConfig("CHILKAT_ZIP_KEY", "", user.ProviderUserKey);
            checkDBConfig("PROXY_USE", "N", user.ProviderUserKey);
            checkDBConfig("PROXY_SERVER", "", user.ProviderUserKey);
            checkDBConfig("PROXY_LOGIN", "N", user.ProviderUserKey);
            checkDBConfig("PROXY_USERNAME", "", user.ProviderUserKey);
            checkDBConfig("PROXY_PASSWORD", "", user.ProviderUserKey);
            checkDBConfig("404_ERROR_LOG", "Y", user.ProviderUserKey);
            checkDBConfig("OMNITURE_PAGE_CODE", "", user.ProviderUserKey);
            checkDBConfig("PREVIEW_ALLOWED_IPS", "", user.ProviderUserKey);
            checkDBConfig("BREADCRUMB_CACHE_ACTIVE", "Y", user.ProviderUserKey);
            checkDBConfig("OMNITURE_SCODE_FILENAME", "s_code.js", user.ProviderUserKey);
            checkDBConfig("404_ERROR_EXTENSIONS", "ico,js,css", user.ProviderUserKey);
            checkDBConfig("REMOVE_EDITOR_LINKS", "N", user.ProviderUserKey);
            checkDBConfig("NO_DEFAULT_META", "N", user.ProviderUserKey);
            checkDBConfig("AUTO_DAILY_RELOAD_CACHE", "N", user.ProviderUserKey);
            checkDBConfig("OMNITURE_TESTNTARGET_FILENAME", "", user.ProviderUserKey);

            var result = config_context.CheckDoneStatus(LIP);

            //if (result.publishers == null || result.publishers == 0)
            //{
            //    publisher_context.UpdatePublisher(new cms_publishers { 
            //        publisher_name  = "Master Administrator",
            //        username = "admin",
            //        password = "changethis",
            //        publisher_email = "it@euromsg.com"
            //    });

            //    GlobalVars.build_logs.Add("Creating default administrator user [user:admin, password:changethis].");
            //}


            SiteService _siteService = new SiteService(new SiteRepository());
            ZoneGroupService _zoneGroupService = new ZoneGroupService(new ZoneGroupRepository());

            if (result.languages == null || result.languages == 0)
            {
                language_context.CreateOrUpdateLanguage(new cms_languages
                {
                    lang_id = "TR",
                    lang_name = "Türkçe",
                    lang_order = 1,
                    publisher_id = user.ProviderUserKey
                });

                GlobalVars.build_logs.Add("Creating default language [Türkçe].");
            }

            if (result.css == null || result.css == 0)
            {
                css_context.UpdateCssCode(new cms_css
                {
                    css_name = "Default CSS",
                    css_code = "",
                    css_fix = "",
                    css_rel_text = "",
                    css_id = -1,
                    css_status = "A",
                    css_type = 1,
                    css_type_text = "",
                    group_id = 0,
                    publisher_id = user.ProviderUserKey,
                    structure_description = "",
                });

                GlobalVars.build_logs.Add("Creating default css style.");
            }

            if (result.templates == null || result.templates == 0)
            {
                layout_context.UpdateLayout(new cms_templates
                {
                    template_name = "Default Template",
                    template_html = "Please Change This Default Template",
                    publisher_id = user.ProviderUserKey
                });

                GlobalVars.build_logs.Add("Creating default site template");
            }

            if (result.sites == null || result.sites == 0)
            {
                var css = css_context.SelectCssList(-1).FirstOrDefault();

                int css_id = 0;

                if (css != null)
                {
                    css_id = css.css_id;
                }

                var layout = layout_context.SelectLayouts(-1).FirstOrDefault();

                int layout_id = -1;

                if (layout != null)
                {
                    layout_id = layout.template_id;
                }

                if (layout_id != -1)
                {
                    _siteService.Insert(new EuroCMS.Model.Site()
                    {
                        CreatedBy = Guid.Parse(user.ProviderUserKey.ToString()),
                        Name = "Your Site",
                        TemplateId = layout_id,
                        Created = DateTime.Now,
                        Updated = DateTime.Now

                    });

                    GlobalVars.build_logs.Add("Creating default site ['Your Site']");
                }
                else
                {
                    if (css_id == 0)
                    {
                        GlobalVars.build_logs.Add("Can NOT read css list.. Please contact with madebycat support.");
                        ge = true;
                    }
                    else
                    {
                        GlobalVars.build_logs.Add("Can NOT read template list.. Please contact with madebycat support.");
                        ge = true;
                    }

                }
            }

            if (result.zone_groups == null || result.zone_groups == 0)
            {
                var site = _siteService.GetAll().FirstOrDefault();

                if (site != null)
                {
                    _zoneGroupService.Insert(new EuroCMS.Model.ZoneGroup()
                    {
                        CreatedBy = Guid.Parse(user.ProviderUserKey.ToString()),
                        Name = "Default Zone Group",
                        SiteId = site.Id,
                        Keywords = "",
                        Updated = DateTime.Now,
                        Created = DateTime.Now
                    });

                    GlobalVars.build_logs.Add("Creating default zone group");
                }
                else
                {
                    GlobalVars.build_logs.Add("Can NOT read site list.. Please contact with madebycat support.");
                    ge = true;
                }
            }

            if (result.cache_update == null || result.cache_update == 0)
            {
                config_context.InsertCacheServer(LIP);
                GlobalVars.build_logs.Add("Adding this server [" + LIP + "] for cache update process.");
            }

            if (ge == false)
            {
                UpdateDBConfig("CONFIG_DONE", "YEAH", user.ProviderUserKey);
                GlobalVars.build_logs.Add("Configuration Update OK. Please create at least a zone and article. And setup your domain with your licence key.");
            }
            else
            {
                GlobalVars.build_logs.Add("Some errors occured.. Configuration NOT completed.");
            }

            GlobalVars.redirect_to_success = true;
        }

        public void checkDBConfig(string name, string value, object pubId)
        {
            //if (Session["CMS_" + name] == null)
            //{
            UpdateDBConfig(name, value, pubId);
            //}
        }

        public void UpdateDBConfig(string name, string value, object pubId)
        {
            if (ConfigurationManager.AppSettings["EuroCMS.WS"].ToLower() == "local")
            {
                config_context.UpdateLocalConfigValue(name, value, pubId);
            }
            else if (ConfigurationManager.AppSettings["EuroCMS.WS"].ToLower() == "remote")
            {
                config_context.UpdateRemoteConfigValue(name, value, pubId);
            }
        }

        protected void Session_Start()
        {
            //Session["publisher_id"] = 1;
            Session["publisher_level"] = 100;
            Session["CMS_APPROVE_LEVEL"] = 100;
            Session["CMS_ENABLE_CHECK_OUT"] = "0";

            Session["site_id"] = 1;
            Session["cms_version"] = "mvc.v1.0";

            //if (Request.Cookies.Count > 0)
            //    foreach (string cookie in Request.Cookies.AllKeys)
            //    {
            //        Response.Cookies[cookie].Secure = true;
            //    }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;

            var cultureInfo = (CultureInfo)Session["Culture"];
            if (cultureInfo == null)
            {
                var languageName = "en";
                if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                {
                    languageName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                }
                cultureInfo = new CultureInfo(languageName);
                Session["Culture"] = cultureInfo;
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            CmsHelper.SaveErrorLog(ex, "Handled on Application_Error", true);
        }
    }
}