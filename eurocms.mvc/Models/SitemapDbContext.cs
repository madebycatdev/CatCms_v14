using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class SitemapDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_sitemaps> Sitemaps { get; set; }

        public SitemapDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_sitemaps>()
                .Map(m => m.ToTable("cms_sitemaps"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_admin_select_sitemaps_Result> SelectSitemaps()
        {
            return this.Database.SqlQuery<cms_asp_admin_select_sitemaps_Result>
                ("dbo.cms_asp_admin_select_sitemaps 0")
                .ToList();
        }

        public List<cms_asp_admin_select_sitemaps_Result> SelectSitemap(int SitemapID)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_sitemaps_Result>
                ("dbo.cms_asp_admin_select_sitemaps @smap_id={0}",
                SitemapID)
                .ToList();
        }

        public List<string> DeleteSitemap(int SitemapID)
        {
            return this.Database.SqlQuery<string>
                ("dbo.cms_asp_admin_delete_sitemap @smap_id={0}",
                SitemapID)
                .ToList();
        }

        public void ReCreateSitemap(int SitemapID)
        {
            try
            {
                this.Database.SqlQuery<string>
             ("dbo.cms_asp_admin_update_sitemap_status @smap_id={0},@status={1},@xml={2}",
             SitemapID, "1", string.Empty).ToList();
            }
            catch (Exception ex)
            {
                //Liste dönmesinden dolayı hata veriyor, devam et
            }
        }

        public List<string> UpdateSitemap(cms_sitemaps sitemap)
        {
            return this.Database.SqlQuery<string>
                 ("dbo.cms_asp_admin_update_sitemap " +
                 "@smap_id={0}," +
                 "@domain_id={1}," +
                 "@domain_alias={2}," +
                 "@notify_google={3}," +
                 "@notify_msn={4}," +
                 "@notify_ask={5}," +
                 "@notify_yahoo={6}," +
                 "@yahoo_id={7}," +
                 "@included_sites={8}," +
                 "@excluded_zonegroups={9}," +
                 "@excluded_zones={10}," +
                 "@excluded_articles={11}," +
                 "@afiles={12}," +
                 "@interval={13}," +
                 "@enabled={14}," +
                 "@gzip_enabled={15}," +
                 "@pubID={16}",
                 sitemap.smap_id,
                 sitemap.domain_id,
                 sitemap.domain_alias,
                 sitemap.notify_google,
                 sitemap.notify_msn,
                 sitemap.notify_ask,
                 sitemap.notify_yahoo,
                 sitemap.yahoo_id,
                 sitemap.included_sites,
                 sitemap.excluded_zonegroups,
                 sitemap.excluded_zones,
                 sitemap.excluded_articles,
                 sitemap.afiles,
                 sitemap.interval,
                 sitemap.enabled,
                 sitemap.gzip_enabled,
                 sitemap.created_by).ToList();
        }
    }
}