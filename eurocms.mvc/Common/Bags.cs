
using EuroCMS.Admin.Models;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EuroCMS.Admin.Common
{
    public static class Bags
    {
        static SiteService _siteService = new SiteService(new SiteRepository());

        static DomainDbContext dContext = new DomainDbContext();
        static ArticleFileDbContext afContext = new ArticleFileDbContext();
        static LayoutDbContext lContext = new LayoutDbContext();

        static ArticleDbContext aContext = new ArticleDbContext();
        static ZoneGroupService _zoneGroupService = new ZoneGroupService(new ZoneGroupRepository());
        static ZoneDbContext zContext = new ZoneDbContext();
        static StructureGroupDbContext sgContext = new StructureGroupDbContext();
        static LanguageDbContext lgContext = new LanguageDbContext();
        static ClassificationDbContext cContext = new ClassificationDbContext();
        static RssFeedDbContext rfContext = new RssFeedDbContext();
        static XmlGeneratorDbContext xgContext = new XmlGeneratorDbContext();

        public static string GetBaseUrl(string domain)
        {
            if (string.IsNullOrEmpty(domain))
                return string.Format("{0}://{1}{2}",
                  HttpContext.Current.Request.Url.Scheme,
                  HttpContext.Current.Request.Url.Host,
                  (HttpContext.Current.Request.Url.Port > 0 ? ":" + HttpContext.Current.Request.Url.Port : ""));
            else
                return string.Format("{0}://{1}",
                HttpContext.Current.Request.Url.Scheme,
                domain);

        }

        public static string GetSitemapStatusText(int status)
        {
            string ret = string.Empty;
            switch (status)
            {
                case 0:
                    ret = "Ready";
                    break;
                case 1:
                    ret = "Waiting for create";
                    break;
                case 2:
                    ret = "Creating";
                    break;
            }
            return ret;
        }

        public static List<SelectListItem> GetDomains()
        {
            List<SelectListItem> domains = new List<SelectListItem>();
            foreach (var domain in dContext.SelectDomains())
            {
                domains.Add(new SelectListItem
                {
                    Value = domain.domain_id.ToString(),
                    Text = HttpUtility.HtmlDecode(domain.domain_names)
                });
            }
            return domains;
        }

        public static List<SelectListItem> GetDomainsWithRows()
        {
            List<SelectListItem> domains = new List<SelectListItem>();
            List<string> allDomains = new List<string>();
            List<string> listDomains = new List<string>();
            listDomains = dContext.SelectDomains().Select(s => s.domain_names).ToList();
            foreach (string domain in listDomains)
            {
                string domainDecode = HttpUtility.HtmlDecode(domain);
                List<string> sbDomains = new List<string>();
                bool isChanged = false;
                if (domainDecode.Contains(","))
                {
                    isChanged = true;
                    sbDomains = domainDecode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                if (domain.Contains(Environment.NewLine))
                {
                    isChanged = true;
                    sbDomains = domainDecode.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                foreach (string d in sbDomains)
                {
                    if (!allDomains.Contains(d))
                    {
                        allDomains.Add(d);
                    }
                }

                if (!isChanged)
                {
                    allDomains.Add(domainDecode);
                }

            }

            foreach (var addDomain in allDomains)
            {
                domains.Add(new SelectListItem
                {
                    Value = addDomain,
                    Text = HttpUtility.HtmlDecode(addDomain)
                });
            }
            return domains;
        }

        public static List<SelectListItem> GetDomainsParticular(int domain_id, string domain_alias)
        {
            List<SelectListItem> domains = new List<SelectListItem>();
            foreach (var domain in dContext.SelectDomains())
            {
                string[] domainNamesArray = domain.domain_names.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var d in domainNamesArray)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = string.Format("{0};;{1}", domain.domain_id.ToString(), d),
                        Text = d,
                        Selected = domain.domain_id == domain_id && domain_alias.Trim().ToLower() == d.Trim().ToLower() ? true : false
                    };

                    if (!domains.Contains(item) && d.Trim() != "")
                        domains.Add(item);
                }
            }

            return domains;
        }

        public static List<SelectListItem> GetFileTypes()
        {
            List<SelectListItem> FileTypes = new List<SelectListItem>();
            foreach (var FileType in afContext.SelectFileTypes())
            {
                FileTypes.Add(new SelectListItem
                {
                    Value = FileType.type_id.ToString(),
                    Text = HttpUtility.HtmlDecode(FileType.type_name)
                });
            }
            return FileTypes;
        }

        public static List<SelectListItem> GetLanguages()
        {
            var isFirst = true;
            List<SelectListItem> languages = new List<SelectListItem>();
            foreach (var language in lgContext.SelectLanguages())
            {
                languages.Add(new SelectListItem
                {
                    Value = language.lang_id,
                    //Selected = language.lang_id == "TR" ? true : false,
                    Selected = isFirst,
                    Text = HttpUtility.HtmlDecode(language.lang_name)
                });
                isFirst = false;
            }
            return languages;
        }

        public static List<GroupDropListItem> GetLayouts()
        {
            List<GroupDropListItem> layoutItems = new List<GroupDropListItem>();
            var layouts = lContext.SelectLayouts(null);
            foreach (var layout in layouts)
            {
                List<OptionItem> items = new List<OptionItem>();

                GroupDropListItem group = layoutItems.Where(li => li.Name == layout.group_name).FirstOrDefault();
                if (group != null)
                {
                    group.Items.Add(new OptionItem() { Text = HttpUtility.HtmlDecode(layout.template_name), Value = layout.template_id.ToString() });
                }
                else
                {
                    group = new GroupDropListItem();
                    group.Name = layout.group_name;
                    group.Items = new List<OptionItem>();
                    group.Items.Add(new OptionItem() { Text = layout.template_name, Value = layout.template_id.ToString() });
                    layoutItems.Add(group);
                }
            }
            return layoutItems;
        }

        public static List<GroupDropListItem> GetSites()
        {
            List<GroupDropListItem> siteItems = new List<GroupDropListItem>();
            var sites = _siteService.GetAll();
            foreach (var site in sites)
            {
                var groupName = site.Group != null ? site.Group.Name : "Ungrouped";

                List<OptionItem> items = new List<OptionItem>();

                GroupDropListItem group = siteItems.Where(li => li.Name == groupName).FirstOrDefault();
                if (group != null)
                {
                    group.Items.Add(new OptionItem() { Text = site.Name, Value = site.Id.ToString() });
                }
                else
                {
                    group = new GroupDropListItem();
                    group.Name = groupName;
                    group.Items = new List<OptionItem>();
                    group.Items.Add(new OptionItem() { Text = site.Name, Value = site.Id.ToString() });
                    siteItems.Add(group);
                }
            }
            return siteItems;
        }

        public static List<SelectListItem> GetSitemapExcludedSites(string included_sites)
        {
            var sites = _siteService.GetAll();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var site in sites)
            {
                if (!included_sites.Split(',').Contains(site.Id.ToString()))
                    items.Add(new SelectListItem() { Text = site.Name, Value = site.Id.ToString() });
            }
            return items;
        }

        public static List<SelectListItem> GetSitemapNotExcludedSites(string included_sites)
        {
            var sites = _siteService.GetAll();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var site in sites)
            {
                if (included_sites.Split(',').Contains(site.Id.ToString()))
                    items.Add(new SelectListItem() { Text = site.Name, Value = site.Id.ToString() });
            }
            return items;
        }

        public static List<GroupDropListItem> GetClasifications(int? GroupID)
        {
            List<GroupDropListItem> layoutItems = new List<GroupDropListItem>();
            var clasifications = cContext.SelectClasifications(GroupID);
            foreach (var clasification in clasifications)
            {
                List<OptionItem> items = new List<OptionItem>();

                GroupDropListItem group = layoutItems.Where(li => li.Name == clasification.group_name).FirstOrDefault();
                if (group != null)
                {
                    group.Items.Add(new OptionItem() { Text = clasification.classification_name, Value = clasification.classification_id.ToString() });
                }
                else
                {
                    group = new GroupDropListItem();
                    group.Name = clasification.group_name;
                    group.Items = new List<OptionItem>();
                    group.Items.Add(new OptionItem() { Text = clasification.classification_name, Value = clasification.classification_id.ToString() });
                    layoutItems.Add(group);
                }
            }
            return layoutItems;
        }

        public static string GetArticleZoneNamesWithoutArticle(string s)
        {
            string[] ids = s.Split('-');
            return s.Length > 0 ? aContext.SelectArticleZonesNames(Convert.ToInt32(ids[0]), -1)[0] : string.Empty;
        }

        public static string GetArticleZoneNames(string s)
        {
            string returnValue = string.Empty;
            string[] ids = s.Split('-');
            if (ids != null)
            {
                if (ids.Count() > 1)
                {
                    returnValue = s.Length > 0 ? aContext.SelectArticleZonesNames(Convert.ToInt32(ids[0]), Convert.ToInt32(ids[1]))[0] : string.Empty;
                }
            }
            return returnValue;
        }

        public static string GetArticleHeadline(string s)
        {
            string returnVal = "";
            try
            {
                string[] ids = s.Split('-');
                if (ids.Length > 1)
                {
                    returnVal = aContext.SelectArticleDetails(Convert.ToInt32(ids[0]), Convert.ToInt32(ids[1])).headline;
                }
            }
            catch (Exception ex)
            {
                returnVal = "";
            }
            return returnVal;
        }

        public static List<SelectListItem> GetGroups(StructureGroupType GroupType, int? GroupID)
        {
            List<SelectListItem> groups = new List<SelectListItem>();
            foreach (var sg in sgContext.SelectStructureGroupByType((int)GroupType))
            {
                groups.Add(new SelectListItem
                {
                    Value = sg.group_id.ToString(),
                    Text = sg.group_name,
                    Selected = GroupID != null && GroupID == sg.group_id ? true : false
                });
            }
            return groups;
        }

        public static List<SelectListItem> GetArticleWithAllPath(string formatted)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            if (formatted != "0-0" && formatted != "0")
            {

                items.AddRange(formatted.Split(',')
                    .Select(t => new SelectListItem()
                    {
                        Value = t,
                        Text = Bags.GetArticleZoneNames(t.ToString().IndexOf("-") == -1 ? "0-" + t : t)
                    })
                );
            }

            return items;
        }

        public static List<SelectListItem> GetArticleOption(string formatted)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.AddRange(formatted.Split(',')
                .Select(t => new SelectListItem()
                {
                    Value = formatted,
                    Text = Bags.GetArticleHeadline(t)
                })
            );

            return items;
        }

        public static List<SelectListItem> GetZones(int selectedZoneId)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var result = zContext.SelectAllZones();

            items.AddRange(result.Select(t =>
                new SelectListItem()
                {
                    Value = t.zone_id.ToString(),
                    Text = string.Format("{0} - {1} / {2} / {3}", HttpUtility.HtmlDecode(t.zone_id.ToString()), HttpUtility.HtmlDecode(t.site_name), HttpUtility.HtmlDecode(t.zone_group_name), HttpUtility.HtmlDecode(t.zone_name)),
                    Selected = t.zone_id.Equals(selectedZoneId)
                })
            );

            return items;
        }

        public static List<SelectListItem> GetZonesByZoneGroup(int SelectedZoneId, int ZoneGroupId)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var result = zContext.SelectZonesByZoneGroup(ZoneGroupId);

            items.AddRange(result.Select(t =>
                new SelectListItem()
                {
                    Value = t.zone_id.ToString(),
                    Text = HttpUtility.HtmlDecode(t.zone_name),
                    Selected = t.zone_id.Equals(SelectedZoneId)
                })
            );

            return items;
        }

        public static List<GroupDropListItem> GetZoneGroupsBySite(int? SiteID, int? ZoneGroupID)
        {
            List<GroupDropListItem> gItems = new List<GroupDropListItem>();
            var zgs = _zoneGroupService.GetAllByParentId(SiteID ?? 0);
            foreach (var zg in zgs)
            {
                List<OptionItem> items = new List<OptionItem>();

                GroupDropListItem group = gItems.Where(li => li.Name == zg.Site.Name).FirstOrDefault();
                if (group != null)
                {
                    group.Items.Add(new OptionItem() { Text = zg.Name, Value = zg.Id.ToString() });
                }
                else
                {
                    group = new GroupDropListItem();
                    group.Name = zg.Site.Name;
                    group.Items = new List<OptionItem>();
                    group.Items.Add(new OptionItem() { Text = zg.Name, Value = zg.Id.ToString() });
                    gItems.Add(group);
                }
            }

            return gItems;
        }

        public static List<string> GetZoneGroupsBySiteForAuth(int? SiteID, int? ZoneGroupID)
        {
            List<string> gItems = new List<string>();
            var zgs = _zoneGroupService.GetAllByParentId(SiteID ?? 0);
            foreach (var zg in zgs)
            {
                gItems.Add(zg.Id.ToString());
            }
            return gItems;
        }

        public static List<GroupDropListItem> GetRssChannels(int? ChannelID)
        {

            List<GroupDropListItem> gItems = new List<GroupDropListItem>();
            var channels = rfContext.SelectRssChannels(null);
            foreach (var rf in channels)
            {
                List<OptionItem> items = new List<OptionItem>();
                OptionItem item = new OptionItem()
                {
                    Value = rf.channel_id.ToString(),
                    Text = string.Format("{0} [{1}]", rf.channel_name, rf.publisher_name)
                };

                GroupDropListItem group = gItems.Where(li => li.Name == rf.group_name).FirstOrDefault();
                if (group != null)
                {
                    group.Items.Add(item);
                }
                else
                {
                    group = new GroupDropListItem();
                    group.Name = rf.channel_name;
                    group.Items = new List<OptionItem>();
                    group.Items.Add(item);
                    gItems.Add(group);
                }
            }
            //Selected = ChannelID != null && ChannelID == rf.channel_id ? true : false
            return gItems;
        }

        public static List<SelectListItem> GetXmlList(int? XmlId)
        {
            var result = xgContext.SelectXmls(6);
            List<SelectListItem> xmls = new List<SelectListItem>();
            foreach (var xml in result)
            {
                xmls.Add(new SelectListItem
                {
                    Value = xml.xml_id.ToString(),
                    Text = xml.xml_name,
                    Selected = XmlId == xml.group_id ? true : false
                });
            }
            return xmls;
        }

        public static List<SelectListItem> GetFtpFolderFiles()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string path = HttpContext.Current.Server.MapPath("/i/ftp");

            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles("*.*"))
                {
                    items.Add(new SelectListItem() { Text = fi.Name, Value = fi.Name });
                }
            }

            return items;
        }

        public static string MapPathReverse(string fullServerPath)
        {
            return "/" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty).Replace("\\", "/");
        }
    }
}
