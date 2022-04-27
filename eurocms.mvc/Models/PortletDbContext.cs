using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class PortletDbContext: BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_portlets> Portlets { get; set; }

        public PortletDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_portlets>()
                .Map(m => m.ToTable("cms_portlets"));
 
             base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_portlets_Result> SelectAll(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_portlets_Result>
                ("dbo.cms_asp_select_portlets @group_id={0}",
                GroupID == null ? -1 : GroupID)
                .ToList();
        }

        public List<cms_asp_select_portlet_details_Result> Select(int PortletId)
        {
            return this.Database.SqlQuery<cms_asp_select_portlet_details_Result>
                ("dbo.cms_asp_select_portlet_details @portlet_id={0}",
                PortletId)
                .ToList();
        }

        public List<cms_asp_admin_update_portlet_Result> Update(cms_portlets portlet)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_portlet_Result>
                 ("dbo.cms_asp_admin_update_portlet " +
                    "@portlet_id={0}," +
                    "@portlet_name={1}, " +
                    "@portlet_status={2}, " +
                    "@portlet_html={3}, " +
                    "@portlet_css={4}, " +
                    "@editor_type={5}, " +
                    "@portlet_header={6}, " +
                    "@portlet_footer={7}, " +
                    "@publisher_id={8}," +
                    "@group_id={9}," +
                    "@structure_description={10}," +
                    "@enable_shortcut={11}",
                     portlet.portlet_id,
                     portlet.portlet_name,
                     portlet.portlet_status,
                     HttpUtility.HtmlEncode(portlet.portlet_html),
                     HttpUtility.HtmlEncode(portlet.portlet_css),
                     portlet.editor_type,
                     HttpUtility.HtmlEncode(portlet.portlet_header),
                     HttpUtility.HtmlEncode(portlet.portlet_footer),
                     portlet.publisher_id,
                     portlet.group_id,
                     portlet.structure_description,
                     portlet.enable_shortcut).ToList();

             
        }

        public List<cms_asp_admin_delete_portlet_Result> Delete(int PortletId, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_portlet_Result>
                ("dbo.cms_asp_admin_delete_portlet @portlet_id={0},@publisher_id={1},@publisher_level={2}",
                PortletId,
                publisherID,
                Publisherlevel)
                .ToList();
        }
    }

    public class PortletPropertyView
    {
        public string ItemSeperator { get; set; }

        public string PrevNextCaption { get; set; }

        public string PagerHeader { get; set; }

        public int PagerPosition { get; set; }

        public int PagerCount { get; set; }

        public string PagerClass { get; set; }

        public string ClassName { get; set; }

        public int ExcludeSelf { get; set; }

        public string ExcludeArticles { get; set; }

        public string IncludeArticles { get; set; }

        public string ContainerTag { get; set; }

        public string PortletHeader { get; set; }

        public int ItemOrdering { get; set; }

        public int ItemCount { get; set; }

        public int ZoneId { get; set; }
    }

    public class SitemapPropertyView
    {
        public string ClassName { get; set; }

        public string ContainerTag { get; set; }

        public int MenuDepth { get; set; }

        public int ItemOrdering { get; set; }

        public int SitemapType { get; set; }

        public int ZoneId { get; set; }

        public string ExcludeZoneIds { get; set; }

        public string ExcludeArticleIds { get; set; }
    }

    public class MenuPropertyView
    {
        
        public int MenuDepth { get; set; }

        public string ClassName { get; set; }

        public string Position { get; set; }

        public bool EliminateSingle { get; set; }

        public bool RemoveOnclikFunction { get; set; }

        public string SelectedItemClass { get; set; }

        public string NotSelectedItemClass { get; set; }

        public string ExcludeArticles { get; set; }

        public string IncludeArticles { get; set; }

        public string ContainerTag { get; set; }

        public string ContainerTagId { get; set; }

        public int ItemOrdering { get; set; }

        public int ZoneId { get; set; }
    }
}