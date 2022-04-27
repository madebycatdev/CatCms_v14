using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
namespace EuroCMS.Model
{
    #region Interfaces
    public interface IRepository<T> : IDisposable
    {
        IList<T> All();

        T Find(int? id);

        T GetRevision(long? id);

        IList<T> Find(string keyword);

        T FindByName(string name);

        T Insert(T item);

        T Update(T item);

        void Delete(T item);

        IList<T> GetAll();

        IList<T> GetAllByGroupId(int groupId);

        IList<T> GetAllByParentId(int parentId);
    }

    public interface IService<T>
    {
        IList<T> All();

        T Find(int? id);

        T GetRevision(long? id);

        IList<T> Find(string keyword);

        T FindByName(string name);

        T Insert(T item);

        T Update(T item);

        void Delete(T item);

        IList<T> GetAll();

        IList<T> GetAllByGroupId(int groupId);

        IList<T> GetAllByParentId(int parentId);
    }
    #endregion

    #region DbContext

    public class CmsDbContext : BaseDbContext
    {
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<ZoneGroup> ZoneGroups { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<ZoneRevision> ZoneRevisions { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<StructureGroup> StructureGroups { get; set; }
        public DbSet<ArticleRevision> ArticleRevisions { get; set; }
        public DbSet<ArticleFile> Files { get; set; }
        public DbSet<ArticleFileRevision> FileRevisions { get; set; }
        public DbSet<ArticleFileRevisionFile> FileRevisionFiles { get; set; }

        #region Yeni Yazılanlar

        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }

        //skylife dışında kaldırılmalı
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Template> Templates { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<TemplateRevision> TemplateRevisions { get; set; }
        public DbSet<ArticleZone> ArticleZones { get; set; }
        public DbSet<ArticleZoneRevision> ArticleZoneRevisions { get; set; }
        public DbSet<LanguageRelation> LanguageRelations { get; set; }
        public DbSet<LanguageRelationRevision> LanguageRelationRevisions { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ArticleRelationRevision> ArticleRelationRevisions { get; set; }
        public DbSet<ArticleCache> ArticleCaches { get; set; }
        public DbSet<CmsConfig> CmsConfigs { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        public DbSet<STFEmail> STFEmails { get; set; }

        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<CustomContent> CustomContents { get; set; }

        public DbSet<InstantMessaging> InstantMessagings { get; set; }
        public DbSet<ClassificationComboValue> ClassificationComboValues { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<URLStructure> URLStructures { get; set; }

        public DbSet<URLRedirect> URLRedirects { get; set; }

        public DbSet<Splash> Splashes { get; set; }

        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserInRole> UserInRoles { get; set; }
        //public DbSet<AccessRule> AccessRules { get; set; }


        public DbSet<vArticlesZonesFull> vArticlesZonesFulls { get; set; }
        public DbSet<vAspNetMembershipUser> vAspNetMembershipUsers { get; set; }
        public DbSet<vw_cms_AccessRules> CmsAccessRules { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<WidgetUser> WidgetUsers { get; set; }
        public DbSet<WidgetConfig> WidgetConfigs { get; set; }
        public DbSet<CustomValues> CustomValues { get; set; }
        public DbSet<Portlet> Portlets { get; set; }
        public DbSet<PageRedirection> PageRedirections { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>()
                .HasOptional(t => t.Sites).WithMany();

            modelBuilder.Entity<Site>()
                .HasOptional(c => c.Domain)
                    .WithMany(d => d.Sites)
                    .HasForeignKey(c => c.DomainId);

            modelBuilder.Entity<Site>()
                .HasRequired(c => c.CreatedUser)
                .WithMany().HasForeignKey(c => c.CreatedBy);

            modelBuilder.Entity<Site>()
                 .HasOptional(c => c.Group);

            modelBuilder.Entity<ZoneGroup>()
                 .HasRequired(c => c.CreatedUser)
                 .WithMany().HasForeignKey(c => c.CreatedBy);

            modelBuilder.Entity<ZoneGroup>()
              .HasRequired(c => c.Site)
                  .WithMany(d => d.ZoneGroups)
                  .HasForeignKey(c => c.SiteId);

            modelBuilder.Entity<ZoneGroup>()
                 .HasRequired(c => c.CreatedUser)
                 .WithMany().HasForeignKey(c => c.CreatedBy);

            modelBuilder.Entity<Zone>()
                 .HasRequired(c => c.CreatedUser)
                 .WithMany().HasForeignKey(c => c.CreatedBy);

            modelBuilder.Entity<Zone>()
                .HasRequired(c => c.ZoneGroup)
                    .WithMany(d => d.Zones)
                    .HasForeignKey(c => c.ZoneGroupId);

            modelBuilder.Entity<Zone>()
                .HasRequired(c => c.CreatedUser)
                .WithMany().HasForeignKey(c => c.CreatedBy);


            //.Map(c =>
            //{
            //    c.MapLeftKey("zone_id");
            //    c.MapRightKey("article_id");
            //    c.ToTable("cms_article_zones");
            //});

            modelBuilder.Entity<Zone>()
                .HasMany<ZoneRevision>(r => r.Revisions)
                .WithRequired(t => t.Zone)
                .HasForeignKey(t => t.ZoneId);

            //modelBuilder.Entity<ZoneRevision>()
            //    .HasMany<ArticleRevision>(r => r.Articles)
            //    .WithMany(t => t.Zones)
            //    .Map(c =>
            //    {
            //        c.MapLeftKey("zone_id");
            //        c.MapRightKey("article_id");
            //        c.ToTable("cms_article_zones_revision");
            //    });

            modelBuilder.Entity<ZoneRevision>()
               .HasRequired(c => c.RevisedUser)
               .WithMany().HasForeignKey(c => c.RevisedBy);

            modelBuilder.Entity<ZoneRevision>()
               .HasRequired(c => c.ApprovedUser)
               .WithMany().HasForeignKey(c => c.ApprovedBy);

            //.Map(c =>
            //{
            //    c.MapLeftKey("article_id");
            //    c.MapRightKey("zone_id");
            //    c.ToTable("cms_article_zones");
            //});

            modelBuilder.Entity<Article>()
                .HasMany<ArticleRevision>(r => r.Revisions)
                .WithRequired(t => t.Article)
                .HasForeignKey(t => t.ArticleId);

            //modelBuilder.Entity<ArticleRevision>()
            //   .HasMany<ZoneRevision>(r => r.Zones)
            //   .WithMany(t => t.Articles)
            //   .Map(c =>
            //   {
            //       c.MapLeftKey("article_id");
            //       c.MapRightKey("zone_id");
            //       c.ToTable("cms_article_zones_revision");
            //   });

            modelBuilder.Entity<ArticleZone>()
     .HasKey(az => new { az.ArticleID, az.ZoneID });

            modelBuilder.Entity<Article>()
                .HasMany(c => c.ArticleZones)
                .WithRequired()
                .HasForeignKey(c => c.ArticleID);

            modelBuilder.Entity<Zone>()
                .HasMany(z => z.ArticleZones)
                .WithRequired()
                .HasForeignKey(z => z.ZoneID);

            modelBuilder.Entity<ArticleFileRevision>()
               .HasMany<ArticleFileRevisionFile>(r => r.FileRevisions)
               .WithRequired(t => t.Revision)
               .HasForeignKey(t => t.RevisionId);

            base.OnModelCreating(modelBuilder);
        }
    }

    #endregion

    #region Repository

    public class SiteRepository : IRepository<Site>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Site Find(int? id)
        {
            return _context.Sites.Find(id);
        }

        public Site Insert(Site item)
        {
            _context.Sites.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Site Update(Site item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Site item)
        {
            _context.Sites.Attach(item);
            _context.Sites.Remove(item);
            _context.SaveChanges();
        }

        public IList<Site> GetAll()
        {
            return _context.Sites.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<Site> GetAllByGroupId(int groupId)
        {
            if (groupId == 0)
                return _context.Sites.AsNoTracking().ToList();

            return _context.Sites.Where(s => s.GroupId == groupId).AsNoTracking().ToList();
        }

        public Site FindByName(string name)
        {
            return _context.Sites.Where(s => s.Name.Equals(name)).FirstOrDefault();
        }

        public IList<Site> Find(string keyword)
        {
            return _context.Sites.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<Site> GetAllByParentId(int parentId)
        {
            return _context.Sites.Where(m => m.Id == parentId).ToList();
        }


        public Site GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Site> All()
        {
            return _context.Sites.ToList();
        }
    }

    public class DomainRepository : IRepository<Domain>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Domain Find(int? id)
        {
            return _context.Domains.Find(id);
        }

        public Domain Insert(Domain item)
        {
            _context.Domains.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Domain Update(Domain item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Domain item)
        {
            _context.Domains.Attach(item);
            _context.Domains.Remove(item);
            _context.SaveChanges();
        }

        public IList<Domain> GetAll()
        {
            return _context.Domains.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<Domain> GetAllByGroupId(int groupId)
        {
            return _context.Domains.AsNoTracking().ToList();
            //return _context.Domains.Where(s => s.GroupId == groupId).AsNoTracking().ToList();
        }

        public Domain FindByName(string name)
        {
            return _context.Domains.Where(s => s.Names.Equals(name)).FirstOrDefault();
        }

        public IList<Domain> Find(string keyword)
        {
            return _context.Domains.Where(s => s.Names.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<Domain> GetAllByParentId(int parentId)
        {
            return _context.Domains.Where(m => m.Id == parentId).ToList();
        }


        public Domain GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Domain> All()
        {
            return _context.Domains.ToList();
        }
    }

    public class StructureGroupRepository : IRepository<StructureGroup>
    {
        private CmsDbContext _context = new CmsDbContext();

        public StructureGroup Find(int? id)
        {
            return _context.StructureGroups.Find(id);
        }

        public StructureGroup Insert(StructureGroup item)
        {
            _context.StructureGroups.Add(item);
            _context.SaveChanges();
            return item;
        }

        public StructureGroup Update(StructureGroup item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(StructureGroup item)
        {
            _context.StructureGroups.Attach(item);
            _context.StructureGroups.Remove(item);
            _context.SaveChanges();
        }

        public IList<StructureGroup> GetAll()
        {
            return _context.StructureGroups.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<StructureGroup> GetAllByGroupId(int groupId)
        {
            return _context.StructureGroups.AsNoTracking().ToList();
            //return _context.Domains.Where(s => s.GroupId == groupId).AsNoTracking().ToList();
        }

        public StructureGroup FindByName(string name)
        {
            return _context.StructureGroups.Where(s => s.Name.Equals(name)).FirstOrDefault();
        }

        public IList<StructureGroup> Find(string keyword)
        {
            return _context.StructureGroups.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<StructureGroup> GetAllByParentId(int parentId)
        {
            return _context.StructureGroups.Where(m => m.Id == parentId).ToList();
        }


        public StructureGroup GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<StructureGroup> All()
        {
            return _context.StructureGroups.ToList();
        }
    }

    public class ZoneGroupRepository : IRepository<ZoneGroup>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ZoneGroup Find(int? id)
        {
            return _context.ZoneGroups.Find(id);
        }

        public ZoneGroup Insert(ZoneGroup item)
        {
            _context.ZoneGroups.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ZoneGroup Update(ZoneGroup item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ZoneGroup item)
        {
            _context.ZoneGroups.Attach(item);
            _context.ZoneGroups.Remove(item);
            _context.SaveChanges();
        }

        public IList<ZoneGroup> GetAll()
        {
            return _context.ZoneGroups.AsNoTracking().ToList();
        }

        public IList<ZoneGroup> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public IList<ZoneGroup> GetAllByParentId(int parentId)
        {
            if (parentId < 1)
                return _context.ZoneGroups.AsNoTracking().ToList();

            return _context.ZoneGroups.Where(zg => zg.SiteId == parentId).AsNoTracking().ToList();
        }

        public IList<ZoneGroup> GetAllByParentId2(int parentId)
        {
            if (parentId < 1)
                return _context.ZoneGroups.ToList();

            return _context.ZoneGroups.Where(zg => zg.SiteId == parentId).AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ZoneGroup FindByName(string name)
        {
            return _context.ZoneGroups.Where(s => s.Name.Equals(name)).FirstOrDefault();
        }

        public IList<ZoneGroup> Find(string keyword)
        {
            return _context.ZoneGroups.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public ZoneGroup GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ZoneGroup> All()
        {
            return _context.ZoneGroups.ToList();
        }
    }

    public class ZoneRepository : IRepository<Zone>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Zone Find(int? id)
        {
            return _context.Zones.Find(id);
        }

        public Zone Insert(Zone item)
        {
            _context.Zones.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Zone Update(Zone item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Zone item)
        {
            _context.Zones.Attach(item);
            _context.Zones.Remove(item);
            _context.SaveChanges();
        }

        public IList<Zone> GetAll()
        {
            return _context.Zones.AsNoTracking().ToList();
        }

        public IList<Zone> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Zone FindByName(string name)
        {
            return _context.Zones.Where(s => s.Name.Equals(name) && s.Status != "D").FirstOrDefault();
        }

        public IList<Zone> Find(string keyword)
        {
            return _context.Zones.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<Zone> GetAllByParentId(int parentId)
        {
            if (parentId < 1)
                return _context.Zones.AsNoTracking().ToList();

            return _context.Zones.Where(zg => zg.ZoneGroup.Id == parentId).OrderByDescending(t => t.Updated).AsNoTracking().ToList();
        }


        public Zone GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Zone> All()
        {
            return _context.Zones.ToList();
        }
    }

    public class ZoneRevisionRepository : IRepository<ZoneRevision>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ZoneRevision Insert(ZoneRevision item)
        {
            _context.ZoneRevisions.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ZoneRevision Update(ZoneRevision item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ZoneRevision item)
        {
            _context.ZoneRevisions.Attach(item);
            _context.ZoneRevisions.Add(item);
            _context.SaveChanges();
        }

        public IList<ZoneRevision> GetAll()
        {
            return _context.ZoneRevisions.AsNoTracking().ToList();
        }

        public IList<ZoneRevision> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ZoneRevision FindByName(string name)
        {
            return _context.ZoneRevisions.Where(s => s.Name.Equals(name)).FirstOrDefault();
        }

        public IList<ZoneRevision> Find(string keyword)
        {
            return _context.ZoneRevisions.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<ZoneRevision> GetAllByParentId(int parentId)
        {
            if (parentId < 1)
                return _context.ZoneRevisions.AsNoTracking().ToList();

            return _context.ZoneRevisions.Where(zg => zg.ZoneGroupId == parentId).OrderByDescending(r => r.RevisionDate).AsNoTracking().ToList();
        }

        public ZoneRevision Find(int? id)
        {
            throw new NotImplementedException();
        }

        public ZoneRevision GetRevision(long? id)
        {
            return _context.ZoneRevisions.Where(r => r.RevisionId == id).FirstOrDefault();
        }

        public IList<ZoneRevision> All()
        {
            return _context.ZoneRevisions.ToList();
        }
    }

    public class ArticleRepository : IRepository<Article>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Article Find(int? id)
        {
            return _context.Articles.Find(id);
        }

        public Article Insert(Article item)
        {
            _context.Articles.Attach(item);
            _context.Articles.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Article Update(Article item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Article item)
        {
            _context.Articles.Remove(item);
            _context.SaveChanges();
        }

        public IList<Article> GetAll()
        {
            return _context.Articles.AsNoTracking().ToList();
        }

        public IList<Article> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Article FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Article Find(string keyword)
        {
            throw new NotImplementedException();
        }

        IList<Article> IRepository<Article>.Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<Article> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public Article GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Article> All()
        {
            return _context.Articles.ToList();
        }
    }

    public class ArticleFileRepository : IRepository<ArticleFile>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ArticleFile Find(int? id)
        {
            return _context.Files.Find(id);
        }

        public ArticleFile Insert(ArticleFile item)
        {
            _context.Files.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ArticleFile Update(ArticleFile item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ArticleFile item)
        {
            _context.Files.Attach(item);
            _context.Files.Remove(item);
            _context.SaveChanges();
        }

        public IList<ArticleFile> GetAll()
        {
            return _context.Files.AsNoTracking().ToList();
        }

        public IList<ArticleFile> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ArticleFile FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public ArticleFile Find(string keyword)
        {
            throw new NotImplementedException();
        }

        IList<ArticleFile> IRepository<ArticleFile>.Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleFile> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public ArticleFile GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleFile> All()
        {
            return _context.Files.ToList();
        }
    }

    #region favoriteRepository
    public class FavoriteRepository : IRepository<Favorite>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Favorite Find(int? id)
        {
            return _context.Favorites.Find(id);
        }

        public Favorite Insert(Favorite item)
        {
            _context.Favorites.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Favorite Update(Favorite item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Favorite item)
        {
            _context.Favorites.Attach(item);
            _context.Favorites.Remove(item);
            _context.SaveChanges();
        }

        public IList<Favorite> GetAll()
        {
            return _context.Favorites.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<Favorite> GetAllByFacebookId(string facebookId)
        {
            return _context.Favorites.Where(s => s.FacebookId == facebookId).AsNoTracking().ToList();
        }

        public IList<Favorite> GetAllByArticleId(int articleId)
        {
            return _context.Favorites.Where(s => s.ArticleId == articleId).AsNoTracking().ToList();
        }

        public IList<Favorite> GetAllByParentId(int parentId)
        {
            return _context.Favorites.Where(m => m.Id == parentId).ToList();
        }

        public Favorite GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Favorite> All()
        {
            return _context.Favorites.ToList();
        }

        public IList<Favorite> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public Favorite FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Favorite> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }
    }
    #endregion


    public class TemplateRepository : IRepository<Template>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Template Find(int? id)
        {
            return _context.Templates.Find(id);
        }

        public Template Insert(Template item)
        {
            _context.Templates.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Template Update(Template item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Template item)
        {
            _context.Templates.Attach(item);
            _context.Templates.Remove(item);
            _context.SaveChanges();
        }

        public IList<Template> GetAll()
        {
            return _context.Templates.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<Template> GetAllByGroupId(int groupId)
        {
            if (groupId == 0)
                return _context.Templates.AsNoTracking().ToList();

            return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public Template FindByName(string name)
        {
            return _context.Templates.Where(s => s.Name.Equals(name)).FirstOrDefault();
        }

        public IList<Template> Find(string keyword)
        {
            return _context.Templates.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<Template> GetAllByParentId(int parentId)
        {
            return _context.Templates.Where(m => m.Id == parentId).ToList();
        }

        public Template GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Template> All()
        {
            return _context.Templates.ToList();
        }
    }

    //public class UserRepository : IRepository<User>
    //{
    //    private CmsDbContext _context = new CmsDbContext();

    //    public User Find(int? id)
    //    {
    //        throw new NotImplementedException();
    //        //return _context.Users.Find(id);
    //    }

    //    public User Insert(User item)
    //    {
    //        _context.Users.Add(item);
    //        _context.SaveChanges();
    //        return item;
    //    }

    //    public User Update(User item)
    //    {
    //        _context.Entry(item).State = EntityState.Modified;
    //        _context.SaveChanges();
    //        return item;
    //    }

    //    public void Delete(User item)
    //    {
    //        _context.Users.Remove(item);
    //        _context.SaveChanges();
    //    }

    //    public IList<User> GetAll()
    //    {
    //        return _context.Users.AsNoTracking().ToList();
    //    }

    //    public void Dispose()
    //    {
    //        _context.Dispose();
    //    }

    //    public IList<User> GetAllByGroupId(int groupId)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public User FindByName(string name)
    //    {
    //        return _context.Users.Where(s => s.Name.Equals(name)).FirstOrDefault();
    //    }

    //    public IList<User> Find(string keyword)
    //    {
    //        return _context.Users.Where(s => s.Name.Contains(keyword)).AsNoTracking().ToList();
    //    }

    //    public IList<User> GetAllByParentId(int parentId)
    //    {
    //        throw new NotImplementedException();
    //        //return _context.Users.Where(m => m.Id == parentId).ToList();
    //    }

    //    public User GetRevision(long? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IList<User> All()
    //    {
    //        return _context.Users.ToList();
    //    }
    //}

    public class TemplateRevisionRepository : IRepository<TemplateRevision>
    {
        private CmsDbContext _context = new CmsDbContext();

        public TemplateRevision Find(int? id)
        {
            return _context.TemplateRevisions.Find(id);
        }

        public TemplateRevision Insert(TemplateRevision item)
        {
            _context.TemplateRevisions.Add(item);
            _context.SaveChanges();
            return item;
        }

        public TemplateRevision Update(TemplateRevision item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(TemplateRevision item)
        {
            _context.TemplateRevisions.Attach(item);
            _context.TemplateRevisions.Remove(item);
            _context.SaveChanges();
        }

        public IList<TemplateRevision> GetAll()
        {
            return _context.TemplateRevisions.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<TemplateRevision> GetAllByGroupId(int groupId)
        {
            //if (groupId == 0)
            //    return _context.TemplateRevisions.AsNoTracking().ToList();
            //
            //return _context.TemplateRevisions.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
            throw new NotImplementedException();
        }

        public TemplateRevision FindByName(string name)
        {
            return _context.TemplateRevisions.Where(s => s.TemplateID == (_context.Templates.Where(t => t.Name.Equals(name)).FirstOrDefault().Id)).FirstOrDefault();
        }

        public IList<TemplateRevision> Find(string keyword)
        {
            return _context.TemplateRevisions.Where(s => s.TemplateID == (_context.Templates.Where(t => t.Name.Contains(keyword)).FirstOrDefault().Id)).AsNoTracking().ToList();
        }

        public IList<TemplateRevision> GetAllByParentId(int parentId)
        {
            return _context.TemplateRevisions.Where(m => m.Id == parentId).ToList();
        }

        public TemplateRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<TemplateRevision> All()
        {
            return _context.TemplateRevisions.ToList();
        }
    }

    public class ArticleZoneRepository : IRepository<ArticleZone>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ArticleZone Find(int? id)
        {
            return _context.ArticleZones.Find(id);
        }

        public ArticleZone Insert(ArticleZone item)
        {
            _context.ArticleZones.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ArticleZone Update(ArticleZone item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ArticleZone item)
        {
            _context.ArticleZones.Attach(item);
            _context.ArticleZones.Remove(item);
            _context.SaveChanges();
        }

        public IList<ArticleZone> GetAll()
        {
            return _context.ArticleZones.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<ArticleZone> GetAllByGroupId(int groupId)
        {

            return _context.ArticleZones.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public ArticleZone FindByName(string name)
        {
            return _context.ArticleZones.Where(s => s.Zone.Name.Equals(name)).FirstOrDefault();
        }

        public IList<ArticleZone> Find(string keyword)
        {
            return _context.ArticleZones.Where(s => s.Zone.Name.Contains(keyword)).AsNoTracking().ToList();
        }

        public IList<ArticleZone> GetAllByParentId(int parentId)
        {
            return _context.ArticleZones.Where(m => m.RelID == parentId).ToList();
        }

        public ArticleZone GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleZone> All()
        {
            return _context.ArticleZones.ToList();
        }
    }

    public class ArticleZoneRevisionRepository : IRepository<ArticleZoneRevision>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ArticleZoneRevision Find(int? id)
        {
            return _context.ArticleZoneRevisions.Find(id);
        }

        public ArticleZoneRevision Insert(ArticleZoneRevision item)
        {
            _context.ArticleZoneRevisions.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ArticleZoneRevision Update(ArticleZoneRevision item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ArticleZoneRevision item)
        {
            _context.ArticleZoneRevisions.Attach(item);
            _context.ArticleZoneRevisions.Remove(item);
            _context.SaveChanges();
        }

        public IList<ArticleZoneRevision> GetAll()
        {
            return _context.ArticleZoneRevisions.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<ArticleZoneRevision> GetAllByGroupId(int groupId)
        {

            return _context.ArticleZoneRevisions.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public ArticleZoneRevision FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleZoneRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleZoneRevision> GetAllByParentId(int parentId)
        {
            return _context.ArticleZoneRevisions.Where(m => m.RevID == parentId).ToList();
        }

        public ArticleZoneRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleZoneRevision> All()
        {
            return _context.ArticleZoneRevisions.ToList();
        }
    }

    public class LanguageRelationRepository : IRepository<LanguageRelation>
    {
        private CmsDbContext _context = new CmsDbContext();

        public LanguageRelation Find(int? id)
        {
            return _context.LanguageRelations.Find(id);
        }

        public LanguageRelation Insert(LanguageRelation item)
        {
            _context.LanguageRelations.Add(item);
            _context.SaveChanges();
            return item;
        }

        public LanguageRelation Update(LanguageRelation item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(LanguageRelation item)
        {
            _context.LanguageRelations.Attach(item);
            _context.LanguageRelations.Remove(item);
            _context.SaveChanges();
        }

        public IList<LanguageRelation> GetAll()
        {
            return _context.LanguageRelations.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<LanguageRelation> GetAllByGroupId(int groupId)
        {

            return _context.LanguageRelations.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public LanguageRelation FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelation> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelation> GetAllByParentId(int parentId)
        {
            return _context.LanguageRelations.Where(m => m.LanguageRelationID == parentId).ToList();
        }

        public LanguageRelation GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelation> All()
        {
            return _context.LanguageRelations.ToList();
        }
    }

    public class LanguageRelationRevisionRepository : IRepository<LanguageRelationRevision>
    {
        private CmsDbContext _context = new CmsDbContext();

        public LanguageRelationRevision Find(int? id)
        {
            return _context.LanguageRelationRevisions.Find(id);
        }

        public LanguageRelationRevision Insert(LanguageRelationRevision item)
        {
            _context.LanguageRelationRevisions.Add(item);
            _context.SaveChanges();
            return item;
        }

        public LanguageRelationRevision Update(LanguageRelationRevision item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(LanguageRelationRevision item)
        {
            _context.LanguageRelationRevisions.Attach(item);
            _context.LanguageRelationRevisions.Remove(item);
            _context.SaveChanges();
        }

        public IList<LanguageRelationRevision> GetAll()
        {
            return _context.LanguageRelationRevisions.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<LanguageRelationRevision> GetAllByGroupId(int groupId)
        {

            return _context.LanguageRelationRevisions.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public LanguageRelationRevision FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelationRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelationRevision> GetAllByParentId(int parentId)
        {
            return _context.LanguageRelationRevisions.Where(m => m.LanguageRelationID == parentId).ToList();
        }

        public LanguageRelationRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelationRevision> All()
        {
            return _context.LanguageRelationRevisions.ToList();
        }
    }

    public class LanguageRepository : IRepository<Language>
    {
        private CmsDbContext _context = new CmsDbContext();

        public Language Find(int? id)
        {
            return _context.Languages.Find(id);
        }

        public Language Insert(Language item)
        {
            _context.Languages.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Language Update(Language item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(Language item)
        {
            _context.Languages.Attach(item);
            _context.Languages.Remove(item);
            _context.SaveChanges();
        }

        public IList<Language> GetAll()
        {
            return _context.Languages.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<Language> GetAllByGroupId(int groupId)
        {

            return _context.Languages.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public Language FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Language> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<Language> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public Language GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Language> All()
        {
            return _context.Languages.ToList();
        }
    }

    public class ArticleRelationRevisionRepository : IRepository<ArticleRelationRevision>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ArticleRelationRevision Find(int? id)
        {
            return _context.ArticleRelationRevisions.Find(id);
        }

        public ArticleRelationRevision Insert(ArticleRelationRevision item)
        {
            _context.ArticleRelationRevisions.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ArticleRelationRevision Update(ArticleRelationRevision item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ArticleRelationRevision item)
        {
            _context.ArticleRelationRevisions.Attach(item);
            _context.ArticleRelationRevisions.Remove(item);
            _context.SaveChanges();
        }

        public IList<ArticleRelationRevision> GetAll()
        {
            return _context.ArticleRelationRevisions.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<ArticleRelationRevision> GetAllByGroupId(int groupId)
        {

            return _context.ArticleRelationRevisions.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public ArticleRelationRevision FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRelationRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRelationRevision> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public ArticleRelationRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRelationRevision> All()
        {
            return _context.ArticleRelationRevisions.ToList();
        }
    }

    public class ArticleRevisionRepository : IRepository<ArticleRevision>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ArticleRevision Find(int? id)
        {
            return _context.ArticleRevisions.Find(id);
        }

        public ArticleRevision Insert(ArticleRevision item)
        {
            _context.ArticleRevisions.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ArticleRevision Update(ArticleRevision item)
        {
            _context.Entry(item).State = EntityState.Detached;
            //_context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ArticleRevision item)
        {
            _context.ArticleRevisions.Attach(item);
            _context.ArticleRevisions.Remove(item);
            _context.SaveChanges();
        }

        public IList<ArticleRevision> GetAll()
        {
            return _context.ArticleRevisions.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<ArticleRevision> GetAllByGroupId(int groupId)
        {

            return _context.ArticleRevisions.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public ArticleRevision FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRevision> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public ArticleRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRevision> All()
        {
            return _context.ArticleRevisions.ToList();
        }
    }




    public class ArticleCacheRepository : IRepository<ArticleCache>
    {
        private CmsDbContext _context = new CmsDbContext();

        public ArticleCache Find(int? id)
        {
            return _context.ArticleCaches.Find(id);
        }

        public ArticleCache Insert(ArticleCache item)
        {
            _context.ArticleCaches.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ArticleCache Update(ArticleCache item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(ArticleCache item)
        {
            _context.ArticleCaches.Attach(item);
            _context.ArticleCaches.Remove(item);
            _context.SaveChanges();
        }

        public IList<ArticleCache> GetAll()
        {
            return _context.ArticleCaches.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<ArticleCache> GetAllByGroupId(int groupId)
        {

            return _context.ArticleCaches.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public ArticleCache FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleCache> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleCache> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public ArticleCache GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleCache> All()
        {
            return _context.ArticleCaches.ToList();
        }
    }



    public class CmsConfigRepository : IRepository<CmsConfig>
    {
        private CmsDbContext _context = new CmsDbContext();

        public CmsConfig Find(int? id)
        {
            return _context.CmsConfigs.Find(id);
        }

        public CmsConfig Insert(CmsConfig item)
        {
            _context.CmsConfigs.Add(item);
            _context.SaveChanges();
            return item;
        }

        public CmsConfig Update(CmsConfig item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(CmsConfig item)
        {
            _context.CmsConfigs.Attach(item);
            _context.CmsConfigs.Remove(item);
            _context.SaveChanges();
        }

        public IList<CmsConfig> GetAll()
        {
            return _context.CmsConfigs.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<CmsConfig> GetAllByGroupId(int groupId)
        {

            return _context.CmsConfigs.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public CmsConfig FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<CmsConfig> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<CmsConfig> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public CmsConfig GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<CmsConfig> All()
        {
            return _context.CmsConfigs.ToList();
        }
    }



    public class FileTypeRepository : IRepository<FileType>
    {
        private CmsDbContext _context = new CmsDbContext();

        public FileType Find(int? id)
        {
            return _context.FileTypes.Find(id);
        }

        public FileType Insert(FileType item)
        {
            _context.FileTypes.Add(item);
            _context.SaveChanges();
            return item;
        }

        public FileType Update(FileType item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(FileType item)
        {
            _context.FileTypes.Attach(item);
            _context.FileTypes.Remove(item);
            _context.SaveChanges();
        }

        public IList<FileType> GetAll()
        {
            return _context.FileTypes.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<FileType> GetAllByGroupId(int groupId)
        {
            return _context.FileTypes.Where(f => f.GroupId == groupId).AsNoTracking().ToList();
        }

        public FileType FindByName(string name)
        {
            return _context.FileTypes.Where(f => f.Name.Contains(name)).ToList().FirstOrDefault();
        }

        public IList<FileType> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<FileType> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public FileType GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<FileType> All()
        {
            return _context.FileTypes.ToList();
        }
    }







    public class vArticlesZonesFullRepository : IRepository<vArticlesZonesFull>
    {
        private CmsDbContext _context = new CmsDbContext();

        public vArticlesZonesFull Find(int? id)
        {
            return _context.vArticlesZonesFulls.Find(id);
        }

        public vArticlesZonesFull Insert(vArticlesZonesFull item)
        {
            _context.vArticlesZonesFulls.Add(item);
            _context.SaveChanges();
            return item;
        }

        public vArticlesZonesFull Update(vArticlesZonesFull item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(vArticlesZonesFull item)
        {
            _context.vArticlesZonesFulls.Remove(item);
            _context.SaveChanges();
        }

        public IList<vArticlesZonesFull> GetAll()
        {
            return _context.vArticlesZonesFulls.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<vArticlesZonesFull> GetAllByGroupId(int groupId)
        {

            return _context.vArticlesZonesFulls.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public vArticlesZonesFull FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<vArticlesZonesFull> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<vArticlesZonesFull> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public vArticlesZonesFull GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<vArticlesZonesFull> All()
        {
            return _context.vArticlesZonesFulls.ToList();
        }
    }

    public class vAspNetMembershipUserRepository : IRepository<vAspNetMembershipUser>
    {
        private CmsDbContext _context = new CmsDbContext();

        public vAspNetMembershipUser Find(int? id)
        {
            return _context.vAspNetMembershipUsers.Find(id);
        }

        public vAspNetMembershipUser Insert(vAspNetMembershipUser item)
        {
            _context.vAspNetMembershipUsers.Add(item);
            _context.SaveChanges();
            return item;
        }

        public vAspNetMembershipUser Update(vAspNetMembershipUser item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public void Delete(vAspNetMembershipUser item)
        {
            _context.vAspNetMembershipUsers.Remove(item);
            _context.SaveChanges();
        }

        public IList<vAspNetMembershipUser> GetAll()
        {
            return _context.vAspNetMembershipUsers.AsNoTracking().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<vAspNetMembershipUser> GetAllByGroupId(int groupId)
        {

            return _context.vAspNetMembershipUsers.AsNoTracking().ToList();

            //return _context.Templates.Where(s => s.GroupID == groupId).AsNoTracking().ToList();
        }

        public vAspNetMembershipUser FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<vAspNetMembershipUser> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<vAspNetMembershipUser> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
            //return _context.Languages.Where(m => m.ID == parentId).ToList();
        }

        public vAspNetMembershipUser GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<vAspNetMembershipUser> All()
        {
            return _context.vAspNetMembershipUsers.ToList();
        }
    }


    #endregion

    #region Service
    public class SiteService : IService<Site>
    {
        private IRepository<Site> _siteRepository;

        public SiteService(IRepository<Site> repository)
        {
            this._siteRepository = repository;
        }

        public Site Find(int? id)
        {
            return this._siteRepository.Find(id);
        }

        public Site Insert(Site item)
        {

            return this._siteRepository.Insert(item);
        }

        public Site Update(Site item)
        {
            return this._siteRepository.Update(item);
        }

        public void Delete(Site item)
        {
            this._siteRepository.Delete(item);
        }

        public IList<Site> GetAll()
        {
            return this._siteRepository.GetAll();
        }

        public IList<Site> GetAllByGroupId(int groupId)
        {
            return this._siteRepository.GetAllByGroupId(groupId);
        }

        public Site FindByName(string name)
        {
            return _siteRepository.FindByName(name);
        }

        public IList<Site> Find(string keyword)
        {
            return _siteRepository.Find(keyword);
        }

        public IList<Site> GetAllByParentId(int parentId)
        {
            return _siteRepository.GetAllByParentId(parentId);
        }

        public Site GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Site> All()
        {
            return _siteRepository.All();
        }
    }

    public class DomainService : IService<Domain>
    {
        private IRepository<Domain> _domainRepository;

        public DomainService(IRepository<Domain> repository)
        {
            this._domainRepository = repository;
        }

        public Domain Find(int? id)
        {
            return this._domainRepository.Find(id);
        }

        public Domain Insert(Domain item)
        {

            return this._domainRepository.Insert(item);
        }

        public Domain Update(Domain item)
        {
            return this._domainRepository.Update(item);
        }

        public void Delete(Domain item)
        {
            this._domainRepository.Delete(item);
        }

        public IList<Domain> GetAll()
        {
            return this._domainRepository.GetAll();
        }

        public IList<Domain> GetAllByGroupId(int groupId)
        {
            return this._domainRepository.GetAllByGroupId(groupId);
        }

        public Domain FindByName(string name)
        {
            return _domainRepository.FindByName(name);
        }

        public IList<Domain> Find(string keyword)
        {
            return _domainRepository.Find(keyword);
        }

        public IList<Domain> GetAllByParentId(int parentId)
        {
            return _domainRepository.GetAllByParentId(parentId);
        }

        public Domain GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Domain> All()
        {
            return _domainRepository.All();
        }
    }

    public class StructureGroupService : IService<StructureGroup>
    {
        private IRepository<StructureGroup> _structureGroupRepository;

        public StructureGroupService(IRepository<StructureGroup> repository)
        {
            this._structureGroupRepository = repository;
        }

        public StructureGroup Find(int? id)
        {
            return this._structureGroupRepository.Find(id);
        }

        public StructureGroup Insert(StructureGroup item)
        {

            return this._structureGroupRepository.Insert(item);
        }

        public StructureGroup Update(StructureGroup item)
        {
            return this._structureGroupRepository.Update(item);
        }

        public void Delete(StructureGroup item)
        {
            this._structureGroupRepository.Delete(item);
        }

        public IList<StructureGroup> GetAll()
        {
            return this._structureGroupRepository.GetAll();
        }

        public IList<StructureGroup> GetAllByGroupId(int groupId)
        {
            return this._structureGroupRepository.GetAllByGroupId(groupId);
        }

        public StructureGroup FindByName(string name)
        {
            return _structureGroupRepository.FindByName(name);
        }

        public IList<StructureGroup> Find(string keyword)
        {
            return _structureGroupRepository.Find(keyword);
        }

        public IList<StructureGroup> GetAllByParentId(int parentId)
        {
            return _structureGroupRepository.GetAllByParentId(parentId);
        }

        public StructureGroup GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<StructureGroup> All()
        {
            return _structureGroupRepository.All();
        }
    }

    public class ZoneGroupService : IService<ZoneGroup>
    {
        private IRepository<ZoneGroup> _zoneGroupRepository;

        public ZoneGroupService(IRepository<ZoneGroup> repository)
        {
            this._zoneGroupRepository = repository;
        }

        public ZoneGroup Find(int? id)
        {
            return this._zoneGroupRepository.Find(id);
        }

        public ZoneGroup Insert(ZoneGroup item)
        {
            return this._zoneGroupRepository.Insert(item);
        }

        public ZoneGroup Update(ZoneGroup item)
        {
            return this._zoneGroupRepository.Update(item);
        }

        public void Delete(ZoneGroup item)
        {
            this._zoneGroupRepository.Delete(item);
        }

        public IList<ZoneGroup> GetAll()
        {
            return this._zoneGroupRepository.GetAll();
        }

        public IList<ZoneGroup> GetAllByGroupId(int groupId)
        {
            return _zoneGroupRepository.GetAllByGroupId(groupId);
        }

        public IList<ZoneGroup> GetAllByParentId(int siteId)
        {
            return _zoneGroupRepository.GetAllByParentId(siteId);
        }

        public ZoneGroup FindByName(string name)
        {
            return _zoneGroupRepository.FindByName(name);
        }

        public IList<ZoneGroup> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        IList<ZoneGroup> IService<ZoneGroup>.Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public ZoneGroup GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ZoneGroup> All()
        {
            return _zoneGroupRepository.All();
        }
    }

    public class ZoneService : IService<Zone>
    {
        private IRepository<Zone> _zoneRepository;

        public ZoneService(IRepository<Zone> repository)
        {
            this._zoneRepository = repository;
        }

        public Zone Find(int? id)
        {
            return this._zoneRepository.Find(id);
        }

        public Zone Insert(Zone item)
        {
            return this._zoneRepository.Insert(item);
        }

        public Zone Update(Zone item)
        {
            return this._zoneRepository.Update(item);
        }

        public void Delete(Zone item)
        {
            this._zoneRepository.Delete(item);
        }

        public IList<Zone> GetAll()
        {
            return this._zoneRepository.GetAll();
        }

        public IList<Zone> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public Zone FindByName(string name)
        {
            return _zoneRepository.FindByName(name);
        }

        public IList<Zone> GetAllByParentId(int parentId)
        {
            return _zoneRepository.GetAllByParentId(parentId);
        }

        public IList<Zone> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public Zone GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Zone> All()
        {
            return _zoneRepository.All();
        }
    }

    public class ZoneRevisionService : IService<ZoneRevision>
    {
        private IRepository<ZoneRevision> _zoneRepository;

        public ZoneRevisionService(IRepository<ZoneRevision> repository)
        {
            this._zoneRepository = repository;
        }

        public ZoneRevision Find(int? id)
        {
            throw new NotImplementedException();
        }

        public ZoneRevision Insert(ZoneRevision item)
        {
            return this._zoneRepository.Insert(item);
        }

        public ZoneRevision Update(ZoneRevision item)
        {
            return this._zoneRepository.Update(item);
        }

        public void Delete(ZoneRevision item)
        {
            this._zoneRepository.Delete(item);
        }

        public IList<ZoneRevision> GetAll()
        {
            return this._zoneRepository.GetAll();
        }

        public IList<ZoneRevision> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public ZoneRevision FindByName(string name)
        {
            throw new NotImplementedException();
        }

        IList<ZoneRevision> IService<ZoneRevision>.Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ZoneRevision> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        ZoneRevision IService<ZoneRevision>.Find(int? id)
        {
            throw new NotImplementedException();
        }

        public ZoneRevision GetRevision(long? id)
        {
            return _zoneRepository.GetRevision(id);
        }

        public IList<ZoneRevision> All()
        {
            throw new NotImplementedException();
        }
    }

    public class ArticleService : IService<Article>
    {
        private IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> repository)
        {
            this._articleRepository = repository;
        }

        public Article Find(int? id)
        {
            return this._articleRepository.Find(id);
        }

        public Article Insert(Article item)
        {
            return this._articleRepository.Insert(item);
        }

        public Article Update(Article item)
        {
            return this._articleRepository.Update(item);
        }

        public void Delete(Article item)
        {
            this._articleRepository.Delete(item);
        }

        public IList<Article> GetAll()
        {
            return this._articleRepository.GetAll();
        }

        public IList<Article> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public Article FindByName(string name)
        {
            return this._articleRepository.GetAll().Where(a => a.Headline.Contains(name)).FirstOrDefault();
        }

        public Article Find(string keyword)
        {
            throw new NotImplementedException();
        }

        IList<Article> IService<Article>.Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<Article> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public Article GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Article> All()
        {
            return this._articleRepository.All();
        }
    }

    public class ArticleFileService : IService<ArticleFile>
    {
        private IRepository<ArticleFile> _articleFileRepository;

        public ArticleFileService(IRepository<ArticleFile> repository)
        {
            this._articleFileRepository = repository;
        }

        public ArticleFile Find(int? id)
        {
            return this._articleFileRepository.Find(id);
        }

        public ArticleFile Insert(ArticleFile item)
        {
            return this._articleFileRepository.Insert(item);
        }

        public ArticleFile Update(ArticleFile item)
        {
            return this._articleFileRepository.Update(item);
        }

        public void Delete(ArticleFile item)
        {
            this._articleFileRepository.Delete(item);
        }

        public IList<ArticleFile> GetAll()
        {
            return this._articleFileRepository.GetAll();
        }

        public IList<ArticleFile> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public ArticleFile FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public ArticleFile Find(string keyword)
        {
            throw new NotImplementedException();
        }

        IList<ArticleFile> IService<ArticleFile>.Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleFile> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public ArticleFile GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleFile> All()
        {
            return this._articleFileRepository.All();
        }
    }




    #region Yeni Yazılanlar




    public class TemplateService : IService<Template>
    {
        private IRepository<Template> _templateRepository;

        public TemplateService(IRepository<Template> repository)
        {
            this._templateRepository = repository;
        }

        public Template Find(int? id)
        {
            return this._templateRepository.Find(id);
        }

        public Template Insert(Template item)
        {
            return this._templateRepository.Insert(item);
        }

        public Template Update(Template item)
        {
            return this._templateRepository.Update(item);
        }

        public void Delete(Template item)
        {
            this._templateRepository.Delete(item);
        }

        public IList<Template> GetAll()
        {
            return this._templateRepository.GetAll();
        }

        public IList<Template> GetAllByGroupId(int groupId)
        {
            return this._templateRepository.GetAllByGroupId(groupId);
        }

        public Template FindByName(string name)
        {
            return _templateRepository.FindByName(name);
        }

        public IList<Template> GetAllByParentId(int parentId)
        {
            return _templateRepository.GetAllByParentId(parentId);
        }

        public IList<Template> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public Template GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Template> All()
        {
            return _templateRepository.All();
        }
    }

    //public class UserService : IService<User>
    //{
    //    private IRepository<User> _userRepository;

    //    public UserService(IRepository<User> repository)
    //    {
    //        this._userRepository = repository;
    //    }

    //    public User Find(int? id)
    //    {
    //        throw new NotImplementedException();
    //        //return this._userRepository.Find(id);
    //    }

    //    public User Insert(User item)
    //    {
    //        return this._userRepository.Insert(item);
    //    }

    //    public User Update(User item)
    //    {
    //        return this._userRepository.Update(item);
    //    }

    //    public void Delete(User item)
    //    {
    //        this._userRepository.Delete(item);
    //    }

    //    public IList<User> GetAll()
    //    {
    //        return this._userRepository.GetAll();
    //    }

    //    public IList<User> GetAllByGroupId(int groupId)
    //    {
    //        return this._userRepository.GetAllByGroupId(groupId);
    //    }


    //    public User FindByName(string name)
    //    {
    //        return _userRepository.FindByName(name);
    //    }

    //    public IList<User> GetAllByParentId(int parentId)
    //    {
    //        return _userRepository.GetAllByParentId(parentId);
    //    }

    //    public IList<User> Find(string keyword)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public User GetRevision(long? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IList<User> All()
    //    {
    //        return _userRepository.All();
    //    }
    //}

    public class TemplateRevisionService : IService<TemplateRevision>
    {
        private IRepository<TemplateRevision> _templateRevisionRepository;

        public TemplateRevisionService(IRepository<TemplateRevision> repository)
        {
            this._templateRevisionRepository = repository;
        }

        public TemplateRevision Find(int? id)
        {
            return this._templateRevisionRepository.Find(id);
        }

        public TemplateRevision Insert(TemplateRevision item)
        {
            return this._templateRevisionRepository.Insert(item);
        }

        public TemplateRevision Update(TemplateRevision item)
        {
            return this._templateRevisionRepository.Update(item);
        }

        public void Delete(TemplateRevision item)
        {
            this._templateRevisionRepository.Delete(item);
        }

        public IList<TemplateRevision> GetAll()
        {
            return this._templateRevisionRepository.GetAll();
        }

        public IList<TemplateRevision> GetAllByGroupId(int groupId)
        {
            return this._templateRevisionRepository.GetAllByGroupId(groupId);
        }


        public TemplateRevision FindByName(string name)
        {
            return _templateRevisionRepository.FindByName(name);
        }

        public IList<TemplateRevision> GetAllByParentId(int parentId)
        {
            return _templateRevisionRepository.GetAllByParentId(parentId);
        }

        public IList<TemplateRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public TemplateRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<TemplateRevision> All()
        {
            return _templateRevisionRepository.All();
        }
    }


    public class ArticleZoneService : IService<ArticleZone>
    {
        private IRepository<ArticleZone> _articleZoneRepository;

        public ArticleZoneService(IRepository<ArticleZone> repository)
        {
            this._articleZoneRepository = repository;
        }

        public ArticleZone Find(int? id)
        {
            return this._articleZoneRepository.Find(id);
        }

        public ArticleZone Insert(ArticleZone item)
        {
            return this._articleZoneRepository.Insert(item);
        }

        public ArticleZone Update(ArticleZone item)
        {
            return this._articleZoneRepository.Update(item);
        }

        public void Delete(ArticleZone item)
        {
            this._articleZoneRepository.Delete(item);
        }

        public IList<ArticleZone> GetAll()
        {
            return this._articleZoneRepository.GetAll();
        }

        public IList<ArticleZone> GetAllByGroupId(int groupId)
        {
            return this._articleZoneRepository.GetAllByGroupId(groupId);
        }

        public ArticleZone FindByName(string name)
        {
            return _articleZoneRepository.FindByName(name);
        }

        public IList<ArticleZone> GetAllByParentId(int parentId)
        {
            return _articleZoneRepository.GetAllByParentId(parentId);
        }

        public IList<ArticleZone> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public ArticleZone GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleZone> All()
        {
            return _articleZoneRepository.All();
        }
    }

    public class ArticleZoneRevisionService : IService<ArticleZoneRevision>
    {
        private IRepository<ArticleZoneRevision> _articleZoneRevisionRepository;

        public ArticleZoneRevisionService(IRepository<ArticleZoneRevision> repository)
        {
            this._articleZoneRevisionRepository = repository;
        }

        public ArticleZoneRevision Find(int? id)
        {
            return this._articleZoneRevisionRepository.Find(id);
        }

        public ArticleZoneRevision Insert(ArticleZoneRevision item)
        {
            return this._articleZoneRevisionRepository.Insert(item);
        }

        public ArticleZoneRevision Update(ArticleZoneRevision item)
        {
            return this._articleZoneRevisionRepository.Update(item);
        }

        public void Delete(ArticleZoneRevision item)
        {
            this._articleZoneRevisionRepository.Delete(item);
        }

        public IList<ArticleZoneRevision> GetAll()
        {
            return this._articleZoneRevisionRepository.GetAll();
        }

        public IList<ArticleZoneRevision> GetAllByGroupId(int groupId)
        {
            return this._articleZoneRevisionRepository.GetAllByGroupId(groupId);
        }

        public ArticleZoneRevision FindByName(string name)
        {
            return _articleZoneRevisionRepository.FindByName(name);
        }

        public IList<ArticleZoneRevision> GetAllByParentId(int parentId)
        {
            return _articleZoneRevisionRepository.GetAllByParentId(parentId);
        }

        public IList<ArticleZoneRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public ArticleZoneRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleZoneRevision> All()
        {
            return _articleZoneRevisionRepository.All();
        }
    }

    public class LanguageRelationService : IService<LanguageRelation>
    {
        private IRepository<LanguageRelation> _languageRelationRepository;

        public LanguageRelationService(IRepository<LanguageRelation> repository)
        {
            this._languageRelationRepository = repository;
        }

        public LanguageRelation Find(int? id)
        {
            return this._languageRelationRepository.Find(id);
        }

        public LanguageRelation Insert(LanguageRelation item)
        {
            return this._languageRelationRepository.Insert(item);
        }

        public LanguageRelation Update(LanguageRelation item)
        {
            return this._languageRelationRepository.Update(item);
        }

        public void Delete(LanguageRelation item)
        {
            this._languageRelationRepository.Delete(item);
        }

        public IList<LanguageRelation> GetAll()
        {
            return this._languageRelationRepository.GetAll();
        }

        public IList<LanguageRelation> GetAllByGroupId(int groupId)
        {
            return this._languageRelationRepository.GetAllByGroupId(groupId);
        }

        public LanguageRelation FindByName(string name)
        {
            return _languageRelationRepository.FindByName(name);
        }

        public IList<LanguageRelation> GetAllByParentId(int parentId)
        {
            return _languageRelationRepository.GetAllByParentId(parentId);
        }

        public IList<LanguageRelation> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public LanguageRelation GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelation> All()
        {
            return _languageRelationRepository.All();
        }
    }

    public class LanguageRelationRevisionService : IService<LanguageRelationRevision>
    {
        private IRepository<LanguageRelationRevision> _languageRelationRevisionRepository;

        public LanguageRelationRevisionService(IRepository<LanguageRelationRevision> repository)
        {
            this._languageRelationRevisionRepository = repository;
        }

        public LanguageRelationRevision Find(int? id)
        {
            return this._languageRelationRevisionRepository.Find(id);
        }

        public LanguageRelationRevision Insert(LanguageRelationRevision item)
        {
            return this._languageRelationRevisionRepository.Insert(item);
        }

        public LanguageRelationRevision Update(LanguageRelationRevision item)
        {
            return this._languageRelationRevisionRepository.Update(item);
        }

        public void Delete(LanguageRelationRevision item)
        {
            this._languageRelationRevisionRepository.Delete(item);
        }

        public IList<LanguageRelationRevision> GetAll()
        {
            return this._languageRelationRevisionRepository.GetAll();
        }

        public IList<LanguageRelationRevision> GetAllByGroupId(int groupId)
        {
            return this._languageRelationRevisionRepository.GetAllByGroupId(groupId);
        }

        public LanguageRelationRevision FindByName(string name)
        {
            return _languageRelationRevisionRepository.FindByName(name);
        }

        public IList<LanguageRelationRevision> GetAllByParentId(int parentId)
        {
            return _languageRelationRevisionRepository.GetAllByParentId(parentId);
        }

        public IList<LanguageRelationRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public LanguageRelationRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageRelationRevision> All()
        {
            return _languageRelationRevisionRepository.All();
        }
    }

    public class LanguageService : IService<Language>
    {
        private IRepository<Language> _languageRepository;

        public LanguageService(IRepository<Language> repository)
        {
            this._languageRepository = repository;
        }

        public Language Find(int? id)
        {
            return this._languageRepository.Find(id);
        }

        public Language Insert(Language item)
        {
            return this._languageRepository.Insert(item);
        }

        public Language Update(Language item)
        {
            return this._languageRepository.Update(item);
        }

        public void Delete(Language item)
        {
            this._languageRepository.Delete(item);
        }

        public IList<Language> GetAll()
        {
            return this._languageRepository.GetAll();
        }

        public IList<Language> GetAllByGroupId(int groupId)
        {
            return this._languageRepository.GetAllByGroupId(groupId);
        }

        public Language FindByName(string name)
        {
            return _languageRepository.FindByName(name);
        }

        public IList<Language> GetAllByParentId(int parentId)
        {
            return _languageRepository.GetAllByParentId(parentId);
        }

        public IList<Language> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public Language GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<Language> All()
        {
            return _languageRepository.All();
        }
    }


    public class ArticleRelationRevisionService : IService<ArticleRelationRevision>
    {
        private IRepository<ArticleRelationRevision> _articleRelationRevisionRepository;

        public ArticleRelationRevisionService(IRepository<ArticleRelationRevision> repository)
        {
            this._articleRelationRevisionRepository = repository;
        }

        public ArticleRelationRevision Find(int? id)
        {
            return this._articleRelationRevisionRepository.Find(id);
        }

        public ArticleRelationRevision Insert(ArticleRelationRevision item)
        {
            return this._articleRelationRevisionRepository.Insert(item);
        }

        public ArticleRelationRevision Update(ArticleRelationRevision item)
        {
            return this._articleRelationRevisionRepository.Update(item);
        }

        public void Delete(ArticleRelationRevision item)
        {
            this._articleRelationRevisionRepository.Delete(item);
        }

        public IList<ArticleRelationRevision> GetAll()
        {
            return this._articleRelationRevisionRepository.GetAll();
        }

        public IList<ArticleRelationRevision> GetAllByGroupId(int groupId)
        {
            return this._articleRelationRevisionRepository.GetAllByGroupId(groupId);
        }

        public ArticleRelationRevision FindByName(string name)
        {
            return _articleRelationRevisionRepository.FindByName(name);
        }

        public IList<ArticleRelationRevision> GetAllByParentId(int parentId)
        {
            return _articleRelationRevisionRepository.GetAllByParentId(parentId);
        }

        public IList<ArticleRelationRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public ArticleRelationRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRelationRevision> All()
        {
            return _articleRelationRevisionRepository.All();
        }
    }

    public class ArticleRevisionService : IService<ArticleRevision>
    {
        private IRepository<ArticleRevision> _articleRevisionRepository;

        public ArticleRevisionService(IRepository<ArticleRevision> repository)
        {
            this._articleRevisionRepository = repository;
        }

        public ArticleRevision Find(int? id)
        {
            return this._articleRevisionRepository.Find(id);
        }

        public ArticleRevision Insert(ArticleRevision item)
        {
            return this._articleRevisionRepository.Insert(item);
        }

        public ArticleRevision Update(ArticleRevision item)
        {
            return this._articleRevisionRepository.Update(item);
        }

        public void Delete(ArticleRevision item)
        {
            this._articleRevisionRepository.Delete(item);
        }

        public IList<ArticleRevision> GetAll()
        {
            return this._articleRevisionRepository.GetAll();
        }

        public IList<ArticleRevision> GetAllByGroupId(int groupId)
        {
            return this._articleRevisionRepository.GetAllByGroupId(groupId);
        }

        public ArticleRevision FindByName(string name)
        {
            return _articleRevisionRepository.FindByName(name);
        }

        public IList<ArticleRevision> GetAllByParentId(int parentId)
        {
            return _articleRevisionRepository.GetAllByParentId(parentId);
        }

        public IList<ArticleRevision> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public ArticleRevision GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleRevision> All()
        {
            return _articleRevisionRepository.All();
        }
    }





    public class ArticleCacheService : IService<ArticleCache>
    {
        private IRepository<ArticleCache> _articleCacheRepository;

        public ArticleCacheService(IRepository<ArticleCache> repository)
        {
            this._articleCacheRepository = repository;
        }

        public ArticleCache Find(int? id)
        {
            return this._articleCacheRepository.Find(id);
        }

        public ArticleCache Insert(ArticleCache item)
        {
            return this._articleCacheRepository.Insert(item);
        }

        public ArticleCache Update(ArticleCache item)
        {
            return this._articleCacheRepository.Update(item);
        }

        public void Delete(ArticleCache item)
        {
            this._articleCacheRepository.Delete(item);
        }

        public IList<ArticleCache> GetAll()
        {
            return this._articleCacheRepository.GetAll();
        }

        public IList<ArticleCache> GetAllByGroupId(int groupId)
        {
            return this._articleCacheRepository.GetAllByGroupId(groupId);
        }

        public ArticleCache FindByName(string name)
        {
            return _articleCacheRepository.FindByName(name);
        }

        public IList<ArticleCache> GetAllByParentId(int parentId)
        {
            return _articleCacheRepository.GetAllByParentId(parentId);
        }

        public IList<ArticleCache> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public ArticleCache GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<ArticleCache> All()
        {
            return _articleCacheRepository.All();
        }
    }


    public class CmsConfigService : IService<CmsConfig>
    {
        private IRepository<CmsConfig> _cmsConfigRepository;

        public CmsConfigService(IRepository<CmsConfig> repository)
        {
            this._cmsConfigRepository = repository;
        }

        public CmsConfig Find(int? id)
        {
            return this._cmsConfigRepository.Find(id);
        }

        public CmsConfig Insert(CmsConfig item)
        {
            return this._cmsConfigRepository.Insert(item);
        }

        public CmsConfig Update(CmsConfig item)
        {
            return this._cmsConfigRepository.Update(item);
        }

        public void Delete(CmsConfig item)
        {
            this._cmsConfigRepository.Delete(item);
        }

        public IList<CmsConfig> GetAll()
        {
            return this._cmsConfigRepository.GetAll();
        }

        public IList<CmsConfig> GetAllByGroupId(int groupId)
        {
            return this._cmsConfigRepository.GetAllByGroupId(groupId);
        }

        public CmsConfig FindByName(string name)
        {
            return _cmsConfigRepository.FindByName(name);
        }

        public IList<CmsConfig> GetAllByParentId(int parentId)
        {
            return _cmsConfigRepository.GetAllByParentId(parentId);
        }

        public IList<CmsConfig> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public CmsConfig GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<CmsConfig> All()
        {
            return _cmsConfigRepository.All();
        }
    }

    public class FileTypeService : IService<FileType>
    {
        private IRepository<FileType> _fileTypeRepository;

        public FileTypeService(IRepository<FileType> repository)
        {
            this._fileTypeRepository = repository;
        }

        public FileType Find(int? id)
        {
            return this._fileTypeRepository.Find(id);
        }

        public FileType Insert(FileType item)
        {
            return this._fileTypeRepository.Insert(item);
        }

        public FileType Update(FileType item)
        {
            return this._fileTypeRepository.Update(item);
        }

        public void Delete(FileType item)
        {
            this._fileTypeRepository.Delete(item);
        }

        public IList<FileType> GetAll()
        {
            return this._fileTypeRepository.GetAll();
        }

        public IList<FileType> GetAllByGroupId(int groupId)
        {
            return this._fileTypeRepository.GetAllByGroupId(groupId);
        }

        public FileType FindByName(string name)
        {
            return _fileTypeRepository.FindByName(name);
        }

        public IList<FileType> GetAllByParentId(int parentId)
        {
            return _fileTypeRepository.GetAllByParentId(parentId);
        }

        public IList<FileType> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public FileType GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<FileType> All()
        {
            return _fileTypeRepository.All();
        }
    }





    //public class RoleService : IService<Role>
    //{
    //    private IRepository<Role> _roleRepository;

    //    public RoleService(IRepository<Role> repository)
    //    {
    //        this._roleRepository = repository;
    //    }

    //    public Role Find(int? id)
    //    {
    //        return this._roleRepository.Find(id);
    //    }

    //    public Role Insert(Role item)
    //    {
    //        return this._roleRepository.Insert(item);
    //    }

    //    public Role Update(Role item)
    //    {
    //        return this._roleRepository.Update(item);
    //    }

    //    public void Delete(Role item)
    //    {
    //        this._roleRepository.Delete(item);
    //    }

    //    public IList<Role> GetAll()
    //    {
    //        return this._roleRepository.GetAll();
    //    }

    //    public IList<Role> GetAllByGroupId(int groupId)
    //    {
    //        return this._roleRepository.GetAllByGroupId(groupId);
    //    }

    //    public Role FindByName(string name)
    //    {
    //        return _roleRepository.FindByName(name);
    //    }

    //    public IList<Role> GetAllByParentId(int parentId)
    //    {
    //        return _roleRepository.GetAllByParentId(parentId);
    //    }

    //    public IList<Role> Find(string keyword)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Role GetRevision(long? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IList<Role> All()
    //    {
    //        return _roleRepository.All();
    //    }
    //}

    //public class UserInRoleService : IService<UserInRole>
    //{
    //    private IRepository<UserInRole> _userInRoleRepository;

    //    public UserInRoleService(IRepository<UserInRole> repository)
    //    {
    //        this._userInRoleRepository = repository;
    //    }

    //    public UserInRole Find(int? id)
    //    {
    //        return this._userInRoleRepository.Find(id);
    //    }

    //    public UserInRole Insert(UserInRole item)
    //    {
    //        return this._userInRoleRepository.Insert(item);
    //    }

    //    public UserInRole Update(UserInRole item)
    //    {
    //        return this._userInRoleRepository.Update(item);
    //    }

    //    public void Delete(UserInRole item)
    //    {
    //        this._userInRoleRepository.Delete(item);
    //    }

    //    public IList<UserInRole> GetAll()
    //    {
    //        return this._userInRoleRepository.GetAll();
    //    }

    //    public IList<UserInRole> GetAllByGroupId(int groupId)
    //    {
    //        return this._userInRoleRepository.GetAllByGroupId(groupId);
    //    }

    //    public UserInRole FindByName(string name)
    //    {
    //        return _userInRoleRepository.FindByName(name);
    //    }

    //    public IList<UserInRole> GetAllByParentId(int parentId)
    //    {
    //        return _userInRoleRepository.GetAllByParentId(parentId);
    //    }

    //    public IList<UserInRole> Find(string keyword)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public UserInRole GetRevision(long? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IList<UserInRole> All()
    //    {
    //        return _userInRoleRepository.All();
    //    }
    //}

    //public class AccessRuleService : IService<AccessRule>
    //{
    //    private IRepository<AccessRule> _accessRuleRepository;

    //    public AccessRuleService(IRepository<AccessRule> repository)
    //    {
    //        this._accessRuleRepository = repository;
    //    }

    //    public AccessRule Find(int? id)
    //    {
    //        return this._accessRuleRepository.Find(id);
    //    }

    //    public AccessRule Insert(AccessRule item)
    //    {
    //        return this._accessRuleRepository.Insert(item);
    //    }

    //    public AccessRule Update(AccessRule item)
    //    {
    //        return this._accessRuleRepository.Update(item);
    //    }

    //    public void Delete(AccessRule item)
    //    {
    //        this._accessRuleRepository.Delete(item);
    //    }

    //    public IList<AccessRule> GetAll()
    //    {
    //        return this._accessRuleRepository.GetAll();
    //    }

    //    public IList<AccessRule> GetAllByGroupId(int groupId)
    //    {
    //        return this._accessRuleRepository.GetAllByGroupId(groupId);
    //    }

    //    public AccessRule FindByName(string name)
    //    {
    //        return _accessRuleRepository.FindByName(name);
    //    }

    //    public IList<AccessRule> GetAllByParentId(int parentId)
    //    {
    //        return _accessRuleRepository.GetAllByParentId(parentId);
    //    }

    //    public IList<AccessRule> Find(string keyword)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public AccessRule GetRevision(long? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IList<AccessRule> All()
    //    {
    //        return _accessRuleRepository.All();
    //    }
    //}






    public class vArticlesZonesFullService : IService<vArticlesZonesFull>
    {
        private IRepository<vArticlesZonesFull> _vArticlesZonesFullRepository;

        public vArticlesZonesFullService(IRepository<vArticlesZonesFull> repository)
        {
            this._vArticlesZonesFullRepository = repository;
        }

        public vArticlesZonesFull Find(int? id)
        {
            return this._vArticlesZonesFullRepository.Find(id);
        }

        public vArticlesZonesFull Insert(vArticlesZonesFull item)
        {
            return this._vArticlesZonesFullRepository.Insert(item);
        }

        public vArticlesZonesFull Update(vArticlesZonesFull item)
        {
            return this._vArticlesZonesFullRepository.Update(item);
        }

        public void Delete(vArticlesZonesFull item)
        {
            this._vArticlesZonesFullRepository.Delete(item);
        }

        public IList<vArticlesZonesFull> GetAll()
        {
            return this._vArticlesZonesFullRepository.GetAll();
        }

        public IList<vArticlesZonesFull> GetAllByGroupId(int groupId)
        {
            return this._vArticlesZonesFullRepository.GetAllByGroupId(groupId);
        }

        public vArticlesZonesFull FindByName(string name)
        {
            return _vArticlesZonesFullRepository.FindByName(name);
        }

        public IList<vArticlesZonesFull> GetAllByParentId(int parentId)
        {
            return _vArticlesZonesFullRepository.GetAllByParentId(parentId);
        }

        public IList<vArticlesZonesFull> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public vArticlesZonesFull GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<vArticlesZonesFull> All()
        {
            return _vArticlesZonesFullRepository.All();
        }
    }

    public class vAspNetMembershipUserService : IService<vAspNetMembershipUser>
    {
        private IRepository<vAspNetMembershipUser> _vAspNetMembershipUserRepository;

        public vAspNetMembershipUserService(IRepository<vAspNetMembershipUser> repository)
        {
            this._vAspNetMembershipUserRepository = repository;
        }

        public vAspNetMembershipUser Find(int? id)
        {
            return this._vAspNetMembershipUserRepository.Find(id);
        }

        public vAspNetMembershipUser Insert(vAspNetMembershipUser item)
        {
            return this._vAspNetMembershipUserRepository.Insert(item);
        }

        public vAspNetMembershipUser Update(vAspNetMembershipUser item)
        {
            return this._vAspNetMembershipUserRepository.Update(item);
        }

        public void Delete(vAspNetMembershipUser item)
        {
            this._vAspNetMembershipUserRepository.Delete(item);
        }

        public IList<vAspNetMembershipUser> GetAll()
        {
            return this._vAspNetMembershipUserRepository.GetAll();
        }

        public IList<vAspNetMembershipUser> GetAllByGroupId(int groupId)
        {
            return this._vAspNetMembershipUserRepository.GetAllByGroupId(groupId);
        }

        public vAspNetMembershipUser FindByName(string name)
        {
            return _vAspNetMembershipUserRepository.FindByName(name);
        }

        public IList<vAspNetMembershipUser> GetAllByParentId(int parentId)
        {
            return _vAspNetMembershipUserRepository.GetAllByParentId(parentId);
        }

        public IList<vAspNetMembershipUser> Find(string keyword)
        {
            throw new NotImplementedException();
        }

        public vAspNetMembershipUser GetRevision(long? id)
        {
            throw new NotImplementedException();
        }

        public IList<vAspNetMembershipUser> All()
        {
            return _vAspNetMembershipUserRepository.All();
        }
    }

    #endregion


    #endregion

    #region User
    [Table("aspnet_Users")]
    public class CmsUser
    {
        public Guid ApplicationId { get; set; }

        [Column("UserId")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string LoweredUserName { get; set; }

        public string MobileAlias { get; set; }

        bool _isAnonymous = false;
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set { _isAnonymous = value == null ? false : value; }
        }

        public DateTime LastActivityDate { get; set; }

    }
    #endregion

    #region Structure Group
    [Table("cms_structure_groups")]
    public class StructureGroup
    {
        [Column("group_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        byte _type = 0;
        [Column("group_type")]
        public byte Type
        {
            get { return _type; }
            set { _type = value == null ? Convert.ToByte(0) : value; }
        }

        string _name = "";
        [Column("group_name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        [Column("created_by")]
        public Guid CraetedBy { get; set; }
    }
    #endregion

    #region Domain
    [Table("cms_domains")]
    public class Domain
    {
        [Column("domain_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //public int DomainId { get; set; }
        public int Id { get; set; }

        string _names = "";
        [Column("domain_names")]
        //public string Domains { get; set; }
        public string Names
        {
            get { return _names; }
            set { _names = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _homePageArticle = "";
        [Column("home_page_article")]
        public string HomePageArticle
        {
            get { return _homePageArticle; }
            set { _homePageArticle = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _errorPageArticle = "";
        [Column("error_page_article")]
        public string ErrorPageArticle
        {
            get { return _errorPageArticle; }
            set { _errorPageArticle = string.IsNullOrEmpty(value) ? "" : value; }
        }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        [Column("created_by")]
        public Guid CraetedBy { get; set; }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        [Column("updated_by")]
        public Guid UpdatedBy { get; set; }

        string _status = "A";
        [Column("domain_status")]
        public string Status
        {
            get { return _status; }
            set { _status = string.IsNullOrEmpty(value) ? "A" : value; }
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Site> Sites { get; set; }
    }
    #endregion

    #region Site
    [Table("cms_sites")]
    public class Site
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("site_id")]
        public int Id { get; set; }

        [Column("site_name")]
        public string Name { get; set; }

        int _cssId = 0;
        [Column("css_id")]
        public int CssId
        {
            get { return _cssId; }
            set { _cssId = value == null ? 0 : value; }
        }

        int _mobileCssId = 0;
        [Column("css_id_mobile")]
        public int MobileCssId
        {
            get { return _mobileCssId; }
            set { _mobileCssId = value == null ? 0 : value; }
        }

        int _printCssId = 0;
        [Column("css_id_print")]
        public int PrintCssId
        {
            get { return _printCssId; }
            set { _printCssId = value == null ? 0 : value; }
        }

        int _templateId = 0;
        [Column("template_id")]
        public int TemplateId
        {
            get { return _templateId; }
            set { _templateId = value == null ? 0 : value; }
        }

        int _mobileTemplateId = 0;
        [Column("template_id_mobile")]
        public int MobileTemplateId
        {
            get { return _mobileTemplateId; }
            set { _mobileTemplateId = value == null ? 0 : value; }
        }

        [Column("publisher_id")]
        public Guid CreatedBy { get; set; }

        string _keywords = "";
        [Column("site_keywords")]
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _header = "";
        [Column("site_header")]
        public string Header
        {
            get { return _header; }
            set { _header = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _js = "";
        [Column("site_js")]
        public string JS
        {
            get { return _js; }
            set { _js = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _customBody = "";
        [Column("custom_body")]
        public string CustomBody
        {
            get { return _customBody; }
            set { _customBody = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _icon = "";
        [Column("site_icon")]
        public string Icon
        {
            get { return _icon; }
            set { _icon = string.IsNullOrEmpty(value) ? "" : value; }
        }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        string _article1 = "";
        [Column("article_1")]
        public string Article1
        {
            get { return _article1; }
            set { _article1 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article2 = "";
        [Column("article_2")]
        public string Article2
        {
            get { return _article2; }
            set { _article2 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article3 = "";
        [Column("article_3")]
        public string Article3
        {
            get { return _article3; }
            set { _article3 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article4 = "";
        [Column("article_4")]
        public string Article4
        {
            get { return _article4; }
            set { _article4 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article5 = "";
        [Column("article_5")]
        public string Article5
        {
            get { return _article5; }
            set { _article5 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _analytics = "";
        [Column("analytics")]
        public string Analytics
        {
            get { return _analytics; }
            set { _analytics = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _tagArticle = "";
        [Column("tag_detail_article")]
        public string TagArticle
        {
            get { return _tagArticle; }
            set { _tagArticle = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("group_id")]
        public int? GroupId { get; set; }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _metaDescription = "";
        [Column("meta_description")]
        public string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content1EditorType = "H";
        [Column("content_1_editor_type")]
        public string Content1EditorType
        {
            get { return _content1EditorType; }
            set { _content1EditorType = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content2EditorType = "H";
        [Column("content_2_editor_type")]
        public string Content2EditorType
        {
            get { return _content2EditorType; }
            set { _content2EditorType = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content3EditorType = "H";
        [Column("content_3_editor_type")]
        public string Content3EditorType
        {
            get { return _content3EditorType; }
            set { _content3EditorType = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content4EditorType = "H";
        [Column("content_4_editor_type")]
        public string Content4EditorType
        {
            get { return _content4EditorType; }
            set { _content4EditorType = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content5EditorType = "H";
        [Column("content_5_editor_type")]
        public string Content5EditorType
        {
            get { return _content5EditorType; }
            set { _content5EditorType = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _defaultArticle = "";
        [Column("default_article")]
        public string DefaultArticle
        {
            get { return _defaultArticle; }
            set { _defaultArticle = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _omnitureCode = "";
        [Column("omniture_code")]
        public string OmnitureCode
        {
            get { return _omnitureCode; }
            set { _omnitureCode = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _alias = "";
        [Column("site_alias")]
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        string _file_path = "";
        [Column("file_path")]
        public string FilePath
        {
            get { return _file_path; }
            set { _file_path = value; }
        }

        //2017-09-18
        string _afterbody = "";
        [Column("afterbody")]
        public string AfterBody
        {
            get { return _afterbody; }
            set { _afterbody = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _sitesuffix = "";
        [Column("sitesuffix")]
        public string SiteSuffix
        {
            get { return _sitesuffix; }
            set { _sitesuffix = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _siteprefix = "";
        [Column("siteprefix")]
        public string SitePrefix
        {
            get { return _siteprefix; }
            set { _siteprefix = string.IsNullOrEmpty(value) ? "" : value; }
        }
        //2017-09-18


        [Column("domain_id")]
        public int? DomainId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Domain Domain { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ZoneGroup> ZoneGroups { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CmsUser CreatedUser { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual StructureGroup Group { get; set; }

    }
    #endregion

    #region Zone Group
    [Table("cms_zone_groups")]
    public class ZoneGroup
    {
        [Column("zone_group_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("zone_group_name")]
        public string Name { get; set; }

        string _keywords = "";
        [Column("zone_group_keywords")]
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = string.IsNullOrEmpty(value) ? "" : value; }
        }

        int _siteId = 0;
        [Column("site_id")]
        public int SiteId
        {
            get { return _siteId; }
            set { _siteId = value == null ? 0 : value; }
        }

        int _cssMerge = 0;
        [Column("css_merge")]
        public int CssMerge
        {
            get { return _cssMerge; }
            set { _cssMerge = value == null ? 0 : value; }
        }

        int _cssId = 0;
        [Column("css_id")]
        public int CssId
        {
            get { return _cssId; }
            set { _cssId = value == null ? 0 : value; }
        }

        int _mobileCssId = 0;
        [Column("css_id_mobile")]
        public int MobileCssId
        {
            get { return _mobileCssId; }
            set { _mobileCssId = value == null ? 0 : value; }
        }

        int _printCssId = 0;
        [Column("css_id_print")]
        public int PrintCssId
        {
            get { return _printCssId; }
            set { _printCssId = value == null ? 0 : value; }
        }

        int _templateId = 0;
        [Column("template_id")]
        public int TemplateId
        {
            get { return _templateId; }
            set { _templateId = value == null ? 0 : value; }
        }

        int _mobileTemplateId = 0;
        [Column("template_id_mobile")]
        public int MobileTemplateId
        {
            get { return _mobileTemplateId; }
            set { _mobileTemplateId = value == null ? 0 : value; }
        }

        //yeni eklenen alanlar
        string _before_head = "";
        [Column("before_head")]
        public string BeforeHead
        {
            get { return _before_head; }
            set { _before_head = value == null ? "" : value; }
        }

        string _before_body = "";
        [Column("before_body")]
        public string BeforeBody
        {
            get { return _before_body; }
            set { _before_body = value == null ? "" : value; }
        }
        //yeni eklenen alanlar

        string _customBody = "";
        [Column("custom_body")]
        public string CustomBody
        {
            get { return _customBody; }
            set { _customBody = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("publisher_id")]
        public Guid CreatedBy { get; set; }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        string _article1 = "";
        [Column("article_1")]
        public string Article1
        {
            get { return _article1; }
            set { _article1 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article2 = "";
        [Column("article_2")]
        public string Article2
        {
            get { return _article2; }
            set { _article2 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article3 = "";
        [Column("article_3")]
        public string Article3
        {
            get { return _article3; }
            set { _article3 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article4 = "";
        [Column("article_4")]
        public string Article4
        {
            get { return _article4; }
            set { _article4 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article5 = "";
        [Column("article_5")]
        public string Article5
        {
            get { return _article5; }
            set { _article5 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        byte _append1 = 0;
        [Column("append_1")]
        public byte Append1
        {
            get { return _append1; }
            set { _append1 = value == null ? Convert.ToByte(0) : value; }
        }

        byte _append2 = 0;
        [Column("append_2")]
        public byte Append2
        {
            get { return _append2; }
            set { _append2 = value == null ? Convert.ToByte(0) : value; }
        }

        byte _append3 = 0;
        [Column("append_3")]
        public byte Append3
        {
            get { return _append3; }
            set { _append3 = value == null ? Convert.ToByte(0) : value; }
        }

        byte _append4 = 0;
        [Column("append_4")]
        public byte Append4
        {
            get { return _append4; }
            set { _append4 = value == null ? Convert.ToByte(0) : value; }
        }

        byte _append5 = 0;
        [Column("append_5")]
        public byte Append5
        {
            get { return _append5; }
            set { _append5 = value == null ? Convert.ToByte(0) : value; }
        }

        string _analytics = "";
        [Column("analytics")]
        public string Analytics
        {
            get { return _analytics; }
            set { _analytics = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _tagArticle = "";
        [Column("tag_detail_article")]
        public string TagArticle
        {
            get { return _tagArticle; }
            set { _tagArticle = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _metaDescription = "";
        [Column("meta_description")]
        public string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _displayName = "";
        [Column("zone_group_name_display")]
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content1EditorType = "H";
        [Column("content_1_editor_type")]
        public string Content1EditorType
        {
            get { return _content1EditorType; }
            set { _content1EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _content2EditorType = "H";
        [Column("content_2_editor_type")]
        public string Content2EditorType
        {
            get { return _content2EditorType; }
            set { _content2EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _content3EditorType = "H";
        [Column("content_3_editor_type")]
        public string Content3EditorType
        {
            get { return _content3EditorType; }
            set { _content3EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _content4EditorType = "H";
        [Column("content_4_editor_type")]
        public string Content4EditorType
        {
            get { return _content4EditorType; }
            set { _content4EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _content5EditorType = "H";
        [Column("content_5_editor_type")]
        public string Content5EditorType
        {
            get { return _content5EditorType; }
            set { _content5EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _defaultArticle = "";
        [Column("default_article")]
        public string DefaultArticle
        {
            get { return _defaultArticle; }
            set { _defaultArticle = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _omnitureCode = "";
        [Column("omniture_code")]
        public string OmnitureCode
        {
            get { return _omnitureCode; }
            set { _omnitureCode = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _alias = "";
        [Column("zg_alias")]
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        //2017-09-18
        string _afterbody = "";
        [Column("afterbody")]
        public string AfterBody
        {
            get { return _afterbody; }
            set { _afterbody = string.IsNullOrEmpty(value) ? "" : value; }
        }
        //2017-09-18

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Site Site { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Zone> Zones { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CmsUser CreatedUser { get; set; }
    }
    #endregion

    #region Zone
    public class ZoneBase
    {
        public ZoneBase()
        {
            Name = "";
            Description = "";
            ZoneGroupId = 0;
            ZoneTypeId = 0;
            CssMerge = 0;
            MobileCssId = 0;
            CssId = 0;
            PrintCssId = 0;
            TemplateId = 0;
            MobileTemplateId = 0;
            CustomBody = "";
            Created = DateTime.Now;
            Article1 = "";
            Article2 = "";
            Article3 = "";
            Article4 = "";
            Article5 = "";
            Append1 = 0;
            Append2 = 0;
            Append3 = 0;
            Append4 = 0;
            Append5 = 0;
            Analytics = "";
            Keywords = "";
            MetaDescription = "";
            DisplayName = "";
            DefaultArticle = "";
            OmnitureCode = "";
            LangId = "";
            Alias = "";
        }
        //yeni eklenen alanlar
        string _before_head = "";
        [Column("before_head")]
        public string BeforeHead
        {
            get { return _before_head; }
            set { _before_head = value == null ? "" : value; }
        }

        string _before_body = "";
        [Column("before_body")]
        public string BeforeBody
        {
            get { return _before_body; }
            set { _before_body = value == null ? "" : value; }
        }
        //yeni eklenen alanlar

        [Column("zone_name")]
        public string Name { get; set; }

        [Column("zone_desc")]
        public string Description { get; set; }

        [Column("zone_group_id")]
        public int ZoneGroupId { get; set; }

        [Column("zone_type_id")]
        public int ZoneTypeId { get; set; }

        [Column("css_merge")]
        public int CssMerge { get; set; }

        [Column("css_id")]
        public int CssId { get; set; }

        [Column("css_id_mobile")]
        public int MobileCssId { get; set; }

        [Column("css_id_print")]
        public int PrintCssId { get; set; }

        [Column("template_id")]
        public int TemplateId { get; set; }

        [Column("template_id_mobile")]
        public int MobileTemplateId { get; set; }

        [Column("custom_body")]
        public string CustomBody { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("article_1")]
        public string Article1 { get; set; }

        [Column("article_2")]
        public string Article2 { get; set; }

        [Column("article_3")]
        public string Article3 { get; set; }

        [Column("article_4")]
        public string Article4 { get; set; }

        [Column("article_5")]
        public string Article5 { get; set; }

        [Column("append_1")]
        public byte Append1 { get; set; }

        [Column("append_2")]
        public byte Append2 { get; set; }

        [Column("append_3")]
        public byte Append3 { get; set; }

        [Column("append_4")]
        public byte Append4 { get; set; }

        [Column("append_5")]
        public byte Append5 { get; set; }

        [Column("analytics")]
        public string Analytics { get; set; }

        [Column("zone_keywords")]
        [DefaultValue("")]
        public string Keywords { get; set; }

        [Column("meta_description")]
        public string MetaDescription { get; set; }

        [Column("zone_name_display")]
        public string DisplayName { get; set; }

        //2017-09-18
        string _afterbody = "";
        [Column("afterbody")]
        public string AfterBody
        {
            get { return _afterbody; }
            set { _afterbody = string.IsNullOrEmpty(value) ? "" : value; }
        }
        //2017-09-18

        // Alttaki satırlar yorum halindeydi

        //[Column("locked")]
        //public DateTime? Locked { get; set; }

        //[Column("locked_by")]
        //public Guid? LockedBy { get; set; }

        // Buraya kadar yorum halindeydi


        [Column("default_article")]
        public string DefaultArticle { get; set; }

        [Column("omniture_code")]
        public string OmnitureCode { get; set; }

        [Column("lang_id")]
        public string LangId { get; set; }

        [Column("zone_alias")]
        public string Alias { get; set; }
    }

    [Table("cms_zones")]
    [JsonObject(IsReference = true)]
    public class Zone : ZoneBase
    {
        [Column("zone_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("locked")]
        public DateTime? Locked { get; set; }

        [Column("locked_by")]
        public Guid? LockedBy { get; set; }


        [Column("publisher_id")]
        public Guid CreatedBy { get; set; }

        [Column("updated")]
        public DateTime Updated { get; set; }

        [Column("zone_status")]
        [DefaultValue("P")]
        public string Status { get; set; }

        //yeni eklenen alanlar
        string _before_head = "";
        [Column("before_head")]
        public string BeforeHead
        {
            get { return _before_head; }
            set { _before_head = value == null ? "" : value; }
        }

        string _before_body = "";
        [Column("before_body")]
        public string BeforeBody
        {
            get { return _before_body; }
            set { _before_body = value == null ? "" : value; }
        }
        //yeni eklenen alanlar

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ZoneGroup ZoneGroup { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<ZoneRevision> Revisions { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        //public virtual List<Article> Articles { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CmsUser CreatedUser { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ArticleZone> ArticleZones { get; set; }

    }
    #endregion

    #region Zone Revision
    [Table("cms_zone_revision")]
    public class ZoneRevision : ZoneBase
    {
        public ZoneRevision()
        {
            ZoneStatus = "";
            RevisionName = "";
            RevisionNote = "";
            ContentEditorType1 = "";
            ContentEditorType2 = "";
            ContentEditorType3 = "";
            ContentEditorType4 = "";
            ContentEditorType5 = "";
        }

        [Column("zone_id")]
        public int ZoneId { get; set; }

        [Column("rev_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long RevisionId { get; set; }

        [Column("rev_date")]
        public DateTime RevisionDate { get; set; }

        [Column("zone_status")]
        [DefaultValue("P")]
        public string ZoneStatus { get; set; }

        [Column("revision_status")]
        [DefaultValue("N")]
        public string RevisionStatus { get; set; }

        [Column("revised_by")]
        public Guid RevisedBy { get; set; }

        [Column("rev_name")]
        public string RevisionName { get; set; }

        [Column("rev_note")]
        public string RevisionNote { get; set; }

        [Column("approval_date")]
        public DateTime? Approved { get; set; }

        [Column("approval_id")]
        public Guid? ApprovedBy { get; set; }

        [Column("created_by")]
        public Guid CreatedBy { get; set; }

        [Column("content_1_editor_type")]
        public string ContentEditorType1 { get; set; }

        [Column("content_2_editor_type")]
        public string ContentEditorType2 { get; set; }

        [Column("content_3_editor_type")]
        public string ContentEditorType3 { get; set; }

        [Column("content_4_editor_type")]
        public string ContentEditorType4 { get; set; }

        [Column("content_5_editor_type")]
        public string ContentEditorType5 { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Zone Zone { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CmsUser RevisedUser { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CmsUser ApprovedUser { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ArticleRevision> Articles { get; set; }
    }
    #endregion

    #region Article


    public class ArticleBase
    {

        int _classificationId = 0;
        [Column("clsf_id")]
        public int ClassificationId
        {
            get { return _classificationId; }
            set { _classificationId = value == null ? 0 : value; }
        }

        //yeni eklenen alanlar
        string _before_head = "";
        [Column("before_head")]
        public string BeforeHead
        {
            get { return _before_head; }
            set { _before_head = value == null ? "" : value; }
        }

        string _before_body = "";
        [Column("before_body")]
        public string BeforeBody
        {
            get { return _before_body; }
            set { _before_body = value == null ? "" : value; }
        }

        bool _no_index_no_follow = false;
        [Column("no_index_no_follow")]
        public bool NoIndexNoFollow
        {
            get { return _no_index_no_follow; }
            set { _no_index_no_follow = value; }
        }

        string _canonical_url = "";
        [Column("canonical_url")]
        public string CanonicalUrl
        {
            get { return _canonical_url; }
            set { _canonical_url = value == null ? "" : value; }
        }

        string _meta_title = "";
        [Column("meta_title")]
        public string MetaTitle
        {
            get { return _meta_title; }
            set { _meta_title = value == null ? "" : value; }
        }

        string _custom_html_attr = "";
        [Column("custom_html_attr")]
        public string CustomHtmlAttr
        {
            get { return _custom_html_attr; }
            set { _custom_html_attr = value == null ? "" : value; }
        }

        string _tag_ids = "";
        [Column("tag_ids")]
        public string TagIds
        {
            get { return _tag_ids; }
            set { _tag_ids = value == null ? "" : value; }
        }

        string _tag_contents = "";
        [Column("tag_contents")]
        public string TagContents
        {
            get { return _tag_contents; }
            set { _tag_contents = value == null ? "" : value; }
        }
        //yeni eklenen alanlar

        byte _status = 0;
        [Column("status")]
        public byte Status
        {
            get { return _status; }
            set { _status = value == null ? Convert.ToByte(0) : value; }
        }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public System.DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }


        //[Column("updated")]
        //public System.DateTime? Updated { get; set; }

        [Column("startdate")]
        public DateTime? Startdate { get; set; }

        [Column("enddate")]
        public DateTime? Enddate { get; set; }

        //[Column("publisher_id")]
        //public Guid CreatedBy { get; set; }

        //[Column("clicks")]
        //public int? Clicks { get; set; }

        int _order = 0;
        [Column("orderno")]
        public int Order
        {
            get { return _order; }
            set { _order = value == null ? 0 : value; }
        }

        string _langId = "TR";
        [Column("lang_id")]
        public string LangId
        {
            get { return _langId; }
            set { _langId = string.IsNullOrEmpty(value) ? "TR" : value; }
        }

        byte _navigationDisplay = 1;
        [Column("navigation_display")]
        public byte NavigationDisplay
        {
            get { return _navigationDisplay; }
            set { _navigationDisplay = value == null ? Convert.ToByte(1) : value; }
        }

        int _navigationZoneId = 0;
        [Column("navigation_zone_id")]
        public int NavigationZoneId
        {
            get { return _navigationZoneId; }
            set { _navigationZoneId = value == null ? 0 : value; }
        }

        string _menuText = "";
        [Column("menu_text")]
        [DefaultValue("")]
        public string MenuText
        {
            get { return _menuText; }
            set { _menuText = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _headline = "";
        [Column("headline")]
        public string Headline
        {
            get { return _headline; }
            set { _headline = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _summary = "";
        [Column("summary")]
        public string Summary
        {
            get { return _summary; }
            set { _summary = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _keywords = "";
        [Column("keywords")]
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = string.IsNullOrEmpty(value) ? "" : value; }
        }

        byte _articleType = 0;
        [Column("article_type")]
        public byte ArticleType
        {
            get { return _articleType; }
            set { _articleType = value == null ? Convert.ToByte(0) : value; }
        }

        string _articleTypeDetail = "";
        [Column("article_type_detail")]
        public string ArticleTypeDetail
        {
            get { return _articleTypeDetail; }
            set { _articleTypeDetail = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article1 = "";
        [Column("article_1")]
        public string Article1
        {
            get { return _article1; }
            set { _article1 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article2 = "";
        [Column("article_2")]
        public string Article2
        {
            get { return _article2; }
            set { _article2 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article3 = "";
        [Column("article_3")]
        public string Article3
        {
            get { return _article3; }
            set { _article3 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article4 = "";
        [Column("article_4")]
        public string Article4
        {
            get { return _article4; }
            set { _article4 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article5 = "";
        [Column("article_5")]
        public string Article5
        {
            get { return _article5; }
            set { _article5 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom1 = "";
        [Column("custom_1")]
        public string Custom1
        {
            get { return _custom1; }
            set { _custom1 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom2 = "";
        [Column("custom_2")]
        public string Custom2
        {
            get { return _custom2; }
            set { _custom2 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom3 = "";
        [Column("custom_3")]
        public string Custom3
        {
            get { return _custom3; }
            set { _custom3 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom4 = "";
        [Column("custom_4")]
        public string Custom4
        {
            get { return _custom4; }
            set { _custom4 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom5 = "";
        [Column("custom_5")]
        public string Custom5
        {
            get { return _custom5; }
            set { _custom5 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom6 = "";
        [Column("custom_6")]
        public string Custom6
        {
            get { return _custom6; }
            set { _custom6 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom7 = "";
        [Column("custom_7")]
        public string Custom7
        {
            get { return _custom7; }
            set { _custom7 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom8 = "";
        [Column("custom_8")]
        public string Custom8
        {
            get { return _custom8; }
            set { _custom8 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom9 = "";
        [Column("custom_9")]
        public string Custom9
        {
            get { return _custom9; }
            set { _custom9 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom10 = "";
        [Column("custom_10")]
        public string Custom10
        {
            get { return _custom10; }
            set { _custom10 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom11 = "";
        [Column("custom_11")]
        public string Custom11
        {
            get { return _custom11; }
            set { _custom11 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom12 = "";
        [Column("custom_12")]
        public string Custom12
        {
            get { return _custom12; }
            set { _custom12 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom13 = "";
        [Column("custom_13")]
        public string Custom13
        {
            get { return _custom13; }
            set { _custom13 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom14 = "";
        [Column("custom_14")]
        public string Custom14
        {
            get { return _custom14; }
            set { _custom14 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom15 = "";
        [Column("custom_15")]
        public string Custom15
        {
            get { return _custom15; }
            set { _custom15 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom16 = "";
        [Column("custom_16")]
        public string Custom16
        {
            get { return _custom16; }
            set { _custom16 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom17 = "";
        [Column("custom_17")]
        public string Custom17
        {
            get { return _custom17; }
            set { _custom17 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom18 = "";
        [Column("custom_18")]
        public string Custom18
        {
            get { return _custom18; }
            set { _custom18 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom19 = "";
        [Column("custom_19")]
        public string Custom19
        {
            get { return _custom19; }
            set { _custom19 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom20 = "";
        [Column("custom_20")]
        public string Custom20
        {
            get { return _custom20; }
            set { _custom20 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _flag1 = false;
        [Column("flag_1")]
        public bool Flag1
        {
            get { return _flag1; }
            set { _flag1 = value == null ? false : value; }
        }

        bool _flag2 = false;
        [Column("flag_2")]
        public bool Flag2
        {
            get { return _flag2; }
            set { _flag2 = value == null ? false : value; }
        }

        bool _flag3 = false;
        [Column("flag_3")]
        public bool Flag3
        {
            get { return _flag3; }
            set { _flag3 = value == null ? false : value; }
        }

        bool _flag4 = false;
        [Column("flag_4")]
        public bool Flag4
        {
            get { return _flag4; }
            set { _flag4 = value == null ? false : value; }
        }

        bool _flag5 = false;
        [Column("flag_5")]
        public bool Flag5
        {
            get { return _flag5; }
            set { _flag5 = value == null ? false : value; }
        }

        [Column("date_1")]
        public DateTime? date_1 { get; set; }

        [Column("date_2")]
        public DateTime? date_2 { get; set; }

        [Column("date_3")]
        public DateTime? date_3 { get; set; }

        [Column("date_4")]
        public DateTime? date_4 { get; set; }

        [Column("date_5")]
        public DateTime? date_5 { get; set; }

        byte _cl1 = 1;
        [Column("cl_1")]
        public byte Cl1
        {
            get { return _cl1; }
            set { _cl1 = value == null ? Convert.ToByte(1) : value; }
        }

        byte _cl2 = 1;
        [Column("cl_2")]
        public byte Cl2
        {
            get { return _cl2; }
            set { _cl2 = value == null ? Convert.ToByte(1) : value; }
        }

        byte _cl3 = 1;
        [Column("cl_3")]
        public byte Cl3
        {
            get { return _cl3; }
            set { _cl3 = value == null ? Convert.ToByte(1) : value; }
        }

        byte _cl4 = 1;
        [Column("cl_4")]
        public byte Cl4
        {
            get { return _cl4; }
            set { _cl4 = value == null ? Convert.ToByte(1) : value; }
        }

        byte _cl5 = 1;
        [Column("cl_5")]
        public byte Cl5
        {
            get { return _cl5; }
            set { _cl5 = value == null ? Convert.ToByte(1) : value; }
        }
        string _customBody = "";
        [Column("custom_body")]
        public string CustomBody
        {
            get { return _customBody; }
            set { _customBody = string.IsNullOrEmpty(value) ? "" : value; }
        }

        //[Column("rating")]
        //public int? Rating { get; set; }

        //[Column("ratingcount")]
        //public int? RaitingCount { get; set; }

        //[Column("locked")]
        //public DateTime? Locked { get; set; }

        //[Column("locked_by")]
        //public Guid LockedBy { get; set; }

        // Yukarıdaki iki property yorum halindeydi

        string _metaDescription = "";
        [Column("meta_description")]
        [DefaultValue("")]
        public string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        //[Column("content_1_editor_type")]
        //public string ContentEditorType1 { get; set; }

        //[Column("content_2_editor_type")]
        //public string ContentEditorType2 { get; set; }

        //[Column("content_3_editor_type")]
        //public string ContentEditorType3 { get; set; }

        //[Column("content_4_editor_type")]
        //public string ContentEditorType4 { get; set; }

        //[Column("content_5_editor_type")]
        //public string ContentEditorType5 { get; set; }

        string _omnitureCode = "";
        [Column("omniture_code")]
        public string OmnitureCode
        {
            get { return _omnitureCode; }
            set { _omnitureCode = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _customSettings = "";
        [Column("custom_setting")]
        public string CustomSettings
        {
            get { return _customSettings; }
            set { _customSettings = string.IsNullOrEmpty(value) ? "" : value; }
        }

        //2017-09-18
        string _afterbody = "";
        [Column("afterbody")]
        public string AfterBody
        {
            get { return _afterbody; }
            set { _afterbody = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _hideprefix = false;
        [Column("hideprefix")]
        public bool HidePrefix
        {
            get { return _hideprefix; }
            set { _hideprefix = value == null ? false : value; }
        }

        bool _hidesuffix = false;
        [Column("hidesuffix")]
        public bool HideSuffix
        {
            get { return _hidesuffix; }
            set { _hidesuffix = value == null ? false : value; }
        }
        //2017-09-18
    }

    [Table("cms_articles")]
    public class Article : ArticleBase
    {
        [Column("article_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        int _rating = 0;
        [Column("rating")]
        public int Rating
        {
            get { return _rating; }
            set { _rating = value == null ? 0 : value; }
        }

        int _ratingCount = 0;
        [Column("ratingcount")]
        public int RatingCount
        {
            get { return _ratingCount; }
            set { _ratingCount = value == null ? 0 : value; }
        }

        int _clicks = 0;
        [Column("clicks")]
        public int Clicks
        {
            get { return _clicks; }
            set { _clicks = value == null ? 0 : value; }
        }

        [Column("locked")]
        public DateTime? Locked { get; set; }

        [Column("locked_by")]
        public Guid? LockedBy { get; set; }

        [Column("updated")]
        public System.DateTime? Updated { get; set; }

        [Column("publisher_id")]
        public Guid CreatedBy { get; set; }


        //public virtual ICollection<Zone> Zones { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ArticleRevision> Revisions { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ArticleZone> ArticleZones { get; set; }
    }
    #endregion

    #region Article Revision
    [Table("cms_article_revision")]
    public class ArticleRevision : ArticleBase
    {

        string _contentEditorType1 = "H";
        [Column("content_1_editor_type")]
        public string ContentEditorType1
        {
            get { return _contentEditorType1; }
            set { _contentEditorType1 = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _contentEditorType2 = "H";
        [Column("content_2_editor_type")]
        public string ContentEditorType2
        {
            get { return _contentEditorType2; }
            set { _contentEditorType2 = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _contentEditorType3 = "H";
        [Column("content_3_editor_type")]
        public string ContentEditorType3
        {
            get { return _contentEditorType3; }
            set { _contentEditorType3 = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _contentEditorType4 = "H";
        [Column("content_4_editor_type")]
        public string ContentEditorType4
        {
            get { return _contentEditorType4; }
            set { _contentEditorType4 = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _contentEditorType5 = "H";
        [Column("content_5_editor_type")]
        public string ContentEditorType5
        {
            get { return _contentEditorType5; }
            set { _contentEditorType5 = string.IsNullOrEmpty(value) ? "H" : value; }
        }


        [Column("created_by")]
        public Guid CreatedBy { get; set; }



        [Column("article_id")]
        public int ArticleId { get; set; }

        [Column("rev_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long RevisionId { get; set; }

        DateTime _revisionDate = DateTime.Now;
        [Column("rev_date")]
        public DateTime RevisionDate
        {
            get { return _revisionDate; }
            set { _revisionDate = value == null ? DateTime.Now : value; }
        }

        string _revisionStatus = "N";
        [Column("revision_status")]
        public string RevisionStatus
        {
            get { return _revisionStatus; }
            set { _revisionStatus = string.IsNullOrEmpty(value) ? "N" : value; }
        }

        [Column("revised_by")]
        public Guid RevisedBy { get; set; }

        string _revisionName = "";
        [Column("rev_name")]
        public string RevisionName
        {
            get { return _revisionName; }
            set { _revisionName = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _revisionNote = "";
        [Column("rev_note")]
        public string RevisionNote
        {
            get { return _revisionNote; }
            set { _revisionNote = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("approval_date")]
        public DateTime? Approved { get; set; }

        [Column("approval_id")]
        public Guid? ApprovedBy { get; set; }

        bool _revisionFlag1 = false;
        [Column("rev_flag_1")]
        public bool RevisionFlag1
        {
            get { return _revisionFlag1; }
            set { _revisionFlag1 = value == null ? false : value; }
        }

        bool _revisionFlag2 = false;
        [Column("rev_flag_2")]
        public bool RevisionFlag2
        {
            get { return _revisionFlag2; }
            set { _revisionFlag2 = value == null ? false : value; }
        }

        bool _revisionFlag3 = false;
        [Column("rev_flag_3")]
        public bool RevisionFlag3
        {
            get { return _revisionFlag3; }
            set { _revisionFlag3 = value == null ? false : value; }
        }

        bool _revisionFlag4 = false;
        [Column("rev_flag_4")]
        public bool RevisionFlag4
        {
            get { return _revisionFlag4; }
            set { _revisionFlag4 = value == null ? false : value; }
        }

        bool _revisionFlag5 = false;
        [Column("rev_flag_5")]
        public bool RevisionFlag5
        {
            get { return _revisionFlag5; }
            set { _revisionFlag5 = value == null ? false : value; }
        }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ZoneRevision> Zones { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Article Article { get; set; }
    }
    #endregion

    #region Article File

    public class ArticleFileBase
    {
        [Column("article_id")]
        public int ArticleId { get; set; }

        int _fileTypeId = 0;
        [Column("file_type_id")]
        public int FileTypeId
        {
            get { return _fileTypeId; }
            set { _fileTypeId = value == null ? 0 : value; }
        }

        string _title = "";
        [Column("file_title")]
        public string Title
        {
            get { return _title; }
            set { _title = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _comment = "";
        [Column("file_comment")]
        public string Comment
        {
            get { return _comment; }
            set { _comment = string.IsNullOrEmpty(value) ? "" : value; }
        }

        int _fileOrder = 0;
        [Column("file_order")]
        public int FileOrder
        {
            get { return _fileOrder; }
            set { _fileOrder = value == null ? 0 : value; }
        }

        string _file1 = "";
        [Column("file_name_1")]
        public string File1
        {
            get { return _file1; }
            set { _file1 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file2 = "";
        [Column("file_name_2")]
        public string File2
        {
            get { return _file2; }
            set { _file2 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file3 = "";
        [Column("file_name_3")]
        public string File3
        {
            get { return _file3; }
            set { _file3 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file4 = "";
        [Column("file_name_4")]
        public string File4
        {
            get { return _file4; }
            set { _file4 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file5 = "";
        [Column("file_name_5")]
        public string File5
        {
            get { return _file5; }
            set { _file5 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file6 = "";
        [Column("file_name_6")]
        public string File6
        {
            get { return _file6; }
            set { _file6 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file7 = "";
        [Column("file_name_7")]
        public string File7
        {
            get { return _file7; }
            set { _file7 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file8 = "";
        [Column("file_name_8")]
        public string File8
        {
            get { return _file8; }
            set { _file8 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file9 = "";
        [Column("file_name_9")]
        public string File9
        {
            get { return _file9; }
            set { _file9 = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file10 = "";
        [Column("file_name_10")]
        public string File10
        {
            get { return _file10; }
            set { _file10 = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }

    [Table("cms_article_files")]
    public class ArticleFile : ArticleFileBase
    {
        [Column("file_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long FileId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Article Article { get; set; }

        //public virtual ICollection<ArticleFileRevision> Revisions { get; set; }

    }
    #endregion

    #region Article File Revision
    [Table("cms_article_files_revision")]
    public class ArticleFileRevision
    {
        [Column("rev_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long RevisionId { get; set; }

        [Column("article_id")]
        public int ArticleId { get; set; }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        [Column("created_by")]
        public Guid CreatedBy { get; set; }

        DateTime _revisionDate = DateTime.Now;
        [Column("rev_date")]
        public DateTime RevisionDate
        {
            get { return _revisionDate; }
            set { _revisionDate = value == null ? DateTime.Now : value; }
        }

        [Column("revised_by")]
        public Guid RevisedBy { get; set; }

        string _revisionStatus = "N";
        [Column("revision_status")]
        public string RevisionStatus
        {
            get { return _revisionStatus; }
            set { _revisionStatus = string.IsNullOrEmpty(value) ? "N" : value; }
        }

        [Column("approval_date")]
        public DateTime? Approved { get; set; }

        [Column("approval_id")]
        public Guid? ApprovedBy { get; set; }

        //  public virtual ICollection<ArticleFile> Files { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ArticleFileRevisionFile> FileRevisions { get; set; }
    }
    #endregion

    #region Article File Revision File
    [Table("cms_article_files_revision_files")]
    public class ArticleFileRevisionFile : ArticleFileBase
    {
        [Column("af_rf_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long RevisionFileId { get; set; }

        [Column("rev_id")]
        public long RevisionId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ArticleFileRevision Revision { get; set; }
    }
    #endregion

    #region Yeni Yazılanlar

    [Table("cms_error_logs")]
    public class ErrorLog
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ErrorId")]
        public int Id { get; set; }
        [Column("ControllerName")]
        public string ControllerName { get; set; }
        [Column("ActionName")]
        public string ActionName { get; set; }
        [Column("UserId")]
        public Guid? UserId { get; set; }
        [Column("LogDate")]
        public DateTime LogDate { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
        [Column("Message")]
        public string Message { get; set; }
        [Column("InnerException")]
        public string InnerException { get; set; }
        [Column("IsInCms")]
        public bool IsInCms { get; set; }
        [Column("StackTrace")]
        public string StackTrace { get; set; }
        [Column("AbsoluteUrl")]
        public string AbsolutePath { get; set; }
        [Column("LineNumber")]
        public int LineNumber { get; set; }
        [Column("IP")]
        public string IP { get; set; }
    }

    [Table("cms_newsletteremails")]
    public class NewsletterEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool MembershipPermission { get; set; }
        public bool eBulletinPermission { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }

    }

    #region Tags
    [Table("cms_tags")]
    public class Tag
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }
        [Column("Text")]
        public string Text { get; set; }
        [Column("SiteID")]
        public int SiteID { get; set; }
        [Column("AddedDate")]
        public DateTime AddedDate { get; set; }
        [Column("PublisherID")]
        public Guid PublisherID { get; set; }
        [Column("Counter")]
        public int Counter { get; set; }
        [Column("Alias")]
        public string Alias { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Site Site { get; set; }
    }

    public class TagResult
    {
        public string id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
    #endregion

    #region Widgets
    [Table("cms_widget_users")]
    public class WidgetUser
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("WidgetID")]
        public int WidgetID { get; set; }
        [Column("UserID")]
        public Guid UserID { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }

    [Table("cms_widget_configs")]
    public class WidgetConfig
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("WidgetUserID")]
        public int WidgetUserID { get; set; }
        [Column("ParamKey")]
        public string ParamKey { get; set; }
        [Column("ParamValue")]
        public string ParamValue { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
    #endregion


    #region Template
    [Table("cms_templates")]
    public class Template
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("template_id")]
        public int Id { get; set; }

        string _status = "A";
        [Column("template_status")]
        public string Status
        {
            get { return _status; }
            set { _status = string.IsNullOrEmpty(value) ? "A" : value; }
        }

        Byte _type = 0;
        [Column("template_type")]
        public Byte Type
        {
            get { return _type; }
            set { _type = value == null ? Convert.ToByte(0) : value; }
        }

        string _name = "";
        [Column("template_name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _html = "";
        [Column("template_html")]
        public string Html
        {
            get { return _html; }
            set { _html = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("publisher_id")]
        public Guid PublisherID { get; set; }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        int _groupID = 0;
        [Column("group_id")]
        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value == null ? 0 : value; }
        }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _content1EditorType = "H";
        [Column("content_1_editor_type")]
        public string Content1EditorType
        {
            get { return _content1EditorType; }
            set { _content1EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _templateDoctype = "";
        [Column("template_doctype")]
        public string TemplateDoctype
        {
            get { return _templateDoctype; }
            set { _templateDoctype = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }

    #endregion

    //#region User
    //[Table("aspnet_users")]
    //public class User
    //{
    //    [Column("ApplicationId")]
    //    public Application ApplicationId { get; set; }

    //    [Key]
    //    [Column("UserId")]
    //    public Guid Id { get; set; }

    //    [Column("UserName")]
    //    public string Name { get; set; }

    //    [Column("LoweredUserName")]
    //    public string LoweredName { get; set; }

    //    [Column("MobileAlias")]
    //    public string MobileAlias { get; set; }

    //    [Column("IsAnonymous")]
    //    public bool IsAnonymous { get; set; }

    //    [Column("LastActivityDate")]
    //    public DateTime LastActivityDate { get; set; }

    //    public virtual ICollection<Site> Sites { get; set; }
    //    public virtual ICollection<ZoneGroup> ZoneGroups { get; set; }
    //    public virtual ICollection<ZoneRevision> ZoneRevisions { get; set; }
    //    public virtual ICollection<Zone> Zones { get; set; }
    //    public virtual Application Application { get; set; }
    //}

    //#endregion

    #region Application
    [Table("aspnet_Applications")]
    public class Application
    {
        [Column("ApplicationName")]
        public string Name { get; set; }

        [Column("LoweredApplicationName")]
        public string LoweredName { get; set; }

        Guid _id = Guid.NewGuid();
        [Key]
        [Column("ApplicationId")]
        public Guid Id
        {
            get { return _id; }
            set { _id = value == null ? Guid.NewGuid() : value; }
        }

        [Column("Description")]
        public string Description { get; set; }

        // Foreginkey bağlantıları yapılacak
    }

    #endregion

    #region TemplateRevision
    [Table("cms_template_revisions")]
    public class TemplateRevision
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("history_id")]
        public int Id { get; set; }

        int _templateID = 0;
        [Column("template_id")]
        public int TemplateID
        {
            get { return _templateID; }
            set { _templateID = value == null ? 0 : value; }
        }

        string _templateHtml = "";
        [Column("template_html")]
        public string TemplateHtml
        {
            get { return _templateHtml; }
            set { _templateHtml = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("publisher_id")]
        public Guid PublisherID { get; set; }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        Byte _templateType = 0;
        [Column("template_type")]
        public Byte TemplateType
        {
            get { return _templateType; }
            set { _templateType = value == null ? Convert.ToByte(0) : value; }
        }

        string _content1EditorType = "H";
        [Column("content_1_editor_type")]
        public string Content1EditorType
        {
            get { return _content1EditorType; }
            set { _content1EditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _templateDoctype = "";
        [Column("template_doctype")]
        public string TemplateDoctype
        {
            get { return _templateDoctype; }
            set { _templateDoctype = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }

    #endregion

    #region ArticleZone
    [Table("cms_article_zones")]

    public class ArticleZone
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("rel_id")]
        public long RelID { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("zone_id")]
        public int ZoneID { get; set; }

        int _azOrder = 0;
        [Column("az_order")]
        public int AzOrder
        {
            get { return _azOrder; }
            set { _azOrder = value == null ? 0 : value; }
        }

        string _azAlias = "";
        [Column("az_alias")]
        public string AzAlias
        {
            get { return _azAlias; }
            set { _azAlias = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _isAliasProtected = false;
        [Column("is_alias_protected")]
        public bool IsAliasProtected
        {
            get { return _isAliasProtected; }
            set { _isAliasProtected = Convert.ToBoolean(value); }
        }

        bool _isPage = true;
        [Column("is_page")]
        public bool IsPage
        {
            get { return _isPage; }
            set { _isPage = Convert.ToBoolean(value); }
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Article Article { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Zone Zone { get; set; }


    }

    #endregion

    #region ArticleZoneRevision
    [Table("cms_article_zones_revision")]
    public class ArticleZoneRevision
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }

        [Column("rev_id")]
        public long RevID { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("zone_id")]
        public int ZoneID { get; set; }

        int _azOrder = 0;
        [Column("az_order")]
        public int AzOrder
        {
            get { return _azOrder; }
            set { _azOrder = value == null ? 0 : value; }
        }

        string _azAlias = "";
        [Column("az_alias")]
        public string AzAlias
        {
            get { return _azAlias; }
            set { _azAlias = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _isAliasProtected = false;
        [Column("is_alias_protected")]
        public bool IsAliasProtected
        {
            get { return _isAliasProtected; }
            set { _isAliasProtected = Convert.ToBoolean(value); }
        }

        bool _isPage = true;
        [Column("is_page")]
        public bool IsPage
        {
            get { return _isPage; }
            set { _isPage = Convert.ToBoolean(value); }
        }
    }

    #endregion

    #region LanguageRelation
    [Table("cms_language_relations")]
    public class LanguageRelation
    {
        [Column("Id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("lr_id")]
        public long LanguageRelationID { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("zone_id")]
        public int ZoneID { get; set; }

    }

    #endregion


    #region Favorite
    //skylife facebook login ve favoriler için alandır skylife dışında kaldırılmalı
    [Table("cms_favorite")]
    public class Favorite
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("FacebookId")]
        public string FacebookId { get; set; }

        [Column("ArticleId")]
        public int ArticleId { get; set; }

        [Column("CreateDt")]
        public DateTime CreateDt { get; set; }

        [Column("ModifiedDt")]
        public DateTime ModifiedDt { get; set; }

        [Column("Status")]
        public int Status { get; set; }
    }

    #endregion







    #region Language
    [Table("cms_languages")]
    public class Language
    {
        [Key]
        string _id = "TR";
        [Column("lang_id")]
        public string ID
        {
            get { return _id; }
            set { _id = string.IsNullOrEmpty(value) ? "TR" : value; }
        }

        string _name = "";
        [Column("lang_name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _xml = "";
        [Column("lang_xml")]
        public string Xml
        {
            get { return _xml; }
            set { _xml = string.IsNullOrEmpty(value) ? "" : value; }
        }

        int _order = 0;
        [Column("lang_order")]
        public int Order
        {
            get { return _order; }
            set { _order = value == null ? 0 : value; }
        }

        [Column("publisher_id")]
        public Guid PublisherID { get; set; }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        string _alias = "";
        [Column("lang_alias")]
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
    }

    #endregion

    #region LanguageRelationRevision
    [Table("cms_language_relations_revision")]
    public class LanguageRelationRevision
    {

        [Column("Id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Key]
        [Column("lr_id")]
        public long LanguageRelationID { get; set; }

        [Column("rev_id")]
        public long RevisionID { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("zone_id")]
        public int ZoneID { get; set; }

    }

    #endregion

    #region ArticleRelationRevision
    [Description("View")]
    [Table("cms_article_relation_revision")]
    public class ArticleRelationRevision
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("arr_id")]
        public long ID { get; set; }

        [Column("rev_id")]
        public long RevisionID { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("related_zone_id")]
        public int RelatedZoneID { get; set; }

        [Column("related_article_id")]
        public int RelatedArticleID { get; set; }
    }

    #endregion

    #region ArticleCache
    [Table("cms_article_cache")]
    public class ArticleCache
    {
        [Column("cache_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Column("zone_id")]
        public int ZoneID { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

    }
    #endregion

    #region CmsConfig
    [Table("cms_config")]
    public class CmsConfig
    {
        [Column("config_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("config_name")]
        public string Name { get; set; }

        [Column("config_value_local")]
        public string LocalValue { get; set; }

        [Column("config_value_remote")]
        public string RemoteValue { get; set; }

        [Column("isDefault")]
        public string IsDefault { get; set; }

        [Column("publisher_id")]
        public Guid PublisherID { get; set; }

        [Column("updated")]
        public DateTime Updated { get; set; }

    }
    #endregion

    #region FileType
    [Table("cms_file_types")]
    public class FileType
    {
        [Column("type_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("type_name")]
        public string Name { get; set; }

        [Column("type_alias")]
        public string Alias { get; set; }

        string _file1Name = "";
        [Column("file1_name")]
        public string File1Name
        {
            get { return _file1Name; }
            set { _file1Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file2Name = "";
        [Column("file2_name")]
        public string File2Name
        {
            get { return _file2Name; }
            set { _file2Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file3Name = "";
        [Column("file3_name")]
        public string File3Name
        {
            get { return _file3Name; }
            set { _file3Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file4Name = "";
        [Column("file4_name")]
        public string File4Name
        {
            get { return _file4Name; }
            set { _file4Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file5Name = "";
        [Column("file5_name")]
        public string File5Name
        {
            get { return _file5Name; }
            set { _file5Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file6Name = "";
        [Column("file6_name")]
        public string File6Name
        {
            get { return _file6Name; }
            set { _file6Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file7Name = "";
        [Column("file7_name")]
        public string FileName7
        {
            get { return _file7Name; }
            set { _file7Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file8Name = "";
        [Column("file8_name")]
        public string File8Name
        {
            get { return _file8Name; }
            set { _file8Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file9Name = "";
        [Column("file9_name")]
        public string File9Name
        {
            get { return _file9Name; }
            set { _file9Name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file10Name = "";
        [Column("file10_name")]
        public string File10Name
        {
            get { return _file10Name; }
            set { _file10Name = string.IsNullOrEmpty(value) ? "" : value; }
        }


        string _file1Extension = "";
        [Column("file1_extension")]
        public string File1Extension
        {
            get { return _file1Extension; }
            set { _file1Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file2Extension = "";
        [Column("file2_extension")]
        public string File2Extension
        {
            get { return _file2Extension; }
            set { _file2Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file3Extension = "";
        [Column("file3_extension")]
        public string File3Extension
        {
            get { return _file3Extension; }
            set { _file3Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file4Extension = "";
        [Column("file4_extension")]
        public string File4Extension
        {
            get { return _file4Extension; }
            set { _file4Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file5Extension = "";
        [Column("file5_extension")]
        public string File5Extension
        {
            get { return _file5Extension; }
            set { _file5Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file6Extension = "";
        [Column("file6_extension")]
        public string File6Extension
        {
            get { return _file6Extension; }
            set { _file6Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file7Extension = "";
        [Column("file7_extension")]
        public string FileExtension7
        {
            get { return _file7Extension; }
            set { _file7Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file8Extension = "";
        [Column("file8_extension")]
        public string File8Extension
        {
            get { return _file8Extension; }
            set { _file8Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file9Extension = "";
        [Column("file9_extension")]
        public string File9Extension
        {
            get { return _file9Extension; }
            set { _file9Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file10Extension = "";
        [Column("file10_extension")]
        public string File10Extension
        {
            get { return _file10Extension; }
            set { _file10Extension = string.IsNullOrEmpty(value) ? "" : value; }
        }


        string _file1WidthHeight = "";
        [Column("file1_wh")]
        public string File1WidthHeight
        {
            get { return _file1WidthHeight; }
            set { _file1WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file2WidthHeight = "";
        [Column("file2_wh")]
        public string File2WidthHeight
        {
            get { return _file2WidthHeight; }
            set { _file2WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file3WidthHeight = "";
        [Column("file3_wh")]
        public string File3WidthHeight
        {
            get { return _file3WidthHeight; }
            set { _file3WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file4WidthHeight = "";
        [Column("file4_wh")]
        public string File4WidthHeight
        {
            get { return _file4WidthHeight; }
            set { _file4WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file5WidthHeight = "";
        [Column("file5_wh")]
        public string File5WidthHeight
        {
            get { return _file5WidthHeight; }
            set { _file5WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file6WidthHeight = "";
        [Column("file6_wh")]
        public string File6WidthHeight
        {
            get { return _file6WidthHeight; }
            set { _file6WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file7WidthHeight = "";
        [Column("file7_wh")]
        public string FileWidthHeight7
        {
            get { return _file7WidthHeight; }
            set { _file7WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file8WidthHeight = "";
        [Column("file8_wh")]
        public string File8WidthHeight
        {
            get { return _file8WidthHeight; }
            set { _file8WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file9WidthHeight = "";
        [Column("file9_wh")]
        public string File9WidthHeight
        {
            get { return _file9WidthHeight; }
            set { _file9WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _file10WidthHeight = "";
        [Column("file10_wh")]
        public string File10WidthHeight
        {
            get { return _file10WidthHeight; }
            set { _file10WidthHeight = string.IsNullOrEmpty(value) ? "" : value; }
        }


        int _file1Size = 0;
        [Column("file1_size")]
        public int File1Size
        {
            get { return _file1Size; }
            set { _file1Size = value == null ? 0 : value; }
        }

        int _file2Size = 0;
        [Column("file2_size")]
        public int File2Size
        {
            get { return _file2Size; }
            set { _file2Size = value == null ? 0 : value; }
        }

        int _file3Size = 0;
        [Column("file3_size")]
        public int File3Size
        {
            get { return _file3Size; }
            set { _file3Size = value == null ? 0 : value; }
        }

        int _file4Size = 0;
        [Column("file4_size")]
        public int File4Size
        {
            get { return _file4Size; }
            set { _file4Size = value == null ? 0 : value; }
        }

        int _file5Size = 0;
        [Column("file5_size")]
        public int File5Size
        {
            get { return _file5Size; }
            set { _file5Size = value == null ? 0 : value; }
        }

        int _file6Size = 0;
        [Column("file6_size")]
        public int File6Size
        {
            get { return _file6Size; }
            set { _file6Size = value == null ? 0 : value; }
        }

        int _file7Size = 0;
        [Column("file7_size")]
        public int FileSize7
        {
            get { return _file7Size; }
            set { _file7Size = value == null ? 0 : value; }
        }

        int _file8Size = 0;
        [Column("file8_size")]
        public int File8Size
        {
            get { return _file8Size; }
            set { _file8Size = value == null ? 0 : value; }
        }

        int _file9Size = 0;
        [Column("file9_size")]
        public int File9Size
        {
            get { return _file9Size; }
            set { _file9Size = value == null ? 0 : value; }
        }

        int _file10Size = 0;
        [Column("file10_size")]
        public int File10Size
        {
            get { return _file10Size; }
            set { _file10Size = value == null ? 0 : value; }
        }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        int _groupId = 0;
        [Column("group_id")]
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value == null ? 0 : value; }
        }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }
    #endregion

    #region STFEmail
    [Table("cms_stf_emails")]
    public class STFEmail
    {
        [Column("stf_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        string _fromName = "";
        [Column("from_name")]
        public string FromName
        {
            get { return _fromName; }
            set { _fromName = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _fromEmail = "";
        [Column("from_email")]
        public string FromEmail
        {
            get { return _fromEmail; }
            set { _fromEmail = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _fromIp = "";
        [Column("from_ip")]
        public string FromIp
        {
            get { return _fromIp; }
            set { _fromIp = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _toName = "";
        [Column("to_name")]
        public string ToName
        {
            get { return _toName; }
            set { _toName = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _toEmail = "";
        [Column("to_email")]
        public string ToEmail
        {
            get { return _toEmail; }
            set { _toEmail = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _toNote = "";
        [Column("to_note")]
        public string ToNote
        {
            get { return _toNote; }
            set { _toNote = string.IsNullOrEmpty(value) ? "" : value; }
        }

        int _templateId = 0;
        [Column("stft_id")]
        public int TemplateID
        {
            get { return _templateId; }
            set { _templateId = value == null ? 0 : value; }
        }

        int _zoneId = 0;
        [Column("zone_id")]
        public int ZoneId
        {
            get { return _zoneId; }
            set { _zoneId = value == null ? 0 : value; }
        }

        int _articleId = 0;
        [Column("article_id")]
        public int ArticleId
        {
            get { return _articleId; }
            set { _articleId = value == null ? 0 : value; }
        }

        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

    }
    #endregion

    #region CustomForm
    [Table("cms_custom_form")]
    public class CustomForm
    {
        [Column("id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        string _senderName = "";
        [Column("sender_name")]
        public string SenderName
        {
            get { return _senderName; }
            set { _senderName = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _senderSurname = "";
        [Column("sender_surname")]
        public string SenderSurname
        {
            get { return _senderSurname; }
            set { _senderSurname = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _senderMail = "";
        [Column("sender_mail")]
        public string SenderMail
        {
            get { return _senderMail; }
            set { _senderMail = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _senderCompany = "";
        [Column("sender_company")]
        public string SenderCompany
        {
            get { return _senderCompany; }
            set { _senderCompany = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _senderPhone = "";
        [Column("sender_phone")]
        public string SenderPhone
        {
            get { return _senderPhone; }
            set { _senderPhone = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _infoType = "";
        [Column("info_type")]
        public string InfoType
        {
            get { return _infoType; }
            set { _infoType = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _subject = "";
        [Column("subject")]
        public string Subject
        {
            get { return _subject; }
            set { _subject = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _subSubject = "";
        [Column("sub_subject")]
        public string SubSubject
        {
            get { return _subSubject; }
            set { _subSubject = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _opinion = "";
        [Column("opinion")]
        public string Opinion
        {
            get { return _opinion; }
            set { _opinion = string.IsNullOrEmpty(value) ? "" : value; }
        }


        string _toName = "";
        [Column("to_name")]
        public string ToName
        {
            get { return _toName; }
            set { _toName = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _toSurname = "";
        [Column("to_surname")]
        public string ToSurname
        {
            get { return _toSurname; }
            set { _toSurname = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _toMail = "";
        [Column("to_mail")]
        public string ToMail
        {
            get { return _toMail; }
            set { _toMail = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _ip = "";
        [Column("ip")]
        public string Ip
        {
            get { return _ip; }
            set { _ip = string.IsNullOrEmpty(value) ? "" : value; }
        }


        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }


        string _job = "";
        [Column("job")]
        public string Job
        {
            get { return _job; }
            set { _job = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _title = "";
        [Column("title")]
        public string Title
        {
            get { return _title; }
            set { _title = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _department = "";
        [Column("department")]
        public string Department
        {
            get { return _department; }
            set { _department = string.IsNullOrEmpty(value) ? "" : value; }
        }

    }
    #endregion

    #region InstantMessaging
    [Table("cms_instant_messaging")]
    public class InstantMessaging
    {
        [Column("ims_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Column("ims_from")]
        public Guid From { get; set; }

        [Column("ims_to")]
        public Guid To { get; set; }

        [Column("ims_subject")]
        public string Subject { get; set; }

        [Column("ims_message")]
        public string Message { get; set; }

        [Column("ims_type")]
        public string Type { get; set; }

        [Column("related_id")]
        public long RelatedId { get; set; }

        [Column("related_name")]
        public string RelatedName { get; set; }


        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        [Column("readed")]
        public DateTime? ReadDate { get; set; }

        [Column("processed")]
        public DateTime? ProcessDate { get; set; }

        [Column("deleted")]
        public DateTime? DeleteDate { get; set; }

        [Column("due")]
        public DateTime? Due { get; set; }

    }
    #endregion

    #region CustomContent
    [Table("cms_custom_content")]
    public class CustomContent
    {
        [Column("cc_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        string _name = "";
        [Column("cc_name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _html = "";
        [Column("cc_html")]
        public string Html
        {
            get { return _html; }
            set { _html = string.IsNullOrEmpty(value) ? "" : value; }
        }

        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        [Column("created_by")]
        public Guid CreatedBy { get; set; }

        DateTime _updated = DateTime.Now;
        [Column("updated")]
        public DateTime UpdateDate
        {
            get { return _updated; }
            set { _updated = value == null ? DateTime.Now : value; }
        }

        [Column("updated_by")]
        public Guid UpdatedBy { get; set; }

        int _groupId = 0;
        [Column("group_id")]
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value == null ? 0 : value; }
        }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

    }
    #endregion

    #region Classfication
    [Table("cms_classifications")]
    public class Classification
    {
        [Column("classification_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("classification_name")]
        public string Name { get; set; }

        DateTime _created = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _created; }
            set { _created = value == null ? DateTime.Now : value; }
        }

        [Column("created_by")]
        public Guid CreatedBy { get; set; }

        bool _summaryCB = false;
        [Column("summary_cb")]
        public bool SummaryCB
        {
            get { return _summaryCB; }
            set { _summaryCB = value == null ? false : value; }
        }

        bool _endDateCB = false;
        [Column("enddate_cb")]
        public bool EndDateCB
        {
            get { return _endDateCB; }
            set { _endDateCB = value == null ? false : value; }
        }

        bool _keywordsCB = false;
        [Column("keywords_cb")]
        public bool KeywordsCB
        {
            get { return _keywordsCB; }
            set { _keywordsCB = value == null ? false : value; }
        }

        bool _custom1CB = false;
        [Column("custom1_cb")]
        public bool Custom1CB
        {
            get { return _custom1CB; }
            set { _custom1CB = value == null ? false : value; }
        }

        bool _custom2CB = false;
        [Column("custom2_cb")]
        public bool Custom2CB
        {
            get { return _custom2CB; }
            set { _custom2CB = value == null ? false : value; }
        }

        bool _custom3CB = false;
        [Column("custom3_cb")]
        public bool Custom3CB
        {
            get { return _custom3CB; }
            set { _custom3CB = value == null ? false : value; }
        }


        bool _custom4CB = false;
        [Column("custom4_cb")]
        public bool Custom4CB
        {
            get { return _custom4CB; }
            set { _custom4CB = value == null ? false : value; }
        }


        bool _custom5CB = false;
        [Column("custom5_cb")]
        public bool Custom5CB
        {
            get { return _custom5CB; }
            set { _custom5CB = value == null ? false : value; }
        }


        bool _custom6CB = false;
        [Column("custom6_cb")]
        public bool Custom6CB
        {
            get { return _custom6CB; }
            set { _custom6CB = value == null ? false : value; }
        }


        bool _custom7CB = false;
        [Column("custom7_cb")]
        public bool Custom7CB
        {
            get { return _custom7CB; }
            set { _custom7CB = value == null ? false : value; }
        }


        bool _custom8CB = false;
        [Column("custom8_cb")]
        public bool Custom8CB
        {
            get { return _custom8CB; }
            set { _custom8CB = value == null ? false : value; }
        }


        bool _custom9CB = false;
        [Column("custom9_cb")]
        public bool Custom9CB
        {
            get { return _custom9CB; }
            set { _custom9CB = value == null ? false : value; }
        }


        bool _custom10CB = false;
        [Column("custom10_cb")]
        public bool Custom10CB
        {
            get { return _custom10CB; }
            set { _custom10CB = value == null ? false : value; }
        }

        bool _custom11CB = false;
        [Column("custom11_cb")]
        public bool Custom11CB
        {
            get { return _custom11CB; }
            set { _custom11CB = value == null ? false : value; }
        }


        bool _custom12CB = false;
        [Column("custom12_cb")]
        public bool Custom12CB
        {
            get { return _custom12CB; }
            set { _custom12CB = value == null ? false : value; }
        }


        bool _custom13CB = false;
        [Column("custom13_cb")]
        public bool Custom13CB
        {
            get { return _custom13CB; }
            set { _custom13CB = value == null ? false : value; }
        }

        bool _custom14CB = false;
        [Column("custom14_cb")]
        public bool Custom14CB
        {
            get { return _custom14CB; }
            set { _custom14CB = value == null ? false : value; }
        }

        bool _custom15CB = false;
        [Column("custom15_cb")]
        public bool Custom15CB
        {
            get { return _custom15CB; }
            set { _custom15CB = value == null ? false : value; }
        }


        bool _custom16CB = false;
        [Column("custom16_cb")]
        public bool Custom16CB
        {
            get { return _custom16CB; }
            set { _custom16CB = value == null ? false : value; }
        }


        bool _custom17CB = false;
        [Column("custom17_cb")]
        public bool Custom17CB
        {
            get { return _custom17CB; }
            set { _custom17CB = value == null ? false : value; }
        }

        bool _custom18CB = false;
        [Column("custom18_cb")]
        public bool Custom18CB
        {
            get { return _custom18CB; }
            set { _custom18CB = value == null ? false : value; }
        }


        bool _custom19CB = false;
        [Column("custom19_cb")]
        public bool Custom19CB
        {
            get { return _custom19CB; }
            set { _custom19CB = value == null ? false : value; }
        }


        bool _custom20CB = false;
        [Column("custom20_cb")]
        public bool Custom20CB
        {
            get { return _custom20CB; }
            set { _custom20CB = value == null ? false : value; }
        }

        bool _flag1CB = false;
        [Column("flag1_cb")]
        public bool Flag1CB
        {
            get { return _flag1CB; }
            set { _flag1CB = value == null ? false : value; }
        }

        bool _flag2CB = false;
        [Column("flag2_cb")]
        public bool Flag2CB
        {
            get { return _flag2CB; }
            set { _flag2CB = value == null ? false : value; }
        }

        bool _flag3CB = false;
        [Column("flag3_cb")]
        public bool Flag3CB
        {
            get { return _flag3CB; }
            set { _flag3CB = value == null ? false : value; }
        }

        bool _flag4CB = false;
        [Column("flag4_cb")]
        public bool Flag4CB
        {
            get { return _flag4CB; }
            set { _flag4CB = value == null ? false : value; }
        }

        bool _flag5CB = false;
        [Column("flag5_cb")]
        public bool Flag5CB
        {
            get { return _flag5CB; }
            set { _flag5CB = value == null ? false : value; }
        }


        bool _date1CB = false;
        [Column("date1_cb")]
        public bool Date1CB
        {
            get { return _date1CB; }
            set { _date1CB = value == null ? false : value; }
        }

        bool _date2CB = false;
        [Column("date2_cb")]
        public bool Date2CB
        {
            get { return _date2CB; }
            set { _date2CB = value == null ? false : value; }
        }

        bool _date3CB = false;
        [Column("date3_cb")]
        public bool Date3CB
        {
            get { return _date3CB; }
            set { _date3CB = value == null ? false : value; }
        }

        bool _date4CB = false;
        [Column("date4_cb")]
        public bool Date4CB
        {
            get { return _date4CB; }
            set { _date4CB = value == null ? false : value; }
        }

        bool _date5CB = false;
        [Column("date5_cb")]
        public bool Date5CB
        {
            get { return _date5CB; }
            set { _date5CB = value == null ? false : value; }
        }

        string _custom1Text = "";
        [Column("custom1_text")]
        public string Custom1Text
        {
            get { return _custom1Text; }
            set { _custom1Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom2Text = "";
        [Column("custom2_text")]
        public string Custom2Text
        {
            get { return _custom2Text; }
            set { _custom2Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom3Text = "";
        [Column("custom3_text")]
        public string Custom3Text
        {
            get { return _custom3Text; }
            set { _custom3Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom4Text = "";
        [Column("custom4_text")]
        public string Custom4Text
        {
            get { return _custom4Text; }
            set { _custom4Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom5Text = "";
        [Column("custom5_text")]
        public string Custom5Text
        {
            get { return _custom5Text; }
            set { _custom5Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom6Text = "";
        [Column("custom6_text")]
        public string Custom6Text
        {
            get { return _custom6Text; }
            set { _custom6Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom7Text = "";
        [Column("custom7_text")]
        public string Custom7Text
        {
            get { return _custom7Text; }
            set { _custom7Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom8Text = "";
        [Column("custom8_text")]
        public string Custom8Text
        {
            get { return _custom8Text; }
            set { _custom8Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom9Text = "";
        [Column("custom9_text")]
        public string Custom9Text
        {
            get { return _custom9Text; }
            set { _custom9Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom10Text = "";
        [Column("custom10_text")]
        public string Custom10Text
        {
            get { return _custom10Text; }
            set { _custom10Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom11Text = "";
        [Column("custom11_text")]
        public string Custom11Text
        {
            get { return _custom11Text; }
            set { _custom11Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom12Text = "";
        [Column("custom12_text")]
        public string Custom12Text
        {
            get { return _custom12Text; }
            set { _custom12Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom13Text = "";
        [Column("custom13_text")]
        public string Custom13Text
        {
            get { return _custom13Text; }
            set { _custom13Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom14Text = "";
        [Column("custom14_text")]
        public string Custom14Text
        {
            get { return _custom14Text; }
            set { _custom14Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom15Text = "";
        [Column("custom15_text")]
        public string Custom15Text
        {
            get { return _custom15Text; }
            set { _custom15Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom16Text = "";
        [Column("custom16_text")]
        public string Custom16Text
        {
            get { return _custom16Text; }
            set { _custom16Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom17Text = "";
        [Column("custom17_text")]
        public string Custom17Text
        {
            get { return _custom17Text; }
            set { _custom17Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom18Text = "";
        [Column("custom18_text")]
        public string Custom18Text
        {
            get { return _custom18Text; }
            set { _custom18Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom19Text = "";
        [Column("custom19_text")]
        public string Custom19Text
        {
            get { return _custom19Text; }
            set { _custom19Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom20Text = "";
        [Column("custom20_text")]
        public string Custom20Text
        {
            get { return _custom20Text; }
            set { _custom20Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _flag1Text = "";
        [Column("flag1_text")]
        public string Flag1Text
        {
            get { return _flag1Text; }
            set { _flag1Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _flag2Text = "";
        [Column("flag2_text")]
        public string Flag2Text
        {
            get { return _flag2Text; }
            set { _flag2Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _flag3Text = "";
        [Column("flag3_text")]
        public string Flag3Text
        {
            get { return _flag3Text; }
            set { _flag3Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _flag4Text = "";
        [Column("flag4_text")]
        public string Flag4Text
        {
            get { return _flag4Text; }
            set { _flag4Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _flag5Text = "";
        [Column("flag5_text")]
        public string Flag5Text
        {
            get { return _flag5Text; }
            set { _flag5Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _date1Text = "";
        [Column("date1_text")]
        public string Date1Text
        {
            get { return _date1Text; }
            set { _date1Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _date2Text = "";
        [Column("date2_text")]
        public string Date2Text
        {
            get { return _date2Text; }
            set { _date2Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _date3Text = "";
        [Column("date3_text")]
        public string Date3Text
        {
            get { return _date3Text; }
            set { _date3Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _date4Text = "";
        [Column("date4_text")]
        public string Date4Text
        {
            get { return _date4Text; }
            set { _date4Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _date5Text = "";
        [Column("date5_text")]
        public string Date5Text
        {
            get { return _date5Text; }
            set { _date5Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _custom1Type = "t";
        [Column("custom1_Type")]
        public string Custom1Type
        {
            get { return _custom1Type; }
            set { _custom1Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom2Type = "t";
        [Column("custom2_Type")]
        public string Custom2Type
        {
            get { return _custom2Type; }
            set { _custom2Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom3Type = "t";
        [Column("custom3_Type")]
        public string Custom3Type
        {
            get { return _custom3Type; }
            set { _custom3Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom4Type = "t";
        [Column("custom4_Type")]
        public string Custom4Type
        {
            get { return _custom4Type; }
            set { _custom4Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom5Type = "t";
        [Column("custom5_Type")]
        public string Custom5Type
        {
            get { return _custom5Type; }
            set { _custom5Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom6Type = "t";
        [Column("custom6_Type")]
        public string Custom6Type
        {
            get { return _custom6Type; }
            set { _custom6Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom7Type = "t";
        [Column("custom7_Type")]
        public string Custom7Type
        {
            get { return _custom7Type; }
            set { _custom7Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom8Type = "t";
        [Column("custom8_Type")]
        public string Custom8Type
        {
            get { return _custom8Type; }
            set { _custom8Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom9Type = "t";
        [Column("custom9_Type")]
        public string Custom9Type
        {
            get { return _custom9Type; }
            set { _custom9Type = string.IsNullOrEmpty(value) ? "t" : value; }
        }

        string _custom10Type = "t";
        [Column("custom10_Type")]
        public string Custom10Type
        {
            get { return _custom10Type; }
            set { _custom10Type = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _summaryText = "";
        [Column("summary_text")]
        public string SummaryText
        {
            get { return _summaryText; }
            set { _summaryText = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _endDateText = "";
        [Column("enddate_text")]
        public string EndDateText
        {
            get { return _endDateText; }
            set { _endDateText = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _keywordsText = "";
        [Column("keywords_text")]
        public string KeywordsText
        {
            get { return _keywordsText; }
            set { _keywordsText = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article1Text = "";
        [Column("article1_text")]
        public string Article1Text
        {
            get { return _article1Text; }
            set { _article1Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article2Text = "";
        [Column("article2_text")]
        public string Article2Text
        {
            get { return _article2Text; }
            set { _article2Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article3Text = "";
        [Column("article3_text")]
        public string Article3Text
        {
            get { return _article3Text; }
            set { _article3Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article4Text = "";
        [Column("article4_text")]
        public string Article4Text
        {
            get { return _article4Text; }
            set { _article4Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _article5Text = "";
        [Column("article5_text")]
        public string Article5Text
        {
            get { return _article5Text; }
            set { _article5Text = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _article1CB = false;
        [Column("article1_cb")]
        public bool Article1CB
        {
            get { return _article1CB; }
            set { _article1CB = value == null ? false : value; }
        }

        bool _article2CB = false;
        [Column("article2_cb")]
        public bool Article2CB
        {
            get { return _article2CB; }
            set { _article2CB = value == null ? false : value; }
        }

        bool _article3CB = false;
        [Column("article3_cb")]
        public bool Article3CB
        {
            get { return _article3CB; }
            set { _article3CB = value == null ? false : value; }
        }

        bool _article4CB = false;
        [Column("article4_cb")]
        public bool Article4CB
        {
            get { return _article4CB; }
            set { _article4CB = value == null ? false : value; }
        }

        bool _article5CB = false;
        [Column("article5_cb")]
        public bool Article5CB
        {
            get { return _article5CB; }
            set { _article5CB = value == null ? false : value; }
        }

        byte _custom1SubColumn = 0;
        [Column("custom1_subcolumn")]
        public byte Custom1SubColumn
        {
            get { return _custom1SubColumn; }
            set { _custom1SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom2SubColumn = 0;
        [Column("custom2_subcolumn")]
        public byte Custom2SubColumn
        {
            get { return _custom2SubColumn; }
            set { _custom2SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom3SubColumn = 0;
        [Column("custom3_subcolumn")]
        public byte Custom3SubColumn
        {
            get { return _custom3SubColumn; }
            set { _custom3SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom4SubColumn = 0;
        [Column("custom4_subcolumn")]
        public byte Custom4SubColumn
        {
            get { return _custom4SubColumn; }
            set { _custom4SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom5SubColumn = 0;
        [Column("custom5_subcolumn")]
        public byte Custom5SubColumn
        {
            get { return _custom5SubColumn; }
            set { _custom5SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom6SubColumn = 0;
        [Column("custom6_subcolumn")]
        public byte Custom6SubColumn
        {
            get { return _custom6SubColumn; }
            set { _custom6SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom7SubColumn = 0;
        [Column("custom7_subcolumn")]
        public byte Custom7SubColumn
        {
            get { return _custom7SubColumn; }
            set { _custom7SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom8SubColumn = 0;
        [Column("custom8_subcolumn")]
        public byte Custom8SubColumn
        {
            get { return _custom8SubColumn; }
            set { _custom8SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom9SubColumn = 0;
        [Column("custom9_subcolumn")]
        public byte Custom9SubColumn
        {
            get { return _custom9SubColumn; }
            set { _custom9SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        byte _custom10SubColumn = 0;
        [Column("custom10_subcolumn")]
        public byte Custom10SubColumn
        {
            get { return _custom10SubColumn; }
            set { _custom10SubColumn = value == null ? Convert.ToByte(0) : value; }
        }

        bool _fileRequiredCB = false;
        [Column("file_required_cb")]
        public bool FileRequiredCB
        {
            get { return _fileRequiredCB; }
            set { _fileRequiredCB = value == null ? false : value; }
        }

        bool _fileTitleRequiredCB = false;
        [Column("file_title_required_cb")]
        public bool FileTitleRequiredCB
        {
            get { return _fileTitleRequiredCB; }
            set { _fileTitleRequiredCB = value == null ? false : value; }
        }

        bool _fileDescriptionRequiredCB = false;
        [Column("file_description_required_cb")]
        public bool FileDescriptionRequiredCB
        {
            get { return _fileDescriptionRequiredCB; }
            set { _fileDescriptionRequiredCB = value == null ? false : value; }
        }

        string _requiredFileTypes = "";
        [Column("required_file_types")]
        public string RequiredFileTypes
        {
            get { return _requiredFileTypes; }
            set { _requiredFileTypes = string.IsNullOrEmpty(value) ? "" : value; }
        }

        int _groupId = 0;
        [Column("group_id")]
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value == null ? 0 : value; }
        }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

    }
    #endregion

    #region URLStructure

    [Table("cms_url_structure")]
    public class URLStructure
    {
        [Column("ID")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("DomainId")]
        public int DomainID { get; set; }

        string _name = "";
        [Column("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("StructureTypeId")]
        public int StructureTypeId { get; set; }

        string _structure = "";
        [Column("Structure")]
        public string Structure
        {
            get { return _structure; }
            set { _structure = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _prefix = "";
        [Column("Prefix")]
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = string.IsNullOrEmpty(value) ? "" : value; }
        }

        Boolean _isProtect = false;
        [Column("IsProtect")]
        public Boolean IsProtect
        {
            get { return _isProtect; }
            set { _isProtect = value == null ? false : value; }
        }

        DateTime _createDate = DateTime.Now;
        [Column("CreateDate")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        DateTime _updateDate = DateTime.Now;
        [Column("UpdateDate")]
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value == null ? DateTime.Now : value; }
        }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Column("UpdatedBy")]
        public Guid UpdatedBy { get; set; }
    }

    #endregion

    #region CustomValues
    [Table("cms_custom_values")]
    public class CustomValues
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("GroupID")]
        public Nullable<int> GroupID { get; set; }
        [Column("GroupParentID")]
        public Nullable<int> GroupParentID { get; set; }
        [Column("intCustom1")]
        public Nullable<int> intCustom1 { get; set; }
        [Column("intCustom2")]
        public Nullable<int> intCustom2 { get; set; }
        [Column("intCustom3")]
        public Nullable<int> intCustom3 { get; set; }
        [Column("intCustom4")]
        public Nullable<int> intCustom4 { get; set; }
        [Column("intCustom5")]
        public Nullable<int> intCustom5 { get; set; }
        [Column("intCustom6")]
        public Nullable<int> intCustom6 { get; set; }
        [Column("intCustom7")]
        public Nullable<int> intCustom7 { get; set; }
        [Column("intCustom8")]
        public Nullable<int> intCustom8 { get; set; }
        [Column("intCustom9")]
        public Nullable<int> intCustom9 { get; set; }
        [Column("intCustom10")]
        public Nullable<int> intCustom10 { get; set; }
        [Column("intCustom11")]
        public Nullable<int> intCustom11 { get; set; }
        [Column("intCustom12")]
        public Nullable<int> intCustom12 { get; set; }
        [Column("intCustom13")]
        public Nullable<int> intCustom13 { get; set; }
        [Column("intCustom14")]
        public Nullable<int> intCustom14 { get; set; }
        [Column("intCustom15")]
        public Nullable<int> intCustom15 { get; set; }
        [Column("longCustom1")]
        public Nullable<long> longCustom1 { get; set; }
        [Column("longCustom2")]
        public Nullable<long> longCustom2 { get; set; }
        [Column("longCustom3")]
        public Nullable<long> longCustom3 { get; set; }
        [Column("longCustom4")]
        public Nullable<long> longCustom4 { get; set; }
        [Column("longCustom5")]
        public Nullable<long> longCustom5 { get; set; }
        [Column("longCustom6")]
        public Nullable<long> longCustom6 { get; set; }
        [Column("longCustom7")]
        public Nullable<long> longCustom7 { get; set; }
        [Column("longCustom8")]
        public Nullable<long> longCustom8 { get; set; }
        [Column("longCustom9")]
        public Nullable<long> longCustom9 { get; set; }
        [Column("longCustom10")]
        public Nullable<long> longCustom10 { get; set; }
        [Column("strCustom1")]
        [DefaultValue("")]
        public string strCustom1 { get; set; }
        [Column("strCustom2")]
        [DefaultValue("")]
        public string strCustom2 { get; set; }
        [Column("strCustom3")]
        [DefaultValue("")]
        public string strCustom3 { get; set; }
        [Column("strCustom4")]
        [DefaultValue("")]
        public string strCustom4 { get; set; }
        [Column("strCustom5")]
        [DefaultValue("")]
        public string strCustom5 { get; set; }
        [Column("strCustom6")]
        [DefaultValue("")]
        public string strCustom6 { get; set; }
        [Column("strCustom7")]
        [DefaultValue("")]
        public string strCustom7 { get; set; }
        [Column("strCustom8")]
        [DefaultValue("")]
        public string strCustom8 { get; set; }
        [Column("strCustom9")]
        [DefaultValue("")]
        public string strCustom9 { get; set; }
        [Column("strCustom10")]
        [DefaultValue("")]
        public string strCustom10 { get; set; }
        [Column("strCustom11")]
        [DefaultValue("")]
        public string strCustom11 { get; set; }
        [Column("strCustom12")]
        [DefaultValue("")]
        public string strCustom12 { get; set; }
        [Column("strCustom13")]
        [DefaultValue("")]
        public string strCustom13 { get; set; }
        [Column("strCustom14")]
        [DefaultValue("")]
        public string strCustom14 { get; set; }
        [Column("strCustom15")]
        [DefaultValue("")]
        public string strCustom15 { get; set; }
        [Column("strCustom16")]
        [DefaultValue("")]
        public string strCustom16 { get; set; }
        [Column("strCustom17")]
        [DefaultValue("")]
        public string strCustom17 { get; set; }
        [Column("strCustom18")]
        [DefaultValue("")]
        public string strCustom18 { get; set; }
        [Column("strCustom19")]
        [DefaultValue("")]
        public string strCustom19 { get; set; }
        [Column("strCustom20")]
        [DefaultValue("")]
        public string strCustom20 { get; set; }
        [Column("strCustom21")]
        [DefaultValue("")]
        public string strCustom21 { get; set; }
        [Column("strCustom22")]
        [DefaultValue("")]
        public string strCustom22 { get; set; }
        [Column("strCustom23")]
        [DefaultValue("")]
        public string strCustom23 { get; set; }
        [Column("strCustom24")]
        [DefaultValue("")]
        public string strCustom24 { get; set; }
        [Column("strCustom25")]
        [DefaultValue("")]
        public string strCustom25 { get; set; }
        [Column("strCustom26")]
        [DefaultValue("")]
        public string strCustom26 { get; set; }
        [Column("strCustom27")]
        [DefaultValue("")]
        public string strCustom27 { get; set; }
        [Column("strCustom28")]
        [DefaultValue("")]
        public string strCustom28 { get; set; }
        [Column("strCustom29")]
        [DefaultValue("")]
        public string strCustom29 { get; set; }
        [Column("strCustom30")]
        [DefaultValue("")]
        public string strCustom30 { get; set; }
        [Column("dtCustom1")]
        public Nullable<System.DateTime> dtCustom1 { get; set; }
        [Column("dtCustom2")]
        public Nullable<System.DateTime> dtCustom2 { get; set; }
        [Column("dtCustom3")]
        public Nullable<System.DateTime> dtCustom3 { get; set; }
        [Column("dtCustom4")]
        public Nullable<System.DateTime> dtCustom4 { get; set; }
        [Column("dtCustom5")]
        public Nullable<System.DateTime> dtCustom5 { get; set; }
        [Column("dtCustom6")]
        public Nullable<System.DateTime> dtCustom6 { get; set; }
        [Column("dtCustom7")]
        public Nullable<System.DateTime> dtCustom7 { get; set; }
        [Column("dtCustom8")]
        public Nullable<System.DateTime> dtCustom8 { get; set; }
        [Column("dtCustom9")]
        public Nullable<System.DateTime> dtCustom9 { get; set; }
        [Column("dtCustom10")]
        public Nullable<System.DateTime> dtCustom10 { get; set; }
        [Column("dtCustom11")]
        public Nullable<System.DateTime> dtCustom11 { get; set; }
        [Column("dtCustom12")]
        public Nullable<System.DateTime> dtCustom12 { get; set; }
        [Column("dtCustom13")]
        public Nullable<System.DateTime> dtCustom13 { get; set; }
        [Column("dtCustom14")]
        public Nullable<System.DateTime> dtCustom14 { get; set; }
        [Column("dtCustom15")]
        public Nullable<System.DateTime> dtCustom15 { get; set; }
        [Column("bCustom1")]
        public Nullable<bool> bCustom1 { get; set; }
        [Column("bCustom2")]
        public Nullable<bool> bCustom2 { get; set; }
        [Column("bCustom3")]
        public Nullable<bool> bCustom3 { get; set; }
        [Column("bCustom4")]
        public Nullable<bool> bCustom4 { get; set; }
        [Column("bCustom5")]
        public Nullable<bool> bCustom5 { get; set; }
        [Column("bCustom6")]
        public Nullable<bool> bCustom6 { get; set; }
        [Column("bCustom7")]
        public Nullable<bool> bCustom7 { get; set; }
        [Column("bCustom8")]
        public Nullable<bool> bCustom8 { get; set; }
        [Column("bCustom9")]
        public Nullable<bool> bCustom9 { get; set; }
        [Column("bCustom10")]
        public Nullable<bool> bCustom10 { get; set; }
        [Column("dCustom1")]
        public Nullable<decimal> dCustom1 { get; set; }
        [Column("dCustom2")]
        public Nullable<decimal> dCustom2 { get; set; }
        [Column("dCustom3")]
        public Nullable<decimal> dCustom3 { get; set; }
        [Column("dCustom4")]
        public Nullable<decimal> dCustom4 { get; set; }
        [Column("dCustom5")]
        public Nullable<decimal> dCustom5 { get; set; }
        [Column("dCustom6")]
        public Nullable<decimal> dCustom6 { get; set; }
        [Column("dCustom7")]
        public Nullable<decimal> dCustom7 { get; set; }
        [Column("dCustom8")]
        public Nullable<decimal> dCustom8 { get; set; }
        [Column("dCustom9")]
        public Nullable<decimal> dCustom9 { get; set; }
        [Column("dCustom10")]
        public Nullable<decimal> dCustom10 { get; set; }
        [Column("status")]
        public Nullable<byte> status { get; set; }
    }
    #endregion

    //#region Role
    //[Table("aspnet_Roles")]
    //public class Role
    //{
    //    [Column("RoleId")]
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public Guid ID { get; set; }

    //    [Column("ApplicationId")]
    //    public Guid ApplicationId { get; set; }

    //    [Column("RoleName")]
    //    public string Name { get; set; }

    //    [Column("LoweredRoleName")]
    //    public string LoweredName { get; set; }

    //    [Column("Description")]
    //    public string Description { get; set; }
    //}
    //#endregion

    //#region User In Role
    //[Table("aspnet_UsersInRoles")]
    //public class UserInRole
    //{
    //    [Column("RoleId")]
    //    public Guid RoleId { get; set; }

    //    [Column("UserId")]
    //    public Guid UserId { get; set; }
    //}
    //#endregion

    //#region Access Rules
    //[Table("cms_AccessRules")]
    //public class AccessRule
    //{
    //    [Column("RuleId")]
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int ID { get; set; }

    //    [Column("RuleName")]
    //    public string Name { get; set; }

    //    string _contentId = "0";
    //    [Column("ContentId")]
    //    public string ContentId
    //    {
    //        get { return _contentId; }
    //        set { _contentId = string.IsNullOrEmpty(value) ? "0" : value; }
    //    }

    //    [Column("ContentType")]
    //    public string ContentType { get; set; }

    //    [Column("Roles")]
    //    public string Roles { get; set; }

    //    [Column("Users")]
    //    public string Users { get; set; }

    //    [Column("Permissions")]
    //    public string Permissions { get; set; }

    //    DateTime _CreateDate = DateTime.Now;
    //    [Column("Created")]
    //    public DateTime CreateDate
    //    {
    //        get { return _CreateDate; }
    //        set { _CreateDate = value == null ? DateTime.Now : value; }
    //    }

    //    [Column("CreatedBy")]
    //    public string CreatedBy { get; set; }


    //    [Column("Updated")]
    //    public DateTime UpdateDate { get; set; }

    //    [Column("UpdatedBy")]
    //    public string UpdatedBy { get; set; }



    //}
    //#endregion


    //#region AspNetProfile
    //[Table("aspnet_Profile")]
    //public class AspNetProfile
    //{
    //    [Column("UserId")]
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public Guid UserID { get; set; }

    //    [Column("PropertyNames")]
    //    public string PropertyName { get; set; }

    //    [Column("PropertyValuesString")]
    //    public string PropertyValueString { get; set; }

    //    [Column("LastUpdatedDate")]
    //    public DateTime LastUpdateDate { get; set; }

    //    [Column("LastUpdatedDate")]
    //    public Boolean IsForgotPassword { get; set; }

    //    [Column("ForgotPasswordValue")]
    //    public string ForgotPasswordValue { get; set; }

    //}
    //#endregion

    #region ClassificationComboValues
    [Table("cms_classification_combo_values")]
    public class ClassificationComboValue
    {
        [Column("id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Column("classification_id")]
        public int ClassificationId { get; set; }

        [Column("column_no")]
        public Byte ColumnNo { get; set; }

        string _comboSupid = "";
        [Column("combo_supid")]
        public string ComboSupid
        {
            get { return _comboSupid; }
            set { _comboSupid = string.IsNullOrEmpty(value) ? "" : value; }
        }

        //string _comboLabel = "";
        [Column("combo_label")]
        public string ComboLabel { get; set; }
        //{
        //    get { return _comboLabel; }
        //    set { _comboLabel = string.IsNullOrEmpty(value) ? "" : value; }
        //}

        //string _comboValue = "";
        [Column("combo_value")]
        public string ComboValue { get; set; }
        //{
        //    get { return _comboValue; }
        //    set { _comboValue = string.IsNullOrEmpty(value) ? "" : value; }
        //}
        //

        [Column("combo_order")]
        public int ComboOrder { get; set; }

        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        [Column("created_by")]
        public Guid CreatedBy { get; set; }

    }
    #endregion

    #region Redirects
    [Table("cms_redirects")]
    public class URLRedirect
    {
        [Column("redirect_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("redirect_alias")]
        public string Alias { get; set; }

        [Column("article_id")]
        public int ArticleID { get; set; }

        [Column("zone_id")]
        public int ZoneID { get; set; }

        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        [Column("publisher_id")]
        public Guid PublisherID { get; set; }

        DateTime _updateDate = DateTime.Now;
        [Column("updated")]
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value == null ? DateTime.Now : value; }
        }

        [Column("updated_by")]
        public Guid UpdatedBy { get; set; }

        int _groupID = 0;
        [Column("group_id")]
        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value == null ? 0 : value; }
        }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _permanentRedirection = false;
        [Column("permanent_redirection")]
        public Boolean PermanentRedirection
        {
            get { return _permanentRedirection; }
            set { _permanentRedirection = value == null ? false : value; }
        }
    }

    #endregion

    #region Splashes
    [Table("cms_splashes")]
    public class Splash
    {
        [Column("ID")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        string _name = "";
        [Column("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        //string _customSplashID = "";
        //[Column("CustomSplashID")]
        //public string CustomSplashID
        //{
        //    get { return _customSplashID; }
        //    set { _customSplashID = string.IsNullOrEmpty(value) ? "" : value; }
        //}

        [Column("ZoneID")]
        public int ZoneID { get; set; }

        [Column("ArticleID")]
        public int ArticleID { get; set; }

        string _width = "720";
        [Column("Width")]
        public string Width
        {
            get { return _width; }
            set { _width = string.IsNullOrEmpty(value) ? "720" : value; }
        }

        string _height = "500";
        [Column("Height")]
        public string Height
        {
            get { return _height; }
            set { _height = string.IsNullOrEmpty(value) ? "500" : value; }
        }

        string _openTime = "0";
        [Column("OpenTime")]
        public string OpenTime
        {
            get { return _openTime; }
            set { _openTime = string.IsNullOrEmpty(value) ? "0" : value; }
        }

        string _closeTime = "0";
        [Column("CloseTime")]
        public string CloseTime
        {
            get { return _closeTime; }
            set { _closeTime = string.IsNullOrEmpty(value) ? "0" : value; }
        }

        bool _isModal = false;
        [Column("IsModal")]
        public bool IsModal
        {
            get { return _isModal; }
            set { _isModal = value == null ? false : value; }
        }

        bool _closeButton = true;
        [Column("CloseButton")]
        public bool CloseButton
        {
            get { return _closeButton; }
            set { _closeButton = value == null ? true : value; }
        }

        bool _cookie = false;
        [Column("Cookie")]
        public bool Cookie
        {
            get { return _cookie; }
            set { _cookie = value == null ? false : value; }
        }

        string _cookieExpire = "1";
        [Column("CookieExpire")]
        public string CookieExpire
        {
            get { return _cookieExpire; }
            set { _cookieExpire = string.IsNullOrEmpty(value) ? "1" : value; }
        }

        string _startDate = "";
        [Column("StartDate")]
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _endDate = "";
        [Column("EndDate")]
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = string.IsNullOrEmpty(value) ? "" : value; }
        }

        //string _afterOpen = "";
        //[Column("AfterOpen")]
        //public string AfterOpen
        //{
        //    get { return _afterOpen; }
        //    set { _afterOpen = string.IsNullOrEmpty(value) ? "" : value; }
        //}

        //string _afterClose = "";
        //[Column("AfterClose")]
        //public string AfterClose
        //{
        //    get { return _afterClose; }
        //    set { _afterClose = string.IsNullOrEmpty(value) ? "" : value; }
        //}

        int _status = 0;
        [Column("Status")]
        public int Status
        {
            get { return _status; }
            set { _status = value == null ? 0 : value; }
        }

        DateTime _createDate = DateTime.Now;
        [Column("CreateDate")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        DateTime _updateDate = DateTime.Now;
        [Column("UpdateDate")]
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value == null ? DateTime.Now : value; }
        }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

    }
    #endregion

    #region Portlets
    [Table("cms_portlets")]
    public class Portlet
    {
        [Column("portlet_id")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        string _name = "";
        [Column("portlet_name")]
        public string Name
        {
            get { return _name; }
            set { _name = string.IsNullOrEmpty(value) ? "" : value; }
        }

        [Column("publisher_id")]
        public Guid PublisherID { get; set; }

        byte _status = 0;
        [Column("portlet_status")]
        public byte Status
        {
            get { return _status; }
            set { _status = value == null ? Convert.ToByte(0) : value; }
        }

        DateTime _createDate = DateTime.Now;
        [Column("created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        DateTime _updateDate = DateTime.Now;
        [Column("updated")]
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value == null ? DateTime.Now : value; }
        }

        [Column("updated_by")]
        public Guid UpdatedBy { get; set; }

        string _html = "";
        [Column("portlet_html")]
        public string HTML
        {
            get { return _html; }
            set { _html = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _css = "";
        [Column("portlet_css")]
        public string CSS
        {
            get { return _css; }
            set { _css = string.IsNullOrEmpty(value) ? "" : value; }
        }

        bool _editorType = false;
        [Column("editor_type")]
        public bool EditorType
        {
            get { return _editorType; }
            set { _editorType = value == null ? false : value; }
        }

        string _header = "";
        [Column("portlet_header")]
        public string Header
        {
            get { return _header; }
            set { _header = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _footer = "";
        [Column("portlet_footer")]
        public string Footer
        {
            get { return _footer; }
            set { _footer = string.IsNullOrEmpty(value) ? "" : value; }
        }

        int _groupId = 0;
        [Column("group_id")]
        public int GroupID
        {
            get { return _groupId; }
            set { _groupId = value == null ? 0 : value; }
        }

        string _structureDescription = "";
        [Column("structure_description")]
        public string StructureDescription
        {
            get { return _structureDescription; }
            set { _structureDescription = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _contentEditorType = "H";
        [Column("content_editor_type")]
        public string ContentEditorType
        {
            get { return _contentEditorType; }
            set { _contentEditorType = string.IsNullOrEmpty(value) ? "H" : value; }
        }

        string _enableShortcut = "Y";
        [Column("enable_shortcut")]
        public string EnableShortcut
        {
            get { return _enableShortcut; }
            set { _enableShortcut = string.IsNullOrEmpty(value) ? "Y" : value; }
        }
    }
    #endregion

    #region Page Redirection
    [Table("cms_page_redirection")]
    public class PageRedirection
    {
        [Column("ID")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        string _redirectFrom = "";
        [Column("RedirectFrom")]
        public string RedirectFrom
        {
            get { return _redirectFrom; }
            set { _redirectFrom = string.IsNullOrEmpty(value) ? "" : value; }
        }

        string _redirectTo = "";
        [Column("RedirectTo")]
        public string RedirectTo
        {
            get { return _redirectTo; }
            set { _redirectTo = string.IsNullOrEmpty(value) ? "" : value; }
        }


        DateTime _createDate = DateTime.Now;
        [Column("Created")]
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }

        DateTime _updateDate = DateTime.Now;
        [Column("Updated")]
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set { _updateDate = value == null ? DateTime.Now : value; }
        }

        [Column("UpdatedBy")]
        public Guid UpdatedBy { get; set; }

        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }

        string _redirectType = "301";
        [Column("RedirectType")]
        public string RedirectType
        {
            get { return _redirectType; }
            set { _redirectType = string.IsNullOrEmpty(value) ? "301" : value; }
        }
    }
    #endregion


    #region Views

    #region vArticleZoneFull
    [Table("vArticlesZonesFull")]
    public class vArticlesZonesFull
    {
        [Column("lang_alias")]
        public string LanguageAlias { get; set; }
        [Column("zone_alias")]
        public string ZoneAlias { get; set; }
        [Column("tag_ids")]
        public string TagIds { get; set; }
        [Column("tag_contents")]
        public string TagContents { get; set; }

        [Column("az_order")]
        [Description("ArticleZone")]
        public int AzOrder { get; set; }
        [Column("zone_type_id")]
        [Description("Zone")]
        public int ZoneTypeID { get; set; }
        [Column("article_id")]
        [Description("Article")]
        [Key]
        public int ArticleID { get; set; }
        [Column("clsf_id")]
        [Description("Article")]
        public int ClassificationID { get; set; }
        [Column("status")]
        [Description("Article")]
        public byte Status { get; set; }
        [Column("created")]
        [Description("Article")]
        public System.DateTime Created { get; set; }
        [Column("updated")]
        [Description("Article")]
        public System.DateTime Updated { get; set; }
        [Column("startdate")]
        [Description("Article")]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Column("enddate")]
        [Description("Article")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Column("publisher_id")]
        [Description("Article")]
        public System.Guid PublisherID { get; set; }
        [Column("clicks")]
        [Description("Article")]
        public int Clicks { get; set; }
        //yeni eklenen alanlar
        string _a_before_head = "";
        [Column("a_before_head")]
        public string ArticleBeforeHead
        {
            get { return _a_before_head; }
            set { _a_before_head = value == null ? "" : value; }
        }

        string _a_before_body = "";
        [Column("a_before_body")]
        public string ArticleBeforeBody
        {
            get { return _a_before_body; }
            set { _a_before_body = value == null ? "" : value; }
        }

        string _z_before_head = "";
        [Column("zone_before_head")]
        public string ZoneBeforeHead
        {
            get { return _z_before_head; }
            set { _z_before_head = value == null ? "" : value; }
        }

        string _z_before_body = "";
        [Column("zone_before_body")]
        public string ZoneBeforeBody
        {
            get { return _z_before_body; }
            set { _z_before_body = value == null ? "" : value; }
        }

        string _zg_before_head = "";
        [Column("zg_before_head")]
        public string ZoneGroupBeforeHead
        {
            get { return _zg_before_head; }
            set { _zg_before_head = value == null ? "" : value; }
        }

        string _zg_before_body = "";
        [Column("zg_before_body")]
        public string ZoneGroupBeforeBody
        {
            get { return _zg_before_body; }
            set { _zg_before_body = value == null ? "" : value; }
        }

        string _zg_alias = "";
        [Column("zg_alias")]
        public string ZoneGroupAlias
        {
            get { return _zg_alias; }
            set { _zg_alias = value; }
        }

        string _site_alias = "";
        [Column("site_alias")]
        public string SiteAlias
        {
            get { return _site_alias; }
            set { _site_alias = value; }
        }

        bool _isAliasProtected = false;
        [Column("is_alias_protected")]
        public bool IsAliasProtected
        {
            get { return _isAliasProtected; }
            set { _isAliasProtected = Convert.ToBoolean(value); }
        }

        bool _isPage = true;
        [Column("is_page")]
        public bool IsPage
        {
            get { return _isPage; }
            set { _isPage = Convert.ToBoolean(value); }
        }

        //yeni eklenen alanlar
        [Column("orderno")]
        [Description("Article")]
        public int OrderNo { get; set; }
        [Column("lang_id")]
        [Description("Article")]
        public string LanguageID { get; set; }
        [Column("navigation_display")]
        [Description("Article")]
        public byte NavigationDisplay { get; set; }
        [Column("navigation_zone_id")]
        [Description("Article")]
        public int NavigationZoneID { get; set; }
        [Column("menu_text")]
        [Description("Article")]
        public string MenuText { get; set; }
        [Column("headline")]
        [Description("Article")]
        public string Headline { get; set; }
        [Column("summary")]
        [Description("Article")]
        public string Summary { get; set; }
        [Column("keywords")]
        [Description("Article")]
        public string Keywords { get; set; }
        [Column("article_type")]
        [Description("Article")]
        public byte ArticleType { get; set; }
        [Column("article_type_detail")]
        [Description("Article")]
        public string ArticleTypeDetail { get; set; }
        [Column("article_1")]
        [Description("Article")]
        public string Article1 { get; set; }
        [Column("article_2")]
        [Description("Article")]
        public string Article2 { get; set; }
        [Column("article_3")]
        [Description("Article")]
        public string Article3 { get; set; }
        [Column("article_4")]
        [Description("Article")]
        public string Article4 { get; set; }
        [Column("article_5")]
        [Description("Article")]
        public string Article5 { get; set; }
        [Column("custom_1")]
        [Description("Article")]
        public string Custom1 { get; set; }
        [Column("custom_2")]
        [Description("Article")]
        public string Custom2 { get; set; }
        [Column("custom_3")]
        [Description("Article")]
        public string Custom3 { get; set; }
        [Column("custom_4")]
        [Description("Article")]
        public string Custom4 { get; set; }
        [Column("custom_5")]
        [Description("Article")]
        public string Custom5 { get; set; }
        [Column("custom_6")]
        [Description("Article")]
        public string Custom6 { get; set; }
        [Column("custom_7")]
        [Description("Article")]
        public string Custom7 { get; set; }
        [Column("custom_8")]
        [Description("Article")]
        public string Custom8 { get; set; }
        [Column("custom_9")]
        [Description("Article")]
        public string Custom9 { get; set; }
        [Column("custom_10")]
        [Description("Article")]
        public string Custom10 { get; set; }
        [Column("custom_11")]
        [Description("Article")]
        public string Custom11 { get; set; }
        [Column("custom_12")]
        [Description("Article")]
        public string Custom12 { get; set; }
        [Column("custom_13")]
        [Description("Article")]
        public string Custom13 { get; set; }
        [Column("custom_14")]
        [Description("Article")]
        public string Custom14 { get; set; }
        [Column("custom_15")]
        [Description("Article")]
        public string Custom15 { get; set; }
        [Column("custom_16")]
        [Description("Article")]
        public string Custom16 { get; set; }
        [Column("custom_17")]
        [Description("Article")]
        public string Custom17 { get; set; }
        [Column("custom_18")]
        [Description("Article")]
        public string Custom18 { get; set; }
        [Column("custom_19")]
        [Description("Article")]
        public string Custom19 { get; set; }
        [Column("custom_20")]
        [Description("Article")]
        public string Custom20 { get; set; }
        [Column("flag_1")]
        [Description("Article")]
        public bool Flag1 { get; set; }
        [Column("flag_2")]
        [Description("Article")]
        public bool Flag2 { get; set; }
        [Column("flag_3")]
        [Description("Article")]
        public bool Flag3 { get; set; }
        [Column("flag_4")]
        [Description("Article")]
        public bool Flag4 { get; set; }
        [Column("flag_5")]
        [Description("Article")]
        public bool Flag5 { get; set; }
        [Column("date_1")]
        [Description("Article")]
        public Nullable<System.DateTime> Date1 { get; set; }
        [Column("date_2")]
        [Description("Article")]
        public Nullable<System.DateTime> Date2 { get; set; }
        [Column("date_3")]
        [Description("Article")]
        public Nullable<System.DateTime> Date3 { get; set; }
        [Column("date_4")]
        [Description("Article")]
        public Nullable<System.DateTime> Date4 { get; set; }
        [Column("date_5")]
        [Description("Article")]
        public Nullable<System.DateTime> Date5 { get; set; }
        [Column("cl_1")]
        [Description("Article")]
        public byte Classification1 { get; set; }
        [Column("cl_2")]
        [Description("Article")]
        public byte Classification2 { get; set; }
        [Column("cl_3")]
        [Description("Article")]
        public byte Classification3 { get; set; }
        [Column("cl_4")]
        [Description("Article")]
        public byte Classification4 { get; set; }
        [Column("cl_5")]
        [Description("Article")]
        public byte Classification5 { get; set; }
        [Column("a_custom_body")]
        [Description("Article")]
        public string ArticleCustomBody { get; set; }
        [Column("zone_id")]
        [Description("Zone")]
        public int ZoneID { get; set; }
        [Column("zone_group_id")]
        [Description("ZoneGroup")]
        public int ZoneGroupID { get; set; }
        [Column("zone_status")]
        [Description("Zone")]
        public string ZoneStatus { get; set; }
        [Column("zone_name")]
        [Description("Zone")]
        public string ZoneName { get; set; }
        [Column("zone_desc")]
        [Description("Zone")]
        public string ZoneDescription { get; set; }
        [Column("zone_css_merge")]
        [Description("Zone")]
        public int ZoneCssMerge { get; set; }
        [Column("zone_css_id")]
        [Description("Zone")]
        public int ZoneCssID { get; set; }
        [Column("zone_css_id_mobile")]
        [Description("Zone")]
        public int ZoneMobileCssID { get; set; }
        [Column("zone_css_id_print")]
        [Description("Zone")]
        public int ZonePrintCssID { get; set; }
        [Column("zone_template_id")]
        [Description("Zone")]
        public int ZoneTemplateID { get; set; }
        [Column("zone_template_id_mobile")]
        [Description("Zone")]
        public int ZoneMobileTemplateID { get; set; }
        [Column("zone_keywords")]
        [Description("Zone")]
        public string ZoneKeywords { get; set; }
        [Column("zone_article_1")]
        [Description("Zone")]
        public string ZoneArticle1 { get; set; }
        [Column("zone_article_2")]
        [Description("Zone")]
        public string ZoneArticle2 { get; set; }
        [Column("zone_article_3")]
        [Description("Zone")]
        public string ZoneArticle3 { get; set; }
        [Column("zone_article_4")]
        [Description("Zone")]
        public string ZoneArticle4 { get; set; }
        [Column("zone_article_5")]
        [Description("Zone")]
        public string ZoneArticle5 { get; set; }
        [Column("append_1")]
        [Description("Zone")]
        public byte ZoneAppend1 { get; set; }
        [Column("append_2")]
        [Description("Zone")]
        public byte ZoneAppend2 { get; set; }
        [Column("append_3")]
        [Description("Zone")]
        public byte ZoneAppend3 { get; set; }
        [Column("append_4")]
        [Description("Zone")]
        public byte ZoneAppend4 { get; set; }
        [Column("append_5")]
        [Description("Zone")]
        public byte ZoneAppend5 { get; set; }
        [Column("zone_publisher_id")]
        [Description("Zone")]
        public System.Guid ZonePublisherID { get; set; }
        [Column("zone_created")]
        [Description("Zone")]
        public System.DateTime ZoneCreated { get; set; }
        [Column("zone_updated")]
        [Description("Zone")]
        public System.DateTime ZoneUpdated { get; set; }
        [Column("zone_custom_body")]
        [Description("Zone")]
        public string ZoneCustomBody { get; set; }
        [Column("zone_group_name")]
        [Description("ZoneGroup")]
        public string ZoneGroupName { get; set; }
        [Column("zone_group_keywords")]
        [Description("ZoneGroup")]
        public string ZoneGroupKeywords { get; set; }
        [Column("site_id")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupSiteId { get; set; }
        [Column("zg_css_merge")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupCssMerge { get; set; }
        [Column("zg_css_id")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupCssID { get; set; }
        [Column("zg_css_id_mobile")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupMobileCssID { get; set; }
        [Column("zg_css_id_print")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupPrintCssID { get; set; }
        [Column("zg_template_id")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupTemplateID { get; set; }
        [Column("zg_template_id_mobile")]
        [Description("ZoneGroup")]
        public Nullable<int> ZoneGroupMobileTemplateID { get; set; }
        [Column("zg_publisher_id")]
        [Description("ZoneGroup")]
        public Nullable<System.Guid> ZoneGroupPublisherID { get; set; }
        [Column("zg_created")]
        [Description("ZoneGroup")]
        public Nullable<System.DateTime> ZoneGroupCreated { get; set; }
        [Column("zg_updated")]
        [Description("ZoneGroup")]
        public Nullable<System.DateTime> ZoneGroupUpdated { get; set; }
        [Column("zg_article_1")]
        [Description("ZoneGroup")]
        public string ZoneGroupArticle1 { get; set; }
        [Column("zg_article_2")]
        [Description("ZoneGroup")]
        public string ZoneGroupArticle2 { get; set; }
        [Column("zg_article_3")]
        [Description("ZoneGroup")]
        public string ZoneGroupArticle3 { get; set; }
        [Column("zg_article_4")]
        [Description("ZoneGroup")]
        public string ZoneGroupArticle4 { get; set; }
        [Column("zg_article_5")]
        [Description("ZoneGroup")]
        public string ZoneGroupArticle5 { get; set; }
        [Column("zg_append_1")]
        [Description("ZoneGroup")]
        public Nullable<byte> ZoneGroupAppend1 { get; set; }
        [Column("zg_append_2")]
        [Description("ZoneGroup")]
        public Nullable<byte> ZoneGroupAppend2 { get; set; }
        [Column("zg_append_3")]
        [Description("ZoneGroup")]
        public Nullable<byte> ZoneGroupAppend3 { get; set; }
        [Column("zg_append_4")]
        [Description("ZoneGroup")]
        public Nullable<byte> ZoneGroupAppend4 { get; set; }
        [Column("zg_append_5")]
        [Description("ZoneGroup")]
        public Nullable<byte> ZoneGroupAppend5 { get; set; }
        [Column("zg_custom_body")]
        [Description("ZoneGroup")]
        public string ZoneGroupCustomBody { get; set; }
        [Column("site_name")]
        [Description("Site")]
        public string SiteName { get; set; }
        [Column("site_css_id")]
        [Description("Site")]
        public Nullable<int> SiteCssId { get; set; }
        [Column("site_css_id_mobile")]
        [Description("Site")]
        public Nullable<int> SiteMobileCssID { get; set; }
        [Column("site_css_id_print")]
        [Description("Site")]
        public Nullable<int> SitePrintCssID { get; set; }
        [Column("site_template_id")]
        [Description("Site")]
        public Nullable<int> SiteTemplateId { get; set; }
        [Column("site_template_id_mobile")]
        [Description("Site")]
        public Nullable<int> SiteMobileTemplateID { get; set; }
        [Column("site_publisher_id")]
        [Description("Site")]
        public Nullable<System.Guid> SitePublisherID { get; set; }
        [Column("site_keywords")]
        [Description("Site")]
        public string SiteKeywords { get; set; }
        [Column("site_header")]
        [Description("Site")]
        public string SiteHeader { get; set; }
        [Column("site_js")]
        [Description("Site")]
        public string SiteJs { get; set; }
        [Column("site_icon")]
        [Description("Site")]
        public string SiteIcon { get; set; }
        [Column("site_created")]
        [Description("Site")]
        public Nullable<System.DateTime> SiteCreated { get; set; }
        [Column("site_updated")]
        [Description("Site")]
        public Nullable<System.DateTime> SiteUpdates { get; set; }
        [Column("s_article_1")]
        [Description("Site")]
        public string SiteArticle1 { get; set; }
        [Column("s_article_2")]
        [Description("Site")]
        public string SiteArticle2 { get; set; }
        [Column("s_article_3")]
        [Description("Site")]
        public string SiteArticle3 { get; set; }
        [Column("s_article_4")]
        [Description("Site")]
        public string SiteArticle4 { get; set; }
        [Column("s_article_5")]
        [Description("Site")]
        public string SiteArticle5 { get; set; }
        [Column("s_custom_body")]
        [Description("Site")]
        public string SiteCustomBody { get; set; }
        [Column("site_analytics")]
        [Description("Site")]
        public string SiteAnalytics { get; set; }
        [Column("zg_analytics")]
        [Description("ZoneGroup")]
        public string ZoneGroupAnalytics { get; set; }
        [Column("zone_analytics")]
        [Description("Zone")]
        public string ZoneAnalytics { get; set; }
        [Column("rating")]
        [Description("Article")]
        public int Rating { get; set; }
        [Column("ratingcount")]
        [Description("Article")]
        public int RatingCount { get; set; }
        [Column("meta_description")]
        [Description("Article")]
        public string MetaDescription { get; set; }
        [Column("zone_meta_description")]
        [Description("Zone")]
        public string ZoneMetaDescription { get; set; }
        [Column("zone_group_meta_description")]
        [Description("ZoneGroup")]
        public string ZoneGroupMetaDescription { get; set; }
        [Column("site_meta_description")]
        [Description("Site")]
        public string SiteMetaDescription { get; set; }
        [Column("zone_group_name_display")]
        [Description("ZoneGroup")]
        public string ZoneGroupNameDisplay { get; set; }
        [Column("zone_name_display")]
        [Description("Zone")]
        public string ZoneNameDisplay { get; set; }
        [Column("az_alias")]
        [Description("ArticleZone")]
        public string ArticleZoneAlias { get; set; }
        [Column("article_omniture_code")]
        [Description("Article")]
        public string ArticleOmnitureCode { get; set; }
        [Column("zone_omniture_code")]
        [Description("Zone")]
        public string ZoneOmnitureCode { get; set; }
        [Column("zone_group_omniture_code")]
        [Description("ZoneGroup")]
        public string ZoneGroupOmnitureCode { get; set; }
        [Column("site_omniture_code")]
        [Description("Site")]
        public string SiteOmnitureCode { get; set; }
        [Column("site_default_article")]
        [Description("Site")]
        public string SiteDefaultArticle { get; set; }
        [Column("zone_group_default_article")]
        [Description("ZoneGroup")]
        public string ZoneGroupDefaultArticle { get; set; }
        [Column("zone_default_article")]
        [Description("Zone")]
        public string ZoneDefaultArticle { get; set; }
        [Column("custom_setting")]
        [Description("Article")]
        public string CustomSetting { get; set; }
        [Column("domain_name")]
        [Description("Domain")]
        public string DomainName { get; set; }
    }

    #endregion

    #region vAspNetMembershipUser

    [Table("vw_aspnet_MembershipUsers")]
    public class vAspNetMembershipUser
    {
        //[Key]
        //public int ID { get; set; }
        [Key]
        [Column("UserId")]
        public System.Guid UserId { get; set; }
        [Column("PasswordFormat")]
        public int PasswordFormat { get; set; }
        [Column("MobilePIN")]
        public string MobilePIN { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("LoweredEmail")]
        public string LoweredEmail { get; set; }
        [Column("PasswordQuestion")]
        public string PasswordQuestion { get; set; }
        [Column("PasswordAnswer")]
        public string PasswordAnswer { get; set; }
        [Column("IsApproved")]
        public bool IsApproved { get; set; }
        [Column("IsLockedOut")]
        public bool IsLockedOut { get; set; }
        [Column("CreateDate")]
        public System.DateTime CreateDate { get; set; }
        [Column("LastLoginDate")]
        public System.DateTime LastLoginDate { get; set; }
        [Column("LastPasswordChangedDate")]
        public System.DateTime LastPasswordChangedDate { get; set; }
        [Column("LastLockoutDate")]
        public System.DateTime LastLockoutDate { get; set; }
        [Column("FailedPasswordAttemptCount")]
        public int FailedPasswordAttemptCount { get; set; }
        [Column("FailedPasswordAttemptWindowStart")]
        public System.DateTime FailedPasswordAttemptWindowStart { get; set; }
        [Column("FailedPasswordAnswerAttemptCount")]
        public int FailedPasswordAnswerAttemptCount { get; set; }
        [Column("FailedPasswordAnswerAttemptWindowStart")]
        public System.DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
        [Column("ApplicationId")]
        public System.Guid ApplicationId { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("MobileAlias")]
        public string MobileAlias { get; set; }
        [Column("IsAnonymous")]
        public bool IsAnonymous { get; set; }
        [Column("LastActivityDate")]
        public System.DateTime LastActivityDate { get; set; }
    }

    #endregion




    public partial class vArticleFilesApprovalDates
    {
        public int article_id { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
    }
    public partial class vArticlesApprovalDates
    {
        public int article_id { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
    }

    public partial class vArticlesRevisionsZonesFull
    {
        public long rev_id { get; set; }
        public int az_order { get; set; }
        public int zone_type_id { get; set; }
        public int article_id { get; set; }
        public int clsf_id { get; set; }
        public byte status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public System.DateTime startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public System.Guid publisher_id { get; set; }
        public string clicks { get; set; }
        public int orderno { get; set; }
        public string lang_id { get; set; }
        public byte navigation_display { get; set; }
        public int navigation_zone_id { get; set; }
        public string menu_text { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }
        public string keywords { get; set; }
        public byte article_type { get; set; }
        public string article_type_detail { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public string custom_1 { get; set; }
        public string custom_2 { get; set; }
        public string custom_3 { get; set; }
        public string custom_4 { get; set; }
        public string custom_5 { get; set; }
        public string custom_6 { get; set; }
        public string custom_7 { get; set; }
        public string custom_8 { get; set; }
        public string custom_9 { get; set; }
        public string custom_10 { get; set; }
        public string custom_11 { get; set; }
        public string custom_12 { get; set; }
        public string custom_13 { get; set; }
        public string custom_14 { get; set; }
        public string custom_15 { get; set; }
        public string custom_16 { get; set; }
        public string custom_17 { get; set; }
        public string custom_18 { get; set; }
        public string custom_19 { get; set; }
        public string custom_20 { get; set; }
        public bool flag_1 { get; set; }
        public bool flag_2 { get; set; }
        public bool flag_3 { get; set; }
        public bool flag_4 { get; set; }
        public bool flag_5 { get; set; }
        public Nullable<System.DateTime> date_1 { get; set; }
        public Nullable<System.DateTime> date_2 { get; set; }
        public Nullable<System.DateTime> date_3 { get; set; }
        public Nullable<System.DateTime> date_4 { get; set; }
        public Nullable<System.DateTime> date_5 { get; set; }
        public byte cl_1 { get; set; }
        public byte cl_2 { get; set; }
        public byte cl_3 { get; set; }
        public byte cl_4 { get; set; }
        public byte cl_5 { get; set; }
        public string a_custom_body { get; set; }
        public int zone_id { get; set; }
        public int zone_group_id { get; set; }
        public string zone_status { get; set; }
        public string zone_name { get; set; }
        public string zone_desc { get; set; }
        public int zone_css_merge { get; set; }
        public int zone_css_id { get; set; }
        public int zone_css_id_mobile { get; set; }
        public int zone_css_id_print { get; set; }
        public int zone_template_id { get; set; }
        public int zone_template_id_mobile { get; set; }
        public string zone_keywords { get; set; }
        public string zone_article_1 { get; set; }
        public string zone_article_2 { get; set; }
        public string zone_article_3 { get; set; }
        public string zone_article_4 { get; set; }
        public string zone_article_5 { get; set; }
        public byte append_1 { get; set; }
        public byte append_2 { get; set; }
        public byte append_3 { get; set; }
        public byte append_4 { get; set; }
        public byte append_5 { get; set; }
        public System.Guid zone_publisher_id { get; set; }
        public System.DateTime zone_created { get; set; }
        public System.DateTime zone_updated { get; set; }
        public string zone_custom_body { get; set; }
        public string zone_group_name { get; set; }
        public string zone_group_keywords { get; set; }
        public Nullable<int> site_id { get; set; }
        public Nullable<int> zg_css_merge { get; set; }
        public Nullable<int> zg_css_id { get; set; }
        public Nullable<int> zg_css_id_mobile { get; set; }
        public Nullable<int> zg_css_id_print { get; set; }
        public Nullable<int> zg_template_id { get; set; }
        public Nullable<int> zg_template_id_mobile { get; set; }
        public Nullable<System.Guid> zg_publisher_id { get; set; }
        public Nullable<System.DateTime> zg_created { get; set; }
        public Nullable<System.DateTime> zg_updated { get; set; }
        public string zg_article_1 { get; set; }
        public string zg_article_2 { get; set; }
        public string zg_article_3 { get; set; }
        public string zg_article_4 { get; set; }
        public string zg_article_5 { get; set; }
        public Nullable<byte> zg_append_1 { get; set; }
        public Nullable<byte> zg_append_2 { get; set; }
        public Nullable<byte> zg_append_3 { get; set; }
        public Nullable<byte> zg_append_4 { get; set; }
        public Nullable<byte> zg_append_5 { get; set; }
        public string zg_custom_body { get; set; }
        public string site_name { get; set; }
        public Nullable<int> site_css_id { get; set; }
        public Nullable<int> site_css_id_mobile { get; set; }
        public Nullable<int> site_css_id_print { get; set; }
        public Nullable<int> site_template_id { get; set; }
        public Nullable<int> site_template_id_mobile { get; set; }
        public Nullable<System.Guid> site_publisher_id { get; set; }
        public string site_keywords { get; set; }
        public string site_header { get; set; }
        public string site_js { get; set; }
        public string site_icon { get; set; }
        public Nullable<System.DateTime> site_created { get; set; }
        public Nullable<System.DateTime> site_updated { get; set; }
        public string s_article_1 { get; set; }
        public string s_article_2 { get; set; }
        public string s_article_3 { get; set; }
        public string s_article_4 { get; set; }
        public string s_article_5 { get; set; }
        public string s_custom_body { get; set; }
        public string site_analytics { get; set; }
        public string zg_analytics { get; set; }
        public string zone_analytics { get; set; }
        public string rating { get; set; }
        public string ratingcount { get; set; }
        public string meta_description { get; set; }
        public string zone_meta_description { get; set; }
        public string zone_group_meta_description { get; set; }
        public string site_meta_description { get; set; }
        public string zone_group_name_display { get; set; }
        public string zone_name_display { get; set; }
        public string az_alias { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string article_omniture_code { get; set; }
        public string zone_omniture_code { get; set; }
        public string zone_group_omniture_code { get; set; }
        public string site_omniture_code { get; set; }
        public string site_default_article { get; set; }
        public string zone_group_default_article { get; set; }
        public string zone_default_article { get; set; }
        public string custom_setting { get; set; }
    }

    public partial class vArticlesZonesNav_1
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public int navigation_zone_id { get; set; }
    }

    public partial class vw_aspnet_Applications
    {
        public string ApplicationName { get; set; }
        public string LoweredApplicationName { get; set; }
        public System.Guid ApplicationId { get; set; }
        public string Description { get; set; }
    }


    public partial class vw_aspnet_Profiles
    {
        public System.Guid UserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<int> DataSize { get; set; }
    }

    public partial class vw_aspnet_Roles
    {
        public System.Guid ApplicationId { get; set; }
        public System.Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
    }

    public partial class vw_aspnet_Sitemap
    {
        public int PageId { get; set; }
        public Nullable<int> PageParentId { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string PageUrl { get; set; }
        public string Roles { get; set; }
    }

    public partial class vw_aspnet_Users
    {
        public System.Guid ApplicationId { get; set; }
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public System.DateTime LastActivityDate { get; set; }
    }

    public partial class vw_aspnet_UsersInRoles
    {
        public System.Guid UserId { get; set; }
        public System.Guid RoleId { get; set; }
    }

    public partial class vw_aspnet_WebPartState_Paths
    {
        public System.Guid ApplicationId { get; set; }
        public System.Guid PathId { get; set; }
        public string Path { get; set; }
        public string LoweredPath { get; set; }
    }

    public partial class vw_aspnet_WebPartState_Shared
    {
        public System.Guid PathId { get; set; }
        public Nullable<int> DataSize { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }

    public partial class vw_aspnet_WebPartState_User
    {
        public Nullable<System.Guid> PathId { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<int> DataSize { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }

    public partial class vw_cms_AccessRules
    {
        [Key]
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Roles { get; set; }
        public string Users { get; set; }
        public string Permissions { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string ContentItemName { get; set; }
        public Nullable<int> AccessLevel { get; set; }
    }

    public partial class vw_cms_AccessRules_Article
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Roles { get; set; }
        public string Users { get; set; }
        public string Permissions { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }

    public partial class vw_cms_AccessRules_Site
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Roles { get; set; }
        public string Users { get; set; }
        public string Permissions { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }

    public partial class vw_cms_AccessRules_Zone
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Roles { get; set; }
        public string Users { get; set; }
        public string Permissions { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }

    public partial class vw_cms_AccessRules_ZoneGroup
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Roles { get; set; }
        public string Users { get; set; }
        public string Permissions { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }

    #endregion

    #endregion
}