using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EuroCMS.Web
{
    public class BehavioralModule : IHttpModule, ICmsHttpModule
    {
        public string DisplayName
        {
            get { return "Euro CMS Behaviroal Targeting Module"; }
        }

        public string VersionName
        {
            get { return "v1.0";  }
        }

        public int VersionLevel
        {
            get { return 1; }
        }

        public string Author
        {
            get { return "Ramazan Dönmez"; }
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.AuthorizeRequest += context_AuthorizeRequest;
        }

        void context_AuthorizeRequest(object sender, EventArgs e)
        {
            
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            
        }
    }
}
