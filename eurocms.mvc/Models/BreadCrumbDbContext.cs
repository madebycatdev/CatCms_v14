using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class BreadCrumbDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_breadcrumbs> BreadCrumbs { get; set; }

        public BreadCrumbDbContext()
            : base()
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_breadcrumbs>()
                .Map(m => m.ToTable("cms_breadcrumbs"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_breadcrumb_Result> SelectBreadCrumbs()
        {
            return this.Database.SqlQuery<cms_asp_select_breadcrumb_Result>
                ("dbo.cms_asp_select_breadcrumb {0}", 0)
                .ToList();
        }

        public List<cms_asp_select_breadcrumb_Result> SelectBreadCrumb(int BreadCrumbID)
        {
            return this.Database.SqlQuery<cms_asp_select_breadcrumb_Result>
                ("dbo.cms_asp_select_breadcrumb {0}", BreadCrumbID)
                .ToList();
        }

        public List<string> DeleteBreadCrumb(int BreadCrumbID)
        {
            return this.Database.SqlQuery<string>
                ("dbo.cms_asp_admin_delete_breadcrumb {0}", BreadCrumbID)
                .ToList();
        }

        public List<string> UpdateBreadCrumb(cms_breadcrumbs breadCrumb)
        {
            return this.Database.SqlQuery<string>
                ("dbo.cms_asp_admin_update_breadcrumb " +
                "@breadcrumb_id={0},"+
	            "@breadcrumb_name={1},"+
	            "@deep_level={2},"+
	            "@include_site={3},"+
	            "@include_zonegroup={4},"+
	            "@include_headline={5},"+
	            "@excluded_sites={6},"+
	            "@excluded_zonegroups={7},"+
	            "@excluded_zones={8},"+
	            "@seperator={9},"+
	            "@ul_class={10},"+
	            "@include_submenus={11},"+
	            "@breadcrumb_main_container={12},"+
	            "@breadcrumb_main_item_container={13},"+
	            "@breadcrumb_sub_container={14},"+
	            "@breadcrumb_sub_item_container={15},"+
	            "@pubID={16}",
                breadCrumb.breadcrumb_id,
                breadCrumb.breadcrumb_name,
                breadCrumb.deep_level,
                breadCrumb.include_site,
                breadCrumb.include_zonegroup,
                breadCrumb.include_headline,
                breadCrumb.excluded_sites,
                breadCrumb.excluded_zonegroups,
                breadCrumb.excluded_zones,
                breadCrumb.seperator,
                breadCrumb.ul_class,
                breadCrumb.include_submenus,
                breadCrumb.breadcrumb_main_container,
                breadCrumb.breadcrumb_main_item_container,
                breadCrumb.breadcrumb_sub_container,
                breadCrumb.breadcrumb_sub_item_container,
                breadCrumb.created_by)
                .ToList();
        }
    }
}