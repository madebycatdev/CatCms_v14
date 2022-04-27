using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem.Local;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Model;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace EuroCMS.Admin.Common
{
    [CmsAuthorize(Roles = "PowerUser,Editor,Author,ContentManager,ContentEntry,UserCreator")]
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var classifications = new ClassificationDbContext().SelectClasifications(null);
            ViewBag.ClassificationMenu = classifications;

            SiteService _siteService = new SiteService(new SiteRepository());

            var sites = _siteService.GetAll();

            ViewBag.SitesMenu = sites;


            base.Initialize(requestContext);
        }

        protected RedirectToRouteResult RedirectToActionSite(string Action)
        {
            if (HttpContext.Session["SiteID"] != null)
                return RedirectToAction(Action, new { SiteID = HttpContext.Session["SiteID"] });
            else
                return RedirectToAction(Action);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            CmsHelper.SaveErrorLog(filterContext.Exception, string.Empty, true);
        }
    }
}