using EuroCMS.Admin.Common;
using EuroCMS.Core;
using EuroCMS.Management;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,PowerUser,Editor")]
    public class SplashController : BaseController
    {
        [CmsAuthorize(Permission = "View", ContentType = "Portlet")]
        public ActionResult Index()
        {
            CmsDbContext dbContext = new CmsDbContext();

            List<Splash> result = new List<Splash>();

            result = dbContext.Splashes.Where(s => s.Status != 2).ToList();

            return View(result);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit", new { id = -1 });
        }

        [CmsAuthorize(Permission = "View", ContentType = "Portlet")]
        public ActionResult Edit(int ID)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.UrlRedirect, null);
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(-1, -1);

            List<Zone> listAllZones = new List<Zone>();

            CmsDbContext dbContext = new CmsDbContext();
            Splash result = new Splash();
            int articleId = 0, zoneId = 0;
            string headline = "";
            listAllZones = dbContext.Zones.Where(z => z.Status == "A").ToList();
            ViewBag.ZoneList = listAllZones;

            if (ID != null && ID > 0)
            {
                result = dbContext.Splashes.Where(s => s.ID == ID && s.Status != 2).FirstOrDefault();
            }

            if (result != null)
            {
                articleId = result.ArticleID;
                zoneId = result.ZoneID;
                vArticlesZonesFull getArticle = new vArticlesZonesFull();
                getArticle = dbContext.vArticlesZonesFulls.Where(s => s.ArticleID == articleId && s.ZoneID == zoneId).FirstOrDefault();
                if (getArticle != null)
                {
                    headline = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(getArticle.Headline.Trim()));
                    ViewBag.ContentArticle = headline;
                }
            }

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "Portlet")]
        public ActionResult Edit(int ID, FormCollection collection)
        {
            TempData.Clear();

            try
            {
                CmsDbContext dbContext = new CmsDbContext();
                Splash splash = new Splash();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                vAspNetMembershipUser getCurrentUser = new vAspNetMembershipUser();
                getCurrentUser = dbContext.vAspNetMembershipUsers.Where(v => v.IsApproved == true && v.UserId == currentUserId).FirstOrDefault();

                if (ID != null && ID > 0)
                {
                    WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SPLASH_EDIT, this));
                    splash = dbContext.Splashes.Where(s => s.ID == ID).FirstOrDefault();
                }
                else
                {
                    WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SPLASH_CREATE, this));
                }

                string status = "", name = "", articleZoneId = "", width = "", height = "", openTime = "", closeTime = "", modal = "", closeButton = "", cookie = "", cookieExpire = "", startDate = "", endDate = "";

                status = !string.IsNullOrEmpty(collection["splash_status"]) ? collection["splash_status"].Trim() : "0";
                name = !string.IsNullOrEmpty(collection["splash_name"]) ? collection["splash_name"].Trim() : "";
                articleZoneId = !string.IsNullOrEmpty(collection["content_article"]) ? collection["content_article"].Trim() : "";
                width = !string.IsNullOrEmpty(collection["splash_width"]) ? collection["splash_width"].Trim() : "720";
                height = !string.IsNullOrEmpty(collection["splash_height"]) ? collection["splash_height"].Trim() : "500";
                openTime = !string.IsNullOrEmpty(collection["splash_open_time"]) ? collection["splash_open_time"].Trim() : "0";
                closeTime = !string.IsNullOrEmpty(collection["splash_close_time"]) ? collection["splash_close_time"].Trim() : "0";
                modal = !string.IsNullOrEmpty(collection["splash_modal"]) ? collection["splash_modal"].Trim() : "0";
                closeButton = !string.IsNullOrEmpty(collection["splash_close_button"]) ? collection["splash_close_button"].Trim() : "0";
                cookie = !string.IsNullOrEmpty(collection["splash_cookie"]) ? collection["splash_cookie"].Trim() : "0";
                cookieExpire = !string.IsNullOrEmpty(collection["splash_cookie_expire"]) ? collection["splash_cookie_expire"].Trim() : "1";
                startDate = !string.IsNullOrEmpty(collection["splash_startdate"]) ? collection["splash_startdate"].Trim() : "";
                endDate = !string.IsNullOrEmpty(collection["splash_enddate"]) ? collection["splash_enddate"].Trim() : "";
                //afterOpen = !string.IsNullOrEmpty(collection["splash_after_open"]) ? collection["splash_after_open"].Trim() : "";
                //afterClose = !string.IsNullOrEmpty(collection["splash_after_close"]) ? collection["splash_after_close"].Trim() : "";

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(articleZoneId) || !articleZoneId.Contains("-"))
                {
                    TempData["HasError"] = true;
                    TempData["Message"] = "Name, Splash ID and Content Article can not empty!";
                    return View();
                }

                int articleId = 0, zoneId = 0;
                zoneId = Convert.ToInt32(articleZoneId.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                articleId = Convert.ToInt32(articleZoneId.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[1]);

                splash.ArticleID = articleId;
                splash.CloseButton = closeButton == "0" ? false : true;
                splash.CloseTime = closeTime;
                splash.Cookie = cookie == "0" ? false : true; //Convert.ToBoolean(cookie);
                splash.CookieExpire = cookieExpire;
                splash.EndDate = endDate;
                splash.Height = height;
                splash.IsModal = modal == "0" ? false : true; //Convert.ToBoolean(modal);
                splash.Name = name;
                splash.OpenTime = openTime;
                splash.StartDate = startDate;
                splash.Status = Convert.ToInt32(status);
                splash.UpdateDate = DateTime.Now;
                splash.Width = width;
                splash.ZoneID = zoneId;

                if (ID < 1 || splash == null)
                {
                    splash.CreatedBy = currentUserId;
                    splash.CreateDate = DateTime.Now;
                    dbContext.Splashes.Add(splash);
                    dbContext.SaveChanges();
                    // new ID
                    ID = splash.ID;
                    TempData["Message"] = "Your Splash has been successfully created";
                }
                else
                {
                    dbContext.Entry<Splash>(splash).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    TempData["Message"] = "Your Splash has been successfully updated";
                }

                //return RedirectToAction("Edit", new { id = ID });
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
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int ID)
        {
            TempData.Clear();

            CmsDbContext dbContext = new CmsDbContext();
            Splash getSplash = new Splash();
            getSplash = dbContext.Splashes.Where(s => s.ID == ID).FirstOrDefault();

            if (getSplash == null || ID < 0)
            {
                TempData["HasError"] = true;
                TempData["Message"] = "Splash is not available";
                return RedirectToAction("Index");
            }

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SPLASH_DELETE, this));
            getSplash.Status = 2;
            dbContext.Entry<Splash>(getSplash).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            TempData["Message"] = "Your Splash has been successfully deleted";

            return RedirectToAction("Index");
        }

        public ActionResult SplashList()
        {
            CmsDbContext dbContext = new CmsDbContext();

            List<Splash> result = new List<Splash>();

            result = dbContext.Splashes.Where(s => s.Status == 1).ToList();

            return View(result);
        }

    }
}
