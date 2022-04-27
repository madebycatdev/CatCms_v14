using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class RssFeedDbContext: BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_rss_channels> RssChannels { get; set; }
        public DbSet<EuroCMS.Admin.entity.cms_rss_content> RssContents { get; set; }

        public RssFeedDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_rss_channels>()
                .Map(m => m.ToTable("cms_sitemaps"));

            modelBuilder.Entity<EuroCMS.Admin.entity.cms_rss_content>()
                .Map(m => m.ToTable("cms_rss_content"));

             base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_rss_channels_Result> SelectRssChannels(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_rss_channels_Result>
                ("dbo.cms_asp_select_rss_channels @group_id={0},@site_id=0,@zone_group_id=0,@zone_id=0",
                GroupID == null ? -1 : GroupID)
                .ToList();
        }

        public List<cms_asp_select_rss_channel_details_Result> SelectRssChannel(int ChannelID)
        {
            return this.Database.SqlQuery<cms_asp_select_rss_channel_details_Result>
                ("dbo.cms_asp_select_rss_channel_details @channel_id={0}",
                ChannelID)
                .ToList();
        }

        public List<cms_asp_select_rss_channel_contents_Result> SelectRssChannelContent(int ChannelID)
        {
            return this.Database.SqlQuery<cms_asp_select_rss_channel_contents_Result>
                ("dbo.cms_asp_select_rss_channel_contents @channel_id={0}",
                ChannelID)
                .ToList();
        }

        public List<cms_asp_admin_select_rss_channel_content_Result> SelectAdminRssChannelContent(int ChannelID)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_rss_channel_content_Result>
                ("dbo.cms_asp_admin_select_rss_channel_content @channel_id={0}",
                ChannelID)
                .ToList();
        }

        public List<cms_asp_admin_update_rss_channel_Result> UpdateSitemap(cms_rss_channels channel)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_rss_channel_Result>
                 ("dbo.cms_asp_admin_update_rss_channel " +
                    "@channel_id={0},"+
                    "@channel_name={1}," +
                    "@channel_status={2}," +
                    "@url={3}," +
                    "@description={4}," +
                    "@lang_id={5}, " +
                    "@managing_editor={6}," +
                    "@copyright={7}," +
                    "@publisher_id={8}," +
                    "@group_id={9}," +
                    "@structure_description={10}," +
                    "@summary_content_field={11}," +
                    "@content_template={12}," +
                    "@content_template_editor_type={13}," +
                    "@singularize_articles={14}",
                 channel.channel_id,
                 channel.channel_name,
                 channel.channel_status,
                 channel.url,
                 channel.description,
                 channel.lang_id,
                 channel.managing_editor,
                 channel.copyright,
                 channel.created_by,
                 channel.group_id,
                 channel.structure_description,
                 channel.summary_content_field,
                 channel.content_template,
                 channel.content_template_editor_type,
                 channel.singularize_articles).ToList();
        }

        public List<cms_asp_admin_delete_rss_channel_Result> DeleteRssChannel(int ChannelID, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_rss_channel_Result>
                ("dbo.cms_asp_admin_delete_rss_channel @channel_id={0},@publisher_id={1},@publisher_level={2}",
                ChannelID,
                publisherID,
                Publisherlevel)
                .ToList();
        }


        public void DeleteRssContent(int ChannelID)
        {
            this.Database.ExecuteSqlCommand
                ("dbo.cms_asp_admin_delete_rss_content @channel_id={0}",
                ChannelID);
        }

        public void InsertRssContent(int ChannelID, int sgzID, string sgzType, string sgzExclude, object PublisherID)
        {
            this.Database.ExecuteSqlCommand
                ("dbo.cms_asp_admin_insert_rss_content @channel_id={0},@sgz_id={1},@sgz_type={2},@sgz_exclude={3},@publisher_id={4}",
                ChannelID,
                sgzID,
                sgzType,
                sgzExclude,
                PublisherID);
        }
    }
}