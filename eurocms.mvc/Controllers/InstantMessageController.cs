using EuroCMS.Admin.Common;
using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class InstantMessageController : BaseController
    {
        //
        // GET: /InstantMessage/

        public ActionResult Index()
        {
            int currentPage = 0;
            int pageCount = 0;
            ViewBag.IsPager = false;

            if (Request.QueryString["page"] != null)
            {
                currentPage = Convert.ToInt32(Request.QueryString["page"]) <= 1 ? 0 : Convert.ToInt32(Request.QueryString["page"]);
            }


            Guid currentUserId = new Guid();
            currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            CmsDbContext dbContext = new CmsDbContext();
            List<InstantMessaging> listInstantMessaging = new List<InstantMessaging>();
            if (Request.IsAuthenticated)
            {
                currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                listInstantMessaging = dbContext.InstantMessagings.Where(im => im.To == currentUserId && im.DeleteDate == null).OrderByDescending(od => od.CreateDate).ToList();
                if (listInstantMessaging.Count() > 20)
                {
                    pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(listInstantMessaging.Count()) / 20));
                    ViewBag.IsPager = pageCount > 0;
                    ViewBag.PageCount = pageCount;
                    ViewBag.CurrentPage = currentPage == 0 ? 1 : currentPage;
                    listInstantMessaging = listInstantMessaging.Skip((currentPage - 1) * 20).Take(20).ToList();
                }
            }
            return View(listInstantMessaging);
        }

        [HttpPost]
        public JsonResult Detail(long id)
        {
            Guid currentUserId = new Guid();
            currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            CmsDbContext dbContext = new CmsDbContext();
            InstantMessaging getInstantMessaging = new InstantMessaging();
            InstantMessageJson getInstantMessageJson = new InstantMessageJson();

            getInstantMessaging = dbContext.InstantMessagings.Where(im => im.ID == id && im.To == currentUserId && im.DeleteDate == null).FirstOrDefault();

            string fromUserFullName = "";
            string toUserFullName = "";
            string contentLiveURL = "";
            string contentRequestRevisionURL = "";
            string contentName = "";
            string isUnRead = "0";
            if (getInstantMessaging != null)
            {
                if (getInstantMessaging.ReadDate == null)
                {
                    isUnRead = "1";
                    getInstantMessaging.ReadDate = DateTime.Now;
                    dbContext.Entry(getInstantMessaging).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                string fromUserName = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == getInstantMessaging.From).FirstOrDefault().UserName;
                var fromUserProfile = System.Web.Profile.ProfileBase.Create(fromUserName, false);
                fromUserFullName = fromUserProfile.GetPropertyValue("System.FullName").ToString().Trim();


                switch (getInstantMessaging.Type)
                {
                    case "AA":
                        ArticleRevision getLiveVersionA = new ArticleRevision();
                        ArticleRevision getRequestVersionA = new ArticleRevision();
                        getLiveVersionA = dbContext.ArticleRevisions.Where(ar => ar.ArticleId == getInstantMessaging.RelatedId && ar.RevisionStatus == "L").OrderByDescending(od => od.RevisionDate).FirstOrDefault();
                        getRequestVersionA = dbContext.ArticleRevisions.Where(ar => ar.ArticleId == getInstantMessaging.RelatedId && ar.RevisedBy == getInstantMessaging.From && ar.RevisionStatus == "N").OrderByDescending(od => od.RevisionDate).FirstOrDefault();

                        contentLiveURL = "/cms/Article/Edit/" + getInstantMessaging.RelatedId.ToString();
                        contentRequestRevisionURL = "/cms/Article/Edit/" + getInstantMessaging.RelatedId.ToString();
                        if (getLiveVersionA != null)
                        {
                            contentLiveURL += "?RevisionId=" + getLiveVersionA.RevisionId.ToString();
                        }

                        if (getRequestVersionA != null)
                        {
                            contentRequestRevisionURL += "?RevisionId=" + getRequestVersionA.RevisionId.ToString();
                        }
                        contentName = dbContext.Articles.Where(a => a.Id == getInstantMessaging.RelatedId).FirstOrDefault() != null ? dbContext.Articles.Where(a => a.Id == getInstantMessaging.RelatedId).FirstOrDefault().Headline : "";
                        break;
                    case "ZA":
                        ZoneRevision getLiveVersionZ = new ZoneRevision();
                        ZoneRevision getRequestVersionZ = new ZoneRevision();
                        getLiveVersionZ = dbContext.ZoneRevisions.Where(ar => ar.ZoneId == getInstantMessaging.RelatedId && ar.RevisionStatus == "L").OrderByDescending(od => od.RevisionDate).FirstOrDefault();
                        getRequestVersionZ = dbContext.ZoneRevisions.Where(ar => ar.ZoneId == getInstantMessaging.RelatedId && ar.RevisedBy == getInstantMessaging.From && ar.RevisionStatus == "N").OrderByDescending(od => od.RevisionDate).FirstOrDefault();

                        contentLiveURL = "/cms/Zone/Edit/" + getInstantMessaging.RelatedId.ToString();
                        contentRequestRevisionURL = "/cms/Zone/Edit/" + getInstantMessaging.RelatedId.ToString();
                        if (getLiveVersionZ != null)
                        {
                            contentLiveURL += "?RevisionId=" + getLiveVersionZ.RevisionId.ToString();
                        }

                        if (getRequestVersionZ != null)
                        {
                            contentRequestRevisionURL += "?RevisionId=" + getRequestVersionZ.RevisionId.ToString();
                        }
                        contentName = dbContext.Zones.Where(z => z.Id == getInstantMessaging.RelatedId).FirstOrDefault() != null ? dbContext.Zones.Where(z => z.Id == getInstantMessaging.RelatedId).FirstOrDefault().Name : "";
                        break;
                    case "FA":

                        ArticleFileRevision getLiveVersionFile = new ArticleFileRevision();
                        ArticleFileRevision getRequestVersionFile = new ArticleFileRevision();
                        getLiveVersionFile = dbContext.FileRevisions.Where(ar => ar.ArticleId == getInstantMessaging.RelatedId && ar.RevisionStatus == "L").OrderByDescending(od => od.RevisionDate).FirstOrDefault();
                        getRequestVersionFile = dbContext.FileRevisions.Where(ar => ar.ArticleId == getInstantMessaging.RelatedId && ar.RevisedBy == getInstantMessaging.From && ar.RevisionStatus == "N").OrderByDescending(od => od.RevisionDate).FirstOrDefault();

                        contentLiveURL = "/cms";
                        contentRequestRevisionURL = "/cms";
                        if (getLiveVersionFile != null)
                        {
                            contentLiveURL += "/ArticleFile?ArticleId=" + getInstantMessaging.RelatedId.ToString() + " &RevisionId=" + getInstantMessaging.RelatedName.ToString();
                        }

                        if (getRequestVersionFile != null)
                        {
                            contentRequestRevisionURL += "/ArticleFile?ArticleId=" + getInstantMessaging.RelatedId.ToString() + "&RevisionId=" + getInstantMessaging.RelatedName.ToString() + "&FileRevisionId=" + getRequestVersionFile.RevisionId.ToString();
                        }

                        contentName = dbContext.Articles.Where(a => a.Id == getInstantMessaging.RelatedId).FirstOrDefault() != null ? dbContext.Articles.Where(a => a.Id == getInstantMessaging.RelatedId).FirstOrDefault().Headline + " - ArticleFile Revizyonu" : "";

                        break;
                }

                getInstantMessageJson.CreateDate = getInstantMessaging.CreateDate;
                getInstantMessageJson.DeleteDate = getInstantMessaging.DeleteDate;
                getInstantMessageJson.Due = getInstantMessaging.Due;
                getInstantMessageJson.From = getInstantMessaging.From;
                getInstantMessageJson.FromUserFullName = fromUserFullName;
                getInstantMessageJson.ID = getInstantMessaging.ID;
                getInstantMessageJson.Message = getInstantMessaging.Message;
                getInstantMessageJson.ProcessDate = getInstantMessaging.ProcessDate;
                getInstantMessageJson.ReadDate = getInstantMessaging.ReadDate;
                getInstantMessageJson.RelatedId = getInstantMessaging.RelatedId;
                getInstantMessageJson.RelatedName = getInstantMessaging.RelatedName;
                getInstantMessageJson.Subject = getInstantMessaging.Subject;
                getInstantMessageJson.To = getInstantMessaging.To;
                getInstantMessageJson.ToUserFullName = toUserFullName;
                getInstantMessageJson.Type = getInstantMessaging.Type;
                getInstantMessageJson.ContentLiveURL = contentLiveURL;
                getInstantMessageJson.ContentRequestRevisionURL = contentRequestRevisionURL;
                getInstantMessageJson.ContentName = contentName;
                getInstantMessageJson.IsUnRead = isUnRead;
            }




            return Json(getInstantMessageJson, JsonRequestBehavior.AllowGet);
        }

        private class InstantMessageJson
        {
            public long ID { get; set; }
            public Guid From { get; set; }
            public Guid To { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
            public string Type { get; set; }
            public long RelatedId { get; set; }
            public string RelatedName { get; set; }
            DateTime _createDate = DateTime.Now;
            public DateTime CreateDate
            {
                get { return _createDate; }
                set { _createDate = value == null ? DateTime.Now : value; }
            }
            public DateTime? ReadDate { get; set; }
            public DateTime? ProcessDate { get; set; }
            public DateTime? DeleteDate { get; set; }
            public DateTime? Due { get; set; }
            public string FromUserFullName { get; set; }
            public string ToUserFullName { get; set; }
            public string ContentLiveURL { get; set; }
            public string ContentRequestRevisionURL { get; set; }
            public string ContentName { get; set; }
            public string IsUnRead { get; set; }
        }

        public ActionResult Delete(long id)
        {
            try
            {
                Guid currentUserId = new Guid();
                currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                CmsDbContext dbContext = new CmsDbContext();
                InstantMessaging getInstantMessaging = new InstantMessaging();
                getInstantMessaging = dbContext.InstantMessagings.Where(im => im.ID == id && im.To == currentUserId && im.DeleteDate == null).FirstOrDefault();

                if (getInstantMessaging != null)
                {
                    if (getInstantMessaging.ReadDate == null)
                    {
                        getInstantMessaging.ReadDate = DateTime.Now;
                    }
                    getInstantMessaging.DeleteDate = DateTime.Now;
                    dbContext.Entry(getInstantMessaging).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                TempData["Message"] = "Message delete succeeded";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Read(long id)
        {
            Guid currentUserId = new Guid();
            currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            CmsDbContext dbContext = new CmsDbContext();
            InstantMessaging getInstantMessaging = new InstantMessaging();
            getInstantMessaging = dbContext.InstantMessagings.Where(im => im.ID == id && im.To == currentUserId && im.DeleteDate == null).FirstOrDefault();

            if (getInstantMessaging != null)
            {
                if (getInstantMessaging.ReadDate == null)
                {
                    getInstantMessaging.ReadDate = DateTime.Now;
                    dbContext.Entry(getInstantMessaging).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllMessage()
        {
            try
            {
                Guid currentUserId = new Guid();
                currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                CmsDbContext dbContext = new CmsDbContext();
                List<InstantMessaging> listInstantMessage = new List<InstantMessaging>();
                listInstantMessage = dbContext.InstantMessagings.Where(im => im.To == currentUserId && im.DeleteDate == null).ToList();

                if (listInstantMessage != null)
                {
                    if (listInstantMessage.Count() > 0)
                    {
                        foreach (InstantMessaging instantMessage in listInstantMessage)
                        {
                            if (instantMessage.ReadDate == null)
                            {
                                instantMessage.ReadDate = DateTime.Now;
                            }
                            instantMessage.DeleteDate = DateTime.Now;
                            dbContext.InstantMessagings.Attach(instantMessage);
                            dbContext.Entry(instantMessage).State = EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }
                }

                TempData["Message"] = "Message delete succeeded";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult ReadAllMessage()
        {
            try
            {
                Guid currentUserId = new Guid();
                currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                CmsDbContext dbContext = new CmsDbContext();
                List<InstantMessaging> listInstantMessage = new List<InstantMessaging>();
                listInstantMessage = dbContext.InstantMessagings.Where(im => im.To == currentUserId && im.DeleteDate == null).ToList();

                if (listInstantMessage != null)
                {
                    if (listInstantMessage.Count() > 0)
                    {
                        foreach (InstantMessaging instantMessage in listInstantMessage)
                        {
                            if (instantMessage.ReadDate == null)
                            {
                                instantMessage.ReadDate = DateTime.Now;
                            }
                            dbContext.InstantMessagings.Attach(instantMessage);
                            dbContext.Entry(instantMessage).State = EntityState.Modified;
                            dbContext.SaveChanges();
                        }
                    }
                }

                TempData["Message"] = "Message read succeeded";
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
