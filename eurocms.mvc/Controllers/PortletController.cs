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
    [CmsAuthorize(Roles = "Administrator,PowerUser,Editor,ContentEntry,UserCreator")]
    public class PortletController : BaseController
    {
        PortletDbContext context = new PortletDbContext();

        [CmsAuthorize(Roles = "ContentEntry,UserCreator", Permission = "View", ContentType = "Portlet")]
        public ActionResult Index(int? GroupId)
        {
            var result = context.SelectAll(GroupId);
            
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Portlet, GroupId);
            
            return View(result);
        }

        [HttpPost]
        [CmsAuthorize(Roles = "ContentEntry,UserCreator", Permission = "View", ContentType = "Portlet")]
        public ActionResult Properties(FormCollection collection)
        { 
            var zone_id = HttpUtility.HtmlEncode(collection["zone_id"]) ?? "0";
            var item_count = HttpUtility.HtmlEncode(collection["item_count"]) ?? "0";
            var item_ordering = HttpUtility.HtmlEncode(collection["item_ordering"]) ?? "11";
            var portlet_header = HttpUtility.HtmlEncode(collection["portlet_header"]) ?? "";
            var container_tag = HttpUtility.HtmlEncode(collection["container_tag"]) ?? "";
            var include_articles = HttpUtility.HtmlEncode(collection["include_articles"]) ?? "0-0";
            var exclude_articles = HttpUtility.HtmlEncode(collection["exclude_articles"]) ?? "0-0";
            var pager_class = HttpUtility.HtmlEncode(collection["pager_class"]) ?? "";
            var class_name = HttpUtility.HtmlEncode(collection["class_name"]) ?? "";
            var exclude_self = HttpUtility.HtmlEncode(collection["exclude_self"]) ?? "0";
            var pager_count = HttpUtility.HtmlEncode(collection["pager_count"]) ?? "0";
            var pager_position = HttpUtility.HtmlEncode(collection["pager_position"]) ?? "0";
            var pager_header = HttpUtility.HtmlEncode(collection["pager_header"]) ?? "";
            var prev_next_caption = HttpUtility.HtmlEncode(collection["prev_next_caption"]) ?? "";
            var item_seperator = HttpUtility.HtmlEncode(collection["item_seperator"]) ?? "";



            if (string.IsNullOrEmpty(include_articles))
                include_articles = "0-0";

            if (string.IsNullOrEmpty(exclude_articles))
                exclude_articles = "0";

            int _zoneId = 0;
            Int32.TryParse(zone_id, out _zoneId);

            int _ItemCount = 0;
            Int32.TryParse(item_count, out _ItemCount);

            int _ItemOrdering = 0;
            Int32.TryParse(item_ordering, out _ItemOrdering);

            int _ExcludeSelf = 0;
            Int32.TryParse(exclude_self, out _ExcludeSelf);

            int _PagerCount = 0;
            Int32.TryParse(pager_count, out _PagerCount);

            int _PagePosition = 0;
            Int32.TryParse(pager_position, out _PagePosition);

         
            PortletPropertyView pv = new PortletPropertyView();
            pv.ZoneId = _zoneId;
            pv.ItemCount = _ItemCount;
            pv.ItemOrdering = _ItemOrdering;
            pv.PortletHeader = portlet_header;
            pv.ContainerTag = container_tag;
            pv.IncludeArticles = include_articles;
            pv.ExcludeArticles = exclude_articles;
            pv.ExcludeSelf = _ExcludeSelf;
            pv.PagerClass = pager_class;
            pv.ClassName = class_name;
            pv.PagerCount = _PagerCount;
            pv.PagerPosition = _PagePosition;
            pv.PagerHeader = pager_header;
            pv.PrevNextCaption = prev_next_caption;
            pv.ItemSeperator = item_seperator;

            ViewBag.IncludeArticles = Bags.GetArticleWithAllPath(include_articles);
            ViewBag.ExcludeArticles = Bags.GetArticleWithAllPath(exclude_articles);

            List<SelectListItem> zones = Bags.GetZones(_zoneId);

            List<SelectListItem> firstItems = new List<SelectListItem> { 
                new SelectListItem { Text = "-- Current Zone --", Value = "0", Selected = zone_id.Equals("0") },
                new SelectListItem { Text = "-- Related Articles Portlet --", Value = "-2", Selected = zone_id.Equals("-2") },
                new SelectListItem { Text = "-- Tag Display Portlet --", Value = "-4", Selected = zone_id.Equals("-4") },
                new SelectListItem { Text = "-- Article File Portlet --", Value = "-1", Selected = zone_id.Equals("-1") },
                new SelectListItem { Text = "-- Selected Articles Portlet --", Value = "-3", Selected = zone_id.Equals("-3") },
                new SelectListItem { Text = "-- Custom Content --", Value = "-5", Selected = zone_id.Equals("-5") }
            };

            zones.InsertRange(0, firstItems);

            ViewBag.Zones = zones;

            return View(pv);
        }

        public ActionResult PortletList()
        {
            var result = context.SelectAll(-1);

            return View(result);
        }

        [CmsAuthorize(Roles = "ContentEntry,UserCreator", Permission = "Create", ContentType = "Portlet")]
        public ActionResult Create()
        { 
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Portlet, null);
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(-1, -1);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.PORTLET_CREATE, this));
            
            try
            {
                UpdatePortlet(-1, collection);
                
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
 
        [CmsAuthorize(Roles = "ContentEntry,UserCreator", Permission = "Edit", ContentType = "Portlet")]
        public ActionResult Edit(int id)
        {
            TempData.Clear();
           
            var result = context.Select(id).FirstOrDefault();
            
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Portlet, null);
            ViewBag.ZoneGroups = Bags.GetZoneGroupsBySite(-1, -1);
            
            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Roles = "ContentEntry,UserCreator", Permission = "Edit", ContentType = "Portlet")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.PORTLET_EDIT, this));
            
            try
            {
                UpdatePortlet(id, collection);
                
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
        [CmsAuthorize(Roles = "Administrator,PowerUser,ContentEntry,UserCreator")]
        public ActionResult Delete(int id)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.PORTLET_DELETE, this));
           
            try 
            {
                var result = context.Delete(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
                
                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted.";
                        break;
                    case "1":
                        throw new ApplicationException("This portlet was not found OR already deleted.");
                    case "2":
                        throw new ApplicationException("You do not have access to delete this portlet. Also you are logged.");
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
 
        private void UpdatePortlet(int id, FormCollection collection)
        {
            if (String.IsNullOrEmpty(collection["portlet_name"]))
                throw new ApplicationException("Portlet name empty!");

            cms_portlets portlet = new cms_portlets();
            portlet.portlet_id = id;
            portlet.portlet_name = collection["portlet_name"] ?? string.Empty;
            portlet.portlet_status = collection["portlet_status"] == null || collection["portlet_status"] != "1" ? (byte)0 : (byte)1;
            portlet.portlet_css = collection["portlet_css"] ?? string.Empty;
            portlet.portlet_html = collection["editor_html"] ?? string.Empty;
            portlet.editor_type = collection["editor_type"] == null || collection["editor_type"] != "T" ? true : false;
            portlet.portlet_header = collection["portlet_header"] ?? string.Empty;
            portlet.portlet_footer = collection["portlet_footer"] ?? string.Empty;
            portlet.group_id = collection["group_id"] != null && !string.IsNullOrEmpty(collection["group_id"]) ? Convert.ToInt32(collection["group_id"]) : 0;
            // portlet.publisher_id = Convert.ToInt32(Session["publisher_id"]);
            portlet.publisher_id = Membership.GetUser().ProviderUserKey;
            portlet.structure_description = collection["structure_description"] ?? string.Empty;
            portlet.enable_shortcut = collection["enable_shortcut"] == null || collection["enable_shortcut"] != "Y" ? "N" : "Y";

            var result = context.Update(portlet).FirstOrDefault();

            if (result.pStat.Equals("U"))
            {
                TempData["message"] = "Your portlet has been successfully updated.";
            }
            else
            {
                TempData["message"] = "Your portlet has been created.";
            }
        }
    }
}
