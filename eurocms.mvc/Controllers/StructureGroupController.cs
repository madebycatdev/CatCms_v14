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

namespace EuroCMS.Admin.Controllers
{
    public class StructureGroupController : BaseController
    {
        StructureGroupDbContext context = new StructureGroupDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "StructureGroup")]
        public ActionResult Index()
        {
             
            var result = from sg in context.SelectStructureGroups()
                               group sg by sg.group_type into g
                         select new Group<cms_asp_admin_select_structure_group_Result, string> 
                               { Key = CoreHelper.GetStructureGroupName(g.Key), Values = g};

            return View(result.ToList());
        }

        [CmsAuthorize(Permission = "List", ContentType = "StructureGroup")]
        public ActionResult List()
        {
            TempData.Clear();
            var domains = context.SelectStructureGroups().GroupBy(g => g.group_type);
            return Json(domains, JsonRequestBehavior.AllowGet);
        }

        [CmsAuthorize(Permission = "View", ContentType = "StructureGroup")]
        public ActionResult Details(int id, int GroupType)
        {
            TempData.Clear();
            var result = context.SelectStructureGroup(id, GroupType);
            return View(result);
        }

        [CmsAuthorize(Permission = "Create", ContentType = "StructureGroup")]
        public ActionResult Create()
        {
            TempData.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "StructureGroup")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.STRUCTURE_GROUP_CREATE, this));

            try
            {
                cms_structure_groups structureGroup = new cms_structure_groups();
                structureGroup.created_by = Membership.GetUser().ProviderUserKey;
                structureGroup.group_name = collection["group_name"];
                structureGroup.group_type = Convert.ToInt32(collection["group_type"]);

                var result = context.CreateStructureGroup(structureGroup).FirstOrDefault();
                switch (result.rCode)
                {
                    case "ALREADYHAVE":
                        throw new ApplicationException("This group name is already used. Please choose another one.");
                    case "UPDATED":
                        TempData["Message"] = "Structure group has been successfully updated.";
                        break;
                    case "INSERTED":
                        TempData["Message"] = "Structure group has been successfully created.";
                        break;
                    default:
                        TempData["Message"] = "unexpected error!. please contact with system administrator";
                        break;
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

        [CmsAuthorize(Permission = "Edit", ContentType = "StructureGroup")]
        public ActionResult Edit(int id, int GroupType)
        {
            TempData.Clear();
            var result = context.SelectStructureGroup(id, GroupType).FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "StructureGroup")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.STRUCTURE_GROUP_EDIT, this));

            try
            {
                cms_structure_groups structureGroup = new cms_structure_groups();
                structureGroup.group_id = id;
                structureGroup.created_by = Membership.GetUser().ProviderUserKey;
                structureGroup.group_name = collection["group_name"];
                structureGroup.group_type = Convert.ToInt32(collection["group_type"]);

                var result = context.UpdateStructureGroup(structureGroup).FirstOrDefault();
                switch (result.rCode)
                {
                    case "ALREADYHAVE":
                        throw new ApplicationException("This group name is already used. Please choose another one.");
                    case "UPDATED":
                        TempData["Message"] = "Structure group has been successfully updated.";
                        break;
                    case "INSERTED":
                        TempData["Message"] = "Structure group has been successfully created.";
                        break;
                    default:
                        TempData["Message"] = "unexpected error!. please contact with system administrator";
                        break;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "Administrator,PowerUser")]
        public ActionResult Delete(int id, int GroupType)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.STRUCTURE_GROUP_DELETE, this));

            try
            {
                var result = context.DeleteStructureGroup(id, GroupType).FirstOrDefault();

                switch (result)
                {
                    case "DELETED":
                        TempData["Message"] = "Successfully Deleted!";
                        break;
                    case "NOTEXIST":
                        throw new ApplicationException("This group was not found OR already deleted before.");
                    case "USED":
                        throw new ApplicationException("This group ise in use. Please unassociate any items that uses this group.");
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
 
    }
}
