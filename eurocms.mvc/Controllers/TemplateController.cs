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
    public class TemplateController : BaseController
    {
        LayoutDbContext context = new LayoutDbContext();

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "Template")]
        public ActionResult Index(int? GroupId)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Layout, GroupId);
            var result = context.SelectLayouts(GroupId);
            return View(result);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "View", ContentType = "Template")]
        public ActionResult Revisions(int id)
        {
            var result = context.SelectLayoutRevisions(id);
            return View(result);
        }
 
        [HttpGet]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Template")]
        public ActionResult Create()
        {
            TempData.Clear();
            
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Layout, null);

            cms_templates layout = new cms_templates();

            return View(layout);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Create", ContentType = "Template")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LAYOUT_CREATE, this));

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Layout, null);

            cms_templates layout = new cms_templates();

            try
            {
                layout.template_name = HttpUtility.HtmlEncode(collection["template_name"]);
                layout.template_html = HttpUtility.HtmlDecode(collection["editor_html"]);
                layout.template_type = (byte)Convert.ToInt32(collection["template_type"]);
                // layout.publisher_id =   Convert.ToInt32(Session["publisher_id"]) ;
                layout.publisher_id = Membership.GetUser().ProviderUserKey;
                layout.structure_description = HttpUtility.HtmlEncode(collection["structure_description"]);
                layout.content_1_editor_type = HttpUtility.HtmlEncode(collection["content_1_editor_type"]);
                layout.template_doctype = HttpUtility.HtmlEncode(collection["template_doctype"]);
                layout.group_id = string.IsNullOrEmpty(collection["group_id"]) ? 0 : Convert.ToInt32(collection["group_id"]);

                if (String.IsNullOrEmpty(collection["template_name"]))
                    throw new ApplicationException("Template name required!");

                var result = context.CreateLayout(layout);

                switch (result[0].tStat)
                {
                    case "D":
                        throw new ApplicationException("This template name is already used. Please choose another one.");
                    case "U":
                        TempData["Message"] = "Your template has been successfully updated.";
                        break;
                    default:
                        TempData["Message"] = "Your template has been successfully created.";
                        break;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                ModelState.AddModelError("HATA", ex.Message);
            }

            return View(layout);
        }

        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Template")]
        public ActionResult Edit(int id, long? RevisionId)
        {
            TempData.Clear();

            List<TemplateRevision> listRevisions = new List<TemplateRevision>();
            CmsDbContext dbContext = new CmsDbContext();
            listRevisions = dbContext.TemplateRevisions.Where(t=>t.TemplateID == id).OrderByDescending(od=>od.Created).ToList();
            

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Layout, null);
            ViewBag.Revisions = listRevisions;


            if (RevisionId != null)
                return View( context.SelectLayoutRevision(RevisionId ?? 0).FirstOrDefault() );
            else
                return View( context.SelectLayout(id) );
        }
  
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Edit", ContentType = "Template")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LAYOUT_EDIT, this));

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Layout, null);

            cms_templates layout = new cms_templates();

            if (ModelState.IsValid)
            {
                try
                {
                    layout.template_id = id;
                    layout.template_name = HttpUtility.HtmlEncode(collection["template_name"]);
                    layout.template_html = HttpUtility.HtmlDecode(collection["editor_html"]);
                    layout.template_type = (byte)Convert.ToInt32(collection["template_type"]);
                    layout.publisher_id = Membership.GetUser().ProviderUserKey;
                    layout.structure_description = HttpUtility.HtmlEncode(collection["structure_description"]);
                    layout.content_1_editor_type = HttpUtility.HtmlEncode(collection["content_1_editor_type"]);
                    layout.template_doctype = HttpUtility.HtmlEncode(collection["template_doctype"]);
                    layout.group_id = string.IsNullOrEmpty(collection["group_id"]) ? 0 : Convert.ToInt32(collection["group_id"]);

                    if (String.IsNullOrEmpty(collection["template_name"]))
                        throw new ApplicationException("Template name required!");

                    var result = context.UpdateLayout(layout);

                    switch (result[0].tStat)
                    {
                        case "D":
                            throw new ApplicationException("This template name is already used. Please choose another one.");
                        case "U":
                            TempData["Message"] = "Your template has been successfully updated.";
                            break;
                        default:
                            TempData["Message"] = "Your template has been successfully created.";
                            break;
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                    ModelState.AddModelError("HATA", ex.Message);
                }
            }

            return View(layout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser,ContentManager,ContentEntry,UserCreator", Permission = "Delete", ContentType = "Template")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.LAYOUT_DELETE, this));

            try
            {
                var result = context.DeleteLayout(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));

                switch (result[0].rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted.";
                        break;
                    case "1":
                        throw new ApplicationException("This template was not found OR already deleted before.");
                    case "2":
                        throw new ApplicationException("This template used on following sites. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "3":
                        throw new ApplicationException("This template used on following zone groups. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "4":
                        throw new ApplicationException("This template used on following zones. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    default:
                        throw new ApplicationException("unexpected error!");
                }
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
