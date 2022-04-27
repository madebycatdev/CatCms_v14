using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
 
namespace EuroCMS.Data
{
    public partial class Dal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int UpdateArticleRating(long articleID, int rate)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_update_article_rating", articleID, rate);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int UpdateArticleClicks(long articleID)
        {

            return DbHelper.ExecuteNonQuery("dbo.cms_asp_update_article_clicks", articleID);

        }
        public DataTable SelectZoneGroupsBySite(int siteID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_zones_groups_by_site", siteID).Tables[0];
        }

        public DataTable SelectZonesByGroup(int zonegroupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_zones_by_group", zonegroupID).Tables[0];
        }

        public DataTable SelectZonesRevisionsDetails(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_zone_revision_details", revID).Tables[0];
        }

        public DataTable SelectZoneGroupDetails(int zonegroupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_zone_group_details", zonegroupID).Tables[0];
        }

        public DataTable SelectZoneDetailsByID(int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_zone_details_by_id", zoneID).Tables[0];
        }

        public DataTable SelectXmlList(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_xml_list", groupID).Tables[0];
        }

        public DataTable SelectXmlDetails(int xmlID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_xml_details", xmlID).Tables[0];
        }

        public DataTable SelectTemplateHtml(int templateID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_template_html", templateID).Tables[0];
        }

        public DataTable SelectTemplateHistoryHtml(int historyID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_template_history_html", historyID).Tables[0];
        }

        public DataTable SelectStfTemplateHtml(int stftID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_stf_template_html", stftID).Tables[0];
        }

        public DataTable SelectSitesRestricted(int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_sites_restricted", publisherID).Tables[0];
        }

        public DataTable SelectSites(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_sites", groupID).Tables[0];
        }

        public DataTable SelectSiteDetails(int siteID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_site_details", siteID).Tables[0];
        }

        public DataTable SelectRssChannels(int groupID, int siteID, int zonegroupID, int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_rss_channels", groupID, siteID, zonegroupID, zoneID).Tables[0];
        }

        public DataTable SelectRssChannelDetails(int channelID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_rss_channel_details", channelID).Tables[0];
        }

        public DataTable SelectRssChannelContents(int channelID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_rss_channel_contents", channelID).Tables[0];
        }

        public DataTable SelectRedirectionByAlias(string redirectAlias, string domainName)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_redirection_by_alias", redirectAlias, domainName).Tables[0];
        }

        public DataTable SelectPublishersForApproval(int ID, long revID, string Type)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_publishers_for_approval", ID, revID, Type).Tables[0];
        }

        public int SelectPublishers()
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_select_publishers");
        }

        public DataTable SelectPublisherZones(int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_publisher_zones", publisherID).Tables[0];
        }

        public DataTable SelectPublisherPermission(int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_publisher_permissions", publisherID).Tables[0];
        }

        public DataTable SelectPublisherDetails(int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_publisher_details", publisherID).Tables[0];
        }

        public DataTable SelectPortlets(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_portlets", groupID).Tables[0];
        }

        public DataTable SelectPortletDetails(int portletID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_portlet_details", portletID).Tables[0];
        }

        public DataTable SelectPlugins(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_plugins", groupID).Tables[0];
        }

        public DataTable SelectPluginCode(int pluginID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_plugin_code", pluginID).Tables[0];
        }

        public DataTable SelectPermission(object  publisherID, int ID, string Type)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_permissions", publisherID, ID, Type).Tables[0];
        }

        public DataTable SelectPermissionObjectByGroup(int zonegroupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_permission_object_by_group", zonegroupID).Tables[0];
        }

        public int SelectLogEventTypes()
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_select_log_event_types");
        }

        public int SelectLanguages()
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_select_languages");
        }

        public DataTable SelectLanguageDetails(int langID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_language_details", langID).Tables[0];
        }

        public DataTable SelectHiddenValues()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_hidden_values").Tables[0];
        }

        public DataTable SelectHiddenDetails(int hiddenID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_hidden_details", hiddenID).Tables[0];
        }

        public DataTable SelectFullTemplateDetailsByZoneID(int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_full_template_details_by_zone_id", zoneID).Tables[0];
        }
        /// <summary>
        /// type_id IN (inID)
        /// type_id NOT IN (inID)
        /// </summary>
        /// <param name="inID">IN</param>
        /// <param name="withORwithout">NOT IN</param>
        /// <returns></returns>
        public DataTable SelectFileTypesWithID(string inID, string withORwithout)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_file_types_with_id", inID, withORwithout).Tables[0];
        }

        public DataTable SelectFileTypes()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_file_types").Tables[0];
        }

        public DataTable SelectDomains()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_domains").Tables[0];
        }

        public DataTable SelectDomainsDetails(int domainID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_domain_details", domainID).Tables[0];
        }

        public DataTable SelectCSSRelType(int ID1, int ID2, int ID3, int ID4, int ID5, int ID6)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_css_rel_type", ID1, ID2, ID3, ID4, ID5, ID6).Tables[0];
        }

        public DataTable SelectCssHistoryCode(int historyID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_css_history_code", historyID).Tables[0];
        }

        public DataTable SelectCssCode(int cssID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_css_code", cssID).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID">Can be -1 or anything</param>
        /// <returns></returns>
        public DataTable SelectCss(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_css", groupID).Tables[0];
        }

        public DataTable SelectComboValues(int classificationID, int columnNO)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_combo_values", classificationID, columnNO).Tables[0];
        }

        public DataTable SelectComboChainedValues(int classificationID, string subValue, int columnNO)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_combo_chained_values", classificationID, subValue, columnNO).Tables[0];
        }

        public DataTable SelectClassifications(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_classifications", groupID).Tables[0];
        }

        public DataTable SelectClassificationDetails(int classificationID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_classification_details", classificationID).Tables[0];
        }

        public DataTable SelectCcDetails(int ccID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_cc_details", ccID).Tables[0];
        }

        public DataTable SelectBreadCrumb(int breadcrumbID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_breadcrumb", breadcrumbID).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="breadcrumbID">Can be -1 or else</param>
        /// <returns></returns>
        public DataTable SelectArticlesByZoneForBreadcrumb(int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_articles_by_zone_for_breadcrumb", zoneID).Tables[0];
        }

        public DataTable SelectArticlesByZone(int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_articles_by_zone", zoneID).Tables[0];
        }

        public DataTable SelectArticleZonesByRevision(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_zones_by_revision", revID).Tables[0];
        }

        public DataTable SelectArticleZonesByArticle(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_zones_by_article", articleID).Tables[0];
        }

        public DataTable SelectArticleTags(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_tags", articleID).Tables[0];
        }

        public DataTable SelectArticleRevisionDetails(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_revision_details", revID).Tables[0];
        }

        public DataTable SelectArticleRelatedByRevision(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_relateds_by_revision", revID).Tables[0];
        }

        public DataTable SelectArticleLanguageRelationsByRevision(long revID, int zoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_language_relations_by_revision", revID, zoneID, articleID).Tables[0];
        }

        public DataTable SelectArticleLanguageRelations(int zoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_language_relations", articleID, zoneID).Tables[0];
        }

        public DataTable SelectArticleFiles(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_files", articleID).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneID">Can be 0 or else</param>
        /// <param name="articleID">Can be 0 or else</param>
        /// <returns></returns>
        public DataTable SelectArticleDetailsForBreadcrumb(int zoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_details_for_breadcrumb", zoneID, articleID).Tables[0];
        }

        public DataTable SelectArticleDetailsForAllZones(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_details_for_all_zones", articleID).Tables[0];
        }

        public DataTable SelectArticleDetails(int zoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_details", zoneID, articleID).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="articleID"></param>
        /// <returns>checked , '' </returns>
        public string SelectArticleCache(int zoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_cache", zoneID, articleID).Tables[0].Rows[0][0].ToString();
        }

        public DataTable SelectArticleByAlias(string articleAlias, string domainName)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_article_by_alias", articleAlias, domainName).Tables[0];
        }

        public DataTable SelectAllZones()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_all_zones").Tables[0];
        }

        public DataTable SelectAllTemplates()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_all_templates").Tables[0];
        }

        public DataTable SelectAllPortlets()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_all_portlets").Tables[0];
        }

        public DataTable SelectAllPlugins()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_all_plugins").Tables[0];
        }

        public DataTable SelectAllCss()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_all_css").Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>articleID,zoneID</returns>

        public DataTable MBuilderSelectUpperLevelAzidChain(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_mbuilder_select_upper_level_azid_chain", articleID).Tables[0];
        }

        public DataTable MBuilderSelectUpperLevelAzid(int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_mbuilder_select_upper_level_azid", zoneID).Tables[0];
        }

        public DataTable LoginSelectPublisherByUsername(string userName)
        {
            return DbHelper.ExecProc("dbo.cms_asp_login_select_publisher_by_username", userName).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromName"></param>
        /// <param name="fromEmail"></param>
        /// <param name="fromIP"></param>
        /// <param name="toName"></param>
        /// <param name="toEmail"></param>
        /// <param name="toNote"></param>
        /// <param name="stftID"></param>
        /// <param name="zoneID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int InsertStfEmails(string fromName, string fromEmail, string fromIP, string toName, string toEmail, string toNote, int stftID, int zoneID, int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_insert_stf_emails", fromName, fromEmail, fromIP, toName, toEmail, toNote, stftID, zoneID, articleID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="serverIP"></param>
        /// <param name="searchQuery"></param>
        /// <param name="searchIN"></param>
        /// <param name="resultCount"></param>
        /// <returns>affected row count</returns>
        public int InsertSearchLog(string clientIP, string serverIP, string searchQuery, string searchIN, int resultCount)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_insert_search_log", clientIP, serverIP, searchQuery, searchIN, resultCount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="zoneID"></param>
        /// <param name="searchText"></param>
        /// <param name="Summary"></param>
        /// <param name="Keywords"></param>
        /// <param name="Description"></param>
        /// <returns>affected row count</returns>
        public int InsertArticleSearchText(int articleID, int zoneID, string searchText, string headline, string Summary, string Keywords, string Description)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_insert_article_search_text", articleID, zoneID, searchText, headline, Summary, Keywords, Description);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int InsertArticleCache(int zoneID, int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_insert_article_cache", zoneID, articleID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>affected row count</returns>
        public int InsertArticleBulkCache()
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_insert_article_bulk_cache");
        }

        public DataTable ImOpenIm(int imsID, int imsTo, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_im_open_im", imsID, imsTo, publisherLevel).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imsFrom"></param>
        /// <param name="imsTo"></param>
        /// <param name="imsSubject"></param>
        /// <param name="imsMessage"></param>
        /// <param name="imsType">ZA,AA,FA</param>
        /// <param name="zaid"></param>
        /// <param name="relatedName"></param>
        /// <param name="relatedID"></param>
        /// <returns>'' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email</returns>
        public DataTable ImInsertIm(int imsFrom, int imsTo, string imsSubject, string imsMessage, string imsType, int zaid, string relatedName, int relatedID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_im_insert_im", imsFrom, imsTo, imsSubject, imsMessage, imsType, zaid, relatedName, relatedID).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imsID"></param>
        /// <param name="imsTo"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>0 or 1</returns>
        public string ImDeleteIm(int imsID, int imsTo, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_im_delete_im", imsID, imsTo, publisherLevel).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imsTo"></param>
        /// <returns>'' as ims_count, '' as due_count</returns>
        public DataTable ImCheckIm(int imsTo)
        {
            return DbHelper.ExecProc("dbo.cms_asp_im_check_im", imsTo).Tables[0];
        }

        public string GetNewGuid()
        {
            return DbHelper.ExecProc("dbo.cms_asp_get_new_guid").Tables[0].Rows[0][0].ToString();
        }

        public int DeleteArticleSearchText(int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_delete_article_search_text", articleID);
        }

        public int DeleteArticleFilesForNewRevision(int articleID,long revID,int approvalID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_delete_article_files_for_new_revision", articleID, revID, approvalID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldRevID"></param>
        /// <param name="revID"></param>
        /// <param name="afrfID"></param>
        /// <param name="articleID"></param>
        /// <returns>@@ERROR</returns>
        public string CopyAritcleFilesRevisionFiles(long oldRevID, long revID, int afrfID,int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_copy_article_files_revision_files",oldRevID,revID,afrfID,articleID).Tables[0].Rows[0][0].ToString();
        }

        public int ConfigUpdateRemoteValue(string configName, string configValueRemote, int publisherID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_config_update_remote_value", configName, configValueRemote, publisherID);
        }

        public int ConfigUpdateLocalValue(string configName, string configValueRemote, int publisherID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_config_update_local_value", configName, configValueRemote, publisherID);
        }


        public DataTable ConfigSelectConfigParameters()
        {
            return DbHelper.ExecProc("dbo.cms_asp_config_select_config_parameters").Tables[0];
        }

        public DataTable ConfigCheckDoneStatus(string serverIP)
        {
            return DbHelper.ExecProc("dbo.cms_asp_config_check_done_status",serverIP).Tables[0];
        }


        public int CacheUpdateUpdateStatus(string serverIP,int status,int timeout)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_cache_update_update_status", serverIP,status,timeout);
        }

        public int CacheRemoveArticleFromCache(int zoneID, int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_cache_remove_article_from_cache", zoneID, articleID);
        }


        public DataTable CacheCheckUpdateStatus(string serverIP)
        {
            return DbHelper.ExecProc("dbo.cms_asp_cache_check_update_status",serverIP).Tables[0];
        }

        public int CacheAddArticleToCache(int zoneID,int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_cache_add_article_to_cache", zoneID,articleID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="approveLevel"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <param name="cio"></param>
        /// <returns></returns>
        public DataTable ApprovalApproveZoneRevision(long revID,int approveLevel,object  publisherID,int publisherLevel,string cio)
        {
            return DbHelper.ExecProc("dbo.cms_asp_approval_approve_zone_revision", revID,approveLevel,publisherID,publisherLevel,cio).Tables[0];
        }

        public DataTable ApprovalApproveIncomingNewsv2(int newsID,int zoneID,int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_approval_approve_incoming_news_v2",newsID,zoneID,publisherID).Tables[0];
        }

        public DataTable ApprovalApproveIncomingNews(int newsID, int zoneID, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_approval_approve_incoming_news", newsID, zoneID, publisherID).Tables[0];
        }

        public DataTable ApprovalApproveArticleRevision(long revID,int approveLevel,object  publisherID,int publisherLevel,string cio)
        {
            return DbHelper.ExecProc("dbo.cms_asp_approval_approve_article_revision", revID,approveLevel,publisherID,publisherLevel,cio).Tables[0];
        }

        public DataTable ApprovalApproveArticleFilesRevision(long revID, int approveLevel, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_approval_approve_article_files_revision", revID, approveLevel, publisherID, publisherLevel).Tables[0];
        }

        public DataSet AdminUpdateZoneGroups(int zoneGroupID, string zoneGroupName, string zoneGroupKeywords, string Analytics, int siteID, int cssMerge, int cssID, int cssIdMobile, int cssIdPrint, int templateID, int templateIdMobile, string customBody, string tagDetailsArticle, string Article1, string Article2, string Article3, string Article4, string Article5, int Append1, int Append2, int Append3, int Append4, int Append5, object  publisherID, string MetaDescription, string zoneGroupNameDisplay, string content1EditorType, string content2EditorType, string content3EditorType, string content4EditorType, string content5EditorType, string defaultArticle, string omnitureCode)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_zonegroups",zoneGroupID,zoneGroupName,zoneGroupKeywords,Analytics,siteID,cssMerge,cssID,cssIdMobile,cssIdPrint,templateID,templateIdMobile,customBody,tagDetailsArticle,Article1,Article2,Article3,Article4,Article5,Append1,Append2,Append3,Append4,Append5,publisherID,MetaDescription,zoneGroupNameDisplay,content1EditorType,content2EditorType,content3EditorType,content4EditorType,content5EditorType,defaultArticle,omnitureCode);
        }

        public string AdminUpdateZoneRevisionStatus(long revID,string revisionStatus,int approvalID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_zone_revision_status", revID, revisionStatus,approvalID).Tables[0].Rows[0][0].ToString();
        }

        public DataTable AdminUpdateZoneRevision(long revID, string revName, string revNote, int zoneID, int zonegroupID, int zoneTypeID, string zoneStatus, string zoneName, string zoneDesc, int cssMerge, int cssID, int cssIdMobile, int cssIdPrint, int templateID, int templateIdMobile, string customBody, string zoneKeywords, string Article1, string Article2, string Article3, string Article4, string Article5, int Append1, int Append2, int Append3, int Append4, int Append5, string Analytics, int revisedBy, string MetaDescription, string zoneNameDisplay, string cio, string content1EditorType, string content2EditorType, string content3EditorType, string content4EditorType, string content5EditorType,string defaultArticle,string omnitureCode,string langID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_zone_revision", revID,revName,revNote,zoneID,zonegroupID,zoneTypeID,zoneStatus,zoneName,zoneDesc,cssMerge,cssID,cssIdMobile,cssIdPrint,templateID,templateIdMobile,customBody,zoneKeywords,Article1,Article2,Article3,Article4,Article5,Append1,Append2,Append3,Append4,Append5,Analytics,MetaDescription,zoneNameDisplay,cio,content1EditorType,content2EditorType,content3EditorType,content4EditorType,content5EditorType,defaultArticle,omnitureCode,langID).Tables[0];
        }


        public DataTable AdminUpdateXmlDetails(int xmlID,string xmlName,string xmlMainNode,string xmlMainNodeAttrib,string xmlPerNode,int xmlSubTemplate,int xmlLevel,string xmlRelatedLine,string xmlXml,int createdBy,int groupID,string structureDescription)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_xml_details", xmlID,xmlMainNode,xmlMainNodeAttrib,xmlPerNode,xmlSubTemplate,xmlLevel,xmlRelatedLine,xmlXml,createdBy,groupID,structureDescription).Tables[0];
        }

        public DataTable AdminUpdateTemplate(int templateID,string templateName,string templateHtml,int templateType,object  publisherID,int groupID,string structureDescription,string content1EditorType,string templateDoctype)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_template", templateID,templateName,templateHtml,templateType,publisherID,groupID,structureDescription,content1EditorType,templateDoctype).Tables[0];
        }

        public DataTable AdminUpdateStructureGroup(object  publisherID,int groupID,string groupName,int groupType)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_structure_group",publisherID,groupID,groupName,groupType).Tables[0];
        }


        public DataTable PageRedirections()
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_redirections").Tables[0];
        }
    }
}
