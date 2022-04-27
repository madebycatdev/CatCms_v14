using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class HiddenValueDbContext: BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_hidden_values> Xmls { get; set; }

        public HiddenValueDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_hidden_values>()
                .Map(m => m.ToTable("cms_hidden_values"));
 
             base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_hidden_values_Result> SelectAll()
        {
            return this.Database.SqlQuery<cms_asp_select_hidden_values_Result>
                ("dbo.cms_asp_select_hidden_values")
                .ToList();
        }

        public List<cms_asp_select_hidden_details_Result> Select(int hvId)
        {
            return this.Database.SqlQuery<cms_asp_select_hidden_details_Result>
                ("dbo.cms_asp_select_hidden_details @hidden_id={0}",
                hvId)
                .ToList();
        }

        public List<cms_asp_admin_update_hidden_values_Result> Update(cms_hidden_values hv)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_hidden_values_Result>
                 ("dbo.cms_asp_admin_update_hidden_values " +
                    "@hidden_id={0}," +
                    "@hidden_value={1}," +
                    "@hidden_type={2}," +
                    "@hidden_desc={3}," +
                    "@publisher_id={4}",
                 hv.hidden_id,
                 hv.hidden_value,
                 hv.hidden_type,
                 hv.hidden_desc,
                 hv.created_by).ToList();
        }

        public List<cms_asp_admin_delete_hidden_value_Result> Delete(int hvId, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_hidden_value_Result>
                ("dbo.cms_asp_admin_delete_hidden_value @hidden_id={0},@publisher_id={1},@publisher_level={2}",
                hvId,
                publisherID,
                Publisherlevel)
                .ToList();
        }
    }
}