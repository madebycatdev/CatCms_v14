using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using EuroCMS.Admin.entity;
using System.Web.Mvc.Html;
using System.Globalization;
using System.Web.Security;
using System.Web.Management;
using EuroCMS.Core;
using EuroCMS.Management;
using EuroCMS.Model;
using System.IO;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Data.OleDb;
using System.Data;
using System.Xml;
using System.Net;
using System.Text;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using EuroCMS.Admin.ViewModels;

namespace EuroCMS.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        ArticleDbContext context = new ArticleDbContext();
        ClassificationDbContext cContext = new ClassificationDbContext();
        ZoneDbContext zContext = new ZoneDbContext();



        [CmsAuthorize(Permission = "List", ContentType = "Article")]
        public JsonResult ListByZoneID(int? ZoneId)
        {
            var result = context.SelectArticlesByZone(ZoneId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [CmsAuthorize(Permission = "List")]
        public JsonResult ArticleZoneName(string Ids)
        {
            string[] ids = Ids.Split('-');

            var result = context.SelectArticleZonesNames(Convert.ToInt32(ids[0]), Convert.ToInt32(ids[1]));

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "View", ContentType = "Article")]
        public ActionResult Index(int? Page, string Keyword, int? SortOrder, int? ZoneGroupId, int? ZoneId, bool? IsRevision, int? Status, bool? Status0, bool? Status1, bool? Status2,
            bool? RevFlag1, bool? RevFlag2, bool? RevFlag3, bool? RevFlag4, bool? RevFlag5,
            string Language, string ArticleIDs, string Alias,
            DateTime? DisplayedStart, DateTime? DisplayedEnd, DateTime? ModifiedStart, DateTime? ModifiedEnd, DateTime? ApprovedStart, DateTime? ApprovedEnd,
            int? ClsfId, int? SiteId, Guid? UserId, string TagID, string fileTypes)
        {
            if (Language == null)
            {
                Language = "";
            }

            // Zone List Start
            CmsDbContext dbContext = new CmsDbContext();
            List<Zone> listAllZones = new List<Zone>();
            listAllZones = dbContext.Zones.Where(z => z.Status == "A").ToList();
            List<string> allowedArticleIds = null;
            List<string> disAllowedArticleIds = null;

            SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : SiteId;

            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, null);
            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.Zones = Bags.GetZonesByZoneGroup(ZoneId ?? 0, ZoneGroupId ?? 0);
            //user yetkisine göre zone'ları filtrele
            if (!User.IsInRole("Administrator"))
            {
                #region Filter Articles
                allowedArticleIds = new List<string>();
                disAllowedArticleIds = new List<string>();

                List<string> roles = System.Web.Security.Roles.GetRolesForUser(User.Identity.Name).ToList();    //userın bağlı olduğu roller
                List<vw_cms_AccessRules> accessRules = dbContext.CmsAccessRules.Where(x => roles.Contains(x.Roles)).ToList();   //bu rollere ait rule'lar

                List<string> allowedZones = new List<string>();
                List<string> disAllowedZones = new List<string>();

                List<string> allowedArticles = accessRules.Where(x => x.ContentType == "article" && !string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();
                List<string> disAllowedArticles = accessRules.Where(x => x.ContentType == "article" && string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();
                List<string> allowedArticleZones = dbContext.vArticlesZonesFulls.Where(x => allowedArticles.Contains(x.ArticleID.ToString())).Select(x => x.ZoneID.ToString()).ToList();
                List<string> disAllowedArticleZones = dbContext.vArticlesZonesFulls.Where(x => disAllowedArticles.Contains(x.ArticleID.ToString())).Select(x => x.ZoneID.ToString()).ToList();
                allowedZones.AddRange(allowedArticleZones);     //izin verilenler listesine zoneları ekle
                allowedArticleIds.AddRange(allowedArticles);    //izin verilen article listesine ekle
                //disAllowedZones.AddRange(disAllowedArticleZones);   //izin verilmeyenler listesine zoneları ekle
                disAllowedArticleIds.AddRange(disAllowedArticles);    //izin verilmeyen article listesine ekle

                List<string> allowedZoneList = accessRules.Where(x => x.ContentType == "zone" && !string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();   //izin verilenler listesine zoneları ekle
                allowedZones.AddRange(allowedZoneList);
                allowedArticleIds.AddRange(dbContext.vArticlesZonesFulls.Where(x => allowedZoneList.Contains(x.ZoneID.ToString())).Select(x => x.ArticleID.ToString()).ToList());    //izin verilen article listesine ekle

                List<string> allowedZoneGroups = accessRules.Where(x => x.ContentType == "zonegroup" && !string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();
                List<string> disAllowedZoneGroups = accessRules.Where(x => x.ContentType == "zonegroup" && string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();
                foreach (string zg in allowedZoneGroups)
                {
                    List<string> allowedZgZones = Bags.GetZonesByZoneGroup(0, Convert.ToInt32(zg)).Select(x => x.Value).ToList();
                    allowedZones.AddRange(allowedZgZones);  //izin verilenler listesine zoneları ekle
                    allowedArticleIds.AddRange(dbContext.vArticlesZonesFulls.Where(x => allowedZgZones.Contains(x.ZoneGroupID.ToString())).Select(x => x.ArticleID.ToString()).ToList());    //izin verilen article listesine ekle
                }
                foreach (string zg in disAllowedZoneGroups)
                {
                    List<string> disAllowedZgZones = Bags.GetZonesByZoneGroup(0, Convert.ToInt32(zg)).Select(x => x.Value).ToList();
                    disAllowedZones.AddRange(disAllowedZgZones);  //izin verilmeyenler listesine zoneları ekle
                    disAllowedArticleIds.AddRange(dbContext.vArticlesZonesFulls.Where(x => disAllowedZgZones.Contains(x.ZoneGroupID.ToString())).Select(x => x.ArticleID.ToString()).ToList());    //izin verilen article listesine ekle
                }

                List<string> allowedSites = accessRules.Where(x => x.ContentType == "site" && !string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();
                List<string> disAllowedSites = accessRules.Where(x => x.ContentType == "site" && string.IsNullOrEmpty(x.Permissions)).Select(x => x.ContentId).ToList();
                foreach (string s in allowedSites)
                {
                    //Bags.GetZoneGroupsBySite(7, null)[0].Items[0].Value

                    List<string> allowedSiteZoneGroupss = Bags.GetZoneGroupsBySiteForAuth(Convert.ToInt32(s), null).ToList();
                    foreach (string zg in allowedSiteZoneGroupss)
                    {
                        List<string> allowedZgZones = Bags.GetZonesByZoneGroup(0, Convert.ToInt32(zg)).Select(x => x.Value).ToList();
                        allowedZones.AddRange(allowedZgZones);  //izin verilenler listesine zoneları ekle
                        allowedArticleIds.AddRange(dbContext.vArticlesZonesFulls.Where(x => allowedZgZones.Contains(x.ZoneID.ToString())).Select(x => x.ArticleID.ToString()).ToList());    //izin verilen article listesine ekle
                    }
                }
                foreach (string s in disAllowedSites)
                {
                    //Bags.GetZoneGroupsBySite(7, null)[0].Items[0].Value
                    List<string> disAllowedSiteZoneGroupss = Bags.GetZoneGroupsBySite(Convert.ToInt32(s), null).Select(x => x.Items.FirstOrDefault().Value).ToList();
                    foreach (string zg in disAllowedSiteZoneGroupss)
                    {
                        List<string> disAllowedZgZones = Bags.GetZonesByZoneGroup(0, Convert.ToInt32(zg)).Select(x => x.Value).ToList();
                        disAllowedZones.AddRange(disAllowedZgZones);  //izin verilenler listesine zoneları ekle
                        disAllowedArticleIds.AddRange(dbContext.vArticlesZonesFulls.Where(x => disAllowedZgZones.Contains(x.ZoneGroupID.ToString())).Select(x => x.ArticleID.ToString()).ToList());    //izin verilen article listesine ekle
                    }
                }

                listAllZones = listAllZones.Where(x => allowedZones.Contains(x.Id.ToString())).ToList();
                #endregion
            }

            ViewBag.ZoneList = listAllZones;
            // Zone List End

            if (Status0 == null && Status1 == null && Status2 == null)
            {
                //eğer active-passive-deleted hiç bişey seçilmemişse active + passive gelsin
                Status0 = true;
                Status1 = true;
            }

            ViewBag.Classifications = dbContext.Classifications.ToList();
            ViewBag.Users = dbContext.vAspNetMembershipUsers.ToList();
            ViewBag.Tags = dbContext.Tags.Where(x => x.IsActive).ToList();

            ViewBag.HeadlineSortParam = SortOrder == 2 ? 3 : 2;
            ViewBag.DisplayedSortParam = SortOrder == 4 ? 5 : 4;
            ViewBag.ClicksSortParam = SortOrder == 6 ? 7 : 6;
            ViewBag.SortOrder = SortOrder;
            ViewBag.ClassificationID = ClsfId;
            ViewBag.UserId = UserId;
            ViewBag.FileTypes = Bags.GetFileTypes();

            ViewBag.Keyword = Keyword;
            ViewData["ZoneGroupID"] = ZoneGroupId ?? 0;
            ViewBag.ZoneID = ZoneId;
            ViewBag.IsRevision = IsRevision;
            ViewBag.Status0 = Status0;
            ViewBag.Status1 = Status1;
            ViewBag.Status2 = Status2;
            ViewBag.RevFlag1 = RevFlag1;
            ViewBag.RevFlag2 = RevFlag2;
            ViewBag.RevFlag3 = RevFlag3;
            ViewBag.RevFlag4 = RevFlag4;
            ViewBag.RevFlag5 = RevFlag5;
            ViewBag.Language = Language;
            ViewBag.ArticleIDs = ArticleIDs;
            ViewBag.Alias = Alias;
            if (Alias != null && Alias.StartsWith("/"))
            {
                //eğer başında / varsa sil
                Alias = Alias.Substring(1);
            }

            ViewBag.DisplayedStart = DisplayedStart;
            ViewBag.DisplayedEnd = DisplayedEnd;
            ViewBag.ModifiedStart = ModifiedStart;
            ViewBag.ModifiedEnd = ModifiedEnd;
            ViewBag.ApprovedStart = ApprovedStart;
            ViewBag.ApprovedEnd = ApprovedEnd;

            var result = context.SearchArticles(Keyword, 0, 0, IsRevision ?? false, ArticleIDs, Alias, Language, ZoneGroupId, ZoneId, Status, Status0, Status1, Status2, DisplayedStart, DisplayedEnd, ModifiedStart, ModifiedEnd, ApprovedStart, ApprovedEnd, RevFlag1, RevFlag2, RevFlag3, RevFlag4, RevFlag5, ClsfId, SortOrder, SiteId, UserId, TagID, allowedArticleIds, disAllowedArticleIds, fileTypes);
            //var result = context.SearchArticles(Keyword, 0, 0, IsRevision ?? true, "", "", "", 0, 0, 0, null, null, null, null, null, null, false, false, false, false, false, SortOrder);
            var articleResults = result.GroupBy(x => x.article_id).Select(x => x.First()).ToList();

            int pageSize = 25;
            int pageNumber = (Page ?? 1);

            return View(articleResults.ToPagedList(pageNumber, pageSize));
        }

        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,UserCreator", Permission = "Create", ContentType = "Article")]
        public ActionResult QuickCreate(int ParentId, string ItemType)
        {
            TempData.Clear();

            ViewBag.Languages = Bags.GetLanguages();

            if (!string.IsNullOrEmpty(ItemType) && ItemType.ToLower() == "article")
            {
                // select parent article
                var lastRevision = context.SelectArticleLastRevision(ParentId).FirstOrDefault();
                var article = context.SelectArticleRevisionDetails(lastRevision).FirstOrDefault();

                // select parent article zone
                var az = context.SelectArticleZonesRevisionDetails(lastRevision).FirstOrDefault();

                // select parent article zone information
                var zoneLastRevision = zContext.SelectZoneLastRevision(az.out_id).FirstOrDefault(); // this is parent article zone last revision ID
                var zone = zContext.SelectZonesRevisionDetails(zoneLastRevision); // this is last revision of parent article zone

                // create new sub zone and approve

                string subZoneName = CheckSubZoneName(zone.zone_group_id, article.headline, 1);
                var createSubZone = zContext.SaveZoneRevision(new cms_zone_revision { zone_group_id = zone.zone_group_id, zone_name = subZoneName, zone_type_id = 0, rev_name = "Structure Automaticly Craeted", zone_status = "A", lang_id = article.lang_id, zone_desc = "This is SubZone" }).FirstOrDefault();
                zContext.ApproveZoneRevision(Convert.ToInt64(createSubZone.rev_id), Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]), Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]), Session["CMS_ENABLE_CHECK_OUT"].ToString());

                // update parent article with navigation_zone_id and approve
                cms_article_revision revision = IntoRevision(article);
                revision.navigation_zone_id = (int)createSubZone.zone_id;
                revision.navigation_display = 3;
                var updateResult = context.SaveArticleRevision(revision).FirstOrDefault();

                // update parent article zones
                if (updateResult.rstat == "U")
                    context.DeleteArticleZonesWithRevision(updateResult.rev_id ?? 0, updateResult.article_id ?? 0);
                context.InsertArticleZonesWithRevision(updateResult.rev_id ?? 0, az.out_id, updateResult.article_id ?? 0, az.az_order, az.az_alias);

                // approve parent article zone
                var approveResult = context.ApproveArticleRevision(updateResult.rev_id ?? 0, Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]), Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]), Session["CMS_ENABLE_CHECK_OUT"].ToString());

                ViewBag.Warning = "Automaticly Sub Zone created for this operation. Sub Zone ID:" + createSubZone.zone_id;
                ViewBag.zone_id = createSubZone.zone_id;
                ViewBag.ForceCreateSubZone = "true";
            }
            else
            {
                ViewBag.zone_id = ParentId;
                ViewBag.ForceCreateSubZone = "false";
            }

            return View();
        }

        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,UserCreator", Permission = "Edit", ContentType = "Article")]
        public ActionResult QuickEdit(int id, int ParentId)
        {
            TempData.Clear();

            ViewBag.Languages = Bags.GetLanguages();

            var LastRevisionId = context.SelectArticleLastRevision(id).FirstOrDefault();

            var zone = context.SelectArticleZonesRevisionDetails(LastRevisionId).FirstOrDefault();

            ViewBag.zone_id = ParentId;
            ViewBag.az_order = zone.az_order;
            ViewBag.az_alias = zone.az_alias;

            var article = context.SelectArticleRevisionDetails(LastRevisionId).FirstOrDefault();

            return View(article);
        }

        [CmsAuthorize(Permission = "Create", ContentType = "Article")]
        public ActionResult Create(int? ClsfId)
        {
            //ViewBag.Languages = Bags.GetLanguages();
            //ViewBag.Clasifications = Bags.GetClasifications(null);

            //int ClsfId = ClassificationId ?? 0;
            //ViewBag.ClassificationID = ClassificationId ?? 0;

            //if (ClsfId > 0)
            //    ViewData["classification_details"] = cContext.SelectClassificationDetails(ClsfId).FirstOrDefault();

            return RedirectToAction("Edit", new { id = -1, ClsfId = ClsfId });
        }

        public ActionResult Clsf(string id)
        {
            return RedirectToAction("Edit", new { id = -1, ClsfId = id });
        }

        public string LanguageRelations(int ZoneID, string articleId, string revisionId)
        {
            string ReturnHtml = string.Empty;

            //int ArticleID = Convert.ToInt32(TempData["ArticleID"]);
            //int RevisionID = Convert.ToInt32(TempData["RevisionID"]);


            int ArticleID = Convert.ToInt32(articleId);
            int RevisionID = Convert.ToInt32(revisionId);

            CmsDbContext dbContext = new CmsDbContext();

            List<EuroCMS.Admin.entity.cms_asp_select_article_language_relations_by_revision_Result> Lr = context.SelectArticleLanguageRelationsRevisionDetails(RevisionID, ZoneID, ArticleID).ToList();
            foreach (cms_asp_select_article_language_relations_by_revision_Result item in Lr)
            {
                ReturnHtml += "<option selected=\"selected\" value=\"" + item.zone_id + "-" + item.article_id + "\">" + item.out_name + "</option>";
            }

            return ReturnHtml;
        }
        [CmsAuthorize(Permission = "Create,Edit", ContentType = "Article")]
        public ActionResult Edit(int id, long? RevisionId, int? ClsfId, bool? isDuplicate)
        {
            // Zone List Start

            CmsDbContext dbContext = new CmsDbContext();

            List<Zone> listAllZones = new List<Zone>();
            List<int> listSelectedZones = new List<int>();

            listAllZones = dbContext.Zones.Where(z => z.Status == "A").ToList();
            listSelectedZones = dbContext.vArticlesZonesFulls.Where(v => v.ZoneStatus == "A" && v.ArticleID == id).Select(s => s.ZoneID).ToList();

            ViewBag.ZoneList = listAllZones;
            ViewBag.SelectedZoneList = listSelectedZones;

            // Zone List End

            if (isDuplicate.HasValue)
            {

                ViewBag.Duplicate = isDuplicate.Value;
            }

            // Article Detail Urls Start
            List<string> listUrl = new List<string>();
            foreach (int zoneId in listSelectedZones)
            {
                string url = "";
                url = CmsHelper.GetArticleAliasOrURL(id, zoneId.ToString());
                var zone = listAllZones.FirstOrDefault(f => f.Id == zoneId);
                if (zone != null)
                {
                    var zoneGroup = dbContext.ZoneGroups.FirstOrDefault(f => f.Id == zone.ZoneGroupId);
                    if (zoneGroup != null)
                    {
                        var sites = dbContext.Sites.FirstOrDefault(f => f.Id == zoneGroup.SiteId);
                        if (sites != null)
                        {
                            var domain = dbContext.Domains.FirstOrDefault(f => f.Id == sites.DomainId);
                            if (domain != null)
                            {
                                var domainNamesUrls = domain.Names.Split('\r').FirstOrDefault();
                                if (!string.IsNullOrEmpty(url.Trim()))
                                {
                                    listUrl.Add(domainNamesUrls.Trim() + "/" + url.Trim());
                                }
                            }
                        }
                    }
                }
            }

            ViewBag.Urls = listUrl;
            // Article Detail Urls End


            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.Clasifications = Bags.GetClasifications(null);

            var SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : -1;
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, null);

            ArticleRepository aRep = new ArticleRepository();
            ArticleService aService = new ArticleService(aRep);

            int _ClsfId = 0;

            if (id != -1 && !ClsfId.HasValue)
            {
                _ClsfId = Convert.ToInt32(aService.Find(id).ClassificationId);
            }
            else if (id == -1 && ClsfId.HasValue || id != -1 && ClsfId.HasValue)
            {
                _ClsfId = ClsfId.Value;
            }
            else
            {
                _ClsfId = 0;
            }

            //if (id != -1)
            //    _ClsfId = ClsfId ?? Convert.ToInt32(aService.Find(id).ClassificationId);

            //if (_ClsfId > 0)
            //    ViewData["classification_details"] = cContext.SelectClassificationDetails(_ClsfId).FirstOrDefault();

            ViewData["classification_details"] = cContext.SelectClassificationDetails(_ClsfId).FirstOrDefault();
            ViewData["article_revisions"] = context.SelectArticleRevisions(id);
            ViewData["article_zones_revision"] = new List<EuroCMS.Admin.entity.cms_asp_select_article_zones_by_revision_Result>();
            ViewData["article_related_revision"] = new List<EuroCMS.Admin.entity.cms_asp_select_article_relateds_by_revision_Result>();

            for (int i = 1; i <= 20; i++)
            {
                ViewData["combo_values_" + i] = cContext.SelectClassificationComboValues(_ClsfId, i);
            }
            var tagList = dbContext.Tags.ToList();
          
            if (id > 0)
            {
                if (RevisionId != null && RevisionId > 0)
                {
                    ViewBag.CurrentRevisionID = RevisionId;

                    var article = context.SelectArticleRevisionDetails(RevisionId ?? 0).FirstOrDefault();

                    if (article.article_type != 0 && article.article_type_detail != "" && article.article_type == 2 || article.article_type == 9)
                        article.article_type_detail_text = Bags.GetArticleHeadline(article.article_type_detail).ToString();
                    else if (article.article_type != 0 && article.article_type == 3 || article.article_type == 4 || article.article_type == 7 || article.article_type == 8)
                        article.article_type_detail_text = Bags.GetArticleZoneNamesWithoutArticle(article.article_type_detail).ToString();
                    else
                        article.article_type_detail_text = article.article_type_detail;


                    //if (_ClsfId > 0)
                    //    article.clsf_id = _ClsfId;

                    if (ClsfId.HasValue)
                        article.clsf_id = ClsfId.Value;


                    ViewData["classification_details"] = cContext.SelectClassificationDetails(article.clsf_id).FirstOrDefault();
                    ViewData["article_revision_detail"] = context.SelectArticleRevisionDetails2(RevisionId ?? 0).FirstOrDefault();
                    ViewData["article_zones_revision"] = context.SelectArticleZonesRevisionDetails(RevisionId ?? 0);
                    ViewData["article_related_revision"] = context.SelectArticleRelationsRevisionDetails(RevisionId ?? 0);

                    ViewBag.SubZone = Bags.GetArticleZoneNamesWithoutArticle(article.navigation_zone_id + "-" + article.article_id);
                    TempData["ArticleID"] = id;
                    TempData["RevisionID"] = RevisionId;
                    if (_ClsfId < 0)
                    {
                        article.clsf_id = 0;
                    }
                    var tagIdsList = article.tag_ids.Split(',').ToList();

                    article.tag_contents = string.Join(",", tagList.Where(w => tagIdsList.Contains(w.ID.ToString())).Select(s => s.Text).ToList());

                    return View(article);
                }
                else
                {
                    var LastRevisionId = context.SelectArticleLastRevision(id).FirstOrDefault();
                    TempData["ArticleID"] = id;
                    TempData["RevisionID"] = LastRevisionId;
                    ViewBag.CurrentRevisionID = LastRevisionId;

                    var article = context.SelectArticleRevisionDetails(LastRevisionId).FirstOrDefault();

                    if (article.article_type != 0 && article.article_type_detail != "" && article.article_type == 2 || article.article_type == 9)
                        article.article_type_detail_text = Bags.GetArticleHeadline(article.article_type_detail).ToString();
                    else if (article.article_type != 0 && article.article_type == 3 || article.article_type == 4 || article.article_type == 7 || article.article_type == 8)
                        article.article_type_detail_text = Bags.GetArticleZoneNamesWithoutArticle(article.article_type_detail).ToString();
                    else
                        article.article_type_detail_text = article.article_type_detail;


                    //if (_ClsfId > 0)
                    //    article.clsf_id = _ClsfId;
                    if (ClsfId.HasValue)
                        article.clsf_id = ClsfId.Value;

                    ViewData["classification_details"] = cContext.SelectClassificationDetails(article.clsf_id).FirstOrDefault();
                    ViewData["article_revision_detail"] = context.SelectArticleRevisionDetails2(LastRevisionId).FirstOrDefault();
                    ViewData["article_zones_revision"] = context.SelectArticleZonesRevisionDetails(LastRevisionId);
                    ViewData["article_related_revision"] = context.SelectArticleRelationsRevisionDetails(LastRevisionId);

                    ViewBag.SubZone = Bags.GetArticleZoneNamesWithoutArticle(article.navigation_zone_id + "-" + article.article_id);
                    var tagIdsList = article.tag_ids.Split(',').ToList();

                    article.tag_contents = string.Join(",", tagList.Where(w => tagIdsList.Contains(w.ID.ToString())).Select(s => s.Text).ToList());
                    return View(article);
                }
            }

            cms_article_revision empty = new cms_article_revision();
            empty.clsf_id = (ClsfId.HasValue ? ClsfId.Value : _ClsfId);
            empty.article_id = (id == -1 ? -1 : id);

            return View(empty);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "ContentManager,UserCreator", Permission = "Edit", ContentType = "Article")]
        public ActionResult Edit(int id, long? RevisionId, int? ClsfId, string ReturnUrl, bool? ForceApprove, FormCollection collection)
        {
            TempData.Clear();

            if (id == -1)
                WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_CREATE, this));
            else
                WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_EDIT, this));

            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.Clasifications = Bags.GetClasifications(null);
            var SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : -1;
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, -1);

            ViewData["article_revisions"] = context.SelectArticleRevisions(id);
            ViewData["article_revision_detail"] = context.SelectArticleRevisionDetails2(RevisionId ?? 0).FirstOrDefault();
            ViewData["article_zones_revision"] = context.SelectArticleZonesRevisionDetails(RevisionId ?? 0);
            ViewData["article_related_revision"] = context.SelectArticleRelationsRevisionDetails(RevisionId ?? 0);
            // ViewData["article_zones_revision"] = new List<EuroCMS.Admin.entity.cms_asp_select_article_zones_by_revision_Result>();
            List<EuroCMS.Admin.entity.cms_asp_select_article_zones_by_revision_Result> zoneList = new List<EuroCMS.Admin.entity.cms_asp_select_article_zones_by_revision_Result>();

            // Zone List Start


            CmsDbContext dbContext = new CmsDbContext();

            List<Zone> listAllZones = new List<Zone>();
            List<int> listSelectedZones = new List<int>();

            listAllZones = dbContext.Zones.Where(z => z.Status == "A").ToList();
            listSelectedZones = dbContext.vArticlesZonesFulls.Where(v => v.ZoneStatus == "A" && v.ArticleID == id).Select(s => s.ZoneID).ToList();

            ViewBag.ZoneList = listAllZones;
            ViewBag.SelectedZoneList = listSelectedZones;

            // Zone List End



            // Article Delete Permission Control Start

            bool isDeleted = false, hasDeletePermission = false;

            if (RevisionId > 0)
            {
                // Edit
                ArticleRevision getLastArticleRev = new ArticleRevision();
                long lastRevId = context.SelectArticleLastRevision(id).FirstOrDefault();
                getLastArticleRev = dbContext.ArticleRevisions.Where(ar => ar.RevisionId == lastRevId).FirstOrDefault();

                if (getLastArticleRev.Status.ToString() != collection["status"].ToString() && collection["status"].ToString() == "2")
                {
                    isDeleted = true;
                }
            }


            if (isDeleted)
            {
                EuroCMS.Model.BaseDbContext baseContext = new EuroCMS.Model.BaseDbContext();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                vAspNetMembershipUser getCurrentUser = new vAspNetMembershipUser();
                getCurrentUser = dbContext.vAspNetMembershipUsers.Where(v => v.IsApproved == true && v.UserId == currentUserId).FirstOrDefault();

                List<string> listUserRole = new List<string>();
                listUserRole = System.Web.Security.Roles.GetRolesForUser(getCurrentUser.UserName).ToList();
                for (int i = 0; i < listUserRole.Count(); i++)
                {
                    if (listUserRole[i].ToLower() == "administrator")
                    {
                        hasDeletePermission = true;
                    }
                }

                if (!hasDeletePermission)
                {
                    hasDeletePermission = baseContext.HasPermission(String.Join(",", System.Web.Security.Roles.GetRolesForUser(getCurrentUser.UserName)), "delete", id.ToString(), "article").Count() > 0;
                }

            }

            if (hasDeletePermission)
            {
                WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_DELETE, this));
            }
            else if (isDeleted)
            {
                throw new ApplicationException("You don't have permission to delete this article");
            }


            // Article Delete Permission Control Start



            for (int i = 1; i <= 20; i++)
            {
                ViewData["combo_values_" + i] = cContext.SelectClassificationComboValues(ClsfId ?? 0, i);
            }

            int _ClsfId = ClsfId ?? 0;
            int ArticleID = -1;
            long RevisionID = RevisionId ?? -1;

            cms_article_revision a = new cms_article_revision();

            if (ModelState.IsValid)
            {
                try
                {
                    #region New Instance
                    string keywords = collection["keywords"] ?? "";
                    if (!string.IsNullOrEmpty(collection["keywords_ao"])
                        && collection["keywords_ao"] == "O")
                        keywords = "|" + keywords;

                    a.rev_name = collection["rev_name"] ?? "";
                    a.rev_note = collection["rev_note"] ?? "";
                    a.revision_status = collection["revision_status"] ?? "N";
                    a.article_id = id;
                    a.rev_id = !string.IsNullOrEmpty(collection["rev_id"]) ? Convert.ToInt64(collection["rev_id"]) : -1;

                    if (!string.IsNullOrEmpty(collection["clsf_id"]))
                        a.clsf_id = Convert.ToInt32(collection["clsf_id"]);

                    a.status = !string.IsNullOrEmpty(collection["status"]) ? Convert.ToByte(collection["status"]) : (byte)0;

                    DateTime startDate = DateTime.Now;
                    if (DateTime.TryParse(collection["startdate"], CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                        a.startdate = startDate;
                    else
                        a.startdate = DateTime.Now;

                    DateTime endDate;
                    if (DateTime.TryParse(collection["enddate"], CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                        a.enddate = endDate;

                    a.orderno = Convert.ToInt32(collection["orderno"]);
                    a.lang_id = collection["lang_id"];
                    a.navigation_display = Convert.ToByte(collection["navigation_display"]);
                    a.navigation_zone_id = Convert.ToInt32(collection["navigation_zone_id"]);
                    a.menu_text = collection["menu_text"] ?? "";
                    a.headline = collection["headline"] ?? "";
                    a.summary = collection["summary"] ?? "";
                    a.keywords = keywords;
                    a.article_type = Convert.ToByte(collection["article_type"]);
                    a.article_type_detail = collection["article_type_detail"] ?? "";
                    a.article_type_detail_text = collection["article_type_detail_text"] ?? "";
                    a.article_1 = collection["article_1"] ?? "";
                    a.article_2 = collection["article_2"] ?? "";
                    a.article_3 = collection["article_3"] ?? "";
                    a.article_4 = collection["article_4"] ?? "";
                    a.article_5 = collection["article_5"] ?? "";
                    a.custom_1 = collection["custom_1"] ?? "";
                    a.custom_2 = collection["custom_2"] ?? "";
                    a.custom_3 = collection["custom_3"] ?? "";
                    a.custom_4 = collection["custom_4"] ?? "";
                    a.custom_5 = collection["custom_5"] ?? "";
                    a.custom_6 = collection["custom_6"] ?? "";
                    a.custom_7 = collection["custom_7"] ?? "";
                    a.custom_8 = collection["custom_8"] ?? "";
                    a.custom_9 = collection["custom_9"] ?? "";
                    a.custom_10 = collection["custom_10"] ?? "";
                    a.custom_11 = collection["custom_11"] ?? "";
                    a.custom_12 = collection["custom_12"] ?? "";
                    a.custom_13 = collection["custom_13"] ?? "";
                    a.custom_14 = collection["custom_14"] ?? "";
                    a.custom_15 = collection["custom_15"] ?? "";
                    a.custom_16 = collection["custom_16"] ?? "";
                    a.custom_17 = collection["custom_17"] ?? "";
                    a.custom_18 = collection["custom_18"] ?? "";
                    a.custom_19 = collection["custom_19"] ?? "";
                    a.custom_20 = collection["custom_20"] ?? "";
                    a.flag_1 = Convert.ToBoolean(collection["flag_1"]);
                    a.flag_2 = Convert.ToBoolean(collection["flag_2"]);
                    a.flag_3 = Convert.ToBoolean(collection["flag_3"]);
                    a.flag_4 = Convert.ToBoolean(collection["flag_4"]);
                    a.flag_5 = Convert.ToBoolean(collection["flag_5"]);
                    a.before_body = HttpUtility.HtmlEncode(collection["before_body"] ?? string.Empty);
                    a.before_head = HttpUtility.HtmlEncode(collection["before_head"] ?? string.Empty);
                    a.no_index_no_follow = (collection["no_index_no_follow"] == "1") ? true : false;
                    a.canonical_url = HttpUtility.HtmlEncode(collection["canonical_url"] ?? string.Empty);
                    a.meta_title = HttpUtility.HtmlEncode(collection["meta_title"] ?? string.Empty);
                    a.custom_html_attr = HttpUtility.HtmlEncode(collection["custom_html_attr"] ?? string.Empty);

                    //2017-09-18
                    a.afterbody = HttpUtility.HtmlEncode(collection["afterbody"]);
                    a.hideprefix = (collection["hideprefix"] == "1") ? true : false;
                    a.hidesuffix = (collection["hidesuffix"] == "1") ? true : false;
                    //2017-09-18

                    #region Tags
                    string tags = collection["tags"];
                    List<Tag> AllTags = dbContext.Tags.ToList();
                    if (!string.IsNullOrEmpty(tags))
                    {
                        List<string> tagList = tags.Split(',').ToList();

                        List<int> tagIds = new List<int>();
                        Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

                        foreach (string s in tagList)
                        {
                            if (AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault() != null)
                            {
                                Tag tCurrent = AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault();
                                tagIds.Add(tCurrent.ID);
                                if (string.IsNullOrEmpty(tCurrent.Alias))
                                {
                                    int counter = 2;
                                    tCurrent.Alias = CmsHelper.StringToAlphaNumeric(tCurrent.Text, false);
                                    //check if alias exists
                                    while (dbContext.Tags.Where(x => x.Alias == tCurrent.Alias).ToList().Count > 0)
                                    {
                                        tCurrent.Alias += "-" + counter;
                                        counter++;
                                    }
                                }

                                dbContext.SaveChanges();
                            }
                            else
                            {
                                Tag newTag = new Tag { AddedDate = DateTime.Now, IsActive = true, Counter = 0, PublisherID = currentUserId, SiteID = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : SiteId, Text = s, Alias = CmsHelper.StringToAlphaNumeric(s, false) };
                                if (newTag.SiteID <= 0)
                                {
                                    if (a.article_id > 0)
                                    {
                                        newTag.SiteID = dbContext.vArticlesZonesFulls.Where(x => x.ArticleID == a.article_id).FirstOrDefault().ZoneGroupSiteId.Value;
                                    }

                                    if (newTag.SiteID <= 0)
                                    {
                                        try
                                        {
                                            string zList = collection["zones[]"] ?? "";

                                            string[] zArray = zList.Split(',');
                                            newTag.SiteID = dbContext.vArticlesZonesFulls.Where(x => x.ZoneID == Convert.ToInt32(zArray[0])).FirstOrDefault().ZoneGroupSiteId.Value;
                                        }
                                        catch (Exception ex)
                                        {
                                            newTag.SiteID = -1;
                                            CmsHelper.SaveErrorLog(ex, "Site Id not found", true);
                                        }
                                    }
                                }
                                dbContext.Tags.Add(newTag);
                                if (dbContext.SaveChanges() > 0)
                                {
                                    tagIds.Add(newTag.ID);
                                }
                            }
                        }
                        a.tag_ids = string.Join(",", tagIds.ToArray());
                        a.tag_contents = tags;
                    }
                    #endregion

                    DateTime date_1;
                    if (DateTime.TryParse(collection["date_1"], CultureInfo.InvariantCulture, DateTimeStyles.None, out date_1))
                        a.date_1 = date_1;

                    DateTime date_2;
                    if (DateTime.TryParse(collection["date_2"], CultureInfo.InvariantCulture, DateTimeStyles.None, out date_2))
                        a.date_2 = date_2;

                    DateTime date_3;
                    if (DateTime.TryParse(collection["date_3"], CultureInfo.InvariantCulture, DateTimeStyles.None, out date_3))
                        a.date_3 = date_3;

                    DateTime date_4;
                    if (DateTime.TryParse(collection["date_4"], CultureInfo.InvariantCulture, DateTimeStyles.None, out date_4))
                        a.date_4 = date_4;

                    DateTime date_5;
                    if (DateTime.TryParse(collection["date_5"], CultureInfo.InvariantCulture, DateTimeStyles.None, out date_5))
                        a.date_5 = date_5;

                    a.rev_flag_1 = Convert.ToBoolean(collection["rev_flag_1"]);
                    a.rev_flag_2 = Convert.ToBoolean(collection["rev_flag_2"]);
                    a.rev_flag_3 = Convert.ToBoolean(collection["rev_flag_3"]);
                    a.rev_flag_4 = Convert.ToBoolean(collection["rev_flag_4"]);
                    a.rev_flag_5 = Convert.ToBoolean(collection["rev_flag_5"]);
                    a.cl_1 = Convert.ToByte(collection["cl_1"]);
                    a.cl_2 = Convert.ToByte(collection["cl_2"]);
                    a.cl_3 = Convert.ToByte(collection["cl_3"]);
                    a.cl_4 = Convert.ToByte(collection["cl_4"]);
                    a.cl_5 = Convert.ToByte(collection["cl_5"]);
                    a.custom_body = collection["custom_body"] ?? "";
                    a.revised_by = Membership.GetUser().ProviderUserKey;
                    a.cio = Session["CMS_ENABLE_CHECK_OUT"] != null ? Session["CMS_ENABLE_CHECK_OUT"].ToString() : "";
                    a.meta_description = collection["meta_description"] ?? "";
                    a.content_1_editor_type = collection["content_1_editor_type"] ?? "H";
                    a.content_2_editor_type = collection["content_2_editor_type"] ?? "H";
                    a.content_3_editor_type = collection["content_3_editor_type"] ?? "H";
                    a.content_4_editor_type = collection["content_4_editor_type"] ?? "H";
                    a.content_5_editor_type = collection["content_5_editor_type"] ?? "H";
                    a.omniture_code = collection["omniture_code"] ?? "";
                    a.custom_setting = string.IsNullOrEmpty(collection["permanent_redirection"]) ? ";" : collection["permanent_redirection"] + ";";
                    a.created = DateTime.Now;

                    TempData["ArticleID"] = a.article_id;
                    TempData["RevisionID"] = a.rev_id;

                    string az_list = collection["zones[]"] ?? "";
                    string ar_list = collection["relateds[]"] ?? "";

                    string[] zones = az_list.Split(',');
                    #endregion
                    string az_cache = string.Empty;
                    string az_order = string.Empty;
                    string az_alias = string.Empty;
                    string lang_relations = string.Empty;
                    string az_name = string.Empty;
                    bool is_protected = false;
                    bool is_page = true;
                    int zi = 0;

                    foreach (string az in zones)
                    {
                        az_cache = collection["cache_" + az] ?? "";
                        az_order = collection["order_" + az] ?? "0";
                        az_alias = collection["alias_" + az] ?? "";
                        az_name = collection["az_name_" + az] ?? "";
                        #region is protected
                        if (!string.IsNullOrEmpty(collection["is_protected_" + az]))
                        {
                            if (collection["is_protected_" + az] == "on")
                            {
                                is_protected = true;
                            }
                            else
                            {
                                is_protected = false;
                            }
                        }
                        else
                        {
                            is_protected = false;
                        }
                        #endregion
                        #region is_page
                        if (!string.IsNullOrEmpty(collection["is_page_" + az]))
                        {
                            if (collection["is_page_" + az] == "on" || id == -1)
                            {
                                is_page = true;
                            }
                            else
                            {
                                is_page = false;
                            }
                        }
                        else
                        {
                            if (id == -1)
                            {
                                is_page = true;
                            }
                            else
                            {
                                is_page = false;
                            }
                        }
                        #endregion

                        lang_relations = collection["relations_" + az] ?? "";
                        if (string.IsNullOrEmpty(az_alias.Trim()))
                        {
                            az_alias = CmsHelper.CreateAliasWithUrlStructure(id, Convert.ToInt32(az));
                        }
                        zoneList.Insert(zi, new cms_asp_select_article_zones_by_revision_Result { az_alias = az_alias, az_order = Convert.ToInt32(az_order), out_id = Convert.ToInt32(az), out_name = az_name, zone_type_id = 0, is_alias_protected = is_protected, is_page = is_page });

                        if (string.IsNullOrEmpty(az_order))
                            az_order = "0";

                        zi++;


                    }



                    ViewData["article_zones_revision"] = zoneList;
                    ViewBag.SubZone = Bags.GetArticleZoneNamesWithoutArticle(a.navigation_zone_id + "-0");

                    #region Check All Aliases
                    foreach (var z in zoneList)
                    {
                        string final_az_alias = string.Empty;

                        if (z.az_alias.StartsWith("/"))
                            z.az_alias = z.az_alias.Substring(1);

                        foreach (string alias in z.az_alias.Split('/'))
                            final_az_alias = final_az_alias + z.az_alias.GetValidAlias() + "/";

                        if (final_az_alias.EndsWith("/"))
                            final_az_alias = final_az_alias.Substring(0, final_az_alias.Length - 1);


                        bool azCheck = CoreHelper.CheckAlias(z.out_id, a.article_id, final_az_alias);

                        if (!azCheck && !string.IsNullOrEmpty(z.az_alias.Trim()))
                            throw new Exception("\"" + final_az_alias + "\" alias not avaiable. please select another one.");

                    }
                    #endregion

                    #region Validate
                    if (String.IsNullOrEmpty(collection["zones[]"]))
                        throw new ApplicationException("Zone required!");

                    if (String.IsNullOrEmpty(a.headline))
                        throw new ApplicationException("Headline required!");

                    if (String.IsNullOrEmpty(a.lang_id))
                        throw new ApplicationException("Language required!");

                    if (!String.IsNullOrEmpty(a.menu_text) && a.menu_text.Length > 100)
                        throw new ApplicationException("Menu Text must be less than 100 characters!");

                    #endregion

                    var clsfDetails = cContext.SelectClassificationDetails(a.clsf_id).FirstOrDefault();
                    ViewData["classification_details"] = clsfDetails;

                    ValidateClassification(clsfDetails, a);

                    #region Save
                    var result = context.SaveArticleRevision(a).FirstOrDefault();
                    string aStat = result.astat ?? "";
                    string rStat = result.rstat ?? "";
                    ArticleID = result.article_id ?? 0;
                    RevisionID = result.rev_id ?? 0;
                    string locked_by = result.locked_by ?? "";
                    var locked = result.locked;

                    if (!string.IsNullOrEmpty(ReturnUrl) && ReturnUrl.Contains("##ArticleID##"))
                    {
                        ReturnUrl = ReturnUrl.Replace("##ArticleID##", ArticleID.ToString());
                    }

                    switch (aStat)
                    {
                        case "D":
                            throw new ApplicationException("This language is not found. Please choose another one.");
                        case "C":
                            TempData["Message"] = "Your Article has been successfully created. Please <a href=\"" + Url.Action("ApproveRevision", new { id = RevisionID }) + "\">click</a> to Approve this revision.";
                            break;
                    }

                    switch (rStat)
                    {
                        case "L":
                            aStat = "D";
                            if (!string.IsNullOrEmpty(locked_by))
                                throw new ApplicationException("This article was locked by " + locked_by + " at " + locked + " and you didn't save.<br /><br />If you want to save your changes, please contact with " + locked_by + " or Administrator to release lock");
                            else
                                throw new ApplicationException("You can not save your changes. Administrator or someone else release your lock. Please open an edit window at another window and click save button (at this window) again");
                    }

                    if (aStat != "D")
                    {
                        switch (rStat)
                        {
                            case "N":
                                TempData["Message"] = "Your Article has been successfully created. ";
                                break;
                            case "U":
                                TempData["Message"] = "Your Article Revision has been successfully updated.";
                                break;
                            case "C":
                                TempData["Message"] = "Your Article Revision has been successfully created.";
                                break;
                        }

                        if (rStat == "U")
                        {
                            context.DeleteArticleZonesWithRevision(RevisionID, ArticleID);
                            context.DeleteArticleRelationsWithRevision(RevisionID, ArticleID);
                        }

                        context.DeleteArticleCache(ArticleID);


                        foreach (string az in zones)
                        {
                            context.DeleteArticleLanguageRelationsWithRevision(Convert.ToInt32(az), ArticleID);



                            az_cache = collection["cache_" + az] ?? "";
                            az_order = collection["order_" + az] ?? "0";
                            az_alias = collection["alias_" + az] ?? "";

                            if (!string.IsNullOrEmpty(az_alias))
                            {
                                #region is protected
                                if (!string.IsNullOrEmpty(collection["is_protected_" + az]))
                                {
                                    if (collection["is_protected_" + az] == "on")
                                    {
                                        is_protected = true;
                                    }
                                    else
                                    {
                                        is_protected = false;
                                    }
                                }
                                else
                                {
                                    is_protected = false;
                                }
                                #endregion
                            }
                            else
                            {
                                is_protected = false;
                            }

                            #region is_page
                            if (!string.IsNullOrEmpty(collection["is_page_" + az]))
                            {
                                if (collection["is_page_" + az] == "on" || id == -1)
                                {
                                    is_page = true;
                                }
                                else
                                {
                                    is_page = false;
                                }
                            }
                            else
                            {
                                if (id == -1)
                                {
                                    is_page = true;
                                }
                                else
                                {
                                    is_page = false;
                                }
                            }
                            #endregion

                            if (string.IsNullOrEmpty(az_alias.Trim()))
                            {
                                //alias elle girilmediyse buraya gelir
                                if (id != -1)
                                {
                                    az_alias = CmsHelper.CreateAliasWithUrlStructure(id, Convert.ToInt32(az));
                                }
                                else
                                {
                                    az_alias = CmsHelper.CreateAliasWithUrlStructure(ArticleID, Convert.ToInt32(az));
                                }
                            }
                            else
                            {
                                //alias elle girildiyse buraya gelir
                                if (!is_protected)
                                {
                                    //alias çakışma kontrol et
                                    #region Çakışma Kontrol
                                    List<ArticleZone> aZones = dbContext.ArticleZones.Where(x => x.AzAlias == az_alias).ToList();
                                    aZones.Remove(aZones.Where(x => x.ArticleID == id && x.ZoneID == Convert.ToInt32(az)).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                                    if (aZones == null || aZones.Count == 0)
                                    {
                                        //ok  
                                    }
                                    else
                                    {
                                        //çakışma var 
                                        int counter = 2;
                                        while (dbContext.ArticleZones.Where(x => x.AzAlias == az_alias + "-" + counter).ToList().Count > 0)
                                        {
                                            counter++;
                                        }
                                        //son - cleanText + "-" + counter
                                        az_alias = az_alias + "-" + counter;
                                    }
                                    #endregion
                                }
                            }

                            if (!is_protected)
                            {
                                az_alias = CmsHelper.StringToAlphaNumeric(az_alias, true);
                            }

                            if (string.IsNullOrEmpty(az_order))
                                az_order = "0";

                            lang_relations = collection["relations_" + az + "[]"] ?? "";



                            string final_az_alias = string.Empty;

                            if (az_alias.StartsWith("/"))
                                az_alias = az_alias.Substring(1);

                            foreach (string alias in az_alias.Split('/'))
                                final_az_alias = final_az_alias + alias + "/";

                            if (final_az_alias.EndsWith("/"))
                                final_az_alias = final_az_alias.Substring(0, final_az_alias.Length - 1);

                            if (!string.IsNullOrEmpty(az_cache) && bool.Parse(az_cache))
                                context.InsertArticleCache(Convert.ToInt32(az), ArticleID);

                            final_az_alias = CoreHelper.CheckRedirectionAlias(Convert.ToInt32(az), ArticleID, final_az_alias, 0);

                            context.InsertArticleZonesWithRevision(RevisionID, Convert.ToInt32(az), ArticleID, Convert.ToInt32(az_order), final_az_alias, is_protected, is_page);



                            foreach (string lr in lang_relations.Split(','))
                            {
                                if (lr.IndexOf("-") != -1)
                                {
                                    context.InsertArticleLanguageRelationsWithRevision(RevisionID, Convert.ToInt32(az), ArticleID, Convert.ToInt32(lr.Split('-')[0]), Convert.ToInt32(lr.Split('-')[1]), null);
                                }
                            }
                        }

                        foreach (string ar in ar_list.Split(','))
                        {
                            if (ar.IndexOf("-") != -1)
                            {
                                context.InsertArticleRelationsWithRevision(RevisionID, ArticleID, Convert.ToInt32(ar.Split('-')[0]), Convert.ToInt32(ar.Split('-')[1]));
                            }
                        }
                    }
                    #endregion

                    ForceApprove = collection["forceapprove"] != null ? Convert.ToBoolean(collection["forceapprove"]) : false;

                    if ((ForceApprove ?? false))
                        return Approve(ArticleID, RevisionID, ClsfId, ReturnUrl, null);

                    return RedirectToAction("Edit", new { id = ArticleID, RevisionId = RevisionID, ClsfId = ClsfId });
                }
                catch (Exception ex)
                {
                    //var st = new System.Diagnostics.StackTrace(ex, true);
                    //var frame = st.GetFrame(0);
                    //var line = frame.GetFileLineNumber();
                    //string errorDetail = ex.Message + " - " + ex.InnerException + " - Line: " + line.ToString() + " - FullStackTrace: " + ex.StackTrace;
                    //ModelState.AddModelError("HATA", errorDetail); 

                    CmsHelper.SaveErrorLog(ex, string.Empty, true);

                    ModelState.AddModelError("HATA", ex.Message);
                }
            }

            //if (!string.IsNullOrEmpty(ReturnUrl))
            //    return Redirect(ReturnUrl);
            //else

            return View(a);
        }

        private void ValidateClassification(cms_asp_select_classification_details_Result clsf, cms_article_revision article)
        {
            if (clsf != null)
            {
                #region Meta
                if (clsf.summary_cb && string.IsNullOrEmpty(article.summary))
                {
                    throw new ApplicationException(clsf.summary_text + " field is required!");
                }
                if (clsf.keywords_cb && string.IsNullOrEmpty(article.keywords))
                {
                    throw new ApplicationException(clsf.keywords_text + " field is required!");
                }
                if (clsf.enddate_cb && article.enddate == null)
                {
                    throw new ApplicationException(clsf.enddate_text + " field is required!");
                }
                #endregion
                #region Custom Content
                if (clsf.custom1_cb && string.IsNullOrEmpty(article.custom_1))
                {
                    throw new ApplicationException(clsf.custom1_text + " field is required!");
                }

                if (clsf.custom2_cb && string.IsNullOrEmpty(article.custom_2))
                {
                    throw new ApplicationException(clsf.custom2_text + " field is required!");
                }

                if (clsf.custom3_cb && string.IsNullOrEmpty(article.custom_3))
                {
                    throw new ApplicationException(clsf.custom3_text + " field is required!");
                }

                if (clsf.custom4_cb && string.IsNullOrEmpty(article.custom_4))
                {
                    throw new ApplicationException(clsf.custom4_text + " field is required!");
                }

                if (clsf.custom5_cb && string.IsNullOrEmpty(article.custom_5))
                {
                    throw new ApplicationException(clsf.custom5_text + " field is required!");
                }

                if (clsf.custom6_cb && string.IsNullOrEmpty(article.custom_6))
                {
                    throw new ApplicationException(clsf.custom6_text + " field is required!");
                }

                if (clsf.custom7_cb && string.IsNullOrEmpty(article.custom_7))
                {
                    throw new ApplicationException(clsf.custom7_text + " field is required!");
                }

                if (clsf.custom8_cb && string.IsNullOrEmpty(article.custom_8))
                {
                    throw new ApplicationException(clsf.custom8_text + " field is required!");
                }

                if (clsf.custom9_cb && string.IsNullOrEmpty(article.custom_9))
                {
                    throw new ApplicationException(clsf.custom9_text + " field is required!");
                }

                if (clsf.custom10_cb && string.IsNullOrEmpty(article.custom_10))
                {
                    throw new ApplicationException(clsf.custom10_text + " field is required!");
                }

                if (clsf.custom11_cb && string.IsNullOrEmpty(article.custom_11))
                {
                    throw new ApplicationException(clsf.custom11_text + " field is required!");
                }

                if (clsf.custom12_cb && string.IsNullOrEmpty(article.custom_12))
                {
                    throw new ApplicationException(clsf.custom12_text + " field is required!");
                }

                if (clsf.custom13_cb && string.IsNullOrEmpty(article.custom_13))
                {
                    throw new ApplicationException(clsf.custom13_text + " field is required!");
                }

                if (clsf.custom14_cb && string.IsNullOrEmpty(article.custom_14))
                {
                    throw new ApplicationException(clsf.custom14_text + " field is required!");
                }

                if (clsf.custom15_cb && string.IsNullOrEmpty(article.custom_15))
                {
                    throw new ApplicationException(clsf.custom15_text + " field is required!");
                }

                if (clsf.custom16_cb && string.IsNullOrEmpty(article.custom_16))
                {
                    throw new ApplicationException(clsf.custom16_text + " field is required!");
                }

                if (clsf.custom17_cb && string.IsNullOrEmpty(article.custom_17))
                {
                    throw new ApplicationException(clsf.custom17_text + " field is required!");
                }

                if (clsf.custom18_cb && string.IsNullOrEmpty(article.custom_18))
                {
                    throw new ApplicationException(clsf.custom18_text + " field is required!");
                }

                if (clsf.custom19_cb && string.IsNullOrEmpty(article.custom_19))
                {
                    throw new ApplicationException(clsf.custom19_text + " field is required!");
                }

                if (clsf.custom20_cb && string.IsNullOrEmpty(article.custom_20))
                {
                    throw new ApplicationException(clsf.custom20_text + " field is required!");
                }
                #endregion
                #region Article Content
                if (clsf.article1_cb && string.IsNullOrEmpty(article.article_1))
                {
                    throw new ApplicationException(clsf.article1_text + " field is required!");
                }

                if (clsf.article2_cb && string.IsNullOrEmpty(article.article_2))
                {
                    throw new ApplicationException(clsf.article1_text + " field is required!");
                }

                if (clsf.article3_cb && string.IsNullOrEmpty(article.article_3))
                {
                    throw new ApplicationException(clsf.article1_text + " field is required!");
                }

                if (clsf.article4_cb && string.IsNullOrEmpty(article.article_4))
                {
                    throw new ApplicationException(clsf.article1_text + " field is required!");
                }

                if (clsf.article5_cb && string.IsNullOrEmpty(article.article_5))
                {
                    throw new ApplicationException(clsf.article1_text + " field is required!");
                }
                #endregion
                #region Custom Date
                DateTime d = DateTime.Now;

                if (clsf.date1_cb && article.date_1 == null)
                {
                    throw new ApplicationException(clsf.date1_text + " field was not in Datetime format!");
                }

                if (clsf.date2_cb && article.date_2 == null)
                {
                    throw new ApplicationException(clsf.date2_text + " field was not in Datetime format!");
                }

                if (clsf.date3_cb && article.date_3 == null)
                {
                    throw new ApplicationException(clsf.date3_text + " field was not in Datetime format!");
                }

                if (clsf.date4_cb && article.date_4 == null)
                {
                    throw new ApplicationException(clsf.date4_text + " field was not in Datetime format!");
                }

                if (clsf.date5_cb && article.date_5 == null)
                {
                    throw new ApplicationException(clsf.date5_text + " field was not in Datetime format!");
                }
                #endregion
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser", Permission = "Delete", ContentType = "Article")]
        public ActionResult Delete(int id, long? RevisionId, bool? ForceApprove, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_DELETE, this));

            try
            {

                if (RevisionId == null)
                    RevisionId = context.SelectArticleLastRevision(id).FirstOrDefault();

                var result = context.DeleteArticle(id, RevisionId ?? 0, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"])).FirstOrDefault();
                if (result.rCode == "1")
                    throw new ApplicationException("Article already deleted or NOT found.");
                else
                {
                    TempData["Message"] = "Your article has been marked for deletion.";
                    if ((ForceApprove ?? false))
                        Approve(id, result.rev_id ?? 0, null, ReturnUrl, null);
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
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,UserCreator", Permission = "Approve", ContentType = "Article")]
        public ActionResult Approve(int id, long RevisionId, int? ClsfId, string ReturnUrl, bool? isDuplicate)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_APPROVE, this));

            try
            {
                if (RevisionId == null)
                {
                    throw new ApplicationException("RevisionId is null");
                }
                if (Session["CMS_APPROVE_LEVEL"] == null)
                {
                    throw new ApplicationException("Session (CMS_APPROVE_LEVEL) is null");
                }
                if (Membership.GetUser().ProviderUserKey == null)
                {
                    throw new ApplicationException("ProviderUserKey is null");
                }
                if (Session["publisher_level"] == null)
                {
                    throw new ApplicationException("Session (publisher_level) is null");
                }
                if (Session["CMS_ENABLE_CHECK_OUT"] == null)
                {
                    throw new ApplicationException("Session (CMS_ENABLE_CHECK_OUT) is null");
                }

                var result = context.ApproveArticleRevision(RevisionId, Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]), Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]), Session["CMS_ENABLE_CHECK_OUT"].ToString());

                //if (result == null)
                //{
                //    throw new ApplicationException("Result is null ------- RevisionId: " + RevisionId.ToString() + " || CMS_APPROVE_LEVEL: " + Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]).ToString() + " || PUBLISHER_LEVEL: " + Convert.ToInt32(Session["publisher_level"]).ToString() + " || CMS_ENABLE_CHECK_OUT: " + Session["CMS_ENABLE_CHECK_OUT"].ToString());
                //}
                //else
                //{
                //    if (result.Count() <= 0)
                //    {
                //        throw new ApplicationException("Result count <= 0 ------- RevisionId: " + RevisionId.ToString() + " || CMS_APPROVE_LEVEL: " + Convert.ToInt32(Session["CMS_APPROVE_LEVEL"]).ToString() + " || PUBLISHER_LEVEL: " + Convert.ToInt32(Session["publisher_level"]).ToString() + " || CMS_ENABLE_CHECK_OUT: " + Session["CMS_ENABLE_CHECK_OUT"].ToString());
                //    }
                //}

                result = result == null ? new List<cms_asp_approval_approve_article_revision_Result>() : result;
                result[0].aStat = result[0].aStat ?? "";
                result[0].rStat = result[0].rStat ?? "";
                result[0].locked_by = result[0].locked_by ?? "";
                //result[0].tStat = result[0].tStat == null ? "" : result[0].tStat;

                switch (result[0].aStat)
                {
                    case "OK":
                        TempData["Message"] = "Your article revision is approved and published.";
                        if (Session["CMS_APPROVE_FILE_WITH_ARTICLE"] != null)
                        {
                            if (Session["CMS_APPROVE_FILE_WITH_ARTICLE"].ToString() == "Y")
                            {
                            }
                        }

                        if (isDuplicate.HasValue && isDuplicate.Value)
                        {
                            long FileRevisionId = 0;
                            string filename = string.Empty;
                            int pref = 0;
                            int preflen = 0;

                            ArticleFileDbContext articleFileContext = new ArticleFileDbContext();

                            var lastFileRevision =
                                    articleFileContext.SelectArticleFileLastRevision(id)
                                    .FirstOrDefault();

                            if (lastFileRevision == null)
                                FileRevisionId = -1;
                            else
                                FileRevisionId = lastFileRevision.rev_id;

                            string aStat = articleFileContext.ApproveArticleFiles(Convert.ToInt32(FileRevisionId), Membership.GetUser().ProviderUserKey).FirstOrDefault().aStat.ToString();

                            if (aStat == "OK")
                            {
                                string tmpsfolder = Server.MapPath("/i/tmp/" + FileRevisionId);
                                string contentfolder = Server.MapPath("/i/content");

                                bool isExists = System.IO.Directory.Exists(tmpsfolder);

                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(tmpsfolder);

                                DirectoryInfo directory = new DirectoryInfo(tmpsfolder);
                                var Files = directory.GetFiles().ToList();
                                foreach (var f in Files)
                                {
                                    filename = f.Name;
                                    pref = id;
                                    preflen = pref.ToString().Length;
                                    if (filename.Substring(0, preflen) == pref.ToString())
                                    {
                                        if (System.IO.Directory.Exists(contentfolder + "\\" + filename))
                                        {
                                            DirectoryInfo directory2 = new DirectoryInfo(contentfolder + "\\" + filename);
                                            directory2.Delete();
                                        }
                                    }
                                }

                                var FileNames = "";
                                foreach (var f in Files)
                                {
                                    filename = f.Name;
                                    pref = id;
                                    preflen = pref.ToString().Length;
                                    if (filename.Substring(0, preflen) == pref.ToString())
                                    {
                                        if (System.IO.File.Exists(Path.Combine(tmpsfolder, filename)))
                                        {

                                            FileNames += f.Name + "\n";
                                            System.IO.File.Copy(Path.Combine(tmpsfolder, filename), Path.Combine(contentfolder, filename), true);
                                        }
                                    }
                                }

                                TempData["Message"] = TempData["Message"] + "Your article files revision is approved and published.\nPublished files:\n--------------------------------\n" + FileNames;
                            }
                        }

                        break;
                    case "OKA":
                        TempData["Message"] = "Your article revision is approved and ready for administrator approval.";
                        break;
                    case "DELETED":
                        TempData["Message"] = "Your article has been deleted successfully.";
                        break;
                    case "NOT_AVAILABLE":
                        throw new ApplicationException("Your article is NOT available for approval. Article not found or already approved before");
                    case "ZONE_NOT_FOUND":
                        throw new ApplicationException("Your article is NOT available for approval. Article zone not found or deleted before. ");
                    case "LOCKED":
                        if (!string.IsNullOrEmpty(result[0].locked_by))
                            throw new ApplicationException("This article was locked by " + result[0].locked_by + " at " + result[0].locked + " and you didn't save.<br /><br />If you want to save your changes, please contact with " + result[0].locked_by + " or Administrator to release lock");
                        else
                            throw new ApplicationException("You can not save your changes. Administrator or someone else release your lock. Please open an edit window at another window and click save button (at this window) again");
                    case "CANT_DELETE":
                        switch (result[0].rStat)
                        {
                            case "USED_IN_DOMAIN_HOME_PAGE":
                                throw new ApplicationException("This article is default article of these domain(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_DOMAIN_404_PAGE":
                                throw new ApplicationException("This article is 404 article of these domain(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ALIAS_REDIRECTION":
                                throw new ApplicationException("This article is used by these redirection(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_SITE_DEFAULT_ARTICLE":
                                throw new ApplicationException("This article is default article of these site(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ZONE_GROUP_DEFAULT_ARTICLE":
                                throw new ApplicationException("This article is default article of these zone group(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ZONE_DEFAULT_ARTICLE":
                                throw new ApplicationException("This article is default article of these zone(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_SITE_TAG_DETAIL_ARTICLE":
                                throw new ApplicationException("This article is tag detail article of these site(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE":
                                throw new ApplicationException("This article is tag detail article of these zone group(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_LANGUAGE_RELATION":
                                throw new ApplicationException("This article is language relationed with these article(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_INTERNAL_ARTICLE_REDIRECTION":
                                throw new ApplicationException("This article is intenal redirected articles of these article(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_MAPPED_ARTICLE":
                                throw new ApplicationException("This article is mapped articles of these article(s): " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                        }
                        break;
                    case "CANT_APPROVE":
                        switch (result[0].rStat)
                        {
                            case "LANGUAGE_RELATIONED_ARTICLE_NOT_FOUND":
                                throw new ApplicationException("Language relationed article(s) not found: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "RELATIONED_ARTICLE_NOT_FOUND":
                                throw new ApplicationException("Relationed article(s) not found: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "INTERNAL_REDIRECTED_ARTICAL_NOT_FOUND":
                                throw new ApplicationException("Internal redirected article not found. You can\'t approve this article. " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "MAPPED_ARTICALE_NOT_FOUND":
                                throw new ApplicationException("Mapped article not found. You can\'t approve this article. " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "NAVIGATION_ZONE_ID_USED":
                                throw new ApplicationException("The sub zone you choosed is using as sub zone by another article: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_DOMAIN_HOME_PAGE":
                                throw new ApplicationException("You can not approve this article because this article is default article of these domain(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_DOMAIN_404_PAGE":
                                throw new ApplicationException("You can not approve this article because this article is 404 article of these domain(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ALIAS_REDIRECTION":
                                throw new ApplicationException("ou can not approve this article because this article is used by these redirection(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_SITE_DEFAULT_ARTICLE":
                                throw new ApplicationException("You can not approve this article because this article is default article of these site(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_SITE_TAG_DETAIL_ARTICLE":
                                throw new ApplicationException("You can not approve this article is tag detail article of these site(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ZONE_GROUP_DEFAULT_ARTICLE":
                                throw new ApplicationException("You can not approve this article is default article of these zone group(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE":
                                throw new ApplicationException("You can not approve this article because this article is tag detail article of these zone group(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_ZONE_DEFAULT_ARTICLE":
                                throw new ApplicationException("You can not approve this article is default article of these zone(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_LANGUAGE_RELATION":
                                throw new ApplicationException("You can not approve this article because this article is language relationed with these article(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_INTERNAL_ARTICLE_REDIRECTION":
                                throw new ApplicationException("You can not approve this article because this article is intenal redirected articles of these article(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                            case "USED_IN_MAPPED_ARTICLE":
                                throw new ApplicationException("You can not approve this article because this article is mapped articles of these article(s) with it\'s old zone: " + result.Select(m => m.tStat.ToString()).InHtmlNewLine());
                        }
                        break;
                    default:
                        throw new ApplicationException("Error occured on article approval. Error Code:" + result[0].aStat);
                }

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;

                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                string errorDetail = ex.Message + " - " + ex.InnerException + " - Line: " + line.ToString() + " - FullStackTrace: " + ex.StackTrace;

                TempData["Message"] = errorDetail; //ex.Message + " -- " + ex.InnerException;
            }

            //if (!string.IsNullOrEmpty(ReturnUrl))
            //    return Redirect(ReturnUrl);
            //else
            //    return RedirectToAction("Edit", new { id = id, RevisionId = RevisionId, ClsfId = ClsfId });

            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("Edit", new { id = id, RevisionId = RevisionId, ClsfId = ClsfId });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,UserCreator", Permission = "Discard", ContentType = "Article")]
        public ActionResult Discard(int id, long RevisionId, int? ClsfId, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_DISCARD, this));

            try
            {
                var result = context.DiscardArticleRevision(RevisionId, Membership.GetUser().ProviderUserKey);

                switch (result[0])
                {

                    case "OK":
                        TempData["Message"] = "Revision successfully discarded. ";
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

            //if (!string.IsNullOrEmpty(ReturnUrl))
            //    return Redirect(ReturnUrl);
            //else
            //    return RedirectToAction("Edit", new { id = id, RevisionId = RevisionId, ClsfId = ClsfId });
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("Edit", new { id = id, RevisionId = RevisionId, ClsfId = ClsfId });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "View", ContentType = "Article")]
        public ActionResult Duplicate(int id)
        {
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ARTICLE_DUPLICATE, this));

            int DuplicatedArticleID = 0;
            long DuplicatedRevisionID = 0;

            var LastRevisionId = context.SelectArticleLastRevision(id).FirstOrDefault();
            var article = context.SelectArticleRevisionDetails(LastRevisionId).FirstOrDefault();

            try
            {
                cms_article_revision a = IntoRevision(article);
                a.article_id = -1;
                a.rev_id = -1;
                a.revised_by = Membership.GetUser().ProviderUserKey;
                a.navigation_zone_id = 0;
                var result = context.SaveArticleRevision(a).FirstOrDefault();

                string aStat = result.astat;
                string rStat = result.rstat;
                DuplicatedArticleID = result.article_id ?? 0;
                DuplicatedRevisionID = result.rev_id ?? 0;
                string locked_by = result.locked_by;
                DateTime? locked = result.locked;

                switch (aStat)
                {
                    case "D":
                        throw new ApplicationException("This language is not found. Please choose another one.");
                    case "C":
                        TempData["Message"] = "Your Article has been successfully duplicated";
                        break;
                }

                if (aStat != "D")
                {
                    var zones = context.SelectArticleZonesRevisionDetails(LastRevisionId);
                    foreach (var zone in zones)
                    {
                        context.InsertArticleZonesWithRevision(DuplicatedRevisionID, zone.out_id, DuplicatedArticleID, zone.az_order, string.Empty);
                    }
                }

                // related article'lar duplicate edilemez.
                // language relation'lar duplicate edilemez.
                // alias'lar duplicate edilemez.

                // ARTICLE FILE DUPLICATE ET!!!!!!!!!!!!
                #region ArticleFiles
                CmsDbContext dbContext = new CmsDbContext();
                ArticleFileDbContext articleFileContext = new ArticleFileDbContext();
                List<cms_article_files> listArticleFile = new List<cms_article_files>();
                listArticleFile = articleFileContext.ArticleFiles.Where(s => s.article_id == id).ToList();


                long NewArticleFileRevisionId = 0;
                long NewFileRevisionId = 0;
                string ArticleFileRevisionId = "-1";
                long FileRevisionId = -1;
                string old_ArticleFileRevisionId = "-1";
                string old_FileRevisionId = "-1";
                string fstat = "";

                string tmpsfolder = Server.MapPath("/i/tmp");

                for (int i = 0; i < listArticleFile.Count; i++)
                {
                    var articleFileResult = articleFileContext.UpdateArticleFileRevision(ArticleFileRevisionId, FileRevisionId.ToString(), DuplicatedArticleID.ToString(), listArticleFile[i].file_title, listArticleFile[i].file_order.ToString(), listArticleFile[i].file_name_1, listArticleFile[i].file_name_2, listArticleFile[i].file_name_3, listArticleFile[i].file_name_4, listArticleFile[i].file_name_5, listArticleFile[i].file_name_6, listArticleFile[i].file_name_7, listArticleFile[i].file_name_8, listArticleFile[i].file_name_9, listArticleFile[i].file_name_10, listArticleFile[i].file_type_id.ToString(), listArticleFile[i].file_comment.ToString(), Membership.GetUser().ProviderUserKey);

                    if (FileRevisionId == -1)
                    {
                        FileRevisionId = articleFileResult[0].rev_id ?? -1;
                    }
                    int aId = listArticleFile[i].article_id;
                    long oldRevId = dbContext.FileRevisions.Where(af => af.ArticleId == aId && af.RevisionStatus == "L").FirstOrDefault().RevisionId;
                    old_FileRevisionId = oldRevId.ToString();

                    if (articleFileResult.Count != 0)
                    {
                        NewArticleFileRevisionId = articleFileResult[0].af_rf_id ?? -1;
                        NewFileRevisionId = articleFileResult[0].rev_id ?? -1;
                        fstat = articleFileResult[0].fstat;
                        rStat = articleFileResult[0].rstat;
                    }

                    string img_prefix = DuplicatedArticleID + "_";
                    string tmpFolder = tmpsfolder + "\\" + NewFileRevisionId.ToString();
                    string tmpOldFolder = tmpsfolder + "\\" + old_FileRevisionId.ToString();

                    if (!Directory.Exists(tmpFolder))
                    {
                        Directory.CreateDirectory(tmpFolder);
                    }

                    string insertMsg = string.Empty;

                    #region Move to NewRevision Folder
                    if (Directory.Exists(tmpOldFolder) && !tmpOldFolder.Contains("-1"))
                    {
                        DirectoryInfo directory = new DirectoryInfo(tmpOldFolder);
                        var files = directory.GetFiles();

                        foreach (FileInfo item in files)
                        {
                            if (System.IO.File.Exists(Path.Combine(tmpOldFolder, item.FullName)))
                            {
                                System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name.Replace(id.ToString(), DuplicatedArticleID.ToString())), true);
                            }
                            else
                            {
                                System.IO.File.Copy(Path.Combine(tmpOldFolder, item.Name), Path.Combine(tmpFolder, item.Name.Replace(id.ToString(), DuplicatedArticleID.ToString())), true);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = "DUPLICATE ARTICLE ERROR: " + ex.Message;
            }

            return RedirectToAction("Edit", new { id = DuplicatedArticleID, RevisionID = DuplicatedRevisionID, isDuplicate = true });
        }


        [CmsAuthorize(Permission = "View", ContentType = "Article")]
        public ActionResult Preview(int id, int ZoneId)
        {
            return Redirect(string.Format("/web/{0}-{1}-1-1", ZoneId, id));
        }

        //[CmsAuthorize(Permission = "View", ContentType = "Article")]
        public ActionResult PreviewRevision(long id)
        {
            CmsDbContext dbContext = new CmsDbContext();

            //var articleId = dbContext.ArticleRevisions.FirstOrDefault(f => f.RevisionId == id).ArticleId;

            //var 

            return Redirect(string.Format("/web/-1,-1,{0}", id));
        }
        [CmsAuthorize(Permission = "View", ContentType = "Article")]
        private cms_article_revision IntoRevision(cms_article_revision revision)
        {
            cms_article_revision newRevision = new cms_article_revision();
            newRevision.article_id = revision.article_id;
            newRevision.rev_id = revision.rev_id;
            newRevision.navigation_zone_id = revision.navigation_zone_id;
            newRevision.approval_date = revision.approval_date;
            newRevision.approval_id = revision.approval_id;
            newRevision.article_1 = HttpUtility.HtmlDecode(revision.article_1);
            newRevision.article_2 = HttpUtility.HtmlDecode(revision.article_2);
            newRevision.article_3 = HttpUtility.HtmlDecode(revision.article_3);
            newRevision.article_4 = HttpUtility.HtmlDecode(revision.article_4);
            newRevision.article_5 = HttpUtility.HtmlDecode(revision.article_5);
            newRevision.article_id = revision.article_id;
            newRevision.article_type = revision.article_type;
            newRevision.article_type_detail = revision.article_type_detail;
            newRevision.cl_1 = revision.cl_1;
            newRevision.cl_2 = revision.cl_2;
            newRevision.cl_3 = revision.cl_3;
            newRevision.cl_4 = revision.cl_4;
            newRevision.cl_5 = revision.cl_5;
            newRevision.clsf_id = revision.clsf_id;
            newRevision.content_1_editor_type = revision.content_1_editor_type;
            newRevision.content_2_editor_type = revision.content_2_editor_type;
            newRevision.content_3_editor_type = revision.content_3_editor_type;
            newRevision.content_4_editor_type = revision.content_4_editor_type;
            newRevision.content_5_editor_type = revision.content_5_editor_type;
            newRevision.created = revision.created;
            newRevision.custom_1 = revision.custom_1;
            newRevision.custom_2 = revision.custom_2;
            newRevision.custom_3 = revision.custom_3;
            newRevision.custom_4 = revision.custom_4;
            newRevision.custom_5 = revision.custom_5;
            newRevision.custom_6 = revision.custom_6;
            newRevision.custom_7 = revision.custom_7;
            newRevision.custom_8 = revision.custom_8;
            newRevision.custom_9 = revision.custom_9;
            newRevision.custom_10 = revision.custom_10;
            newRevision.custom_11 = revision.custom_11;
            newRevision.custom_12 = revision.custom_12;
            newRevision.custom_13 = revision.custom_13;
            newRevision.custom_14 = revision.custom_14;
            newRevision.custom_15 = revision.custom_15;
            newRevision.custom_16 = revision.custom_16;
            newRevision.custom_17 = revision.custom_17;
            newRevision.custom_18 = revision.custom_18;
            newRevision.custom_19 = revision.custom_19;
            newRevision.custom_20 = revision.custom_20;
            newRevision.custom_body = revision.custom_body;
            newRevision.custom_setting = revision.custom_setting;
            newRevision.date_1 = revision.date_1;
            newRevision.date_2 = revision.date_2;
            newRevision.date_3 = revision.date_3;
            newRevision.date_4 = revision.date_4;
            newRevision.date_5 = revision.date_5;
            newRevision.enddate = revision.enddate;
            newRevision.flag_1 = revision.flag_1;
            newRevision.flag_2 = revision.flag_2;
            newRevision.flag_3 = revision.flag_3;
            newRevision.flag_4 = revision.flag_4;
            newRevision.flag_5 = revision.flag_5;
            newRevision.headline = revision.headline;
            newRevision.keywords = revision.keywords;
            newRevision.lang_id = revision.lang_id;
            newRevision.menu_text = revision.menu_text;
            newRevision.meta_description = revision.meta_description;
            newRevision.navigation_display = revision.navigation_display;
            newRevision.navigation_zone_id = revision.navigation_zone_id;
            newRevision.omniture_code = revision.omniture_code;
            newRevision.orderno = revision.orderno;
            newRevision.rev_date = revision.rev_date;
            newRevision.rev_flag_1 = revision.rev_flag_1;
            newRevision.rev_flag_2 = revision.rev_flag_2;
            newRevision.rev_flag_3 = revision.rev_flag_3;
            newRevision.rev_flag_4 = revision.rev_flag_4;
            newRevision.rev_flag_5 = revision.rev_flag_5;
            newRevision.rev_id = revision.rev_id;
            newRevision.rev_name = revision.rev_name;
            newRevision.rev_note = revision.rev_note;
            newRevision.revised_by = revision.revised_by;
            newRevision.revision_status = revision.revision_status;
            newRevision.startdate = revision.startdate;
            newRevision.status = revision.status;
            newRevision.summary = revision.summary;

            return newRevision;
        }

        private string CheckSubZoneName(int ZoneGroupId, string NewZoneName, int i)
        {
            var subZoneExists = zContext.SelectZonesByZoneGroup(ZoneGroupId).Where(w => w.zone_name == NewZoneName).FirstOrDefault();

            if (subZoneExists != null)
                return CheckSubZoneName(ZoneGroupId, string.Format("{0} {1}", NewZoneName, i), i++);
            else
                return NewZoneName;
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
        [CmsAuthorize(Permission = "View", ContentType = "Article")]
        public ActionResult SendToApprove(int ArticleId, long? RevisionId, int? ClsfId, Guid UserId, string SendToApproveMessage, string ReturnUrl)
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

                getArticleZoneFull = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ArticleID == ArticleId).FirstOrDefault();
                getSelectedUser = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == UserId && v.IsApproved && !v.IsLockedOut).FirstOrDefault();
                getCurrentUser = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == currentUserId && v.IsApproved && !v.IsLockedOut).FirstOrDefault();

                if (getArticleZoneFull == null)
                {
                    throw new ApplicationException("Article is not found");
                }
                if (getSelectedUser == null || getCurrentUser == null)
                {
                    throw new ApplicationException("User is not found");
                }

                getInstantMessaging = dbContext.InstantMessagings.Where(im => im.From == currentUserId && im.Type == SendToApproveType.ArticleApprove.ToString() && im.RelatedId == ArticleId && im.ReadDate == null).FirstOrDefault();

                if (getInstantMessaging != null)
                {
                    throw new ApplicationException("You can not send to approve request for this article because you already sent");
                }

                var selectedUserProfile = System.Web.Profile.ProfileBase.Create(getSelectedUser.UserName, false);
                var currentUserProfile = System.Web.Profile.ProfileBase.Create(getCurrentUser.UserName, false);

                string selectedUserFullName = selectedUserProfile.GetPropertyValue("System.FullName").ToString().Trim();
                string currentUserFullName = currentUserProfile.GetPropertyValue("System.FullName").ToString().Trim();

                string articleRevisionPreviewUrl = "http://" + HttpContext.Request.Url.Host.ToString() + "/web/-1,-1," + (RevisionId != null ? RevisionId.ToString() : ArticleId.ToString());
                string articleUrl = "";
                string editArticleUrl = "";
                string mailBody = "";

                articleUrl = getArticleZoneFull.ArticleZoneAlias;
                if (string.IsNullOrEmpty(articleUrl))
                {
                    articleUrl = CmsHelper.getContentLinkAlias(getArticleZoneFull.ZoneID.ToString(), getArticleZoneFull.ArticleID.ToString(), getArticleZoneFull.SiteName, getArticleZoneFull.ZoneGroupName, getArticleZoneFull.ZoneName, getArticleZoneFull.Headline, "");
                }
                articleUrl = "http://" + HttpContext.Request.Url.Host + (articleUrl.StartsWith("/") ? articleUrl : "/" + articleUrl);

                editArticleUrl = "http://" + HttpContext.Request.Url.Host + "/cms/Article/Edit/" + ArticleId.ToString() + "?RevisionId=" + RevisionId.ToString() + "&ClsfId=" + ClsfId.ToString();

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
                mailBody = mailBody.Replace("##ApproveType##", "ARTICLE");
                mailBody = mailBody.Replace("##senderNameSurname##", currentUserFullName);
                mailBody = mailBody.Replace("##message##", SendToApproveMessage);
                mailBody = mailBody.Replace("##contentName##", getArticleZoneFull.Headline.Trim());
                mailBody = mailBody.Replace("##contentUrl##", articleUrl);
                mailBody = mailBody.Replace("##contentCMSUrl##", editArticleUrl);
                mailBody = mailBody.Replace("##date##", DateTime.Now.ToString("dd MMMM yyyy hh:mm"));
                mailBody = "<html><head></head>" + mailBody + "</html>";
                // Mail Template Render End


                // DB Insert Start
                InstantMessaging insertInstantMessaging = new InstantMessaging();
                insertInstantMessaging.CreateDate = DateTime.Now;
                insertInstantMessaging.From = currentUserId;
                insertInstantMessaging.Message = SendToApproveMessage.Trim(); //currentUserFullName + " kullanıcısı " + getArticleZoneFull.Headline.Trim() + " isimli article üzerinde bir değişiklik yaptı ve bunu onaylamanızı istiyor.";
                insertInstantMessaging.Subject = "Article Approve Request";
                insertInstantMessaging.RelatedId = getArticleZoneFull.ArticleID;
                insertInstantMessaging.RelatedName = "Article";
                insertInstantMessaging.To = UserId;
                insertInstantMessaging.Type = SendToApproveType.ArticleApprove;
                dbContext.InstantMessagings.Add(insertInstantMessaging);
                dbContext.SaveChanges();
                // DB Insert End


                // MAIL SEND START
                var mailResult = MailSender.SendMail(getSelectedUser.Email.Trim(), null, null, "Article Approve Request", mailBody, null);
                if (mailResult.status)
                {
                    TempData["Message"] = "Success";
                }
                else
                {
                    TempData["Message"] = mailResult.message;
                }
                // MAIL SEND END



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
                return RedirectToAction("Edit", new { id = ArticleId, RevisionId = RevisionId, ClsfId = ClsfId });
            }
        }
        // Send To Approve End

        public string GetTags(string term)
        {
            string result = string.Empty;
            CmsDbContext dbContext = new CmsDbContext();
            List<Tag> tags = dbContext.Tags.Where(x => x.Text.Contains(term)).ToList();
            List<TagResult> tResults = new List<TagResult>();

            foreach (Tag t in tags)
            {
                TagResult tr = new TagResult { id = t.ID.ToString(), label = t.Text, value = t.Text };
                tResults.Add(tr);
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            result = jss.Serialize(tResults);

            return result;
        }

        [CmsAuthorize(Roles = "PowerUser")]
        public ActionResult BulkImport()
        {
            return View();
        }

        [CmsAuthorize(Roles = "PowerUser")]
        public ActionResult BulkArticleCreate(FormCollection collection)
        {
            HttpPostedFileBase file = Request.Files["upload"];
            if (file != null && file.ContentLength > 0)
            {
                string fileName = file.FileName.Split('.')[0];
                string extension = file.FileName.Split('.')[1];

                try
                {
                    #region Save File
                    string path = Server.MapPath("/i/content") + "//" + fileName + " - " + DateTime.Now.ToLongDateString() + "." + extension;
                    Request.Files["upload"].SaveAs(path);
                    #endregion

                    #region Get Datatable From Excel
                    string connectionString = string.Empty;
                    if (file.FileName.Contains(".xml"))
                    {
                        GetBulkDataFromXml(path);
                    }
                    else
                    {
                        if (file.FileName.Contains(".xlsx"))
                        {
                            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
                        }
                        else
                        {
                            //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=YES\";";
                            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "; Extended Properties=\"Excel 8.0;HDR=Yes;\";";
                        }

                        OleDbConnection conn = new OleDbConnection(connectionString);
                        conn.Open();
                        DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();

                        string strSQL = "SELECT * FROM [" + firstSheetName + "]";
                        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                        DataSet ds = new DataSet();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(ds);

                        Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                        CmsDbContext dbContext = new CmsDbContext();

                        bool result = true;

                        #region Get Article From Excel Data
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Article a = new Article();
                            a.Headline = ds.Tables[0].Rows[i]["Headline"].ToString();
                            a.Summary = ds.Tables[0].Rows[i]["Summary"].ToString();
                            a.ClassificationId = Convert.ToInt32(ds.Tables[0].Rows[i]["ClassificationId"].ToString());
                            a.LangId = ds.Tables[0].Rows[i]["LangId"].ToString();
                            a.Article1 = ds.Tables[0].Rows[i]["Content_1"].ToString();
                            a.Article2 = ds.Tables[0].Rows[i]["Content_2"].ToString();
                            a.Custom1 = ds.Tables[0].Rows[i]["Custom_1"].ToString();
                            a.Custom2 = ds.Tables[0].Rows[i]["Custom_2"].ToString();
                            a.Custom3 = ds.Tables[0].Rows[i]["Custom_3"].ToString();
                            a.Custom4 = ds.Tables[0].Rows[i]["Custom_4"].ToString();
                            a.Custom5 = ds.Tables[0].Rows[i]["Custom_5"].ToString();
                            a.Custom6 = ds.Tables[0].Rows[i]["Custom_6"].ToString();
                            a.Custom7 = ds.Tables[0].Rows[i]["Custom_7"].ToString();
                            a.Custom8 = ds.Tables[0].Rows[i]["Custom_8"].ToString();
                            a.Custom9 = ds.Tables[0].Rows[i]["Custom_9"].ToString();
                            a.Custom10 = ds.Tables[0].Rows[i]["Custom_10"].ToString();

                            a.Created = DateTime.Now;
                            a.CreatedBy = currentUserId;
                            a.Status = (byte)1;
                            a.Updated = DateTime.Now;
                            a.Startdate = DateTime.Now;

                            #region Tags
                            string tags = ds.Tables[0].Rows[i]["Tags"].ToString();
                            if (!string.IsNullOrEmpty(tags))
                            {
                                List<string> tagList = tags.Split(',').ToList();
                                List<Tag> AllTags = dbContext.Tags.ToList();
                                List<int> tagIds = new List<int>();

                                foreach (string s in tagList)
                                {
                                    if (AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault() != null)
                                    {
                                        Tag tCurrent = AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault();
                                        tagIds.Add(tCurrent.ID);
                                        if (string.IsNullOrEmpty(tCurrent.Alias))
                                        {
                                            int counter = 2;
                                            tCurrent.Alias = CmsHelper.StringToAlphaNumeric(tCurrent.Text, false);
                                            //check if alias exists
                                            while (dbContext.Tags.Where(x => x.Alias == tCurrent.Alias).ToList().Count > 0)
                                            {
                                                tCurrent.Alias += "-" + counter;
                                                counter++;
                                            }
                                        }

                                        dbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        //tag kayıt
                                        Tag newTag = new Tag { AddedDate = DateTime.Now, IsActive = true, Counter = 0, PublisherID = currentUserId, SiteID = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : 6, Text = s, Alias = CmsHelper.StringToAlphaNumeric(s, false) };

                                        dbContext.Tags.Add(newTag);
                                        if (dbContext.SaveChanges() > 0)
                                        {
                                            tagIds.Add(newTag.ID);
                                        }
                                    }
                                }
                                a.TagIds = string.Join(",", tagIds.ToArray());
                                a.TagContents = tags;
                            }
                            #endregion

                            result = BulkSaveArticle(a, ds, i);
                        }
                        #endregion

                        if (result)
                        {
                            TempData["Message"] = "Bulk Import Completed";
                        }
                        else
                        {
                            TempData["HasError"] = true;
                            TempData["Message"] = "Bulk Import Failed";
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;
                }
            }

            return RedirectToAction("BulkImport");
        }

        private void GetBulkDataFromXml(string path)
        {
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            XmlDocument doc = new XmlDocument();
            CmsDbContext dbContext = new CmsDbContext();
            bool result = true;
            doc.Load(path);

            //doc.GetElementsByTagName("article")[0]["Headline"].InnerText
            for (int i = 0; i < doc.GetElementsByTagName("article").Count; i++)
            {
                Article a = new Article();
                a.Headline = doc.GetElementsByTagName("article")[i]["Headline"].InnerText;
                a.Summary = doc.GetElementsByTagName("article")[i]["Summary"].InnerText;
                a.ClassificationId = Convert.ToInt32(doc.GetElementsByTagName("article")[i]["ClassificationId"].InnerText);
                a.LangId = doc.GetElementsByTagName("article")[i]["LangId"].InnerText;
                a.Article1 = doc.GetElementsByTagName("article")[i]["Content_1"].InnerText;
                a.Article2 = doc.GetElementsByTagName("article")[i]["Content_2"].InnerText;
                a.Custom1 = doc.GetElementsByTagName("article")[i]["Custom_1"].InnerText;
                a.Custom2 = doc.GetElementsByTagName("article")[i]["Custom_2"].InnerText;
                a.Custom3 = doc.GetElementsByTagName("article")[i]["Custom_3"].InnerText;
                a.Custom4 = doc.GetElementsByTagName("article")[i]["Custom_4"].InnerText;
                a.Custom5 = doc.GetElementsByTagName("article")[i]["Custom_5"].InnerText;
                a.Custom6 = doc.GetElementsByTagName("article")[i]["Custom_6"].InnerText;
                a.Custom7 = doc.GetElementsByTagName("article")[i]["Custom_7"].InnerText;
                a.Custom8 = doc.GetElementsByTagName("article")[i]["Custom_8"].InnerText;
                a.Custom9 = doc.GetElementsByTagName("article")[i]["Custom_9"].InnerText;
                a.Custom10 = doc.GetElementsByTagName("article")[i]["Custom_10"].InnerText;

                a.Created = DateTime.Now;
                a.CreatedBy = currentUserId;
                a.Status = (byte)1;
                a.Updated = DateTime.Now;
                a.Startdate = DateTime.Now;

                #region Tags
                string tags = doc.GetElementsByTagName("article")[i]["Tags"].InnerText;
                if (!string.IsNullOrEmpty(tags))
                {
                    List<string> tagList = tags.Split(',').ToList();
                    List<Tag> AllTags = dbContext.Tags.ToList();
                    List<int> tagIds = new List<int>();

                    foreach (string s in tagList)
                    {
                        if (AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault() != null)
                        {
                            Tag tCurrent = AllTags.Where(x => x.Text.ToLower() == s.ToLower()).FirstOrDefault();
                            tagIds.Add(tCurrent.ID);
                        }
                        else
                        {
                            Tag newTag = new Tag { AddedDate = DateTime.Now, IsActive = true, Counter = 0, PublisherID = currentUserId, SiteID = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : 6, Text = s };

                            dbContext.Tags.Add(newTag);
                            if (dbContext.SaveChanges() > 0)
                            {
                                tagIds.Add(newTag.ID);
                            }
                        }
                    }
                    a.TagIds = string.Join(",", tagIds.ToArray());
                    a.TagContents = tags;
                }
                #endregion

                XmlNodeList columns = doc.SelectNodes("articles/article")[0].ChildNodes;

                dbContext.Articles.Add(a);
                if (dbContext.SaveChanges() > 0)
                {
                    ArticleRevision ar = new ArticleRevision();
                    ar.Headline = a.Headline;
                    ar.Summary = a.Summary;
                    ar.ClassificationId = a.ClassificationId;
                    ar.LangId = a.LangId;
                    ar.Article1 = a.Article1;
                    ar.Article2 = a.Article2;
                    ar.Custom1 = a.Custom1;
                    ar.Custom2 = a.Custom2;
                    ar.Custom3 = a.Custom3;
                    ar.Custom4 = a.Custom4;
                    ar.Custom5 = a.Custom5;
                    ar.Custom6 = a.Custom6;
                    ar.Custom7 = a.Custom7;
                    ar.Custom8 = a.Custom8;
                    ar.Custom9 = a.Custom9;
                    ar.Custom10 = a.Custom10;

                    ar.Created = a.Created;
                    ar.CreatedBy = a.CreatedBy;
                    ar.Status = (byte)1;
                    ar.RevisionStatus = "L";
                    ar.TagContents = a.TagContents;
                    ar.TagIds = a.TagIds;
                    ar.ArticleId = a.Id;
                    ar.Approved = DateTime.Now;
                    ar.ApprovedBy = currentUserId;
                    ar.RevisedBy = currentUserId;
                    ar.Startdate = DateTime.Now;

                    dbContext.ArticleRevisions.Add(ar);
                    if (dbContext.SaveChanges() > 0)
                    {
                        //article ve revizyonu oluşturuldu
                        #region Zones
                        foreach (XmlNode dc in columns)
                        {
                            if (dc.Name.Contains("ZoneId"))
                            {
                                int zoneId = Convert.ToInt32(dc.Name.Substring(dc.Name.IndexOf("ZoneId_") + "ZoneId_".Length));
                                string alias = doc.GetElementsByTagName("article")[i]["Alias_" + zoneId].InnerText;

                                ArticleZone az = new ArticleZone();
                                az.Article = a;
                                az.ArticleID = a.Id;
                                az.AzAlias = alias;
                                az.AzOrder = 0;
                                az.IsAliasProtected = true;
                                az.ZoneID = zoneId;

                                dbContext.ArticleZones.Add(az);
                                if (dbContext.SaveChanges() > 0)
                                {
                                    ArticleZoneRevision azr = new ArticleZoneRevision();
                                    azr.ArticleID = a.Id;
                                    azr.AzAlias = az.AzAlias;
                                    azr.AzOrder = az.AzOrder;
                                    azr.IsAliasProtected = az.IsAliasProtected;
                                    azr.ZoneID = az.ZoneID;
                                    azr.RevID = ar.RevisionId;
                                    dbContext.ArticleZoneRevisions.Add(azr);

                                    if (dbContext.SaveChanges() > 0)
                                    {
                                        // hepsi tamam
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }

                if (result)
                {
                    TempData["Message"] = "Bulk Import Completed";
                }
                else
                {
                    TempData["HasError"] = true;
                    TempData["Message"] = "Bulk Import Failed";
                }
            }
        }

        private bool BulkSaveArticle(Article a, DataSet ds, int i)
        {
            CmsDbContext dbContext = new CmsDbContext();
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
            DataColumnCollection columns = ds.Tables[0].Columns;
            bool result = true;

            dbContext.Articles.Add(a);
            if (dbContext.SaveChanges() > 0)
            {
                ArticleRevision ar = new ArticleRevision();
                ar.Headline = a.Headline;
                ar.Summary = a.Summary;
                ar.ClassificationId = a.ClassificationId;
                ar.LangId = a.LangId;
                ar.Article1 = a.Article1;
                ar.Article2 = a.Article2;
                ar.Custom1 = a.Custom1;
                ar.Custom2 = a.Custom2;
                ar.Custom3 = a.Custom3;
                ar.Custom4 = a.Custom4;
                ar.Custom5 = a.Custom5;
                ar.Custom6 = a.Custom6;
                ar.Custom7 = a.Custom7;
                ar.Custom8 = a.Custom8;
                ar.Custom9 = a.Custom9;
                ar.Custom10 = a.Custom10;

                ar.Created = a.Created;
                ar.CreatedBy = a.CreatedBy;
                ar.Status = (byte)1;
                ar.RevisionStatus = "L";
                ar.TagContents = a.TagContents;
                ar.TagIds = a.TagIds;
                ar.ArticleId = a.Id;
                ar.Approved = DateTime.Now;
                ar.ApprovedBy = currentUserId;
                ar.RevisedBy = currentUserId;
                ar.Startdate = DateTime.Now;

                dbContext.ArticleRevisions.Add(ar);
                if (dbContext.SaveChanges() > 0)
                {
                    //article ve revizyonu oluşturuldu
                    #region Zones
                    foreach (DataColumn dc in columns)
                    {
                        if (dc.ColumnName.Contains("ZoneId"))
                        {
                            int zoneId = Convert.ToInt32(dc.ColumnName.Substring(dc.ColumnName.IndexOf("ZoneId_") + "ZoneId_".Length));
                            string alias = ds.Tables[0].Rows[i]["Alias_" + zoneId].ToString();

                            ArticleZone az = new ArticleZone();
                            az.Article = a;
                            az.ArticleID = a.Id;
                            az.AzAlias = alias;
                            az.AzOrder = 0;
                            az.IsAliasProtected = true;
                            az.ZoneID = zoneId;

                            dbContext.ArticleZones.Add(az);
                            if (dbContext.SaveChanges() > 0)
                            {
                                ArticleZoneRevision azr = new ArticleZoneRevision();
                                azr.ArticleID = a.Id;
                                azr.AzAlias = az.AzAlias;
                                azr.AzOrder = az.AzOrder;
                                azr.IsAliasProtected = az.IsAliasProtected;
                                azr.ZoneID = az.ZoneID;
                                azr.RevID = ar.RevisionId;
                                dbContext.ArticleZoneRevisions.Add(azr);

                                if (dbContext.SaveChanges() > 0)
                                {
                                    // hepsi tamam
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        [CmsAuthorize(Roles = "PowerUser,Editor,ContentManager,UserCreator", Permission = "View", ContentType = "Article")]
        public ActionResult Compare(int id, int revisionId)
        {

            ArticleCompareViewModel model = new ArticleCompareViewModel();
            CmsDbContext dbContext = new CmsDbContext();
            var article = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == id && f.RevisionStatus == "L");
            model.revisionList = dbContext.ArticleRevisions.Where(w => w.ArticleId == article.ArticleId && w.RevisionStatus != "L").ToList();

            if (article != null)
            {
                var articleRevision = model.revisionList.FirstOrDefault(f => f.RevisionId == revisionId);

                model.live = FillFields(article);
                model.revision = FillFields(articleRevision);
            }


            return View(model);
        }

        public Rev FillFields(ArticleRevision article)
        {
            CmsDbContext dbContext = new CmsDbContext();
            Dictionary<string, string> customs = new Dictionary<string, string>();

            var subzone = dbContext.Zones.FirstOrDefault(f => f.Id == article.NavigationZoneId);
            var classification = dbContext.Classifications.FirstOrDefault(f => f.ID == article.ClassificationId);
            var rev = new Rev();
            rev.ArticleId = article.ArticleId;
            rev.ClassificationId = article.ClassificationId;
            rev.Status = article.Status;
            rev.RevisionDate = article.RevisionDate;
            rev.RevisedBy = article.RevisedBy;
            rev.RevisionId = article.RevisionId;
            rev.RevisionStatus = article.RevisionStatus;
            rev.Classification = (classification != null ? (classification.ID + " - " + classification.Name) : " - ");
            rev.Language = article.LangId;
            rev.Zones = FillZone(article.RevisionId);
            rev.StartDt = article.Startdate;
            rev.EndDt = article.Enddate;
            rev.ArticleType = FillType(article);
            rev.NavigationDisplay = FillNavigationDisplay(article);
            rev.SubZone = (subzone != null ? (subzone.Id + " - " + subzone.Name) : " - ");
            rev.MenuText = article.MenuText;
            rev.Headline = article.Headline;
            rev.Summary = article.Summary;
            rev.BeforeHead = HttpUtility.HtmlDecode(article.BeforeHead);
            rev.BeforeBody = HttpUtility.HtmlDecode(article.BeforeBody);
            rev.CustomBody = HttpUtility.HtmlDecode(article.CustomBody);
            rev.CustomHtml = HttpUtility.HtmlDecode(article.CustomHtmlAttr);
            rev.Tags = article.TagContents;
            rev.RelatedArticles = FillRelatedArticle(article.RevisionId);
            rev.NoIndexFollow = article.NoIndexNoFollow;
            rev.MetaDescription = article.MetaDescription;
            rev.Canonical = article.CanonicalUrl;
            rev.MetaTitle = article.MetaTitle;
            rev.Container1 = HttpUtility.HtmlDecode(article.Article1);
            rev.Container2 = HttpUtility.HtmlDecode(article.Article2);
            rev.Container3 = HttpUtility.HtmlDecode(article.Article3);
            rev.Container4 = HttpUtility.HtmlDecode(article.Article4);
            rev.Container5 = HttpUtility.HtmlDecode(article.Article5);
            if (classification != null)
            {
                var clsfProps = classification.GetType().GetProperties().Where(w => w.Name.StartsWith("Custom") && w.Name.EndsWith("Text")).ToList();
                foreach (var prop in clsfProps)
                {
                    var propValue = dbContext.Entry(classification).Property(prop.Name).CurrentValue.ToString();
                    if (!string.IsNullOrEmpty(propValue))
                    {
                        var propArticle = prop.Name.Replace("Text", "");
                        var propArticleValue = dbContext.Entry(article).Property(propArticle).CurrentValue.ToString();
                        customs.Add(propValue, propArticleValue);
                    }
                }
            }
            rev.Customs = customs;

            return rev;
        }

        public string FillType(ArticleRevision article)
        {
            string articleType = "";
            switch (article.ArticleType)
            {
                case 0:
                    articleType = "Internal Article";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 1:
                    articleType = "Redirect to External URL";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 2:
                    articleType = "Redirect to Internal Article";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 3:
                    articleType = "Redirect to Internal Zone [Last Updated Article]";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 4:
                    articleType = "Redirect to Internal Zone [First Ordered Article]";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 5:
                    articleType = "Inline IFRAME";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 6:
                    articleType = "Free Form Link";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 7:
                    articleType = "Redirect to Internal Zone [First Updated Article]";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 8:
                    articleType = "Redirect to Internal Zone [Last Ordered Article]";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                case 9:
                    articleType = "Mapped Article";
                    if (!string.IsNullOrEmpty(article.ArticleTypeDetail))
                    {
                        articleType = articleType + " : " + article.ArticleTypeDetail;
                    }
                    break;
                default:
                    break;
            }
            return articleType;
        }

        public string FillNavigationDisplay(ArticleRevision article)
        {
            string navigationDisplay = "";
            switch (article.NavigationDisplay)
            {
                case 0:
                    navigationDisplay = "Hide from Navigation Menu";
                    break;
                case 1:
                    navigationDisplay = "Display as Item";
                    break;
                case 2:
                    navigationDisplay = "Display as Folder [Open Folder Only]";
                    break;
                case 3:
                    navigationDisplay = "Display as Folder [Open Page on Click]";
                    break;
                default:
                    break;
            }
            return navigationDisplay;
        }


        public List<string> FillRelatedArticle(long articleRevisionId)
        {
            List<string> relatedArticles = new List<string>();
            var articles = context.SelectArticleRelationsRevisionDetails(articleRevisionId);
            foreach (var article in articles)
            {
                relatedArticles.Add(article.article_id + " - " + article.out_name);
            }
            return relatedArticles;
        }

        public List<ZoneDetailModel> FillZone(long articleRevisionId)
        {
            List<ZoneDetailModel> zoneList = new List<ZoneDetailModel>();
            CmsDbContext dbContext = new CmsDbContext();
            var articleZones = dbContext.ArticleZoneRevisions.Where(w => w.RevID == articleRevisionId).ToList();
            foreach (var articleZone in articleZones)
            {
                var zoneListItem = new ZoneDetailModel();
                var zone = dbContext.Zones.FirstOrDefault(f => f.Id == articleZone.ZoneID);
                if (zone != null)
                {
                    zoneListItem.Zone = zone.Id + " - " + zone.Name;
                    zoneListItem.Order = articleZone.AzOrder;
                    zoneListItem.Alias = articleZone.AzAlias;
                    zoneListItem.IsPage = articleZone.IsPage;

                    var languageRelationList = context.SelectArticleLanguageRelationsRevisionDetails(articleZone.RevID, articleZone.ZoneID, articleZone.ArticleID).ToList();
                    var languageRelations = new List<string>();
                    foreach (var langeuageRelation in languageRelationList)
                    {
                        languageRelations.Add(langeuageRelation.article_id + " - " + langeuageRelation.out_name);
                    }
                    zoneListItem.LanguageRelations = languageRelations;

                    zoneList.Add(zoneListItem);
                }
            }
            return zoneList;
        }
    }
}