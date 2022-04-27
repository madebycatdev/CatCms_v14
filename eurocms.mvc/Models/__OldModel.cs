using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EuroCMS.Admin.entity
{
    public class SiteStructureModel
    {
        public SiteStructureModel()
        {
            this.List = new List<SiteStructureModel>();
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ChildType { get; set; }
        public string ParentIdName { get; set; }
        public string Status { get; set; }
        public int RooLevelId { get; set; }

        public IList<SiteStructureModel> List { get; private set; }
        public bool IsChild
        {
            get
            {
                return this.List.Count == 0;
            }
        }
    }
    public partial class cms_zones
    {
        public cms_zones()
        {
            articles = new List<cms_articles>();
            zone_id = 0;
            zone_name = "";
            zone_desc = "";
            zone_keywords = "";
            css_merge = 0;
            css_id = 0;
            template_id = 0;
            zone_group_id = 0;
            zone_status = "A";
            css_id_mobile = 0;
            css_id_print = 0;
            template_id_mobile = 0;
            custom_body = "";
            append_1 = (byte)0;
            append_2 = (byte)0;
            append_3 = (byte)0;
            append_4 = (byte)0;
            append_5 = (byte)0;
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";
            // publisher_id = 0;
            zone_type_id = 0;
            analytics = "";
            meta_description = "";
            zone_name_display = "";
            default_article = "";
            omniture_code = "";
            lang_id = "";
        }

        [Key]
        public int zone_id { get; set; }
        public int zone_group_id { get; set; }
        public int zone_type_id { get; set; }
        public string zone_status { get; set; }
        public string zone_name { get; set; }
        public string zone_desc { get; set; }
        public int css_merge { get; set; }
        public int css_id { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id { get; set; }
        public int template_id_mobile { get; set; }
        public string custom_body { get; set; }
        public string zone_keywords { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public byte append_1 { get; set; }
        public byte append_2 { get; set; }
        public byte append_3 { get; set; }
        public byte append_4 { get; set; }
        public byte append_5 { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string analytics { get; set; }
        public string meta_description { get; set; }
        public string zone_name_display { get; set; }
        public Nullable<System.DateTime> locked { get; set; }
        public Nullable<int> locked_by { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
        public string lang_id { get; set; }
        public List<cms_articles> articles { get; set; }
    }
    public partial class cms_zone_revision
    {
        public cms_zone_revision()
        {
            rev_id = -1;
            revision_status = "";
            zone_id = 0;
            zone_name = "";
            zone_desc = "";
            zone_keywords = "";
            revised_by = "";
            approval_id = "";
            css_merge = 0;
            css_id = 0;
            template_id = 0;
            zone_group_id = 0;
            zone_status = "A";
            css_id_mobile = 0;
            css_id_print = 0;
            template_id_mobile = 0;
            custom_body = "";
            append_1 = (byte)0;
            append_2 = (byte)0;
            append_3 = (byte)0;
            append_4 = (byte)0;
            append_5 = (byte)0;
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";
            rev_name = "";
            rev_note = "";
            zone_type_id = -1;
            analytics = "";
            meta_description = "";
            zone_name_display = "";
            content_1_editor_type = "H";
            content_2_editor_type = "H";
            content_3_editor_type = "H";
            content_4_editor_type = "H";
            content_5_editor_type = "H";
            default_article = "";
            omniture_code = "";
            lang_id = "";
        }

        [Key]
        public long rev_id { get; set; }
        public System.DateTime created { get; set; }
        public virtual object created_by { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public object revised_by { get; set; }
        public string rev_name { get; set; }
        public string rev_note { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public object approval_id { get; set; }
        public int zone_id { get; set; }
        public int zone_group_id { get; set; }
        public int zone_type_id { get; set; }
        public string zone_status { get; set; }
        public string current_status { get; set; }
        public string zone_name { get; set; }
        public string publisher_name { get; set; }
        public string revised_name { get; set; }
        public string approval_name { get; set; }
        public string zone_desc { get; set; }
        public int css_merge { get; set; }
        public int css_id { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id { get; set; }
        public int template_id_mobile { get; set; }

        public string custom_body { get; set; }
        public string zone_keywords { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public byte append_1 { get; set; }
        public byte append_2 { get; set; }
        public byte append_3 { get; set; }
        public byte append_4 { get; set; }
        public byte append_5 { get; set; }
        public string analytics { get; set; }
        public string meta_description { get; set; }
        public string zone_name_display { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
        public string lang_id { get; set; }
        public string cio { get; set; }
    }
    public partial class cms_zone_groups
    {
        public cms_zone_groups()
        {
            zone_group_id = -1;
            zone_group_name = "";
            zone_group_keywords = "";
            site_id = 0;
            css_merge = 0;
            css_id = 0;
            css_id_mobile = 0;
            css_id_print = 0;
            template_id = 0;
            template_id_mobile = 0;
            custom_body = "";
            //publisher_id =0;
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";
            append_1 = 0;
            append_2 = 0;
            append_3 = 0;
            append_4 = 0;
            append_5 = 0;
            analytics = "";
            tag_detail_article = "";
            meta_description = "";
            zone_group_name_display = "";
            content_1_editor_type = "";
            content_2_editor_type = "";
            content_3_editor_type = "";
            content_4_editor_type = "";
            content_5_editor_type = "";
            default_article = "";
            omniture_code = "";
        }

        [Key]
        public int zone_group_id { get; set; }
        public string zone_group_name { get; set; }
        public string zone_group_keywords { get; set; }
        public int site_id { get; set; }
        public int css_merge { get; set; }
        public int css_id { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id { get; set; }
        public int template_id_mobile { get; set; }
        public string custom_body { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public byte append_1 { get; set; }
        public byte append_2 { get; set; }
        public byte append_3 { get; set; }
        public byte append_4 { get; set; }
        public byte append_5 { get; set; }
        public string analytics { get; set; }
        public string tag_detail_article { get; set; }
        public string meta_description { get; set; }
        public string zone_group_name_display { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
    }
    public partial class cms_xml
    {
        [Key]
        public int xml_id { get; set; }
        public string xml_name { get; set; }
        public string xml_main_node { get; set; }
        public string xml_main_node_attrib { get; set; }
        public string xml_per_node { get; set; }
        public string xml_per_node_attrib { get; set; }
        public string xml_sub_node { get; set; }
        public int xml_sub_template { get; set; }
        public int xml_level { get; set; }
        public string xml_related_line { get; set; }
        public string xml_xml { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_tmp
    {
        public int tmp_id { get; set; }
        public string tmp { get; set; }
    }
    public partial class cms_templates
    {
        public cms_templates()
        {
            template_id = 0;
            content_1_editor_type = "H";
            //publisher_id=0;
            group_id = 0;
            structure_description = "";
            template_doctype = "";
            template_type = (byte)0;
            template_html = "";
        }

        [Key]
        public int template_id { get; set; }
        public byte template_type { get; set; }
        [Required]
        public string template_name { get; set; }
        [Required]
        public string template_html { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string template_doctype { get; set; }
    }
    public partial class cms_template_revisions
    {
        [Key]
        public int history_id { get; set; }
        public int template_id { get; set; }
        public string template_html { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public byte template_type { get; set; }
        public string content_1_editor_type { get; set; }
        public string template_doctype { get; set; }
    }
    public partial class cms_structure_groups
    {
        [Key]
        public int group_id { get; set; }
        public int group_type { get; set; }
        public string group_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
    }
    public partial class cms_stf_templates
    {
        public int stft_id { get; set; }
        public string stft_status { get; set; }
        public string stft_name { get; set; }
        public string stft_form_html { get; set; }
        public string stft_thanks { get; set; }
        public string stft_mail_html { get; set; }
        public string stft_mail_from_name { get; set; }
        public string stft_mail_subject { get; set; }
        public string stft_wh { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public string omniture_function { get; set; }
    }
    public partial class cms_stf_emails
    {
        public long stf_id { get; set; }
        public string from_name { get; set; }
        public string from_email { get; set; }
        public string from_ip { get; set; }
        public string to_name { get; set; }
        public string to_email { get; set; }
        public string to_note { get; set; }
        public int stft_id { get; set; }
        public int zone_id { get; set; }
        public int article_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_sites
    {
        public cms_sites()
        {
            content_1_editor_type = "";
            content_2_editor_type = "";
            content_3_editor_type = "";
            content_4_editor_type = "";
            content_5_editor_type = "";
            default_article = "";
            css_id = 0;
            css_id_mobile = 0;
            css_id_print = 0;
            template_id_mobile = -1;
            template_id_mobile = 0;
            //publisher_id = 0;
            site_keywords = "";
            site_header = "";
            site_icon = "";
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";
            tag_detail_article = "";
            group_id = 0;
            structure_description = "";
            meta_description = "";
            site_js = "";
            analytics = "";
            omniture_code = "";
            custom_body = "";
            site_id = 0;
        }

        [Key]
        public int site_id { get; set; }
        [Required]
        public string site_name { get; set; }
        public int css_id { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id { get; set; }
        public int template_id_mobile { get; set; }
        public object publisher_id { get; set; }
        public string site_keywords { get; set; }
        public string site_header { get; set; }
        public string site_js { get; set; }
        public string custom_body { get; set; }
        public string site_icon { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public string analytics { get; set; }
        public string tag_detail_article { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string meta_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
        public Nullable<int> domain_id { get; set; }
        public virtual cms_domains domains { get; set; }
    }
    public partial class cms_sitemaps
    {
        [Key]
        public int smap_id { get; set; }
        public int domain_id { get; set; }
        public string domain_alias { get; set; }
        public byte status { get; set; }
        public Nullable<System.DateTime> last_update { get; set; }
        public Nullable<System.DateTime> last_generate { get; set; }
        public Nullable<System.DateTime> last_generate_start { get; set; }
        public string notify_google { get; set; }
        public string notify_msn { get; set; }
        public string notify_ask { get; set; }
        public string notify_yahoo { get; set; }
        public string yahoo_id { get; set; }
        public string included_sites { get; set; }
        public string excluded_zonegroups { get; set; }
        public string excluded_zones { get; set; }
        public string excluded_articles { get; set; }
        public string afiles { get; set; }
        public int interval { get; set; }
        public string enabled { get; set; }
        public string xml { get; set; }
        public byte[] gz { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<Guid> updated_by { get; set; }
        public string gzip_enabled { get; set; }
    }
    public partial class cms_search_log
    {
        public long sid { get; set; }
        public System.DateTime created { get; set; }
        public string server_ip { get; set; }
        public string client_ip { get; set; }
        public string search_query { get; set; }
        public string search_in { get; set; }
        public int result_count { get; set; }
    }
    public partial class cms_rss_content
    {
        public int rss_zone_id { get; set; }
        [Key]
        public int channel_id { get; set; }
        public int sgz_id { get; set; }
        public string sgz_type { get; set; }
        public string sgz_exclude { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
    }
    public partial class cms_rss_channels
    {
        [Key]
        public int channel_id { get; set; }
        public string channel_status { get; set; }
        public string channel_name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string lang_id { get; set; }
        public string managing_editor { get; set; }
        public string copyright { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string summary_content_field { get; set; }
        public string content_template { get; set; }
        public string content_template_editor_type { get; set; }
        public string singularize_articles { get; set; }
    }
    public partial class cms_relations
    {
        public long rel_id { get; set; }
        public byte rel_type { get; set; }
        public int article_id { get; set; }
        public int classification_id { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_redirects
    {
        [Key]
        public int redirect_id { get; set; }
        public string redirect_alias { get; set; }
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public System.DateTime created { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public Nullable<bool> permanent_redirection { get; set; }
    }
    public partial class cms_publishers
    {
        public cms_publishers()
        {
            //publisher_id = "";
            publisher_level = 100;
            publisher_department = "";
            publisher_note = "";
            publisher_status = "A";
            updated_by = 0;
        }

        [Key]
        public object publisher_id { get; set; }
        public string publisher_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string publisher_status { get; set; }
        public string publisher_email { get; set; }
        public byte publisher_level { get; set; }
        public string publisher_department { get; set; }
        public string publisher_note { get; set; }
        public int publisher_zg { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public object created_by { get; set; }
        public int updated_by { get; set; }
    }
    public partial class cms_publisher_permissions
    {
        public int rel_id { get; set; }
        public byte rel_type { get; set; }
        public object publisher_id { get; set; }
        public int related_id { get; set; }
        public string auth { get; set; }
    }
    public partial class cms_publisher_logs
    {
        public int log_id { get; set; }
        public object publisher_id { get; set; }
        public int event_id { get; set; }
        public long note_id { get; set; }
        public string title { get; set; }
        public string note { get; set; }
        public string ip { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_publisher_log_events
    {
        public int event_id { get; set; }
        public string event_name { get; set; }
        public string event_description { get; set; }
        public byte event_type { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_portlets
    {
        [Key]
        public int portlet_id { get; set; }
        public string portlet_name { get; set; }
        public object publisher_id { get; set; }
        public byte portlet_status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public string portlet_html { get; set; }
        public string portlet_css { get; set; }
        public bool editor_type { get; set; }
        public string portlet_header { get; set; }
        public string portlet_footer { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string content_editor_type { get; set; }
        public string enable_shortcut { get; set; }
    }
    public partial class cms_plugins
    {
        [Key]
        public int plugin_id { get; set; }
        public byte plugin_status { get; set; }
        public string plugin_name { get; set; }
        public string plugin_code { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_languages
    {
        public cms_languages()
        {
            lang_xml = "";
            // publisher_id = 0;
        }

        [Key]
        public string lang_id { get; set; }
        public string lang_name { get; set; }
        public string lang_xml { get; set; }
        public int lang_order { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string lang_alias { get; set; }
    }
    public partial class cms_language_relations_revision
    {
        public long lr_id { get; set; }
        public long rev_id { get; set; }
        public int zone_id { get; set; }
        public int article_id { get; set; }
    }
    public partial class cms_language_relations
    {
        public long lr_id { get; set; }
        public int zone_id { get; set; }
        public int article_id { get; set; }
    }
    public partial class cms_instant_messaging
    {
        public long ims_id { get; set; }
        public int ims_from { get; set; }
        public int ims_to { get; set; }
        public string ims_subject { get; set; }
        public string ims_message { get; set; }
        public string ims_type { get; set; }
        public long related_id { get; set; }
        public string related_name { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> readed { get; set; }
        public Nullable<System.DateTime> processed { get; set; }
        public Nullable<System.DateTime> deleted { get; set; }
        public Nullable<System.DateTime> due { get; set; }
    }
    public partial class cms_hidden_values
    {
        [Key]
        public int hidden_id { get; set; }
        public string hidden_value { get; set; }
        public byte hidden_type { get; set; }
        public string hidden_desc { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
    }
    public partial class cms_fop_failure_log
    {
        public int log_id { get; set; }
        public string op_action { get; set; }
        public string source_path { get; set; }
        public string dest_path { get; set; }
        public string file_name { get; set; }
        public string summary { get; set; }
        public System.DateTime created { get; set; }
        public byte retry_count { get; set; }
        public Nullable<System.DateTime> processed { get; set; }
        public Nullable<int> processed_by { get; set; }
        public string op_status { get; set; }
    }
    public partial class cms_file_types
    {
        [Key]
        public int type_id { get; set; }
        public string type_name { get; set; }
        public string type_alias { get; set; }
        public string file1_name { get; set; }
        public string file2_name { get; set; }
        public string file3_name { get; set; }
        public string file4_name { get; set; }
        public string file5_name { get; set; }
        public string file6_name { get; set; }
        public string file7_name { get; set; }
        public string file8_name { get; set; }
        public string file9_name { get; set; }
        public string file10_name { get; set; }
        public string file1_extension { get; set; }
        public string file2_extension { get; set; }
        public string file3_extension { get; set; }
        public string file4_extension { get; set; }
        public string file5_extension { get; set; }
        public string file6_extension { get; set; }
        public string file7_extension { get; set; }
        public string file8_extension { get; set; }
        public string file9_extension { get; set; }
        public string file10_extension { get; set; }
        public string file1_wh { get; set; }
        public string file2_wh { get; set; }
        public string file3_wh { get; set; }
        public string file4_wh { get; set; }
        public string file5_wh { get; set; }
        public string file6_wh { get; set; }
        public string file7_wh { get; set; }
        public string file8_wh { get; set; }
        public string file9_wh { get; set; }
        public string file10_wh { get; set; }
        public int file1_size { get; set; }
        public int file2_size { get; set; }
        public int file3_size { get; set; }
        public int file4_size { get; set; }
        public int file5_size { get; set; }
        public int file6_size { get; set; }
        public int file7_size { get; set; }
        public int file8_size { get; set; }
        public int file9_size { get; set; }
        public int file10_size { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_domains
    {
        [Key]
        public int domain_id { get; set; }
        [Required(ErrorMessage = "Domain Names is required!")]
        public string domain_names { get; set; }
        [Required(ErrorMessage = "Home Page Article is required!")]
        public string home_page_article { get; set; }
        public string domain_status { get; set; }
        public System.DateTime created { get; set; }
        [Required(ErrorMessage = "Created by is required!")]
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public object updated_by { get; set; }
        [Required(ErrorMessage = "Error Page Article is required!")]
        public string error_page_article { get; set; }
    }
    public partial class cms_custom_form
    {
        public int id { get; set; }
        public string sender_name { get; set; }
        public string sender_surname { get; set; }
        public string sender_mail { get; set; }
        public string sender_company { get; set; }
        public string sender_phone { get; set; }
        public string info_type { get; set; }
        public string subject { get; set; }
        public string sub_subject { get; set; }
        public string opinion { get; set; }
        public string to_name { get; set; }
        public string to_surname { get; set; }
        public string to_mail { get; set; }
        public string ip { get; set; }
        public System.DateTime created { get; set; }
        public string job { get; set; }
        public string title { get; set; }
        public string department { get; set; }
    }
    public partial class cms_custom_content
    {
        [Key]
        public int cc_id { get; set; }
        public string cc_name { get; set; }
        public string cc_html { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_css_revisions
    {
        public int history_id { get; set; }
        public int css_id { get; set; }
        public string css_code { get; set; }
        public string css_fix { get; set; }
        public string css_rel_text { get; set; }
        public string css_type_text { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public byte css_type { get; set; }
    }
    public partial class cms_css
    {
        [Key]
        public int css_id { get; set; }
        public string css_name { get; set; }
        public string css_status { get; set; }
        public int css_type { get; set; }
        public string css_code { get; set; }
        public string css_fix { get; set; }
        public string css_rel_text { get; set; }
        public string css_type_text { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_config
    {
        [Key]
        public int config_id { get; set; }
        public string config_name { get; set; }
        public string config_value_local { get; set; }
        public string config_value_remote { get; set; }
        public string isDefault { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_classifications
    {
        [Key]
        public int classification_id { get; set; }
        public string classification_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public bool summary_cb { get; set; }
        public bool enddate_cb { get; set; }
        public bool keywords_cb { get; set; }
        public bool custom1_cb { get; set; }
        public bool custom2_cb { get; set; }
        public bool custom3_cb { get; set; }
        public bool custom4_cb { get; set; }
        public bool custom5_cb { get; set; }
        public bool custom6_cb { get; set; }
        public bool custom7_cb { get; set; }
        public bool custom8_cb { get; set; }
        public bool custom9_cb { get; set; }
        public bool custom10_cb { get; set; }
        public bool custom11_cb { get; set; }
        public bool custom12_cb { get; set; }
        public bool custom13_cb { get; set; }
        public bool custom14_cb { get; set; }
        public bool custom15_cb { get; set; }
        public bool custom16_cb { get; set; }
        public bool custom17_cb { get; set; }
        public bool custom18_cb { get; set; }
        public bool custom19_cb { get; set; }
        public bool custom20_cb { get; set; }
        public bool flag1_cb { get; set; }
        public bool flag2_cb { get; set; }
        public bool flag3_cb { get; set; }
        public bool flag4_cb { get; set; }
        public bool flag5_cb { get; set; }
        public bool date1_cb { get; set; }
        public bool date2_cb { get; set; }
        public bool date3_cb { get; set; }
        public bool date4_cb { get; set; }
        public bool date5_cb { get; set; }
        public string custom1_text { get; set; }
        public string custom2_text { get; set; }
        public string custom3_text { get; set; }
        public string custom4_text { get; set; }
        public string custom5_text { get; set; }
        public string custom6_text { get; set; }
        public string custom7_text { get; set; }
        public string custom8_text { get; set; }
        public string custom9_text { get; set; }
        public string custom10_text { get; set; }
        public string custom11_text { get; set; }
        public string custom12_text { get; set; }
        public string custom13_text { get; set; }
        public string custom14_text { get; set; }
        public string custom15_text { get; set; }
        public string custom16_text { get; set; }
        public string custom17_text { get; set; }
        public string custom18_text { get; set; }
        public string custom19_text { get; set; }
        public string custom20_text { get; set; }
        public string flag1_text { get; set; }
        public string flag2_text { get; set; }
        public string flag3_text { get; set; }
        public string flag4_text { get; set; }
        public string flag5_text { get; set; }
        public string date1_text { get; set; }
        public string date2_text { get; set; }
        public string date3_text { get; set; }
        public string date4_text { get; set; }
        public string date5_text { get; set; }
        public string custom1_type { get; set; }
        public string custom2_type { get; set; }
        public string custom3_type { get; set; }
        public string custom4_type { get; set; }
        public string custom5_type { get; set; }
        public string custom6_type { get; set; }
        public string custom7_type { get; set; }
        public string custom8_type { get; set; }
        public string custom9_type { get; set; }
        public string custom10_type { get; set; }
        public string summary_text { get; set; }
        public string enddate_text { get; set; }
        public string keywords_text { get; set; }
        public string article1_text { get; set; }
        public string article2_text { get; set; }
        public string article3_text { get; set; }
        public string article4_text { get; set; }
        public string article5_text { get; set; }
        public bool article1_cb { get; set; }
        public bool article2_cb { get; set; }
        public bool article3_cb { get; set; }
        public bool article4_cb { get; set; }
        public bool article5_cb { get; set; }
        public byte custom1_subcolumn { get; set; }
        public byte custom2_subcolumn { get; set; }
        public byte custom3_subcolumn { get; set; }
        public byte custom4_subcolumn { get; set; }
        public byte custom5_subcolumn { get; set; }
        public byte custom6_subcolumn { get; set; }
        public byte custom7_subcolumn { get; set; }
        public byte custom8_subcolumn { get; set; }
        public byte custom9_subcolumn { get; set; }
        public byte custom10_subcolumn { get; set; }
        public bool file_required_cb { get; set; }
        public bool file_title_required_cb { get; set; }
        public bool file_description_required_cb { get; set; }
        public string required_file_types { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_classification_combo_values
    {
        public int id { get; set; }
        public int classification_id { get; set; }
        public byte column_no { get; set; }
        public string combo_supid { get; set; }
        public string combo_label { get; set; }
        public string combo_value { get; set; }
        public int combo_order { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
    }
    public partial class cms_cache_update
    {
        public byte id { get; set; }
        public byte status { get; set; }
        public System.DateTime timeout { get; set; }
        public string server_ip { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_breadcrumbs
    {
        [Key]
        public int breadcrumb_id { get; set; }
        public string breadcrumb_name { get; set; }
        public byte deep_level { get; set; }
        public string include_site { get; set; }
        public string include_zonegroup { get; set; }
        public string include_headline { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<int> updated_by { get; set; }
        public string excluded_sites { get; set; }
        public string excluded_zonegroups { get; set; }
        public string excluded_zones { get; set; }
        public string seperator { get; set; }
        public string ul_class { get; set; }
        public string include_submenus { get; set; }
        public string breadcrumb_main_container { get; set; }
        public string breadcrumb_main_item_container { get; set; }
        public string breadcrumb_sub_container { get; set; }
        public string breadcrumb_sub_item_container { get; set; }
    }
    public partial class cms_asp_select_zones_groups_by_site_Result
    {
        public int zone_group_id { get; set; }
        public string zone_group_name { get; set; }
        public object publisher_id { get; set; }
        public string site_name { get; set; }
        public string publisher_name { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_asp_select_zones_by_group_Result
    {
        public int zone_id { get; set; }
        public string zone_desc { get; set; }
        public string zone_status { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string publisher_name { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int zone_type_id { get; set; }
        public Nullable<System.DateTime> locked { get; set; }
        public Nullable<int> locked_by { get; set; }
    }
    public partial class cms_asp_select_zone_revision_details_Result
    {
        public string article_id { get; set; }
        public string status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public System.DateTime startdate { get; set; }
        public System.DateTime enddate { get; set; }
        public string publisher_id { get; set; }
        public string clicks { get; set; }
        public string orderno { get; set; }
        public string az_order { get; set; }
        public string lang_id { get; set; }
        public string navigation_display { get; set; }
        public string navigation_zone_id { get; set; }
        public int zone_type_id { get; set; }
        public string menu_text { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }
        public string keywords { get; set; }
        public string article_type { get; set; }
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
        public string flag_1 { get; set; }
        public string flag_2 { get; set; }
        public string flag_3 { get; set; }
        public string flag_4 { get; set; }
        public string flag_5 { get; set; }
        public System.DateTime date_1 { get; set; }
        public System.DateTime date_2 { get; set; }
        public System.DateTime date_3 { get; set; }
        public System.DateTime date_4 { get; set; }
        public System.DateTime date_5 { get; set; }
        public int cl_1 { get; set; }
        public int cl_2 { get; set; }
        public int cl_3 { get; set; }
        public int cl_4 { get; set; }
        public int cl_5 { get; set; }
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
        public int zone_publisher_id { get; set; }
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
        public Nullable<int> zg_publisher_id { get; set; }
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
        public Nullable<int> site_publisher_id { get; set; }
        public string site_keywords { get; set; }
        public string site_header { get; set; }
        public string site_icon { get; set; }
        public Nullable<System.DateTime> site_created { get; set; }
        public Nullable<System.DateTime> site_updated { get; set; }
        public string s_article_1 { get; set; }
        public string s_article_2 { get; set; }
        public string s_article_3 { get; set; }
        public string s_article_4 { get; set; }
        public string s_article_5 { get; set; }
        public string s_custom_body { get; set; }
        public string site_js { get; set; }
        public string site_analytics { get; set; }
        public string zg_analytics { get; set; }
        public string zone_analytics { get; set; }
        public string zone_meta_description { get; set; }
        public string zone_group_meta_description { get; set; }
        public string site_meta_description { get; set; }
        public string zone_group_name_display { get; set; }
        public string zone_name_display { get; set; }
        public string az_alias { get; set; }
        public string article_omniture_code { get; set; }
        public string zone_omniture_code { get; set; }
        public string zone_group_omniture_code { get; set; }
        public string site_omniture_code { get; set; }
    }
    public partial class cms_asp_select_zone_group_details_Result
    {
        public int zone_group_id { get; set; }
        public string zone_group_name { get; set; }
        public string zone_group_keywords { get; set; }
        public int site_id { get; set; }
        public int css_merge { get; set; }
        public int css_id { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id { get; set; }
        public int template_id_mobile { get; set; }
        public string custom_body { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public byte append_1 { get; set; }
        public byte append_2 { get; set; }
        public byte append_3 { get; set; }
        public byte append_4 { get; set; }
        public byte append_5 { get; set; }
        public string analytics { get; set; }
        public string tag_detail_article { get; set; }
        public string meta_description { get; set; }
        public string zone_group_name_display { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
    }
    public partial class cms_asp_select_zone_details_by_id_Result
    {
        public int zone_id { get; set; }
        public string out_name { get; set; }
        public string zone_name { get; set; }
        public int zone_type_id { get; set; }
    }
    public partial class cms_asp_select_xml_list_Result
    {
        public int xml_id { get; set; }
        public string xml_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public string publisher_name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_xml_details_Result
    {
        public int xml_id { get; set; }
        public string xml_name { get; set; }
        public string xml_main_node { get; set; }
        public string xml_main_node_attrib { get; set; }
        public string xml_per_node { get; set; }
        public string xml_per_node_attrib { get; set; }
        public string xml_sub_node { get; set; }
        public int xml_sub_template { get; set; }
        public int xml_level { get; set; }
        public string xml_related_line { get; set; }
        public string xml_xml { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_select_template_html_Result
    {
        public string template_name { get; set; }
        public string template_html { get; set; }
        public byte template_type { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string template_doctype { get; set; }
    }
    public partial class cms_asp_select_template_history_html_Result
    {
        public string template_name { get; set; }
        public string template_html { get; set; }
        public byte template_type { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string template_doctype { get; set; }
    }
    public partial class cms_asp_select_stf_template_html_Result
    {
        public string stft_name { get; set; }
        public string stft_status { get; set; }
        public string stft_form_html { get; set; }
        public string stft_thanks { get; set; }
        public string stft_mail_html { get; set; }
        public string stft_mail_subject { get; set; }
        public string stft_mail_from_name { get; set; }
        public string stft_wh { get; set; }
        public string omniture_function { get; set; }
    }
    public partial class cms_asp_select_sites_Result
    {
        public int site_id { get; set; }
        public string site_name { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
        public List<cms_asp_select_zones_groups_by_site_Result> zone_groups;
        //public List<cms_domains> domains { get; set; }
    }
    public partial class cms_asp_select_sites_restricted_Result
    {
        public int site_id { get; set; }
        public string site_name { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_select_site_details_Result
    {
        public int site_id { get; set; }
        public string site_name { get; set; }
        public int css_id { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id { get; set; }
        public int template_id_mobile { get; set; }
        public object publisher_id { get; set; }
        public string site_keywords { get; set; }
        public string site_header { get; set; }
        public string site_js { get; set; }
        public string custom_body { get; set; }
        public string site_icon { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public string analytics { get; set; }
        public string tag_detail_article { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string meta_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
        public Nullable<int> domain_id { get; set; }
        public List<cms_domains> domains { get; set; }
    }
    public partial class cms_asp_select_rss_channels_Result
    {
        public int channel_id { get; set; }
        public string channel_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public object updated_by { get; set; }
        public string publisher_name { get; set; }
        public string channel_status { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_rss_channel_details_Result
    {
        public int channel_id { get; set; }
        public string channel_name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string lang_id { get; set; }
        public string managing_editor { get; set; }
        public string copyright { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public object updated_by { get; set; }
        public string channel_status { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string summary_content_field { get; set; }
        public string content_template { get; set; }
        public string content_template_editor_type { get; set; }
        public string singularize_articles { get; set; }
    }
    public partial class cms_asp_select_rss_channel_contents_Result
    {
        public string article_1 { get; set; }
        public string az_alias { get; set; }
        public string summary { get; set; }
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
        public string menu_text { get; set; }
        public int article_id { get; set; }
        public Nullable<int> site_id { get; set; }
        public int zone_group_id { get; set; }
        public int zone_id { get; set; }
        public string headline { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string summary1 { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public System.DateTime updated { get; set; }
        public byte status { get; set; }
        public int zone_type_id { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
    }
    public partial class cms_asp_select_redirection_by_alias_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string headline { get; set; }
        public bool permanent_redirection { get; set; }
    }
    public partial class cms_asp_select_publishers_Result
    {
        public object publisher_id { get; set; }
        public string publisher_name { get; set; }
        public string publisher_status { get; set; }
        public string publisher_email { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public string created_by_name { get; set; }
    }
    public partial class cms_asp_select_publishers_for_approval_Result
    {
        public string auth { get; set; }
        public int rel_type { get; set; }
        public object publisher_id { get; set; }
        public string publisher_name { get; set; }
        public byte publisher_level { get; set; }
        public string publisher_department { get; set; }
        public Nullable<int> item_owner { get; set; }
        public string current_status { get; set; }
    }
    public partial class cms_asp_select_publisher_permissions_Result
    {
        public string out_type { get; set; }
        public int out_id { get; set; }
        public string auth { get; set; }
        public string out_name { get; set; }
    }
    public partial class cms_asp_select_publisher_details_Result
    {
        public object publisher_id { get; set; }
        public string publisher_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string publisher_status { get; set; }
        public string publisher_email { get; set; }
        public byte publisher_level { get; set; }
        public string publisher_department { get; set; }
        public string publisher_note { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public object created_by { get; set; }
        public int updated_by { get; set; }
        public string created_by_name { get; set; }
        public int publisher_zg { get; set; }
    }
    public partial class cms_asp_select_portlets_Result
    {
        public int portlet_id { get; set; }
        public string portlet_name { get; set; }
        public object publisher_id { get; set; }
        public byte portlet_status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public object updated_by { get; set; }
        public string updated_name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_portlet_details_Result
    {
        public int portlet_id { get; set; }
        public string portlet_name { get; set; }
        public object publisher_id { get; set; }
        public byte portlet_status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public object updated_by { get; set; }
        public string portlet_html { get; set; }
        public string portlet_css { get; set; }
        public string content_editor_type { get; set; }
        public string portlet_header { get; set; }
        public string portlet_footer { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string enable_shortcut { get; set; }
    }
    public partial class cms_asp_select_plugins_Result
    {
        public int plugin_id { get; set; }
        public byte plugin_status { get; set; }
        public string plugin_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public string publisher_name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_plugin_code_Result
    {
        public string plugin_name { get; set; }
        public string plugin_code { get; set; }
        public byte plugin_status { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_select_permissions_Result
    {
        public string auth { get; set; }
        public int rel_type { get; set; }
        public object publisher_id { get; set; }
        public string publisher_name { get; set; }
        public byte publisher_level { get; set; }
        public string publisher_department { get; set; }
        public Nullable<int> item_owner { get; set; }
    }
    public partial class cms_asp_select_permission_object_by_group_Result
    {
        public int out_id { get; set; }
        public string out_name { get; set; }
        public string out_type { get; set; }
    }
    public partial class cms_asp_select_log_event_types_Result
    {
        public int event_id { get; set; }
        public string event_name { get; set; }
        public string event_description { get; set; }
        public byte event_type { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_asp_select_languages_Result
    {
        public string lang_id { get; set; }
        public string lang_name { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_asp_select_language_details_Result
    {
        public string lang_id { get; set; }
        public string lang_name { get; set; }
        public string lang_xml { get; set; }
        public int lang_order { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string lang_alias { get; set; }
    }
    public partial class cms_asp_select_hidden_values_Result
    {
        public int hidden_id { get; set; }
        public string hidden_value { get; set; }
        public byte hidden_type { get; set; }
        public string hidden_desc { get; set; }
        public object created_by { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_select_hidden_details_Result
    {
        public int hidden_id { get; set; }
        public string hidden_value { get; set; }
        public byte hidden_type { get; set; }
        public string hidden_desc { get; set; }
        public object created_by { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_asp_select_full_template_details_by_zone_id_Result
    {
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
        public int zone_publisher_id { get; set; }
        public System.DateTime zone_created { get; set; }
        public System.DateTime zone_updated { get; set; }
        public string zone_custom_body { get; set; }
        public string zone_group_name { get; set; }
        public string zone_group_keywords { get; set; }
        public int site_id { get; set; }
        public int zg_css_merge { get; set; }
        public int zg_css_id { get; set; }
        public int zg_css_id_mobile { get; set; }
        public int zg_css_id_print { get; set; }
        public int zg_template_id { get; set; }
        public int zg_template_id_mobile { get; set; }
        public int zg_publisher_id { get; set; }
        public System.DateTime zg_created { get; set; }
        public System.DateTime zg_updated { get; set; }
        public string zg_article_1 { get; set; }
        public string zg_article_2 { get; set; }
        public string zg_article_3 { get; set; }
        public string zg_article_4 { get; set; }
        public string zg_article_5 { get; set; }
        public byte zg_append_1 { get; set; }
        public byte zg_append_2 { get; set; }
        public byte zg_append_3 { get; set; }
        public byte zg_append_4 { get; set; }
        public byte zg_append_5 { get; set; }
        public string zg_custom_body { get; set; }
        public string site_name { get; set; }
        public int site_css_id { get; set; }
        public int site_css_id_mobile { get; set; }
        public int site_css_id_print { get; set; }
        public int site_template_id { get; set; }
        public int site_template_id_mobile { get; set; }
        public int site_publisher_id { get; set; }
        public string site_keywords { get; set; }
        public string site_header { get; set; }
        public string site_js { get; set; }
        public string site_icon { get; set; }
        public System.DateTime site_created { get; set; }
        public System.DateTime site_updated { get; set; }
        public string s_article_1 { get; set; }
        public string s_article_2 { get; set; }
        public string s_article_3 { get; set; }
        public string s_article_4 { get; set; }
        public string s_article_5 { get; set; }
        public string s_custom_body { get; set; }
    }
    public partial class cms_asp_select_file_types_Result
    {
        public int type_id { get; set; }
        public string type_name { get; set; }
        public string type_alias { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_domains_Result
    {
        public int domain_id { get; set; }
        public string domain_names { get; set; }
        public string home_page_article { get; set; }
        public object created_by { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_select_domain_details_Result
    {
        public int domain_id { get; set; }
        public string domain_names { get; set; }
        public string home_page_article { get; set; }
        public object created_by { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string error_page_article { get; set; }
    }
    public partial class cms_asp_select_custom_contents_Result
    {
        public int cc_id { get; set; }
        public int group_id { get; set; }
        public string cc_name { get; set; }
        public string cc_html { get; set; }
        public Nullable<DateTime> updated { get; set; }
        public string group_name { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_select_css_Result
    {
        public int css_id { get; set; }
        public string css_name { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
        public byte css_type { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_css_rel_type_Result
    {
        public int css_id { get; set; }
        public string rel_type { get; set; }
    }
    public partial class cms_asp_select_css_history_code_Result
    {
        public int css_id { get; set; }
        public int history_id { get; set; }
        public string css_name { get; set; }
        public string css_code { get; set; }
        public byte css_type { get; set; }
        public string css_fix { get; set; }
        public string css_rel_text { get; set; }
        public string css_type_text { get; set; }
        public Nullable<int> group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_select_css_code_Result
    {
        public string css_name { get; set; }
        public string css_code { get; set; }
        public byte css_type { get; set; }
        public string css_fix { get; set; }
        public string css_rel_text { get; set; }
        public string css_type_text { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_select_combo_values_Result
    {
        public int id { get; set; }
        public int classification_id { get; set; }
        public byte column_no { get; set; }
        public string combo_supid { get; set; }
        public string combo_label { get; set; }
        public string combo_value { get; set; }
        public int combo_order { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
    }
    public partial class cms_asp_select_combo_chained_values_Result
    {
        public int id { get; set; }
        public int classification_id { get; set; }
        public byte column_no { get; set; }
        public string combo_supid { get; set; }
        public string combo_label { get; set; }
        public string combo_value { get; set; }
        public int combo_order { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
    }
    public partial class cms_asp_select_classifications_Result
    {
        public int classification_id { get; set; }
        public string classification_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public string publisher_name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_select_classification_details_Result
    {

        public cms_asp_select_classification_details_Result()
        {
            custom1_type = "t";
            custom2_type = "t";
            custom3_type = "t";
            custom4_type = "t";
            custom5_type = "t";
            custom6_type = "t";
            custom7_type = "t";
            custom8_type = "t";
            custom9_type = "t";
            custom10_type = "t";
        }
        public string classification_name { get; set; }
        public bool summary_cb { get; set; }
        public bool enddate_cb { get; set; }
        public bool keywords_cb { get; set; }
        public bool custom1_cb { get; set; }
        public bool custom2_cb { get; set; }
        public bool custom3_cb { get; set; }
        public bool custom4_cb { get; set; }
        public bool custom5_cb { get; set; }
        public bool custom6_cb { get; set; }
        public bool custom7_cb { get; set; }
        public bool custom8_cb { get; set; }
        public bool custom9_cb { get; set; }
        public bool custom10_cb { get; set; }
        public bool custom11_cb { get; set; }
        public bool custom12_cb { get; set; }
        public bool custom13_cb { get; set; }
        public bool custom14_cb { get; set; }
        public bool custom15_cb { get; set; }
        public bool custom16_cb { get; set; }
        public bool custom17_cb { get; set; }
        public bool custom18_cb { get; set; }
        public bool custom19_cb { get; set; }
        public bool custom20_cb { get; set; }
        public bool date1_cb { get; set; }
        public bool date2_cb { get; set; }
        public bool date3_cb { get; set; }
        public bool date4_cb { get; set; }
        public bool date5_cb { get; set; }
        public string custom1_text { get; set; }
        public string custom2_text { get; set; }
        public string custom3_text { get; set; }
        public string custom4_text { get; set; }
        public string custom5_text { get; set; }
        public string custom6_text { get; set; }
        public string custom7_text { get; set; }
        public string custom8_text { get; set; }
        public string custom9_text { get; set; }
        public string custom10_text { get; set; }
        public string custom11_text { get; set; }
        public string custom12_text { get; set; }
        public string custom13_text { get; set; }
        public string custom14_text { get; set; }
        public string custom15_text { get; set; }
        public string custom16_text { get; set; }
        public string custom17_text { get; set; }
        public string custom18_text { get; set; }
        public string custom19_text { get; set; }
        public string custom20_text { get; set; }
        public string flag1_text { get; set; }
        public string flag2_text { get; set; }
        public string flag3_text { get; set; }
        public string flag4_text { get; set; }
        public string flag5_text { get; set; }
        public string date1_text { get; set; }
        public string date2_text { get; set; }
        public string date3_text { get; set; }
        public string date4_text { get; set; }
        public string date5_text { get; set; }
        public string custom1_type { get; set; }
        public string custom2_type { get; set; }
        public string custom3_type { get; set; }
        public string custom4_type { get; set; }
        public string custom5_type { get; set; }
        public string custom6_type { get; set; }
        public string custom7_type { get; set; }
        public string custom8_type { get; set; }
        public string custom9_type { get; set; }
        public string custom10_type { get; set; }
        public bool custom1_cb1 { get; set; }
        public bool custom2_cb1 { get; set; }
        public bool custom3_cb1 { get; set; }
        public bool custom4_cb1 { get; set; }
        public bool custom5_cb1 { get; set; }
        public bool custom6_cb1 { get; set; }
        public bool custom7_cb1 { get; set; }
        public bool custom8_cb1 { get; set; }
        public bool custom9_cb1 { get; set; }
        public bool custom10_cb1 { get; set; }
        public string summary_text { get; set; }
        public string enddate_text { get; set; }
        public string keywords_text { get; set; }
        public string article1_text { get; set; }
        public string article2_text { get; set; }
        public string article3_text { get; set; }
        public string article4_text { get; set; }
        public string article5_text { get; set; }
        public bool article1_cb { get; set; }
        public bool article2_cb { get; set; }
        public bool article3_cb { get; set; }
        public bool article4_cb { get; set; }
        public bool article5_cb { get; set; }
        public string date1_text1 { get; set; }
        public string date2_text1 { get; set; }
        public bool date1_cb1 { get; set; }
        public bool date2_cb1 { get; set; }
        public bool summary_cb1 { get; set; }
        public bool enddate_cb1 { get; set; }
        public bool keywords_cb1 { get; set; }
        public byte custom1_subcolumn { get; set; }
        public byte custom2_subcolumn { get; set; }
        public byte custom3_subcolumn { get; set; }
        public byte custom4_subcolumn { get; set; }
        public byte custom5_subcolumn { get; set; }
        public byte custom6_subcolumn { get; set; }
        public byte custom7_subcolumn { get; set; }
        public byte custom8_subcolumn { get; set; }
        public byte custom9_subcolumn { get; set; }
        public byte custom10_subcolumn { get; set; }
        public bool file_required_cb { get; set; }
        public bool file_title_required_cb { get; set; }
        public bool file_description_required_cb { get; set; }
        public string required_file_types { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_select_cc_details_Result
    {
        public int cc_id { get; set; }
        public string cc_name { get; set; }
        public string cc_html { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public object created_by { get; set; }
        public int updated_by { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_select_breadcrumb_Result
    {
        public int breadcrumb_id { get; set; }
        public string breadcrumb_name { get; set; }
        public byte deep_level { get; set; }
        public string include_site { get; set; }
        public string include_zonegroup { get; set; }
        public string include_headline { get; set; }
        public string publisher_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public object updated_by { get; set; }
        public string excluded_sites { get; set; }
        public string excluded_zonegroups { get; set; }
        public string excluded_zones { get; set; }
        public string seperator { get; set; }
        public string ul_class { get; set; }
        public string include_submenus { get; set; }
        public string breadcrumb_main_container { get; set; }
        public string breadcrumb_main_item_container { get; set; }
        public string breadcrumb_sub_container { get; set; }
        public string breadcrumb_sub_item_container { get; set; }
    }
    public partial class cms_asp_select_articles_by_zone_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string headline { get; set; }
        public string zone_name { get; set; }
        public string zone_group_name { get; set; }
        public string site_name { get; set; }
        public string menu_text { get; set; }
        public string az_alias { get; set; }
        public byte navigation_display { get; set; }
    }
    public class cms_asp_select_articles_by_zone_id_Result
    {
        public int article_id { get; set; }
        public int navigation_zone_id { get; set; }
        public int zone_id { get; set; }
        public string headline { get; set; }
        public string menu_text { get; set; }
        public object navigation_display { get; set; }
        public byte status { get; set; }
    }
    public partial class cms_asp_select_articles_by_zone_for_breadcrumb_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string headline { get; set; }
        public string zone_name { get; set; }
        public string zone_group_name { get; set; }
        public string site_name { get; set; }
        public string menu_text { get; set; }
        public string az_alias { get; set; }
        public byte navigation_display { get; set; }
    }
    public partial class cms_asp_select_article_zones_by_revision_Result
    {
        public int out_id { get; set; }
        public string out_name { get; set; }
        public int az_order { get; set; }
        public int zone_type_id { get; set; }
        public string az_alias { get; set; }
        public bool is_alias_protected { get; set; }
        public bool is_page { get; set; }
    }
    public partial class cms_asp_select_article_zones_by_article_Result
    {
        public int zone_id { get; set; }
        public int zone_type_id { get; set; }
        public object publisher_id { get; set; }
    }
    public partial class cms_asp_select_article_tags_Result
    {
        public int zone_id { get; set; }
        public string zone_name { get; set; }
        public string zg_tag_detail_article { get; set; }
        public string s_tag_detail_article { get; set; }
    }
    public partial class cms_asp_select_article_revision_details_Result
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
        public object publisher_id { get; set; }
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
        public int zone_publisher_id { get; set; }
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
        public Nullable<int> zg_publisher_id { get; set; }
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
        public Nullable<int> site_publisher_id { get; set; }
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
    public partial class cms_asp_select_article_relateds_by_revision_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string out_name { get; set; }
    }
    public partial class cms_asp_select_article_language_relations_Result
    {
        public int zone_id { get; set; }
        public int article_id { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string headline { get; set; }
        public string lang_name { get; set; }
        public string lang_id { get; set; }
        public Nullable<int> lang_order { get; set; }
        public string dir { get; set; }
        public string out_name { get; set; }
        public string az_alias { get; set; }
    }
    public partial class cms_asp_select_article_language_relations_by_revision_Result
    {
        public int zone_id { get; set; }
        public int article_id { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string headline { get; set; }
        public string lang_name { get; set; }
        public string lang_id { get; set; }
        public Nullable<int> lang_order { get; set; }
        public string dir { get; set; }
        public string out_name { get; set; }
    }

    public partial class cms_asp_select_article_files_Result
    {
        public long file_id { get; set; }
        public string file_title { get; set; }
        public int file_order { get; set; }
        public string file_name_1 { get; set; }
        public string file_name_2 { get; set; }
        public string file_name_3 { get; set; }
        public string file_name_4 { get; set; }
        public string file_name_5 { get; set; }
        public string file_name_6 { get; set; }
        public string file_name_7 { get; set; }
        public string file_name_8 { get; set; }
        public string file_name_9 { get; set; }
        public string file_name_10 { get; set; }
        public int file_type_id { get; set; }
        public string file_comment { get; set; }
        public string type_alias { get; set; }
    }
    public partial class cms_asp_select_article_details_Result
    {
        public int az_order { get; set; }
        public int zone_type_id { get; set; }
        public int article_id { get; set; }
        public int clsf_id { get; set; }
        public byte status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public object publisher_id { get; set; }
        public int clicks { get; set; }
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
        public object zone_publisher_id { get; set; }
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
        public object zg_publisher_id { get; set; }
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
        public object site_publisher_id { get; set; }
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
        public int rating { get; set; }
        public int ratingcount { get; set; }
        public string meta_description { get; set; }
        public string zone_meta_description { get; set; }
        public string zone_group_meta_description { get; set; }
        public string site_meta_description { get; set; }
        public string zone_group_name_display { get; set; }
        public string zone_name_display { get; set; }
        public string az_alias { get; set; }
        public string article_omniture_code { get; set; }
        public string zone_omniture_code { get; set; }
        public string zone_group_omniture_code { get; set; }
        public string site_omniture_code { get; set; }
        public string site_default_article { get; set; }
        public string zone_group_default_article { get; set; }
        public string zone_default_article { get; set; }
        public string custom_setting { get; set; }
        public string domain_name { get; set; }
    }
    public partial class cms_asp_select_article_details_for_breadcrumb_Result
    {
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_group_name_display { get; set; }
        public string zone_name { get; set; }
        public string zone_name_display { get; set; }
        public string headline { get; set; }
        public string menu_text { get; set; }
        public Nullable<int> site_id { get; set; }
        public int zone_group_id { get; set; }
        public int zone_id { get; set; }
        public int article_id { get; set; }
        public int navigation_zone_id { get; set; }
        public string az_alias { get; set; }
        public string zone_default_article { get; set; }
        public byte article_type { get; set; }
        public string article_type_detail { get; set; }
        public byte navigation_display { get; set; }
    }
    public partial class cms_asp_select_article_details_for_all_zones_Result
    {
        public int az_order { get; set; }
        public int zone_type_id { get; set; }
        public int article_id { get; set; }
        public int clsf_id { get; set; }
        public byte status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public object publisher_id { get; set; }
        public int clicks { get; set; }
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
        public int zone_publisher_id { get; set; }
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
        public Nullable<int> zg_publisher_id { get; set; }
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
        public Nullable<int> site_publisher_id { get; set; }
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
        public int rating { get; set; }
        public int ratingcount { get; set; }
        public string meta_description { get; set; }
        public string zone_meta_description { get; set; }
        public string zone_group_meta_description { get; set; }
        public string site_meta_description { get; set; }
        public string zone_group_name_display { get; set; }
        public string zone_name_display { get; set; }
        public string az_alias { get; set; }
        public string article_omniture_code { get; set; }
        public string zone_omniture_code { get; set; }
        public string zone_group_omniture_code { get; set; }
        public string site_omniture_code { get; set; }
        public string site_default_article { get; set; }
        public string zone_group_default_article { get; set; }
        public string zone_default_article { get; set; }
        public string custom_setting { get; set; }
        public string domain_name { get; set; }
    }
    public partial class cms_asp_select_article_by_alias_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string headline { get; set; }
        public Nullable<int> domain_id { get; set; }
    }
    public partial class cms_asp_select_all_zones_Result
    {
        public int zone_id { get; set; }
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
    }
    public partial class cms_asp_select_all_templates_Result
    {
        public int template_id { get; set; }
        public string template_name { get; set; }
        public string template_html { get; set; }
        public string template_doctype { get; set; }
    }
    public partial class cms_asp_select_all_portlets_Result
    {
        public int portlet_id { get; set; }
        public string portlet_name { get; set; }
        public string portlet_html { get; set; }
        public string portlet_css { get; set; }
        public string portlet_header { get; set; }
        public string portlet_footer { get; set; }
        public string enable_shortcut { get; set; }
    }
    public partial class cms_asp_select_all_plugins_Result
    {
        public int plugin_id { get; set; }
        public string plugin_name { get; set; }
        public string plugin_code { get; set; }
    }
    public partial class cms_asp_select_all_css_Result
    {
        public int css_id { get; set; }
        public string css_name { get; set; }
        public string css_code { get; set; }
        public string css_fix { get; set; }
        public string css_rel_text { get; set; }
        public string css_type_text { get; set; }
    }
    public partial class cms_asp_mbuilder_select_upper_level_azid_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
    }
    public partial class cms_asp_login_select_publisher_by_username_Result
    {
        public object publisher_id { get; set; }
        public string publisher_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string publisher_status { get; set; }
        public string publisher_email { get; set; }
        public byte publisher_level { get; set; }
        public string publisher_department { get; set; }
        public string publisher_note { get; set; }
        public int publisher_zg { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public object created_by { get; set; }
        public int updated_by { get; set; }
    }
    public partial class cms_asp_im_open_im_Result
    {
        public long ims_id { get; set; }
        public int ims_from { get; set; }
        public int ims_to { get; set; }
        public string ims_subject { get; set; }
        public string ims_message { get; set; }
        public string ims_type { get; set; }
        public long related_id { get; set; }
        public string related_name { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> readed { get; set; }
        public Nullable<System.DateTime> processed { get; set; }
        public Nullable<System.DateTime> deleted { get; set; }
        public Nullable<System.DateTime> due { get; set; }
        public string from_name { get; set; }
        public string to_name { get; set; }
    }
    public partial class cms_asp_im_insert_im_Result
    {
        public string im_stat { get; set; }
        public string im_id { get; set; }
        public string publisher_name { get; set; }
        public string publisher_email { get; set; }
    }
    public partial class cms_asp_im_check_im_Result
    {
        public Nullable<int> ims_count { get; set; }
        public Nullable<int> due_count { get; set; }
    }
    public partial class cms_asp_errors
    {
        public int ID { get; set; }
        public string MonitorSent { get; set; }
        public string SessionID { get; set; }
        public string RequestMethod { get; set; }
        public string ServerPort { get; set; }
        public string HTTPS { get; set; }
        public string LocalAddr { get; set; }
        public string HostAddress { get; set; }
        public string UserAgent { get; set; }
        public string URL { get; set; }
        public string CustomerRefID { get; set; }
        public string FormData { get; set; }
        public string AllHTTP { get; set; }
        public string ErrASPCode { get; set; }
        public string ErrNumber { get; set; }
        public string ErrSource { get; set; }
        public string ErrCategory { get; set; }
        public string ErrFile { get; set; }
        public Nullable<int> ErrLine { get; set; }
        public Nullable<int> ErrColumn { get; set; }
        public string ErrDescription { get; set; }
        public string ErrAspDescription { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string AAS_Checked { get; set; }
    }
    public partial class cms_asp_copy_article_files_revision_files_Result
    {
        public string aStat { get; set; }
    }
    public partial class cms_asp_config_select_config_parameters_Result
    {
        public int config_id { get; set; }
        public string config_name { get; set; }
        public string config_value_local { get; set; }
        public string config_value_remote { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_asp_config_check_done_status_Result
    {
        public Nullable<int> publishers { get; set; }
        public Nullable<int> languages { get; set; }
        public Nullable<int> css { get; set; }
        public Nullable<int> templates { get; set; }
        public Nullable<int> sites { get; set; }
        public Nullable<int> zone_groups { get; set; }
        public Nullable<int> cache_update { get; set; }
        public string status { get; set; }
    }
    public partial class cms_asp_approval_approve_zone_revision_Result
    {
        public string aStat { get; set; }
        public string found_name { get; set; }
        public string rStat { get; set; }
        public string tStat { get; set; }
        public object rev_id { get; set; }
        public object locked { get; set; }
        public string locked_by { get; set; }
    }
    public class cms_asp_approval_approve_article_revision_Result
    {
        public string aStat { get; set; }
        public string found_name { get; set; }
        public string rStat { get; set; }
        public string tStat { get; set; }
        public object rev_id { get; set; }
        public object locked { get; set; }
        public string locked_by { get; set; }
    }
    public partial class cms_asp_select_redirection_details_Result
    {
        public int ID { get; set; }
        public string RedirectFrom { get; set; }
        public string RedirectTo { get; set; }
        public object CreatedBy { get; set; }
        public object UpdatedBy { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }

    }
    public partial class cms_asp_admin_update_redirection_Result
    {
        public string dStat { get; set; }
        public object ID { get; set; }
    }
    public partial class cms_page_redirection
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Redirection From is required!")]
        public string RedirectFrom { get; set; }
        [Required(ErrorMessage = "Redirection To is required!")]
        public string RedirectTo { get; set; }

        public System.DateTime Created { get; set; }
        [Required(ErrorMessage = "Created by is required!")]
        public object CreatedBy { get; set; }
        public System.DateTime Updated { get; set; }
        public object UpdatedBy { get; set; }

    }
    public partial class cms_asp_select_redirection_Result
    {
        public int ID { get; set; }
        public string RedirectFrom { get; set; }
        public string RedirectTo { get; set; }
        public object CreatedBy { get; set; }
        public object UpdatedBy { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_approval_approve_article_files_revision_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
        public string aStat { get; set; }
    }
    public partial class cms_asp_admin_update_zonegroups_Result
    {
        public string zgStat { get; set; }
        public object zone_group_id { get; set; }
        public string site_name { get; set; }
        public object created { get; set; }
    }
    public partial class cms_asp_admin_update_zone_revision_Result
    {
        public string zstat { get; set; }
        public string rstat { get; set; }
        public int zone_id { get; set; }
        public long rev_id { get; set; }
        public object locked { get; set; }
        public object locked_by { get; set; }
    }
    public partial class cms_asp_admin_update_xml_details_Result
    {
        public string xStat { get; set; }
        public object xml_id { get; set; }
        public object created { get; set; }
    }
    public partial class cms_asp_admin_update_template_Result
    {
        public string tStat { get; set; }
        public object template_id { get; set; }
        public object created { get; set; }
    }
    public partial class cms_asp_admin_update_structure_group_Result
    {
        public string rCode { get; set; }
    }
    public partial class cms_asp_admin_update_stf_template_Result
    {
        public string tStat { get; set; }
        public Nullable<int> stft_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_asp_admin_update_sitemap_status_Result
    {
        public string rCode { get; set; }
        public Nullable<System.DateTime> rDate { get; set; }
    }
    public partial class cms_asp_admin_update_site_Result
    {
        public string sStat { get; set; }
        public object site_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_asp_admin_update_rss_channel_Result
    {
        public string cStat { get; set; }
        public int channel_id { get; set; }
        public Nullable<DateTime> created { get; set; }
    }
    public partial class cms_asp_admin_update_redirection_details_Result
    {
        public string rStat { get; set; }
        public object redirect_id { get; set; }
        public object created { get; set; }
    }
    public partial class cms_asp_admin_update_publisher_details_Result
    {
        public string pStat { get; set; }
        public object publisher_id { get; set; }
        public object created { get; set; }
    }
    public partial class cms_asp_admin_update_portlet_Result
    {
        public string pStat { get; set; }
        public object portlet_id { get; set; }
        public object updated { get; set; }
    }
    public partial class cms_asp_admin_update_plugin_Result
    {
        public string pStat { get; set; }
        public Nullable<int> plugin_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_asp_admin_update_language_Result
    {
        public string lStat { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_asp_admin_update_hidden_values_Result
    {
        public string dStat { get; set; }
        public object hidden_id { get; set; }
        public object updated { get; set; }
    }
    public partial class cms_asp_admin_update_file_type_Result
    {
        public string sStat { get; set; }
        public object type_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_asp_admin_update_domains_Result
    {
        public string dStat { get; set; }
        public object domain_id { get; set; }
        public System.DateTime updated { get; set; }
    }
    public partial class cms_asp_admin_update_css_Result
    {
        public string cStat { get; set; }
        public object css_id { get; set; }
        public object created { get; set; }
    }
    public partial class cms_asp_admin_update_config_parameter_Result
    {
        public string cStat { get; set; }
        public object config_id { get; set; }
        public object updated { get; set; }
    }
    public partial class cms_asp_admin_update_classification_details_Result
    {
        public string cStat { get; set; }
        public int classification_id { get; set; }
        public Nullable<DateTime> created { get; set; }
    }
    public partial class cms_asp_admin_update_classification_combo_values_Result
    {
        public string cStat { get; set; }
        public Nullable<int> classification_id { get; set; }
        public System.DateTime created { get; set; }
    }
    public partial class cms_asp_admin_update_cc_Result
    {
        public string cStat { get; set; }
        public object cc_id { get; set; }
        public object updated { get; set; }
    }
    public partial class cms_asp_admin_update_article_revision_Result
    {
        public string astat { get; set; }
        public string rstat { get; set; }
        public Nullable<int> article_id { get; set; }
        public Nullable<long> rev_id { get; set; }
        public Nullable<System.DateTime> locked { get; set; }
        public string locked_by { get; set; }
    }
    public partial class cms_asp_admin_update_article_files_revision_Result
    {
        public string fstat { get; set; }
        public string rstat { get; set; }
        public Nullable<long> af_rf_id { get; set; }
        public Nullable<long> rev_id { get; set; }
    }
    public partial class cms_asp_admin_select_zone_revision_list_Result
    {
        public long rev_id { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public string zone_status { get; set; }
        public string rev_name { get; set; }
        public string revised_name { get; set; }
        public string approval_name { get; set; }
    }
    public partial class cms_asp_admin_select_zone_revision_details_Result
    {
        public cms_asp_admin_select_zone_revision_details_Result()
        {
            rev_id = -1;
            revision_status = "";
            zone_id = 0;
            zone_name = "";
            zone_desc = "";
            zone_keywords = "";

            css_merge = 0;
            css_id = 0;
            template_id = 0;
            zone_group_id = 0;
            zone_status = "A";
            current_status = "";
            css_id_mobile = 0;
            css_id_print = 0;
            template_id_mobile = 0;
            custom_body = "";
            append_1 = (byte)0;
            append_2 = (byte)0;
            append_3 = (byte)0;
            append_4 = (byte)0;
            append_5 = (byte)0;
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";

            rev_name = "";
            rev_note = "";
            zone_type_id = 0;
            analytics = "";
            revised_name = "";
            approval_name = "";
            publisher_name = "";
            meta_description = "";
            zone_name_display = "";
            content_1_editor_type = "H";
            content_2_editor_type = "H";
            content_3_editor_type = "H";
            content_4_editor_type = "H";
            content_5_editor_type = "H";
            default_article = "";
            omniture_code = "";
            lang_id = "";
        }

        public long rev_id { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public int zone_id { get; set; }
        public string zone_name { get; set; }
        public string zone_desc { get; set; }
        public string zone_keywords { get; set; }
        public object revised_by { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public object approval_id { get; set; }
        public int css_merge { get; set; }
        public int css_id { get; set; }
        public int template_id { get; set; }
        public int zone_group_id { get; set; }
        public string zone_status { get; set; }
        public string current_status { get; set; }
        public int css_id_mobile { get; set; }
        public int css_id_print { get; set; }
        public int template_id_mobile { get; set; }
        public string custom_body { get; set; }
        public byte append_1 { get; set; }
        public byte append_2 { get; set; }
        public byte append_3 { get; set; }
        public byte append_4 { get; set; }
        public byte append_5 { get; set; }
        public string article_1 { get; set; }
        public string article_2 { get; set; }
        public string article_3 { get; set; }
        public string article_4 { get; set; }
        public string article_5 { get; set; }
        public object publisher_id { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public string rev_name { get; set; }
        public string rev_note { get; set; }
        public int zone_type_id { get; set; }
        public string analytics { get; set; }
        public string revised_name { get; set; }
        public string approval_name { get; set; }
        public string publisher_name { get; set; }
        public string meta_description { get; set; }
        public string zone_name_display { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string default_article { get; set; }
        public string omniture_code { get; set; }
        public string lang_id { get; set; }
    }
    public partial class cms_asp_admin_select_url_redirects_Result
    {
        public int redirect_id { get; set; }
        public string redirect_alias { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
        public string full_url { get; set; }
    }
    public partial class cms_asp_admin_select_templates_Result
    {
        public int template_id { get; set; }
        public string template_name { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string publisher_name { get; set; }
        public byte template_type { get; set; }
        public int group_id { get; set; }
        public string group_name { get; set; }
    }
    public partial class cms_asp_admin_select_template_revisions_Result
    {
        public int history_id { get; set; }
        public int template_id { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_admin_select_structure_group_Result
    {
        public int group_id { get; set; }
        public byte group_type { get; set; }
        public string group_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public string publisher_name { get; set; }
        public object publisher_id { get; set; }
    }
    public partial class cms_asp_admin_select_stf_templates_Result
    {
        public int stft_id { get; set; }
        public string stft_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime updated { get; set; }
        public int updated_by { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_admin_select_sitemaps_Result
    {
        public string domain_names { get; set; }
        [Key]
        public int smap_id { get; set; }
        public int domain_id { get; set; }
        public string domain_alias { get; set; }
        public byte status { get; set; }
        public Nullable<System.DateTime> last_update { get; set; }
        public Nullable<System.DateTime> last_generate { get; set; }
        public string notify_google { get; set; }
        public string notify_msn { get; set; }
        public string notify_ask { get; set; }
        public string notify_yahoo { get; set; }
        public string yahoo_id { get; set; }
        public string included_sites { get; set; }
        public string excluded_zonegroups { get; set; }
        public string excluded_zones { get; set; }
        public string excluded_articles { get; set; }
        public string afiles { get; set; }
        public int interval { get; set; }
        public string enabled { get; set; }
        public System.DateTime created { get; set; }
        public string gzip_enabled { get; set; }
    }
    public partial class cms_asp_admin_select_sitemap_status_Result
    {
        public string rCode { get; set; }
        public int smap_id { get; set; }
        public int domain_id { get; set; }
        public string domain_alias { get; set; }
        public byte status { get; set; }
        public Nullable<System.DateTime> last_update { get; set; }
        public Nullable<System.DateTime> last_generate { get; set; }
        public Nullable<System.DateTime> last_generate_start { get; set; }
        public string notify_google { get; set; }
        public string notify_msn { get; set; }
        public string notify_ask { get; set; }
        public string notify_yahoo { get; set; }
        public string yahoo_id { get; set; }
        public string included_sites { get; set; }
        public string excluded_zonegroups { get; set; }
        public string excluded_zones { get; set; }
        public string excluded_articles { get; set; }
        public string afiles { get; set; }
        public int interval { get; set; }
        public string enabled { get; set; }
        public string xml { get; set; }
        public byte[] gz { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public Nullable<int> updated_by { get; set; }
        public string gzip_enabled { get; set; }
    }
    public partial class cms_asp_admin_select_sitemap_by_domain_Result
    {
        public string xml { get; set; }
        public byte[] gz { get; set; }
        public string gzip_enabled { get; set; }
    }
    public partial class cms_asp_admin_select_rss_channel_content_Result
    {
        public string out_type { get; set; }
        public int out_id { get; set; }
        public string out_name { get; set; }
        public string sgz_exclude { get; set; }
    }
    public partial class cms_asp_admin_select_required_file_columns_Result
    {
        public Nullable<bool> file_title_required { get; set; }
        public Nullable<bool> file_description_required { get; set; }
    }
    public partial class cms_asp_admin_select_redirection_detail_Result
    {
        public int redirect_id { get; set; }
        public string redirect_alias { get; set; }
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string out_name { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
        public bool permanent_redirection { get; set; }
    }
    public partial class cms_asp_admin_select_fop_failures_Result
    {
        public int log_id { get; set; }
        public string op_action { get; set; }
        public string source_path { get; set; }
        public string dest_path { get; set; }
        public string file_name { get; set; }
    }
    public partial class cms_asp_admin_select_file_type_details_Result
    {
        public int type_id { get; set; }
        public string type_name { get; set; }
        public string type_alias { get; set; }
        public string file1_name { get; set; }
        public string file2_name { get; set; }
        public string file3_name { get; set; }
        public string file4_name { get; set; }
        public string file5_name { get; set; }
        public string file6_name { get; set; }
        public string file7_name { get; set; }
        public string file8_name { get; set; }
        public string file9_name { get; set; }
        public string file10_name { get; set; }
        public string file1_extension { get; set; }
        public string file2_extension { get; set; }
        public string file3_extension { get; set; }
        public string file4_extension { get; set; }
        public string file5_extension { get; set; }
        public string file6_extension { get; set; }
        public string file7_extension { get; set; }
        public string file8_extension { get; set; }
        public string file9_extension { get; set; }
        public string file10_extension { get; set; }
        public string file1_wh { get; set; }
        public string file2_wh { get; set; }
        public string file3_wh { get; set; }
        public string file4_wh { get; set; }
        public string file5_wh { get; set; }
        public string file6_wh { get; set; }
        public string file7_wh { get; set; }
        public string file8_wh { get; set; }
        public string file9_wh { get; set; }
        public string file10_wh { get; set; }
        public int file1_size { get; set; }
        public int file2_size { get; set; }
        public int file3_size { get; set; }
        public int file4_size { get; set; }
        public int file5_size { get; set; }
        public int file6_size { get; set; }
        public int file7_size { get; set; }
        public int file8_size { get; set; }
        public int file9_size { get; set; }
        public int file10_size { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public int group_id { get; set; }
        public string structure_description { get; set; }
    }
    public partial class cms_asp_admin_select_css_revisions_Result
    {
        public int history_id { get; set; }
        public int css_id { get; set; }
        public object publisher_id { get; set; }
        public System.DateTime created { get; set; }
        public string publisher_name { get; set; }
    }
    public partial class cms_asp_admin_select_cached_articles_Result
    {
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string out_name { get; set; }
    }
    public partial class cms_asp_admin_select_az_check_Result
    {
        public string rType { get; set; }
        public string rDescription { get; set; }
        public long rID { get; set; }
    }
    public partial class cms_asp_admin_select_article_revision_list_Result
    {
        public long rev_id { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public byte status { get; set; }
        public string rev_name { get; set; }
        public string revised_name { get; set; }
        public string approval_name { get; set; }
    }
    public partial class cms_asp_admin_select_article_revision_details_Result
    {
        public cms_asp_admin_select_article_revision_details_Result()
        {
            keywords = "";
            custom_setting = "";
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";
            content_1_editor_type = "";
            content_2_editor_type = "";
            content_3_editor_type = "";
            content_4_editor_type = "";
            content_5_editor_type = "";
        }
        public long rev_id { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public object revised_by { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public object approval_id { get; set; }
        public byte current_status { get; set; }
        public int article_id { get; set; }
        public int clsf_id { get; set; }
        public byte status { get; set; }
        public System.DateTime startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public int orderno { get; set; }
        public string lang_id { get; set; }
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
        public bool rev_flag_1 { get; set; }
        public bool rev_flag_2 { get; set; }
        public bool rev_flag_3 { get; set; }
        public bool rev_flag_4 { get; set; }
        public bool rev_flag_5 { get; set; }
        public byte cl_1 { get; set; }
        public byte cl_2 { get; set; }
        public byte cl_3 { get; set; }
        public byte cl_4 { get; set; }
        public byte cl_5 { get; set; }
        public string custom_body { get; set; }
        public object publisher_id { get; set; }
        public DateTime created { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public string rev_name { get; set; }
        public string rev_note { get; set; }
        public byte navigation_display { get; set; }
        public int navigation_zone_id { get; set; }
        public string menu_text { get; set; }
        public string revised_name { get; set; }
        public string approval_name { get; set; }
        public string publisher_name { get; set; }
        public string meta_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string omniture_code { get; set; }
        public string custom_setting { get; set; }
        public string cio { get; set; }
    }
    public partial class cms_asp_admin_select_article_last_revision_Result
    {

        public long rev_id { get; set; }
    }
    public partial class cms_asp_admin_select_article_files_revision_list_Result
    {
        public long rev_id { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public string revised_name { get; set; }
        public string approval_name { get; set; }
    }
    public partial class cms_asp_admin_select_article_files_revision_files_Result
    {
        public long af_rf_id { get; set; }
        public long rev_id { get; set; }
        public int article_id { get; set; }
        public string file_title { get; set; }
        public int file_order { get; set; }
        public string file_name_1 { get; set; }
        public string file_name_2 { get; set; }
        public string file_name_3 { get; set; }
        public string file_name_4 { get; set; }
        public string file_name_5 { get; set; }
        public string file_name_6 { get; set; }
        public string file_name_7 { get; set; }
        public string file_name_8 { get; set; }
        public string file_name_9 { get; set; }
        public string file_name_10 { get; set; }
        public int file_type_id { get; set; }
        public string file_comment { get; set; }
        public string revision_status { get; set; }
        public string type_name { get; set; }
        public Nullable<int> type_id { get; set; }
    }
    public partial class cms_asp_admin_delete_hidden_value_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_select_article_files_revision_file_details_Result
    {
        public int file_type_id { get; set; }
        public string file_title { get; set; }
        public int file_order { get; set; }
        public string file_comment { get; set; }
        public string file_name_1 { get; set; }
        public string file_name_2 { get; set; }
        public string file_name_3 { get; set; }
        public string file_name_4 { get; set; }
        public string file_name_5 { get; set; }
        public string file_name_6 { get; set; }
        public string file_name_7 { get; set; }
        public string file_name_8 { get; set; }
        public string file_name_9 { get; set; }
        public string file_name_10 { get; set; }
        public string revision_status { get; set; }
    }
    public partial class cms_asp_admin_select_article_files_last_revision_forced_Result
    {
        public long rev_id { get; set; }
        public string revision_status { get; set; }
    }
    public partial class cms_asp_admin_delete_zonegroup_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_zone_Result
    {
        public string aStat { get; set; }
        public string found_name { get; set; }
        public object rev_id { get; set; }
    }
    public partial class cms_asp_admin_delete_xml_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_template_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_stf_template_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_site_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_rss_channel_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_redirection_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_publisher_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_portlet_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_plugin_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_language_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_domain_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_css_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_config_parameter_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_classification_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_cc_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
    }
    public partial class cms_asp_admin_delete_article_Result
    {
        public string rCode { get; set; }
        public string found_name { get; set; }
        public Nullable<int> rev_id { get; set; }
    }
    public partial class cms_asp_admin_delete_article_files_revision_file_Result
    {
        public string rCode { get; set; }

    }
    public partial class vArticlesZones
    {
        public string site_name { get; set; }
        public string zone_group_name { get; set; }
        public string zone_name { get; set; }
        public string headline { get; set; }
        public string menu_text { get; set; }
        public int article_id { get; set; }
    }
    public partial class cms_articles
    {
        public cms_articles()
        {
            zones = new List<cms_zones>();
        }

        [Key]
        public int article_id { get; set; }
        public int clsf_id { get; set; }
        public byte status { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public object publisher_id { get; set; }
        public int clicks { get; set; }
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
        public string custom_body { get; set; }
        public int rating { get; set; }
        public int ratingcount { get; set; }
        public Nullable<System.DateTime> locked { get; set; }
        public Nullable<int> locked_by { get; set; }
        public string meta_description { get; set; }
        public string omniture_code { get; set; }
        public string custom_setting { get; set; }

        public List<cms_zones> zones { get; set; }
    }
    public partial class cms_article_zones_revision
    {
        public long rev_id { get; set; }
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public int az_order { get; set; }
        public string az_alias { get; set; }
    }
    public partial class cms_article_zones
    {
        public long rel_id { get; set; }
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public int az_order { get; set; }
        public string az_alias { get; set; }
    }
    public partial class cms_article_search_result
    {
        public int az_order { get; set; }
        public int article_id { get; set; }
        public long rev_id { get; set; }
        public string rev_name { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime rev_date { get; set; }
        public object revised_by { get; set; }
        public string menu_text { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }
        public System.DateTime startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public object approval_id { get; set; }
        public string revision_status { get; set; }
        public byte status { get; set; }
        public int clicks { get; set; }
        public string zone_name { get; set; }
        public int zone_id { get; set; }
        public Nullable<int> locked_by { get; set; }
        public Nullable<System.DateTime> locked { get; set; }
        public string publisher_name { get; set; }
        public string az_alias { get; set; }
    }
    public partial class cms_article_search
    {
        public int search_id { get; set; }
        public int article_id { get; set; }
        public int zone_id { get; set; }
        public string search_text { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
    }
    public partial class cms_article_revision
    {
        public cms_article_revision()
        {
            keywords = "";
            custom_setting = "";
            article_1 = "";
            article_2 = "";
            article_3 = "";
            article_4 = "";
            article_5 = "";
            content_1_editor_type = "H";
            content_2_editor_type = "H";
            content_3_editor_type = "H";
            content_4_editor_type = "H";
            content_5_editor_type = "H";
            navigation_display = null;
            status = null;
        }
        public long rev_id { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public object revised_by { get; set; }
        public string rev_name { get; set; }
        public string rev_note { get; set; }
        public bool rev_flag_1 { get; set; }
        public bool rev_flag_2 { get; set; }
        public bool rev_flag_3 { get; set; }
        public bool rev_flag_4 { get; set; }
        public bool rev_flag_5 { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public object approval_id { get; set; }
        public int article_id { get; set; }
        public int clsf_id { get; set; }
        public byte? status { get; set; }

        public System.DateTime startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public int orderno { get; set; }
        public string lang_id { get; set; }
        public byte? navigation_display { get; set; }
        public int navigation_zone_id { get; set; }
        public string menu_text { get; set; }
        public string headline { get; set; }
        public string summary { get; set; }
        public string keywords { get; set; }
        public byte article_type { get; set; }
        public string article_type_detail { get; set; }
        public string article_type_detail_text { get; set; }
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
        public string custom_body { get; set; }
        public string meta_description { get; set; }
        public string content_1_editor_type { get; set; }
        public string content_2_editor_type { get; set; }
        public string content_3_editor_type { get; set; }
        public string content_4_editor_type { get; set; }
        public string content_5_editor_type { get; set; }
        public string omniture_code { get; set; }
        public string custom_setting { get; set; }
        public string cio { get; set; }
        public string before_head { get; set; }
        public string before_body { get; set; }
        public bool no_index_no_follow { get; set; }
        public string canonical_url { get; set; }
        public string custom_html_attr { get; set; }
        public string meta_title { get; set; }
        public string alias { get; set; }
        public string tag_ids { get; set; }
        public string tag_contents { get; set; }
        public string afterbody { get; set; }
        public bool hidesuffix { get; set; }
        public bool hideprefix { get; set; }
    }
    public partial class cms_article_relation_revision
    {
        public long arr_id { get; set; }
        public long rev_id { get; set; }
        public int article_id { get; set; }
        public int related_zone_id { get; set; }
        public int related_article_id { get; set; }
    }
    public partial class cms_article_relation
    {
        public long ar_id { get; set; }
        public int article_id { get; set; }
        public int related_zone_id { get; set; }
        public int related_article_id { get; set; }
    }
    public partial class cms_article_files_revision_files
    {
        public long af_rf_id { get; set; }
        public long rev_id { get; set; }
        public int article_id { get; set; }
        public string file_title { get; set; }
        public int file_order { get; set; }
        public string file_name_1 { get; set; }
        public string file_name_2 { get; set; }
        public string file_name_3 { get; set; }
        public string file_name_4 { get; set; }
        public string file_name_5 { get; set; }
        public string file_name_6 { get; set; }
        public string file_name_7 { get; set; }
        public string file_name_8 { get; set; }
        public string file_name_9 { get; set; }
        public string file_name_10 { get; set; }
        public string type_name { get; set; }
        public int file_type_id { get; set; }
        public string file_comment { get; set; }
    }
    public partial class cms_article_files_revision
    {
        [Key]
        public long rev_id { get; set; }
        public System.DateTime created { get; set; }
        public object created_by { get; set; }
        public System.DateTime rev_date { get; set; }
        public string revision_status { get; set; }
        public object revised_by { get; set; }
        public Nullable<System.DateTime> approval_date { get; set; }
        public object approval_id { get; set; }
        public int article_id { get; set; }
    }
    public partial class cms_article_cache
    {
        public long cache_id { get; set; }
        public int zone_id { get; set; }
        public int article_id { get; set; }
        public Nullable<System.DateTime> created { get; set; }
    }
    public partial class cms_article_files
    {
        [Key]
        public long file_id { get; set; }
        public int article_id { get; set; }
        public string file_title { get; set; }
        public int file_order { get; set; }
        public string file_name_1 { get; set; }
        public string file_name_2 { get; set; }
        public string file_name_3 { get; set; }
        public string file_name_4 { get; set; }
        public string file_name_5 { get; set; }
        public string file_name_6 { get; set; }
        public string file_name_7 { get; set; }
        public string file_name_8 { get; set; }
        public string file_name_9 { get; set; }
        public string file_name_10 { get; set; }
        public int file_type_id { get; set; }
        public string file_comment { get; set; }
    }
}
