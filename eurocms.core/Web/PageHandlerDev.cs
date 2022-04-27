using EuroCMS.Data;
using EuroCMS.Core;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Hosting;
using eurocms.controls;
using EuroCMS.WebControl;

namespace EuroCMS.Web
{
      
    public class PageHandlerDev : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return true; }
        }

        string dependecyFile = ConfigurationManager.AppSettings["EuroCMS.CacheDependecyFile"] ?? "";

        public string RenderControl(Control control)
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
 
        public void ProcessRequest(HttpContext context)
        {
            // Daily caching
            //TimeSpan t = new TimeSpan(1, 0, 0, 0);

            //// Create a Cache Policy to Page generating
            //context.Response.Cache.SetOmitVaryStar(false);
            //context.Response.Cache.SetNoTransforms();
            //context.Response.Cache.SetRevalidation(HttpCacheRevalidation.None);
            //// Adding to Cache Dependency. All Pages updates when any articles approved.
            //context.Response.AddCacheDependency(new SqlCacheDependency("EuroCMS", "cms_articles"));
            //// All Pages updates when the cache.dat file modified.
            //context.Response.AddFileDependency((string.IsNullOrEmpty(dependecyFile) ? context.Server.MapPath("~/App_Data/cache.dat") : dependecyFile));
            //context.Response.Cache.SetCacheability(context.User.Identity.IsAuthenticated ? HttpCacheability.NoCache : HttpCacheability.ServerAndPrivate);
            //// Daily caching
            //context.Response.Cache.SetExpires(DateTime.Now.Add(t));
            //context.Response.Cache.SetMaxAge(t);
            //context.Response.Cache.SetValidUntilExpires(true);
            //// This is Raw Url. /a, /b, /any_url. Each url corresponds to Custom Cache Key. 
            //// This key generated from global.asax.cs File on GetVaryByCustomString() event.
            //context.Response.Cache.SetVaryByCustom("key");
            //context.Response.Cache.VaryByParams.IgnoreParams = true;
            //context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            //// Last Modified Date sends to the browser. Cause user want to see when The Page generated.
            //context.Response.Cache.SetLastModified(DateTime.Now);
            //context.Response.Cache.SetLastModifiedFromFileDependencies();
              
            // Create a new instance of ASP.NET Page
            string pageFile = "~/Page.aspx";
            // Get compiled type by path
            Type type = BuildManager.GetCompiledType(pageFile);
            //create instance of the page
            Page page = (Page)Activator.CreateInstance(type);
            
            // getting html from database
            string html = "<html><head></head><body><cms:CachedControl id=\"id3\" runat=\"server\" /><cms:APortlet id=\"id1\" runat=\"server\" /><cms:Breadcrumb id=\"id2\" runat=\"server\" /></body></html>";

            string holder = string.Format("<asp:PlaceHolder id=\"plc1\" runat=\"server\">{0}</asp:PlaceHolder>", html);

            var control = page.ParseControl(holder);

            page.Controls.Add(control);
             
            //string output = RenderControl(page);

            //context.Response.Write(output);

            //process request
            page.ProcessRequest(context);

           // HttpContext.Current.ApplicationInstance.CompleteRequest();

            //Page page = (Page)PageParser.GetCompiledPageInstance(context.Request.Path, context.Server.MapPath("~/Page.aspx"), context);

            //Control child = page.ParseControl("<cms:Menu runat=server />");

            //page.Controls.Add(child);

            //((IHttpHandler)page).ProcessRequest(context);

            //context.Response.End();



 


            //// Default Daily caching
            //TimeSpan t = new TimeSpan(1, 0, 0, 0);

            //if (vars.a["enddate"] != DBNull.Value)
            //    t = DateTime.Now - Convert.ToDateTime(vars.a["enddate"]);

            //// Create a Cache Policy to Page generating
            //context.Response.Cache.SetOmitVaryStar(false);
            //context.Response.Cache.SetNoTransforms();
            //context.Response.Cache.SetRevalidation(HttpCacheRevalidation.None);
            //// Adding to Cache Dependency. All Pages updates when any articles approved.
            //context.Response.AddCacheDependency(new SqlCacheDependency("EuroCMS", "cms_articles"));
            //// All Pages updates when the cache.dat file modified.
            //context.Response.AddFileDependency((string.IsNullOrEmpty(dependecyFile) ? context.Server.MapPath("~/App_Data/cache.dat") : dependecyFile));
            //context.Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            //// Set cache expire by the page publishing enddate
            //context.Response.Cache.SetExpires(DateTime.Now.Add(t));
            //// 
            //context.Response.Cache.SetMaxAge(t);
            //// The page response is valid until expired. Specifies
            //context.Response.Cache.SetValidUntilExpires(true);
            //// This is Raw Url. /a, /b, /any_url. Each url corresponds to Custom Cache Key. 
            //// This key generated from global.asax.cs File on GetVaryByCustomString() event.
            //context.Response.Cache.SetVaryByCustom("key");
            //// Ignore GET or SET parameters. Indicating whether an HTTP response varias by GET or SET parameters.
            //context.Response.Cache.VaryByParams.IgnoreParams = true;
            //// Makes the response is available in the client browser history cache
            //context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            //// Last Modified Date sends to the browser. Cause user want to see when The Page generated.
            //context.Response.Cache.SetLastModified(DateTime.Now);

            //Page page = (Page)PageParser.GetCompiledPageInstance(context.Request.Path, context.Server.MapPath("~/Page.aspx"), context);

            //// HTML string parsing from ASP.NET compiler
            //Control child = page.ParseControl(html);
            //// Adding rendered control to the Page
            //page.Controls.Add(child);
            //// Rendered HTML of the Page
            //string output = RenderControl(page);
            //// Writing output to HTTP Response
            //context.Response.Write(output);
            //// Current thread aborted!
            //context.Response.End();
        }

        public ICmsControl CreateInstanceControl(string assemblyName, string controlName)
        {
            string assemblyPath = HttpContext.Current.Server.MapPath("~/bin/" + assemblyName + ".dll");
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Type T = assembly.GetType(controlName);
            return (ICmsControl)Activator.CreateInstance(T);
        }
    }
}
