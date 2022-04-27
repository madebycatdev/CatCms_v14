using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EuroCMS.Admin.entity;
using EuroCMS.Model;
using EuroCMS.Core;
using System.EnterpriseServices;


namespace EuroCMS.Admin.Models
{


    public class ArticleDbContext : BaseDbContext
    {
        public DbSet<cms_articles> Articles { get; set; }

        public ArticleDbContext()
            : base()
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_articles>()
                  .HasMany(a => a.zones)
                  .WithMany(z => z.articles)
                  .Map(m =>
                  {
                      m.ToTable("cms_article_zones");
                      m.MapLeftKey("article_id");
                      m.MapRightKey("zone_id");
                  });




            base.OnModelCreating(modelBuilder);
        }

        //public bool UpdateTag(Tag tag, string oldName,string userId)
        //{
        //    CmsDbContext dbContext = new CmsDbContext();

        //    var articles = dbContext.Articles.Where(w => w.TagIds.Split(',').ToList().Contains(tag.ID.ToString())).ToList();

        //    foreach (var article in articles)
        //    {
        //        article.TagContents = article.TagContents.Replace(oldName, tag.Text);
        //        dbContext.Entry(article).State = EntityState.Modified;
        //        dbContext.SaveChanges();

        //        var article_revision = dbContext.ArticleRevisions.FirstOrDefault(f => f.ArticleId == article.Id);
        //        if (article_revision != null)
        //        {
        //            article_revision.RevisionStatus = "E";
        //            dbContext.Entry(article_revision).State = EntityState.Modified;
        //            dbContext.SaveChanges();
        //        }

        //        article_revision = new ArticleRevision();
        //        article_revision.Created = DateTime.Now;
        //        article_revision.CreatedBy = new Guid(userId);
        //        article_revision.RevisionDate = DateTime.Now;
        //        article_revision.RevisionStatus = "L";
        //        article_revision.RevisedBy = new Guid(userId);
        //        article_revision.RevisionNote = "Tag Update";
        //        article_revision.RevisionFlag1 = false;
        //        article_revision.RevisionFlag2 = false;
        //        article_revision.RevisionFlag3 = false;
        //        article_revision.RevisionFlag4 = false;
        //        article_revision.RevisionFlag5 = false;
        //        article_revision.Approved = DateTime.Now;
        //        article_revision.ApprovedBy = new Guid(userId);
        //        article_revision.ArticleId = article.Id;
        //        article_revision.ClassificationId = article.ClassificationId;
        //        article_revision.Status = article.Status;
        //        article_revision.Startdate = article.Startdate;
        //        article_revision.Enddate = article.Enddate;
        //        article_revision.Order = article.Order;
        //        article_revision.LangId = article.LangId;
        //        article_revision.NavigationDisplay = article.NavigationDisplay;
        //        article_revision.NavigationZoneId = article.NavigationZoneId;
        //        article_revision.MenuText = article.MenuText;
        //        article_revision.Headline = article.Headline;
        //        article_revision.Summary = article.Summary;
        //        article_revision.Keywords = article.Keywords;
        //        article_revision.ArticleType = article.ArticleType;
        //        article_revision.ArticleTypeDetail = article.ArticleTypeDetail;
        //        article_revision.artic








        //    }

        //    return true;
        //}



        public List<cms_asp_select_articles_by_zone_Result> SelectArticlesByZone(int? ZoneID)
        {
            List<cms_asp_select_articles_by_zone_Result> returnList = new List<cms_asp_select_articles_by_zone_Result>();

            int zoneId = ZoneID == null ? -1 : Convert.ToInt32(ZoneID);

            CmsDbContext dbContext = new CmsDbContext();

            List<vArticlesZonesFull> listVArticleZone = new List<vArticlesZonesFull>();

            if (zoneId == -1)
            {
                listVArticleZone = dbContext.vArticlesZonesFulls.Where(va => va.Status == 1).OrderBy(o => o.Headline).ToList();
            }
            else
            {
                listVArticleZone = dbContext.vArticlesZonesFulls.Where(va => va.Status == 1 && va.ZoneID == ZoneID).OrderBy(o => o.Headline).ToList();
            }


            foreach (vArticlesZonesFull vArticleZone in listVArticleZone)
            {
                cms_asp_select_articles_by_zone_Result returnItem = new cms_asp_select_articles_by_zone_Result();

                returnItem.article_id = vArticleZone.ArticleID;
                returnItem.zone_id = vArticleZone.ZoneID;
                returnItem.headline = vArticleZone.Headline;
                returnItem.zone_name = vArticleZone.ZoneName;
                returnItem.zone_group_name = vArticleZone.ZoneGroupName;
                returnItem.site_name = vArticleZone.SiteName;
                returnItem.menu_text = vArticleZone.MenuText;
                returnItem.az_alias = vArticleZone.ArticleZoneAlias;
                returnItem.navigation_display = vArticleZone.NavigationDisplay;

                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_articles_by_zone_Result>("dbo.cms_asp_select_articles_by_zone @zone_id={0}", ZoneID == null ? -1 : ZoneID).ToList();
        }
        public List<cms_asp_select_article_zones_by_revision_Result> SelectArticleZonesRevisionDetails(long RevisionID)
        {

            List<cms_asp_select_article_zones_by_revision_Result> returnList = new List<cms_asp_select_article_zones_by_revision_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleZoneRevision> listArticleZR = new List<ArticleZoneRevision>();
            listArticleZR = dbContext.ArticleZoneRevisions.Where(a => a.RevID == RevisionID).ToList();

            foreach (ArticleZoneRevision articleZR in listArticleZR)
            {
                cms_asp_select_article_zones_by_revision_Result returnItem = new cms_asp_select_article_zones_by_revision_Result();

                Zone zone = new Zone();
                zone = dbContext.Zones.Where(z => z.Id == articleZR.ZoneID).FirstOrDefault();

                ZoneGroup zoneGroup = new ZoneGroup();
                zoneGroup = zone.ZoneGroup;

                Site site = new Site();
                site = dbContext.Sites.Where(s => s.Id == zoneGroup.SiteId).FirstOrDefault();

                string siteName = "", zoneGroupName = "", zoneName = "";
                int zoneID = 0, zoneTypeID = 0;
                if (zone != null)
                {
                    zoneID = zone.Id;
                    zoneName = !string.IsNullOrEmpty(zone.Name) ? zone.Name : "";
                    zoneTypeID = zone.ZoneTypeId != null ? zone.ZoneTypeId : 0;
                }

                if (zoneGroup != null)
                {
                    zoneGroupName = !string.IsNullOrEmpty(zoneGroup.Name) ? zoneGroup.Name : "";
                }
                if (site != null)
                {
                    siteName = !string.IsNullOrEmpty(site.Name) ? site.Name : "";
                }
                int articleId = articleZR.ArticleID;
                //vArticlesZonesFull vArticle = dbContext.vArticlesZonesFulls.Where(x => articleZR.)

                returnItem.out_id = zoneID;
                returnItem.out_name = siteName + " / " + zoneGroupName + " / " + zoneName;
                returnItem.az_order = articleZR.AzOrder;
                returnItem.zone_type_id = zoneTypeID;
                returnItem.az_alias = articleZR.AzAlias;
                returnItem.is_alias_protected = articleZR.IsAliasProtected;
                returnItem.is_page = articleZR.IsPage;

                #region Create Alias
                if (string.IsNullOrEmpty(returnItem.az_alias))
                {
                    //alias yoksa
                    //string urlStructure = "##lang##/##zonegroup##/##zone##/##article##";    //burası dinamik gelecek. 
                    returnItem.az_alias = CmsHelper.CreateAliasWithUrlStructure(articleZR.ArticleID, articleZR.ZoneID);
                }
                #endregion

                returnList.Add(returnItem);
            }

            returnList = returnList.OrderBy(o => o.out_name).ToList();
            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_article_zones_by_revision_Result>
            //("dbo.cms_asp_select_article_zones_by_revision @rev_id={0}",
            //RevisionID)
            //.ToList();
        }

        public List<cms_asp_select_article_language_relations_by_revision_Result> SelectArticleLanguageRelationsRevisionDetails(long RevisionID, int ZoneID, int ArticleID)
        {
            List<cms_asp_select_article_language_relations_by_revision_Result> returnList = new List<cms_asp_select_article_language_relations_by_revision_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            long languageRelationID = 0;
            //languageRelationID = dbContext.LanguageRelations.Where(lr => lr.ArticleID == ArticleID && lr.ZoneID == ZoneID).Select(s => s.LanguageRelationID).FirstOrDefault() != null ? dbContext.LanguageRelations.Where(lr => lr.ArticleID == ArticleID && lr.ZoneID == ZoneID).Select(s => s.LanguageRelationID).FirstOrDefault() : 0;

            //List<LanguageRelation> listLanguageR = new List<LanguageRelation>();
            //listLanguageR = dbContext.LanguageRelations.Where(lr => lr.LanguageRelationID == languageRelationID && lr.ArticleID != ArticleID).ToList();

            List<LanguageRelationRevision> listLanguageR = new List<LanguageRelationRevision>();
            var firstRel = dbContext.LanguageRelationRevisions.FirstOrDefault(lr => lr.RevisionID == RevisionID && lr.ArticleID == ArticleID && lr.ZoneID == ZoneID);
            if (firstRel != null)
            {
                languageRelationID = firstRel.LanguageRelationID;
            }


            listLanguageR = dbContext.LanguageRelationRevisions.Where(lr => lr.LanguageRelationID == languageRelationID && lr.ArticleID != ArticleID).ToList();


            foreach (LanguageRelationRevision langR in listLanguageR)
            {
                cms_asp_select_article_language_relations_by_revision_Result returnItem = new cms_asp_select_article_language_relations_by_revision_Result();

                Article getArticle = new Article();
                getArticle = dbContext.Articles.Where(a => a.Id == langR.ArticleID).FirstOrDefault();

                Zone getZone = new Zone();
                getZone = dbContext.Zones.Where(z => z.Id == langR.ZoneID).FirstOrDefault();

                ZoneGroup getZoneGroup = new ZoneGroup();
                getZoneGroup = getZone.ZoneGroup;

                Site getSite = new Site();
                getSite = getZoneGroup.Site;

                Language getLanguage = new Language();
                getLanguage = dbContext.Languages.Where(l => l.ID == getArticle.LangId).FirstOrDefault();

                string siteName = "", zoneGroupName = "", zoneName = "", articleHeadline = "", langName = "", articleLangID = "";
                int zoneID = 0, articleID = 0, langOrder = 0;

                if (getArticle != null)
                {
                    articleHeadline = !string.IsNullOrEmpty(getArticle.Headline) ? getArticle.Headline : "";
                    articleID = getArticle.Id;
                    articleLangID = getArticle.LangId != null ? getArticle.LangId : "";
                }
                if (getZone != null)
                {
                    zoneID = getZone.Id;
                    zoneName = !string.IsNullOrEmpty(getZone.Name) ? getZone.Name : "";
                }

                if (getZoneGroup != null)
                {
                    zoneGroupName = !string.IsNullOrEmpty(getZoneGroup.Name) ? getZoneGroup.Name : "";
                }
                if (getLanguage != null)
                {
                    langName = !string.IsNullOrEmpty(getLanguage.Name) ? getLanguage.Name : "";
                    langOrder = getLanguage.Order != null ? getLanguage.Order : 0;
                }
                if (getSite != null)
                {
                    siteName = !string.IsNullOrEmpty(getSite.Name) ? getSite.Name : "";
                }

                returnItem.zone_id = zoneID;
                returnItem.article_id = articleID;
                returnItem.site_name = siteName;
                returnItem.zone_group_name = zoneGroupName;
                returnItem.zone_name = zoneName;
                returnItem.headline = articleHeadline;
                returnItem.lang_name = langName;
                returnItem.lang_id = articleLangID;
                returnItem.lang_order = langOrder;
                returnItem.dir = "normal";
                returnItem.out_name = siteName + " / " + zoneGroupName + " / " + zoneName + " / " + articleHeadline;
                returnList.Add(returnItem);
            }

            returnList = returnList.OrderBy(o => o.lang_order).ToList();

            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_article_language_relations_by_revision_Result>
            //("dbo.cms_asp_select_article_language_relations_by_revision @rev_id={0},@zone_id={1},@article_id={2}",
            //RevisionID, ZoneID, ArticleID).ToList();

        }

        public List<cms_asp_select_article_relateds_by_revision_Result> SelectArticleRelationsRevisionDetails(long RevisionID)
        {
            List<cms_asp_select_article_relateds_by_revision_Result> returnList = new List<cms_asp_select_article_relateds_by_revision_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleRelationRevision> listArticleRelationR = new List<ArticleRelationRevision>();

            listArticleRelationR = dbContext.ArticleRelationRevisions.Where(a => a.RevisionID == RevisionID).ToList();

            foreach (ArticleRelationRevision articleRR in listArticleRelationR)
            {
                cms_asp_select_article_relateds_by_revision_Result returnItem = new cms_asp_select_article_relateds_by_revision_Result();

                if (articleRR.RelatedArticleID > 0)
                {
                    Article getArticle = new Article();
                    getArticle = dbContext.Articles.Where(a => a.Id == articleRR.RelatedArticleID).FirstOrDefault();

                    Zone getZone = new Zone();
                    getZone = dbContext.Zones.Where(z => z.Id == articleRR.RelatedZoneID).FirstOrDefault();

                    ZoneGroup getZoneGroup = new ZoneGroup();
                    getZoneGroup = getZone.ZoneGroup;

                    Site getSite = new Site();
                    getSite = getZoneGroup.Site;


                    returnItem.article_id = getArticle.Id;
                    returnItem.zone_id = getZone.Id;
                    returnItem.out_name = getSite.Name + " / " + getZoneGroup.Name + " / " + getZone.Name + " / " + getArticle.Headline;

                    returnList.Add(returnItem);
                }
            }

            returnList = returnList.OrderBy(o => o.out_name).ToList();

            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_article_relateds_by_revision_Result>
            //("dbo.cms_asp_select_article_relateds_by_revision @rev_id={0}",
            //RevisionID)
            //.ToList();
        }

        public List<cms_asp_select_article_zones_by_revision_Result> SelectArticlesByZone(long RevisionID)
        {
            List<cms_asp_select_article_zones_by_revision_Result> returnList = new List<cms_asp_select_article_zones_by_revision_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleZoneRevision> listArticleZR = new List<ArticleZoneRevision>();
            listArticleZR = dbContext.ArticleZoneRevisions.Where(az => az.RevID == RevisionID).ToList();

            foreach (ArticleZoneRevision articleZR in listArticleZR)
            {
                cms_asp_select_article_zones_by_revision_Result returnItem = new cms_asp_select_article_zones_by_revision_Result();

                Zone getZone = new Zone();
                getZone = dbContext.Zones.Where(z => z.Id == articleZR.ZoneID).FirstOrDefault();

                ZoneGroup getZoneGroup = new ZoneGroup();
                getZoneGroup = getZone.ZoneGroup;

                Site getSite = new Site();
                getSite = getZoneGroup.Site;

                returnItem.out_id = getZone.Id;
                returnItem.out_name = getSite.Name + " / " + getZoneGroup.Name + " / " + getZone.Name;
                returnItem.az_order = articleZR.AzOrder;
                returnItem.zone_type_id = getZone.ZoneTypeId;
                returnItem.az_alias = articleZR.AzAlias;

                returnList.Add(returnItem);
            }

            returnList = returnList.OrderBy(o => o.out_name).ToList();

            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_article_zones_by_revision_Result>
            //("dbo.cms_asp_select_article_zones_by_revision @rev_id={0}",
            //RevisionID)
            //.ToList();
        }

        public List<cms_asp_select_articles_by_zone_id_Result> SelectArticlesByZoneForStructure(int ZoneID)
        {
            List<cms_asp_select_articles_by_zone_id_Result> returnList = new List<cms_asp_select_articles_by_zone_id_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<vArticlesZonesFull> listVArticleZone = new List<vArticlesZonesFull>();
            listVArticleZone = dbContext.vArticlesZonesFulls.Where(va => va.Status != 2 && va.ZoneID == ZoneID && va.ZoneTypeID == 1 && va.ZoneStatus == "A").ToList();

            foreach (vArticlesZonesFull vArticleZone in listVArticleZone)
            {
                cms_asp_select_articles_by_zone_id_Result returnItem = new cms_asp_select_articles_by_zone_id_Result();
                returnItem.article_id = vArticleZone.ArticleID;
                returnItem.navigation_zone_id = vArticleZone.NavigationZoneID;
                returnItem.zone_id = vArticleZone.ZoneID;
                returnItem.headline = vArticleZone.Headline;
                returnItem.navigation_display = vArticleZone.NavigationDisplay;
                returnItem.menu_text = vArticleZone.MenuText;
                returnItem.status = vArticleZone.Status;
                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_articles_by_zone_id_Result>
            //("select a.article_id, a.navigation_zone_id, a.zone_id, a.headline, a.navigation_display, a.menu_text, a.status " +
            //" from dbo.vArticlesZones a with (nolock)" +
            //" where status <> 2 and zone_id = {0} and zone_type_id = 1 and zone_status = 'A'",
            //ZoneID)
            //.ToList();
        }

        public List<cms_asp_select_articles_by_zone_id_Result> SelectArticlesBySubZoneForStructure(int ZoneID)
        {
            List<cms_asp_select_articles_by_zone_id_Result> returnList = new List<cms_asp_select_articles_by_zone_id_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<vArticlesZonesFull> listVArticleZone = new List<vArticlesZonesFull>();
            listVArticleZone = dbContext.vArticlesZonesFulls.Where(va => va.Status != 2 && va.ZoneID == ZoneID && va.ZoneTypeID == 0 && va.ZoneStatus == "A").ToList();

            foreach (vArticlesZonesFull vArticleZone in listVArticleZone)
            {
                cms_asp_select_articles_by_zone_id_Result returnItem = new cms_asp_select_articles_by_zone_id_Result();
                returnItem.article_id = vArticleZone.ArticleID;
                returnItem.navigation_zone_id = vArticleZone.NavigationZoneID;
                returnItem.zone_id = vArticleZone.ZoneID;
                returnItem.headline = vArticleZone.Headline;
                returnItem.navigation_display = vArticleZone.NavigationDisplay;
                returnItem.menu_text = vArticleZone.MenuText;
                returnItem.status = vArticleZone.Status;
                returnList.Add(returnItem);
            }

            return returnList;


            //return this.Database.SqlQuery<cms_asp_select_articles_by_zone_id_Result>
            //("select a.article_id, a.navigation_zone_id, a.zone_id, a.headline, a.navigation_display, a.menu_text, a.status " +
            //" from dbo.vArticlesZones a with (nolock)" +
            //" where status <> 2 and zone_id = {0} and zone_type_id = 0 and zone_status = 'A'",
            //ZoneID)
            //.ToList();
        }

        public cms_asp_select_article_details_Result SelectArticleDetails(int zoneID, int articleID)
        {
            cms_asp_select_article_details_Result returnItem = new cms_asp_select_article_details_Result();

            CmsDbContext dbContext = new CmsDbContext();

            vArticlesZonesFull vArticleZone = new vArticlesZonesFull();
            if (zoneID == 0)
            {
                vArticleZone = dbContext.vArticlesZonesFulls.Where(va => va.ArticleID == articleID).FirstOrDefault();
            }
            else
            {
                vArticleZone = dbContext.vArticlesZonesFulls.Where(va => va.ArticleID == articleID && va.ZoneID == zoneID).FirstOrDefault();
            }

            returnItem.a_custom_body = vArticleZone.ArticleCustomBody;
            returnItem.append_1 = vArticleZone.ZoneAppend1;
            returnItem.append_2 = vArticleZone.ZoneAppend2;
            returnItem.append_3 = vArticleZone.ZoneAppend3;
            returnItem.append_4 = vArticleZone.ZoneAppend4;
            returnItem.append_5 = vArticleZone.ZoneAppend5;
            returnItem.article_1 = vArticleZone.Article1;
            returnItem.article_2 = vArticleZone.Article2;
            returnItem.article_3 = vArticleZone.Article3;
            returnItem.article_4 = vArticleZone.Article4;
            returnItem.article_5 = vArticleZone.Article5;
            returnItem.article_id = vArticleZone.ArticleID;
            returnItem.article_omniture_code = vArticleZone.ArticleOmnitureCode;
            returnItem.article_type = vArticleZone.ArticleType;
            returnItem.article_type_detail = vArticleZone.ArticleTypeDetail;
            returnItem.az_alias = vArticleZone.ArticleZoneAlias;
            returnItem.az_order = vArticleZone.AzOrder;
            returnItem.cl_1 = vArticleZone.Classification1;
            returnItem.cl_2 = vArticleZone.Classification2;
            returnItem.cl_3 = vArticleZone.Classification3;
            returnItem.cl_4 = vArticleZone.Classification4;
            returnItem.cl_5 = vArticleZone.Classification5;
            returnItem.clicks = vArticleZone.Clicks;
            returnItem.clsf_id = vArticleZone.ClassificationID;
            returnItem.created = vArticleZone.Created;
            returnItem.custom_1 = vArticleZone.Custom1;
            returnItem.custom_2 = vArticleZone.Custom2;
            returnItem.custom_3 = vArticleZone.Custom3;
            returnItem.custom_4 = vArticleZone.Custom4;
            returnItem.custom_5 = vArticleZone.Custom5;
            returnItem.custom_6 = vArticleZone.Custom6;
            returnItem.custom_7 = vArticleZone.Custom7;
            returnItem.custom_8 = vArticleZone.Custom8;
            returnItem.custom_9 = vArticleZone.Custom9;
            returnItem.custom_10 = vArticleZone.Custom10;
            returnItem.custom_11 = vArticleZone.Custom11;
            returnItem.custom_12 = vArticleZone.Custom12;
            returnItem.custom_13 = vArticleZone.Custom13;
            returnItem.custom_14 = vArticleZone.Custom14;
            returnItem.custom_15 = vArticleZone.Custom15;
            returnItem.custom_16 = vArticleZone.Custom16;
            returnItem.custom_17 = vArticleZone.Custom17;
            returnItem.custom_18 = vArticleZone.Custom18;
            returnItem.custom_19 = vArticleZone.Custom19;
            returnItem.custom_20 = vArticleZone.Custom20;
            returnItem.custom_setting = vArticleZone.CustomSetting;
            returnItem.date_1 = vArticleZone.Date1;
            returnItem.date_2 = vArticleZone.Date2;
            returnItem.date_3 = vArticleZone.Date3;
            returnItem.date_4 = vArticleZone.Date4;
            returnItem.date_5 = vArticleZone.Date5;
            returnItem.domain_name = vArticleZone.DomainName;
            returnItem.enddate = vArticleZone.EndDate;
            returnItem.flag_1 = vArticleZone.Flag1;
            returnItem.flag_2 = vArticleZone.Flag2;
            returnItem.flag_3 = vArticleZone.Flag3;
            returnItem.flag_4 = vArticleZone.Flag4;
            returnItem.flag_5 = vArticleZone.Flag5;
            returnItem.headline = vArticleZone.Headline;
            returnItem.keywords = vArticleZone.Keywords;
            returnItem.lang_id = vArticleZone.LanguageID;
            returnItem.menu_text = vArticleZone.MenuText;
            returnItem.meta_description = vArticleZone.MetaDescription;
            returnItem.navigation_display = vArticleZone.NavigationDisplay;
            returnItem.navigation_zone_id = vArticleZone.NavigationZoneID;
            returnItem.orderno = vArticleZone.OrderNo;
            returnItem.publisher_id = vArticleZone.PublisherID;
            returnItem.rating = vArticleZone.Rating;
            returnItem.ratingcount = vArticleZone.RatingCount;
            returnItem.s_article_1 = vArticleZone.SiteArticle1;
            returnItem.s_article_2 = vArticleZone.SiteArticle2;
            returnItem.s_article_3 = vArticleZone.SiteArticle3;
            returnItem.s_article_4 = vArticleZone.SiteArticle4;
            returnItem.s_article_5 = vArticleZone.SiteArticle5;
            returnItem.s_custom_body = vArticleZone.SiteCustomBody;
            returnItem.site_analytics = vArticleZone.SiteAnalytics;
            returnItem.site_created = vArticleZone.SiteCreated;
            returnItem.site_css_id = vArticleZone.SiteCssId;
            returnItem.site_css_id_mobile = vArticleZone.SiteMobileCssID;
            returnItem.site_css_id_print = vArticleZone.SitePrintCssID;
            returnItem.site_default_article = vArticleZone.SiteDefaultArticle;
            returnItem.site_header = vArticleZone.SiteHeader;
            returnItem.site_icon = vArticleZone.SiteIcon;
            returnItem.site_id = vArticleZone.ZoneGroupSiteId;
            returnItem.site_js = vArticleZone.SiteJs;
            returnItem.site_keywords = vArticleZone.SiteKeywords;
            returnItem.site_meta_description = vArticleZone.SiteMetaDescription;
            returnItem.site_name = vArticleZone.SiteName;
            returnItem.site_omniture_code = vArticleZone.SiteOmnitureCode;
            returnItem.site_publisher_id = vArticleZone.SitePublisherID;
            returnItem.site_template_id = vArticleZone.SiteTemplateId;
            returnItem.site_template_id_mobile = vArticleZone.SiteMobileTemplateID;
            returnItem.site_updated = vArticleZone.SiteUpdates;
            returnItem.startdate = vArticleZone.StartDate;
            returnItem.status = vArticleZone.Status;
            returnItem.summary = vArticleZone.Summary;
            returnItem.updated = vArticleZone.Updated;
            returnItem.zg_analytics = vArticleZone.ZoneGroupAnalytics;
            returnItem.zg_append_1 = vArticleZone.ZoneGroupAppend1;
            returnItem.zg_append_2 = vArticleZone.ZoneGroupAppend2;
            returnItem.zg_append_3 = vArticleZone.ZoneGroupAppend3;
            returnItem.zg_append_4 = vArticleZone.ZoneGroupAppend4;
            returnItem.zg_append_5 = vArticleZone.ZoneGroupAppend5;
            returnItem.zg_article_1 = vArticleZone.ZoneGroupArticle1;
            returnItem.zg_article_2 = vArticleZone.ZoneGroupArticle2;
            returnItem.zg_article_3 = vArticleZone.ZoneGroupArticle3;
            returnItem.zg_article_4 = vArticleZone.ZoneGroupArticle4;
            returnItem.zg_article_5 = vArticleZone.ZoneGroupArticle5;
            returnItem.zg_created = vArticleZone.ZoneGroupCreated;
            returnItem.zg_css_id = vArticleZone.ZoneGroupCssID;
            returnItem.zg_css_id_mobile = vArticleZone.ZoneGroupMobileCssID;
            returnItem.zg_css_id_print = vArticleZone.ZoneGroupPrintCssID;
            returnItem.zg_css_merge = vArticleZone.ZoneGroupCssMerge;
            returnItem.zg_custom_body = vArticleZone.ZoneGroupCustomBody;
            returnItem.zg_publisher_id = vArticleZone.ZoneGroupPublisherID;
            returnItem.zg_template_id = vArticleZone.ZoneGroupTemplateID;
            returnItem.zg_template_id_mobile = vArticleZone.ZoneGroupMobileTemplateID;
            returnItem.zg_updated = vArticleZone.ZoneGroupUpdated;
            returnItem.zone_analytics = vArticleZone.ZoneAnalytics;
            returnItem.zone_article_1 = vArticleZone.ZoneArticle1;
            returnItem.zone_article_2 = vArticleZone.ZoneArticle2;
            returnItem.zone_article_3 = vArticleZone.ZoneArticle3;
            returnItem.zone_article_4 = vArticleZone.ZoneArticle4;
            returnItem.zone_article_5 = vArticleZone.ZoneArticle5;
            returnItem.zone_created = vArticleZone.ZoneCreated;
            returnItem.zone_css_id = vArticleZone.ZoneCssID;
            returnItem.zone_css_id_mobile = vArticleZone.ZoneMobileCssID;
            returnItem.zone_css_id_print = vArticleZone.ZonePrintCssID;
            returnItem.zone_css_merge = vArticleZone.ZoneCssMerge;
            returnItem.zone_custom_body = vArticleZone.ZoneCustomBody;
            returnItem.zone_default_article = vArticleZone.ZoneDefaultArticle;
            returnItem.zone_desc = vArticleZone.ZoneDescription;
            returnItem.zone_group_default_article = vArticleZone.ZoneGroupDefaultArticle;
            returnItem.zone_group_id = vArticleZone.ZoneGroupID;
            returnItem.zone_group_keywords = vArticleZone.ZoneGroupKeywords;
            returnItem.zone_group_meta_description = vArticleZone.ZoneGroupMetaDescription;
            returnItem.zone_group_name = vArticleZone.ZoneGroupName;
            returnItem.zone_group_name_display = vArticleZone.ZoneGroupNameDisplay;
            returnItem.zone_group_omniture_code = vArticleZone.ZoneGroupOmnitureCode;
            returnItem.zone_id = vArticleZone.ZoneID;
            returnItem.zone_keywords = vArticleZone.ZoneKeywords;
            returnItem.zone_meta_description = vArticleZone.ZoneMetaDescription;
            returnItem.zone_name = vArticleZone.ZoneName;
            returnItem.zone_name_display = vArticleZone.ZoneNameDisplay;
            returnItem.zone_omniture_code = vArticleZone.ZoneOmnitureCode;
            returnItem.zone_publisher_id = vArticleZone.ZonePublisherID;
            returnItem.zone_status = vArticleZone.ZoneStatus;
            returnItem.zone_template_id = vArticleZone.ZoneTemplateID;
            returnItem.zone_template_id_mobile = vArticleZone.ZoneMobileTemplateID;
            returnItem.zone_type_id = vArticleZone.ZoneTypeID;
            returnItem.zone_updated = vArticleZone.ZoneUpdated;

            return returnItem;
            //return this.Database.SqlQuery<cms_asp_select_article_details_Result>("dbo.cms_asp_select_article_details @zone_id = {0}, @article_id={1}", zoneID, articleID).FirstOrDefault();
        }

        public List<vArticlesZones> SelectArticles()
        {
            List<vArticlesZones> returnList = new List<vArticlesZones>();

            CmsDbContext dbContext = new CmsDbContext();

            List<vArticlesZonesFull> listVArticleZone = new List<vArticlesZonesFull>();
            listVArticleZone = dbContext.vArticlesZonesFulls.ToList();

            foreach (vArticlesZonesFull vArticleZone in listVArticleZone)
            {
                vArticlesZones returnItem = new vArticlesZones();

                returnItem.article_id = vArticleZone.ArticleID;
                returnItem.headline = vArticleZone.Headline;
                returnItem.menu_text = vArticleZone.MenuText;
                returnItem.site_name = vArticleZone.SiteName;
                returnItem.zone_group_name = vArticleZone.ZoneGroupName;
                returnItem.zone_name = vArticleZone.ZoneName;

                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<vArticlesZones>
            //("select * from dbo.vArticlesZones")
            //.ToList();
        }

        public List<string> SelectArticleZonesNames(int ZoneID, int ArticleID)
        {
            //List<string> returnList = new List<string>();

            //ArticleRepository ar = new ArticleRepository();
            //ArticleService articleService = new ArticleService(ar);

            //ZoneRepository zr = new ZoneRepository();
            //ZoneService zoneService = new ZoneService(zr);

            //ZoneGroupRepository zgr = new ZoneGroupRepository();
            //ZoneGroupService zoneGroupService = new ZoneGroupService(zgr);

            //SiteRepository sr = new SiteRepository();
            //SiteService siteService = new SiteService(sr);

            //string outZone = "";
            //string outArticle = "";
            //string outName = "";
            //if (ZoneID > 0)
            //{
            //    Zone getZone = new Zone();
            //    getZone = zoneService.GetAll().Where(z => z.Id == ZoneID).FirstOrDefault();
            //    string zoneName = getZone.Name;
            //    string zoneGroupName = getZone.ZoneGroup.Name;
            //    string siteName = getZone.ZoneGroup.Site.Name;
            //    outZone = siteName + " / " + zoneGroupName + " / " + zoneName;

            //    if (ArticleID > -1)
            //    {
            //        Article getArticle = new Article();
            //        getArticle = articleService.GetAll().Where(a => a.Id == ArticleID).FirstOrDefault();
            //        outArticle = " / " + getArticle.Headline;
            //    }
            //}
            //else
            //{
            //    if (ArticleID > -1)
            //    {
            //        Article getArticle = new Article();
            //        getArticle = articleService.GetAll().Where(a => a.Id == ArticleID).FirstOrDefault();
            //        outArticle = " / " + getArticle.Headline;
            //    }
            //}

            //outName = outZone + outArticle;

            //returnList.Add(outName);

            //return returnList;

            return this.Database.SqlQuery<string>
            ("dbo.cms_asp_admin_select_article_zone_names @zone_id={0}, @article_id = {1}", ZoneID, ArticleID)
            .ToList();
        }

        // Entity Framework'e çevrilmedi
        public List<cms_article_search_result> SearchArticles(string Keyword, int ClasificationID, object publisherID, bool IsRevision, string ArticleIDs,
                string Alias, string Language, int? ZoneGroupID, int? ZoneID, int? Status, bool? Status0, bool? Status1, bool? Status2, DateTime? displayedStart, DateTime? displayedEnd,
                DateTime? modifiedStart, DateTime? modifiedEnd, DateTime? approvedStart, DateTime? approvedEnd,
                bool? revFlag1, bool? revFlag2, bool? revFlag3, bool? revFlag4, bool? revFlag5, int? ClassificationID, int? SearchOrder, int? SiteID, Guid? UserId, string TagID, List<string> allowedArticleIds, List<string> disAllowedArticleIds, string fileTypes)
        {
            string zone_joiner = "";
            string sql = "";
            string search_query = "";
            string search_order = "";

            //if (SiteID > 0)
            //    search_query = search_query + " AND s.site_id = " + SiteID + " ";

            #region fileTypes
            if (!string.IsNullOrEmpty(fileTypes))
            {
                CmsDbContext dbContext = new CmsDbContext();
                FileType ft = dbContext.FileTypes.Where(x => x.Name.ToLower() == fileTypes.ToLower()).FirstOrDefault();
                List<int> fileTypeArticles = dbContext.Files.Where(x => x.FileTypeId == ft.ID).GroupBy(x => x.ArticleId).Select(x => x.FirstOrDefault()).Select(x => x.ArticleId).ToList();

                List<int> finalArticleList = new List<int>();
                if (!string.IsNullOrEmpty(ArticleIDs))
                {
                    List<string> wantedArticles = ArticleIDs.Split(',').ToList();

                    ArticleIDs = string.Empty;
                    foreach (int i in fileTypeArticles)
                    {
                        if (wantedArticles.Contains(i.ToString()))
                        {
                            finalArticleList.Add(i);
                        }
                    }
                }
                else
                {
                    finalArticleList = fileTypeArticles;
                }

                if (finalArticleList.Count > 0)
                {
                    ArticleIDs = string.Join(",", finalArticleList.ToArray());
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(ArticleIDs))
                search_query = search_query + " AND r.article_id in (" + ArticleIDs + ") ";

            if (!string.IsNullOrEmpty(Alias))
                search_query = search_query + "AND zr.az_alias = '" + Alias + "'";

            if (!string.IsNullOrEmpty(Language))
                search_query = search_query + " AND r.lang_id = '" + Language + "' ";

            if (ClasificationID > 0)
                search_query = search_query + " AND r.clsf_id = " + ClasificationID + " ";

            if (Convert.ToInt32(publisherID) != 0)
            {
                if (Convert.ToInt32(publisherID) == -1)
                    search_query = search_query + " AND r.revised_by = " + HttpContext.Current.Session["PublisherID"] + " ";
                else if (Convert.ToInt32(publisherID) == -2)
                    search_query = search_query + " AND r.approval_id = " + HttpContext.Current.Session["PublisherID"] + " ";
                else
                    search_query = search_query + " AND r.revised_by = " + HttpContext.Current.Session["PublisherID"] + " ";
            }

            #region Status
            /* if (Status == 1)
                search_query = search_query + " AND r.status = 1 ";
            else if (Status == 2)
                search_query = search_query + " AND r.status = 0 ";*/
            if (Status == 3)
                search_query = search_query + " AND r.revision_status = 'A' ";
            else if (Status == 4)
                search_query = search_query + " AND r.revision_status = 'W' ";
            else if (Status == 5)
                search_query = search_query + " AND r.revision_status = 'N' ";
            else if (Status == 6)
                search_query = search_query + " AND r.revision_status = 'X' ";
            else if (Status == 7)
                search_query = search_query + " AND r.revision_status = 'L' ";
            else if (Status == 8)
                search_query = search_query + " AND r.revision_status = 'E' ";

            string statusQuery = string.Empty;
            List<int> statusColumns = new List<int>();
            if (Status0 == true)
            {
                statusColumns.Add(1);
            }
            if (Status1 == true)
            {
                statusColumns.Add(0);
            }
            if (Status2 == true)
            {
                statusColumns.Add(2);
            }

            if (statusColumns.Count > 0)
            {
                statusQuery = " AND (";
                for (int i = 0; i < statusColumns.Count; i++)
                {
                    statusQuery += " r.status = " + statusColumns[i];
                    if (i != statusColumns.Count - 1)
                    {
                        statusQuery += " or ";
                    }
                }
                statusQuery += ")";
            }
            search_query += statusQuery;
            #endregion

            if (!string.IsNullOrEmpty(TagID))
            {
                search_query = search_query + " AND tag_contents like '%" + TagID + "%'";
            }

            if (allowedArticleIds != null)
            {
                if (disAllowedArticleIds != null)
                {
                    foreach (string s in disAllowedArticleIds)
                    {
                        allowedArticleIds.Remove(s);
                    }
                }
                if (allowedArticleIds.Count > 0)
                {
                    search_query = search_query + " AND a.article_id in (" + string.Join(",", allowedArticleIds.ToArray()) + ") ";
                }
            }

            if (SearchOrder == 2)
                search_order = " r.headline asc";
            else if (SearchOrder == 3)
                search_order = " r.headline desc";
            else if (SearchOrder == 4)
                search_order = "  r.startdate asc ";
            else if (SearchOrder == 5)
                search_order = " r.startdate desc";
            else if (SearchOrder == 6)
                search_order = " a.clicks asc";
            else if (SearchOrder == 7)
                search_order = " a.clicks desc ";
            else
                search_order = " r.rev_date desc";

            if (ClassificationID != null && ClassificationID > 0)
                search_query = search_query + " AND a.clsf_id = " + ClassificationID;

            if (UserId != null)
            {
                if (!string.IsNullOrEmpty(UserId.ToString()))
                {
                    search_query = search_query + "AND (r.created_by = '" + UserId + "' or r.revised_by = '" + UserId + "' or r.approval_id = '" + UserId + "')";
                }
            }

            //if (Status != 6)
            //    search_query = search_query + " AND r.revision_status <> 'X' ";

            if (revFlag1 != null && revFlag1 == true)
                search_query = search_query + " AND r.rev_flag_1 = 1";
            if (revFlag2 != null && revFlag2 == true)
                search_query = search_query + " AND r.rev_flag_2 = 1";
            if (revFlag3 != null && revFlag3 == true)
                search_query = search_query + " AND r.rev_flag_3 = 1";
            if (revFlag4 != null && revFlag4 == true)
                search_query = search_query + " AND r.rev_flag_4 = 1";
            if (revFlag5 != null && revFlag5 == true)
                search_query = search_query + " AND r.rev_flag_5 = 1";

            if (ZoneID > 0)
                zone_joiner = " and zr.zone_id = " + ZoneID;

            if (ZoneGroupID > 0 && ZoneID < 1)
                zone_joiner = " and zr.zone_id in (select zone_id from dbo.cms_zones with (nolock) where zone_group_id = " + ZoneGroupID + " )";

            if (!string.IsNullOrEmpty(Keyword))
            {
                search_query = search_query + " AND ( " +
               " r.rev_name like '%" + Keyword + "%' OR " +
               " r.rev_note like '%" + Keyword + "%' OR " +
               " r.menu_text like '%" + Keyword + "%' OR " +
               " r.headline like '%" + Keyword + "%' OR " +
               " r.summary like '%" + Keyword + "%' OR " +
               " r.keywords like '%" + Keyword + "%' OR " +
               " r.article_1 like '%" + Keyword + "%' OR " +
               " r.article_2 like '%" + Keyword + "%' OR " +
               " r.article_3 like '%" + Keyword + "%' OR " +
               " r.article_4 like '%" + Keyword + "%' OR " +
               " r.article_5 like '%" + Keyword + "%' OR " +
               " r.custom_1 like '%" + Keyword + "%' OR " +
               " r.custom_2 like '%" + Keyword + "%' OR " +
               " r.custom_3 like '%" + Keyword + "%' OR " +
               " r.custom_4 like '%" + Keyword + "%' OR " +
               " r.custom_5 like '%" + Keyword + "%' OR " +
               " r.custom_6 like '%" + Keyword + "%' OR " +
               " r.custom_7 like '%" + Keyword + "%' OR " +
               " r.custom_8 like '%" + Keyword + "%' OR " +
               " r.custom_9 like '%" + Keyword + "%' OR " +
               " r.custom_10 like '%" + Keyword + "%' OR " +
               " r.custom_11 like '%" + Keyword + "%' OR " +
               " r.custom_12 like '%" + Keyword + "%' OR " +
               " r.custom_13 like '%" + Keyword + "%' OR " +
               " r.custom_14 like '%" + Keyword + "%' OR " +
               " r.custom_15 like '%" + Keyword + "%' OR " +
               " r.custom_16 like '%" + Keyword + "%' OR " +
               " r.custom_17 like '%" + Keyword + "%' OR " +
               " r.custom_18 like '%" + Keyword + "%' OR " +
               " r.custom_19 like '%" + Keyword + "%' OR " +
               " r.custom_20 like '%" + Keyword + "%' OR " +
               " zr.az_alias like '%" + Keyword + "%' " +
               " ) ";
            }

            if (IsRevision)
            {

                sql = "select top 1000  zr.az_order, r.article_id, r.rev_id, r.rev_name, r.created, r.created_by, r.rev_date, r.revised_by, r.menu_text, r.headline, r.summary, r.startdate, r.enddate, r.approval_date, r.approval_id, r.revision_status, r.status, a.clicks, " +
                             " (select top 1 cs.site_name + ' | ' + czg.zone_group_name + ' | ' + cz.zone_name from dbo.cms_zones cz with (nolock) left join dbo.cms_zone_groups czg with (nolock) on czg.zone_group_id = cz.zone_group_id left join dbo.cms_sites cs with (nolock) on cs.site_id = czg.site_id where cz.zone_id = zr.zone_id) as zone_name, zr.zone_id, a.locked_by, a.locked, p.UserName, zr.az_alias" +
                             " from dbo.cms_article_revision r with (nolock) inner join dbo.cms_article_zones_revision zr with (nolock) on zr.rev_id = r.rev_id " + zone_joiner + "  left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id left join dbo.vw_aspnet_MembershipUsers p with (nolock) on a.locked_by = p.UserId  where 1 = 1 " + search_query + " order by " + search_order + "";
                //where status <> 2 kısmını  1==1 olarak değiştirdim çünkü tüm query'ler AND ile devam edecek şekilde hazırlanmıştı. Değişmesi gereken çok yer vardı 
            }
            else
            {
                sql = "select top 1000 zr.az_order, r.article_id, r.rev_id, r.rev_name, r.created, r.created_by, r.rev_date, r.revised_by, r.menu_text, r.headline, r.summary, r.startdate, r.enddate, r.approval_date, r.approval_id, r.revision_status, r.status, a.clicks, " +
                             " (select top 1 cs.site_name + ' | ' + czg.zone_group_name + ' | ' + cz.zone_name from dbo.cms_zones cz with (nolock) left join dbo.cms_zone_groups czg with (nolock) on czg.zone_group_id = cz.zone_group_id left join dbo.cms_sites cs with (nolock) on cs.site_id = czg.site_id where cz.zone_id = zr.zone_id) as zone_name, zr.zone_id, a.locked_by, a.locked, p.UserName, zr.az_alias" +
                             " from dbo.vArticlesLiveRevisions r with (nolock) inner join dbo.cms_article_zones_revision zr with (nolock) on zr.article_id = r.article_id  " + zone_joiner + " and  zr.rev_id = r.rev_id left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id left join dbo.vw_aspnet_MembershipUsers p with (nolock)  on a.locked_by = p.UserId   where 1 = 1 " + search_query + " order by " + search_order + "";
                //where status <> 2 kısmını  1==1 olarak değiştirdim çünkü tüm query'ler AND ile devam edecek şekilde hazırlanmıştı. Değişmesi gereken çok yer vardı 
            }

            return this.Database.SqlQuery<cms_article_search_result>
                (sql)
                .ToList();
        }

        public List<cms_asp_admin_update_article_revision_Result> SaveArticleRevision(cms_article_revision revision)
        {
            List<cms_asp_admin_update_article_revision_Result> returnList = new List<cms_asp_admin_update_article_revision_Result>();

            cms_asp_admin_update_article_revision_Result returnItem = new cms_asp_admin_update_article_revision_Result();

            CmsDbContext dbContext = new CmsDbContext();

            LanguageRepository lr = new LanguageRepository();
            LanguageService languageService = new LanguageService(lr);

            ArticleRepository ar = new ArticleRepository();
            ArticleService articleService = new ArticleService(ar);

            ArticleRevisionRepository arr = new ArticleRevisionRepository();
            ArticleRevisionService articleRevisionService = new ArticleRevisionService(arr);


            revision.rev_id = revision.rev_id == 0 ? -1 : revision.rev_id;
            revision.rev_name = revision.rev_name ?? "";
            revision.rev_note = revision.rev_note ?? "";
            revision.article_id = revision.article_id == 0 ? -1 : revision.article_id;
            revision.menu_text = revision.menu_text ?? "";
            revision.summary = revision.summary ?? "";
            revision.keywords = revision.keywords ?? "";
            revision.article_type_detail = revision.article_type_detail ?? "";
            revision.article_1 = HttpUtility.HtmlEncode(revision.article_1 ?? "");
            revision.article_2 = HttpUtility.HtmlEncode(revision.article_2 ?? "");
            revision.article_3 = HttpUtility.HtmlEncode(revision.article_3 ?? "");
            revision.article_4 = HttpUtility.HtmlEncode(revision.article_4 ?? "");
            revision.article_5 = HttpUtility.HtmlEncode(revision.article_5 ?? "");
            revision.custom_body = revision.custom_body ?? "";
            revision.revised_by = revision.revised_by;
            revision.cio = revision.cio;
            revision.meta_description = revision.meta_description ?? "";
            revision.content_1_editor_type = revision.content_1_editor_type ?? "H";
            revision.content_2_editor_type = revision.content_2_editor_type ?? "H";
            revision.content_3_editor_type = revision.content_3_editor_type ?? "H";
            revision.content_4_editor_type = revision.content_4_editor_type ?? "H";
            revision.content_5_editor_type = revision.content_5_editor_type ?? "H";
            revision.omniture_code = revision.omniture_code ?? "";
            revision.custom_setting = revision.custom_setting ?? ";";
            revision.tag_ids = revision.tag_ids ?? "";
            revision.tag_contents = revision.tag_contents ?? "";

            Language getLanguage = new Language();
            getLanguage = dbContext.Languages.Where(l => l.ID == revision.lang_id).FirstOrDefault();

            if (getLanguage != null)
            {
                if (revision.article_id == -1)
                {
                    Article insertArticle = new Article();
                    insertArticle.LangId = revision.lang_id;
                    insertArticle.Headline = revision.headline;
                    insertArticle.CreatedBy = (Guid)revision.revised_by;

                    insertArticle.Created = DateTime.Now;
                    insertArticle.Updated = DateTime.Now;

                    int insertedArticleID = 0;
                    insertedArticleID = articleService.Insert(insertArticle).Id;
                    revision.article_id = insertedArticleID;
                    returnItem.article_id = insertedArticleID;

                    returnItem.astat = "C";
                }

                if (revision.rev_id == -1)
                {
                    ArticleRevision insertArticleRevision = new ArticleRevision();

                    insertArticleRevision.Created = DateTime.Now;
                    insertArticleRevision.RevisionDate = DateTime.Now;
                    insertArticleRevision.RevisionStatus = "N";

                    insertArticleRevision.RevisedBy = (Guid)revision.revised_by;
                    insertArticleRevision.RevisionName = revision.rev_name;
                    insertArticleRevision.RevisionNote = revision.rev_note;
                    insertArticleRevision.CreatedBy = (Guid)revision.revised_by;
                    insertArticleRevision.ArticleId = revision.article_id;
                    insertArticleRevision.ClassificationId = revision.clsf_id;
                    insertArticleRevision.Status = Convert.ToByte(revision.status);
                    insertArticleRevision.Startdate = revision.startdate;
                    insertArticleRevision.Enddate = revision.enddate;
                    insertArticleRevision.Order = revision.orderno;
                    insertArticleRevision.LangId = revision.lang_id;
                    insertArticleRevision.NavigationDisplay = Convert.ToByte(revision.navigation_display);
                    insertArticleRevision.NavigationZoneId = revision.navigation_zone_id;
                    insertArticleRevision.MenuText = revision.menu_text;
                    insertArticleRevision.Headline = revision.headline;
                    insertArticleRevision.Summary = revision.summary;
                    insertArticleRevision.Keywords = revision.keywords;
                    insertArticleRevision.ArticleType = revision.article_type;
                    insertArticleRevision.ArticleTypeDetail = revision.article_type_detail;
                    insertArticleRevision.Article1 = revision.article_1;
                    insertArticleRevision.Article2 = revision.article_2;
                    insertArticleRevision.Article3 = revision.article_3;
                    insertArticleRevision.Article4 = revision.article_4;
                    insertArticleRevision.Article5 = revision.article_5;
                    insertArticleRevision.Custom1 = revision.custom_1;
                    insertArticleRevision.Custom2 = revision.custom_2;
                    insertArticleRevision.Custom3 = revision.custom_3;
                    insertArticleRevision.Custom4 = revision.custom_4;
                    insertArticleRevision.Custom5 = revision.custom_5;
                    insertArticleRevision.Custom6 = revision.custom_6;
                    insertArticleRevision.Custom7 = revision.custom_7;
                    insertArticleRevision.Custom8 = revision.custom_8;
                    insertArticleRevision.Custom9 = revision.custom_9;
                    insertArticleRevision.Custom10 = revision.custom_10;
                    insertArticleRevision.Custom11 = revision.custom_11;
                    insertArticleRevision.Custom12 = revision.custom_12;
                    insertArticleRevision.Custom13 = revision.custom_13;
                    insertArticleRevision.Custom14 = revision.custom_14;
                    insertArticleRevision.Custom15 = revision.custom_15;
                    insertArticleRevision.Custom16 = revision.custom_16;
                    insertArticleRevision.Custom17 = revision.custom_17;
                    insertArticleRevision.Custom18 = revision.custom_18;
                    insertArticleRevision.Custom19 = revision.custom_19;
                    insertArticleRevision.Custom20 = revision.custom_20;
                    insertArticleRevision.Flag1 = revision.flag_1;
                    insertArticleRevision.Flag2 = revision.flag_2;
                    insertArticleRevision.Flag3 = revision.flag_3;
                    insertArticleRevision.Flag4 = revision.flag_4;
                    insertArticleRevision.Flag5 = revision.flag_5;
                    insertArticleRevision.date_1 = revision.date_1;
                    insertArticleRevision.date_2 = revision.date_2;
                    insertArticleRevision.date_3 = revision.date_3;
                    insertArticleRevision.date_4 = revision.date_4;
                    insertArticleRevision.date_5 = revision.date_5;
                    insertArticleRevision.RevisionFlag1 = revision.rev_flag_1;
                    insertArticleRevision.RevisionFlag2 = revision.rev_flag_2;
                    insertArticleRevision.RevisionFlag3 = revision.rev_flag_3;
                    insertArticleRevision.RevisionFlag4 = revision.rev_flag_4;
                    insertArticleRevision.RevisionFlag5 = revision.rev_flag_5;
                    insertArticleRevision.Cl1 = revision.cl_1;
                    insertArticleRevision.Cl2 = revision.cl_2;
                    insertArticleRevision.Cl3 = revision.cl_3;
                    insertArticleRevision.Cl4 = revision.cl_4;
                    insertArticleRevision.Cl5 = revision.cl_5;
                    insertArticleRevision.CustomBody = revision.custom_body;
                    insertArticleRevision.MetaDescription = revision.meta_description;
                    insertArticleRevision.ContentEditorType1 = revision.content_1_editor_type;
                    insertArticleRevision.ContentEditorType2 = revision.content_2_editor_type;
                    insertArticleRevision.ContentEditorType3 = revision.content_3_editor_type;
                    insertArticleRevision.ContentEditorType4 = revision.content_4_editor_type;
                    insertArticleRevision.ContentEditorType5 = revision.content_5_editor_type;
                    insertArticleRevision.OmnitureCode = revision.omniture_code;
                    insertArticleRevision.CustomSettings = revision.custom_setting;
                    insertArticleRevision.BeforeBody = revision.before_body;
                    insertArticleRevision.BeforeHead = revision.before_head;
                    insertArticleRevision.NoIndexNoFollow = revision.no_index_no_follow;
                    insertArticleRevision.CustomHtmlAttr = revision.custom_html_attr;
                    insertArticleRevision.MetaTitle = revision.meta_title;
                    insertArticleRevision.CanonicalUrl = revision.canonical_url;
                    insertArticleRevision.TagIds = revision.tag_ids;
                    insertArticleRevision.TagContents = revision.tag_contents;

                    //2017-09-19
                    insertArticleRevision.AfterBody = revision.afterbody;
                    insertArticleRevision.HidePrefix = revision.hideprefix;
                    insertArticleRevision.HideSuffix = revision.hidesuffix;
                    //2017-09-19

                    ArticleRevision getArticleRev = new ArticleRevision();
                    getArticleRev = articleRevisionService.Insert(insertArticleRevision);

                    revision.rev_id = getArticleRev.RevisionId;
                    returnItem.rev_id = getArticleRev.RevisionId;
                    returnItem.rstat = "C";

                }
                else
                {
                    Article getArticle = new Article();

                    getArticle = dbContext.Articles.Where(a => a.Id == revision.article_id && a.LockedBy == (Guid)revision.revised_by).FirstOrDefault();

                    if (getArticle == null && revision.cio == "1")
                    {
                        Article getArt = new Article();
                        getArt = dbContext.Articles.Where(a => a.Id == revision.article_id).FirstOrDefault();

                        vAspNetMembershipUser getVAspNetMU = new vAspNetMembershipUser();
                        getVAspNetMU = dbContext.vAspNetMembershipUsers.Where(v => v.UserId == getArt.LockedBy).FirstOrDefault();

                        returnItem.rstat = "L";
                        returnItem.locked = getArt.Locked;
                        returnItem.locked_by = getArt.LockedBy.ToString();
                        returnList.Add(returnItem);
                        return returnList;
                    }

                    ArticleRevision getArticleRev = new ArticleRevision();
                    getArticleRev = dbContext.ArticleRevisions.Where(s => s.RevisionId == revision.rev_id && s.RevisionStatus == "N").FirstOrDefault();

                    if (getArticleRev != null)
                    {
                        ArticleRevision updateArticleRevision = new ArticleRevision();
                        updateArticleRevision = dbContext.ArticleRevisions.Where(s => s.RevisionId == revision.rev_id).FirstOrDefault();

                        updateArticleRevision.Created = DateTime.Now;

                        updateArticleRevision.RevisionDate = DateTime.Now;
                        updateArticleRevision.RevisedBy = (Guid)revision.revised_by;
                        updateArticleRevision.RevisionName = revision.rev_name;
                        updateArticleRevision.RevisionNote = revision.rev_note;
                        updateArticleRevision.ArticleId = revision.article_id;
                        updateArticleRevision.ClassificationId = revision.clsf_id;
                        updateArticleRevision.Status = Convert.ToByte(revision.status);
                        updateArticleRevision.Startdate = revision.startdate;
                        updateArticleRevision.Enddate = revision.enddate;
                        updateArticleRevision.Order = revision.orderno;
                        updateArticleRevision.LangId = revision.lang_id;
                        updateArticleRevision.NavigationDisplay = Convert.ToByte(revision.navigation_display);
                        updateArticleRevision.NavigationZoneId = revision.navigation_zone_id;
                        updateArticleRevision.MenuText = revision.menu_text;
                        updateArticleRevision.Headline = revision.headline;
                        updateArticleRevision.Keywords = revision.keywords;
                        updateArticleRevision.ArticleType = revision.article_type;
                        updateArticleRevision.ArticleTypeDetail = revision.article_type_detail;
                        updateArticleRevision.Article1 = revision.article_1;
                        updateArticleRevision.Article2 = revision.article_2;
                        updateArticleRevision.Article3 = revision.article_3;
                        updateArticleRevision.Article4 = revision.article_4;
                        updateArticleRevision.Article5 = revision.article_5;
                        updateArticleRevision.Custom1 = revision.custom_1;
                        updateArticleRevision.Custom2 = revision.custom_2;
                        updateArticleRevision.Custom3 = revision.custom_3;
                        updateArticleRevision.Custom4 = revision.custom_4;
                        updateArticleRevision.Custom5 = revision.custom_5;
                        updateArticleRevision.Custom6 = revision.custom_6;
                        updateArticleRevision.Custom7 = revision.custom_7;
                        updateArticleRevision.Custom8 = revision.custom_8;
                        updateArticleRevision.Custom9 = revision.custom_9;
                        updateArticleRevision.Custom10 = revision.custom_10;
                        updateArticleRevision.Custom11 = revision.custom_11;
                        updateArticleRevision.Custom12 = revision.custom_12;
                        updateArticleRevision.Custom13 = revision.custom_13;
                        updateArticleRevision.Custom14 = revision.custom_14;
                        updateArticleRevision.Custom15 = revision.custom_15;
                        updateArticleRevision.Custom16 = revision.custom_16;
                        updateArticleRevision.Custom17 = revision.custom_17;
                        updateArticleRevision.Custom18 = revision.custom_18;
                        updateArticleRevision.Custom19 = revision.custom_19;
                        updateArticleRevision.Custom20 = revision.custom_20;
                        updateArticleRevision.Flag1 = revision.flag_1;
                        updateArticleRevision.Flag2 = revision.flag_2;
                        updateArticleRevision.Flag3 = revision.flag_3;
                        updateArticleRevision.Flag4 = revision.flag_4;
                        updateArticleRevision.Flag5 = revision.flag_5;
                        updateArticleRevision.date_1 = revision.date_1;
                        updateArticleRevision.date_2 = revision.date_2;
                        updateArticleRevision.date_3 = revision.date_3;
                        updateArticleRevision.date_4 = revision.date_4;
                        updateArticleRevision.date_5 = revision.date_5;
                        updateArticleRevision.RevisionFlag1 = revision.rev_flag_1;
                        updateArticleRevision.RevisionFlag2 = revision.rev_flag_2;
                        updateArticleRevision.RevisionFlag3 = revision.rev_flag_3;
                        updateArticleRevision.RevisionFlag4 = revision.rev_flag_4;
                        updateArticleRevision.RevisionFlag5 = revision.rev_flag_5;
                        updateArticleRevision.Cl1 = revision.cl_1;
                        updateArticleRevision.Cl2 = revision.cl_2;
                        updateArticleRevision.Cl3 = revision.cl_3;
                        updateArticleRevision.Cl4 = revision.cl_4;
                        updateArticleRevision.Cl5 = revision.cl_5;
                        updateArticleRevision.CustomBody = revision.custom_body;
                        updateArticleRevision.MetaDescription = revision.meta_description;
                        updateArticleRevision.ContentEditorType1 = revision.content_1_editor_type;
                        updateArticleRevision.ContentEditorType2 = revision.content_2_editor_type;
                        updateArticleRevision.ContentEditorType3 = revision.content_3_editor_type;
                        updateArticleRevision.ContentEditorType4 = revision.content_4_editor_type;
                        updateArticleRevision.ContentEditorType5 = revision.content_5_editor_type;
                        updateArticleRevision.OmnitureCode = revision.omniture_code;
                        updateArticleRevision.CustomSettings = revision.custom_setting;
                        updateArticleRevision.BeforeBody = revision.before_body;
                        updateArticleRevision.BeforeHead = revision.before_head;
                        updateArticleRevision.NoIndexNoFollow = revision.no_index_no_follow;
                        updateArticleRevision.CustomHtmlAttr = revision.custom_html_attr;
                        updateArticleRevision.MetaTitle = revision.meta_title;
                        updateArticleRevision.CanonicalUrl = revision.canonical_url;
                        updateArticleRevision.TagIds = revision.tag_ids;
                        updateArticleRevision.TagContents = revision.tag_contents;

                        //2017-09-19
                        updateArticleRevision.AfterBody = revision.afterbody;
                        updateArticleRevision.HidePrefix = revision.hideprefix;
                        updateArticleRevision.HideSuffix = revision.hidesuffix;
                        //2017-09-19

                        articleRevisionService.Update(updateArticleRevision);

                        returnItem.rstat = "U";

                    }
                    else
                    {
                        ArticleRevision updateArticleRevision = new ArticleRevision();
                        updateArticleRevision = dbContext.ArticleRevisions.Where(s => s.RevisionId == revision.rev_id).FirstOrDefault();

                        updateArticleRevision.RevisionFlag1 = false;
                        updateArticleRevision.RevisionFlag2 = false;
                        updateArticleRevision.RevisionFlag3 = false;
                        updateArticleRevision.RevisionFlag4 = false;
                        updateArticleRevision.RevisionFlag5 = false;

                        articleRevisionService.Update(updateArticleRevision);

                        ArticleRevision insertArticleRevision = new ArticleRevision();
                        insertArticleRevision.RevisedBy = (Guid)revision.revised_by;
                        insertArticleRevision.RevisionName = revision.rev_name;
                        insertArticleRevision.RevisionNote = revision.rev_note;
                        insertArticleRevision.ArticleId = revision.article_id;
                        insertArticleRevision.ClassificationId = revision.clsf_id;
                        insertArticleRevision.Status = Convert.ToByte(revision.status);
                        insertArticleRevision.Startdate = revision.startdate;
                        insertArticleRevision.Enddate = revision.enddate;
                        insertArticleRevision.Order = revision.orderno;
                        insertArticleRevision.LangId = revision.lang_id;
                        insertArticleRevision.NavigationDisplay = Convert.ToByte(revision.navigation_display);
                        insertArticleRevision.NavigationZoneId = revision.navigation_zone_id;
                        insertArticleRevision.MenuText = revision.menu_text;
                        insertArticleRevision.Headline = revision.headline;
                        insertArticleRevision.Summary = revision.summary;
                        insertArticleRevision.Keywords = revision.keywords;
                        insertArticleRevision.ArticleType = revision.article_type;
                        insertArticleRevision.ArticleTypeDetail = revision.article_type_detail;
                        insertArticleRevision.Article1 = revision.article_1;
                        insertArticleRevision.Article2 = revision.article_2;
                        insertArticleRevision.Article3 = revision.article_3;
                        insertArticleRevision.Article4 = revision.article_4;
                        insertArticleRevision.Article5 = revision.article_5;
                        insertArticleRevision.Custom1 = revision.custom_1;
                        insertArticleRevision.Custom2 = revision.custom_2;
                        insertArticleRevision.Custom3 = revision.custom_3;
                        insertArticleRevision.Custom4 = revision.custom_4;
                        insertArticleRevision.Custom5 = revision.custom_5;
                        insertArticleRevision.Custom6 = revision.custom_6;
                        insertArticleRevision.Custom7 = revision.custom_7;
                        insertArticleRevision.Custom8 = revision.custom_8;
                        insertArticleRevision.Custom9 = revision.custom_9;
                        insertArticleRevision.Custom10 = revision.custom_10;
                        insertArticleRevision.Custom11 = revision.custom_11;
                        insertArticleRevision.Custom12 = revision.custom_12;
                        insertArticleRevision.Custom13 = revision.custom_13;
                        insertArticleRevision.Custom14 = revision.custom_14;
                        insertArticleRevision.Custom15 = revision.custom_15;
                        insertArticleRevision.Custom16 = revision.custom_16;
                        insertArticleRevision.Custom17 = revision.custom_17;
                        insertArticleRevision.Custom18 = revision.custom_18;
                        insertArticleRevision.Custom19 = revision.custom_19;
                        insertArticleRevision.Custom20 = revision.custom_20;
                        insertArticleRevision.Flag1 = revision.flag_1;
                        insertArticleRevision.Flag2 = revision.flag_2;
                        insertArticleRevision.Flag3 = revision.flag_3;
                        insertArticleRevision.Flag4 = revision.flag_4;
                        insertArticleRevision.Flag5 = revision.flag_5;
                        insertArticleRevision.date_1 = revision.date_1;
                        insertArticleRevision.date_2 = revision.date_2;
                        insertArticleRevision.date_3 = revision.date_3;
                        insertArticleRevision.date_4 = revision.date_4;
                        insertArticleRevision.date_5 = revision.date_5;
                        insertArticleRevision.RevisionFlag1 = revision.rev_flag_1;
                        insertArticleRevision.RevisionFlag2 = revision.rev_flag_2;
                        insertArticleRevision.RevisionFlag3 = revision.rev_flag_3;
                        insertArticleRevision.RevisionFlag4 = revision.rev_flag_4;
                        insertArticleRevision.RevisionFlag5 = revision.rev_flag_5;
                        insertArticleRevision.Cl1 = revision.cl_1;
                        insertArticleRevision.Cl2 = revision.cl_2;
                        insertArticleRevision.Cl3 = revision.cl_3;
                        insertArticleRevision.Cl4 = revision.cl_4;
                        insertArticleRevision.Cl5 = revision.cl_5;
                        insertArticleRevision.CustomBody = revision.custom_body;
                        insertArticleRevision.MetaDescription = revision.meta_description;
                        insertArticleRevision.ContentEditorType1 = revision.content_1_editor_type;
                        insertArticleRevision.ContentEditorType2 = revision.content_2_editor_type;
                        insertArticleRevision.ContentEditorType3 = revision.content_3_editor_type;
                        insertArticleRevision.ContentEditorType4 = revision.content_4_editor_type;
                        insertArticleRevision.ContentEditorType5 = revision.content_5_editor_type;
                        insertArticleRevision.OmnitureCode = revision.omniture_code;
                        insertArticleRevision.CustomSettings = revision.custom_setting;
                        insertArticleRevision.BeforeBody = revision.before_body;
                        insertArticleRevision.BeforeHead = revision.before_head;
                        insertArticleRevision.NoIndexNoFollow = revision.no_index_no_follow;
                        insertArticleRevision.CustomHtmlAttr = revision.custom_html_attr;
                        insertArticleRevision.MetaTitle = revision.meta_title;
                        insertArticleRevision.CanonicalUrl = revision.canonical_url;
                        insertArticleRevision.TagIds = revision.tag_ids;
                        insertArticleRevision.TagContents = revision.tag_contents;

                        //2017-09-19
                        insertArticleRevision.AfterBody = revision.afterbody;
                        insertArticleRevision.HidePrefix = revision.hideprefix;
                        insertArticleRevision.HideSuffix = revision.hidesuffix;
                        //2017-09-19

                        ArticleRevision getArticleRevId = new ArticleRevision();
                        getArticleRevId = articleRevisionService.Insert(insertArticleRevision);
                        revision.rev_id = getArticleRevId.RevisionId;

                        returnItem.rev_id = getArticleRevId.RevisionId;
                        returnItem.rstat = "N";
                    }
                }

                returnItem.article_id = revision.article_id;
                returnItem.rev_id = revision.rev_id;
                returnList.Add(returnItem);
            }
            else
            {
                returnList.Clear();
                returnList = new List<cms_asp_admin_update_article_revision_Result>();
                returnItem = new cms_asp_admin_update_article_revision_Result();
                returnItem.astat = "D";
                returnItem.rstat = "";
                returnItem.article_id = revision.article_id;
                returnItem.rev_id = revision.rev_id;
                returnList.Add(returnItem);
            }

            return returnList;
        }

        public List<cms_asp_admin_update_article_revision_Result> SaveArticleRevisionOld(cms_article_revision revision)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_article_revision_Result>
            ("dbo.cms_asp_admin_update_article_revision @rev_id={0}," +
            "@rev_name={1}," +
            "@rev_note={2}," +
            "@article_id={3}," +
            "@clsf_id={4}," +
            "@status={5}," +
            "@startdate={6}," +
            "@enddate={7}," +
            "@orderno={8}," +
            "@lang_id={9}," +
            "@navigation_display={10}," +
            "@navigation_zone_id={11}," +
            "@menu_text={12}," +
            "@headline={13}," +
            "@summary={14}," +
            "@keywords={15}," +
            "@article_type={16}," +
            "@article_type_detail={17}," +
            "@article_1={18}," +
            "@article_2={19}," +
            "@article_3={20}," +
            "@article_4={21}," +
            "@article_5={22}," +
            "@custom_1={23}," +
            "@custom_2={24}," +
            "@custom_3={25}," +
            "@custom_4={26}," +
            "@custom_5={27}," +
            "@custom_6={28}," +
            "@custom_7={29}," +
            "@custom_8={30}," +
            "@custom_9={31}," +
            "@custom_10={32}," +
            "@custom_11={33}," +
            "@custom_12={34}," +
            "@custom_13={35}," +
            "@custom_14={36}," +
            "@custom_15={37}," +
            "@custom_16={38}," +
            "@custom_17={39}," +
            "@custom_18={40}," +
            "@custom_19={41}," +
            "@custom_20={42}," +
            "@flag_1={43}," +
            "@flag_2={44}," +
            "@flag_3={45}," +
            "@flag_4={46}," +
            "@flag_5={47}," +
            "@date_1={48}," +
            "@date_2={49}," +
            "@date_3={50}," +
            "@date_4={51}," +
            "@date_5={52}," +
            "@rev_flag_1={53}," +
            "@rev_flag_2={54}," +
            "@rev_flag_3={55}," +
            "@rev_flag_4={56}," +
            "@rev_flag_5={57}," +
            "@cl_1={58}," +
            "@cl_2={59}," +
            "@cl_3={60}," +
            "@cl_4={61}," +
            "@cl_5={62}," +
            "@custom_body={63}," +
            "@revised_by={64}," +
            "@cio={65}," +
            "@meta_description={66}," +
            "@content_1_editor_type={67}," +
            "@content_2_editor_type={68}," +
            "@content_3_editor_type={69}," +
            "@content_4_editor_type={70}," +
            "@content_5_editor_type={71}," +
            "@omniture_code={72}," +
            "@custom_setting={73}",
            revision.rev_id == 0 ? -1 : revision.rev_id,
            revision.rev_name ?? "",
            revision.rev_note ?? "",
            revision.article_id == 0 ? -1 : revision.article_id,
            revision.clsf_id,
            revision.status,
            revision.startdate,
            revision.enddate,
            revision.orderno,
            revision.lang_id,
            revision.navigation_display,
            revision.navigation_zone_id,
            revision.menu_text ?? "",
            revision.headline,
            revision.summary ?? "",
            revision.keywords ?? "",
            revision.article_type,
            revision.article_type_detail ?? "",
            HttpUtility.HtmlEncode(revision.article_1 ?? ""),
            HttpUtility.HtmlEncode(revision.article_2 ?? ""),
            HttpUtility.HtmlEncode(revision.article_3 ?? ""),
            HttpUtility.HtmlEncode(revision.article_4 ?? ""),
            HttpUtility.HtmlEncode(revision.article_5) ?? "",
            revision.custom_1,
            revision.custom_2,
            revision.custom_3,
            revision.custom_4,
            revision.custom_5,
            revision.custom_6,
            revision.custom_7,
            revision.custom_8,
            revision.custom_9,
            revision.custom_10,
            revision.custom_11,
            revision.custom_12,
            revision.custom_13,
            revision.custom_14,
            revision.custom_15,
            revision.custom_16,
            revision.custom_17,
            revision.custom_18,
            revision.custom_19,
            revision.custom_20,
            revision.flag_1,
            revision.flag_2,
            revision.flag_3,
            revision.flag_4,
            revision.flag_5,
            revision.date_1,
            revision.date_2,
            revision.date_3,
            revision.date_4,
            revision.date_5,
            revision.rev_flag_1,
            revision.rev_flag_2,
            revision.rev_flag_3,
            revision.rev_flag_4,
            revision.rev_flag_5,
            revision.cl_1,
            revision.cl_2,
            revision.cl_3,
            revision.cl_4,
            revision.cl_5,
            revision.custom_body ?? "",
            revision.revised_by,
            revision.cio,
            revision.meta_description ?? "",
            revision.content_1_editor_type ?? "H",
            revision.content_2_editor_type ?? "H",
            revision.content_3_editor_type ?? "H",
            revision.content_4_editor_type ?? "H",
            revision.content_5_editor_type ?? "H",
            revision.omniture_code ?? "",
            revision.custom_setting ?? ";")
            .ToList();
        }

        public List<string> DiscardArticleRevision(long RevisionID, object ApprovalID)
        {
            List<string> returnList = new List<string>();

            CmsDbContext dbContext = new CmsDbContext();

            ArticleRevisionRepository arr = new ArticleRevisionRepository();
            ArticleRevisionService articleRevisionService = new ArticleRevisionService(arr);

            ArticleRevision getArticleRev = new ArticleRevision();
            getArticleRev = dbContext.ArticleRevisions.Where(a => a.RevisionId == RevisionID && (a.RevisionStatus == "N" || a.RevisionStatus == "A" || a.RevisionStatus == "W")).FirstOrDefault();

            if (getArticleRev != null)
            {
                ArticleRevision updateArticleRev = new ArticleRevision();
                updateArticleRev = dbContext.ArticleRevisions.Where(a => a.RevisionId == RevisionID).FirstOrDefault();

                updateArticleRev.RevisionStatus = "X";
                updateArticleRev.Approved = DateTime.Now;
                updateArticleRev.ApprovedBy = (Guid)ApprovalID;
                //articleRevisionService.Update(updateArticleRev);
                dbContext.Entry(updateArticleRev).State = EntityState.Modified;
                dbContext.SaveChanges();

                returnList.Add("OK");
            }
            else
            {
                returnList.Add("STATUS");
            }

            return returnList;

            //return this.Database.SqlQuery<string>
            //("dbo.cms_asp_admin_update_article_revision_status @rev_id={0},@revision_status={1},@approval_id={2}",
            //RevisionID, "X", ApprovalID)
            //.ToList();
        }

        // Entity Framework'e çevrilmedi
        public List<cms_asp_approval_approve_article_revision_Result> ApproveArticleRevision(long RevisionID, int ApproveLevel, object publisherID, int PublisherLevel, string cio)
        {
            return this.Database.SqlQuery<cms_asp_approval_approve_article_revision_Result>
           ("dbo.cms_asp_approval_approve_article_revision @rev_id={0},@approve_level={1},@publisher_id={2},@publisher_level={3},@cio={4}",
           RevisionID, ApproveLevel, publisherID, PublisherLevel, cio)
           .ToList();
        }

        public List<cms_asp_admin_delete_article_Result> DeleteArticle(int ArticleID, long RevisionID, object publisherID, int PublisherLevel)
        {
            List<cms_asp_admin_delete_article_Result> returnList = new List<cms_asp_admin_delete_article_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            ArticleRevisionRepository arr = new ArticleRevisionRepository();
            ArticleRevisionService articleRevisionService = new ArticleRevisionService(arr);

            ArticleZoneRevisionRepository azr = new ArticleZoneRevisionRepository();
            ArticleZoneRevisionService articleZoneRevisionService = new ArticleZoneRevisionService(azr);

            Article getArticle = new Article();
            getArticle = dbContext.Articles.Where(a => a.Id == ArticleID && a.Status != 2).FirstOrDefault();

            string headline = "";
            long newRevID = 0;

            if (getArticle != null)
            {
                headline = dbContext.Articles.Where(a => a.Id == ArticleID).FirstOrDefault().Headline;

                ArticleRevision insertArticleRev = new ArticleRevision();
                insertArticleRev.ArticleId = ArticleID;
                insertArticleRev.RevisionName = "Delete Request";
                insertArticleRev.Headline = headline;
                insertArticleRev.Startdate = DateTime.Now;
                insertArticleRev.Status = 2;
                insertArticleRev.RevisedBy = (Guid)publisherID;
                //insertArticleRev.CreatedBy = (Guid)publisherID;

                newRevID = articleRevisionService.Insert(insertArticleRev).RevisionId;

                ArticleZoneRevision getArticleZoneRev = new ArticleZoneRevision();
                getArticleZoneRev = dbContext.ArticleZoneRevisions.Where(a => a.ArticleID == ArticleID && a.RevID == RevisionID).FirstOrDefault();

                ArticleZoneRevision insertArticleZoneRev = new ArticleZoneRevision();
                insertArticleZoneRev.RevID = newRevID;
                insertArticleZoneRev.ArticleID = getArticleZoneRev.ArticleID;
                insertArticleZoneRev.ZoneID = getArticleZoneRev.ZoneID;

                articleZoneRevisionService.Insert(insertArticleZoneRev);

                cms_asp_admin_delete_article_Result returnItem = new cms_asp_admin_delete_article_Result();
                returnItem.rCode = "0";
                returnItem.found_name = "";
                returnItem.rev_id = Convert.ToInt32(newRevID);
                returnList.Add(returnItem);
            }
            else
            {
                cms_asp_admin_delete_article_Result returnItem = new cms_asp_admin_delete_article_Result();
                returnItem.rCode = "1";
                returnItem.found_name = "";
                returnItem.rev_id = 0;
                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_delete_article_Result>
            //("dbo.cms_asp_admin_delete_article @article_id={0},@rev_id={1},@publisher_id={2},@publisher_level={3}",
            //ArticleID, RevisionID, publisherID, PublisherLevel)
            //.ToList();
        }

        public List<long> SelectArticleLastRevision(int ArticleID)
        {
            List<long> returnList = new List<long>();

            CmsDbContext dbContext = new CmsDbContext();

            ArticleRevision getArticleRev = new ArticleRevision();
            getArticleRev = dbContext.ArticleRevisions.Where(a => a.ArticleId == ArticleID && (a.RevisionStatus == "L" || a.RevisionStatus == "E")).OrderByDescending(od => od.RevisionStatus).ThenByDescending(o => o.RevisionDate).ToList().FirstOrDefault();

            if (getArticleRev != null)
            {
                returnList.Add(getArticleRev.RevisionId);
            }
            else
            {
                getArticleRev = new ArticleRevision();
                getArticleRev = dbContext.ArticleRevisions.Where(a => a.ArticleId == ArticleID).OrderBy(o => o.RevisionStatus).ThenByDescending(o => o.RevisionDate).ToList().FirstOrDefault();
                returnList.Add(getArticleRev.RevisionId);
            }

            return returnList;

            //return this.Database.SqlQuery<long>
            //("dbo.cms_asp_admin_select_article_last_revision @article_id={0}",
            //ArticleID)
            //.ToList();
        }

        public List<cms_asp_admin_select_article_revision_list_Result> SelectArticleRevisions(int ArticleID)
        {
            List<cms_asp_admin_select_article_revision_list_Result> returnList = new List<cms_asp_admin_select_article_revision_list_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleRevision> listArticleRev = new List<ArticleRevision>();
            listArticleRev = dbContext.ArticleRevisions.Where(a => a.ArticleId == ArticleID && a.RevisionStatus != "X").OrderByDescending(od => od.RevisionDate).Take(50).ToList();

            foreach (ArticleRevision articleRev in listArticleRev)
            {
                cms_asp_admin_select_article_revision_list_Result returnItem = new cms_asp_admin_select_article_revision_list_Result();

                string revisedName = "";
                string approvalName = "";

                vAspNetMembershipUser getMember = new vAspNetMembershipUser();
                getMember = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == articleRev.RevisedBy).FirstOrDefault();
                if (getMember != null)
                {
                    if (!string.IsNullOrEmpty(getMember.UserName))
                    {
                        revisedName = getMember.UserName;
                    }
                }
                getMember = new vAspNetMembershipUser();
                getMember = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == articleRev.ApprovedBy).FirstOrDefault();
                if (getMember != null)
                {
                    if (!string.IsNullOrEmpty(getMember.UserName))
                    {
                        approvalName = getMember.UserName;
                    }
                }
                returnItem.status = articleRev.Status;
                returnItem.rev_id = articleRev.RevisionId;
                returnItem.rev_date = articleRev.RevisionDate;
                returnItem.revision_status = articleRev.RevisionStatus;
                returnItem.revised_name = revisedName;
                returnItem.approval_name = approvalName;
                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_select_article_revision_list_Result>
            //("dbo.cms_asp_admin_select_article_revision_list @article_id={0}",
            //ArticleID)
            //.ToList();
        }

        public List<cms_article_revision> SelectArticleRevisionDetails(long RevisionID)
        {
            List<cms_article_revision> returnList = new List<cms_article_revision>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleRevision> listArticleRev = new List<ArticleRevision>();
            listArticleRev = dbContext.ArticleRevisions.Where(a => a.RevisionId == RevisionID).ToList();

            foreach (ArticleRevision articleRev in listArticleRev)
            {
                cms_article_revision returnItem = new cms_article_revision();

                string revisedName = "", approvalName = "", publisherName = "";

                Article getArticle = new Article();
                getArticle = dbContext.Articles.Where(a => a.Id == articleRev.ArticleId).FirstOrDefault();

                vAspNetMembershipUser getMembershipUser = new vAspNetMembershipUser();
                getMembershipUser = dbContext.vAspNetMembershipUsers.Where(m => m.UserId == articleRev.RevisedBy).FirstOrDefault();
                if (getMembershipUser != null)
                {
                    if (!string.IsNullOrEmpty(getMembershipUser.UserName))
                    {
                        revisedName = getMembershipUser.UserName;
                    }
                }
                getMembershipUser = new vAspNetMembershipUser();
                getMembershipUser = dbContext.vAspNetMembershipUsers.Where(m => m.UserId == articleRev.ApprovedBy).FirstOrDefault();
                if (getMembershipUser != null)
                {
                    if (!string.IsNullOrEmpty(getMembershipUser.UserName))
                    {
                        approvalName = getMembershipUser.UserName;
                    }
                }
                getMembershipUser = new vAspNetMembershipUser();
                getMembershipUser = dbContext.vAspNetMembershipUsers.Where(m => m.UserId == articleRev.Article.CreatedBy).FirstOrDefault();
                if (getMembershipUser != null)
                {
                    if (!string.IsNullOrEmpty(getMembershipUser.UserName))
                    {
                        publisherName = getMembershipUser.UserName;
                    }
                }

                returnItem.rev_id = articleRev.RevisionId;
                returnItem.rev_date = articleRev.RevisionDate;
                returnItem.revision_status = articleRev.RevisionStatus;
                returnItem.revised_by = articleRev.RevisedBy;
                returnItem.approval_date = articleRev.Approved;
                returnItem.approval_id = articleRev.ApprovedBy;
                returnItem.article_id = articleRev.ArticleId;
                returnItem.clsf_id = articleRev.ClassificationId;
                returnItem.status = articleRev.Status;
                returnItem.startdate = Convert.ToDateTime(articleRev.Startdate);
                returnItem.enddate = articleRev.Enddate;
                returnItem.orderno = articleRev.Order;
                returnItem.lang_id = articleRev.LangId;
                returnItem.headline = articleRev.Headline;
                returnItem.summary = articleRev.Summary;
                returnItem.keywords = articleRev.Keywords;
                returnItem.article_type = articleRev.ArticleType;
                returnItem.article_type_detail = articleRev.ArticleTypeDetail;
                returnItem.article_1 = articleRev.Article1;
                returnItem.article_2 = articleRev.Article2;
                returnItem.article_3 = articleRev.Article3;
                returnItem.article_4 = articleRev.Article4;
                returnItem.article_5 = articleRev.Article5;
                returnItem.custom_1 = articleRev.Custom1;
                returnItem.custom_2 = articleRev.Custom2;
                returnItem.custom_3 = articleRev.Custom3;
                returnItem.custom_4 = articleRev.Custom4;
                returnItem.custom_5 = articleRev.Custom5;
                returnItem.custom_6 = articleRev.Custom6;
                returnItem.custom_7 = articleRev.Custom7;
                returnItem.custom_8 = articleRev.Custom8;
                returnItem.custom_9 = articleRev.Custom9;
                returnItem.custom_10 = articleRev.Custom10;
                returnItem.custom_11 = articleRev.Custom11;
                returnItem.custom_12 = articleRev.Custom12;
                returnItem.custom_13 = articleRev.Custom13;
                returnItem.custom_14 = articleRev.Custom14;
                returnItem.custom_15 = articleRev.Custom15;
                returnItem.custom_16 = articleRev.Custom16;
                returnItem.custom_17 = articleRev.Custom17;
                returnItem.custom_18 = articleRev.Custom18;
                returnItem.custom_19 = articleRev.Custom19;
                returnItem.custom_20 = articleRev.Custom20;
                returnItem.flag_1 = articleRev.Flag1;
                returnItem.flag_2 = articleRev.Flag2;
                returnItem.flag_3 = articleRev.Flag3;
                returnItem.flag_4 = articleRev.Flag4;
                returnItem.flag_5 = articleRev.Flag5;
                returnItem.date_1 = articleRev.date_1;
                returnItem.date_2 = articleRev.date_2;
                returnItem.date_3 = articleRev.date_3;
                returnItem.date_4 = articleRev.date_4;
                returnItem.date_5 = articleRev.date_5;
                returnItem.rev_flag_1 = articleRev.RevisionFlag1;
                returnItem.rev_flag_2 = articleRev.RevisionFlag2;
                returnItem.rev_flag_3 = articleRev.RevisionFlag3;
                returnItem.rev_flag_4 = articleRev.RevisionFlag4;
                returnItem.rev_flag_5 = articleRev.RevisionFlag5;
                returnItem.cl_1 = articleRev.Cl1;
                returnItem.cl_2 = articleRev.Cl2;
                returnItem.cl_3 = articleRev.Cl3;
                returnItem.cl_4 = articleRev.Cl4;
                returnItem.cl_5 = articleRev.Cl5;
                returnItem.custom_body = articleRev.CustomBody;
                returnItem.created_by = getArticle.CreatedBy;
                returnItem.created = getArticle.Created;
                returnItem.rev_name = articleRev.RevisionName;
                returnItem.rev_note = articleRev.RevisionNote;
                returnItem.navigation_display = articleRev.NavigationDisplay;
                returnItem.navigation_zone_id = articleRev.NavigationZoneId;
                returnItem.menu_text = articleRev.MenuText;
                returnItem.revised_by = revisedName;
                returnItem.meta_description = articleRev.MetaDescription;
                returnItem.content_1_editor_type = articleRev.ContentEditorType1;
                returnItem.content_2_editor_type = articleRev.ContentEditorType2;
                returnItem.content_3_editor_type = articleRev.ContentEditorType3;
                returnItem.content_4_editor_type = articleRev.ContentEditorType4;
                returnItem.content_5_editor_type = articleRev.ContentEditorType5;
                returnItem.omniture_code = articleRev.OmnitureCode;
                returnItem.custom_setting = articleRev.CustomSettings;
                returnItem.before_body = articleRev.BeforeBody;
                returnItem.before_head = articleRev.BeforeHead;
                returnItem.no_index_no_follow = articleRev.NoIndexNoFollow;
                returnItem.canonical_url = articleRev.CanonicalUrl;
                returnItem.meta_title = articleRev.MetaTitle;
                returnItem.custom_html_attr = articleRev.CustomHtmlAttr;
                returnItem.tag_ids = articleRev.TagIds;
                returnItem.tag_contents = articleRev.TagContents;
                returnItem.afterbody = articleRev.AfterBody;
                returnItem.hideprefix = articleRev.HidePrefix;
                returnItem.hidesuffix = articleRev.HideSuffix;
                returnList.Add(returnItem);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_article_revision>
            //("dbo.cms_asp_admin_select_article_revision_details @rev_id={0}",
            //RevisionID)
            //.ToList();
        }

        public List<cms_asp_admin_select_article_revision_details_Result> SelectArticleRevisionDetails2(long RevisionID)
        {
            List<cms_asp_admin_select_article_revision_details_Result> returnList = new List<cms_asp_admin_select_article_revision_details_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleRevision> listArticleRev = new List<ArticleRevision>();
            listArticleRev = dbContext.ArticleRevisions.Where(a => a.RevisionId == RevisionID).ToList();

            foreach (ArticleRevision articleRev in listArticleRev)
            {
                cms_asp_admin_select_article_revision_details_Result returnItem = new cms_asp_admin_select_article_revision_details_Result();

                string revisedName = "", approvalName = "", publisherName = "";

                Article getArticle = new Article();
                getArticle = dbContext.Articles.Where(a => a.Id == articleRev.ArticleId).FirstOrDefault();

                vAspNetMembershipUser getMembershipUser = new vAspNetMembershipUser();
                getMembershipUser = dbContext.vAspNetMembershipUsers.Where(m => m.UserId == articleRev.RevisedBy).FirstOrDefault();
                if (getMembershipUser != null)
                {
                    if (!string.IsNullOrEmpty(getMembershipUser.UserName))
                    {
                        revisedName = getMembershipUser.UserName;
                    }
                }
                getMembershipUser = new vAspNetMembershipUser();
                getMembershipUser = dbContext.vAspNetMembershipUsers.Where(m => m.UserId == articleRev.ApprovedBy).FirstOrDefault();
                if (getMembershipUser != null)
                {
                    if (!string.IsNullOrEmpty(getMembershipUser.UserName))
                    {
                        approvalName = getMembershipUser.UserName;
                    }
                }
                getMembershipUser = new vAspNetMembershipUser();
                getMembershipUser = dbContext.vAspNetMembershipUsers.Where(m => m.UserId == articleRev.Article.CreatedBy).FirstOrDefault();
                if (getMembershipUser != null)
                {
                    if (!string.IsNullOrEmpty(getMembershipUser.UserName))
                    {
                        publisherName = getMembershipUser.UserName;
                    }
                }

                returnItem.rev_id = articleRev.RevisionId;
                returnItem.rev_date = articleRev.RevisionDate;
                returnItem.revision_status = articleRev.RevisionStatus;
                returnItem.revised_by = articleRev.RevisedBy;
                returnItem.approval_date = articleRev.Approved;
                returnItem.approval_id = articleRev.ApprovedBy;
                returnItem.current_status = getArticle.Status;
                returnItem.article_id = articleRev.ArticleId;
                returnItem.clsf_id = articleRev.ClassificationId;
                returnItem.status = articleRev.Status;
                returnItem.startdate = Convert.ToDateTime(articleRev.Startdate);
                returnItem.enddate = articleRev.Enddate;
                returnItem.orderno = articleRev.Order;
                returnItem.lang_id = articleRev.LangId;
                returnItem.headline = articleRev.Headline;
                returnItem.summary = articleRev.Summary;
                returnItem.keywords = articleRev.Keywords;
                returnItem.article_type = articleRev.ArticleType;
                returnItem.article_type_detail = articleRev.ArticleTypeDetail;
                returnItem.article_1 = articleRev.Article1;
                returnItem.article_2 = articleRev.Article2;
                returnItem.article_3 = articleRev.Article3;
                returnItem.article_4 = articleRev.Article4;
                returnItem.article_5 = articleRev.Article5;
                returnItem.custom_1 = articleRev.Custom1;
                returnItem.custom_2 = articleRev.Custom2;
                returnItem.custom_3 = articleRev.Custom3;
                returnItem.custom_4 = articleRev.Custom4;
                returnItem.custom_5 = articleRev.Custom5;
                returnItem.custom_6 = articleRev.Custom6;
                returnItem.custom_7 = articleRev.Custom7;
                returnItem.custom_8 = articleRev.Custom8;
                returnItem.custom_9 = articleRev.Custom9;
                returnItem.custom_10 = articleRev.Custom10;
                returnItem.custom_11 = articleRev.Custom11;
                returnItem.custom_12 = articleRev.Custom12;
                returnItem.custom_13 = articleRev.Custom13;
                returnItem.custom_14 = articleRev.Custom14;
                returnItem.custom_15 = articleRev.Custom15;
                returnItem.custom_16 = articleRev.Custom16;
                returnItem.custom_17 = articleRev.Custom17;
                returnItem.custom_18 = articleRev.Custom18;
                returnItem.custom_19 = articleRev.Custom19;
                returnItem.custom_20 = articleRev.Custom20;
                returnItem.flag_1 = articleRev.Flag1;
                returnItem.flag_2 = articleRev.Flag2;
                returnItem.flag_3 = articleRev.Flag3;
                returnItem.flag_4 = articleRev.Flag4;
                returnItem.flag_5 = articleRev.Flag5;
                returnItem.date_1 = articleRev.date_1;
                returnItem.date_2 = articleRev.date_2;
                returnItem.date_3 = articleRev.date_3;
                returnItem.date_4 = articleRev.date_4;
                returnItem.date_5 = articleRev.date_5;
                returnItem.rev_flag_1 = articleRev.RevisionFlag1;
                returnItem.rev_flag_2 = articleRev.RevisionFlag2;
                returnItem.rev_flag_3 = articleRev.RevisionFlag3;
                returnItem.rev_flag_4 = articleRev.RevisionFlag4;
                returnItem.rev_flag_5 = articleRev.RevisionFlag5;
                returnItem.cl_1 = articleRev.Cl1;
                returnItem.cl_2 = articleRev.Cl2;
                returnItem.cl_3 = articleRev.Cl3;
                returnItem.cl_4 = articleRev.Cl4;
                returnItem.cl_5 = articleRev.Cl5;
                returnItem.custom_body = articleRev.CustomBody;
                returnItem.publisher_id = getArticle.CreatedBy;
                returnItem.created = getArticle.Created;
                returnItem.updated = getArticle.Updated;
                returnItem.rev_name = articleRev.RevisionName;
                returnItem.rev_note = articleRev.RevisionNote;
                returnItem.navigation_display = articleRev.NavigationDisplay;
                returnItem.navigation_zone_id = articleRev.NavigationZoneId;
                returnItem.menu_text = articleRev.MenuText;
                returnItem.revised_by = revisedName;
                returnItem.meta_description = articleRev.MetaDescription;
                returnItem.content_1_editor_type = articleRev.ContentEditorType1;
                returnItem.content_2_editor_type = articleRev.ContentEditorType2;
                returnItem.content_3_editor_type = articleRev.ContentEditorType3;
                returnItem.content_4_editor_type = articleRev.ContentEditorType4;
                returnItem.content_5_editor_type = articleRev.ContentEditorType5;
                returnItem.omniture_code = articleRev.OmnitureCode;
                returnItem.custom_setting = articleRev.CustomSettings;
                returnList.Add(returnItem);
            }

            return returnList;
            //return this.Database.SqlQuery<cms_asp_admin_select_article_revision_details_Result>
            //("dbo.cms_asp_admin_select_article_revision_details @rev_id={0}",
            //RevisionID)
            //.ToList();
        }

        public int DeleteArticleZonesWithRevision(long RevisionID, int ArticleID)
        {
            ArticleZoneRevisionRepository azrr = new ArticleZoneRevisionRepository();
            ArticleZoneRevisionService articleZoneRevisionService = new ArticleZoneRevisionService(azrr);

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleZoneRevision> listArticleZoneRev = new List<ArticleZoneRevision>();
            listArticleZoneRev = dbContext.ArticleZoneRevisions.Where(a => a.ArticleID == ArticleID && a.RevID == RevisionID).ToList();

            foreach (ArticleZoneRevision articleZoneRev in listArticleZoneRev)
            {
                ArticleZoneRevision deleteArticleZoneRev = new ArticleZoneRevision();
                deleteArticleZoneRev = articleZoneRev;
                articleZoneRevisionService.Delete(deleteArticleZoneRev);
            }

            int returnValue = 0;
            if (listArticleZoneRev.Count > 0)
            {
                returnValue = listArticleZoneRev.Count;
            }

            return returnValue;

            //return this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_admin_delete_article_zones_with_revision @rev_id={0},@article_id={1}",
            //RevisionID, ArticleID);
        }

        public int DeleteArticleRelationsWithRevision(long RevisionID, int ArticleID)
        {
            ArticleRelationRevisionRepository arrr = new ArticleRelationRevisionRepository();
            ArticleRelationRevisionService articleRelationService = new ArticleRelationRevisionService(arrr);

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleRelationRevision> listArticleRelationRev = new List<ArticleRelationRevision>();
            listArticleRelationRev = dbContext.ArticleRelationRevisions.Where(a => a.ArticleID == ArticleID && a.RevisionID == RevisionID).ToList();

            foreach (ArticleRelationRevision articleRelationRev in listArticleRelationRev)
            {
                ArticleRelationRevision deleteArticleRelationRev = new ArticleRelationRevision();
                deleteArticleRelationRev = articleRelationRev;
                articleRelationService.Delete(deleteArticleRelationRev);
            }

            int returnValue = 0;
            if (listArticleRelationRev.Count > 0)
            {
                returnValue = listArticleRelationRev.Count;
            }

            return returnValue;

            //return this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_admin_delete_article_relations_with_revision @rev_id={0},@article_id={1}",
            //RevisionID, ArticleID);
        }

        public int DeleteArticleLanguageRelationsWithRevision(int zone_id, int ArticleID)
        {

            LanguageRelationRevisionRepository lrrr = new LanguageRelationRevisionRepository();
            LanguageRelationRevisionService languageRRService = new LanguageRelationRevisionService(lrrr);

            CmsDbContext dbContext = new CmsDbContext();

            List<LanguageRelationRevision> listLanguageRelationRev = new List<LanguageRelationRevision>();
            var lr_id = dbContext.LanguageRelationRevisions.FirstOrDefault(l => l.ArticleID == ArticleID && l.ZoneID == zone_id);

            if (lr_id != null)
            {
                long languageRelationId = Convert.ToInt64(lr_id.LanguageRelationID);
                listLanguageRelationRev = dbContext.LanguageRelationRevisions.Where(w => w.ArticleID != ArticleID && w.LanguageRelationID == languageRelationId).ToList();
            }

            foreach (LanguageRelationRevision languageRelationRev in listLanguageRelationRev)
            {
                LanguageRelationRevision deleteLanguageRelationRev = new LanguageRelationRevision();
                deleteLanguageRelationRev = languageRelationRev;
                languageRRService.Delete(deleteLanguageRelationRev);
            }

            int returnValue = 0;
            if (listLanguageRelationRev.Count > 0)
            {
                returnValue = listLanguageRelationRev.Count;
            }

            return returnValue;

            //return this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_admin_delete_article_language_relations_with_revision @rev_id={0},@article_id={1}",
            //RevisionID, ArticleID);
        }

        public void DeleteArticleCache(int ArticleID)
        {
            ArticleCacheRepository acp = new ArticleCacheRepository();
            ArticleCacheService articleCacheService = new ArticleCacheService(acp);

            CmsDbContext dbContext = new CmsDbContext();

            List<ArticleCache> listArticleCache = new List<ArticleCache>();
            listArticleCache = dbContext.ArticleCaches.Where(a => a.ArticleID == ArticleID).ToList();

            foreach (ArticleCache articleCache in listArticleCache)
            {
                ArticleCache deleteArticleCache = new ArticleCache();
                deleteArticleCache = articleCache;
                articleCacheService.Delete(deleteArticleCache);
            }

            //this.Database.ExecuteSqlCommand("dbo.cms_asp_admin_delete_article_cache @article_id={0}", ArticleID);
        }

        // Entitity Framework'e çevrilmedi
        public void InsertArticleLanguageRelationsWithRevision(long RevisionID, int ZoneID, int ArticleID, int RelatedZoneID, int RelatedArticleID, int? PoolID)
        {
            this.Database.ExecuteSqlCommand
               ("dbo.cms_asp_admin_insert_article_language_relations_with_revision @rev_id={0},@zone_id={1},@article_id={2},@related_zone_id={3},@related_article_id={4},@pool_id={5}",
               RevisionID, ZoneID, ArticleID, RelatedZoneID, RelatedArticleID, PoolID);
        }

        public void InsertArticleZonesWithRevision(long RevisionID, int ZoneID, int ArticleID, int Order, string Alias, bool isAliasProtected, bool isPage)
        {
            ArticleZoneRepository azr = new ArticleZoneRepository();
            ArticleZoneService articleZoneService = new ArticleZoneService(azr);

            ArticleZoneRevisionRepository azrr = new ArticleZoneRevisionRepository();
            ArticleZoneRevisionService articleZoneRevisionService = new ArticleZoneRevisionService(azrr);

            CmsDbContext dbContext = new CmsDbContext();

            ArticleZoneRevision getArticleZoneRev = new ArticleZoneRevision();
            getArticleZoneRev = dbContext.ArticleZoneRevisions.Where(a => a.RevID == RevisionID && a.ArticleID == ArticleID && a.ZoneID == ZoneID).FirstOrDefault();

            ArticleZone getArticleZone = new ArticleZone();
            getArticleZone = dbContext.ArticleZones.Where(a => a.ArticleID == ArticleID).FirstOrDefault();

            if (getArticleZoneRev == null)
            {
                ArticleZoneRevision insertArticleZoneRev = new ArticleZoneRevision();
                insertArticleZoneRev.RevID = RevisionID;
                insertArticleZoneRev.ArticleID = ArticleID;
                insertArticleZoneRev.ZoneID = ZoneID;
                insertArticleZoneRev.AzOrder = Order;
                insertArticleZoneRev.AzAlias = Alias;
                insertArticleZoneRev.IsAliasProtected = isAliasProtected;
                insertArticleZoneRev.IsPage = isPage;
                articleZoneRevisionService.Insert(insertArticleZoneRev);
            }

            if (getArticleZone == null)
            {
                ArticleZone insertArticleZone = new ArticleZone();
                insertArticleZone.ArticleID = ArticleID;
                insertArticleZone.ZoneID = ZoneID;
                insertArticleZone.AzOrder = Order;
                insertArticleZone.AzAlias = Alias;
                insertArticleZone.IsAliasProtected = isAliasProtected;
                insertArticleZone.IsPage = isPage;
                articleZoneService.Insert(insertArticleZone);
            }

            //this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_admin_insert_article_zones_with_revision @rev_id={0},@article_id={1},@zone_id={2},@az_order={3},@az_alias={4}",
            //RevisionID, ArticleID, ZoneID, Order, Alias);
        }

        public void InsertArticleZonesWithRevision(long RevisionID, int ZoneID, int ArticleID, int Order, string Alias)
        {
            ArticleZoneRepository azr = new ArticleZoneRepository();
            ArticleZoneService articleZoneService = new ArticleZoneService(azr);

            ArticleZoneRevisionRepository azrr = new ArticleZoneRevisionRepository();
            ArticleZoneRevisionService articleZoneRevisionService = new ArticleZoneRevisionService(azrr);

            CmsDbContext dbContext = new CmsDbContext();

            ArticleZoneRevision getArticleZoneRev = new ArticleZoneRevision();
            getArticleZoneRev = dbContext.ArticleZoneRevisions.Where(a => a.RevID == RevisionID && a.ArticleID == ArticleID && a.ZoneID == ZoneID).FirstOrDefault();

            ArticleZone getArticleZone = new ArticleZone();
            getArticleZone = dbContext.ArticleZones.Where(a => a.ArticleID == ArticleID).FirstOrDefault();

            if (getArticleZoneRev == null)
            {
                ArticleZoneRevision insertArticleZoneRev = new ArticleZoneRevision();
                insertArticleZoneRev.RevID = RevisionID;
                insertArticleZoneRev.ArticleID = ArticleID;
                insertArticleZoneRev.ZoneID = ZoneID;
                insertArticleZoneRev.AzOrder = Order;
                insertArticleZoneRev.AzAlias = Alias;
                articleZoneRevisionService.Insert(insertArticleZoneRev);
            }

            if (getArticleZone == null)
            {
                ArticleZone insertArticleZone = new ArticleZone();
                insertArticleZone.ArticleID = ArticleID;
                insertArticleZone.ZoneID = ZoneID;
                insertArticleZone.AzOrder = Order;
                insertArticleZone.AzAlias = Alias;
                articleZoneService.Insert(insertArticleZone);
            }

            //this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_admin_insert_article_zones_with_revision @rev_id={0},@article_id={1},@zone_id={2},@az_order={3},@az_alias={4}",
            //RevisionID, ArticleID, ZoneID, Order, Alias);
        }

        public void InsertArticleRelationsWithRevision(long RevisionID, int ArticleID, int RelatedZoneID, int RelatedArticleID)
        {
            ArticleRelationRevisionRepository arrr = new ArticleRelationRevisionRepository();
            ArticleRelationRevisionService articleRelationRevisionService = new ArticleRelationRevisionService(arrr);

            CmsDbContext dbContext = new CmsDbContext();

            ArticleRelationRevision getArticleRelationRev = new ArticleRelationRevision();
            getArticleRelationRev = dbContext.ArticleRelationRevisions.Where(a => a.RevisionID == RevisionID && a.ArticleID == ArticleID && a.RelatedZoneID == RelatedZoneID && a.RelatedArticleID == RelatedArticleID).FirstOrDefault();

            if (getArticleRelationRev == null)
            {
                ArticleRelationRevision insertArticleRelationRev = new ArticleRelationRevision();
                insertArticleRelationRev.RevisionID = RevisionID;
                insertArticleRelationRev.ArticleID = ArticleID;
                insertArticleRelationRev.RelatedZoneID = RelatedZoneID;
                insertArticleRelationRev.RelatedArticleID = RelatedArticleID;
                articleRelationRevisionService.Insert(insertArticleRelationRev);
            }

            //this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_admin_insert_article_relations_with_revision @rev_id={0},@article_id={1},@related_zone_id={2},@related_article_id={3}",
            //RevisionID, ArticleID, RelatedZoneID, RelatedArticleID);
        }

        public void InsertArticleCache(int ZoneID, int ArticleID)
        {
            ArticleCacheRepository acr = new ArticleCacheRepository();
            ArticleCacheService articleCacheService = new ArticleCacheService(acr);

            CmsDbContext dbContext = new CmsDbContext();

            ArticleCache getArticleCache = new ArticleCache();
            getArticleCache = dbContext.ArticleCaches.Where(a => a.ZoneID == ZoneID && a.ArticleID == ArticleID).FirstOrDefault();

            if (getArticleCache == null)
            {
                ArticleCache insertArticleCache = new ArticleCache();
                insertArticleCache.ArticleID = ArticleID;
                insertArticleCache.ZoneID = ZoneID;
                insertArticleCache.Created = DateTime.Now;
                articleCacheService.Insert(insertArticleCache);
            }

            //this.Database.ExecuteSqlCommand
            //("dbo.cms_asp_insert_article_cache @zone_id={0},@article_id={1}",
            //ZoneID, ArticleID);
        }
    }
}