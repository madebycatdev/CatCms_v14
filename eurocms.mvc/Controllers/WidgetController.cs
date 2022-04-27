using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace EuroCMS.Admin.Controllers
{
    public class WidgetController : BaseController
    {
        //
        // GET: /Widget/

        public ActionResult Index()
        {
            return View();
        }

        public enum Widgets
        {
            QuickArticle = 1,
            QuickZone = 2,
            GoogleAnalytics = 3,
            ActivityLogs = 4
        }

        public void SaveConfig(FormCollection collection)
        {
            string result = string.Empty;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string widgetType = collection["WidgetType"];

            switch (widgetType)
            {
                case "QuickArticle":
                    string zones = collection["zones[]"];
                    string clsfId = collection["ClsfId"];
                    string langId = collection["Language"];
                    string widgetUserId = collection["[0].WidgetUserID"];    //her partialda name değişiyo o yüzden index 0 kullandım
                    result = SaveQuickArticleConfig(zones, clsfId, langId, widgetUserId);
                    break;
                case "QuickZone":
                    result = SaveZoneConfig(collection);
                    break;
                case "Activities":
                    result = SaveActivityConfig(collection);
                    break;
                default:
                    break;
            }

            result = jss.Serialize(result);
            Response.Write(result);
            Response.End();
        }

        private string SaveActivityConfig(FormCollection collection)
        {
            string result = string.Empty;
            string users = collection["users"];
            string days = collection["article_type"];
            string widgetUserId = collection["[0].WidgetUserID"];

            try
            {
                CmsDbContext dbContext = new CmsDbContext();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                List<string> paramKeys = new List<string> { "Users", "ActivityDays" };
                List<string> paramValues = new List<string> { users, days };

                if (string.IsNullOrEmpty(widgetUserId))
                {
                    //ilk config - widget user kaydı yap önce
                    widgetUserId = SaveFirstConfigs(widgetUserId, dbContext, currentUserId, paramKeys, paramValues, Convert.ToInt32((int)Widgets.ActivityLogs));
                }
                else
                {
                    UpdateConfigs(widgetUserId, dbContext, paramKeys, paramValues);
                }

                ViewBag.WidgetUserId = widgetUserId;
                result = "OK-" + widgetUserId;
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Cannot create activity config", true);
                result = "NOK";
            }

            return result;
        }

        public string SaveZoneConfig(FormCollection collection)
        {
            string result = string.Empty;
            string zoneGroup = collection["zoneGroup"];
            string language = collection["Language"];
            string template = collection["Template"];
            string widgetUserId = collection["[0].WidgetUserID"];

            try
            {
                CmsDbContext dbContext = new CmsDbContext();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                List<string> paramKeys = new List<string> { "ZoneGroup", "Language", "Template" };
                List<string> paramValues = new List<string> { zoneGroup, language, template };

                if (string.IsNullOrEmpty(widgetUserId))
                {
                    //ilk config - widget user kaydı yap önce
                    widgetUserId = SaveFirstConfigs(widgetUserId, dbContext, currentUserId, paramKeys, paramValues, Convert.ToInt32((int)Widgets.QuickZone));
                }
                else
                {
                    UpdateConfigs(widgetUserId, dbContext, paramKeys, paramValues);
                }

                ViewBag.WidgetUserId = widgetUserId;
                result = "OK-" + widgetUserId;
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Cannot create quick zone config", true);
                result = "NOK";
            }

            return result;
        }

        private static string SaveFirstConfigs(string widgetUserId, CmsDbContext dbContext, Guid currentUserId, List<string> paramKeys, List<string> paramValues, int widgetId)
        {
            WidgetUser wu = new WidgetUser();
            wu.UserID = currentUserId;
            wu.WidgetID = widgetId;
            wu.IsActive = true;

            dbContext.WidgetUsers.Add(wu);
            dbContext.SaveChanges();

            widgetUserId = wu.ID.ToString();

            List<WidgetConfig> wcList = new List<WidgetConfig>();
            for (int i = 0; i < paramKeys.Count; i++)
            {
                WidgetConfig wc = new WidgetConfig();
                wc.IsActive = true;
                wc.ParamKey = paramKeys[i];
                wc.ParamValue = paramValues[i];
                wc.WidgetUserID = Convert.ToInt32(widgetUserId);
                wcList.Add(wc);
            }

            dbContext.WidgetConfigs.AddRange(wcList);
            dbContext.SaveChanges();
            return widgetUserId;
        }

        private static void UpdateConfigs(string widgetUserId, CmsDbContext dbContext, List<string> paramKeys, List<string> paramValues)
        {
            List<WidgetConfig> wcList = dbContext.WidgetConfigs.Where(x => x.WidgetUserID.ToString() == widgetUserId && x.IsActive).ToList();

            if (wcList != null && wcList.Count > 0)
            {
                for (int i = 0; i < wcList.Count; i++)
                {
                    wcList[i].ParamValue = paramValues[paramKeys.IndexOf(wcList[i].ParamKey)];
                }
            }
            else
            {
                wcList = new List<WidgetConfig>();
                for (int i = 0; i < paramKeys.Count; i++)
                {
                    WidgetConfig wc = new WidgetConfig();
                    wc.ParamKey = paramKeys[i];
                    wc.ParamValue = paramValues[i];
                    wc.WidgetUserID = Convert.ToInt32(widgetUserId);
                    wc.IsActive = true;

                    wcList.Add(wc);
                }

                dbContext.WidgetConfigs.AddRange(wcList);
            }
            dbContext.SaveChanges();
        }

        public string SaveQuickArticleConfig(string zones, string clsfId, string langId, string widgetUserId)
        {
            string result = string.Empty;
            try
            {
                CmsDbContext dbContext = new CmsDbContext();
                Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                List<string> paramKeys = new List<string> { "Zones", "ClassificationID", "LanguageID" };
                List<string> paramValues = new List<string> { zones, clsfId, langId };

                if (string.IsNullOrEmpty(widgetUserId))
                {
                    //ilk config - widget user kaydı yap önce
                    SaveFirstConfigs(widgetUserId, dbContext, currentUserId, paramKeys, paramValues, Convert.ToInt32((int)Widgets.QuickArticle));
                }
                else
                {
                    UpdateConfigs(widgetUserId, dbContext, paramKeys, paramValues);
                }

                ViewBag.WidgetUserId = widgetUserId;
                result = "OK-" + widgetUserId;
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, "Cannot create quick article config", true);
                result = "NOK";
            }

            return result;
        }

        public ActionResult GetDetails(string widgetUserId)
        {
            CmsDbContext dbContext = new CmsDbContext();
            List<WidgetConfig> wcList = dbContext.WidgetConfigs.Where(x => x.WidgetUserID.ToString() == widgetUserId).ToList();
            List<WidgetUser> wuList = dbContext.WidgetUsers.Where(x => x.ID.ToString() == widgetUserId).ToList();
            string prtView = string.Empty;

            if (wuList != null && wuList.Count > 0)
            {
                if (wuList.FirstOrDefault().WidgetID == (int)Widgets.QuickArticle)
                {
                    string _ClsfId = wcList.Where(x => x.ParamKey == "ClassificationID").FirstOrDefault() == null ? string.Empty : wcList.Where(x => x.ParamKey == "ClassificationID").FirstOrDefault().ParamValue;
                    int clsfId = -1;
                    if (!string.IsNullOrEmpty(_ClsfId))
                    {
                        clsfId = Convert.ToInt32(_ClsfId);
                    }

                    if (clsfId > 0)
                    {
                        ClassificationDbContext cContext = new ClassificationDbContext();
                        ViewData["classification_details"] = cContext.SelectClassificationDetails(clsfId).FirstOrDefault();
                        for (int i = 1; i <= 20; i++)
                        {
                            ViewData["combo_values_" + i] = cContext.SelectClassificationComboValues(clsfId, i);
                        }
                    }

                    prtView = "AddQuickArticle";
                }
                else if (wuList.FirstOrDefault().WidgetID == (int)Widgets.QuickZone)
                {
                    prtView = "AddQuickZone";
                }
                else if (wuList.FirstOrDefault().WidgetID == (int)Widgets.ActivityLogs)
                {
                    prtView = "Activities";
                }
            }

            return PartialView(prtView, wcList);
        }

        public ActionResult GetQuickZone(string widgetUserId)
        {
            CmsDbContext dbContext = new CmsDbContext();
            WidgetUser wu = dbContext.WidgetUsers.Where(x => x.ID.ToString() == widgetUserId && x.IsActive).FirstOrDefault();
            return PartialView("AddQuickZone", wu);
        }

        public ActionResult GetConfigs(string widgetUserId)
        {
            CmsDbContext dbContext = new CmsDbContext();
            List<WidgetConfig> wcList = dbContext.WidgetConfigs.Where(x => x.WidgetUserID.ToString() == widgetUserId).ToList();
            WidgetUser wu = dbContext.WidgetUsers.Where(x => x.ID.ToString() == widgetUserId).FirstOrDefault();
            int wType = 0;

            if (wu != null)
            {
                wType = wu.WidgetID;
            }
            string prtView = string.Empty;

            if (wType == (int)Widgets.QuickArticle)
            {
                prtView = "QuickArticleConfig";
            }
            else if (wType == (int)Widgets.QuickZone)
            {
                prtView = "QuickZoneConfig";
            }
            else if (wType == (int)Widgets.ActivityLogs)
            {
                prtView = "ActivityConfig";
            }

            return PartialView(prtView, wcList);
        }

        public void SaveQuickArticle(FormCollection collection)
        {
            CmsDbContext dbContext = new CmsDbContext();
            string az_list = collection["zones[]"] ?? "";
            if (!string.IsNullOrEmpty(az_list))
            {
                string[] zones = az_list.Split(',');
                int zoneId = Convert.ToInt32(zones[0]);
                int zgId = dbContext.Zones.Where(x => x.Id == zoneId).FirstOrDefault().ZoneGroupId;
                int siteId = dbContext.ZoneGroups.Where(x => x.Id == zgId).FirstOrDefault().SiteId;
                Session["CurrentSiteID"] = siteId;
                TempData["CurrentSiteID"] = siteId;
            }
            var result = new ArticleController().Edit(-1, null, 1, string.Empty, true, collection);
        }

        public void RemoveConfig(FormCollection collection)
        {
            string widgetUserId = collection["[0].WidgetUserID"];
            int wid = Convert.ToInt32(widgetUserId);

            CmsDbContext dbContext = new CmsDbContext();
            List<WidgetConfig> wcList = dbContext.WidgetConfigs.Where(x => x.WidgetUserID == wid).ToList();
            List<WidgetUser> wuList = dbContext.WidgetUsers.Where(x => x.ID == wid).ToList();

            foreach (WidgetUser wu in wuList)
            {
                wu.IsActive = false;
            }

            foreach (WidgetConfig wc in wcList)
            {
                wc.IsActive = false;
            }

            dbContext.SaveChanges();
        }

        public ActionResult AddWidget(int widgetId)
        {
            CmsDbContext dbContext = new CmsDbContext();
            Guid currentUserId = (Guid)Membership.GetUser().ProviderUserKey;

            #region Config yapılmamışları sil
            //select * from cms_widget_users wu where UserId = '71B3FDF5-DD68-4397-B185-87C5B29D2E9D' and (select count(*) from cms_widget_configs wc where wc.WidgetUserId = wu.ID) = 0
            List<WidgetUser> wuList = dbContext.WidgetUsers.Where(x => x.UserID == currentUserId && x.IsActive).ToList();
            if (wuList != null)
            {
                foreach (WidgetUser item in wuList)
                {
                    List<WidgetConfig> wcList = dbContext.WidgetConfigs.Where(x => x.WidgetUserID == item.ID).ToList();
                    if (wcList == null || wcList.Count == 0)
                    {
                        if (item.WidgetID != (int)Widgets.GoogleAnalytics)
                        {
                            item.IsActive = false; 
                        }
                    }
                }
            }
            #endregion

            WidgetUser wu = new WidgetUser();
            wu.UserID = currentUserId;
            wu.WidgetID = widgetId;
            wu.IsActive = true;

            dbContext.WidgetUsers.Add(wu);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }
    }
}
