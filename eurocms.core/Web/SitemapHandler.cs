using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.UI;

namespace EuroCMS.Web
{
    public class SitemapHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }
 
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/xml";

            context.Response.Write("<sitemap />");
 
        }
    }
}
