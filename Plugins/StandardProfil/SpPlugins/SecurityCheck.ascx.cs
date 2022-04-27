using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EuroCMS.Core;
using EuroCMS.Plugin.StandardProfil.Models;
using System.Web.SessionState;

namespace EuroCMS.Plugin.StandardProfil
{
    public partial class SecurityCheck : System.Web.UI.UserControl
    {

        private string _returnUrl;

        public string returnurl
        {
            get { return _returnUrl; }
            set { _returnUrl = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadModel()
        {
            string redirectUrl = "/";
            if (!string.IsNullOrEmpty(returnurl))
            {
                HttpContext.Current.Session["returnUrl"] = returnurl;
            }

            using (StandardProfilDbContext dbContext = new StandardProfilDbContext())
            {
                var loginRedirect = dbContext.CmsConfigs.FirstOrDefault(f => f.Name == "LoginRedirect");
                if (loginRedirect != null)
                {
                    if (!string.IsNullOrEmpty(loginRedirect.RemoteValue))
                    {
                        redirectUrl = loginRedirect.RemoteValue;
                    }
                }
            }

            var sessionCheck = HttpContext.Current.Session["SpSecurityCheck"];
            if (sessionCheck != null)
            {
                var securityCheck = Convert.ToBoolean(HttpContext.Current.Session["SpSecurityCheck"].ToString());
                if (!securityCheck)
                {
                    HttpContext.Current.Response.Redirect(redirectUrl);
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(redirectUrl);
            }



        }
    }
}