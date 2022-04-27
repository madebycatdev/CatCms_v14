using EuroCMS.Admin.Common;
using EuroCMS.Core;
using EuroCMS.Management;
using EuroCMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class TagController : BaseController
    {
        CmsDbContext dbContext = new CmsDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "Tag")]
        public ActionResult Index(int? SiteId)
        {
            SiteId = SiteId ?? Convert.ToInt32(Session["CurrentSiteID"]);

            ViewBag.Sites = Bags.GetSites();
            ViewData["SiteID"] = SiteId;
            var datas = new List<Tag>();
            datas = dbContext.Tags.ToList();
            if (SiteId > 0)
            {
                datas = datas.Where(w => w.SiteID == SiteId).ToList();
            }
            return View(datas);
        }

        [CmsAuthorize(Roles = "Administrator", Permission = "Create", ContentType = "Tag")]
        public ActionResult Create(int? SiteId)
        {
            TempData.Clear();

            SiteId = SiteId ?? Convert.ToInt32(Session["CurrentSiteID"]);
            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            Tag tag = new Tag();

            return View(tag);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator", Permission = "Create", ContentType = "Tag", ContentIdParam = "site_id")]
        public ActionResult Create(FormCollection collection, string ReturnUrl)
        {
            TempData.Clear();

            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.TAG_CREATE, this));

            Tag tag = new Tag();

            if (ModelState.IsValid)
            {
                try
                {
                    string oldName = tag.Text;
                    tag.Text = collection["tag_name"] ?? "";
                    tag.SiteID = Convert.ToInt32(collection["site_id"]);
                    tag.PublisherID = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    tag.AddedDate = DateTime.Now;
                    tag.Counter = 0;
                    tag.IsActive = true;
                    #region Alias
                    string alias = collection["tag_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["tag_name"], "-1",tag.SiteID.ToString());
                    }

                    tag.Alias = alias;
                    #endregion

                    if (tag.SiteID == 0)
                        throw new ApplicationException("Site required!");

                    if (String.IsNullOrEmpty(tag.Text))
                        throw new ApplicationException("Tag Name required!");

                    if (String.IsNullOrEmpty(tag.Alias))
                        throw new ApplicationException("Alias required!");

                    var result = dbContext.Tags.FirstOrDefault(f => f.Text == tag.Text); ;

                    if (result == null || result.ID == tag.ID)
                    {
                        dbContext.Tags.Add(tag);
                        dbContext.SaveChanges();



                        TempData["Message"] = "Your Tag has been successfully created.";
                    }
                    else
                        throw new ApplicationException("This Tag name is already used. Please choose another one.");

                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    else
                        return RedirectToAction("Index", new { SiteID = tag.SiteID });
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;

                    ModelState.AddModelError("HATA", ex.Message);

                    return View(tag);
                }
            }

            return View(tag);
        }

        [CmsAuthorize(Roles = "Administrator", Permission = "Edit", ContentType = "Tag")]
        public ActionResult Edit(int id)
        {
            TempData.Clear();

            ViewBag.Sites = Bags.GetSites();
            ViewBag.Layouts = Bags.GetLayouts();

            var result = dbContext.Tags.FirstOrDefault(f => f.ID == id);

            if (string.IsNullOrEmpty(result.Alias))
            {
                result.Alias = CheckAlias(result.Text, id.ToString(),result.SiteID.ToString());
                result.Alias = JsonConvert.DeserializeObject(result.Alias).ToString();
            }

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator", Permission = "Edit", ContentType = "Tag")]
        public ActionResult Edit(int id, string ReturnUrl, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.ZONE_GROUP_EDIT, this));

            var tag = dbContext.Tags.FirstOrDefault(f => f.ID == id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (tag == null)
                        throw new Exception("The tag was not found");



                    tag.Text = collection["tag_name"] ?? "";
                    tag.SiteID = Convert.ToInt32(collection["site_id"]);
                    tag.PublisherID = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    tag.AddedDate = DateTime.Now;
                    tag.Counter = 0;
                    tag.IsActive = true;
                    #region Alias
                    string alias = collection["tag_alias"];
                    if (string.IsNullOrEmpty(alias))
                    {
                        alias = CheckAlias(collection["tag_name"], tag.ID.ToString(), tag.SiteID.ToString());
                    }

                    tag.Alias = alias;
                    #endregion

                    if (tag.SiteID == 0)
                        throw new ApplicationException("Site required!");

                    if (String.IsNullOrEmpty(tag.Text))
                        throw new ApplicationException("Tag Name required!");

                    if (String.IsNullOrEmpty(tag.Alias))
                        throw new ApplicationException("Alias required!");

                    var result = dbContext.Tags.FirstOrDefault(f => f.Text == tag.Text); ;

                    if (result == null || result.ID == tag.ID)
                    {
                        dbContext.Entry(tag).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        TempData["Message"] = "Your Tag has been successfully updated.";
                    }
                    else
                        throw new ApplicationException("This Tag name is already used. Please choose another one.");


                    UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    else
                        return RedirectToAction("Index", new { SiteID = tag.SiteID });



                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    TempData["HasError"] = true;
                    TempData["Message"] = ex.Message;

                    ModelState.AddModelError("HATA", ex.Message);

                    return View(tag);
                }
            }

            return View(tag);
        }

        public ActionResult UpdateRecords()
        {
            var tags = dbContext.Tags.ToList();
            var articles = dbContext.Articles.Where(w => !string.IsNullOrEmpty(w.TagIds)).ToList();
            foreach (var article in articles)
            {
                var articleTagIds = article.TagIds.Split(',').ToList();
                var articleTagContents = tags.Where(w => articleTagIds.Contains(w.ID.ToString())).Select(s => s.Text).ToArray();

                article.TagContents = string.Join(",", articleTagContents);
                dbContext.Entry(article).State = EntityState.Modified;
                dbContext.SaveChanges();


                var articleRevision = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id && f.RevisionStatus == "L");
                if (articleRevision != null)
                {
                    articleRevision.TagContents = string.Join(",", articleTagContents);
                    dbContext.Entry(articleRevision).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            TempData["Message"] = "Articles has been successfully updated.";
            return RedirectToAction("Index", "Tag");
        }


        [CmsAuthorize(Roles = "Administrator", ContentType = "Tag")]
        public ActionResult ChangeStatus(int id)
        {
            TempData.Clear();
            try
            {
                var result = dbContext.Tags.FirstOrDefault(f => f.ID == id);
                if (result.IsActive)
                {
                    result.IsActive = false;
                    dbContext.Entry(result).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    TempData["Message"] = "Your Tag has been successfully updated.";
                }
                else if (!result.IsActive)
                {
                    result.IsActive = true;
                    dbContext.Entry(result).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    TempData["Message"] = "Your Tag has been successfully updated.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error : " + ex.Message;
            }
            return RedirectToAction("Index", "Tag");
        }


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
                    CmsDbContext dbContext = new CmsDbContext();
                    int currentParentId = Convert.ToInt32(parentId);
                    List<Tag> tags = dbContext.Tags.Where(x => x.Alias == cleanText && x.SiteID == currentParentId).ToList();

                    int currentId = Convert.ToInt32(id);
                    tags.Remove(tags.Where(x => x.ID == currentId).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                    if (tags == null || tags.Count == 0)
                    {
                        //ok 
                        result = cleanText;
                    }
                    else
                    {
                        //çakışma var
                        int counter = 2;
                        while (dbContext.Tags.Where(x => x.Alias == cleanText + "-" + counter && x.SiteID == currentParentId).ToList().Count > 0)
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
                CmsHelper.SaveErrorLog(ex, "Can't create tag alias", true);
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
