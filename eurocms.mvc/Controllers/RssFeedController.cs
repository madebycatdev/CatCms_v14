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
    public class RssFeedController : BaseController
    {
        RssFeedDbContext context = new RssFeedDbContext();
 
        public JsonResult SiteAndZoneGroups()
        {
            SiteService _siteService = new SiteService(new SiteRepository());
            
            var sites = _siteService.GetAll();
            for(var i = 0; i<sites.Count ; i++)
            {
                //sites[i].zone_groups = zgc.SelectZoneGroupsBySite(sites[i].site_id);
            }
            return Json(sites, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RssChannels()
        {
            ViewBag.RssChannels = Bags.GetRssChannels(null);
            return View();
        }

        public ActionResult Index(int? GroupId)
        {
            var result = context.SelectRssChannels(GroupId);
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.RssChannel, GroupId);
            return View(result);
        }
  
        public ActionResult Create(int? SiteId)
        {
            TempData.Clear();
            SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : SiteId;

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.RssChannel, null);
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteId, null);
            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.Layouts = Bags.GetLayouts();
             
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.RSS_FEED_CREATE, this));

            try
            {
                UpdateRssFeed(-1, collection);
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
            var result = context.SelectRssChannel(id).FirstOrDefault();

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.RssChannel, null);

            int SiteID = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : -1;

            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(SiteID, null);
            ViewBag.Languages = Bags.GetLanguages();
            ViewBag.Layouts = Bags.GetLayouts();

            ViewData["rss_channel_contents"] = context.SelectAdminRssChannelContent(id);

            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.RSS_FEED_EDIT, this));

            try
            {

                UpdateRssFeed(id, collection);

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

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.RSS_FEED_DELETE, this));

            try
            {
                var result = context.DeleteRssChannel(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted.";
                        break;
                    case "1":
                        throw new ApplicationException("This RSS Channel was not found OR already deleted before.");
                    case "2":
                        throw new ApplicationException("This  RSS Channel has content. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "3":
                        throw new ApplicationException("This  RSS Channel used on following zone groups. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
                    case "4":
                        throw new ApplicationException("This  RSS Channel used on following zones. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
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

        private void UpdateRssFeed(int id, FormCollection collection)
        {

            if (String.IsNullOrEmpty(collection["channel_name"]))
                throw new ApplicationException("Channel name required!");

            if (String.IsNullOrEmpty(collection["url"]))
                throw new ApplicationException("Channel URL required!");
              
            if (String.IsNullOrEmpty(collection["description"]))
                throw new ApplicationException("Channel Description required!");

            if (String.IsNullOrEmpty(collection["editor_html"]))
                throw new ApplicationException("Feed Content Template required!");

            cms_rss_channels channel = new cms_rss_channels();
            channel.channel_id = id;
            channel.channel_name = collection["channel_name"] ?? string.Empty;
            channel.channel_status = collection["channel_status"] ?? "P";
            channel.url = collection["url"] ?? string.Empty;
            channel.description = collection["description"] ?? string.Empty;
            channel.lang_id = collection["lang_id"] ?? string.Empty;
            channel.managing_editor = collection["managing_editor"] ?? string.Empty;
            channel.copyright = collection["copyright"] ?? string.Empty;
            channel.created_by = Membership.GetUser().ProviderUserKey;
            channel.group_id = !string.IsNullOrEmpty(collection["group_id"]) ? Convert.ToInt32(collection["group_id"]) : 0;
            channel.structure_description = collection["structure_description"] ?? string.Empty;
            channel.summary_content_field = collection["summary_content_field"] ?? "summary";
            channel.content_template = collection["editor_html"] ?? string.Empty;
            channel.content_template_editor_type = collection["content_template_editor_type"] ?? "H";
            channel.singularize_articles = collection["singularize_articles"] ?? "N";

            var result = context.UpdateSitemap(channel).FirstOrDefault();

            if(result.cStat.Equals("U"))
                context.DeleteRssContent(Convert.ToInt32(result.channel_id));

            if (!result.cStat.Equals("D"))
            {
                string channel_content = collection["channel_content"] ?? string.Empty;

                foreach(var content in channel_content.Split(','))
                {
                    var ie = string.Empty;
                    var x = content.Trim();
                    var z = string.Empty;

                    if (x.Length > 1)
                    {
                        var y = x.Substring(0, 1);
                        if (y.Equals("X")) // Exlude
                        {
                            ie = "E";
                            x = x.Substring(1);
                            y = x.Substring(0, 1);
                        }
                        else if (y.Equals("D")) // Exlude
                        {
                            ie = "D";
                            x = x.Substring(1);
                            y = x.Substring(0, 1);
                        }
                        else // Include
                        {
                            ie = "I";
                        }

                        z = x.Substring(1);
                        int zo = 0;
                        int.TryParse(z, out zo);

                        if((y=="Z" || y=="G" || y=="S") && zo > 0)
                            context.InsertRssContent(Convert.ToInt32(result.channel_id), zo, y, ie, Membership.GetUser().ProviderUserKey);
                    } 
                }
            }

            if (result.cStat.Equals("D"))
                throw new ApplicationException("This channel name is already used. Please choose another one");
            else if (result.cStat.Equals("U"))
                TempData["Message"] = "Rss Feed Channel has been successfully updated.";
            else
                TempData["Message"] = "Rss Feed Channel has been successfully created.";
        }
    }
}
