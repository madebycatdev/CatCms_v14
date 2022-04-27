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
    public class XmlGeneratorController : BaseController
    {
        XmlGeneratorDbContext context = new XmlGeneratorDbContext();

        [CmsAuthorize(Permission = "View", ContentType = "XmlGenerator")]
        public ActionResult Index(int? GroupId)
        {
            var result = context.SelectXmls(GroupId);
            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Xml, GroupId);
            return View(result);
        }

        [CmsAuthorize(Permission = "Create", ContentType = "XmlGenerator")]
        public ActionResult Create(int? SiteId)
        {
            TempData.Clear();
            SiteId = Session["CurrentSiteID"] != null ? Convert.ToInt32(Session["CurrentSiteID"]) : SiteId;

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Xml, null);
            ViewBag.XmlList = Bags.GetXmlList(null);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Create", ContentType = "XmlGenerator")]
        public ActionResult Create(FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.XML_GENERATOR_CREATE, this));

            try
            {
                UpdateXml(-1, collection);
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "XmlGenerator")]
        public ActionResult Edit(int id)
        {
            var result = context.SelectXml(id).FirstOrDefault();

            ViewBag.Groups = Bags.GetGroups(StructureGroupType.Xml, null);
            ViewBag.XmlList = Bags.GetXmlList(result.xml_sub_template);
             
            return View(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CmsAuthorize(Permission = "Edit", ContentType = "XmlGenerator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            TempData.Clear();

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.XML_GENERATOR_EDIT, this));

            try
            {

                UpdateXml(id, collection);

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

            WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.XML_GENERATOR_DELETE, this));

            try
            {
                var result = context.DeleteXml(id, Membership.GetUser().ProviderUserKey, Convert.ToInt32(Session["publisher_level"]));
                switch (result.FirstOrDefault().rCode)
                {
                    case "0":
                        TempData["Message"] = "Successfully Deleted.";
                        break;
                    case "1":
                        throw new ApplicationException("This xml was not found OR already deleted before.");
                    case "2":
                        throw new ApplicationException("This xml used on some xxx. You need to delete this relations first." + result.Select(m => m.found_name).InHtmlNewLine());
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

        private void UpdateXml(int id, FormCollection collection)
        {

            if (String.IsNullOrEmpty(collection["xml_name"]))
                throw new ApplicationException("XML name empty!");

            if (String.IsNullOrEmpty(collection["xml_main_node"]))
                throw new ApplicationException("XML main node name empty!");

            if (String.IsNullOrEmpty(collection["xml_per_node"]))
                throw new ApplicationException("XML per item node name empty!");

            if (String.IsNullOrEmpty(collection["xml_sub_node"]))
                throw new ApplicationException("XML second level line empty!");

            StringBuilder _xml = new StringBuilder();
            for (var x = 0; x < 20; x++)
            {
                _xml.AppendLine("<xml_data>");
                _xml.AppendLine("<name>" + (collection["name_" + x]??string.Empty) + "</name>");
                _xml.AppendLine("<attribute>" + (collection["attribute_" + x] ?? string.Empty) + "</attribute>");
                _xml.AppendLine("<value>" + (collection["value_" + x] ?? string.Empty) + "</value>");
                _xml.AppendLine("<cdata>" + (collection["cdata_" + x] ?? string.Empty) + "</cdata>");
                _xml.AppendLine("<afiles>" + (collection["afiles_" + x] ?? string.Empty) + "</afiles>");
                _xml.AppendLine("</xml_data>");
            }

            string raw_xml = "<xml_xml>" + Environment.NewLine + _xml.ToString() + Environment.NewLine + "</xml_xml>" + Environment.NewLine;
                
            cms_xml xml = new cms_xml();
            xml.xml_id = id;
            xml.xml_level = !string.IsNullOrEmpty(collection["xml_level"]) ? Convert.ToInt32(collection["xml_level"]) : 1;
            xml.xml_main_node = collection["xml_main_node"] ?? "P";
            xml.xml_main_node_attrib = collection["xml_main_node_attrib"] ?? string.Empty;
            xml.xml_name = collection["xml_name"] ?? string.Empty;
            xml.xml_per_node = collection["xml_per_node"] ?? string.Empty;
            xml.xml_per_node_attrib = collection["xml_per_node_attrib"] ?? string.Empty;
            xml.xml_related_line = collection["xml_related_line"] ?? string.Empty;
            xml.created_by = Membership.GetUser().ProviderUserKey;
            xml.group_id = !string.IsNullOrEmpty(collection["group_id"]) ? Convert.ToInt32(collection["group_id"]) : 0;
            xml.structure_description = collection["structure_description"] ?? string.Empty;
            xml.xml_sub_node = collection["xml_sub_node"] ?? string.Empty;
            xml.xml_sub_template = !string.IsNullOrEmpty(collection["xml_sub_template"]) ? Convert.ToInt32(collection["xml_sub_template"]) : 0;
            xml.xml_xml = raw_xml;
             
            var result = context.UpdateXml(xml).FirstOrDefault();
            var xStat = result.xStat;
             
            switch (xStat)
            {
                case "0":
                    TempData["Message"] = "Successfully Deleted.";
                    break;
                case "D":
                    throw new ApplicationException("This xml name is already used. Please choose another one.");
                case "U":
                    TempData["Message"] = "Your XML has been successfully updated.";
                    break;
                default:
                    TempData["Message"] = "Your xml has been successfully created.";
                    break;
            }
        }
    }
}
