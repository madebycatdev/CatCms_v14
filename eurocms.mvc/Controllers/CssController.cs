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

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator,PowerUser")]
    public class CssController : BaseController
    {
        CssDbContext context = new CssDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "Css")]
        public ActionResult Index(int? GroupId)
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Css, GroupId);

            var result = context.SelectCssList(GroupId);

            return View(result);
        }
 
        [CmsAuthorize(Permission = "List", ContentType = "Css")]
        public ActionResult Revisions(int id)
        {
            var result = context.SelectCssRevisions(id);

            return View(result);
        }
  
        [CmsAuthorize(Permission = "Create", ContentType = "Css")]
        public ActionResult Create()
        {
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Css, null);

            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "Css")]
        public ActionResult Create(FormCollection collection)
        {
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CSS_CREATE, this));

            try
            {
                UpdateCssCode(-1, collection);

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
 
        [CmsAuthorize(Permission = "Edit", ContentType = "Css")]
        public ActionResult Edit(int id, long? RevisionId)
        {
            TempData.Clear();

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Css, null);

            if (RevisionId != null)
                return View(context.SelectCssHistoryCode(RevisionId ?? 0).FirstOrDefault());
            else
                return View(context.SelectCssCode(id).FirstOrDefault());
        }
 
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "Css")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CSS_EDIT, this));

            try
            {
                UpdateCssCode(id, collection);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;    
            }

            return RedirectToAction("Index");
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser", Permission = "Delete", ContentType = "Css")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CSS_DELETE, this));

            try
            {
                var result = context.DeleteCssCode(id,
                        Membership.GetUser().ProviderUserKey,
                        Convert.ToInt32(Session["publisher_level"]));

                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "1":
                        throw new ApplicationException("This CSS was not found OR already deleted before.");
                    case "2":
                        throw new ApplicationException("This CSS used on following sites. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "3":
                        throw new ApplicationException("This CSS used on following zone groups. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "4":
                        throw new ApplicationException("This CSS used on following zones. You need to delete this relations first.\nCSS NOT deleted." + result.Select(m => m.found_name).InHtmlNewLine());
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

            return RedirectToAction("Index");
        }

        private void UpdateCssCode(int id, FormCollection collection)
        {
            string css_name = collection["css_name"] ?? string.Empty;
            
            if (string.IsNullOrEmpty(css_name))
                throw new ApplicationException("Saving CSS code. CSS name or code empty!");

            string group_id = collection["group_id"] ?? "0";
            if (string.IsNullOrEmpty(group_id))
                group_id = "0";

            cms_css css = new cms_css();
            css.css_id = id;
            css.css_name = css_name;
            css.css_code = collection["css_code"] ?? string.Empty;
            css.css_fix = collection["css_fix"] ?? string.Empty;
            css.css_rel_text = collection["css_rel_text"] ?? "stylesheet";
            css.css_type_text = collection["css_type_text"] ?? "text/css";
            css.group_id = Convert.ToInt32(group_id);
            css.structure_description = collection["structure_description"] ?? string.Empty;
            css.css_type = Convert.ToInt32(collection["css_type"] ?? "0");
           // css.publisher_id = Convert.ToInt32(Session["publisher_id"] ?? "0");
            css.publisher_id = Membership.GetUser().ProviderUserKey;
            
            var result = context.UpdateCssCode(css).FirstOrDefault();

            switch (result.cStat)
            {
                 
                case "D":
                    throw new ApplicationException("This css name is already used. Please choose another one.");
                case "U":
                    TempData["Message"] = "Your CSS has been successfully updated.";
                    break;
                default:
                    TempData["Message"] = "Your CSS has been successfully created.";
                    break;
                    
            }
        }
    }
}
