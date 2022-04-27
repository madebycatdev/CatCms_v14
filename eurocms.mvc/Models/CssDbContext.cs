using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class CssDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_css> CssList { get; set; }

        public CssDbContext()
            : base()
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_css>()
                .Map(m => m.ToTable("cms_css"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_css_Result> SelectCssList(int? GroupId)
        {
            return this.Database.SqlQuery<cms_asp_select_css_Result>
                ("dbo.cms_asp_select_css {0}", GroupId ?? -1)
                .ToList();
        }

        public List<cms_asp_admin_select_css_revisions_Result> SelectCssRevisions(int CssId)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_css_revisions_Result>
                ("dbo.cms_asp_admin_select_css_revisions {0}", CssId)
                .ToList();
        }

        public List<cms_asp_select_css_history_code_Result> SelectCssHistoryCode(long HistoryId)
        {
            return this.Database.SqlQuery<cms_asp_select_css_history_code_Result>
                ("dbo.cms_asp_select_css_history_code {0}", HistoryId)
                .ToList();
        }

        public List<cms_asp_select_css_history_code_Result> SelectCssCode(int CssId)
        {
            return this.Database.SqlQuery<cms_asp_select_css_history_code_Result>
                ("dbo.cms_asp_select_css_code {0}", CssId)
                .ToList();
        }

        public List<cms_asp_admin_delete_css_Result> DeleteCssCode(int CssId, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_css_Result>
                ("dbo.cms_asp_admin_delete_css @css_id={0}, @publisher_id={1}, @publisher_level={2}", CssId, publisherID, PublisherLevel)
                .ToList();
        }

        public List<cms_asp_admin_update_css_Result> UpdateCssCode(cms_css css)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_css_Result>
                ("dbo.cms_asp_admin_update_css " +
                "@css_id={0}," +
                "@css_name={1}," +
                "@css_code={2}," +
                "@css_fix={3}," +
                "@css_type={4}," +
                "@css_rel_text={5}," +
                "@css_type_text={6}," +
                "@publisher_id={7}," +
                "@group_id={8}," +
                "@structure_description={9}",
                css.css_id,
                css.css_name,
                css.css_code,
                css.css_fix,
                css.css_type,
                css.css_rel_text,
                css.css_type_text,
                css.publisher_id,
                css.group_id,
                css.structure_description
                )
                .ToList();
        }
    }
}