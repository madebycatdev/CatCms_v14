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
using System.Web.Mvc.Html;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class UrlRedirectController : BaseController
    {
        UrlRedirectDbContext context = new UrlRedirectDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "UrlRedirect")]
        public ActionResult Index()
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.UrlRedirect, null);
            var result = from sg in context.SelectUrlRedirects()
                         group sg by sg.group_name into g
                         select new Group<EuroCMS.Admin.entity.cms_asp_admin_select_url_redirects_Result, string> { Key = g.Key, Values = g };

            return View(result.ToList());
        }

        [CmsAuthorize(Permission = "Create", ContentType = "UrlRedirect")]
        public ActionResult Create()
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.UrlRedirect, null);
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(-1, -1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "UrlRedirect")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.URL_REDIRECT_CREATE, this));

            try
            {
                cms_redirects redirection = GetValidRedirection(collection);
                redirection.redirect_id = -1;
                redirection.permanent_redirection = Convert.ToBoolean(collection["permanent_redirection"]);
                UpdateRedirection(redirection);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "UrlRedirect")]
        public ActionResult Edit(int id)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.UrlRedirect, null);
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(-1, -1);
            var result = context.SelectUrlRedirect(id).FirstOrDefault();
            ViewBag.redirect_article = Bags.GetArticleZoneNames(result.zone_id +"-"+ result.article_id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "UrlRedirect")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.URL_REDIRECT_EDIT, this));

            try
            {
                cms_redirects redirection = GetValidRedirection(collection);
                redirection.redirect_id = id;
                redirection.permanent_redirection = Convert.ToBoolean(collection["permanent_redirection"]);
                UpdateRedirection(redirection);
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

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.URL_REDIRECT_DELETE, this));

            try
            {
                var result = context.DeleteUrlRedirect(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"])).FirstOrDefault();
                switch (result.rCode)
                {
                    case "0":
                    case "DELETED":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "1":
                        throw new ApplicationException("This redirection was not found OR already deleted before.");
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

        public cms_redirects GetValidRedirection(FormCollection collection)
        {
            string redirect_alias = !string.IsNullOrEmpty(collection["redirect_alias"]) ? EuroCMS.Core.CmsHelper.c2QS(collection["redirect_alias"].Trim()) : string.Empty;
            bool permanent = string.IsNullOrEmpty(collection["permanent_redirection"]) ? false : true;
            string redirect_article = collection["redirect_article"] ?? string.Empty;

            if (string.IsNullOrEmpty(redirect_alias))
                throw new Exception("Url Alias is required!");

            if (redirect_article.Split('-').Length != 2)
                throw new Exception("Redirection article is required!");

            cms_redirects redirection = new cms_redirects();
            redirection.group_id = string.IsNullOrEmpty(collection["group_id"]) ? 0 : Convert.ToInt32(collection["group_id"]);
            redirection.permanent_redirection = permanent;
            redirection.structure_description = collection["structure_description"];
            // redirection.publisher_id = Convert.ToInt32(Session["publisher_id"]);
            redirection.publisher_id = Membership.GetUser().ProviderUserKey;
            redirection.zone_id = Convert.ToInt32(redirect_article.ToString().Split('-')[0]);
            redirection.article_id = Convert.ToInt32(redirect_article.ToString().Split('-')[1]);
            redirect_alias = CoreHelper.CheckRedirectionAlias(redirection.zone_id, redirection.article_id, redirect_alias, 0);
            redirection.redirect_alias = redirect_alias;

            return redirection;
        }

        public void UpdateRedirection(cms_redirects redirection)
        { 
            var result = context.UpdateRedirect(redirection).FirstOrDefault();

                switch (result.rStat)
                {
                    case "D":
                        throw new ApplicationException("This url alias is already used. Please choose another one.");
                    case "U":
                        TempData["Message"] = "Your redirection has been successfully updated.";
                        break;
                    case "F":
                        throw new Exception("Your redirection has not been saved. This article is not related with this zone.");
                    default:
                        TempData["Message"] = "Your redirection has been successfully.";
                        break;
                }
        }
    }
}
