using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class UrlRedirectDbContext : BaseDbContext
    {
        public DbSet<cms_redirects> Redirects { get; set; }

        public UrlRedirectDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_redirects>()
                .Map(m => m.ToTable("cms_redirects"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_admin_select_url_redirects_Result> SelectUrlRedirects()
        {
            //List<cms_asp_admin_select_url_redirects_Result> returnList = new List<cms_asp_admin_select_url_redirects_Result>();

            //CmsDbContext dbContext = new CmsDbContext();

            //returnList = (from r in dbContext.URLRedirects.AsNoTracking()
            //              join p in dbContext.vAspNetMembershipUsers.AsNoTracking() on r.PublisherID equals p.UserId into pr
            //              from p in pr.DefaultIfEmpty()

            //              join sg in dbContext.StructureGroups.AsNoTracking() on r.GroupID equals sg.Id into sgr
            //              from sg in sgr.DefaultIfEmpty()

            //              join az in dbContext.ArticleZones.AsNoTracking() on new { r.ArticleID, r.ZoneID } equals new { az.ArticleID, az.ZoneID } into azr
            //              from az in azr.DefaultIfEmpty()

            //              join z in dbContext.Zones.AsNoTracking() on az.ZoneID equals z.Id into zr
            //              from z in zr.DefaultIfEmpty()

            //              join zg in dbContext.ZoneGroups.AsNoTracking() on z.ZoneGroupId equals zg.Id into zgr
            //              from zg in zgr.DefaultIfEmpty()

            //              join s in dbContext.Sites.AsNoTracking() on zg.SiteId equals s.Id into sr
            //              from s in sr.DefaultIfEmpty()

            //              join d in dbContext.Domains.AsNoTracking() on s.DomainId equals d.Id into dr
            //              from d in dr.DefaultIfEmpty()

            //              orderby new { sg.Name, r.Alias }
            //              select new cms_asp_admin_select_url_redirects_Result() { redirect_id = r.ID, redirect_alias = r.Alias, publisher_id = r.PublisherID, created = r.CreateDate, updated = r.UpdateDate, publisher_name = p.UserName, group_id = r.GroupID, group_name = sg.Name, full_url = (d.Names.IndexOf(Environment.NewLine) > 0 ? d.Names.Substring(0, d.Names.IndexOf(Environment.NewLine)) : d.Names) + '/' + r.Alias }).ToList();

            //return returnList;
            return this.Database.SqlQuery<cms_asp_admin_select_url_redirects_Result>("dbo.cms_asp_admin_select_url_redirects").ToList();
        }

        public List<cms_asp_admin_select_redirection_detail_Result> SelectUrlRedirect(int RedirectID)
        {
            //List<cms_asp_admin_select_redirection_detail_Result> returnList = new List<cms_asp_admin_select_redirection_detail_Result>();

            //CmsDbContext dbContext = new CmsDbContext();

            //returnList = (from r in dbContext.URLRedirects.AsNoTracking()

            //              join a in dbContext.Articles.AsNoTracking() on r.ArticleID equals a.Id into ar
            //              from a in ar.DefaultIfEmpty()

            //              join z in dbContext.Zones.AsNoTracking() on r.ZoneID equals z.Id into zr
            //              from z in zr.DefaultIfEmpty()

            //              join zg in dbContext.ZoneGroups.AsNoTracking() on z.ZoneGroupId equals zg.Id into zgr
            //              from zg in zgr.DefaultIfEmpty()

            //              join s in dbContext.Sites.AsNoTracking() on zg.SiteId equals s.Id into sr
            //              from s in sr.DefaultIfEmpty()
            //              where r.ID == RedirectID
            //              select new cms_asp_admin_select_redirection_detail_Result() { redirect_id = r.ID, redirect_alias = r.Alias, article_id = a.Id, zone_id = z.Id, out_name = (s.Name + " / " + zg.Name + " / " + z.Name + " / " + a.Headline), group_id = r.GroupID, permanent_redirection = (r.PermanentRedirection == null ? false : Convert.ToBoolean(r.PermanentRedirection)), structure_description = r.StructureDescription}).ToList();
            
            //return returnList;
            return this.Database.SqlQuery<cms_asp_admin_select_redirection_detail_Result>("dbo.cms_asp_admin_select_redirection_detail {0}", RedirectID).ToList();
        }

        public List<cms_asp_admin_delete_redirection_Result> DeleteUrlRedirect(int RedirectID, object publisherID, int PublisherLevel)
        {
            List<cms_asp_admin_delete_redirection_Result> returnList = new List<cms_asp_admin_delete_redirection_Result>();
            cms_asp_admin_delete_redirection_Result returnItem = new cms_asp_admin_delete_redirection_Result();

            CmsDbContext dbContext = new CmsDbContext();

            URLRedirect getURLRedirect = new URLRedirect();
            getURLRedirect = dbContext.URLRedirects.Where(s => s.ID == RedirectID).FirstOrDefault();
            if (getURLRedirect != null)
            {
                dbContext.URLRedirects.Remove(getURLRedirect);
                dbContext.SaveChanges();
                returnItem.found_name = "";
                returnItem.rCode = "0";
            }
            else
            {
                returnItem.found_name = "";
                returnItem.rCode = "1";
            }

            returnList.Add(returnItem);

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_delete_redirection_Result>
            //    ("dbo.cms_asp_admin_delete_redirection " +
            //    "@redirect_id={0}," +
            //    "@publisher_id={1}," +
            //    "@publisher_level={2}",
            //    RedirectID,
            //    publisherID,
            //    PublisherLevel)
            //    .ToList();
        }

        public List<cms_asp_admin_update_redirection_details_Result> UpdateRedirect(cms_redirects redirect)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_redirection_details_Result>
                ("dbo.cms_asp_admin_update_redirection_details " +
                 "@redirect_id={0}," +
                 "@redirect_alias={1}," +
                 "@zone_id={2}," +
                 "@article_id={3}," +
                 "@group_id={4}," +
                 "@structure_description={5}," +
                 "@permanent_redirection={6}," +
                 "@publisher_id={7}",
                redirect.redirect_id,
                redirect.redirect_alias,
                redirect.zone_id,
                redirect.article_id,
                redirect.group_id,
                redirect.structure_description,
                redirect.permanent_redirection,
                redirect.publisher_id)
                .ToList();
        }

        public List<cms_asp_admin_select_az_check_Result> SelectAzCheck(int ZoneID, int ArticleID, string Alias)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_az_check_Result>
               ("dbo.cms_asp_admin_select_az_check @article_id={0}, @zone_id={1}, @az_alias={2}", ArticleID, ZoneID, Alias)
               .ToList();
        }
    }
}