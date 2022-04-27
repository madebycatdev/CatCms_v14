using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class RedirectionDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_domains> Domains { get; set; }

        public RedirectionDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_domains>()
                .Map(m => m.ToTable("cms_page_redirection"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_redirection_Result> SelectRedirections()
        {
            return this.Database.SqlQuery<cms_asp_select_redirection_Result>
                ("dbo.cms_asp_select_redirections")
                .ToList();
        }



        public List<cms_asp_select_redirection_details_Result> SelectRedirection(int ID)
        {
            return this.Database.SqlQuery<cms_asp_select_redirection_details_Result>
                ("dbo.cms_asp_select_redirections_details @ID = {0}", 
                    ID)
                .ToList();
        }

        public List<cms_asp_admin_update_redirection_Result> UpdateRedirection(cms_page_redirection redirection)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_redirection_Result>
                ("dbo.cms_asp_admin_update_redirections @ID = {0}, @RedirectFrom = {1}, @RedirectTo = {2}, @Publisher = {3}",
                    redirection.ID,
                    redirection.RedirectFrom,
                    redirection.RedirectTo,
                    redirection.UpdatedBy)
                 .ToList();
        }

        public List<cms_asp_admin_update_redirection_Result> CreateRedirection(cms_page_redirection redirection)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_redirection_Result>
                ("dbo.cms_asp_admin_update_redirections @ID = {0}, @RedirectFrom = {1}, @RedirectTo = {2}, @Publisher = {3}",
                    -1,
                    redirection.RedirectFrom,
                    redirection.RedirectTo,
                    redirection.CreatedBy)
                .ToList();
        }

        public List<cms_asp_admin_delete_domain_Result> DeleteDomain(int ID, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_domain_Result>
                ("dbo.cms_asp_admin_delete_relation @ID = {0}, @Publisher = {1}, @PublisherLevel = {2}",
                    ID,
                    publisherID,
                    PublisherLevel)
                .ToList();
        }
    }
}