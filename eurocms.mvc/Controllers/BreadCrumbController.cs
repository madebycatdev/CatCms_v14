using EuroCMS.Admin.entity;
using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;
using EuroCMS.Model;

namespace EuroCMS.Admin.Controllers
{
    public class BreadCrumbController : BaseController
    {
        BreadCrumbDbContext context = new BreadCrumbDbContext();
        [CmsAuthorize(Permission = "View", ContentType = "BreadCrumb")]
        public ActionResult Index()
        {
            var result = context.SelectBreadCrumbs();
            return View(result);
        }
        [CmsAuthorize(Permission = "List")]
        public ActionResult BreadCrumbList()
        {
            var result = context.SelectBreadCrumbs();
            return View(result);
        }
        [CmsAuthorize(Permission = "Create", ContentType = "BreadCrumb")]
        public ActionResult Create()
        {
            CmsDbContext dbContext = new CmsDbContext();
            ViewBag.ZoneGroups = dbContext.ZoneGroups.ToList();
            ViewBag.Zones = dbContext.Zones.Where(x => x.Status == "A").ToList();
            ViewBag.Articles = dbContext.vArticlesZonesFulls.Where(x => x.Status == (byte)1).ToList();
            ViewBag.Sites = dbContext.Sites.ToList();

            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "BreadCrumb")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.BREADCRUMB_CREATE, this));

            try
            {
                cms_breadcrumbs breadCrumb = GetValidBreadCrumbs(-1, collection);
                UpdateBreadCrumb(breadCrumb);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

         [CmsAuthorize(Permission = "Edit", ContentType = "BreadCrumb")]
        public ActionResult Edit(int id)
        {
            var result = context.SelectBreadCrumb(id).FirstOrDefault();
            return View(result);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "BreadCrumb")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.BREADCRUMB_EDIT, this));

            try
            {
                cms_breadcrumbs breadCrumb = GetValidBreadCrumbs(id, collection);
                UpdateBreadCrumb(breadCrumb);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser", Permission = "Delete", ContentType = "BreadCrumb")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.BREADCRUMB_DELETE, this));

            try
            {
                var result = context.DeleteBreadCrumb(id).FirstOrDefault();

                switch (result)
                {
                    case "DELETED":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "NOTEXIST":
                        throw new ApplicationException("This breadcrumb was not found OR already deleted before.");
                    default:
                        throw new ApplicationException("unexpected error! please contact with system administrator");
                }
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        private cms_breadcrumbs GetValidBreadCrumbs(int id, FormCollection collection)
        { 
            cms_breadcrumbs breadCrumb = new cms_breadcrumbs();
            breadCrumb.breadcrumb_id = id;
            breadCrumb.breadcrumb_main_container = collection["breadcrumb_main_container"] ?? string.Empty;
            breadCrumb.breadcrumb_main_item_container = collection["breadcrumb_main_item_container"] ?? string.Empty;
            breadCrumb.breadcrumb_name = collection["breadcrumb_name"] ?? string.Empty;
            breadCrumb.breadcrumb_sub_container = collection["breadcrumb_sub_container"] ?? string.Empty;
            breadCrumb.breadcrumb_sub_item_container = collection["breadcrumb_sub_item_container"] ?? string.Empty;
            breadCrumb.created_by = Membership.GetUser().ProviderUserKey;
            breadCrumb.deep_level = collection["deep_level"] != null ? Convert.ToByte(collection["deep_level"]) : (byte)0;
            breadCrumb.excluded_sites = collection["excluded_sites"] ?? string.Empty;
            breadCrumb.excluded_zonegroups = collection["excluded_zonegroups"] ?? string.Empty;
            breadCrumb.excluded_zones = collection["excluded_zones"] ?? string.Empty;
            breadCrumb.include_headline = collection["include_headline"] ?? string.Empty;
            breadCrumb.include_site = collection["include_site"] ?? string.Empty;
            breadCrumb.include_submenus = collection["include_submenus"] ?? string.Empty;
            breadCrumb.include_zonegroup = collection["include_zonegroup"] ?? string.Empty;
            breadCrumb.seperator = collection["seperator"] ?? string.Empty;
            breadCrumb.ul_class = collection["ul_class"] ?? string.Empty;

            if (breadCrumb.excluded_sites.StartsWith(","))
            {
                breadCrumb.excluded_sites = breadCrumb.excluded_sites.Substring(1);
            }
            if (breadCrumb.excluded_zonegroups.StartsWith(","))
            {
                breadCrumb.excluded_zonegroups = breadCrumb.excluded_zonegroups.Substring(1);
            }
            if (breadCrumb.excluded_zones.StartsWith(","))
            {
                breadCrumb.excluded_zones = breadCrumb.excluded_zones.Substring(1);
            }

            if (string.IsNullOrEmpty(breadCrumb.breadcrumb_name))
                throw new Exception("Bread Crumb Name required!");

            if (string.IsNullOrEmpty(breadCrumb.breadcrumb_main_container))
                breadCrumb.breadcrumb_main_container = "ul";

            if (string.IsNullOrEmpty(breadCrumb.breadcrumb_main_item_container))
                breadCrumb.breadcrumb_main_item_container = "li";

            if (string.IsNullOrEmpty(breadCrumb.breadcrumb_sub_container))
                breadCrumb.breadcrumb_sub_container = "ul";

            if (string.IsNullOrEmpty(breadCrumb.breadcrumb_sub_item_container))
                breadCrumb.breadcrumb_sub_item_container = "li";

            breadCrumb.excluded_sites = breadCrumb.excluded_sites.Replace(" ", "");
            if (breadCrumb.excluded_sites.EndsWith(","))
                breadCrumb.excluded_sites = breadCrumb.excluded_sites.Substring(0, breadCrumb.excluded_sites.Length - 1);

            breadCrumb.excluded_zonegroups = breadCrumb.excluded_zonegroups.Replace(" ", "");
            if (breadCrumb.excluded_zonegroups.EndsWith(","))
                breadCrumb.excluded_zonegroups = breadCrumb.excluded_zonegroups.Substring(0, breadCrumb.excluded_zonegroups.Length - 1);

            breadCrumb.excluded_zones = breadCrumb.excluded_zones.Replace(" ", "");
            if (breadCrumb.excluded_zones.EndsWith(","))
                breadCrumb.excluded_zones = breadCrumb.excluded_zones.Substring(0, breadCrumb.excluded_zones.Length - 1);

            if (!breadCrumb.include_headline.Equals("Y"))
                breadCrumb.include_headline = "N";

            if (!breadCrumb.include_zonegroup.Equals("Y"))
                breadCrumb.include_zonegroup = "N";

            if (!breadCrumb.include_site.Equals("Y"))
                breadCrumb.include_site = "N";

            if (!breadCrumb.include_submenus.Equals("Y"))
                breadCrumb.include_submenus = "N";
 
            return breadCrumb;
        }

        private void UpdateBreadCrumb(cms_breadcrumbs breadCrumb)
        {
            var result = context.UpdateBreadCrumb(breadCrumb).FirstOrDefault();
            switch (result)
            {
                case "CREATED":
                    TempData["Message"] = "Bread Crumb has been successfully created.";
                    break;
                case "UPDATED":
                    TempData["Message"] = "Bread Crumb has been successfully updated.";
                    break;
                default:
                    throw new ApplicationException("unexpected error! please contact with system administrator");
            }
        }
    }
}
