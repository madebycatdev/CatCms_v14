using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace EuroCMS.Web
{
    public class CmsPage : System.Web.UI.Page
    {

        public string Headline
        {
            get
            {
                return this.Page.Items["headline"].ToString();
            }
        }

        protected readonly string dependecyFile = ConfigurationManager.AppSettings["EuroCMS.CacheDependecyFile"] ?? "";

        protected WebPartManager partManager;

        public string ZoneId
        {
            get;
            set;
        }

        public string ArticleId
        {
            get;
            set;
        }

        protected override void LoadControlState(object savedState)
        {
            base.LoadControlState(savedState);
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected override void InitOutputCache(OutputCacheParameters cacheSettings)
        {
            base.InitOutputCache(cacheSettings);

            if (this.IsManager())
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }

            // Add file dependency
            Response.AddFileDependency(string.IsNullOrEmpty(dependecyFile) ? Server.MapPath("~/App_Data/cache.dat") : dependecyFile);
        }

        protected bool IsManager()
        {
            return HttpContext.Current.Request.IsAuthenticated
                 && Roles.IsUserInRole("Administrator")
                 || Roles.IsUserInRole("PowerUser")
                 || Roles.IsUserInRole("Editor")
                 || Roles.IsUserInRole("Autor")
                 || Roles.IsUserInRole("ContentManager")
                 || Roles.IsUserInRole("ContentEntry")
                 || Roles.IsUserInRole("UserCreator");
        }

        protected string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    control.RenderControl(tw);
                }
            }
            return sb.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            Context.Items["ZoneId"] = this.ZoneId;
            Context.Items["ArticleId"] = this.ArticleId;

            partManager = WebPartManager.GetCurrentWebPartManager(this);

            base.OnInit(e);
        }

        protected override void InitializeCulture()
        {
            base.InitializeCulture();
        }
    }
}
