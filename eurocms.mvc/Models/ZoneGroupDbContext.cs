using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EuroCMS.Admin.entity;
using EuroCMS.Model;

namespace EuroCMS.Admin.Models
{
    public class ZoneGroupDbContext : BaseDbContext
    {
        public DbSet<cms_zone_groups> ZoneGroups { get; set; }

        public ZoneGroupDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_zone_groups>()
                .Map(m => m.ToTable("cms_zone_groups"));


            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_admin_update_zonegroups_Result> UpdateZoneGroup(cms_zone_groups zg)
        {
            List<cms_asp_admin_update_zonegroups_Result> returnList = new List<cms_asp_admin_update_zonegroups_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            ZoneGroupRepository zgr = new ZoneGroupRepository();
            ZoneGroupService zoneGroupService = new ZoneGroupService(zgr);

            SiteRepository sr = new SiteRepository();
            SiteService siteService = new SiteService(sr);

            Site getSite = dbContext.Sites.Where(s => s.Id == zg.site_id).FirstOrDefault();

            ZoneGroup getZoneG = new ZoneGroup();
            getZoneG = dbContext.ZoneGroups.Where(z => z.Id != zg.zone_group_id && z.Name == zg.zone_group_name && z.SiteId == zg.site_id).FirstOrDefault();

            cms_asp_admin_update_zonegroups_Result returnItem = new cms_asp_admin_update_zonegroups_Result();

            if (getZoneG != null)
            {
                // zone group exist.. update

                ZoneGroup updateZoneG = new ZoneGroup();
                updateZoneG = dbContext.ZoneGroups.Where(z => z.Id == zg.zone_group_id).FirstOrDefault();

                if (updateZoneG != null)
                {
                    updateZoneG.Name = zg.zone_group_name;
                    updateZoneG.CssMerge = zg.css_merge;
                    updateZoneG.SiteId = zg.site_id;
                    updateZoneG.CssId = zg.css_id;
                    updateZoneG.MobileCssId = zg.css_id_mobile;
                    updateZoneG.PrintCssId = zg.css_id_print;
                    updateZoneG.TemplateId = zg.template_id;
                    updateZoneG.MobileTemplateId = zg.template_id_mobile;
                    updateZoneG.CustomBody = zg.custom_body;
                    updateZoneG.Keywords = zg.zone_group_keywords;
                    updateZoneG.Analytics = zg.analytics;
                    updateZoneG.TagArticle = zg.tag_detail_article;
                    updateZoneG.Article1 = HttpUtility.HtmlEncode(zg.article_1);
                    updateZoneG.Article2 = HttpUtility.HtmlEncode(zg.article_2);
                    updateZoneG.Article3 = HttpUtility.HtmlEncode(zg.article_3);
                    updateZoneG.Article4 = HttpUtility.HtmlEncode(zg.article_4);
                    updateZoneG.Article5 = HttpUtility.HtmlEncode(zg.article_5);
                    updateZoneG.Append1 = zg.append_1;
                    updateZoneG.Append2 = zg.append_2;
                    updateZoneG.Append3 = zg.append_3;
                    updateZoneG.Append4 = zg.append_4;
                    updateZoneG.Append5 = zg.append_5;
                    updateZoneG.Updated = DateTime.Now;
                    updateZoneG.MetaDescription = HttpUtility.HtmlEncode(zg.meta_description);
                    updateZoneG.DisplayName = zg.zone_group_name_display;
                    updateZoneG.Content1EditorType = zg.content_1_editor_type;
                    updateZoneG.Content2EditorType = zg.content_2_editor_type;
                    updateZoneG.Content3EditorType = zg.content_3_editor_type;
                    updateZoneG.Content4EditorType = zg.content_4_editor_type;
                    updateZoneG.Content5EditorType = zg.content_5_editor_type;
                    updateZoneG.DefaultArticle = zg.default_article;
                    updateZoneG.OmnitureCode = HttpUtility.HtmlEncode(zg.omniture_code);

                    zoneGroupService.Update(updateZoneG);

                    returnItem.zgStat = "U";
                    returnItem.zone_group_id = zg.zone_group_id;
                    returnItem.site_name = getSite.Name;
                    returnItem.created = DateTime.Now;
                }
                else
                {
                    ZoneGroup insertZoneG = new ZoneGroup();
                    insertZoneG.Name = zg.zone_group_name;
                    insertZoneG.CssMerge = zg.css_merge;
                    insertZoneG.SiteId = zg.site_id;
                    insertZoneG.CssId = zg.css_id;
                    insertZoneG.MobileCssId = zg.css_id_mobile;
                    insertZoneG.PrintCssId = zg.css_id_print;
                    insertZoneG.TemplateId = zg.template_id;
                    insertZoneG.MobileTemplateId = zg.template_id_mobile;
                    insertZoneG.CustomBody = zg.custom_body;
                    insertZoneG.CreatedBy = (Guid)zg.publisher_id;
                    insertZoneG.Keywords = zg.zone_group_keywords;
                    insertZoneG.Analytics = zg.analytics;
                    insertZoneG.TagArticle = zg.tag_detail_article;
                    insertZoneG.Article1 = HttpUtility.HtmlEncode(zg.article_1);
                    insertZoneG.Article2 = HttpUtility.HtmlEncode(zg.article_2);
                    insertZoneG.Article3 = HttpUtility.HtmlEncode(zg.article_3);
                    insertZoneG.Article4 = HttpUtility.HtmlEncode(zg.article_4);
                    insertZoneG.Article5 = HttpUtility.HtmlEncode(zg.article_5);
                    insertZoneG.Append1 = zg.append_1;
                    insertZoneG.Append2 = zg.append_2;
                    insertZoneG.Append3 = zg.append_3;
                    insertZoneG.Append4 = zg.append_4;
                    insertZoneG.Append5 = zg.append_5;
                    insertZoneG.Updated = DateTime.Now;
                    insertZoneG.MetaDescription = HttpUtility.HtmlEncode(zg.meta_description);
                    insertZoneG.DisplayName = zg.zone_group_name_display;
                    insertZoneG.Content1EditorType = zg.content_1_editor_type;
                    insertZoneG.Content2EditorType = zg.content_2_editor_type;
                    insertZoneG.Content3EditorType = zg.content_3_editor_type;
                    insertZoneG.Content4EditorType = zg.content_4_editor_type;
                    insertZoneG.Content5EditorType = zg.content_5_editor_type;
                    insertZoneG.DefaultArticle = zg.default_article;
                    insertZoneG.OmnitureCode = HttpUtility.HtmlEncode(zg.omniture_code);

                    ZoneGroup iZoneG = zoneGroupService.Insert(insertZoneG);

                    returnItem.zgStat = "I";
                    returnItem.zone_group_id = iZoneG.Id;
                    returnItem.site_name = getSite.Name;
                    returnItem.created = DateTime.Now;
                }
            }
            else
            {
                returnItem.zgStat = "D";
                returnItem.zone_group_id = "";
                returnItem.site_name = "";
                returnItem.created = "";
            }

            returnList.Add(returnItem);

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_update_zonegroups_Result>
            //    ("dbo.cms_asp_admin_update_zonegroups @zone_group_id={0}, " +
            //        "@zone_group_name={1}, " +
            //        "@zone_group_keywords={2}, " +
            //        "@analytics={3}, " +
            //        "@site_id={4}," +
            //        "@css_merge={5}, " +
            //        "@css_id={6}, " +
            //        "@css_id_mobile={7}, " +
            //        "@css_id_print={8}, " +
            //        "@template_id={9}, " +
            //        "@template_id_mobile={10}," +
            //        "@custom_body={11}," +
            //        "@tag_detail_article={12}, " +
            //        "@article_1={13}, " +
            //        "@article_2={14}, " +
            //        "@article_3={15}, " +
            //        "@article_4={16}, " +
            //        "@article_5={17}, " +
            //        "@append_1={18}, " +
            //        "@append_2={19}, " +
            //        "@append_3={20}, " +
            //        "@append_4={21}, " +
            //        "@append_5={22}, " +
            //        "@publisher_id={23}, " +
            //        "@meta_description={24}, " +
            //        "@zone_group_name_display={25}, " +
            //        "@content_1_editor_type={26}, " +
            //        "@content_2_editor_type={27}, " +
            //        "@content_3_editor_type={28}, " +
            //        "@content_4_editor_type={29}, " +
            //        "@content_5_editor_type={30}, " +
            //        "@default_article={31}, " +
            //        "@omniture_code={32}",
            //        zg.zone_group_id,
            //        zg.zone_group_name,
            //        zg.zone_group_keywords,
            //        zg.analytics,
            //        zg.site_id,
            //        zg.css_merge,
            //        zg.css_id,
            //        zg.css_id_mobile,
            //        zg.css_id_print,
            //        zg.template_id,
            //        zg.template_id_mobile,
            //        zg.custom_body,
            //        zg.tag_detail_article,
            //        HttpUtility.HtmlEncode(zg.article_1),
            //        HttpUtility.HtmlEncode(zg.article_2),
            //        HttpUtility.HtmlEncode(zg.article_3),
            //        HttpUtility.HtmlEncode(zg.article_4),
            //        HttpUtility.HtmlEncode(zg.article_5),
            //        zg.append_1,
            //        zg.append_2,
            //        zg.append_3,
            //        zg.append_4,
            //        zg.append_5,
            //        zg.publisher_id,
            //        HttpUtility.HtmlEncode(zg.meta_description),
            //        zg.zone_group_name_display,
            //        zg.content_1_editor_type,
            //        zg.content_2_editor_type,
            //        zg.content_3_editor_type,
            //        zg.content_4_editor_type,
            //        zg.content_5_editor_type,
            //        zg.default_article,
            //        HttpUtility.HtmlEncode(zg.omniture_code))
            //    .ToList();
        }

        public List<cms_asp_admin_update_zonegroups_Result> CreateZoneGroup(cms_zone_groups zg)
        {

            List<cms_asp_admin_update_zonegroups_Result> returnList = new List<cms_asp_admin_update_zonegroups_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            ZoneGroupRepository zgr = new ZoneGroupRepository();
            ZoneGroupService zoneGroupService = new ZoneGroupService(zgr);

            SiteRepository sr = new SiteRepository();
            SiteService siteService = new SiteService(sr);

            Site getSite = new Site();
            getSite = dbContext.Sites.Where(s => s.Id == zg.site_id).FirstOrDefault();

            cms_asp_admin_update_zonegroups_Result returnItem = new cms_asp_admin_update_zonegroups_Result();


            ZoneGroup insertZoneG = new ZoneGroup();
            insertZoneG.Name = zg.zone_group_name;
            insertZoneG.CssMerge = zg.css_merge;
            insertZoneG.SiteId = zg.site_id;
            insertZoneG.CssId = zg.css_id;
            insertZoneG.MobileCssId = zg.css_id_mobile;
            insertZoneG.PrintCssId = zg.css_id_print;
            insertZoneG.TemplateId = zg.template_id;
            insertZoneG.MobileTemplateId = zg.template_id_mobile;
            insertZoneG.CustomBody = zg.custom_body;
            insertZoneG.CreatedBy = (Guid)zg.publisher_id;
            insertZoneG.Keywords = zg.zone_group_keywords;
            insertZoneG.Analytics = zg.analytics;
            insertZoneG.TagArticle = zg.tag_detail_article;
            insertZoneG.Article1 = HttpUtility.HtmlEncode(zg.article_1);
            insertZoneG.Article2 = HttpUtility.HtmlEncode(zg.article_2);
            insertZoneG.Article3 = HttpUtility.HtmlEncode(zg.article_3);
            insertZoneG.Article4 = HttpUtility.HtmlEncode(zg.article_4);
            insertZoneG.Article5 = HttpUtility.HtmlEncode(zg.article_5);
            insertZoneG.Append1 = zg.append_1;
            insertZoneG.Append2 = zg.append_2;
            insertZoneG.Append3 = zg.append_3;
            insertZoneG.Append4 = zg.append_4;
            insertZoneG.Append5 = zg.append_5;
            insertZoneG.Updated = DateTime.Now;
            insertZoneG.MetaDescription = HttpUtility.HtmlEncode(zg.meta_description);
            insertZoneG.DisplayName = zg.zone_group_name_display;
            insertZoneG.Content1EditorType = zg.content_1_editor_type;
            insertZoneG.Content2EditorType = zg.content_2_editor_type;
            insertZoneG.Content3EditorType = zg.content_3_editor_type;
            insertZoneG.Content4EditorType = zg.content_4_editor_type;
            insertZoneG.Content5EditorType = zg.content_5_editor_type;
            insertZoneG.DefaultArticle = zg.default_article;
            insertZoneG.OmnitureCode = HttpUtility.HtmlEncode(zg.omniture_code);

            ZoneGroup iZoneG = zoneGroupService.Insert(insertZoneG);

            returnItem.zgStat = "I";
            returnItem.zone_group_id = iZoneG.Id;
            returnItem.site_name = getSite.Name;
            returnItem.created = DateTime.Now;

            returnList.Add(returnItem);
            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_update_zonegroups_Result>
            //("dbo.cms_asp_admin_update_zonegroups @zone_group_id={0}, " +
            //    "@zone_group_name={1}, " +
            //    "@zone_group_keywords={2}, " +
            //    "@analytics={3}, " +
            //    "@site_id={4}," +
            //    "@css_merge={5}, " +
            //    "@css_id={6}, " +
            //    "@css_id_mobile={7}, " +
            //    "@css_id_print={8}, " +
            //    "@template_id={9}, " +
            //    "@template_id_mobile={10}," +
            //    "@custom_body={11}," +
            //    "@tag_detail_article={12}, " +
            //    "@article_1={13}, " +
            //    "@article_2={14}, " +
            //    "@article_3={15}, " +
            //    "@article_4={16}, " +
            //    "@article_5={17}, " +
            //    "@append_1={18}, " +
            //    "@append_2={19}, " +
            //    "@append_3={20}, " +
            //    "@append_4={21}, " +
            //    "@append_5={22}, " +
            //    "@publisher_id={23}, " +
            //    "@meta_description={24}, " +
            //    "@zone_group_name_display={25}, " +
            //    "@content_1_editor_type={26}, " +
            //    "@content_2_editor_type={27}, " +
            //    "@content_3_editor_type={28}, " +
            //    "@content_4_editor_type={29}, " +
            //    "@content_5_editor_type={30}, " +
            //    "@default_article={31}, " +
            //    "@omniture_code={32}",
            //    -1,
            //    zg.zone_group_name,
            //    zg.zone_group_keywords,
            //    zg.analytics,
            //    zg.site_id,
            //    zg.css_merge,
            //    zg.css_id,
            //    zg.css_id_mobile,
            //    zg.css_id_print,
            //    zg.template_id,
            //    zg.template_id_mobile,
            //    zg.custom_body,
            //    zg.tag_detail_article,
            //    HttpUtility.HtmlEncode(zg.article_1),
            //    HttpUtility.HtmlEncode(zg.article_2),
            //    HttpUtility.HtmlEncode(zg.article_3),
            //    HttpUtility.HtmlEncode(zg.article_4),
            //    HttpUtility.HtmlEncode(zg.article_5),
            //    zg.append_1,
            //    zg.append_2,
            //    zg.append_3,
            //    zg.append_4,
            //    zg.append_5,
            //    zg.publisher_id,
            //    HttpUtility.HtmlEncode(zg.meta_description),
            //    zg.zone_group_name_display,
            //    zg.content_1_editor_type,
            //    zg.content_2_editor_type,
            //    zg.content_3_editor_type,
            //    zg.content_4_editor_type,
            //    zg.content_5_editor_type,
            //    zg.default_article,
            //    HttpUtility.HtmlEncode(zg.omniture_code))
            //.ToList();
        }

        public List<cms_asp_admin_delete_zonegroup_Result> DeleteZoneGroup(int ZoneGroupID, object publisherID, int PublisherLevel)
        {
            List<cms_asp_admin_delete_zonegroup_Result> returnList = new List<cms_asp_admin_delete_zonegroup_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            ZoneGroupRepository zgr = new ZoneGroupRepository();
            ZoneGroupService zoneGroupService = new ZoneGroupService(zgr);

            ZoneRepository zr = new ZoneRepository();
            ZoneService zoneService = new ZoneService(zr);

            cms_asp_admin_delete_zonegroup_Result returnItem = new cms_asp_admin_delete_zonegroup_Result();

            ZoneGroup getZoneG = new ZoneGroup();
            getZoneG = dbContext.ZoneGroups.Where(z => z.Id == ZoneGroupID).FirstOrDefault();
            if (getZoneG != null)
            {
                Zone getZone = new Zone();
                getZone = dbContext.Zones.Where(z => z.ZoneGroupId == ZoneGroupID && z.Status != "D").FirstOrDefault();
                if (getZone != null)
                {
                    returnItem.rCode = "3";
                    returnItem.found_name = getZone.Name;
                    returnList.Add(returnItem);
                    return returnList;
                }

                List<ZoneGroup> deleteZoneGroups = new List<ZoneGroup>();
                deleteZoneGroups = dbContext.ZoneGroups.Where(z => z.Id == ZoneGroupID).ToList();
                foreach (ZoneGroup zoneGroup in deleteZoneGroups)
                {
                    zoneGroupService.Delete(zoneGroup);
                }

                returnItem.rCode = "0";
                returnItem.found_name = "";
                returnList.Add(returnItem);
                return returnList;
            }
            else
            {
                returnItem.rCode = "1";
                returnItem.found_name = "";
            }

            returnList.Add(returnItem);
            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_delete_zonegroup_Result>
            //("dbo.cms_asp_admin_delete_zonegroup @zone_group_id={0},@publisher_id={1},@publisher_level={2}",
            //ZoneGroupID, publisherID, PublisherLevel)
            //.ToList();
        }

        public List<cms_asp_select_zone_group_details_Result> SelectZoneGroup(int ZoneGroupID)
        {
            List<cms_asp_select_zone_group_details_Result> returnList = new List<cms_asp_select_zone_group_details_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ZoneGroup> listZoneGroup = new List<ZoneGroup>();

            listZoneGroup = dbContext.ZoneGroups.Where(z => z.Id == ZoneGroupID).ToList();
            foreach (ZoneGroup zoneG in listZoneGroup)
            {
                cms_asp_select_zone_group_details_Result returnItem = new cms_asp_select_zone_group_details_Result();
                returnItem.zone_group_id = zoneG.Id;
                returnItem.zone_group_name = zoneG.Name;
                returnItem.zone_group_keywords = zoneG.Keywords;
                returnItem.site_id = zoneG.SiteId;
                returnItem.css_merge = zoneG.CssMerge;
                returnItem.css_id = zoneG.CssId;
                returnItem.css_id_print = zoneG.PrintCssId;
                returnItem.css_id_mobile = zoneG.MobileCssId;
                returnItem.template_id = zoneG.TemplateId;
                returnItem.template_id_mobile = zoneG.MobileTemplateId;
                returnItem.custom_body = zoneG.CustomBody;
                returnItem.publisher_id = zoneG.CreatedBy;
                returnItem.created = zoneG.Created;
                returnItem.updated = zoneG.Updated;
                returnItem.article_1 = zoneG.Article1;
                returnItem.article_2 = zoneG.Article2;
                returnItem.article_3 = zoneG.Article3;
                returnItem.article_4 = zoneG.Article4;
                returnItem.article_5 = zoneG.Article5;
                returnItem.append_1 = zoneG.Append1;
                returnItem.append_2 = zoneG.Append2;
                returnItem.append_3 = zoneG.Append3;
                returnItem.append_4 = zoneG.Append4;
                returnItem.append_5 = zoneG.Append5;
                returnItem.analytics = zoneG.Analytics;
                returnItem.tag_detail_article = zoneG.TagArticle;
                returnItem.meta_description = zoneG.MetaDescription;
                returnItem.zone_group_name_display = zoneG.DisplayName;
                returnItem.content_1_editor_type = zoneG.Content1EditorType;
                returnItem.content_2_editor_type = zoneG.Content2EditorType;
                returnItem.content_3_editor_type = zoneG.Content3EditorType;
                returnItem.content_4_editor_type = zoneG.Content4EditorType;
                returnItem.content_5_editor_type = zoneG.Content5EditorType;
                returnItem.default_article = zoneG.DefaultArticle;
                returnItem.omniture_code = zoneG.OmnitureCode;
                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_zone_group_details_Result>
            //("dbo.cms_asp_select_zone_group_details @zone_group_id={0}",
            //ZoneGroupID)
            //.ToList();
        }

        public List<cms_asp_select_zones_groups_by_site_Result> SelectZoneGroupsBySite(int? SiteID)
        {
            List<cms_asp_select_zones_groups_by_site_Result> returnList = new List<cms_asp_select_zones_groups_by_site_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            int siteId = SiteID == null ? -1 : Convert.ToInt32(SiteID);

            List<ZoneGroup> listZoneG = new List<ZoneGroup>();

            if (siteId == -1)
            {
                listZoneG = dbContext.ZoneGroups.OrderBy(o => o.Site.Name).ThenBy(t => t.Name).ToList();
            }
            else
            {
                listZoneG = dbContext.ZoneGroups.Where(z => z.SiteId == siteId).OrderBy(o => o.Site.Name).ThenBy(t => t.Name).ToList();
            }


            foreach (ZoneGroup zoneG in listZoneG)
            {
                cms_asp_select_zones_groups_by_site_Result returnItem = new cms_asp_select_zones_groups_by_site_Result();

                Site getSite = new Site();
                getSite = dbContext.Sites.Where(s => s.Id == zoneG.SiteId).FirstOrDefault();

                vAspNetMembershipUser getUser = new vAspNetMembershipUser();
                getUser = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zoneG.CreatedBy).FirstOrDefault();

                returnItem.zone_group_id = zoneG.Id;
                returnItem.zone_group_name = zoneG.Name;
                returnItem.publisher_id = zoneG.CreatedBy;
                returnItem.site_name = getSite.Name;
                returnItem.publisher_name = getUser.UserName;
                returnItem.created = zoneG.Created;
                returnItem.updated = zoneG.Updated;

                returnList.Add(returnItem);
            }


            return returnList;


            //return this.Database.SqlQuery<cms_asp_select_zones_groups_by_site_Result>
            //("dbo.cms_asp_select_zones_groups_by_site @site_id={0}",
            //SiteID == null ? -1 : SiteID)
            //.ToList();
        }
    }
}