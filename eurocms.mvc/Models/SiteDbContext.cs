using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class SiteDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_sites> Sites { get; set; }
         
        public SiteDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_sites>()
                .Map(m => m.ToTable("cms_sites"))
                .HasKey(m => m.domain_id)
                .HasRequired(g => g.domains)
                .WithOptional();
 
             base.OnModelCreating(modelBuilder);
        }
 
        public List<cms_asp_select_sites_Result> SelectSites(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_sites_Result>
                ("dbo.cms_asp_select_sites @group_id={0}",
                GroupID==null?-1:GroupID)
                .ToList();
        }

        public List<cms_asp_select_site_details_Result> SelectSite(int SiteID)
        {
            return this.Database.SqlQuery<cms_asp_select_site_details_Result>
                ("dbo.cms_asp_select_site_details @site_id={0}",
                SiteID)
                .ToList();
        }

        public List<cms_asp_admin_update_site_Result> CreateSite(cms_asp_select_site_details_Result site)
        {
            var result = this.Database.SqlQuery<cms_asp_admin_update_site_Result>
                ("dbo.cms_asp_admin_update_site  @site_id={0}," +
	                                        "@site_name={1},"+
	                                        "@css_id={2},"+
	                                        "@css_id_mobile={3},"+
	                                        "@css_id_print={4},"+
	                                        "@template_id={5},"+
	                                        "@template_id_mobile={6},"+
	                                        "@site_keywords={7},"+
	                                        "@site_header={8},"+
	                                        "@site_js={9},"+
	                                        "@analytics={10},"+
	                                        "@custom_body={11},"+
	                                        "@site_icon={12},"+
	                                        "@tag_detail_article={13},"+
	                                        "@article_1={14},"+
	                                        "@article_2={15},"+
	                                        "@article_3={16},"+
	                                        "@article_4={17},"+
	                                        "@article_5={18},"+
	                                        "@publisher_id={19},"+
	                                        "@group_id={20},"+
	                                        "@structure_description={21},"+
	                                        "@meta_description={22},"+
	                                        "@content_1_editor_type={23},"+
	                                        "@content_2_editor_type={24},"+
	                                        "@content_3_editor_type={25},"+
	                                        "@content_4_editor_type={26},"+
	                                        "@content_5_editor_type={27},"+
	                                        "@default_article={28},"+
	                                        "@omniture_code={29},"+
                                            "@domain_id={30}",
                         -1,
                        site.site_name,
                        site.css_id,
                        site.css_id_mobile,
	                    site.css_id_print,
                        site.template_id,
                        site.template_id_mobile,
                        site.site_keywords,
                        site.site_header,
                        site.site_js,
                        site.analytics,
                        site.custom_body,
                        site.site_icon,
                        site.tag_detail_article,
                        HttpUtility.HtmlEncode(site.article_1),
                        HttpUtility.HtmlEncode(site.article_2),
                        HttpUtility.HtmlEncode(site.article_3),
                        HttpUtility.HtmlEncode(site.article_4),
                        HttpUtility.HtmlEncode(site.article_5),
                        site.publisher_id,
                        site.group_id,
                        site.structure_description,
                        site.meta_description,
                        site.content_1_editor_type,
                        site.content_2_editor_type,
                        site.content_3_editor_type,
                        site.content_4_editor_type,
                        site.content_5_editor_type,
                        site.default_article,
                        site.omniture_code,
                        site.domain_id
                     )
                 .ToList();

            return result;
        }

        public List<cms_asp_admin_update_site_Result> UpdateSite(cms_asp_select_site_details_Result site)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_site_Result>
                 ("dbo.cms_asp_admin_update_site  @site_id={0}," +
                                             "@site_name={1}," +
                                             "@css_id={2}," +
                                             "@css_id_mobile={3}," +
                                             "@css_id_print={4}," +
                                             "@template_id={5}," +
                                             "@template_id_mobile={6}," +
                                             "@site_keywords={7}," +
                                             "@site_header={8}," +
                                             "@site_js={9}," +
                                             "@analytics={10}," +
                                             "@custom_body={11}," +
                                             "@site_icon={12}," +
                                             "@tag_detail_article={13}," +
                                             "@article_1={14}," +
                                             "@article_2={15}," +
                                             "@article_3={16}," +
                                             "@article_4={17}," +
                                             "@article_5={18}," +
                                             "@publisher_id={19}," +
                                             "@group_id={20}," +
                                             "@structure_description={21}," +
                                             "@meta_description={22}," +
                                             "@content_1_editor_type={23}," +
                                             "@content_2_editor_type={24}," +
                                             "@content_3_editor_type={25}," +
                                             "@content_4_editor_type={26}," +
                                             "@content_5_editor_type={27}," +
                                             "@default_article={28}," +
                                             "@omniture_code={29}," +
                                             "@domain_id={30}",
                         site.site_id,
                         site.site_name,
                         site.css_id,
                         site.css_id_mobile,
                         site.css_id_print,
                         site.template_id,
                         site.template_id_mobile,
                         site.site_keywords,
                         site.site_header,
                         site.site_js,
                         site.analytics,
                         site.custom_body,
                         site.site_icon,
                         site.tag_detail_article,
                         HttpUtility.HtmlEncode(site.article_1),
                         HttpUtility.HtmlEncode(site.article_2),
                         HttpUtility.HtmlEncode(site.article_3),
                         HttpUtility.HtmlEncode(site.article_4),
                         HttpUtility.HtmlEncode(site.article_5),
                         site.publisher_id,
                         site.group_id,
                         site.structure_description,
                         site.meta_description,
                         site.content_1_editor_type,
                         site.content_2_editor_type,
                         site.content_3_editor_type,
                         site.content_4_editor_type,
                         site.content_5_editor_type,
                         site.default_article,
                         site.omniture_code,
                         site.domain_id
                      )
                  .ToList();
        }

        public List<cms_asp_admin_delete_site_Result> DeleteSite(int SiteID, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_site_Result>
                ("dbo.cms_asp_admin_delete_site @site_id = {0}, @publisher_id = {1}, @publisher_level = {2}",
                    SiteID,
                    publisherID,
                    PublisherLevel)
                .ToList();
        }
    }
}