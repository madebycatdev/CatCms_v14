﻿using System;
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
    public class XmlHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        string dependecyFile = ConfigurationManager.AppSettings["EuroCMS.CacheDependecyFile"] ?? "";
 
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";

            context.Response.Write("<xml />");
 
        }

       
    }
}
