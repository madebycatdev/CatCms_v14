using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class CustomContentDbContext: BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_custom_content> Xmls { get; set; }

        public CustomContentDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_custom_content>()
                .Map(m => m.ToTable("cms_custom_content"));
 
             base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_custom_contents_Result> SelectAll(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_custom_contents_Result>
                ("dbo.cms_asp_select_custom_contents @group_id={0}",
                GroupID == null ? -1 : GroupID)
                .ToList();
        }

        public List<cms_asp_select_cc_details_Result> Select(int CCId)
        {
            return this.Database.SqlQuery<cms_asp_select_cc_details_Result>
                ("dbo.cms_asp_select_cc_details @cc_id={0}",
                CCId)
                .ToList();
        }

        public List<cms_asp_admin_update_cc_Result> Update(cms_custom_content cc)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_cc_Result>
                 ("dbo.cms_asp_admin_update_cc " +
                    "@cc_id={0}," +
                    "@cc_name={1}," +
                    "@cc_html={2}," +
                    "@publisher_id={3}," +
                    "@group_id={4}," +
                    "@structure_description={5}",
                 cc.cc_id,
                 cc.cc_name,
                 HttpUtility.HtmlEncode(cc.cc_html),
                 cc.created_by,
                 cc.group_id,
                 cc.structure_description).ToList();
        }

        public List<cms_asp_admin_delete_cc_Result> Delete(int CCId, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_cc_Result>
                ("dbo.cms_asp_admin_delete_cc @cc_id={0},@publisher_id={1},@publisher_level={2}",
                CCId,
                publisherID,
                Publisherlevel)
                .ToList();
        }
    }
}