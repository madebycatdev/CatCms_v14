using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Configuration;
using EuroCMS.Core;
using System.Web.Configuration;
using EuroCMS.Data;

namespace EuroCMS.Web
{
    public class ErrorModule : IHttpModule, IDisposable, ICmsHttpModule
    {
        
        #region IHttpModule Members

        private bool IsDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ErrorModule()
        {
            Dispose(false);
        }
 
        protected virtual void Dispose(bool disposedStatus)
        {
             if (!IsDisposed)
             {
                 IsDisposed = true;
                 // Released unmanaged Resources
                 if (disposedStatus)
                 {
                     // Released managed Resources
                      
                 }
             }
         }

        public void Init(HttpApplication application)
        {
            application.Error += new EventHandler(Application_Error);
            application.PostMapRequestHandler += new EventHandler(context_PostMapRequestHandler);
        }

        #endregion

        protected void context_PostMapRequestHandler(object sender, EventArgs args)
        {
            Page aux = HttpContext.Current.Handler as Page;
            if (aux != null)
            {
                aux.Error += new EventHandler(aux_Error);
            }
        }

        #region Error Events
        protected void aux_Error(object sender, EventArgs e)
        {
            CreateLog();
        }

        protected void Application_Error(object sender, EventArgs args)
        {
            CreateLog();
            
        }
        #endregion

        private void CreateLog()
        {
            HttpContext context = HttpContext.Current;

            Exception ex = context.Server.GetLastError();

            String LocalIPAddress = ConfigurationManager.AppSettings["EuroCMS.ErrorModule.LocalIPs"].ToString();
            
            var config = (CompilationSection)WebConfigurationManager.GetSection("system.web/compilation");

            Dal conn = new Dal();

            conn.CreateAspErrors(context.Session.SessionID,
                context.Request.RequestType,
                context.Request.Url.Port.ToString(),
                context.Request.IsSecureConnection ? "on" : "off",
                context.Request.UserHostAddress,
                context.Request.UserHostName,
                context.Request.UserAgent,
                context.Request.Url.ToString(),
                "WEB",
                context.Session["DEBUG_SQL"] != null ? context.Session["DEBUG_SQL"].ToString() : "",
                context.Request.ServerVariables["ALL_RAW"].ToString(),
                ex.Message,
                ex.StackTrace,
                ex.Source,
                "",
                "",
                0,
                0,
                "",
                "",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                );

            context.Server.ClearError();

            if (config.Debug && LocalIPAddress.IndexOf(CmsHelper.GetCurrentIP()) != -1)
            {
                context.Response.Write(String.Format("<b> Error Message: {0}  </b> <hr /> ", ex.Message));
                context.Response.Write(String.Format("<b> Stack trace:  {0}  </b> <br/> ", ex.StackTrace.Replace(" ", "&nbsp;").Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />")));
                if (context.Session["DEBUG_SQL"] != null)
                    context.Response.Write(String.Format("<b> Debug SQL: {0} </b> ", context.Session["DEBUG_SQL"].ToString()));
            }
            else
                context.Response.Redirect("Error.aspx");
    
        }
 
        public string DisplayName
        {
            get { return "EuroCMS Error Module"; }
        }

        public string VersionName
        {
            get { return "v1.0"; }
        }

        public int VersionLevel
        {
            get { return 1; }
        }

        public string Author
        {
            get { return "Ramazan Dönmez"; }
        }
    }
}
