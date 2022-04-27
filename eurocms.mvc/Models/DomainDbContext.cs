using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class DomainDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_domains> Domains { get; set; }

        public DomainDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_domains>()
                .Map(m => m.ToTable("cms_domains"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_domains_Result> SelectDomains()
        {
            return this.Database.SqlQuery<cms_asp_select_domains_Result>
                ("dbo.cms_asp_select_domains")
                .ToList();
        }

       

        public List<cms_asp_select_domain_details_Result> SelectDomain(int DomainID)
        {
            return this.Database.SqlQuery<cms_asp_select_domain_details_Result>
                ("dbo.cms_asp_select_domain_details @domain_id = {0}", 
                    DomainID)
                .ToList();
        }

        public List<cms_asp_admin_update_domains_Result> UpdateDomain(cms_domains domain)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_domains_Result>
                ("dbo.cms_asp_admin_update_domains @domain_id = {0}, @domain_names = {1}, @home_page_article = {2}, @publisher_id = {3}, @error_page_article = {4}",
                    domain.domain_id,
                    domain.domain_names,
                    domain.home_page_article,
                    domain.created_by,
                    domain.error_page_article)
                 .ToList();
        }

        public  List<cms_asp_admin_update_domains_Result> CreateDomain(cms_domains domain)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_domains_Result>
                ("dbo.cms_asp_admin_update_domains @domain_id = {0}, @domain_names = {1}, @home_page_article = {2}, @publisher_id = {3}, @error_page_article = {4}",
                    -1,
                    domain.domain_names,
                    domain.home_page_article,
                    domain.created_by,
                    domain.error_page_article)
                .ToList();
        }

        public List<cms_asp_admin_delete_domain_Result> DeleteDomain(int DomainID, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_domain_Result>
                ("dbo.cms_asp_admin_delete_domain @domain_id = {0}, @publisher_id = {1}, @publisher_level = {2}",
                    DomainID,
                    publisherID,
                    PublisherLevel)
                .ToList();
        }
    }
}