using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.FrontEnd.Plugins.DExpert
{
    /// <summary>
    /// Summary description for Plugins
    /// </summary>
    public class Plugins : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}