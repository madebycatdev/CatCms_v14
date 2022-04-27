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
using PagedList;
using System.Web.Script.Serialization;
using EuroCMS.Model;
using System.IO;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using Newtonsoft.Json;


namespace EuroCMS.Admin.Controllers
{
    public class ZoneController : BaseController
    {
        ZoneDbContext context = new ZoneDbContext();

        IService<Zone> _service;
        IService<ZoneRevision> _revisionService;

        public ZoneController()
        {
            _service = new ZoneService(new ZoneRepository());
            _revisionService = new ZoneRevisionService(new ZoneRevisionRepository());
        }

        public ZoneController(ZoneService zoneService, ZoneRevisionService zoneRevisionService)
        {
            _service = zoneService;
            _revisionService = zoneRevisionService;
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "List", ContentType = "Zone")]
        public string ListByZoneGroupID(int? ZoneGroupId)
        {
            var result = _service.GetAllByParentId(ZoneGroupId ?? 0).Where(z => z.Status != "D");

            string json = JsonConvert.SerializeObject(result);

            return json;
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "Zone")]
        public ActionResult Index(int? SiteId, int? ZoneGroupId, int? Page)
        {
            SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : SiteId;

            ViewData["ZoneGroupID"] = ZoneGroupId ?? 0;
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, ZoneGroupId);

            int pageSize = 25;
            int pageNumber = (Page ?? 1);


            return View(_service.GetAllByParentId(ZoneGroupId ?? 0).Where(z => z.Status != "D").ToPagedList(pageNumber, pageSize));


        }

        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Zone")]
        public ActionResult QuickCreate(int ParentId)
        {
            TempData.Clear();

            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(null, null);
            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.Layouts = Bags.GetLayouts();
            ViewBag.zone_group_id = ParentId;


            return View();
        }

        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Zone")]
        public ActionResult QuickEdit(int id, int ParentId)
        {
            TempData.Clear();

            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(null, null);
            ViewBag.Layouts = Bags.GetLayouts();

            var LastRevisionID = context.SelectZoneLastRevision(Convert.ToInt32(id))[0];
            var result = context.SelectZonesRevisionDetails(LastRevisionID);

            return View(result);

        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Zone")]
        public ActionResult Create(int? SiteId, int? ZoneGroupId)
        {
            TempData.Clear();

            return RedirectToAction("Edit", new { id = -1, SiteId = SiteId, ZoneGroupId = ZoneGroupId });
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Zone")]
        public ActionResult Edit(int id, long? RevisionId, int? ZoneGroupId)
        {
            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(null, null);
            ViewBag.Layouts = Bags.GetLayouts();
            ViewBag.ZoneGroupID = ZoneGroupId != null ? ZoneGroupId : 0;

            CmsDbContext dbContext = new CmsDbContext();

            ViewData["zone_revisions"] = new List<ZoneRevision>();

            if (id > 0)
            {
                var zone = dbContext.Zones.Where(zn => zn.Id == id).FirstOrDefault(); //_service.Find(id);

                //_revisionService.GetAll().Where(z => z.ZoneId == zone.Id).ToList();
                var revisions = zone.Revisions.OrderByDescending(o => o.RevisionDate);

                var articleRevision = dbContext.ArticleRevisions.FirstOrDefault(f => f.NavigationZoneId == id);
                if (articleRevision != null)
                {
                    ViewBag.ArticleRevision = articleRevision;
                }

                ViewData["zone_revisions"] = revisions.ToList();

                ViewBag.ZoneID = id;

                if (RevisionId != null)
                {
                    ViewBag.CurrentRevisionID = RevisionId;

                    var revision = revisions.Where(z => z.RevisionId == RevisionId).FirstOrDefault();

                    return View(revision);
                }
                else
                {
                    long LastRevisionId = 1;

                    ZoneRevision ZoneRev = revisions.Where(r => r.RevisionStatus == "L").FirstOrDefault();
                    //RevisionId.ToString()
                    if (ZoneRev == null)
                    {
                        ZoneRevision zr = new ZoneRevision();
                        zr = revisions.Where(r => r.Zone.Status != "D").FirstOrDefault();
                        if (zr != null)
                        {
                            if (zr.RevisionId != null)
                            {
                                LastRevisionId = zr.RevisionId;
                            }
                        }

                    }
                    else
                    {
                        if (ZoneRev.RevisionId != null)
                        {
                            LastRevisionId = ZoneRev.RevisionId;
                        }
                    }


                    ViewBag.CurrentRevisionID = LastRevisionId;

                    var lastRevision = revisions.Where(r => r.RevisionId == LastRevisionId).FirstOrDefault();

                    if (string.IsNullOrEmpty(lastRevision.Alias))
                    {
                        lastRevision.Alias = CheckAlias(lastRevision.Name, id.ToString(), lastRevision.ZoneGroupId.ToString());
                        lastRevision.Alias = JsonConvert.DeserializeObject(lastRevision.Alias).ToString();
                    }

                    return View(lastRevision);
                }

            }

            ZoneRevision empty = new ZoneRevision();
            empty.ZoneStatus = "A";
            SiteService site = new SiteService(new SiteRepository());
            Site SiteDetails = new Site();
            if (Convert.ToInt32(Session["CurrentSiteID"]) != 0)
            {
                SiteDetails = site.Find(Convert.ToInt32(Session["CurrentSiteID"]));
                empty.LangId = SiteDetails.ZoneGroups.FirstOrDefault().Zones.FirstOrDefault().LangId;
            }
            return View(empty);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Zone")]
        public ActionResult Edit(int id, FormCollection collection, bool? ForceApprove, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_CREATE, this));

            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(null, null);
            ViewBag.Layouts = Bags.GetLayouts();

            ViewData["zone_revisions"] = new List<ZoneRevision>();

            CmsDbContext dbContext = new CmsDbContext();

            Zone zone = new Zone();

            if (id > 0)
            {
                zone = dbContext.Zones.Where(zn => zn.Id == id).FirstOrDefault(); //_service.Find(id);

                var revisions = zone.Revisions.OrderByDescending(o => o.RevisionDate);

                ViewData["zone_revisions"] = revisions.ToList();

            }

            string targetZoneGroupId = collection["TargetZoneGroupId"] ?? "0";

            ViewBag.ZoneGroupID = targetZoneGroupId;

            ZoneRevision z = new ZoneRevision();

            if (ModelState.IsValid)
            {
                try
                {
                    string keywords = collection["zone_keywords"] ?? "";
                    if (!string.IsNullOrEmpty(collection["keywords_ao"]) && collection["keywords_ao"] == "O")
                        keywords = "|" + keywords;


                    if (collection["revision_id"] != null && collection["revision_id"] != "")
                        z.RevisionId = Convert.ToInt64(collection["revision_id"]);
                    else
                        z.RevisionId = -1;

                    z.ZoneId = id;
                    z.Keywords = keywords;
                    z.RevisionName = HttpUtility.HtmlEncode(collection["rev_name"]) ?? "";
                    z.RevisionNote = HttpUtility.HtmlEncode(collection["rev_note"]) ?? "";
                    z.Analytics = HttpUtility.HtmlEncode(collection["analytics"]) ?? "";
                    z.TemplateId = string.IsNullOrEmpty(collection["template_id"]) ? 0 : Convert.ToInt32(collection["template_id"]);
                    z.MobileTemplateId = string.IsNullOrEmpty(collection["template_id_mobile"]) ? 0 : Convert.ToInt32(collection["template_id_mobile"]);
                    z.CustomBody = HttpUtility.HtmlEncode(collection["custom_body"]) ?? "";
                    z.Article1 = HttpUtility.HtmlEncode(collection["article_1"]) ?? "";
                    z.Article2 = HttpUtility.HtmlEncode(collection["article_2"]) ?? "";
                    z.Article3 = HttpUtility.HtmlEncode(collection["article_3"]) ?? "";
                    z.Article4 = HttpUtility.HtmlEncode(collection["article_4"]) ?? "";
                    z.Article5 = HttpUtility.HtmlEncode(collection["article_5"]) ?? "";
                    z.Append1 = Convert.ToByte(collection["append_1"]);
                    z.Append2 = Convert.ToByte(collection["append_2"]);
                    z.Append3 = Convert.ToByte(collection["append_3"]);
                    z.Append4 = Convert.ToByte(collection["append_4"]);
                    z.Append5 = Convert.ToByte(collection["append_5"]);
                    z.RevisedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    z.MetaDescription = HttpUtility.HtmlEncode(collection["meta_description"]) ?? "";
                    z.ContentEditorType1 = HttpUtility.HtmlEncode(collection["content_1_editor_type"]) ?? "H";
                    z.ContentEditorType2 = HttpUtility.HtmlEncode(collection["content_2_editor_type"]) ?? "H";
                    z.ContentEditorType3 = HttpUtility.HtmlEncode(collection["content_3_editor_type"]) ?? "H";
                    z.ContentEditorType4 = HttpUtility.HtmlEncode(collection["content_4_editor_type"]) ?? "H";
                    z.ContentEditorType5 = HttpUtility.HtmlEncode(collection["content_5_editor_type"]) ?? "H";
                    z.DefaultArticle = HttpUtility.HtmlEncode(collection["default_article"]) ?? "";
                    z.OmnitureCode = HttpUtility.HtmlEncode(collection["omniture_code"]) ?? "";
                    z.DisplayName = HttpUtility.HtmlEncode(collection["zone_name_display"]) ?? "";
                    z.RevisionStatus = HttpUtility.HtmlEncode(collection["revision_status"]) ?? "N";
                    z.BeforeBody = HttpUtility.HtmlEncode(collection["before_body"]) ?? "";
                    z.BeforeHead = HttpUtility.HtmlEncode(collection["before_head"]) ?? "";

                    //2017-09-18
                    z.AfterBody = HttpUtility.HtmlEncode(collection["afterbody"]);
                    //2017-09-18

                    #region Alias
                    string alias = collection["zone_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["zone_name"], id.ToString(), z.ZoneGroupId.ToString());
                    }

                    z.Alias = alias;
                    #endregion

                    if (!string.IsNullOrEmpty(targetZoneGroupId) && !targetZoneGroupId.Equals("0"))
                        z.ZoneGroupId = Convert.ToInt32(targetZoneGroupId);
                    else
                        z.ZoneGroupId = collection["zone_group_id"] != null && !string.IsNullOrEmpty(collection["zone_group_id"]) ? Convert.ToInt32(collection["zone_group_id"]) : -1;

                    z.ZoneTypeId = Convert.ToInt32(collection["zone_type_id"] ?? "1");

                    z.ZoneStatus = collection["zone_status"] ?? "P";
                    z.Name = collection["zone_name"];
                    z.Description = collection["zone_desc"] ?? "";
                    //z.cio = Session["CMS_ENABLE_CHECK_OUT"].ToString();
                    z.LangId = collection["LangId"];
                    z.RevisionDate = DateTime.Now;
                    z.Created = DateTime.Now;
                    z.CreatedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                    if (z.ZoneGroupId < 1)
                        throw new ApplicationException("Zone Group required!");

                    if (String.IsNullOrEmpty(z.Name))
                        throw new ApplicationException("Zone Name required!");

                    if (String.IsNullOrEmpty(z.LangId))
                        throw new ApplicationException("Language required!");

                    if (String.IsNullOrEmpty(z.Alias))
                        throw new ApplicationException("Alias required!");

                    //var result = dbContext.Zones.Where(zn => zn.Name == z.Name).FirstOrDefault(); //_service.FindByName(z.Name);

                    // Zone Name Control
                    var zoneResult = dbContext.Zones.Where(zn => zn.Name == z.Name && zn.ZoneGroupId == z.ZoneGroupId).FirstOrDefault();
                    //if (zoneResult != null && result.ZoneGroupId != z.ZoneGroupId)
                    //{
                    //    throw new ApplicationException("This Zone name is already used. Please choose another one.");
                    //}

                    if (zoneResult == null || (zoneResult.Id == z.ZoneId))
                    {
                        if (z.ZoneId < 1)
                        {
                            zone.Keywords = keywords;
                            zone.Analytics = z.Analytics;
                            zone.TemplateId = z.TemplateId;
                            zone.MobileTemplateId = z.MobileTemplateId;
                            zone.CustomBody = z.CustomBody;
                            zone.Article1 = z.Article1;
                            zone.Article2 = z.Article2;
                            zone.Article3 = z.Article3;
                            zone.Article4 = z.Article4;
                            zone.Article5 = z.Article5;
                            zone.Append1 = z.Append1;
                            zone.Append2 = z.Append2;
                            zone.Append3 = z.Append3;
                            zone.Append4 = z.Append4;
                            zone.Append5 = z.Append5;
                            zone.MetaDescription = z.MetaDescription;
                            zone.DefaultArticle = z.DefaultArticle;
                            zone.OmnitureCode = z.OmnitureCode;
                            zone.DisplayName = z.DisplayName;
                            zone.Created = z.Created;
                            zone.CreatedBy = z.CreatedBy;
                            zone.Updated = DateTime.Now;
                            zone.ZoneGroupId = z.ZoneGroupId;
                            zone.ZoneTypeId = z.ZoneTypeId;
                            zone.Status = z.ZoneStatus;
                            zone.Name = z.Name;
                            zone.Description = z.Description;
                            zone.LangId = z.LangId;
                            z.Created = DateTime.Now;
                            zone = _service.Insert(zone);
                            z.ZoneId = zone.Id;
                            zone.BeforeBody = z.BeforeBody;
                            zone.BeforeHead = z.BeforeHead;
                            zone.AfterBody = z.AfterBody;
                        }

                        if (z.RevisionId < 1)
                        {
                            z = _revisionService.Insert(z);

                            TempData["Message"] = "Your Zone has been successfully created.";
                        }
                        else
                        {

                            if (z.RevisionStatus == "N")
                            {
                                z = _revisionService.Update(z);
                            }
                            else
                            {
                                z.RevisionStatus = "N";
                                z = _revisionService.Insert(z);
                            }

                            TempData["Message"] = "Your Zone has been successfully updated.";
                        }
                    }
                    else
                        throw new ApplicationException("This Zone name is already used. Please choose another one.");


                    if (!string.IsNullOrEmpty(collection["forceapprove"]))
                    {
                        if (Convert.ToBoolean(collection["forceapprove"]))
                        {
                            ForceApprove = true;
                        }
                    }


                    if ((ForceApprove ?? false))
                        return Approve((id < 1 ? z.ZoneId : id), z.RevisionId, ReturnUrl);

                    return RedirectToAction("Edit", new { id = zone.Id, RevisionId = z.RevisionId });
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    ModelState.AddModelError("HATA", ex.Message);

                    return View(z);
                }
            }

            return View(z);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Delete", ContentType = "Zone")]
        public ActionResult Delete(int id, bool? ForceApprove, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_DELETE, this));

            try
            {

                //var zone = _service.Find(id);

                //if (zone == null)
                //    throw new Exception("The zone was not found");

                //if(zone.Articles.Count > 0)
                //    throw new ApplicationException("This zone have related articles. You need to delete this relations first. Zone NOT deleted." + zone.Articles.Select(m => m.Headline).InHtmlNewLine());

                //ZoneRevision rev = new ZoneRevision() { 
                //    ZoneId = id,
                //    RevisionStatus = "D",
                //    Created = DateTime.Now,
                //    RevisionDate = DateTime.Now,
                //    RevisedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()),
                //    CreatedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())
                //};

                //_revisionService.Delete(rev);

                // approve revision

                //TempData["Message"] = "Successfully Deleted.";

                //if (zoneGroup.Zones.Count > 1)
                //    throw new Exception("The ZoneGroup has one or more Zone");

                //_service.Delete(zoneGroup);

                var result = context.DeleteZone(id, Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]), Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]), Session["CMS_ENABLE_CHECK_OUT"].ToString());

                switch (result[0].aStat)
                {
                    case "0":
                        TempData["Message"] = "Waiting Delete Approval. ";
                        //if ((ForceApprove ?? false))
                        //    ApproveRevision(Convert.ToInt64(result[0].rev_id), ReturnUrl);
                        break;
                    case "DELETED":

                        break;
                    case "1":

                    case "2":
                        throw new ApplicationException("This zone have relations for menu structure. You need to delete this relations first. Zone NOT deleted." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "3":
                        throw new ApplicationException("This zone have relations. You need to delete this relations first. Zone NOT deleted. " + result.Select(m => m.found_name).InHtmlNewLine());
                    case "4":
                        throw new ApplicationException("Zone not found or already deleted before");
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

            //if (!string.IsNullOrEmpty(ReturnUrl))
            //    return Redirect(ReturnUrl);
            //else
            //    return RedirectToActionSite("Index");

            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
            {
                ReturnUrl = ReturnUrl.Replace("##ZoneID##", id.ToString());
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToActionSite("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,ContentEntry,UserCreator", Permission = "Approve", ContentType = "Zone")]
        public ActionResult Approve(int id, long RevisionId, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_APPROVE, this));

            try
            {
                var result = context.ApproveZoneRevision(RevisionId, Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]), Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]), Session["CMS_ENABLE_CHECK_OUT"].ToString());

                switch (result[0].aStat)
                {
                    case "0":
                    case "DELETED":
                        TempData["Message"] = "Your zone has been successfully deleted. ";
                        break;
                    case "LOCKED":
                        if (!string.IsNullOrEmpty(result[0].locked_by))
                            throw new ApplicationException("This zone was locked by " + result[0].locked_by + " at " + result[0].locked + " and you didn't save.<br /><br />If you want to save your changes, please contact with " + result[0].locked_by + " or Administrator to release lock");
                        else
                            throw new ApplicationException("You can not approve this zone. Administrator or someone else release your lock. Please open an edit window at another window and click approve button (at this window) again");
                    case "OK":
                        TempData["Message"] = "Your zone revision is approved and published.";
                        break;
                    case "OKA":
                        TempData["Message"] = "Your zone revision is approved and ready for administrator approval.";
                        break;
                    case "NOT_AVAILABLE":
                        throw new ApplicationException("Your zone is NOT available for approval. Zone not found or already approved before.");
                    case "ARTICLE_EXIST":
                        throw new ApplicationException("Your zone is NOT available for delete approval. There are some articles related to this zone. " + result.Select(m => m.found_name).InHtmlNewLine());
                    case "MENU_EXIST":
                        throw new ApplicationException("Your zone is NOT available for delete approval. There are some menu relations exists. " + result.Select(m => m.found_name).InHtmlNewLine());
                    case "CANT_DELETE":
                        switch (result[0].rStat)
                        {
                            case "USED_IN_DOMAIN_HOME_PAGE":
                                throw new ApplicationException("This zone is default article of these domain(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_DOMAIN_404_PAGE":
                                throw new ApplicationException("This zone is 404 article of these domain(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_ALIAS_REDIRECTION":
                                throw new ApplicationException("This zone is used by these redirection(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_SITE_DEFAULT_ARTICLE":
                                throw new ApplicationException("This zone is default article of these site(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_ZONE_GROUP_DEFAULT_ARTICLE":
                                throw new ApplicationException("This zone is default article of these zone group(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_SITE_TAG_DETAIL_ARTICLE":
                                throw new ApplicationException("This zone is tag detail article of these site(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE":
                                throw new ApplicationException("This zone is tag detail article of these zone group(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_INTERNAL_ARTICLE_REDIRECTION":
                                throw new ApplicationException("This zone is intenal redirected articles of these article(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                            case "USED_IN_MAPPED_ARTICLE":
                                throw new ApplicationException("This zone is mapped articles of these article(s). " + result.Select(m => m.tStat).InHtmlNewLine());
                        }
                        break;
                    default:
                        throw new ApplicationException("Error occured on zone approval. Error Code:" + result[0].aStat);
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            if (!string.IsNullOrEmpty(ReturnUrl))
                return Redirect(ReturnUrl);
            else
                return RedirectToAction("Edit", new { id = id, RevisionId = RevisionId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,ContentEntry,UserCreator", Permission = "Discard", ContentType = "Zone")]
        public ActionResult Discard(int id, long RevisionId, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_DISCARD, this));

            try
            {
                var result = context.DiscardZoneRevision(RevisionId, Membership.GetUser().ProviderUserKey);

                switch (result[0])
                {

                    case "OK":
                        TempData["Message"] = "Revision successfully discarded.";
                        break;
                    case "STATUS":
                        throw new ApplicationException("You can not discard this revision. Already processed.");
                    case "NP":
                        throw new ApplicationException("You can not discard this revision. You do not have a permission.");
                    default:
                        throw new ApplicationException("unexpected error! please contact with system administrator.");
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            if (!string.IsNullOrEmpty(ReturnUrl))
                return Redirect(ReturnUrl);
            else
                return RedirectToAction("Edit", new { id = id, RevisionId = RevisionId });
        }

        // Send To Approve Start

        public struct SendToApproveType
        {
            public const string ArticleApprove = "AA";
            public const string ZoneApprove = "ZA";
            public const string ArticleFileApprove = "FA";
        }

        public string GetSendToApproveName(string type)
        {
            string returnVal = "";
            switch (type)
            {
                case "AA":
                    returnVal = "Article Approve";
                    break;
                case "ZA":
                    returnVal = "Zone Approve";
                    break;
                case "FA":
                    returnVal = "Article File Approve";
                    break;
            }
            return returnVal;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "Article")]
        public ActionResult SendToApprove(int ZoneId, long? RevisionId, Guid UserId, string SendToApproveMessage, string ReturnUrl)
        {
            TempData.Clear();

            try
            {
                if (string.IsNullOrEmpty(UserId.ToString()) || UserId.ToString() == "-1")
                {
                    throw new ApplicationException("Please select user");
                }

                CmsDbContext dbContext = new CmsDbContext();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                vAspNetMembershipUser getCurrentUser = new vAspNetMembershipUser();
                vAspNetMembershipUser getSelectedUser = new vAspNetMembershipUser();
                vArticlesZonesFull getArticleZoneFull = new vArticlesZonesFull();
                InstantMessaging getInstantMessaging = new InstantMessaging();

                getArticleZoneFull = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ZoneID == ZoneId).FirstOrDefault();
                getSelectedUser = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == UserId && v.IsApproved && !v.IsLockedOut).FirstOrDefault();
                getCurrentUser = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == currentUserId && v.IsApproved && !v.IsLockedOut).FirstOrDefault();

                if (getArticleZoneFull == null)
                {
                    throw new ApplicationException("Zone is not found");
                }
                if (getSelectedUser == null || getCurrentUser == null)
                {
                    throw new ApplicationException("User is not found");
                }

                getInstantMessaging = dbContext.InstantMessagings.Where(im => im.From == currentUserId && im.Type == SendToApproveType.ArticleApprove.ToString() && im.RelatedId == ZoneId && im.ReadDate == null).FirstOrDefault();

                if (getInstantMessaging != null)
                {
                    throw new ApplicationException("You can not send to approve request for this article because you already sent");
                }

                var selectedUserProfile = System.Web.Profile.ProfileBase.Create(getSelectedUser.UserName, false);
                var currentUserProfile = System.Web.Profile.ProfileBase.Create(getCurrentUser.UserName, false);

                string selectedUserFullName = selectedUserProfile.GetPropertyValue("System.FullName").ToString().Trim();
                string currentUserFullName = currentUserProfile.GetPropertyValue("System.FullName").ToString().Trim();

                string articleRevisionPreviewUrl = "http://" + HttpContext.Request.Url.Host.ToString() + "/web/-1,-1," + (RevisionId != null ? RevisionId.ToString() : ZoneId.ToString());
                string articleUrl = "";
                string editArticleUrl = "";
                string mailBody = "";

                articleUrl = getArticleZoneFull.ArticleZoneAlias;
                if (string.IsNullOrEmpty(articleUrl))
                {
                    articleUrl = CmsHelper.getContentLinkAlias(getArticleZoneFull.ZoneID.ToString(), getArticleZoneFull.ArticleID.ToString(), getArticleZoneFull.SiteName, getArticleZoneFull.ZoneGroupName, getArticleZoneFull.ZoneName, getArticleZoneFull.Headline, "");
                }
                articleUrl = "http://" + HttpContext.Request.Url.Host + (articleUrl.StartsWith("/") ? articleUrl : "/" + articleUrl);

                editArticleUrl = "http://" + HttpContext.Request.Url.Host + "/cms/Zone/Edit/" + ZoneId.ToString() + "?RevisionId=" + RevisionId.ToString();

                //mailBody = "Sayın <b>" + selectedUserFullName + "</b>,<br><br> ";
                //mailBody += "<b>" + currentUserFullName + "</b> kullanıcısı " + getArticleZoneFull.Headline.Trim() + " isimli article üzerinde bir değişiklik yaptı ve bunu onaylamanızı istiyor.<br> ";
                //mailBody += "Yapılan değişikliği <a href='" + articleRevisionPreviewUrl + "' target='_blank' style='text-decoration:none'>buraya</a>, şu anki halini ise ";
                //mailBody += "<a href='" + articleUrl + "' target='_blank' style='text-decoration:none'>buraya</a> tıklayarak görebilirsiniz.<br> ";
                //mailBody += "Değişiklikleri hemen onaylamak için <a href='" + editArticleUrl + "' target='_blank' style='text-decoration:none'>buraya</a> tıklayabilirsiniz.<br><br> ";


                // Mail Template Render Start
                var view = ViewEngines.Engines.FindView(ControllerContext, "SendToApproveMailTemplate", null);
                var writer = new StringWriter();
                var viewContext = new ViewContext(ControllerContext, view.View, ViewData, TempData, writer);
                view.View.Render(viewContext, writer);
                writer.Flush();
                mailBody = writer.ToString();
                int startIndex = mailBody.IndexOf("<!-- Start Template -->");
                int endIndex = mailBody.IndexOf("<!-- End Template -->");
                mailBody = mailBody.Substring(startIndex, endIndex - startIndex);
                mailBody = mailBody.Replace("<!-- Start Template -->", "");

                mailBody = mailBody.Replace("##ApproveType##", "ZONE");
                mailBody = mailBody.Replace("##senderNameSurname##", currentUserFullName);
                mailBody = mailBody.Replace("##message##", SendToApproveMessage);
                mailBody = mailBody.Replace("##contentName##", getArticleZoneFull.ZoneName.Trim());
                mailBody = mailBody.Replace("##contentUrl##", editArticleUrl);
                mailBody = mailBody.Replace("##contentCMSUrl##", editArticleUrl);
                mailBody = mailBody.Replace("##date##", DateTime.Now.ToString("dd MMMM yyyy hh:mm"));

                mailBody = "<html><head></head>" + mailBody + "</html>";

                // Mail Template Render End


                // DB Insert Start
                InstantMessaging insertInstantMessaging = new InstantMessaging();
                insertInstantMessaging.CreateDate = DateTime.Now;
                insertInstantMessaging.From = currentUserId;
                insertInstantMessaging.Message = SendToApproveMessage.Trim(); //currentUserFullName + " kullanıcısı " + getArticleZoneFull.Headline.Trim() + " isimli article üzerinde bir değişiklik yaptı ve bunu onaylamanızı istiyor.";
                insertInstantMessaging.Subject = "Zone Approve Request";
                insertInstantMessaging.RelatedId = getArticleZoneFull.ZoneID;
                insertInstantMessaging.RelatedName = "Zone";
                insertInstantMessaging.To = UserId;
                insertInstantMessaging.Type = SendToApproveType.ZoneApprove;
                dbContext.InstantMessagings.Add(insertInstantMessaging);
                dbContext.SaveChanges();
                // DB Insert End


                // MAIL SEND START
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                SmtpSection smtpInfo = new SmtpSection();
                smtpInfo = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");

                SmtpClient smtpClient = new SmtpClient(smtpInfo.Network.Host, smtpInfo.Network.Port);
                if (smtpInfo.Network.DefaultCredentials)
                {
                    smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                else
                {
                    if (string.IsNullOrEmpty(smtpInfo.Network.ClientDomain))
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password);
                    }
                    else
                    {
                        smtpClient.Credentials = new System.Net.NetworkCredential(smtpInfo.Network.UserName, smtpInfo.Network.Password, smtpInfo.Network.ClientDomain);
                    }
                }

                smtpClient.UseDefaultCredentials = smtpInfo.Network.DefaultCredentials;
                smtpClient.EnableSsl = smtpInfo.Network.EnableSsl;
                smtpClient.DeliveryMethod = smtpInfo.DeliveryMethod;

                MailMessage mail = new MailMessage();

                mail.IsBodyHtml = true;
                mail.Subject = "Zone Approve Request";
                mail.To.Add(getSelectedUser.Email.Trim());
                mail.From = new MailAddress(smtpInfo.From);
                mail.Body = mailBody;
                smtpClient.Send(mail);
                // MAIL SEND END

                TempData["Message"] = "Success";

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }


            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("Edit", new { id = ZoneId, RevisionId = RevisionId });
            }
        }
        // Send To Approve End


        [CmsAuthorize()]
        public string CheckAlias(string text, string id, string parentId)
        {
            string result = string.Empty;
            text = text.Trim();

            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    string cleanText = CmsHelper.StringToAlphaNumeric(text, false);
                    int currentZoneGroupId = Convert.ToInt32(parentId);
                    CmsDbContext dbContext = new CmsDbContext();
                    List<Zone> zones = dbContext.Zones.Where(x => x.Alias == cleanText && x.ZoneGroupId == currentZoneGroupId).ToList();

                    int currentId = Convert.ToInt32(id);

                    zones.Remove(zones.Where(x => x.Id == currentId).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                    if (zones == null || zones.Count == 0)
                    {
                        //ok 
                        result = cleanText;
                    }
                    else
                    {
                        //çakışma var 
                        int counter = 2;
                        while (dbContext.Zones.Where(x => x.Alias == cleanText + "-" + counter && x.ZoneGroupId == currentZoneGroupId).ToList().Count > 0)
                        {
                            counter++;
                        }
                        //son - cleanText + "-" + counter
                        result = cleanText + "-" + counter;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "_#NOK#_";
                CmsHelper.SaveErrorLog(ex, "Can't create zone alias", true);
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
