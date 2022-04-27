using EuroCMS.Admin.Common;
using EuroCMS.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.Admin.Controllers
{
    public class MenuController : BaseController
    {
        [HttpPost]
        [CmsAuthorize(Permission = "View", ContentType = "Menu")]
        public ActionResult Properties(FormCollection collection)
        {
            var zone_id = collection["zone_id"] ?? "0";
            var menu_depth = collection["menu_depth"] ?? "";
            var item_ordering = collection["item_ordering"] ?? "11";
            var class_name = collection["class_name"] ?? "";
            var container_tag = collection["container_tag"] ?? "";
            var include_articles = collection["include_articles"] ?? "0-0";
            var exclude_articles = collection["exclude_articles"] ?? "0-0";
            var position = collection["position"] ?? "v";
            var container_tag_id = collection["container_tag_id"] ?? "v";
            var eliminate_single = collection["eliminate_single"] ?? "False";
            var remove_onclick_function = collection["remove_onclick_function"] ?? "False";
            var selected_item_class = collection["selected_item_class"] ?? "";
            var not_selected_item_class = collection["not_selected_item_class"] ?? "";

            if (string.IsNullOrEmpty(include_articles))
                include_articles = "0-0";

            if (string.IsNullOrEmpty(exclude_articles))
                exclude_articles = "0";

            int _zoneId = 0;
            Int32.TryParse(zone_id, out _zoneId);

            int _menuDepth = 0;
            Int32.TryParse(menu_depth, out _menuDepth);
 
            int _ItemOrdering = 0;
            Int32.TryParse(item_ordering, out _ItemOrdering);

            bool _EliminateSingle = false;
            Boolean.TryParse(eliminate_single, out _EliminateSingle);

            bool _RemoveOnclikFunction = false;
            Boolean.TryParse(remove_onclick_function, out _RemoveOnclikFunction);
           
            MenuPropertyView pv = new MenuPropertyView();
            pv.ZoneId = _zoneId;
            pv.MenuDepth = _menuDepth;
            pv.ItemOrdering = _ItemOrdering;
            pv.ClassName = class_name;
            pv.ContainerTag = container_tag;
            pv.IncludeArticles = include_articles;
            pv.ExcludeArticles = exclude_articles;
            pv.Position = position;
            pv.ContainerTagId = container_tag_id;
            pv.EliminateSingle = _EliminateSingle;
            pv.RemoveOnclikFunction = _RemoveOnclikFunction;
            pv.SelectedItemClass = selected_item_class;
            pv.NotSelectedItemClass = not_selected_item_class;
 
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
    }
}
