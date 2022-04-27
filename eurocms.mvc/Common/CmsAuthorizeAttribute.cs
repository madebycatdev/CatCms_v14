using EuroCMS.Admin.Models;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.Admin.Common
{
    public class CmsAuthorize : AuthorizeAttribute
    {
        public string ContentIdParam { get; set; }

        public string ContentType { get; set; }

        public string Permission { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //If its an unauthorized/timed out ajax request go to top window and redirect to logon.
            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new JavaScriptResult() { Script = "top.location  = 'Account/Login';" };

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //If authorization results in HttpUnauthorizedResult, redirect to error page instead of Logon page.
                if (filterContext.Result is HttpUnauthorizedResult)
                    filterContext.Result = new RedirectResult("~/Error/ForbiddenAccess?ReturnUrl=" + HttpUtility.UrlDecode(filterContext.HttpContext.Request.Url.PathAndQuery));
            }

            // Ip Address Control
            if (System.Configuration.ConfigurationManager.AppSettings["ValidIpAddress"] != null)
            {
                List<string> listIpAddress = new List<string>();
                List<string> listRequestIpAddress = new List<string>();
                List<string> listServerVariables = new List<string>();
                bool isValidIpAddress = false, isControl = false;

                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["ValidIpAddress"]))
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["ValidIpAddress"].ToString().Contains(","))
                    {
                        listIpAddress = System.Configuration.ConfigurationManager.AppSettings["ValidIpAddress"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                    }
                    else
                    {
                        listIpAddress.Add(System.Configuration.ConfigurationManager.AppSettings["ValidIpAddress"].ToString().Trim());
                    }
                }


                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IpAddressControl"]))
                {
                    if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IpAddressControl"]))
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["IpAddressControl"].Contains(","))
                        {
                            listServerVariables = System.Configuration.ConfigurationManager.AppSettings["IpAddressControl"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                        }
                        else
                        {
                            listServerVariables.Add(System.Configuration.ConfigurationManager.AppSettings["IpAddressControl"].Trim());
                        }
                    }
                }

                if (listServerVariables.Count > 0)
                {
                    for (int i = 0; i < listServerVariables.Count; i++)
                    {
                        string serverVariable = listServerVariables[i];
                        if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.ServerVariables[serverVariable]))
                        {
                            listRequestIpAddress.Add(filterContext.HttpContext.Request.ServerVariables[serverVariable].ToString().Trim());
                        }
                    }
                }

                if (listIpAddress.Count > 0 && listRequestIpAddress.Count > 0)
                {
                    isControl = true;

                    for (int i = 0; i < listIpAddress.Count; i++)
                    {
                        if (listRequestIpAddress.Contains(listIpAddress[i]))
                        {
                            isValidIpAddress = true;
                            break;
                        }
                    }
                }

                if (!isValidIpAddress && isControl)
                {
                    filterContext.Result = new RedirectResult("/"); //new RedirectResult(filterContext.HttpContext.Request.Url.Host.ToString());
                }
            }

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            if (httpContext.User.IsInRole("Administrator"))
                return true;

            if (Roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Any(httpContext.User.IsInRole))
                return true;

            if (string.IsNullOrEmpty(Roles) && string.IsNullOrEmpty(Permission) && httpContext.User.Identity.IsAuthenticated)
                return true;

            //string contentType = string.Empty;
            //if (HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("controller"))
            //    contentType = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();

            //string action = string.Empty;
            //if (HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("action"))
            //    action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

            bool hasPermission = string.IsNullOrEmpty(Roles);

            string id = GetContentId();

            if (!string.IsNullOrEmpty(ContentType)
                && !string.IsNullOrEmpty(Permission)
                && !string.IsNullOrEmpty(id)
                && !id.Equals("-1"))
            {

                using (BaseDbContext context = new BaseDbContext())
                {
                    hasPermission = context.HasPermission(
                        String.Join(",", System.Web.Security.Roles.GetRolesForUser(httpContext.User.Identity.Name)),
                        this.Permission.ToLower(),
                        id.ToLower(),
                        this.ContentType.ToLower()).Count() > 0;
                }
            }

            return hasPermission;
        }

        public string GetContentId()
        {
            string ContentId = string.Empty;
            if (!string.IsNullOrEmpty(ContentIdParam))
                ContentId = HttpContext.Current.Request.RequestContext.HttpContext.Request[ContentIdParam].ToString();
            else if (HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("id"))
                ContentId = HttpContext.Current.Request.RequestContext.RouteData.Values["id"].ToString();

            return ContentId;
        }
    }
}