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
    public class HiddenValueController : BaseController
    {
        HiddenValueDbContext context = new HiddenValueDbContext();

        public ActionResult Index()
        {
            var result = context.SelectAll();
            
            return View(result);
        }

        public ActionResult Create(int? SiteId)
        {
            TempData.Clear();
             
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.HIDDEN_VALUE_CREATE, this));

            try
            {
                UpdateHV(-1, collection);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var result = context.Select(id).FirstOrDefault();
   
            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.HIDDEN_VALUE_EDIT, this));

            try
            {

                UpdateHV(id, collection);

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
            try
            {
                var result = context.Delete(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted." + result.Select(m => m.found_name).InHtmlNewLine();
                        break;
                    case "1":
                        throw new ApplicationException("This hidden value was not found OR already deleted.");
                    case "2":
                        throw new ApplicationException("You do not have access to delete this hidden value. Also you are logged!");
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

        private void UpdateHV(int id, FormCollection collection)
        {

            if (String.IsNullOrEmpty(collection["hidden_value"]))
                throw new ApplicationException("Hidden value empty!");

            if (String.IsNullOrEmpty(collection["hidden_type"]))
                throw new ApplicationException("Hidden type empty!");

            int hidden_type = 0;
            try
            {
                hidden_type = Convert.ToInt32(collection["hidden_type"]);
            }
            catch(Exception ex)
            { 
                CmsHelper.SaveErrorLog(ex, "Hidden type invalid!", true);
                throw new ApplicationException("Hidden type invalid!"); 
            }
               
            cms_hidden_values hv = new cms_hidden_values();
            hv.hidden_id = id;
            hv.hidden_value = collection["hidden_value"] ?? "";
            hv.hidden_type = (byte)hidden_type;
            hv.created_by = Membership.GetUser().ProviderUserKey;
            hv.hidden_desc = collection["hidden_desc"] ?? string.Empty;

            var result = context.Update(hv).FirstOrDefault();

            switch (result.dStat)
            {
                case "D":
                    throw new ApplicationException("The hidden value \"" + hv.hidden_value +"\" has already been used before.\nPlease use another one.");
                case "U":
                    TempData["Message"] = "Your hidden value has been successfully updated.";
                    break;
                default:
                    TempData["Message"] = "Your hidden value has been successfully created.";
                    break;
            }
        }
    }
}
