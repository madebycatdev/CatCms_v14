using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EuroCMS.Data
{
    public partial class Dal
    {

        private static Dal instance = null;

        public static Dal Instance
        { 
            get
            {
                if(instance == null)
                 instance = new Dal();

                return instance;
            }
        }

        public DataTable SelectDefaultArticleForBreadCrumb(string type, int id)
        {
            return DbHelper.ExecProc("dbo.cms_asp_select_default_article_for_breadcrumb", type, id).Tables[0];
        }
 
        public DataTable SelectGetXMLData(int zoneID, string order)
        {
            return DbHelper.ExecuteSQLString("select * from dbo.vArticlesZonesFull with (nolock) where zone_id = " + zoneID.ToString() + " and status = 1 and zone_status = 'A' and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) order by " + order).Tables[0];
        }

        public DataTable SelectGetAFilesData(string alias, int articleID)
        {
            return DbHelper.ExecuteSQLString("select top 1 caf.file_title, caf.file_comment, caf.file_name_1, caf.file_name_2, caf.file_name_3, caf.file_name_4, caf.file_name_5, caf.file_name_6, caf.file_name_7, caf.file_name_8, caf.file_name_9, caf.file_name_10 from dbo.cms_article_files caf with (nolock) left join dbo.cms_file_types cft with (nolock) on cft.type_id = caf.file_type_id where caf.article_id = " + articleID.ToString() + " and cft.type_alias = '" + alias + "' order by caf.file_order").Tables[0];
        }
        public DataTable SelectGetAFilesDataByFileID(string alias, int articleID,int FileID)
        {
            return DbHelper.ExecuteSQLString("select top 1 caf.file_title, caf.file_comment, caf.file_name_1, caf.file_name_2, caf.file_name_3, caf.file_name_4, caf.file_name_5, caf.file_name_6, caf.file_name_7, caf.file_name_8, caf.file_name_9, caf.file_name_10 from dbo.cms_article_files caf with (nolock) left join dbo.cms_file_types cft with (nolock) on cft.type_id = caf.file_type_id where caf.article_id = " + articleID.ToString() + " and caf.file_id='" + FileID + "' and cft.type_alias = '" + alias + "' order by caf.file_order").Tables[0];
        }
        public DataTable SelectGetAFilesData2(string alias, int articleID)
        {
            return DbHelper.ExecuteSQLString("select caf.file_name_1, caf.file_name_2, caf.file_name_3, caf.file_name_4, caf.file_name_5, caf.file_name_6,	 caf.file_name_7, caf.file_name_8, caf.file_name_9, caf.file_name_10, caf.file_title, caf.file_comment, cft.type_alias from dbo.cms_article_files caf with (nolock) left join dbo.cms_file_types cft with (nolock) on cft.type_id = caf.file_type_id where caf.article_id = " + articleID.ToString() + " and cft.type_alias in (" + alias + ") order by caf.file_type_id, caf.file_order").Tables[0];
        }

        public DataTable SelectXmlRelatedArticles(int articleID)
        {
            return DbHelper.ExecuteSQLString("select related_zone_id, related_article_id from dbo.cms_article_relation with (nolock) where article_id =" + articleID).Tables[0];
        }

        public DataTable SelectXmlRelatedArticles2(string azIDs, string order)
        {
            return DbHelper.ExecuteSQLString("select * from dbo.vArticlesZones with (nolock) where ( " + azIDs + ") and status = 1 and zone_status = 'A' and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) order by " + order).Tables[0];
        }

        public DataTable SelectGetAFilesData3(string alias, int articleID)
        {
            return DbHelper.ExecuteSQLString(" select caf.file_name_1, caf.file_name_2, caf.file_name_3, caf.file_name_4, caf.file_name_5, caf.file_name_6, caf.file_name_7, caf.file_name_8, caf.file_name_9, caf.file_name_10, caf.file_title, caf.file_comment, cft.type_alias from dbo.cms_article_files caf with (nolock) left join dbo.cms_file_types cft with (nolock) on cft.type_id = caf.file_type_id where caf.article_id = " + articleID + " " + alias +" order by caf.file_type_id, caf.file_order").Tables[0];
        }
 
        public DataTable SelectUpdateDomainCacheDomains()
        {
            return DbHelper.ExecuteSQLString("select domain_names, home_page_article, error_page_article from dbo.cms_domains with (nolock) where domain_status = 'A'").Tables[0];
        }

        public DataTable SelectSitemapIncludeSites(string includeSites)
        {
            return DbHelper.ExecuteSQLString("Select zone_id, article_id, site_name, zone_group_name, zone_name, headline, az_alias, updated "+
						 "FROM dbo.vArticlesZonesFull with (nolock) " + 
						 "WHERE status = 1 AND zone_type_id = 0 AND article_type = 0 AND ((startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate())) AND site_id in (" + includeSites + ") ").Tables[0];
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>DELETED, NOT DELETED as rCode</returns>
        public string HelperRemoveArticle(long revID, int articleID)
        {
            return DbHelper.ExecProc("dbo._helper_cms_remove_article", revID, articleID).Tables[0].Rows[0][0].ToString();
        }


        public int CreateAspErrors(  string SessionID,
                                        string RequestMethod,
                                        string ServerPort,
                                        string HTTPS,
                                        string LocalAddr,
                                        string HostAddress,
                                        string UserAgent,
                                        string URL,
                                        string CustomerRefID,
                                        string FormData,
                                        string AllHTTP,
                                        string ErrASPCode,
                                        string ErrNumber,
                                        string ErrSource,
                                        string ErrCategory,
                                        string ErrFile,
                                        int ErrLine,
                                        int ErrColumn,
                                        string ErrDescription,
                                        string ErrAspDescription,
                                        string InsertDate)
        {

            return DbHelper.ExecuteNonQuery("dbo.cms_asp_insert_error", SessionID,
                                        RequestMethod,
                                        ServerPort,
                                        HTTPS,
                                        LocalAddr,
                                        HostAddress,
                                        UserAgent,
                                        URL,
                                        CustomerRefID,
                                        FormData,
                                        AllHTTP,
                                        ErrASPCode,
                                        ErrNumber,
                                        ErrSource,
                                        ErrCategory,
                                        ErrFile,
                                        ErrLine,
                                        ErrColumn,
                                        ErrDescription,
                                        ErrAspDescription,
                                        InsertDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="orderNo"></param>
        /// <param name="headline"></param>
        /// <param name="summary"></param>
        /// <param name="article1"></param>
        /// <param name="publisherID"></param>
        /// <returns>OK, NOT_FOUND, @@ERROR as rCode</returns>
        public string AdminBulkUpdateArticleDetails(long revID, int orderNo, string headline, string summary, string article1, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_bulk_update_article_details", revID, orderNo, headline, summary, article1, publisherID).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="revID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>select '0' as rCode, '' as found_name, @new_rev_id as rev_id,
        ///          select '1' as rCode, '' as found_name, '0' as rev_id</returns>
        public DataTable AdminDeleteArticle(int articleID, long revID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_article", articleID, revID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int AdminDeleteArticleCache(int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_article_cache", articleID);
        }


       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleFileRevFileID"></param>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>DF, NF as rCode</returns>
        public string AdminDeleteArticleFilesRevisionFile(long articleFileRevFileID, long revID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_article_files_revision_file", articleFileRevFileID, revID, articleID).Tables[0].Rows[0][0].ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int AdminDeleteArticleLanguageRelationsWithRevision(long revID, int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_article_language_relations_with_revision",revID, articleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int AdminDeleteArticleRelationsWithRevision(long revID, int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_article_relations_with_revision", revID, articleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <returns>affected row count</returns>
        public int AdminDeleteArticleZonesWithRevision(long revID, int articleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_article_zones_with_revision", revID, articleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="breadCrumbID"></param>
        /// <returns>NOTEXIST, DELETED as rCode</returns>
        public string AdminDeleteBreadCrumb(int breadCrumbID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_breadcrumb", breadCrumbID).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ccID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns> select '0' as rCode, '' as found_name
        ///         select '1' as rCode, '' as found_name 
        ///         select '2' as rCode, '' as found_name 
        ///            </returns>
        public DataTable AdminDeleteCC(int ccID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_cc", ccID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clasificationID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0, 1, 2), found_name (empty string)</returns>
        public DataTable AdminDeleteClasification(int clasificationID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_classification", clasificationID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clasificationID"></param>
        /// <param name="columnNo"></param>
        /// <returns>affected row count</returns>
        public int AdminDeleteClasificationComboValues(int clasificationID, int columnNo)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_classification_combo_values", clasificationID, columnNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configID"></param>
        /// <param name="ws"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0, 1, 2), found_name (empty string)</returns>
        public DataTable AdminDeleteConfigParameter(int configID, string ws, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_config_parameter", configID, ws, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cssID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0, 1, 2), found_name (site_name, zone_group_name, zone_name or empty string)</returns>
        public DataTable AdminDeleteCSS(int cssID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_css", cssID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0, 1, 2), found_name (empty string)</returns>
        public DataTable AdminDeleteDomain(int domainID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_domain", domainID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns>0, 1</returns>
        public string AdminDeleteFileType(int typeID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_file_type", typeID).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns>Columns: rCode (0, 1), found_name (empty string)</returns>
        public DataTable AdminDeleteHiddenValue(int hiddenID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_hidden_value", hiddenID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2,3,4,5), found_name (empty string)</returns>
        public DataTable AdminDeleteLanguage(int langID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_language", langID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="langID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0, 1, 2,3), found_name (template_name, zone_name, headline or empty string)</returns>
        public DataTable AdminDeletePlugin(int pluginID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_plugin", pluginID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portletID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2), found_name (empty string)</returns>
        public DataTable AdminDeletePortlet(int portletID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_portlet", portletID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publisherID"></param>
        /// <param name="adminID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2,3), found_name (empty string)</returns>
        public DataTable AdminDeletePublisher(object  publisherID, int adminID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_publisher", publisherID, adminID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publisherID"></param>
        /// <param name="adminID"></param>
        /// <returns>affected row count</returns>
        public int AdminDeletePublisherPermission(object  publisherID, int adminID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_publisher_permissions", publisherID, adminID);
        }

        /// <returns>Columns: rCode (0,1,2), found_name (empty string)</returns>
        public DataTable AdminDeleteRedirection(int redirectID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_redirection", redirectID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2), found_name (empty string)</returns>
        public DataTable AdminDeleteRSSChannel(int channelID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_rss_channel", channelID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns>affected row count</returns>
        public int AdminDeleteRSSContent(int channelID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_delete_rss_content", channelID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2,3,4,5), found_name (domain_alias, zone_group_name, empty string)</returns>
        public DataTable AdminDeleteSite(int siteID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_site", siteID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMapID"></param>
        /// <returns>NOK, OK as rCode</returns>
        public string AdminDeleteSiteMap(int sMapID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_sitemap", sMapID).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stftID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2,3,4), found_name (headline, template_name, zone_name, empty string)</returns>
        public DataTable AdminDeleteStfTemplate(int stftID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_stf_template", stftID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="groupType"></param>
        /// <returns>USED, NOTEXIST,DELETED</returns>
        public string AdminDeleteStructureGroup(int groupID, int groupType)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_structure_group", groupID, groupType).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1,2,3,4), found_name (zone_name, zone_group_name, site_name, empty string)</returns>
        public DataTable AdminDeleteTemplate(int templateID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_template", templateID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>Columns: rCode (0,1), found_name (empty string)</returns>
        public DataTable AdminDeleteXml(int xmlID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_xml", xmlID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="approveLevel"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <param name="cio"></param>
        /// <returns>aStat, found_name, rev_id</returns>
        public DataTable AdminDeleteZone(int zoneID, int approveLevel, object  publisherID, int publisherLevel, char cio)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_zone", zoneID, approveLevel, publisherID, publisherLevel, cio).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneGroupID"></param>
        /// <param name="publisherID"></param>
        /// <param name="publisherLevel"></param>
        /// <returns>rCode, found_name</returns>
        public DataTable AdminDeleteZone(int zoneGroupID, object  publisherID, int publisherLevel)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_delete_zonegroup", zoneGroupID, publisherID, publisherLevel).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="revID"></param>
        /// <returns>OK, NOK</returns>
        public string AdminDiscardArticleFileRevision(int articleID, long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_discard_article_files_revision", articleID, revID).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="publisherID"></param>
        /// <param name="process"></param>
        /// <param name="publisherLevel"></param>
        /// <param name="articleOrRevision"></param>
        /// <returns>fields: rStatus, rLockedBy, rLocked</returns>
        public DataTable AdminEditArticleLock(int articleID, object  publisherID, char process, int publisherLevel, char articleOrRevision)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_edit_article_lock", articleID, publisherID, process, publisherLevel, articleOrRevision).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="publisherID"></param>
        /// <param name="process"></param>
        /// <param name="publisherLevel"></param>
        /// <param name="zoneOrRevision"></param>
        /// <returns>fields: rStatus, rLockedBy, rLocked</returns>
        public DataTable AdminEditZoneLock(int zoneID, object  publisherID, char process, int publisherLevel, char zoneOrRevision)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_edit_zone_lock", zoneID, publisherID, process, publisherLevel, zoneOrRevision).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="zoneID"></param>
        /// <param name="articleID"></param>
        /// <param name="relatedZoneID"></param>
        /// <param name="relatedArticleID"></param>
        /// <param name="poolID"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertArticleLanguageRelationWithRevision(long revID, int zoneID, int articleID, int relatedZoneID, int relatedArticleID, int poolID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_article_language_relations_with_revision", revID, zoneID, articleID, relatedZoneID, relatedArticleID, poolID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <param name="relatedZoneID"></param>
        /// <param name="relatedArticleID"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertArticleLanguageRelationWithRevision(long revID, int articleID, int relatedZoneID, int relatedArticleID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_article_relations_with_revision", revID, articleID, relatedZoneID, relatedArticleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revID"></param>
        /// <param name="articleID"></param>
        /// <param name="zoneID"></param>
        /// <param name="azOrder"></param>
        /// <param name="azAlias"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertArticleZonesWithRevison(long revID, int articleID, int zoneID, int azOrder, string azAlias)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_article_zones_with_revision", revID, articleID, zoneID, azOrder, azOrder, azAlias);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverIP"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertCacheServer(string serverIP)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_cache_server", serverIP);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opAction"></param>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <param name="fileName"></param>
        /// <param name="summary"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertFopFailureLog(string opAction, string sourcePath, string destPath, string fileName, string summary)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_fop_failure_log", opAction, sourcePath, destPath, fileName, summary);
        }

        public int AdminInsertLog(object publisherID,int noteID,string eventName,string Note,string IP,string IdType)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_log", publisherID,noteID,eventName,Note,IP,IdType);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="publisherID"></param>
        /// <param name="noteID"></param>
        /// <param name="eventName"></param>
        /// <param name="note"></param>
        /// <param name="ip"></param>
        /// <param name="idType"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertPublisherPermission(int relType, object  publisherID, int relatedID, string auth, int adminID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_publisher_permissions", relType, publisherID, relatedID, auth, adminID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relType"></param>
        /// <param name="publisherID"></param>
        /// <param name="relatedID"></param>
        /// <param name="auth"></param>
        /// <param name="adminID"></param>
        /// <returns>affected row count</returns>
        public int AdminInsertRSSContent(int channleID, int sgzID, char sgzType, char sgzExclude, int publisherID)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_insert_rss_content", channleID, sgzID, sgzType, sgzExclude, publisherID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneID"></param>
        /// <param name="articleID"></param>
        /// <returns>fields: aStat (ARTICLE_NOT_FOUND, ZONE_NOT_FOUND, REVISION_NOT_FOUND, OK)</returns>
        public string AdminProcessSubZoneArticleRelation(int zoneID, int articleID)
        {
            return DbHelper.ExecuteSQLProc("cms_asp_admin_process_subzone_article_relation", zoneID, articleID).Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>fields: rev_id</returns>
        public long AdminSelectArticleFilesLastRevision(int articleID)
        {
            return Convert.ToInt64(DbHelper.ExecuteSQLProc("cms_asp_admin_select_article_files_last_revision", articleID).Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns>fields: rev_id, rev_status</returns>
        public DataTable AdminSelectArticleFilesLastRevisionForced(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_files_last_revision_forced", articleID).Tables[0];
        }

        public DataTable AdminSelectArticleFilesLastRevisionFileDetails(long afrfID, long revID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_files_revision_file_details", afrfID, revID, articleID).Tables[0];
        }

        public DataTable AdminSelectArticleFilesRevisionFiles(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_files_revision_files", revID).Tables[0];
        }

        public DataTable AdminSelectArticleFilesRevisionList(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_files_revision_list", revID).Tables[0];
        }

        public long AdminSelectArticleLastRevision(int articleID)
        {
            return Convert.ToInt64(DbHelper.ExecProc("dbo.cms_asp_admin_select_article_last_revision", articleID, 0).Tables[0].Rows[0][0]);
        }

        public DataTable AdminSelectArticleRevisionDetails(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_revision_details", articleID, 0).Tables[0];
        }

        public DataTable AdminSelectArticleRevisionList(int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_revision_list", articleID, 0).Tables[0];
        }

        public DataTable AdminSelectArticleZoneNames(int zoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_article_zone_names", zoneID, articleID).Tables[0];
        }

        public int AdminSelectArticlesMaxOrderByZone(int zoneID)
        {
            return Convert.ToInt32(DbHelper.ExecProc("dbo.cms_asp_admin_select_articles_max_order_by_zone", zoneID).Tables[0].Rows[0][0]);
        }

        public DataTable AdminSelectAZCheck(int articleID, int zoneID, string azAlias)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_az_check", articleID, zoneID, azAlias).Tables[0];
        }

        public DataTable AdminSelectCachedArticles()
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_cached_articles").Tables[0];
        }

        public int AdminSelectCountWfar(long revID)
        {
            return Convert.ToInt32(DbHelper.ExecProc("dbo.cms_asp_admin_select_count_wfar", revID).Tables[0].Rows[0][0]);
        }

        public DataTable AdminSelectCSSRevisions(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_css_revisions", revID).Tables[0];
        }

        public DataTable AdminSelectFileTypeDetails(int typeID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_file_type_details", typeID).Tables[0];
        }

        public DataTable AdminSelectFopFailures(int destPath)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_fop_failures", destPath).Tables[0];
        }

        public DataTable AdminSelectNavigationZoneCount(int navigationZoneID, int articleID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_navigation_zone_count", navigationZoneID, articleID).Tables[0];
        }

        public DataTable AdminSelectRedirectionDetail(int redirectID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_redirection_detail", redirectID).Tables[0];
        }

        public DataTable AdminSelectRequiredFileColumns(int articleID, int fileTypeID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_required_file_columns", articleID, fileTypeID).Tables[0];
        }

        public int AdminSelectRequiredFileColumns(long revID)
        {
            return Convert.ToInt32(DbHelper.ExecProc("dbo.cms_asp_admin_select_required_file_count", revID).Tables[0].Rows[0][0]);
        }

        public DataTable AdminSelectRSSChannelContent(int channelID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_rss_channel_content", channelID).Tables[0];
        }

        public DataTable AdminSelectSitemapByDomain(string domainAlias)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_sitemap_by_domain", domainAlias).Tables[0];
        }

        public DataTable AdminSelectSitemapStatus(int smapID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_sitemap_status", smapID).Tables[0];
        }

        public DataTable AdminSelectSitemaps(int smapID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_sitemaps", smapID).Tables[0];
        }

        public DataTable AdminSelectStfTemplates()
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_stf_templates").Tables[0];
        }

        public DataTable AdminSelectStructureGroup(int groupID, int groupType)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_structure_group", groupID, groupType).Tables[0];
        }

        public DataTable AdminSelectTemplateRevisions(int templateID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_template_revisions", templateID).Tables[0];
        }

        public DataTable AdminSelectTemplates(int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_templates", groupID).Tables[0];
        }

        public DataTable AdminSelectTemplates()
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_url_redirects").Tables[0];
        }

        public long AdminSelectZoneLasRevision()
        {
            return Convert.ToInt64(DbHelper.ExecProc("dbo.cms_asp_admin_select_zone_last_revision").Tables[0].Rows[0][0]);
        }

        public DataTable AdminSelectZoneRevisionDetails(long revID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_zone_revision_details", revID).Tables[0];
        }

        public DataTable AdminSelectZoneRevisionList(int zoneID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_select_zone_revision_list", zoneID).Tables[0];
        }

        public DataTable AdminUpdateArticleFileRevision(long afrfID, long revID, int articleID, string fileTitle, int fileOrder, string fileName1, string fileName2, string fileName3, string fileName4, string fileName5, string fileName6, string fileName7, string fileName8, string fileName9, string fileName10, int fileTypeID, string fileComment, int revisedBy)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_article_files_revision", afrfID, revID, articleID, fileTitle, fileOrder, fileName1, fileName2, fileName3, fileName4, fileName5, fileName6, fileName7, fileName8, fileName9, fileName10, fileTypeID, fileComment, revisedBy).Tables[0];
        }

        public DataTable AdminUpdateArticleRevision(long revID, string revName, int articleID, int clsfID, int status, DateTime startDate, DateTime endDate, int orderNo, string langID, int navigationDisplay, int navigationZoneID, string menuText, string headline, string summary, string keywords, int articleType, string articleTypeDetail, string article1, string article2, string article3, string article4, string article5, string custom1, string custom2, string custom3, string custom4, string custom5, string custom6, string custom7, string custom8, string custom9, string custom10, string custom11, string custom12, string custom13, string custom14, string custom15, string custom16, string custom17, string custom18, string custom19, string custom20, bool flag1, bool flag2, bool flag3, bool flag4, bool flag5, DateTime date1, DateTime date2, DateTime date3, DateTime date4, DateTime date5, bool revFlag1, bool revFlag2, bool revFlag3, bool revFlag4, bool revFlag5, int cl1, int cl2, int cl3, int cl4, int cl5, string customBody, int revisedBy, char cio, string metaDescription, char content1EditorType, char content2EditorType, char content3EditorType, char content4EditorType, char content5EditorType, string omnitureCode, string customSetting)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_article_revision", revID, revName, articleID, clsfID, status, startDate, endDate, orderNo, langID, navigationDisplay, navigationZoneID, menuText, headline, summary, keywords, articleType, articleTypeDetail, article1, article2,  article3, article4, article5, custom1, custom2, custom3, custom4, custom5, custom6, custom7, custom8, custom9, custom10, custom11, custom12,custom13, custom14, custom15, custom16, custom17, custom18, custom19, custom20, flag1, flag2, flag3, flag4, flag5, date1, date2, date3, date4, date5, revFlag1, revFlag2, revFlag3, revFlag4, revFlag5, cl1, cl2, cl3, cl4, cl5, customBody, revisedBy, cio, metaDescription, content1EditorType, content2EditorType, content3EditorType, content4EditorType, content5EditorType, omnitureCode, customSetting).Tables[0];
        }

        public DataTable AdminUpdateArticleRevision(long revID, string revisionStatus, int approvalID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_article_revision_status",revID,revisionStatus,approvalID).Tables[0];
        }


        public DataTable AdminUpdateBreadCrumb(long breadCrumbID, string breadCrumbName, int deepLevel, char includeSite, char includeZoneGroup, char includeHeadline, string excludedSites, string excludedZoneGroups, string excludedZones, string seperator, string ulClass, char includeSubZones, string breadCrumbMainContainer, string breadCrumbSubContainer, string breadCrumbSubItemContainer, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_breadcrumb", breadCrumbID, breadCrumbName, deepLevel, includeSite, includeZoneGroup, includeHeadline, excludedSites, excludedZoneGroups, excludedZones, seperator, ulClass, includeSubZones, breadCrumbMainContainer, breadCrumbSubContainer, breadCrumbSubItemContainer, publisherID).Tables[0];
        }

        public DataTable AdminUpdateCC(int ccID, string ccName, string ccHTML, object  publisherID, int groupID, string structureDescription)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_cc", ccID, ccName, ccHTML, publisherID, groupID, structureDescription).Tables[0];
        }

        public DataTable AdminUpdateClasificaitonComboValues(int clasificaitonID, int columnNo, string comboSupid, string comboLabel, string comboValue, int createdBy)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_classification_combo_values", clasificaitonID, columnNo, comboSupid, comboLabel, comboValue, createdBy).Tables[0];
        }

        public DataTable AdminUpdateClasificaitonDetails(int clasificaitonID, string clasificationName, bool summaryCb, bool endDateCb, bool keywordsCb, bool custom1Cb, bool custom2Cb, bool custom3Cb, bool custom4Cb, bool custom5Cb, bool custom6Cb, bool custom7Cb, bool custom8Cb, bool custom9Cb, bool custom110Cb, bool custom11Cb, bool custom12Cb, bool custom13Cb, bool custom14Cb, bool custom15Cb, bool custom16Cb, bool custom17Cb, bool custom18Cb, bool custom19Cb, bool custom20Cb, bool date1Cb, bool date2Cb, bool date3Cb, bool date4Cb, bool date5Cb, string custom1Text, string custom2Text, string custom3Text, string custom4Text, string custom5Text, string custom6Text, string custom7Text, string custom8Text, string custom9Text, string custom10Text, string custom11Text, string custom12Text, string custom13Text, string custom14Text, string custom15Text, string custom16Text, string custom17Text, string custom18Text, string custom19Text, string custom20Text, char custom1Type, char custom2Type, char custom3Type, char custom4Type, char custom5Type, char custom6Type, char custom7Type, char custom8Type, char custom9Type, char custom10Type, string flag1Text, string flag2Text, string flag3Text, string flag4Text, string flag5Text, string date1Text, string date2Text, string date3Text, string date4Text, string date5Text, string summaryText, string enddateText, string keywordText, string article1Text, string article2Text, string article3Text, string article4Text, string article5Text, bool article1Cb, bool article2Cb, bool article3Cb, bool article4Cb, bool article5Cb, int custom1SubColumn, int custom2SubColumn, int custom3SubColumn, int custom4SubColumn, int custom5SubColumn, int custom6SubColumn, int custom7SubColumn, int custom8SubColumn, int custom9SubColumn, int custom10SubColumn, bool fileRequiredCb, bool fileTitleRequiredCb, bool fileDescriptionRequiredCb, string requiredFileTypes, int createdBy, int groupID, string structureDescription)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_classification_details", clasificaitonID, clasificationName, summaryCb, endDateCb, keywordsCb, custom1Cb, custom2Cb, custom3Cb, custom4Cb, custom5Cb, custom6Cb, custom7Cb, custom8Cb,  custom9Cb, custom110Cb, custom11Cb, custom12Cb, custom13Cb, custom14Cb, custom15Cb, custom16Cb, custom17Cb, custom18Cb, custom19Cb, custom20Cb, date1Cb, date2Cb, date3Cb, date4Cb, date5Cb, custom1Text, custom2Text, custom3Text, custom4Text, custom5Text, custom6Text, custom7Text, custom8Text, custom9Text, custom10Text, custom11Text, custom12Text, custom13Text, custom14Text, custom15Text, custom16Text, custom17Text, custom18Text, custom19Text, custom20Text, custom1Type, custom2Type, custom3Type, custom4Type, custom5Type, custom6Type,  custom7Type, custom8Type,  custom9Type,  custom10Type,  flag1Text,flag2Text, flag3Text, flag4Text, flag5Text, date1Text, date2Text, date3Text, date4Text,date5Text, summaryText, enddateText, keywordText, article1Text,  article2Text, article3Text, article4Text, article5Text, article1Cb, article2Cb, article3Cb, article4Cb, article5Cb, custom1SubColumn, custom2SubColumn, custom3SubColumn, custom4SubColumn, custom5SubColumn, custom6SubColumn, custom7SubColumn, custom8SubColumn, custom9SubColumn, custom10SubColumn, fileRequiredCb, fileTitleRequiredCb, fileDescriptionRequiredCb, requiredFileTypes, createdBy, groupID, structureDescription).Tables[0];
        }

        public DataTable AdminUpdateConfigParameter(int configID, string configName, string configValue, string ws, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_config_parameter", configID, configName, configValue, ws, publisherID).Tables[0];
        }

        public DataTable AdminUpdateCSS(int cssID, string cssName, string cssCode, string cssFix, int cssType, string cssRelText, string cssRelTypeText, object  publisherID, int groupID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_css", cssID, cssName, cssCode, cssFix, cssType, cssRelText, cssRelTypeText, publisherID, groupID).Tables[0];
        }

        public DataTable AdminUpdateDomains(int domainID, string domainNames, string homePageArticle, object  publisherID, string errorPageArticle)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_domains", domainID, domainNames, homePageArticle, publisherID, errorPageArticle).Tables[0];
        }

        public DataTable AdminUpdateFileType(int fileTypeID, string typeName, string typeAlias, string file1Name, string file1Extension, int file1Size, string file1Wh
             , string file2Name, string file2Extension, int file2Size, string file2Wh
             , string file3Name, string file3Extension, int file3Size, string file3Wh
             , string file4Name, string file4Extension, int file4Size, string file4Wh
             , string file5Name, string file5Extension, int file5Size, string file5Wh
             , string file6Name, string file6Extension, int file6Size, string file6Wh
             , string file7Name, string file7Extension, int file7Size, string file7Wh
             , string file8Name, string file8Extension, int file8Size, string file8Wh
             , string file9Name, string file9Extension, int file9Size, string file9Wh
             , string file10Name, string file10Extension, int file10Size, string file10Wh
             , int groupID, string structureDescription)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_file_type", fileTypeID,  typeName, typeAlias, file1Name, file1Extension, file1Size, file1Wh
             , file2Name, file2Extension, file2Size, file2Wh
             , file3Name, file3Extension, file3Size, file3Wh
             , file4Name, file4Extension, file4Size, file4Wh
             , file5Name, file5Extension, file5Size, file5Wh
             , file6Name, file6Extension, file6Size, file6Wh
             , file7Name, file7Extension, file7Size, file7Wh
             , file8Name, file8Extension, file8Size, file8Wh
             , file9Name, file9Extension, file9Size, file9Wh
             , file10Name, file10Extension, file10Size, file10Wh
             , groupID, structureDescription).Tables[0];
        }

        public int AdminUpdateFopFailureStatus(int logID, char opStatus, string summary, int processedBy)
        {
            return DbHelper.ExecuteNonQuery("dbo.cms_asp_admin_update_fop_failure_status", logID, opStatus, summary, processedBy);
        }

        public DataTable AdminUpdateHiddenValues(int hiddenID, string hiddenValue, int hiddenType, string hiddenDesc, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_hidden_values", hiddenID, hiddenValue, hiddenType, hiddenDesc, publisherID).Tables[0];
        }

        public DataTable AdminUpdateHiddenValues(int langID, string langName, string langXml, int langOrder, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_language", langID, langName, langXml, langOrder, publisherID).Tables[0];
        }

        public DataTable AdminUpdatePlugin(int pluginID, string pluginName, string pluginCode, int pluginStatus, object  publisherID, int groupID, string structureDescription)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_plugin", pluginID, pluginName, pluginCode, pluginStatus, publisherID, groupID, structureDescription).Tables[0];
        }

        public DataTable AdminUpdatePortlet(int portletID, string portletName, int portletStatus, string portletHTML, string portletCss, char editorType, string portletHeader, string portletFooter, object  publisherID, int groupID, string structureDescription, char enableShourcut)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_portlet", portletID, portletName, portletStatus, portletHTML, portletCss, editorType, portletHeader, portletFooter, publisherID, groupID, structureDescription, enableShourcut).Tables[0];
        }

        public DataTable AdminUpdatePublisherDetails(object  publisherID, string publisherName, string userName, string password, char publisherStatus, string publisherEmail, int publisherLevel, string publisherDepartment, string publihserNote, int updatedBy)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_publisher_details", publisherID, publisherName, userName, password, publisherStatus, publisherEmail, publisherLevel, publisherDepartment, publihserNote, updatedBy).Tables[0];
        }
        public DataTable AdminUpdatePublisherProfile(object  publisherID, int publisherZG, string oldPassword, string newPassword)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_publisher_profile",publisherID,publisherZG,oldPassword,newPassword).Tables[0];
        }


        public DataTable AdminUpdateRedirectionDetails(int redirectID, string redirectAlias, int zoneID, int articleID, int groupID, string structureDescription, bool permanentRedirection, int publisherID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_redirection_details", redirectID, redirectAlias, zoneID, articleID, groupID, structureDescription, permanentRedirection, publisherID).Tables[0];
        }

        public DataTable AdminUpdateRSSChannel(int channelID, string channelName, char channelStatus, string url, string description, string langID, string managingEditor, string copright, object  publisherID, int groupID, string structureDescription, string summaryContentField, string contentTemplate, char contentEditorType, char singularizeArticles)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_rss_channel", channelID, channelName, channelStatus, url, description, langID, managingEditor, copright, publisherID, groupID, structureDescription, summaryContentField, contentTemplate, contentEditorType, singularizeArticles).Tables[0];
        }

        public DataTable AdminUpdateSite(int siteID, string siteName, int cssID, int cssIDMobile, int templateID, int templateIDMobile, string siteKeywords, string siteHeader, string siteJs, string analytics, string customBody, string siteIcon, string tagDetailArticle, string article1, string article2, string article3, string article4, string article5, object  publisherID, int groupID, string structureDescription, string metaDescription, char content1EditorType, char content2EditorType, char content3EditorType, char content4EditorType, char content5EditorType, string defaultArticle, string omnitureCode, int domainID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_site",siteID, siteName, cssID, cssIDMobile, templateID, templateIDMobile, siteKeywords, siteHeader, siteJs, analytics, customBody, siteIcon, tagDetailArticle, article1, article2, article3, article4, article5, publisherID, groupID, structureDescription, metaDescription, content1EditorType, content2EditorType, content3EditorType, content4EditorType, content5EditorType, defaultArticle, omnitureCode, domainID).Tables[0];
        }

        public DataTable AdminUpdateSitemap(int smapID, int domainID, string domainAlias, char notifyGoogle, char notifyMSN, char notifyAsk, char notifyYahoo, string yahooID, string includedSites, string excludedZoneGroups, string excludedZones, string excludedArticles, char aFiles, int interval, char enabled, char gzipEnabled, int pubID)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_sitemap",smapID, domainID, domainAlias, notifyGoogle, notifyMSN, notifyAsk, notifyYahoo, yahooID, includedSites, excludedZoneGroups,  excludedZones, excludedArticles, aFiles, interval, enabled, gzipEnabled, pubID).Tables[0];
        }

        public void AdminUpdateSitemapGZ(int smapID, byte[] image)
        {
            DbHelper.ExecProc("dbo.cms_asp_admin_update_sitemap_gz", smapID, image);
        }

        public DataTable AdminUpdateSitemapStatus(int smapID, int status, string xml)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_sitemap_status", smapID, status, xml).Tables[0];
        }

        public DataTable AdminUpdateSTFTemplate(int stfID, string stfName, string stfFormHtml, string stfThanks, string stfMailHtml, string stfMailSubject, string stfMailFromName, string stfWH, string omnitureFunction, int createdBy)
        {
            return DbHelper.ExecProc("dbo.cms_asp_admin_update_stf_template", stfID, stfName, stfFormHtml, stfThanks, stfMailHtml, stfMailSubject, stfMailFromName, stfWH, omnitureFunction, createdBy).Tables[0];
        }
    }
}
