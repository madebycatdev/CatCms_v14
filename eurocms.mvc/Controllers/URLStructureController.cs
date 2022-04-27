using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class URLStructureController : Controller
    {
        //
        // GET: /URLStructure/

        public ActionResult Index()
        {
            CmsDbContext dbContext = new CmsDbContext();
            List<URLStructure> result = new List<URLStructure>();
            result = dbContext.URLStructures.ToList();
            return View(result);
        }


        public ActionResult Edit(int id)
        {
            CmsDbContext dbContext = new CmsDbContext();
            URLStructure result = new URLStructure();
            result = dbContext.URLStructures.Where(s => s.ID == id).FirstOrDefault();

            List<Domain> listDomain = new List<Domain>();
            List<int> selectedDomains = new List<int>();
            selectedDomains = dbContext.URLStructures.Select(s => s.DomainID).ToList();
            if (result != null)
            {
                selectedDomains.Remove(result.DomainID);
            }

            listDomain = dbContext.Domains.Where(s => !selectedDomains.Contains(s.Id)).ToList();

            ViewBag.Domains = listDomain;
            ViewBag.ExistingArticle = TempData["listExistingArticle"];
            ViewBag.ExistingURL = TempData["listExistingURL"];
            TempData["listExistingArticle"] = null;
            TempData["listExistingURL"] = null;

            return View(result);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator")]
        public ActionResult AjaxEdit(int Id, string structureName, string domain, string structureType, string prefix, string protecturl)
        {
            string name = "";
            int newId = 0;

            TempData.Clear();

            try
            {
                // Çakışanlar varsa 
                // TempData["Message"] = Successful;
                // ViewBag.Existing = listExistingArticle;
                // return RedirectToAction("Index",Id); 
                // yoksa 
                // TempData["Message"] = Successful;
                // return RedirectToAction("Index");

                name = string.IsNullOrEmpty(structureName) ? "" : structureName.Trim();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(structureType) || string.IsNullOrEmpty(protecturl))
                {
                    TempData["HasError"] = true;
                    TempData["Message"] = "Please fill all required fields";
                    return Json(new { RedirectUrl = Url.Action("Edit", new { id = Id }) });
                }

                bool isProtectUrl = false;

                isProtectUrl = string.IsNullOrEmpty(protecturl) || protecturl != "1" ? false : true;


                string prefixAlias = "";
                int domainId = 0;

                domainId = Convert.ToInt32(domain);

                prefixAlias = CmsHelper.StringToAlphaNumeric(prefix, false);
                if (prefixAlias == "-")
                {
                    prefixAlias = "";
                }

                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

                CmsDbContext dbContext = new CmsDbContext();

                

                if (Id >= 1)
                {
                    // Edit

                    URLStructure getUrlStructure = new URLStructure();
                    getUrlStructure = dbContext.URLStructures.Where(s => s.ID == Id).FirstOrDefault();

                    if (getUrlStructure == null)
                    {
                        TempData["HasError"] = true;
                        TempData["Message"] = "URL Structure is not found";
                        return Json(new { RedirectUrl = Url.Action("Index")});
                    }

                    getUrlStructure.DomainID = domainId;
                    getUrlStructure.Structure = structureType;
                    getUrlStructure.UpdateDate = DateTime.Now;
                    getUrlStructure.UpdatedBy = currentUserId;
                    getUrlStructure.Name = name;
                    getUrlStructure.Prefix = prefixAlias;
                    getUrlStructure.IsProtect = isProtectUrl;

                    dbContext.Entry<URLStructure>(getUrlStructure).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    newId = Id;

                }
                else
                {
                    // Insert

                    URLStructure urlStructure = new URLStructure();
                    urlStructure.CreateDate = DateTime.Now;
                    urlStructure.CreatedBy = currentUserId;
                    urlStructure.DomainID = domainId;
                    urlStructure.IsProtect = isProtectUrl;
                    urlStructure.Name = name;
                    urlStructure.Prefix = prefixAlias;
                    urlStructure.Structure = structureType;
                    urlStructure.UpdateDate = DateTime.Now;
                    urlStructure.UpdatedBy = currentUserId;

                    dbContext.URLStructures.Add(urlStructure);
                    dbContext.SaveChanges();
                    newId = urlStructure.ID;
                }


                if (isProtectUrl)
                {
                    TempData["Message"] = Id >= 1 ? "Your URL Structure has been successfully updated" : "Your URL Structure has been successfully created";
                    return Json(new { RedirectUrl = Url.Action("Index")});
                }


                #region Update

                List<vArticlesZonesFull> listExistingURL = new List<vArticlesZonesFull>();

                // Language Alias Update
                List<Language> listLanguage = new List<Language>();
                listLanguage = dbContext.Languages.Where(l => string.IsNullOrEmpty(l.Alias)).ToList();

                foreach (Language lang in listLanguage)
                {
                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(lang.Name, false);

                    Language getLang = new Language();
                    getLang = dbContext.Languages.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getLang != null)
                    {
                        alias = alias + "-" + i;
                        getLang = dbContext.Languages.Where(s => s.Alias == alias).FirstOrDefault();
                    }


                    lang.Alias = alias;

                    dbContext.Entry<Language>(lang).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                listLanguage = dbContext.Languages.ToList();


                // Site Alias Update
                List<Site> listSite = new List<Site>();
                listSite = dbContext.Sites.Where(s => s.DomainId == domainId && string.IsNullOrEmpty(s.Alias)).ToList();
                List<int> listSiteId = new List<int>();


                foreach (Site site in listSite)
                {
                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(site.Name, false);

                    Site getSite = new Site();
                    getSite = dbContext.Sites.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getSite != null)
                    {
                        alias = alias + "-" + i;
                        getSite = dbContext.Sites.Where(s => s.Alias == alias).FirstOrDefault();
                    }

                    site.Alias = alias;
                    site.CreatedBy = currentUserId;

                    dbContext.Entry<Site>(site).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                listSite = dbContext.Sites.Where(s => s.DomainId == domainId).ToList();
                listSiteId = listSite.Select(s => s.Id).ToList();


                // Zone Group Alias Update
                List<ZoneGroup> listZoneGroup = new List<ZoneGroup>();
                listZoneGroup = dbContext.ZoneGroups.Where(s => string.IsNullOrEmpty(s.Alias) && listSiteId.Contains(s.SiteId)).ToList();
                List<int> listZoneGroupId = new List<int>();


                foreach (ZoneGroup zoneGroup in listZoneGroup)
                {
                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(zoneGroup.Name, false);

                    ZoneGroup getZoneGroup = new ZoneGroup();
                    getZoneGroup = dbContext.ZoneGroups.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getZoneGroup != null)
                    {
                        alias = alias + "-" + i;
                        getZoneGroup = dbContext.ZoneGroups.Where(s => s.Alias == alias).FirstOrDefault();
                    }

                    zoneGroup.Alias = alias;
                    zoneGroup.CreatedBy = currentUserId;

                    dbContext.Entry<ZoneGroup>(zoneGroup).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                listZoneGroup = dbContext.ZoneGroups.Where(s => listSiteId.Contains(s.SiteId)).ToList();
                listZoneGroupId = listZoneGroup.Select(s => s.Id).ToList();


                // Zone Alias Update
                List<Zone> listZone = new List<Zone>();
                listZone = dbContext.Zones.Where(s => string.IsNullOrEmpty(s.Alias) && listZoneGroupId.Contains(s.ZoneGroupId)).ToList();
                List<int> listZoneId = new List<int>();

                foreach (Zone zone in listZone)
                {
                    // Update Zones

                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(zone.Name, false);

                    Zone getZone = new Zone();
                    getZone = dbContext.Zones.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getZone != null)
                    {
                        alias = alias + "-" + i;
                        getZone = dbContext.Zones.Where(s => s.Alias == alias).FirstOrDefault();
                    }

                    zone.Alias = alias;
                    zone.CreatedBy = currentUserId;

                    dbContext.Entry<Zone>(zone).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    // Insert Zone Revision

                    ZoneRevision getLastZoneRevision = new ZoneRevision();
                    ZoneRevision insertZoneRevision = new ZoneRevision();

                    var revisions = zone.Revisions.OrderByDescending(o => o.RevisionDate);
                    long LastRevisionId = 1;
                    ZoneRevision ZoneRev = revisions.Where(r => r.RevisionStatus == "L").FirstOrDefault();
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

                    getLastZoneRevision = dbContext.ZoneRevisions.Where(s => s.RevisionId == LastRevisionId).FirstOrDefault();
                    insertZoneRevision = getLastZoneRevision;
                    insertZoneRevision.Alias = alias;
                    insertZoneRevision.Created = DateTime.Now;
                    insertZoneRevision.RevisionDate = DateTime.Now;
                    insertZoneRevision.RevisedBy = currentUserId;
                    insertZoneRevision.ApprovedBy = currentUserId;
                    insertZoneRevision.CreatedBy = currentUserId;
                    insertZoneRevision.RevisionNote = "URL Structure Bulk Update";

                    if (getLastZoneRevision.RevisionStatus == "L")
                    {
                        getLastZoneRevision.RevisionStatus = "E";
                        insertZoneRevision.RevisionStatus = "L";
                    }

                    dbContext.Entry<ZoneRevision>(getLastZoneRevision).State = System.Data.Entity.EntityState.Modified;
                    dbContext.ZoneRevisions.Add(insertZoneRevision);
                    dbContext.SaveChanges();

                }

                listZone = dbContext.Zones.Where(s => listZoneGroupId.Contains(s.ZoneGroupId)).ToList();
                listZoneId = listZone.Select(s => s.Id).ToList();

                // Article Alias Update
                List<string> listExistArticleAlias = new List<string>();
                List<vArticlesZonesFull> listExistArticle = new List<vArticlesZonesFull>();
                List<vArticlesZonesFull> listArticle = new List<vArticlesZonesFull>();

                Domain getDomain = new Domain();
                getDomain = dbContext.Domains.Where(s => s.Id == domainId).FirstOrDefault();
                string domainNames = "";
                if (getDomain != null)
                {
                    domainNames = getDomain.Names;
                    listArticle = dbContext.vArticlesZonesFulls.AsNoTracking().Where(s => !s.IsAliasProtected && domainNames.Contains(s.DomainName) && s.Status != 2).ToList();
                }
                if (listArticle.Count <= 0)
                {
                    listArticle = dbContext.vArticlesZonesFulls.AsNoTracking().Where(s => (string.IsNullOrEmpty(s.ArticleZoneAlias) || !s.IsAliasProtected) && listZoneId.Contains(s.ZoneID) && s.Status != 2).ToList();
                }

                List<int> listInsertedArticleIds = new List<int>();
                List<long> listInsertedRevisionIds = new List<long>();

                foreach (vArticlesZonesFull vArticleZone in listArticle)
                {

                    string alias = "", headlineAlias = "";

                    headlineAlias = CmsHelper.StringToAlphaNumeric(vArticleZone.Headline, false);

                    alias = structureType.ToLower();
                    alias = alias.Replace("##lang##", vArticleZone.LanguageAlias);
                    alias = alias.Replace("##site##", vArticleZone.SiteAlias);
                    alias = alias.Replace("##zonegroup##", vArticleZone.ZoneGroupAlias);
                    alias = alias.Replace("##zone##", vArticleZone.ZoneAlias);
                    alias = alias.Replace("##article##", headlineAlias);


                    if (!string.IsNullOrEmpty(prefixAlias))
                    {
                        alias = prefixAlias + "/" + alias;
                    }


                    bool isExistAlias = false, isAliasChanged = false;
                    int i = 2;
                    isExistAlias = dbContext.vArticlesZonesFulls.Where(s => s.ArticleZoneAlias == alias && s.ArticleID != vArticleZone.ArticleID && s.ZoneID != vArticleZone.ZoneID).ToList().Count() > 0 ? true : false;
                    while (isExistAlias)
                    {
                        isAliasChanged = true;
                        alias = alias + "-" + i;
                        isExistAlias = dbContext.vArticlesZonesFulls.Where(s => s.ArticleZoneAlias == alias).ToList().Count() > 0 ? true : false;
                    }

                    if (isAliasChanged)
                    {
                        listExistArticle.Add(vArticleZone);
                        listExistArticleAlias.Add(alias);
                    }

                    long insertedArticleRevId = 0;
                    long lastArticleRevId = 0;

                    if (!listInsertedArticleIds.Contains(vArticleZone.ArticleID))
                    {
                        // Insert Article Revision
                        ArticleRevision insertArticleRev = new ArticleRevision();
                        ArticleRevision getLastArticleRev = new ArticleRevision();

                        ArticleDbContext articleDbContext = new ArticleDbContext();
                        lastArticleRevId = articleDbContext.SelectArticleLastRevision(vArticleZone.ArticleID).FirstOrDefault();
                        getLastArticleRev = dbContext.ArticleRevisions.Where(s => s.RevisionId == lastArticleRevId).FirstOrDefault();
                        insertArticleRev = getLastArticleRev;
                        //insertArticleRev.RevisionId = null;
                        insertArticleRev.RevisedBy = currentUserId;
                        insertArticleRev.CreatedBy = currentUserId;
                        insertArticleRev.RevisionDate = DateTime.Now;
                        insertArticleRev.Created = DateTime.Now;
                        insertArticleRev.RevisionNote = "URL Structure Bulk Update";

                        if (getLastArticleRev.RevisionStatus == "L")
                        {
                            getLastArticleRev.RevisionStatus = "E";
                            insertArticleRev.RevisionStatus = "L";
                        }

                        dbContext.Entry<ArticleRevision>(getLastArticleRev).State = System.Data.Entity.EntityState.Modified;
                        dbContext.ArticleRevisions.Add(insertArticleRev);
                        dbContext.SaveChanges();

                        insertedArticleRevId = insertArticleRev.RevisionId;

                        listInsertedArticleIds.Add(vArticleZone.ArticleID);
                        listInsertedRevisionIds.Add(insertedArticleRevId);

                    }
                    else
                    {
                        int index = listInsertedArticleIds.IndexOf(vArticleZone.ArticleID);
                        insertedArticleRevId = listInsertedRevisionIds[index];
                    }


                    // Insert Article Zone Revision
                    ArticleZoneRevision insertArticleZoneRev = new ArticleZoneRevision();

                    insertArticleZoneRev.ArticleID = vArticleZone.ArticleID;
                    insertArticleZoneRev.AzAlias = alias;
                    insertArticleZoneRev.RevID = insertedArticleRevId;
                    insertArticleZoneRev.ZoneID = vArticleZone.ZoneID;

                    dbContext.ArticleZoneRevisions.Add(insertArticleZoneRev);
                    dbContext.SaveChanges();


                    // Delete Article Zone 
                    ArticleZone deleteArticleZone = new ArticleZone();
                    deleteArticleZone = dbContext.ArticleZones.Where(s => s.ArticleID == vArticleZone.ArticleID && s.ZoneID == vArticleZone.ZoneID).FirstOrDefault();

                    if (deleteArticleZone != null)
                    {
                        dbContext.ArticleZones.Attach(deleteArticleZone);
                        dbContext.ArticleZones.Remove(deleteArticleZone);
                        dbContext.SaveChanges();
                    }

                    // Insert Article Zone

                    ArticleZone insertArticleZone = new ArticleZone();
                    insertArticleZone.ArticleID = vArticleZone.ArticleID;
                    insertArticleZone.ZoneID = vArticleZone.ZoneID;
                    insertArticleZone.AzAlias = alias;
                    insertArticleZone.IsAliasProtected = false;

                    dbContext.ArticleZones.Add(insertArticleZone);
                    dbContext.SaveChanges();

                }


                // Çakışanların Listesi 
                listExistingURL = listExistArticle.ToList();

                #endregion


                if (listExistingURL != null && listExistingURL.Count > 0)
                {
                    TempData["Message"] = Id >= 1 ? "Your URL Structure has been successfully updated" : "Your URL Structure has been successfully created";
                    TempData["listExistingArticle"] = listExistingURL;
                    TempData["listExistingURL"] = listExistArticleAlias;
                    return Json(new { RedirectUrl = Url.Action("Edit", new { id = newId })});
                }


                TempData["Message"] = Id >= 1 ? "Your URL Structure has been successfully updated" : "Your URL Structure has been successfully created";
                return Json(new { RedirectUrl = Url.Action("Index")});

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
                //return Json(new { RedirectUrl = Url.Action("Edit", new { id = newId }) });
            }


            return Json(new { RedirectUrl = Url.Action("Index") });
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator")]
        public ActionResult Edit(int Id, FormCollection collection)
        {
            try
            {
                // Çakışanlar varsa 
                // TempData["Message"] = Successful;
                // ViewBag.Existing = listExistingArticle;
                // return RedirectToAction("Index",Id); 
                // yoksa 
                // TempData["Message"] = Successful;
                // return RedirectToAction("Index");

                string name = "", domain = "", structureType = "", prefix = "", protecturl = "";

                name = string.IsNullOrEmpty(collection["structurename"]) ? "" : collection["structurename"].ToString().Trim();
                domain = string.IsNullOrEmpty(collection["domain"]) ? "" : collection["domain"].ToString().Trim();
                structureType = string.IsNullOrEmpty(collection["structureType"]) ? "" : collection["structureType"].ToString().Trim();
                prefix = string.IsNullOrEmpty(collection["prefix"]) ? "" : collection["prefix"].ToString().Trim();
                protecturl = string.IsNullOrEmpty(collection["protecturl"]) ? "" : collection["protecturl"].ToString().Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(structureType) || string.IsNullOrEmpty(protecturl))
                {
                    TempData["HasError"] = true;
                    TempData["Message"] = "Please fill all required fields";
                    return RedirectToAction("Edit", new { id = Id });
                }

                bool isProtectUrl = false;

                isProtectUrl = string.IsNullOrEmpty(protecturl) || protecturl != "1" ? false : true;


                string prefixAlias = "";
                int domainId = 0, newId = 0;

                domainId = Convert.ToInt32(domain);

                prefixAlias = CmsHelper.StringToAlphaNumeric(prefix, false);
                if (prefixAlias == "-")
                {
                    prefixAlias = "";
                }

                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

                CmsDbContext dbContext = new CmsDbContext();

                if (Id >= 1)
                {
                    // Edit

                    URLStructure getUrlStructure = new URLStructure();
                    getUrlStructure = dbContext.URLStructures.Where(s => s.ID == Id).FirstOrDefault();

                    if (getUrlStructure == null)
                    {
                        TempData["HasError"] = true;
                        TempData["Message"] = "URL Structure is not found";
                        return RedirectToAction("Index");
                    }

                    getUrlStructure.DomainID = domainId;
                    getUrlStructure.Structure = structureType;
                    getUrlStructure.UpdateDate = DateTime.Now;
                    getUrlStructure.UpdatedBy = currentUserId;
                    getUrlStructure.Name = name;
                    getUrlStructure.Prefix = prefixAlias;
                    getUrlStructure.IsProtect = isProtectUrl;

                    dbContext.Entry<URLStructure>(getUrlStructure).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    newId = Id;

                }
                else
                {
                    // Insert

                    URLStructure urlStructure = new URLStructure();
                    urlStructure.CreateDate = DateTime.Now;
                    urlStructure.CreatedBy = currentUserId;
                    urlStructure.DomainID = domainId;
                    urlStructure.IsProtect = isProtectUrl;
                    urlStructure.Name = name;
                    urlStructure.Prefix = prefixAlias;
                    urlStructure.Structure = structureType;
                    urlStructure.UpdateDate = DateTime.Now;
                    urlStructure.UpdatedBy = currentUserId;

                    dbContext.URLStructures.Add(urlStructure);
                    dbContext.SaveChanges();
                    newId = urlStructure.ID;
                }


                if (isProtectUrl)
                {
                    TempData["Message"] = Id >= 1 ? "Your URL Structure has been successfully updated" : "Your URL Structure has been successfully created";
                    return RedirectToAction("Index");
                }


                #region Update

                List<vArticlesZonesFull> listExistingURL = new List<vArticlesZonesFull>();

                // Language Alias Update
                List<Language> listLanguage = new List<Language>();
                listLanguage = dbContext.Languages.Where(l => string.IsNullOrEmpty(l.Alias)).ToList();

                foreach (Language lang in listLanguage)
                {
                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(lang.Name, false);

                    Language getLang = new Language();
                    getLang = dbContext.Languages.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getLang != null)
                    {
                        alias = alias + "-" + i;
                        getLang = dbContext.Languages.Where(s => s.Alias == alias).FirstOrDefault();
                    }


                    lang.Alias = alias;

                    dbContext.Entry<Language>(lang).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                listLanguage = dbContext.Languages.ToList();


                // Site Alias Update
                List<Site> listSite = new List<Site>();
                listSite = dbContext.Sites.Where(s => s.DomainId == domainId && string.IsNullOrEmpty(s.Alias)).ToList();
                List<int> listSiteId = new List<int>();


                foreach (Site site in listSite)
                {
                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(site.Name, false);

                    Site getSite = new Site();
                    getSite = dbContext.Sites.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getSite != null)
                    {
                        alias = alias + "-" + i;
                        getSite = dbContext.Sites.Where(s => s.Alias == alias).FirstOrDefault();
                    }

                    site.Alias = alias;
                    site.CreatedBy = currentUserId;

                    dbContext.Entry<Site>(site).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                listSite = dbContext.Sites.Where(s => s.DomainId == domainId).ToList();
                listSiteId = listSite.Select(s => s.Id).ToList();


                // Zone Group Alias Update
                List<ZoneGroup> listZoneGroup = new List<ZoneGroup>();
                listZoneGroup = dbContext.ZoneGroups.Where(s => string.IsNullOrEmpty(s.Alias) && listSiteId.Contains(s.SiteId)).ToList();
                List<int> listZoneGroupId = new List<int>();


                foreach (ZoneGroup zoneGroup in listZoneGroup)
                {
                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(zoneGroup.Name, false);

                    ZoneGroup getZoneGroup = new ZoneGroup();
                    getZoneGroup = dbContext.ZoneGroups.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getZoneGroup != null)
                    {
                        alias = alias + "-" + i;
                        getZoneGroup = dbContext.ZoneGroups.Where(s => s.Alias == alias).FirstOrDefault();
                    }

                    zoneGroup.Alias = alias;
                    zoneGroup.CreatedBy = currentUserId;

                    dbContext.Entry<ZoneGroup>(zoneGroup).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                listZoneGroup = dbContext.ZoneGroups.Where(s => listSiteId.Contains(s.SiteId)).ToList();
                listZoneGroupId = listZoneGroup.Select(s => s.Id).ToList();


                // Zone Alias Update
                List<Zone> listZone = new List<Zone>();
                listZone = dbContext.Zones.Where(s => string.IsNullOrEmpty(s.Alias) && listZoneGroupId.Contains(s.ZoneGroupId)).ToList();
                List<int> listZoneId = new List<int>();

                foreach (Zone zone in listZone)
                {
                    // Update Zones

                    string alias = "";
                    alias = CmsHelper.StringToAlphaNumeric(zone.Name, false);

                    Zone getZone = new Zone();
                    getZone = dbContext.Zones.Where(s => s.Alias == alias).FirstOrDefault();

                    int i = 2;

                    while (getZone != null)
                    {
                        alias = alias + "-" + i;
                        getZone = dbContext.Zones.Where(s => s.Alias == alias).FirstOrDefault();
                    }

                    zone.Alias = alias;
                    zone.CreatedBy = currentUserId;

                    dbContext.Entry<Zone>(zone).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    // Insert Zone Revision

                    ZoneRevision getLastZoneRevision = new ZoneRevision();
                    ZoneRevision insertZoneRevision = new ZoneRevision();

                    var revisions = zone.Revisions.OrderByDescending(o => o.RevisionDate);
                    long LastRevisionId = 1;
                    ZoneRevision ZoneRev = revisions.Where(r => r.RevisionStatus == "L").FirstOrDefault();
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

                    getLastZoneRevision = dbContext.ZoneRevisions.Where(s => s.RevisionId == LastRevisionId).FirstOrDefault();
                    insertZoneRevision = getLastZoneRevision;
                    insertZoneRevision.Alias = alias;
                    insertZoneRevision.Created = DateTime.Now;
                    insertZoneRevision.RevisionDate = DateTime.Now;
                    insertZoneRevision.RevisedBy = currentUserId;
                    insertZoneRevision.ApprovedBy = currentUserId;
                    insertZoneRevision.CreatedBy = currentUserId;
                    insertZoneRevision.RevisionNote = "URL Structure Bulk Update";

                    if (getLastZoneRevision.RevisionStatus == "L")
                    {
                        getLastZoneRevision.RevisionStatus = "E";
                        insertZoneRevision.RevisionStatus = "L";
                    }

                    dbContext.Entry<ZoneRevision>(getLastZoneRevision).State = System.Data.Entity.EntityState.Modified;
                    dbContext.ZoneRevisions.Add(insertZoneRevision);
                    dbContext.SaveChanges();

                }

                listZone = dbContext.Zones.Where(s => listZoneGroupId.Contains(s.ZoneGroupId)).ToList();
                listZoneId = listZone.Select(s => s.Id).ToList();

                // Article Alias Update
                List<vArticlesZonesFull> listExistArticle = new List<vArticlesZonesFull>();
                List<vArticlesZonesFull> listArticle = new List<vArticlesZonesFull>();

                Domain getDomain = new Domain();
                getDomain = dbContext.Domains.Where(s => s.Id == domainId).FirstOrDefault();
                string domainNames = "";
                if (getDomain != null)
                {
                    domainNames = getDomain.Names;
                    listArticle = dbContext.vArticlesZonesFulls.AsNoTracking().Where(s => !s.IsAliasProtected && domainNames.Contains(s.DomainName) && s.Status != 2).ToList();
                }
                if (listArticle.Count <= 0)
                {
                    listArticle = dbContext.vArticlesZonesFulls.AsNoTracking().Where(s => (string.IsNullOrEmpty(s.ArticleZoneAlias) || !s.IsAliasProtected) && listZoneId.Contains(s.ZoneID) && s.Status != 2).ToList();
                }

                List<int> listInsertedArticleIds = new List<int>();
                List<long> listInsertedRevisionIds = new List<long>();

                foreach (vArticlesZonesFull vArticleZone in listArticle)
                {

                    string alias = "", headlineAlias = "";

                    headlineAlias = CmsHelper.StringToAlphaNumeric(vArticleZone.Headline, false);

                    alias = structureType.ToLower();
                    alias = alias.Replace("##lang##", vArticleZone.LanguageAlias);
                    alias = alias.Replace("##site##", vArticleZone.SiteAlias);
                    alias = alias.Replace("##zonegroup##", vArticleZone.ZoneGroupAlias);
                    alias = alias.Replace("##zone##", vArticleZone.ZoneAlias);
                    alias = alias.Replace("##article##", headlineAlias);


                    if (!string.IsNullOrEmpty(prefixAlias))
                    {
                        alias = prefixAlias + "/" + alias;
                    }


                    bool isExistAlias = false, isAliasChanged = false;
                    int i = 2;
                    isExistAlias = dbContext.vArticlesZonesFulls.Where(s => s.ArticleZoneAlias == alias && s.ArticleID != vArticleZone.ArticleID && s.ZoneID != vArticleZone.ZoneID).ToList().Count() > 0 ? true : false;
                    while (isExistAlias)
                    {
                        isAliasChanged = true;
                        alias = alias + "-" + i;
                        isExistAlias = dbContext.vArticlesZonesFulls.Where(s => s.ArticleZoneAlias == alias).ToList().Count() > 0 ? true : false;
                    }

                    if (isAliasChanged)
                    {
                        listExistArticle.Add(vArticleZone);
                    }

                    long insertedArticleRevId = 0;
                    long lastArticleRevId = 0;
                    
                    if (!listInsertedArticleIds.Contains(vArticleZone.ArticleID))
                    {
                        // Insert Article Revision
                        ArticleRevision insertArticleRev = new ArticleRevision();
                        ArticleRevision getLastArticleRev = new ArticleRevision();

                        ArticleDbContext articleDbContext = new ArticleDbContext();
                        lastArticleRevId = articleDbContext.SelectArticleLastRevision(vArticleZone.ArticleID).FirstOrDefault();
                        getLastArticleRev = dbContext.ArticleRevisions.Where(s => s.RevisionId == lastArticleRevId).FirstOrDefault();
                        insertArticleRev = getLastArticleRev;
                        //insertArticleRev.RevisionId = null;
                        insertArticleRev.RevisedBy = currentUserId;
                        insertArticleRev.CreatedBy = currentUserId;
                        insertArticleRev.RevisionDate = DateTime.Now;
                        insertArticleRev.Created = DateTime.Now;
                        insertArticleRev.RevisionNote = "URL Structure Bulk Update";

                        if (getLastArticleRev.RevisionStatus == "L")
                        {
                            getLastArticleRev.RevisionStatus = "E";
                            insertArticleRev.RevisionStatus = "L";
                        }

                        dbContext.Entry<ArticleRevision>(getLastArticleRev).State = System.Data.Entity.EntityState.Modified;
                        dbContext.ArticleRevisions.Add(insertArticleRev);
                        dbContext.SaveChanges();

                        insertedArticleRevId = insertArticleRev.RevisionId;

                        listInsertedArticleIds.Add(vArticleZone.ArticleID);
                        listInsertedRevisionIds.Add(insertedArticleRevId);

                    }
                    else
                    {
                        int index = listInsertedArticleIds.IndexOf(vArticleZone.ArticleID);
                        insertedArticleRevId = listInsertedRevisionIds[index];
                    }


                    // Insert Article Zone Revision
                    ArticleZoneRevision insertArticleZoneRev = new ArticleZoneRevision();

                    insertArticleZoneRev.ArticleID = vArticleZone.ArticleID;
                    insertArticleZoneRev.AzAlias = alias;
                    insertArticleZoneRev.RevID = insertedArticleRevId;
                    insertArticleZoneRev.ZoneID = vArticleZone.ZoneID;

                    dbContext.ArticleZoneRevisions.Add(insertArticleZoneRev);
                    dbContext.SaveChanges();


                    // Delete Article Zone 
                    ArticleZone deleteArticleZone = new ArticleZone();
                    deleteArticleZone = dbContext.ArticleZones.Where(s => s.ArticleID == vArticleZone.ArticleID && s.ZoneID == vArticleZone.ZoneID).FirstOrDefault();

                    if (deleteArticleZone != null)
                    {
                        dbContext.ArticleZones.Attach(deleteArticleZone);
                        dbContext.ArticleZones.Remove(deleteArticleZone);
                        dbContext.SaveChanges();
                    }

                    // Insert Article Zone

                    ArticleZone insertArticleZone = new ArticleZone();
                    insertArticleZone.ArticleID = vArticleZone.ArticleID;
                    insertArticleZone.ZoneID = vArticleZone.ZoneID;
                    insertArticleZone.AzAlias = alias;
                    insertArticleZone.IsAliasProtected = false;

                    dbContext.ArticleZones.Add(insertArticleZone);
                    dbContext.SaveChanges();

                }


                // Çakışanların Listesi 
                listExistingURL = listExistArticle.ToList();

                #endregion


                if (listExistingURL != null && listExistingURL.Count > 0)
                {
                    TempData["Message"] = Id >= 1 ? "Your URL Structure has been successfully updated" : "Your URL Structure has been successfully created";
                    TempData["listExistingURL"] = listExistingURL;
                    return RedirectToAction("Edit", new { id = newId });
                }


                TempData["Message"] = Id >= 1 ? "Your URL Structure has been successfully updated" : "Your URL Structure has been successfully created";
                return RedirectToAction("Index");

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
        public ActionResult UpdateAll(FormCollection[] collection)
        {
            List<vArticlesZonesFull> result = new List<vArticlesZonesFull>();

            string prefix = "", prefixAlias = "", structureType = "";
            int domainId = 5;

            prefixAlias = CmsHelper.StringToAlphaNumeric(prefix, false);
            if (prefixAlias == "-")
            {
                prefixAlias = "";
            }

            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

            CmsDbContext dbContext = new CmsDbContext();

            // Language Alias Update
            List<Language> listLanguage = new List<Language>();
            listLanguage = dbContext.Languages.Where(l => string.IsNullOrEmpty(l.Alias)).ToList();

            foreach (Language lang in listLanguage)
            {
                string alias = "";
                alias = CmsHelper.StringToAlphaNumeric(lang.Name, false);

                Language getLang = new Language();
                getLang = dbContext.Languages.Where(s => s.Alias == alias).FirstOrDefault();

                int i = 2;

                while (getLang != null)
                {
                    alias = alias + "-" + i;
                    getLang = dbContext.Languages.Where(s => s.Alias == alias).FirstOrDefault();
                }


                lang.Alias = alias;

                dbContext.Entry<Language>(lang).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }


            // Site Alias Update
            List<Site> listSite = new List<Site>();
            listSite = dbContext.Sites.Where(s => s.DomainId == domainId && string.IsNullOrEmpty(s.Alias)).ToList();
            List<int> listSiteId = new List<int>();
            listSiteId = listSite.Select(s => s.Id).ToList();

            foreach (Site site in listSite)
            {
                string alias = "";
                alias = CmsHelper.StringToAlphaNumeric(site.Name, false);

                Site getSite = new Site();
                getSite = dbContext.Sites.Where(s => s.Alias == alias).FirstOrDefault();

                int i = 2;

                while (getSite != null)
                {
                    alias = alias + "-" + i;
                    getSite = dbContext.Sites.Where(s => s.Alias == alias).FirstOrDefault();
                }

                site.Alias = alias;
                site.CreatedBy = currentUserId;

                dbContext.Entry<Site>(site).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }


            // Zone Group Alias Update
            List<ZoneGroup> listZoneGroup = new List<ZoneGroup>();
            listZoneGroup = dbContext.ZoneGroups.Where(s => string.IsNullOrEmpty(s.Alias) && listSiteId.Contains(s.SiteId)).ToList();
            List<int> listZoneGroupId = new List<int>();
            listZoneGroupId = listZoneGroup.Select(s => s.Id).ToList();

            foreach (ZoneGroup zoneGroup in listZoneGroup)
            {
                string alias = "";
                alias = CmsHelper.StringToAlphaNumeric(zoneGroup.Name, false);

                ZoneGroup getZoneGroup = new ZoneGroup();
                getZoneGroup = dbContext.ZoneGroups.Where(s => s.Alias == alias).FirstOrDefault();

                int i = 2;

                while (getZoneGroup != null)
                {
                    alias = alias + "-" + i;
                    getZoneGroup = dbContext.ZoneGroups.Where(s => s.Alias == alias).FirstOrDefault();
                }

                zoneGroup.Alias = alias;
                zoneGroup.CreatedBy = currentUserId;

                dbContext.Entry<ZoneGroup>(zoneGroup).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }


            // Zone Alias Update
            List<Zone> listZone = new List<Zone>();
            listZone = dbContext.Zones.Where(s => string.IsNullOrEmpty(s.Alias) && listZoneGroupId.Contains(s.ZoneGroupId)).ToList();
            List<int> listZoneId = new List<int>();
            listZoneId = listZone.Select(s => s.Id).ToList();

            foreach (Zone zone in listZone)
            {
                // Update Zones

                string alias = "";
                alias = CmsHelper.StringToAlphaNumeric(zone.Name, false);

                Zone getZone = new Zone();
                getZone = dbContext.Zones.Where(s => s.Alias == alias).FirstOrDefault();

                int i = 2;

                while (getZone != null)
                {
                    alias = alias + "-" + i;
                    getZone = dbContext.Zones.Where(s => s.Alias == alias).FirstOrDefault();
                }

                zone.Alias = alias;
                zone.CreatedBy = currentUserId;

                dbContext.Entry<Zone>(zone).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();

                // Insert Zone Revision

                ZoneRevision getLastZoneRevision = new ZoneRevision();
                ZoneRevision insertZoneRevision = new ZoneRevision();

                var revisions = zone.Revisions.OrderByDescending(o => o.RevisionDate);
                long LastRevisionId = 1;
                ZoneRevision ZoneRev = revisions.Where(r => r.RevisionStatus == "L").FirstOrDefault();
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

                getLastZoneRevision = dbContext.ZoneRevisions.Where(s => s.RevisionId == LastRevisionId).FirstOrDefault();
                insertZoneRevision = getLastZoneRevision;
                insertZoneRevision.Alias = alias;
                insertZoneRevision.Created = DateTime.Now;
                insertZoneRevision.RevisionDate = DateTime.Now;
                insertZoneRevision.RevisedBy = currentUserId;
                insertZoneRevision.ApprovedBy = currentUserId;
                insertZoneRevision.CreatedBy = currentUserId;
                insertZoneRevision.RevisionNote = "URL Structure Bulk Update";

                if (getLastZoneRevision.RevisionStatus == "L")
                {
                    getLastZoneRevision.RevisionStatus = "E";
                    insertZoneRevision.RevisionStatus = "L";
                }

                dbContext.Entry<ZoneRevision>(getLastZoneRevision).State = System.Data.Entity.EntityState.Modified;
                dbContext.ZoneRevisions.Add(insertZoneRevision);
                dbContext.SaveChanges();

            }


            // Article Alias Update
            List<vArticlesZonesFull> listExistArticle = new List<vArticlesZonesFull>();
            List<vArticlesZonesFull> listArticle = new List<vArticlesZonesFull>();
            listArticle = dbContext.vArticlesZonesFulls.Where(s => (string.IsNullOrEmpty(s.ArticleZoneAlias) || !s.IsAliasProtected) && listZoneId.Contains(s.ZoneID) && s.Status != 2).ToList();

            foreach (vArticlesZonesFull vArticleZone in listArticle)
            {

                string alias = "", headlineAlias = "";

                headlineAlias = CmsHelper.StringToAlphaNumeric(vArticleZone.Headline, false);

                alias = structureType.ToLower();
                alias = alias.Replace("##lang##", vArticleZone.LanguageAlias);
                alias = alias.Replace("##site##", vArticleZone.SiteAlias);
                alias = alias.Replace("##zonegroup##", vArticleZone.ZoneGroupAlias);
                alias = alias.Replace("##zone##", vArticleZone.ZoneAlias);
                alias = alias.Replace("##article##", headlineAlias);


                if (!string.IsNullOrEmpty(prefixAlias))
                {
                    alias = prefixAlias + "/" + alias;
                }


                bool isExistAlias = false, isAliasChanged = false;
                int i = 2;
                isExistAlias = dbContext.vArticlesZonesFulls.Where(s => s.ArticleZoneAlias == alias).ToList().Count() > 0 ? true : false;
                while (isExistAlias)
                {
                    isAliasChanged = true;
                    alias = alias + "-" + i;
                    isExistAlias = dbContext.vArticlesZonesFulls.Where(s => s.ArticleZoneAlias == alias).ToList().Count() > 0 ? true : false;
                }

                if (isAliasChanged)
                {
                    listExistArticle.Add(vArticleZone);
                }


                // Insert Article Revision
                ArticleRevision insertArticleRev = new ArticleRevision();
                ArticleRevision getLastArticleRev = new ArticleRevision();

                ArticleDbContext articleDbContext = new ArticleDbContext();
                long lastArticleRevId = articleDbContext.SelectArticleLastRevision(vArticleZone.ArticleID).FirstOrDefault();
                long insertedArticleRevId = 0;

                getLastArticleRev = dbContext.ArticleRevisions.Where(s => s.RevisionId == lastArticleRevId).FirstOrDefault();
                insertArticleRev = getLastArticleRev;
                //insertArticleRev.RevisionId = null;
                insertArticleRev.RevisedBy = currentUserId;
                insertArticleRev.CreatedBy = currentUserId;
                insertArticleRev.RevisionDate = DateTime.Now;
                insertArticleRev.Created = DateTime.Now;
                insertArticleRev.RevisionNote = "URL Structure Bulk Update";

                if (getLastArticleRev.RevisionStatus == "L")
                {
                    getLastArticleRev.RevisionStatus = "E";
                    insertArticleRev.RevisionStatus = "L";
                }

                dbContext.Entry<ArticleRevision>(getLastArticleRev).State = System.Data.Entity.EntityState.Modified;
                insertedArticleRevId = dbContext.ArticleRevisions.Add(insertArticleRev).RevisionId;
                dbContext.SaveChanges();


                // Insert Article Zone Revision
                ArticleZoneRevision insertArticleZoneRev = new ArticleZoneRevision();

                insertArticleZoneRev.ArticleID = vArticleZone.ArticleID;
                insertArticleZoneRev.AzAlias = alias;
                insertArticleZoneRev.RevID = insertedArticleRevId;
                insertArticleZoneRev.ZoneID = vArticleZone.ZoneID;

                dbContext.ArticleZoneRevisions.Add(insertArticleZoneRev);
                dbContext.SaveChanges();


                // Delete Article Zone 
                ArticleZone deleteArticleZone = new ArticleZone();
                deleteArticleZone = dbContext.ArticleZones.Where(s => s.ArticleID == vArticleZone.ArticleID && s.ZoneID == vArticleZone.ZoneID).FirstOrDefault();

                if (deleteArticleZone != null)
                {
                    dbContext.ArticleZones.Attach(deleteArticleZone);
                    dbContext.ArticleZones.Remove(deleteArticleZone);
                    dbContext.SaveChanges();
                }

                // Insert Article Zone

                ArticleZone insertArticleZone = new ArticleZone();
                insertArticleZone.ArticleID = vArticleZone.ArticleID;
                insertArticleZone.ZoneID = vArticleZone.ZoneID;
                insertArticleZone.AzAlias = alias;
                insertArticleZone.IsAliasProtected = false;

                dbContext.ArticleZones.Add(insertArticleZone);
                dbContext.SaveChanges();

            }


            // Çakışanları Göster 
            result = listExistArticle.ToList();
            return View(result);
        }


    }
}
