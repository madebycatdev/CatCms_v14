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

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator")]
    public class DomainController : BaseController
    {
        DomainDbContext context = new DomainDbContext();
        ArticleDbContext acontext = new ArticleDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "Domain")]
        public ActionResult Index()
        {
            var domains = context.SelectDomains();
            var SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : -1;
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, null);
            return View(domains);
        }

        [CmsAuthorize(Permission = "List", ContentType = "Domain")]
        public ActionResult List()
        {
            TempData.Clear();
            var domains = context.SelectDomains();
            return Json(domains, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "View", ContentType = "Domain")]
        public ActionResult Details(int id)
        {
            TempData.Clear();
            var result = context.SelectDomain(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "Domain")]
        public ActionResult Edit(int id)
        {
            TempData.Clear();
            var result = context.SelectDomain(id)[0];
 
            // ViewBag is dynamic type
            ViewBag.home_page_article_text = Bags.GetArticleZoneNames(result.home_page_article);
            ViewBag.error_page_article_text = Bags.GetArticleZoneNames(result.error_page_article);

            return View(result);
        }

        [CmsAuthorize(Permission = "Create", ContentType = "Domain")]
        public ActionResult Create()
        {
            TempData.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "Domain")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.DOMAIN_CREATE, this));

            try
            {
                cms_domains domain = new cms_domains();

                domain.domain_names = collection["domain_names"];
                domain.home_page_article = collection["home_page_article"];
                domain.created_by = Membership.GetUser().ProviderUserKey;
                domain.error_page_article = collection["error_page_article"];

                var result = context.CreateDomain(domain);
                switch (result[0].dStat)
                {
                    case "U":
                        TempData["Message"] = "Your domain names has been successfully updated.";
                        break;
                    default:
                        TempData["Message"] = "Your domain names has been successfully created.";
                        break;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "Domain")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.DOMAIN_EDIT, this));

            try
            {
                cms_domains domain = new cms_domains();
                domain.domain_id = id;
                domain.domain_names = collection["domain_names"]; 
                domain.home_page_article = collection["home_page_article"];
                domain.created_by = Membership.GetUser().ProviderUserKey;
                domain.error_page_article = collection["error_page_article"];

                var result = context.UpdateDomain(domain);
                switch (result[0].dStat)
                {
                    case "U":
                        TempData["Message"] = "Your domain names has been successfully updated.";
                        break;
                    default:
                        TempData["Message"] = "Your domain names has been successfully created.";
                        break;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser", Permission = "Delete", ContentType = "Domain")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.DOMAIN_DELETE, this));

            try
            {
                var result = context.DeleteDomain(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
            
                switch (result[0].rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "1":
                        throw new ApplicationException("This domain was not found OR already deleted.");
                    case "2":
                        throw new ApplicationException("You do not have access to delete this domain. Also you are logged!");
                    case "3":
                        throw new ApplicationException("You can not delete this domain. There is no any other domain left.");
                    default:
                        throw new ApplicationException("unexpected error!");
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
    }
}
