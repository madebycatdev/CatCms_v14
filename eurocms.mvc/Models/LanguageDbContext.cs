using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class LanguageDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_languages> Languages { get; set; }

        public LanguageDbContext()
            : base()
        { 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_languages>()
                .Map(m => m.ToTable("cms_languages"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_languages_Result> SelectLanguages()
        {
            return this.Database.SqlQuery<cms_asp_select_languages_Result>
                ("dbo.cms_asp_select_languages")
                .ToList();
        }

        public List<cms_asp_select_language_details_Result> SelectLanguage(string LanguageID)
        {
            return this.Database.SqlQuery<cms_asp_select_language_details_Result>
                ("dbo.cms_asp_select_language_details @lang_id = {0}",
                    LanguageID)
                .ToList();
        }

        public List<cms_asp_admin_update_language_Result> CreateOrUpdateLanguage(cms_languages language)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_language_Result>
                ("dbo.cms_asp_admin_update_language @lang_id = {0}, @lang_name = {1}, @lang_xml = {2}, @lang_order = {3}, @publisher_id = {4}, @lang_alias = {5}",
                    language.lang_id,
                    language.lang_name,
                    HttpUtility.HtmlEncode(language.lang_xml),
                    language.lang_order,
                    language.publisher_id,
                    language.lang_alias)
                 .ToList();
        }
 
        public List<cms_asp_admin_delete_language_Result> DeleteLanguage(string LanguageID, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_language_Result>
                ("dbo.cms_asp_admin_delete_language @lang_id = {0}, @publisher_id = {1}, @publisher_level = {2}",
                    LanguageID,
                    publisherID,
                    PublisherLevel)
                .ToList();
        }
    }
}