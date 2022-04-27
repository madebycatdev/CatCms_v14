using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Model;
using MvcSiteMapProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class HomeController : BaseController
    {

        DomainDbContext dContext = new DomainDbContext();

        [AllowAnonymous]
        public ActionResult InstallSuccess()
        {
            if (GlobalVars.redirect_to_success == false)
                return RedirectToAction("Dashboard");

            return View();
        }

        [CmsAuthorize(Permission = "View", ContentType = "Home")]
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            ArrayList list = new ArrayList();
            if (!HttpContext.User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Article");
            }
            if (HttpContext.User.IsInRole("Administrator"))
            {

            }

            Roles.GetRolesForUser("roletest");

            #region Widgets
            CmsDbContext dbContext = new CmsDbContext();
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

            List<WidgetUser> wuList = dbContext.WidgetUsers.Where(x => x.UserID == currentUserId && x.IsActive).ToList();
            List<WidgetConfig> wcList = new List<WidgetConfig>();
            foreach (WidgetUser wu in wuList)
            {
                wcList.AddRange(dbContext.WidgetConfigs.Where(x => x.WidgetUserID == wu.ID && x.IsActive).ToList());
            }

            ViewBag.wcList = wcList;
            ViewBag.wuList = wuList;
            #endregion

            return View();
        }

        public string ReloadCache()
        {

            // control pub level

            using (ConfigurationDbContext context = new ConfigurationDbContext())
            {
                context.CacheUpdateUpdateStatus("", 1, 100);
            }


            System.IO.File.AppendAllText(
                    ConfigurationManager.AppSettings["EuroCMS.CacheDependecyFile"] ?? Server.MapPath("/App_Data/cache.dat"),
                    DateTime.Now.ToString()
            );

            return "OK";
        }

        public ActionResult Structure()
        {

            IList<EuroCMS.Admin.entity.SiteStructureModel> treeview = new List<EuroCMS.Admin.entity.SiteStructureModel>();

            if (Session["CurrentSiteID"] == null || Session["CurrentSiteID"].ToString() == "0")
            {
                TempData["Message"] = "Please Select Site!";
                TempData["HasError"] = true;

            }

            ZoneGroupService _zoneGroupService = new ZoneGroupService(new ZoneGroupRepository());

            int siteID = Convert.ToInt32(Session["CurrentSiteID"]);
            var zoneGroups = _zoneGroupService.GetAllByParentId(siteID);

            foreach (var zg in zoneGroups)
            {
                var zgModel = new EuroCMS.Admin.entity.SiteStructureModel
                {
                    Id = zg.Id,
                    Name = zg.Name,
                    Type = "ZoneGroup",
                    ChildType = "Zone",
                    ParentId = siteID
                };

                var zones = new ZoneDbContext().SelectZonesByZoneGroup(zg.Id).Where(z => z.zone_type_id == 1 && z.zone_status == "A");
                foreach (var z in zones)
                {
                    var zModel = new EuroCMS.Admin.entity.SiteStructureModel
                    {
                        Id = z.zone_id,
                        Name = z.zone_name,
                        Type = "Zone",
                        ChildType = "Article",
                        Status = z.zone_status == "P" ? "passive" : "active",
                        ParentId = zg.Id
                    };

                    var articles = new ArticleDbContext().SelectArticlesByZoneForStructure(z.zone_id);

                    foreach (var a in articles)
                    {
                        var aModel = new EuroCMS.Admin.entity.SiteStructureModel
                        {
                            Id = a.article_id,
                            Name = string.IsNullOrEmpty(a.headline) ? a.menu_text : a.headline,
                            Type = "Article",
                            ChildType = "Article",
                            Status = a.status == 1 ? "active" : "passive",
                            ParentId = z.zone_id
                        };

                        SubArticles(a.navigation_zone_id, ref aModel);

                        // add to zone sub items
                        zModel.List.Add(aModel);
                    }

                    // add to zone group sub items
                    zgModel.List.Add(zModel);
                }

                // add to treview
                treeview.Add(zgModel);
            }

            return View(treeview);
        }

        public ActionResult ChangeCulture(string lang, string ReturnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ChangeSite(int? SiteID, string ReturnUrl)
        {
            SiteService _siteService = new SiteService(new SiteRepository());

            var currentSite = _siteService.Find(SiteID ?? 0);

            Session["CurrentSiteName"] = currentSite != null ? currentSite.Name : null;
            Session["CurrentSiteID"] = SiteID;

            if (!string.IsNullOrEmpty(ReturnUrl))
                return Redirect(ReturnUrl);
            else
                return RedirectToActionSite("Dashboard");
        }

        public ActionResult PreviewSite(int SiteId)
        {
            SiteService _siteService = new SiteService(new SiteRepository());
            var site = _siteService.Find(SiteId);
            string domain = string.Empty;

            if (site != null && site.Domain != null)
                domain = site.Domain.Names.Split('\n').FirstOrDefault().Trim();

            return Redirect(Bags.GetBaseUrl(domain));

        }

        public void SubArticles(int SubZoneID, ref EuroCMS.Admin.entity.SiteStructureModel parentModel)
        {
            var articles = new ArticleDbContext().SelectArticlesBySubZoneForStructure(SubZoneID);
            if (articles.Count > 0)
            {
                foreach (var a1 in articles)
                {
                    var aModel = new EuroCMS.Admin.entity.SiteStructureModel
                    {
                        Id = a1.article_id,
                        Name = string.IsNullOrEmpty(a1.headline) ? a1.menu_text : a1.headline,
                        Type = "Article",
                        ChildType = "Article",
                        Status = a1.status == 1 ? "active" : "passive",
                        ParentId = SubZoneID
                    };

                    parentModel.List.Add(aModel);

                    SubArticles(a1.navigation_zone_id, ref aModel);
                }
            }
        }
    }
}
