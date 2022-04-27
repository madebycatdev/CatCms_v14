using EuroCMS.Admin.entity;
using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class CustomContentController : BaseController
    {
        CustomContentDbContext context = new CustomContentDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "CustomContent")]
        public ActionResult Index(int? GroupId)
        {
            var result = context.SelectAll(GroupId);
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.CustomContent, GroupId);
            return View(result);
        }
  
        [CmsAuthorize(Permission = "Create", ContentType = "CustomContent")]
        public ActionResult Create(int? SiteId)
        {
            TempData.Clear();
            SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : SiteId;

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.CustomContent, null);
            
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "CustomContent")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CUSTOM_CONTENT_CREATE, this));

            try
            {
                UpdateCC(-1, collection);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "CustomContent")]
        public ActionResult Edit(int id)
        {
            var result = context.Select(id).FirstOrDefault();

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.CustomContent, null);
             
            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "CustomContent")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CUSTOM_CONTENT_EDIT, this));

            try
            {

                UpdateCC(id, collection);

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
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CUSTOM_CONTENT_DELETE, this));

            try
            {
                var result = context.Delete(id,
                        Membership.GetUser().ProviderUserKey, 
                        Convert.ToInt32(Session["publisher_level"]));
                
                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted." + result.Select(m => m.found_name).InHtmlNewLine();
                        break;
                    case "1":
                        throw new ApplicationException("This custom content was not found OR already deleted.");
                    case "2":
                        throw new ApplicationException("You do not have access to delete this custom content. Also you are logged!");
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

        private void UpdateCC(int id, FormCollection collection)
        {

            if (String.IsNullOrEmpty(collection["cc_name"]))
                throw new ApplicationException("Custom Content Name empty!");
 
            cms_custom_content cc = new cms_custom_content();
            cc.cc_id = id;
            cc.cc_name = collection["cc_name"];
            cc.cc_html = collection["cc_edithtml"] ?? "";
            cc.created_by = Membership.GetUser().ProviderUserKey;
            cc.group_id = !string.IsNullOrEmpty(collection["group_id"]) ? Convert.ToInt32(collection["group_id"]) : 0;
            cc.structure_description = collection["structure_description"] ?? string.Empty;
             
            var result = context.Update(cc).FirstOrDefault();
           
            switch (result.cStat)
            {
                case "U":
                    TempData["Message"] = "Your custom content has been successfully updated.";
                    break;
                default:
                    TempData["Message"] = "Your custom content has been successfully created.";
                    break;
            }
        }
    }
}
