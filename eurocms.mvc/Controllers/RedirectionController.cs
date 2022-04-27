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
    [CmsAuthorize(Roles = "Administrator")]
    public class RedirectionController : BaseController
    {
        ArticleDbContext acontext = new ArticleDbContext();
        CmsDbContext dbContext = new CmsDbContext();
        
        [CmsAuthorize(Permission = "View", ContentType = "Redirection")]
        public ActionResult Index()
        {
            List<PageRedirection> listPageRedirections = new List<PageRedirection>();
            try
            {
                listPageRedirections = dbContext.PageRedirections.OrderByDescending(od => od.UpdateDate).ThenByDescending(td => td.CreateDate).ToList();
                Session["ErrorCount"] = null;
                Session.Remove("ErrorCount");
            }
            catch (Exception ex)
            {
                if (Session["ErrorCount"] == null)
                {
                    Guid userId = dbContext.vAspNetMembershipUsers.Where(s => s.UserName == "keremd").FirstOrDefault().UserId;
                    string sql = "update cms_page_redirection set UpdatedBy = '" + userId + "'";
                    dbContext.Database.ExecuteSqlCommand(sql);
                    Session["ErrorCount"] = "1";
                    return RedirectToAction("Index");
                }
            }
            //var redirections = context.SelectRedirections();
            var SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : -1;
            ViewBag.Domains = Bags.GetDomainsWithRows();
            return View(listPageRedirections);
        }

        [CmsAuthorize(Permission = "List", ContentType = "Redirection")]
        public ActionResult List()
        {
            TempData.Clear();
            var redirections = "";//context.SelectRedirections();
            return Json(redirections, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "View", ContentType = "Redirection")]
        public ActionResult Details(int id)
        {
            TempData.Clear();
            var result = ""; //context.SelectRedirection(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "Redirection")]
        public ActionResult Edit(int id)
        {
            TempData.Clear();
            PageRedirection pageRedirection = new PageRedirection();
            pageRedirection = dbContext.PageRedirections.Where(s => s.ID == id).FirstOrDefault();
            string currentDomain = "";
            if (pageRedirection != null)
            {
                string url = pageRedirection.RedirectFrom.Trim();
                UriBuilder uriBuilder = new UriBuilder(url);
                Uri currentUri = uriBuilder.Uri;
                currentDomain = currentUri.Host.Trim();
            }

            ViewBag.Domains = Bags.GetDomainsWithRows();

            if (!string.IsNullOrEmpty(currentDomain))
            {
                if (Bags.GetDomainsWithRows().Where(s => s.Value == currentDomain).FirstOrDefault() != null)
                {
                    List<SelectListItem> listDomains = new List<SelectListItem>();
                    listDomains = Bags.GetDomainsWithRows();
                    listDomains.Where(s => s.Value == currentDomain).FirstOrDefault().Selected = true;
                    ViewBag.Domains = listDomains;
                }
            }
            
            return View(pageRedirection);
        }

        [CmsAuthorize(Permission = "Create", ContentType = "Redirection")]
        public ActionResult Create()
        {
            TempData.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "Redirection")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.DOMAIN_CREATE, this));

            try
            {
                string requestProtocol = "", requestDomain = "", requestURL = "", targetProtocol = "", targetDomain = "", targetURL = "", redirectType = "";
                string redirectFrom = "", redirectTo = "";

                requestProtocol = !string.IsNullOrEmpty(collection["redirectRequestProtocol"]) ? collection["redirectRequestProtocol"] : "http";
                requestDomain = !string.IsNullOrEmpty(collection["redirectRequestDomain"]) ? collection["redirectRequestDomain"] : "";
                requestURL = !string.IsNullOrEmpty(collection["redirectRequestUrl"]) ? collection["redirectRequestUrl"] : "";
                targetProtocol = !string.IsNullOrEmpty(collection["redirectTargetProtocol"]) ? collection["redirectTargetProtocol"] : "http";
                targetDomain = !string.IsNullOrEmpty(collection["redirectTargetDomain"]) ? collection["redirectTargetDomain"] : "";
                targetURL = !string.IsNullOrEmpty(collection["redirectTargetUrl"]) ? collection["redirectTargetUrl"] : "";
                redirectType = !string.IsNullOrEmpty(collection["redirectType"]) ? collection["redirectType"] : "301";

                if (string.IsNullOrEmpty(requestDomain) || string.IsNullOrEmpty(targetDomain))
                {
                    throw new ApplicationException("Domain required!");
                }

                if (!string.IsNullOrEmpty(requestURL))
                {
                    requestURL = requestURL.Trim().StartsWith("/") ? requestURL.Trim() : "/" + requestURL;
                }

                if (!string.IsNullOrEmpty(targetURL))
                {
                    targetURL = targetURL.Trim().StartsWith("/") ? targetURL.Trim() : "/" + targetURL;
                }

                redirectTo = targetProtocol.Trim() + "://" + targetDomain.Trim() + targetURL.Trim();
                redirectFrom = requestProtocol.Trim() + "://" + requestDomain.Trim() + requestURL.Trim();

                PageRedirection insertPageRedirection = new PageRedirection();
                insertPageRedirection.CreateDate = DateTime.Now;
                insertPageRedirection.CreatedBy = (Guid)Membership.GetUser().ProviderUserKey;
                insertPageRedirection.UpdatedBy = (Guid)Membership.GetUser().ProviderUserKey;
                insertPageRedirection.RedirectFrom = redirectFrom;
                insertPageRedirection.RedirectTo = redirectTo;
                insertPageRedirection.RedirectType = redirectType;

                dbContext.PageRedirections.Add(insertPageRedirection);
                dbContext.SaveChanges();
                TempData["Message"] = "Your redirection has been successfully created";
                //cms_page_redirection redirection = new cms_page_redirection();
                //redirection.RedirectFrom = collection["redirectionsFrom"];
                //redirection.RedirectTo = collection["redirectionTo"];
                //redirection.CreatedBy = Membership.GetUser().ProviderUserKey;
                //var result = context.CreateRedirection(redirection);
                //switch (result[0].dStat)
                //{
                //    case "U":
                //        TempData["Message"] = "Your redirection names has been successfully updated.";
                //        break;
                //    default:
                //        TempData["Message"] = "Your redirection names has been successfully created.";
                //        break;
                //}
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
        [CmsAuthorize(Permission = "Edit", ContentType = "Redirection")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.DOMAIN_EDIT, this));
            try
            {
                string requestProtocol = "", requestDomain = "", requestURL = "", targetProtocol = "", targetDomain = "", targetURL = "", redirectType = "";
                string redirectFrom = "", redirectTo = "";

                requestProtocol = !string.IsNullOrEmpty(collection["redirectRequestProtocol"]) ? collection["redirectRequestProtocol"] : "http";
                requestDomain = !string.IsNullOrEmpty(collection["redirectRequestDomain"]) ? collection["redirectRequestDomain"] : "";
                requestURL = !string.IsNullOrEmpty(collection["redirectRequestUrl"]) ? collection["redirectRequestUrl"] : "";
                targetProtocol = !string.IsNullOrEmpty(collection["redirectTargetProtocol"]) ? collection["redirectTargetProtocol"] : "http";
                targetDomain = !string.IsNullOrEmpty(collection["redirectTargetDomain"]) ? collection["redirectTargetDomain"] : "";
                targetURL = !string.IsNullOrEmpty(collection["redirectTargetUrl"]) ? collection["redirectTargetUrl"] : "";
                redirectType = !string.IsNullOrEmpty(collection["redirectType"]) ? collection["redirectType"] : "301";

                if (string.IsNullOrEmpty(requestDomain) || string.IsNullOrEmpty(targetDomain))
                {
                    throw new ApplicationException("Domain required!");
                }

                if (!string.IsNullOrEmpty(requestURL))
                {
                    requestURL = requestURL.Trim().StartsWith("/") ? requestURL.Trim() : "/" + requestURL;
                }

                if (!string.IsNullOrEmpty(targetURL))
                {
                    targetURL = targetURL.Trim().StartsWith("/") ? targetURL.Trim() : "/" + targetURL;
                }

                redirectTo = targetProtocol.Trim() + "://" + targetDomain.Trim() + targetURL.Trim();
                redirectFrom = requestProtocol.Trim() + "://" + requestDomain.Trim() + requestURL.Trim();

                CmsDbContext dbContext = new CmsDbContext();
                PageRedirection pageRedirection = new PageRedirection();
                pageRedirection = dbContext.PageRedirections.Where(s => s.ID == id).FirstOrDefault();
                if (pageRedirection == null)
                {
                    throw new ApplicationException("Redirection is not found");
                }

                pageRedirection.RedirectFrom = redirectFrom;
                pageRedirection.RedirectTo = redirectTo;
                pageRedirection.RedirectType = redirectType;
                pageRedirection.UpdateDate = DateTime.Now;
                pageRedirection.UpdatedBy = (Guid)Membership.GetUser().ProviderUserKey;

                dbContext.Entry(pageRedirection).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                TempData["Message"] = "Your redirection has been successfully updated";

                //cms_page_redirection redirection = new cms_page_redirection();
                //redirection.ID = id;
                //redirection.RedirectFrom = collection["redirectionFrom"];
                //redirection.RedirectTo = collection["redirectionTo"];
                //redirection.CreatedBy = Membership.GetUser().ProviderUserKey;

                //var result = context.UpdateRedirection(redirection);
                //switch (result[0].dStat)
                //{
                //    case "U":
                //        TempData["Message"] = "Your redirection names has been successfully updated.";
                //        break;
                //    default:
                //        TempData["Message"] = "Your redirection names has been successfully created.";
                //        break;
                //}
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
        [CmsAuthorize(Roles = "PowerUser", Permission = "Delete", ContentType = "Redirection")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            TempData.Clear();
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.DOMAIN_DELETE, this));
            try
            {
                //CmsDbContext dbContext = new CmsDbContext();
                //dbContext.Database.ExecuteSqlCommand("delete from cms_page_redirection where ID = " + id);
                PageRedirection pageRedirection = new PageRedirection();
                pageRedirection = dbContext.PageRedirections.Where(s => s.ID == id).FirstOrDefault();
                if (pageRedirection != null)
                {
                    dbContext.PageRedirections.Attach(pageRedirection);
                    dbContext.PageRedirections.Remove(pageRedirection);
                    dbContext.SaveChanges();
                }
                TempData["Message"] = "Successfully Deleted!";
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
