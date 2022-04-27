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
using EuroCMS.Model;
using Newtonsoft.Json;


namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator")]
    public class SiteController : BaseController
    {
        IService<Site> _service;

        public SiteController()
        {
            _service = new SiteService(new SiteRepository());
        }

        public SiteController(SiteService siteService)
        {
            _service = siteService;
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "Site")]
        public ActionResult Index(int? GroupId)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Site, GroupId);

            return View(_service.GetAllByGroupId(GroupId ?? 0));
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "List", ContentType = "Site")]
        public JsonResult ListByGroupID(int? GroupId)
        {
            //var result = context.SelectSites(GroupId);

            return Json(_service.GetAllByGroupId(GroupId ?? 0), JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Detail", ContentType = "Site")]
        public ActionResult Details(int id)
        {
            return Json(_service.Find(id), JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Site")]
        public ActionResult Create()
        {

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Site, null);
            ViewBag.Domains = Bags.GetDomains();
            ViewBag.Layouts = Bags.GetLayouts();

            Site site = new Site();

            return View(site);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Site")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITE_CREATE, this));

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Site, null);
            ViewBag.Domains = Bags.GetDomains();
            ViewBag.Layouts = Bags.GetLayouts();

            Site site = new Site();

            if (ModelState.IsValid)
            {
                try
                {
                    site.Name = HttpUtility.HtmlEncode(collection["site_name"]);
                    site.TemplateId = string.IsNullOrEmpty(collection["template_id"]) ? 0 : Convert.ToInt32(collection["template_id"]);
                    site.MobileTemplateId = string.IsNullOrEmpty(collection["template_id_mobile"]) ? 0 : Convert.ToInt32(collection["template_id_mobile"]);
                    site.Keywords = HttpUtility.HtmlEncode(collection["site_keywords"]);
                    site.MetaDescription = HttpUtility.HtmlEncode(collection["meta_description"]);
                    site.Header = HttpUtility.HtmlEncode(collection["site_header"]);
                    site.JS = HttpUtility.HtmlEncode(collection["site_js"]);
                    site.Analytics = HttpUtility.HtmlEncode(collection["analytics"]);
                    site.CustomBody = HttpUtility.HtmlEncode(collection["custom_body"]);
                    site.Icon = HttpUtility.HtmlEncode(collection["site_icon"]);
                    site.TagArticle = HttpUtility.HtmlEncode(collection["tag_detail_article"]) ?? "";
                    site.Article1 = HttpUtility.HtmlEncode(collection["article_1"]) ?? "";
                    site.Article2 = HttpUtility.HtmlEncode(collection["article_2"]) ?? "";
                    site.Article3 = HttpUtility.HtmlEncode(collection["article_3"]) ?? "";
                    site.Article4 = HttpUtility.HtmlEncode(collection["article_4"]) ?? "";
                    site.Article5 = HttpUtility.HtmlEncode(collection["article_5"]) ?? "";
                    site.CreatedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    site.StructureDescription = HttpUtility.HtmlEncode(collection["structure_description"]) ?? "";
                    site.Content1EditorType = HttpUtility.HtmlEncode(collection["content_1_editor_type"]) ?? "";
                    site.Content2EditorType = HttpUtility.HtmlEncode(collection["content_2_editor_type"]) ?? "";
                    site.Content3EditorType = HttpUtility.HtmlEncode(collection["content_3_editor_type"]) ?? "";
                    site.Content4EditorType = HttpUtility.HtmlEncode(collection["content_4_editor_type"]) ?? "";
                    site.Content5EditorType = HttpUtility.HtmlEncode(collection["content_5_editor_type"]) ?? "";
                    site.DefaultArticle = collection["default_article"] ?? "";
                    site.OmnitureCode = collection["omniture_code"];
                    site.DomainId = string.IsNullOrEmpty(collection["DomainId"]) ? 0 : Convert.ToInt32(collection["DomainId"]);
                    site.GroupId = string.IsNullOrEmpty(collection["GroupId"]) ? 0 : Convert.ToInt32(collection["GroupId"]);
                    site.Created = DateTime.Now;
                    site.Updated = DateTime.Now;
                    site.FilePath = HttpUtility.HtmlEncode(collection["file_path"]);

                    //2017-09-18
                    site.AfterBody = HttpUtility.HtmlEncode(collection["afterbody"]);
                    site.SitePrefix = HttpUtility.HtmlEncode(collection["siteprefix"]);
                    site.SiteSuffix = HttpUtility.HtmlEncode(collection["sitesuffix"]);
                    //2017-09-18

                    #region Alias
                    string alias = collection["site_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["site_name"], "-1","");
                    }

                    site.Alias = alias;
                    #endregion

                    if (String.IsNullOrEmpty(site.Name))
                        throw new ApplicationException("Site name required!");

                    if (site.DomainId == 0)
                        throw new ApplicationException("Domain required!");

                    if (site.TemplateId == 0)
                        throw new ApplicationException("Template required!");

                    if (string.IsNullOrEmpty(site.Alias))
                        throw new ApplicationException("Alias required!");

                    var result = _service.FindByName(site.Name);

                    if (result == null)
                    {
                        _service.Insert(site);
                        TempData["Message"] = "Your Site has been successfully created.";
                    }
                    else
                        throw new ApplicationException("This site name is already used. Please choose another one.");

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    ModelState.AddModelError("HATA", ex.Message);

                    return View(site);
                }
            }

            return View(site);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Site")]
        public ActionResult Edit(int id)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Site, null);
            ViewBag.Domains = Bags.GetDomains();
            ViewBag.Layouts = Bags.GetLayouts();

            CmsDbContext dbContext = new CmsDbContext();

            var result = dbContext.Sites.Where(s => s.Id == id).FirstOrDefault(); //_service.Find(id);

            ViewBag.default_article = Bags.GetArticleZoneNames(result.DefaultArticle);
            ViewBag.tag_detail_article = Bags.GetArticleZoneNames(result.TagArticle);

            if (string.IsNullOrEmpty(result.Alias))
            {
                result.Alias = CheckAlias(result.Name, id.ToString(),result.DomainId.ToString());
                result.Alias = JsonConvert.DeserializeObject(result.Alias).ToString();
            }

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Site")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITE_EDIT, this));

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Site, null);
            ViewBag.Domains = Bags.GetDomains();
            ViewBag.Layouts = Bags.GetLayouts();

            //cms_asp_select_site_details_Result site = new cms_asp_select_site_details_Result();

            var site = _service.Find(id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (site == null)
                        throw new Exception("The site was not found");

                    site.Name = HttpUtility.HtmlEncode(collection["site_name"]);
                    site.TemplateId = string.IsNullOrEmpty(collection["template_id"]) ? 0 : Convert.ToInt32(collection["template_id"]);
                    site.MobileTemplateId = string.IsNullOrEmpty(collection["template_id_mobile"]) ? 0 : Convert.ToInt32(collection["template_id_mobile"]);
                    site.Keywords = HttpUtility.HtmlEncode(collection["site_keywords"]);
                    site.MetaDescription = HttpUtility.HtmlEncode(collection["meta_description"]);
                    site.Header = HttpUtility.HtmlEncode(collection["site_header"]);
                    site.JS = HttpUtility.HtmlEncode(collection["site_js"]);
                    site.Analytics = HttpUtility.HtmlEncode(collection["analytics"]);
                    site.CustomBody = HttpUtility.HtmlEncode(collection["custom_body"]);
                    site.Icon = HttpUtility.HtmlEncode(collection["site_icon"]);
                    site.TagArticle = HttpUtility.HtmlEncode(collection["tag_detail_article"]) ?? "";
                    site.Article1 = HttpUtility.HtmlEncode(collection["article_1"]) ?? "";
                    site.Article2 = HttpUtility.HtmlEncode(collection["article_2"]) ?? "";
                    site.Article3 = HttpUtility.HtmlEncode(collection["article_3"]) ?? "";
                    site.Article4 = HttpUtility.HtmlEncode(collection["article_4"]) ?? "";
                    site.Article5 = HttpUtility.HtmlEncode(collection["article_5"]) ?? "";
                    site.CreatedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                    site.StructureDescription = HttpUtility.HtmlEncode(collection["structure_description"]) ?? "";
                    site.Content1EditorType = HttpUtility.HtmlEncode(collection["content_1_editor_type"]) ?? "";
                    site.Content2EditorType = HttpUtility.HtmlEncode(collection["content_2_editor_type"]) ?? "";
                    site.Content3EditorType = HttpUtility.HtmlEncode(collection["content_3_editor_type"]) ?? "";
                    site.Content4EditorType = HttpUtility.HtmlEncode(collection["content_4_editor_type"]) ?? "";
                    site.Content5EditorType = HttpUtility.HtmlEncode(collection["content_5_editor_type"]) ?? "";
                    site.DefaultArticle = HttpUtility.HtmlEncode(collection["default_article"]) ?? "";
                    site.OmnitureCode = HttpUtility.HtmlEncode(collection["omniture_code"]);
                    site.DomainId = string.IsNullOrEmpty(collection["DomainId"]) ? 0 : Convert.ToInt32(collection["DomainId"]);
                    site.GroupId = string.IsNullOrEmpty(collection["GroupId"]) ? 0 : Convert.ToInt32(collection["GroupId"]);
                    site.Updated = DateTime.Now;
                    site.FilePath = HttpUtility.HtmlEncode(collection["file_path"]);

                    //2017-09-18
                    site.AfterBody = HttpUtility.HtmlEncode(collection["afterbody"]);
                    site.SitePrefix = HttpUtility.HtmlEncode(collection["siteprefix"]);
                    site.SiteSuffix = HttpUtility.HtmlEncode(collection["sitesuffix"]);
                    //2017-09-18

                    #region Alias
                    string alias = collection["site_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["site_name"],id.ToString(),"");
                    }

                    site.Alias = alias;
                    #endregion

                    if (String.IsNullOrEmpty(site.Name))
                        throw new ApplicationException("Site name required!");

                    if (site.DomainId == 0)
                        throw new ApplicationException("Domain required!");

                    if (site.TemplateId == 0)
                        throw new ApplicationException("Template required!");

                    if (string.IsNullOrEmpty(site.Alias))
                        throw new ApplicationException("Alias required!");

                    var result = _service.FindByName(site.Name);

                    if (result == null || result.Id == site.Id)
                    {
                        _service.Update(site);
                        TempData["Message"] = "Your Site has been successfully updated.";
                    }
                    else
                        throw new ApplicationException("This site name is already used. Please choose another one.");

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    ModelState.AddModelError("HATA", ex.Message);

                    return View(site);
                }
            }

            return View(site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Delete", ContentType = "Site")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SITE_DELETE, this));

            try
            {
                var site = _service.Find(id);

                if (site == null)
                    throw new Exception("The site was not found");

                if (site.ZoneGroups.Count > 1)
                    throw new Exception("The site has one or more ZoneGroup");

                _service.Delete(site);

                TempData["Message"] = "Successfully Deleted.";

            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [CmsAuthorize()]
        public string CheckAlias(string text,string id,string parentId)
        {
            string result = string.Empty;
            text = text.Trim();

            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    string cleanText = CmsHelper.StringToAlphaNumeric(text, false);
                    int currentParentId = Convert.ToInt32(parentId);
                    CmsDbContext dbContext = new CmsDbContext();
                    List<Site> sites = dbContext.Sites.Where(x => x.Alias == cleanText && x.DomainId == currentParentId).ToList();

                    int currentId = Convert.ToInt32(id);
                    sites.Remove(sites.Where(x => x.Id == currentId).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                    if (sites == null || sites.Count == 0)
                    {
                        //ok 
                        result = cleanText;
                    }
                    else
                    {
                        //çakışma var
                        int counter = 2;
                        while (dbContext.Sites.Where(x => x.Alias == cleanText + "-" + counter && x.DomainId == currentParentId).ToList().Count > 0)
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
                CmsHelper.SaveErrorLog(ex, "Can't create site alias", true);
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
