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
using Newtonsoft.Json;
using EuroCMS.Admin.ViewModels;
using System.Web.Script.Serialization;

namespace EuroCMS.Admin.Controllers
{

    public class ArticleOrderController : BaseController
    {
        ArticleOrderDbContext context = new ArticleOrderDbContext();
        CmsDbContext dbContext = new CmsDbContext();


        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        [CmsAuthorize(Permission = "List", ContentType = "ArticleOrder")]
        public ActionResult Index(int? ZoneId)
        {
            List<Zone> listAllZones = new List<Zone>();
            listAllZones = dbContext.Zones.Where(z => z.Status == "A").ToList();

            List<string> allowedArticleIds = null;
            List<string> disAllowedArticleIds = null;
            var model = new ArticleOrderViewModel();

            #region Membership Permission Control
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
                    List<string> allowedSiteZoneGroupss = Bags.GetZoneGroupsBySite(Convert.ToInt32(s), null).Select(x => x.Items.FirstOrDefault().Value).ToList();
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
                #endregion

                listAllZones = listAllZones.Where(x => allowedZones.Contains(x.Id.ToString())).ToList();
                #endregion
            }
            model.ZoneList = listAllZones;
            model.ZoneGroupIds = listAllZones.Select(s => s.ZoneGroupId).Distinct().ToList();

            if (ZoneId.HasValue)
            {
                model.ZoneId = ZoneId.Value;
                model.result = dbContext.vArticlesZonesFulls.Where(w => w.ZoneID == model.ZoneId).OrderBy(o => o.AzOrder).Select(s => new ArticleOrderArticle { id = s.ArticleID, headline = s.Headline, status = s.Status }).ToList(); ;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ArticleOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<ArticleOrderList> articles = js.Deserialize<List<ArticleOrderList>>(model.ids);
                int count = 1;

                foreach (var article in articles)
                {
                    var articleZone = dbContext.ArticleZones.FirstOrDefault(s => s.ZoneID == model.ZoneId && s.ArticleID == article.id);
                    if (articleZone != null)
                    {
                        articleZone.AzOrder = (count * 10);
                        if (dbContext.SaveChanges() > 0)
                        {
                            var articleRevision = dbContext.ArticleRevisions.FirstOrDefault(s => s.ArticleId == article.id && s.RevisionStatus == "L");
                            if (articleRevision != null)
                            {
                                var articleZoneRevision = dbContext.ArticleZoneRevisions.FirstOrDefault(s => s.ArticleID == article.id && s.ZoneID == model.ZoneId && s.RevID == articleRevision.RevisionId);
                                if (articleZoneRevision != null)
                                {
                                    articleZoneRevision.AzOrder = (count * 10);
                                    dbContext.SaveChanges();
                                }
                            }
                        }
                    }
                    count++;
                }
                TempData["Message"] = "İşlem başarılı...";
                return RedirectToAction("Index", "ArticleOrder", new { ZoneId = model.ZoneId });
            }
            return RedirectToAction("Index", "ArticleOrder");
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id, int zoneId)
        {
            try
            {
                var article = dbContext.Articles.FirstOrDefault(f => f.Id == id);
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                if (article != null)
                {
                    //article tablosu güncelle
                    article.Status = (byte)(article.Status == 1 ? 0 : (article.Status == 0 ? 1 : 1));
                    if (dbContext.SaveChanges() > 0)
                    {
                        //article revision tablosunda revision_status L olan datayı E yap
                        var articleRevision = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
                        if (articleRevision != null)
                        {
                            articleRevision.RevisionStatus = "E";
                            if (dbContext.SaveChanges() > 0)
                            {
                                //article Revision tablosuna revision_status L olan insert at
                                var newArticleRevision = new ArticleRevision();
                                newArticleRevision.Created = DateTime.Now;
                                newArticleRevision.CreatedBy = currentUserId;
                                newArticleRevision.RevisionDate = DateTime.Now;
                                newArticleRevision.RevisionStatus = "L";
                                newArticleRevision.RevisedBy = currentUserId;
                                newArticleRevision.RevisionNote = "ArticleOrder Status Change";
                                newArticleRevision.RevisionFlag1 = false;
                                newArticleRevision.RevisionFlag2 = false;
                                newArticleRevision.RevisionFlag3 = false;
                                newArticleRevision.RevisionFlag4 = false;
                                newArticleRevision.RevisionFlag5 = false;
                                newArticleRevision.Approved = DateTime.Now;
                                newArticleRevision.ApprovedBy = currentUserId;
                                newArticleRevision.ArticleId = article.Id;
                                newArticleRevision.ClassificationId = article.ClassificationId;
                                newArticleRevision.Status = article.Status;
                                newArticleRevision.Startdate = article.Startdate;
                                newArticleRevision.Enddate = article.Enddate;
                                newArticleRevision.Order = article.Order;
                                newArticleRevision.LangId = article.LangId;
                                newArticleRevision.NavigationDisplay = article.NavigationDisplay;
                                newArticleRevision.NavigationZoneId = article.NavigationZoneId;
                                newArticleRevision.MenuText = article.MenuText;
                                newArticleRevision.Headline = article.Headline;
                                newArticleRevision.Summary = article.Summary;
                                newArticleRevision.Keywords = article.Keywords;
                                newArticleRevision.ArticleType = article.ArticleType;
                                newArticleRevision.ArticleTypeDetail = article.ArticleTypeDetail;
                                newArticleRevision.Article1 = article.Article1;
                                newArticleRevision.Article2 = article.Article2;
                                newArticleRevision.Article3 = article.Article3;
                                newArticleRevision.Article4 = article.Article4;
                                newArticleRevision.Article5 = article.Article5;
                                newArticleRevision.Custom1 = article.Custom1;
                                newArticleRevision.Custom2 = article.Custom2;
                                newArticleRevision.Custom3 = article.Custom3;
                                newArticleRevision.Custom4 = article.Custom4;
                                newArticleRevision.Custom5 = article.Custom5;
                                newArticleRevision.Custom6 = article.Custom6;
                                newArticleRevision.Custom7 = article.Custom7;
                                newArticleRevision.Custom8 = article.Custom8;
                                newArticleRevision.Custom9 = article.Custom9;
                                newArticleRevision.Custom10 = article.Custom10;
                                newArticleRevision.Custom11 = article.Custom11;
                                newArticleRevision.Custom12 = article.Custom12;
                                newArticleRevision.Custom13 = article.Custom13;
                                newArticleRevision.Custom14 = article.Custom14;
                                newArticleRevision.Custom15 = article.Custom15;
                                newArticleRevision.Custom16 = article.Custom16;
                                newArticleRevision.Custom17 = article.Custom17;
                                newArticleRevision.Custom18 = article.Custom18;
                                newArticleRevision.Custom19 = article.Custom19;
                                newArticleRevision.Custom20 = article.Custom20;
                                newArticleRevision.Flag1 = article.Flag1;
                                newArticleRevision.Flag2 = article.Flag2;
                                newArticleRevision.Flag3 = article.Flag3;
                                newArticleRevision.Flag4 = article.Flag4;
                                newArticleRevision.Flag5 = article.Flag5;
                                newArticleRevision.date_1 = article.date_1;
                                newArticleRevision.date_2 = article.date_2;
                                newArticleRevision.date_3 = article.date_3;
                                newArticleRevision.date_4 = article.date_4;
                                newArticleRevision.date_5 = article.date_5;
                                newArticleRevision.Cl1 = article.Cl1;
                                newArticleRevision.Cl2 = article.Cl2;
                                newArticleRevision.Cl3 = article.Cl3;
                                newArticleRevision.Cl4 = article.Cl4;
                                newArticleRevision.Cl5 = article.Cl5;
                                newArticleRevision.CustomBody = article.CustomBody;
                                newArticleRevision.MetaDescription = article.MetaDescription;
                                newArticleRevision.ContentEditorType1 = "H";
                                newArticleRevision.ContentEditorType2 = "H";
                                newArticleRevision.ContentEditorType3 = "H";
                                newArticleRevision.ContentEditorType4 = "H";
                                newArticleRevision.ContentEditorType5 = "H";
                                newArticleRevision.OmnitureCode = article.OmnitureCode;
                                newArticleRevision.CustomSettings = article.CustomSettings;
                                newArticleRevision.BeforeHead = article.BeforeHead;
                                newArticleRevision.BeforeBody = article.BeforeBody;
                                newArticleRevision.NoIndexNoFollow = article.NoIndexNoFollow;
                                newArticleRevision.CustomHtmlAttr = article.CustomHtmlAttr;
                                newArticleRevision.MetaTitle = article.MetaTitle;
                                newArticleRevision.CanonicalUrl = article.CanonicalUrl;
                                newArticleRevision.TagIds = article.TagIds;
                                newArticleRevision.TagContents = article.TagContents;

                                dbContext.ArticleRevisions.Add(newArticleRevision);

                                if (dbContext.SaveChanges() > 0)
                                {
                                    //article_zone a article_id ile select at 
                                    var articleZone = dbContext.ArticleZones.FirstOrDefault(f => f.ArticleID == article.Id && f.ZoneID == zoneId);
                                    if (articleZone != null)
                                    {
                                        //atılan insert sonucunda oluşan rev_id yi ekleyerek article_zone a attığın select soncundaki bilgiler ile article_zone_revision tablosuna insert at
                                        var articleZoneRevision = new ArticleZoneRevision();
                                        articleZoneRevision.RevID = newArticleRevision.RevisionId;
                                        articleZoneRevision.ArticleID = article.Id;
                                        articleZoneRevision.ZoneID = articleZone.ZoneID;
                                        articleZoneRevision.AzOrder = articleZone.AzOrder;
                                        articleZoneRevision.AzAlias = articleZone.AzAlias;
                                        articleZoneRevision.IsAliasProtected = articleZone.IsAliasProtected;
                                        articleZoneRevision.IsPage = articleZone.IsPage;

                                        dbContext.ArticleZoneRevisions.Add(articleZoneRevision);

                                        if (dbContext.SaveChanges() > 0)
                                        {
                                            return Json(new { error = false, status = article.Status.ToString(), message = "işlem başarılı" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //bitti
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = true, status = "", message = "işlem başarısız" }, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
