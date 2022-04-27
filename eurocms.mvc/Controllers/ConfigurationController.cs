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
    [CmsAuthorize(Roles = "Administrator")]
    public class ConfigurationController : BaseController
    {
        ConfigurationDbContext context = new ConfigurationDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "Configuration")]
        public ActionResult Index()
        {
            var result = context.SelectAll();
            return View(result);
        }
  
        [CmsAuthorize(Permission = "Create", ContentType = "Configuration")]
        public ActionResult Create()
        {
            TempData.Clear();
            return View();
        }
 
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "Configuration")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CONFIGURATION_CREATE, this));

            try
            {
                UpdateConfig(-1, collection);
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
  
        [CmsAuthorize(Permission = "Edit", ContentType = "Configuration")]
        public ActionResult Edit(int id)
        {
            TempData.Clear();
            var result = context.Select(id).FirstOrDefault();
            return View(result);
        }
 
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "Configuration")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CONFIGURATION_EDIT, this));

            try
            {
                UpdateConfig(id, collection);
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
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "PowerUser", Permission = "Delete", ContentType = "Configuration")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.CONFIGURATION_DELETE, this));

            try 
            {
                var result = context.Delete(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted.";
                        break;
                    case "1":
                        throw new ApplicationException("This parameter was not found OR you can not delete default system parameters.");
                    case "2":
                        throw new ApplicationException("You can not delete this parameter.");
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

        private void UpdateConfig(int id, FormCollection collection)
        {
            if (String.IsNullOrEmpty(collection["config_name"]))
                throw new ApplicationException("Parameter name empty!");
            
            cms_config config = new cms_config();
            config.config_id = id;
            config.config_name = collection["config_name"] ?? string.Empty;
            config.config_value_local = collection["config_value"] ?? string.Empty;
            config.config_value_remote = collection["config_value"] ?? string.Empty;
            // config.publisher_id = Convert.ToInt32(Session["publisher_id"]);
            config.publisher_id = Membership.GetUser().ProviderUserKey;
            

            var result = context.Update(config).FirstOrDefault();
            
            if (result.cStat.Equals("D"))
             {
                throw new ApplicationException("This parameter name is already used. Please choose another one.");
             }
            else if (result.cStat.Equals("U"))
            {
                TempData["message"] = "Your parameter has been successfully updated.";
            }
            else
            {
                TempData["message"] = "Your parameter has been successfully created.";
            }
        }
    }
}
