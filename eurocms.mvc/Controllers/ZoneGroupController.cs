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


namespace EuroCMS.Admin.Controllers
{
    public class ZoneGroupController : BaseController
    {
        IService<ZoneGroup> _service;

        public ZoneGroupController()
        {
            _service = new ZoneGroupService(new ZoneGroupRepository());
        }

        public ZoneGroupController(ZoneGroupService zoneGroupService)
        {
            _service = zoneGroupService;
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Detail", ContentType = "ZoneGroup")]
        public ActionResult SelectZoneGroup(int? SiteId)
        {
            SiteId = SiteId ?? Convert.ToInt32(Session["CurrentSiteID"]);
            ViewData["SiteID"] = SiteId;

            ViewBag.Sites = Bags.GetSites();
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, null);

            return View();
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "ZoneGroup")]
        public ActionResult Index(int? SiteId)
        {
            SiteId = SiteId ?? Convert.ToInt32(Session["CurrentSiteID"]);

            ViewBag.Sites = Bags.GetSites();
            ViewData["SiteID"] = SiteId;

            return View(_service.GetAllByParentId(SiteId ?? 0));
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "List", ContentType = "ZoneGroup")]
        public JsonResult ListBySiteID(int? SiteId)
        {
            var result = _service.GetAllByParentId(SiteId ?? 0);

            List<ZoneGroupResult> zg = new List<ZoneGroupResult>();

            foreach (var item in result)
            {
                zg.Add(new ZoneGroupResult() { zone_group_id = item.Id.ToString(), zone_group_name = item.Name });
            }

            return Json(zg, JsonRequestBehavior.AllowGet);
        }

        struct ZoneGroupResult
        {
            public string zone_group_id { get; set; }
            public string zone_group_name { get; set; }
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Detail", ContentType = "ZoneGroup")]
        public ActionResult Details(int id)
        {
            return Json(_service.Find(id), JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "ZoneGroup")]
        public ActionResult QuickCreate(int ParentId)
        {
            TempData.Clear();
            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();
            ViewBag.site_id = ParentId;

            return View();
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "ZoneGroup")]
        public ActionResult QuickEdit(int id)
        {
            TempData.Clear();

            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            return View(_service.Find(id));
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "ZoneGroup")]
        public ActionResult Create(int? SiteId)
        {
            TempData.Clear();

            SiteId = SiteId ?? Convert.ToInt32(Session["CurrentSiteID"]);
            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            ZoneGroup zoneGroup = new ZoneGroup();

            return View(zoneGroup);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Site", ContentIdParam = "site_id")]
        public ActionResult Create(FormCollection collection, string ReturnUrl)
        {
            TempData.Clear();

            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_GROUP_CREATE, this));

            ZoneGroup zoneGroup = new ZoneGroup();

            if (ModelState.IsValid)
            {
                try
                {
                    string keywords = collection["zone_group_keywords"] ?? "";
                    if (!string.IsNullOrEmpty(collection["keywords_ao"]) && collection["keywords_ao"] == "O")
                        keywords = "|" + keywords;

                    zoneGroup.Name = collection["zone_group_name"] ?? "";
                    zoneGroup.Keywords = keywords;
                    zoneGroup.Analytics = collection["analytics"] ?? "";
                    zoneGroup.SiteId = Convert.ToInt32(collection["site_id"]);
                    zoneGroup.TemplateId = string.IsNullOrEmpty(collection["template_id"]) ? 0 : Convert.ToInt32(collection["template_id"]);
                    zoneGroup.MobileTemplateId = string.IsNullOrEmpty(collection["template_id_mobile"]) ? 0 : Convert.ToInt32(collection["template_id_mobile"]);
                    zoneGroup.CustomBody = collection["custom_body"] ?? "";
                    zoneGroup.TagArticle = collection["tag_detail_article"] ?? "";
                    zoneGroup.Article1 = collection["article_1"] ?? "";
                    zoneGroup.Article2 = collection["article_2"] ?? "";
                    zoneGroup.Article3 = collection["article_3"] ?? "";
                    zoneGroup.Article4 = collection["article_4"] ?? "";
                    zoneGroup.Article5 = collection["article_5"] ?? "";
                    zoneGroup.Append1 = Convert.ToByte(collection["append_1"]);
                    zoneGroup.Append2 = Convert.ToByte(collection["append_2"]);
                    zoneGroup.Append3 = Convert.ToByte(collection["append_3"]);
                    zoneGroup.Append4 = Convert.ToByte(collection["append_4"]);
                    zoneGroup.Append5 = Convert.ToByte(collection["append_5"]);
                    zoneGroup.CreatedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    zoneGroup.MetaDescription = collection["meta_description"] ?? "";
                    zoneGroup.Content1EditorType = collection["content_1_editor_type"] ?? "H";
                    zoneGroup.Content2EditorType = collection["content_2_editor_type"] ?? "H";
                    zoneGroup.Content3EditorType = collection["content_3_editor_type"] ?? "H";
                    zoneGroup.Content4EditorType = collection["content_4_editor_type"] ?? "H";
                    zoneGroup.Content5EditorType = collection["content_5_editor_type"] ?? "H";
                    zoneGroup.DefaultArticle = collection["default_article"] ?? "";
                    zoneGroup.OmnitureCode = collection["omniture_code"] ?? "";
                    zoneGroup.DisplayName = collection["zone_group_name_display"] ?? "";
                    zoneGroup.BeforeHead = collection["before_head"] ?? "";
                    zoneGroup.BeforeBody = collection["before_body"] ?? "";
                    zoneGroup.Created = DateTime.Now;
                    zoneGroup.Updated = DateTime.Now;

                    //2017-09-18
                    zoneGroup.AfterBody = HttpUtility.HtmlEncode(collection["afterbody"]);
                    //2017-09-18

                    #region Alias
                    string alias = collection["zone_group_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["zone_group_name"], "-1", zoneGroup.SiteId.ToString());
                    }

                    zoneGroup.Alias = alias;
                    #endregion

                    if (zoneGroup.SiteId == 0)
                        throw new ApplicationException("Site required!");

                    if (String.IsNullOrEmpty(zoneGroup.Name))
                        throw new ApplicationException("Zone Group Name required!");

                    if (String.IsNullOrEmpty(zoneGroup.Alias))
                        throw new ApplicationException("Alias required!");

                    var result = _service.FindByName(zoneGroup.Name);

                    if (result == null || result.Id == zoneGroup.Id)
                    {
                        _service.Insert(zoneGroup);
                        TempData["Message"] = "Your ZoneGroup has been successfully created.";
                    }
                    else
                        throw new ApplicationException("This ZoneGroup name is already used. Please choose another one.");

                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    else
                        return RedirectToAction("Index", new { SiteID = zoneGroup.SiteId });
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;

                    ModelState.AddModelError("HATA", ex.Message);

                    return View(zoneGroup);
                }
            }

            return View(zoneGroup);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "ZoneGroup")]
        public ActionResult Edit(int id)
        {
            TempData.Clear();

            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            var result = _service.Find(id);

            ViewBag.default_article = Bags.GetArticleZoneNames(result.DefaultArticle);
            ViewBag.tag_detail_article = Bags.GetArticleZoneNames(result.TagArticle);


            if (string.IsNullOrEmpty(result.Alias))
            {
                result.Alias = CheckAlias(result.Name, id.ToString(),result.SiteId.ToString());
                result.Alias = JsonConvert.DeserializeObject(result.Alias).ToString();
            }

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "ZoneGroup")]
        public ActionResult Edit(int id, string ReturnUrl, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_GROUP_EDIT, this));

            var zoneGroup = _service.Find(id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (zoneGroup == null)
                        throw new Exception("The ZoneGroup was not found");

                    string keywords = collection["zone_group_keywords"] ?? "";
                    if (!string.IsNullOrEmpty(collection["keywords_ao"]) && collection["keywords_ao"] == "O")
                        keywords = "|" + keywords;

                    zoneGroup.Name = HttpUtility.HtmlEncode(collection["zone_group_name"]) ?? "";
                    zoneGroup.Keywords = keywords;
                    zoneGroup.Analytics = HttpUtility.HtmlEncode(collection["analytics"]) ?? "";
                    zoneGroup.SiteId = Convert.ToInt32(collection["site_id"]);
                    zoneGroup.TemplateId = string.IsNullOrEmpty(collection["template_id"]) ? 0 : Convert.ToInt32(collection["template_id"]);
                    zoneGroup.MobileTemplateId = string.IsNullOrEmpty(collection["template_id_mobile"]) ? 0 : Convert.ToInt32(collection["template_id_mobile"]);
                    zoneGroup.CustomBody = HttpUtility.HtmlEncode(collection["custom_body"]) ?? "";
                    zoneGroup.TagArticle = HttpUtility.HtmlEncode(collection["tag_detail_article"]) ?? "";
                    zoneGroup.Article1 = HttpUtility.HtmlEncode(collection["article_1"]) ?? "";
                    zoneGroup.Article2 = HttpUtility.HtmlEncode(collection["article_2"]) ?? "";
                    zoneGroup.Article3 = HttpUtility.HtmlEncode(collection["article_3"]) ?? "";
                    zoneGroup.Article4 = HttpUtility.HtmlEncode(collection["article_4"]) ?? "";
                    zoneGroup.Article5 = HttpUtility.HtmlEncode(collection["article_5"]) ?? "";
                    zoneGroup.Append1 = Convert.ToByte(collection["append_1"]);
                    zoneGroup.Append2 = Convert.ToByte(collection["append_2"]);
                    zoneGroup.Append3 = Convert.ToByte(collection["append_3"]);
                    zoneGroup.Append4 = Convert.ToByte(collection["append_4"]);
                    zoneGroup.Append5 = Convert.ToByte(collection["append_5"]);
                    zoneGroup.CreatedBy = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    zoneGroup.MetaDescription = HttpUtility.HtmlEncode(collection["meta_description"]) ?? "";
                    zoneGroup.Content1EditorType = HttpUtility.HtmlEncode(collection["content_1_editor_type"]) ?? "H";
                    zoneGroup.Content2EditorType = HttpUtility.HtmlEncode(collection["content_2_editor_type"]) ?? "H";
                    zoneGroup.Content3EditorType = HttpUtility.HtmlEncode(collection["content_3_editor_type"]) ?? "H";
                    zoneGroup.Content4EditorType = HttpUtility.HtmlEncode(collection["content_4_editor_type"]) ?? "H";
                    zoneGroup.Content5EditorType = HttpUtility.HtmlEncode(collection["content_5_editor_type"]) ?? "H";
                    zoneGroup.DefaultArticle = HttpUtility.HtmlEncode(collection["default_article"]) ?? "";
                    zoneGroup.OmnitureCode = HttpUtility.HtmlEncode(collection["omniture_code"]) ?? "";
                    zoneGroup.DisplayName = HttpUtility.HtmlEncode(collection["zone_group_name_display"]) ?? "";
                    zoneGroup.BeforeBody = HttpUtility.HtmlEncode(collection["before_body"]) ?? "";
                    zoneGroup.BeforeHead = HttpUtility.HtmlEncode(collection["before_head"]) ?? "";
                    zoneGroup.Updated = DateTime.Now;

                    //2017-09-18
                    zoneGroup.AfterBody = HttpUtility.HtmlEncode(collection["afterbody"]);
                    //2017-09-18

                    #region Alias
                    string alias = collection["zone_group_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["zone_group_name"], id.ToString(), zoneGroup.SiteId.ToString());
                    }

                    zoneGroup.Alias = alias;
                    #endregion

                    if (zoneGroup.SiteId == 0)
                        throw new ApplicationException("Site required!");

                    if (String.IsNullOrEmpty(zoneGroup.Name))
                        throw new ApplicationException("Zone Group Name required!");

                    if (String.IsNullOrEmpty(zoneGroup.Alias))
                        throw new ApplicationException("Alias required!");

                    var result = _service.FindByName(zoneGroup.Name);

                    if (result == null || result.Id == zoneGroup.Id)
                    {
                        _service.Update(zoneGroup);
                        TempData["Message"] = "Your ZoneGroup has been successfully updated.";
                    }
                    else
                        throw new ApplicationException("This ZoneGroup name is already used. Please choose another one.");

                    //if (!string.IsNullOrEmpty(ReturnUrl))
                    //    return Redirect(ReturnUrl);
                    //else
                    //    return RedirectToAction("Index", new { SiteID = zoneGroup.SiteId });

                    UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
                    if (!string.IsNullOrEmpty(ReturnUrl) && urlHelper.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", new { SiteID = zoneGroup.SiteId });
                    }


                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;

                    ModelState.AddModelError("HATA", ex.Message);

                    return View(zoneGroup);
                }
            }

            return View(zoneGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Delete", ContentType = "ZoneGroup")]
        public ActionResult Delete(int id, string ReturnUrl)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_GROUP_DELETE, this));

            ZoneRepository zr = new ZoneRepository();
            ZoneService _zoneService = new ZoneService(zr);

            List<Zone> listZone = new List<Zone>();


            try
            {
                var zoneGroup = _service.Find(id);

                if (zoneGroup == null)
                    throw new Exception("The ZoneGroup was not found");

                listZone = _zoneService.GetAllByParentId(zoneGroup.Id).ToList();
                if (listZone.Count > 1) //if ( zoneGroup.Zones.Count > 1 )
                    throw new Exception("The ZoneGroup has one or more Zone");

                _service.Delete(zoneGroup);

                TempData["Message"] = "Successfully Deleted.";

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
                return RedirectToActionSite("Index");
        }

        [CmsAuthorize()]
        public string CheckAlias(string text, string id,string parentId)
        {
            string result = string.Empty;
            text = text.Trim();

            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    string cleanText = CmsHelper.StringToAlphaNumeric(text, false);
                    CmsDbContext dbContext = new CmsDbContext();
                    int currentParentId = Convert.ToInt32(parentId);
                    List<ZoneGroup> zgs = dbContext.ZoneGroups.Where(x => x.Alias == cleanText && x.SiteId == currentParentId).ToList();

                    int currentId = Convert.ToInt32(id);
                    zgs.Remove(zgs.Where(x => x.Id == currentId).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                    if (zgs == null || zgs.Count == 0)
                    {
                        //ok 
                        result = cleanText;
                    }
                    else
                    {
                        //çakışma var
                        int counter = 2;
                        while (dbContext.ZoneGroups.Where(x => x.Alias == cleanText + "-" + counter && x.SiteId == currentParentId).ToList().Count > 0)
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
                CmsHelper.SaveErrorLog(ex, "Can't create zone group alias", true);
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
