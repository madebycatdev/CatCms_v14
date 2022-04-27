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
    public class SitemapController : BaseController
    {
        SitemapDbContext context = new SitemapDbContext();

        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Index()
        {
            var result = context.SelectSitemaps();

            return View(result);
        }

        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Create()
        {
            ViewBag.ParticularDomains = Bags.GetDomainsParticular(0, string.Empty);
            ViewData["ExcludedSites"] = Bags.GetSitemapExcludedSites(string.Empty);
            ViewData["NotExcludedSites"] = Bags.GetSitemapNotExcludedSites(string.Empty);

            CmsDbContext dbContext = new CmsDbContext();
            ViewBag.ZoneGroups = dbContext.ZoneGroups.ToList();
            ViewBag.Zones = dbContext.Zones.Where(x => x.Status == "A").ToList();
            ViewBag.Articles = dbContext.vArticlesZonesFulls.Where(x => x.Status == (byte)1).ToList();
            ViewBag.Sites = dbContext.Sites.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITEMAP_CREATE, this));

            try
            {
                UpdateSitemap(-1, collection);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Edit(int id)
        {
            var result = context.SelectSitemap(id).FirstOrDefault(); //context.Sitemaps.Where(s => s.smap_id == id).FirstOrDefault();

            List<ZoneGroup> listZoneGroup = new List<ZoneGroup>();
            List<Zone> listZone = new List<Zone>();
            List<Article> listArticle = new List<Article>();

            List<int> listZoneGroupId = new List<int>();
            List<int> listZoneId = new List<int>();
            List<int> listArticleId = new List<int>();
             
            CmsDbContext dbContext = new CmsDbContext();
             
            ViewBag.ZoneGroups = dbContext.ZoneGroups.ToList();
            ViewBag.Zones = dbContext.Zones.Where(x => x.Status == "A").ToList();
            ViewBag.Articles = dbContext.vArticlesZonesFulls.Where(x => x.Status == (byte)1).ToList();
            ViewBag.Sites = dbContext.Sites.ToList();

            ViewBag.ParticularDomains = Bags.GetDomainsParticular(result.domain_id, result.domain_alias);
            ViewData["ExcludedSites"] = Bags.GetSitemapExcludedSites(result.included_sites);
            ViewData["NotExcludedSites"] = Bags.GetSitemapNotExcludedSites(result.included_sites);

            ViewData["ExcludedZoneGroupIds"] = result.excluded_zonegroups;
            ViewData["ExcludedZoneIds"] = result.excluded_zones;
            ViewData["ExcludedArticleIds"] = result.excluded_articles;

            ViewData["ExcludedZoneGroups"] = listZoneGroup;
            ViewData["ExcludedZones"] = listZone;
            ViewData["ExcludedArticles"] = listArticle;

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITEMAP_EDIT, this));

            try
            {
                UpdateSitemap(id, collection);
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
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITEMAP_DELETE, this));

            try
            {
                var result = context.DeleteSitemap(id).FirstOrDefault();

                switch (result)
                {
                    case "OK":
                        TempData["Message"] = "Successfully Deleted.";
                        break;
                    case "NOK":
                        throw new ApplicationException("Sitemap creating in progress, can not be deleted.");
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

        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult ReCreate(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITEMAP_RECREATE, this));

            try
            {
                context.ReCreateSitemap(id);

                TempData["Message"] = "Sitemap ready to re-create....";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        private void UpdateSitemap(int id, FormCollection collection)
        {
            TempData.Clear();

            if (String.IsNullOrEmpty(collection["domain_alias"]))
                throw new ApplicationException("Domain select a domain!");

            if (String.IsNullOrEmpty(collection["a_included_sites"]))
                throw new ApplicationException("Please select a site!");

            if (String.IsNullOrEmpty(collection["sm_interval"]))
                throw new ApplicationException("Please enter a valid 'Interval' value!");

            if (String.IsNullOrEmpty(collection["sm_name"]))
                throw new ApplicationException("Please enter a valid site name!");


            string excluded_zonegroups = collection["excluded_zonegroups"] != null ? collection["excluded_zonegroups"].Trim() : "";
            List<string> excludedZg = excluded_zonegroups.Split(',').ToList().Distinct().ToList();
            excluded_zonegroups = string.Join(",", excludedZg.ToArray());

            string excluded_zones = collection["excluded_zones"] != null ? collection["excluded_zones"].Trim() : "";
            List<string> excludedZones = excluded_zones.Split(',').ToList().Distinct().ToList();
            excluded_zones = string.Join(",", excludedZones.ToArray());

            string excluded_articles = collection["excluded_articles"] != null ? collection["excluded_articles"].Trim() : "";
            List<string> ExcludedArticles = excluded_articles.Split(',').ToList().Distinct().ToList();
            excluded_articles = string.Join(",",ExcludedArticles.ToArray());

            if (excluded_zonegroups.StartsWith(","))
            {
                excluded_zonegroups = excluded_zonegroups.Substring(1);
            }

            if (excluded_zones.StartsWith(","))
            {
                excluded_zones = excluded_zones.Substring(1);
            }

            if (excluded_articles.StartsWith(","))
            {
                excluded_articles = excluded_articles.Substring(1);
            }

            if (excluded_zonegroups.EndsWith(","))
                excluded_zonegroups = excluded_zonegroups.Substring(0, excluded_zonegroups.Length - 2);

            if (excluded_zones.EndsWith(","))
                excluded_zones = excluded_zones.Substring(0, excluded_zones.Length - 2);

            if (excluded_articles.EndsWith(","))
                excluded_articles = excluded_articles.Substring(0, excluded_articles.Length - 2);

            cms_sitemaps sm = new cms_sitemaps();
            sm.domain_id = Convert.ToInt32(collection["domain_alias"].Split(new string[1] { ";;" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            sm.domain_alias = collection["domain_alias"].Split(new string[1] { ";;" }, StringSplitOptions.RemoveEmptyEntries)[1];
            sm.smap_id = id;
            sm.interval = Convert.ToInt32(collection["sm_interval"]);
            sm.included_sites = collection["a_included_sites"];
            sm.afiles = collection["afiles"] ?? "N";
            sm.excluded_zonegroups = excluded_zonegroups;
            sm.excluded_zones = excluded_zones;
            sm.excluded_articles = excluded_articles;
            sm.notify_ask = collection["notify_ask"] ?? "N";
            sm.notify_google = collection["notify_google"] ?? "N";
            sm.notify_msn = collection["notify_msn"] ?? "N";
            sm.notify_yahoo = collection["notify_yahoo"] ?? "N";
            sm.enabled = collection["enabled"] ?? "N";
            sm.yahoo_id = collection["sm_name"] ?? "";
            sm.gzip_enabled = collection["gzip_enabled"] ?? "N";
            sm.created_by = Membership.GetUser().ProviderUserKey;

            var result = context.UpdateSitemap(sm).FirstOrDefault();
            switch (result)
            {
                case "EXIST":
                    throw new ApplicationException("There's a sitemap for this domain already. Please select another domain.");
                case "CREATED":
                    TempData["Message"] = "Sitemap has been successfully created.";
                    break;
                case "UPDATED":
                    TempData["Message"] = "Sitemap has been successfully updated.";
                    break;
                default:
                    throw new ApplicationException("unexpected error! please contact with system administrator");
            }
        }

        [HttpPost]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Properties(FormCollection collection)
        {
            var zone_id = collection["zone_id"] ?? "0";
            var menu_depth = collection["menu_depth"] ?? "0";
            var item_ordering = collection["item_ordering"] ?? "11";
            var container_tag = collection["container_tag"] ?? "";
            var class_name = collection["class_name"] ?? "";
            var sitemap_type = collection["sitemap_type"] ?? "";

            var exclude_article_ids = collection["exclude_article_ids"] ?? "";
            var exclude_zone_ids = collection["exclude_zone_ids"] ?? "";


            int _zoneId = 0;
            Int32.TryParse(zone_id, out _zoneId);

            int _menuDepth = 0;
            Int32.TryParse(menu_depth, out _menuDepth);

            int _sitemapType = 0;
            Int32.TryParse(sitemap_type, out _sitemapType);

            int _ItemOrdering = 0;
            Int32.TryParse(item_ordering, out _ItemOrdering);

            SitemapPropertyView pv = new SitemapPropertyView();
            pv.ZoneId = _zoneId;
            pv.ItemOrdering = Convert.ToInt32(item_ordering);
            pv.ContainerTag = container_tag;
            pv.MenuDepth = _menuDepth;
            pv.SitemapType = _sitemapType;
            pv.ClassName = class_name;
            pv.ExcludeArticleIds = exclude_article_ids;
            pv.ExcludeZoneIds = exclude_zone_ids;

            List<SelectListItem> zones = Bags.GetZones(_zoneId);

            List<SelectListItem> firstItems = new List<SelectListItem> { 
                new SelectListItem { Text = "-- Current Zone --", Value = "0", Selected = zone_id.Equals("0") },
                new SelectListItem { Text = "-- Related Articles Portlet --", Value = "-2", Selected = zone_id.Equals("-2") },
                new SelectListItem { Text = "-- Tag Display Portlet --", Value = "-4", Selected = zone_id.Equals("-4") },
                new SelectListItem { Text = "-- Article File Portlet --", Value = "-1", Selected = zone_id.Equals("-1") },
                new SelectListItem { Text = "-- Selected Articles Portlet --", Value = "-3", Selected = zone_id.Equals("-3") },
                new SelectListItem { Text = "-- Custom Content --", Value = "-5", Selected = zone_id.Equals("-5") }
            };

            zones.InsertRange(0, firstItems);

            ViewBag.Zones = zones;

            return View(pv);
        }
    }
}
