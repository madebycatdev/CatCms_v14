using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class PluginDbContext: BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_plugins> Plugins { get; set; }

        public PluginDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_plugins>()
                .Map(m => m.ToTable("cms_plugins"));
 
             base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_plugins_Result> SelectAll(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_plugins_Result>
                ("dbo.cms_asp_select_plugins @group_id={0}",
                GroupID == null ? -1 : GroupID)
                .ToList();
        }

        public List<cms_asp_select_plugin_code_Result> Select(int PluginId)
        {
            return this.Database.SqlQuery<cms_asp_select_plugin_code_Result>
                ("dbo.cms_asp_select_plugin_code @plugin_id={0}",
                PluginId)
                .ToList();
        }

        public List<cms_asp_admin_update_plugin_Result> Update(cms_plugins plugin)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_plugin_Result>
                 ("dbo.cms_asp_admin_update_plugin " +
                    "@plugin_id={0}," +
                    "@plugin_name={1}," +
                    "@plugin_code={2}," +
                    "@plugin_status={3}," +
                    "@publisher_id={4}," +
                    "@group_id={5}, " +
                    "@structure_description={6}",
                     plugin.plugin_id,
                     plugin.plugin_name,
                     HttpUtility.HtmlEncode(plugin.plugin_code),
                     plugin.plugin_status,
                     plugin.created_by,
                     plugin.group_id,
                     plugin.structure_description).ToList();
        }

        public List<cms_asp_admin_delete_plugin_Result> Delete(int PluginId, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_plugin_Result>
                ("dbo.cms_asp_admin_delete_plugin @plugin_id={0},@publisher_id={1},@publisher_level={2}",
                PluginId,
                publisherID,
                Publisherlevel)
                .ToList();
        }
    }
}