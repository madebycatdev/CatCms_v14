using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroCMS.Core
{
    public class GlobalVars : IDisposable
    {

        private bool IsDisposed = false;

        public int pubLevel = 100;
        public object pubID;
        public int pubZGID = 0;

        public int pPageCurrent = 0;
        public int pPageCount = 10;
        public int pRecordsCount = 0;
        public int page_start = 0;
        public int page_end = 0;

        public int bc_lastStep = 0; // Bread crumb last step

        public int breadcrumb_deep_level = 0;

        public string[,] BreadCrumbItems = new string[30,7]; //Second index items| 0: article_id, 1: zone_id, 2: zone_group_id, 3: site_id, 4: article link, 5: menu text (or headline), 6: navigation_display
 
        public SortedList<string, string> CACHED_PAGES_UPDATE_TIMES = new SortedList<string, string>(10000);
        public string pubName = string.Empty;
        public string pubEmail = string.Empty;
        public string pubDept = string.Empty;

        public bool IsMobile = false;

        public string DebugSQL = string.Empty;

        public string fullAlias = string.Empty;

        public string[] QS;

        public string Today 
        {
            get 
            {
                return CmsHelper.dDay(DateTime.Now.Day.ToString()) + "/" + CmsHelper.dDay(DateTime.Now.Month.ToString()) + "/" + CmsHelper.dDay(DateTime.Now.Year.ToString());
            }
        }

        public string TodayTime
        {
            get
            {
                return CmsHelper.dDay(DateTime.Now.Hour.ToString()) + ":" + CmsHelper.dDay(DateTime.Now.Minute.ToString());
            }
        }

        public string TodayFull
        {
            get
            {
                return Today + " " + CmsHelper.dDay(DateTime.Now.Hour.ToString()) + ":" + CmsHelper.dDay(DateTime.Now.Minute.ToString()) + ":" + CmsHelper.dDay(DateTime.Now.Second.ToString());
            }
        }
             
        public string[] eb = new string[6];

        public Dictionary<string, object> a = new Dictionary<string, object>();
        private Dictionary<string, object> b = new Dictionary<string, object>();


        public Dictionary<string, object> h = new Dictionary<string, object>();
        

        public GlobalVars()
        { 
            a.Add("article_type", "");
            a.Add("article_type_detail", "");
            a.Add("zone_id", "");
            a.Add("article_id", "");
            a.Add("site_name", "");
            a.Add("site_name_backup", "");
            a.Add("zone_group_name", "");
            a.Add("zone_group_name_backup", "");
            a.Add("zone_name", "");
            a.Add("zone_name_backup", "");
            a.Add("listed_zone_name", "");
            a.Add("zone_name_display", "");
            a.Add("zone_group_name_display", "");
            a.Add("site_name_display", "");
            a.Add("headline", "");
            a.Add("publisher_id", "");
            a.Add("az_alias", "");
            a.Add("menu_text", "");
            a.Add("custom_1", "");
            a.Add("custom_2", "");
            a.Add("custom_3", "");
            a.Add("custom_4", "");
            a.Add("custom_5", "");
            a.Add("custom_6", "");
            a.Add("custom_7", "");
            a.Add("custom_8", "");
            a.Add("custom_9", "");
            a.Add("custom_10", "");
            a.Add("custom_11", "");
            a.Add("custom_12", "");
            a.Add("custom_13", "");
            a.Add("custom_14", "");
            a.Add("custom_15", "");
            a.Add("custom_16", "");
            a.Add("custom_17", "");
            a.Add("custom_18", "");
            a.Add("custom_19", "");
            a.Add("custom_20", "");
            a.Add("article_1", "");
            a.Add("article_2", "");
            a.Add("article_3", "");
            a.Add("article_4", "");
            a.Add("article_5", "");
            a.Add("date_1", "");
            a.Add("date_2", "");
            a.Add("date_3", "");
            a.Add("date_4", "");
            a.Add("date_5", "");
            a.Add("summary", "");
            a.Add("mapped_article_url", "");
            a.Add("menu_text_headline", "");
            a.Add("custom_setting", "");
            a.Add("zone_default_article", "");
            a.Add("zone_group_default_article", "");
            a.Add("site_default_article", "");
            a.Add("site_omniture_code", "");
            a.Add("zone_group_omniture_code", "");
            a.Add("zone_omniture_code", "");
            a.Add("article_omniture_code", "");
            a.Add("site_meta_description", "");
            a.Add("zone_group_meta_description", "");
            a.Add("zone_meta_description", "");
            a.Add("meta_description", "");
            a.Add("ratingcount", "");
            a.Add("rating", "");
            a.Add("zone_analytics", "");
            a.Add("zg_analytics", "");
            a.Add("site_analytics", "");
            a.Add("s_custom_body", "");
            a.Add("s_article_5", "");
            a.Add("s_article_4", "");
            a.Add("s_article_3", "");
            a.Add("s_article_2", "");
            a.Add("s_article_1", "");
            a.Add("site_updated", "");
            a.Add("site_created", "");
            a.Add("site_icon", "");
            a.Add("site_js", "");
            a.Add("site_header", "");
            a.Add("site_keywords", "");
            a.Add("site_publisher_id", "");
            a.Add("site_template_id_mobile", "");
            a.Add("site_template_id", "");
            a.Add("site_css_id_print", "");
            a.Add("site_css_id_mobile", "");
            a.Add("site_css_id", "");
            a.Add("zg_custom_body", "");
            a.Add("zg_append_5", "");
            a.Add("zg_append_4", "");
            a.Add("zg_append_3", "");
            a.Add("zg_append_2", "");
            a.Add("zg_append_1", "");
            a.Add("zg_article_5", "");
            a.Add("zg_article_4", "");
            a.Add("zg_article_3", "");
            a.Add("zg_article_2", "");
            a.Add("zg_article_1", "");
            a.Add("zg_updated", "");
            a.Add("zg_created", "");
            a.Add("zg_publisher_id", "");
            a.Add("zg_template_id_mobile", "");
            a.Add("zg_template_id", "");
            a.Add("zg_css_id_print", "");
            a.Add("zg_css_id_mobile", "");
            a.Add("zg_css_id", "");
            a.Add("zg_css_merge", "");
            a.Add("site_id", "");
            a.Add("zone_group_keywords", "");
            a.Add("zone_custom_body", "");
            a.Add("zone_updated", "");
            a.Add("zone_created", "");
            a.Add("zone_publisher_id", "");
            a.Add("append_4", "");
            a.Add("append_3", "");
            a.Add("append_2", "");
            a.Add("append_1", "");
            a.Add("zone_article_5", "");
            a.Add("zone_article_4", "");
            a.Add("zone_article_3", "");
            a.Add("zone_article_2", "");
            a.Add("zone_article_1", "");
            a.Add("zone_keywords", "");
            a.Add("zone_template_id_mobile", "");
            a.Add("zone_template_id", "");
            a.Add("zone_css_id_print", "");
            a.Add("zone_css_id_mobile", "");
            a.Add("zone_css_id", "");
            a.Add("zone_css_merge", "");
            a.Add("zone_desc", "");
            a.Add("zone_status", "");
            a.Add("zone_group_id", "");
            a.Add("a_custom_body", "");
            a.Add("cl_5", "");
            a.Add("cl_4", "");
            a.Add("cl_3", "");
            a.Add("cl_2", "");
            a.Add("cl_1", "");
            a.Add("flag_5", "");
            a.Add("flag_4", "");
            a.Add("flag_3", "");
            a.Add("flag_2", "");
            a.Add("flag_1", "");
            a.Add("keywords", "");
            a.Add("navigation_zone_id", "");
            a.Add("navigation_display", "");
            a.Add("lang_id", "");
            a.Add("orderno", "");
            a.Add("clicks", "");
            a.Add("enddate", "");
            a.Add("startdate", "");
            a.Add("updated", "");
            a.Add("created", "");
            a.Add("status", "");
            a.Add("clsf_id", "");
            a.Add("zone_type_id", "");
            a.Add("az_order", "");
            a.Add("domain_name", "");
            a.Add("page_title", "");
            a.Add("a_before_head", "");
            a.Add("a_before_body", "");
            a.Add("zone_before_head", "");
            a.Add("zone_before_body", "");
            a.Add("zg_before_head", "");
            a.Add("zg_before_body", "");
            a.Add("meta_title", "");
            a.Add("no_index_no_follow", "");
            a.Add("custom_html_attr", "");
            a.Add("canonical_url", "");

            a.Add("a_afterbody", "");
            a.Add("z_afterbody", "");
            a.Add("zg_afterbody", "");
            a.Add("site_afterbody", "");
            a.Add("site_suffix", "");
            a.Add("site_prefix", "");
            a.Add("a_hideprefix", 0);
            a.Add("a_hidesuffix", 0);

            foreach (string s in a.Keys)
            {
                b.Add(s + "_exist", "");
            }

            a = a.Concat(b).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            eb[0] = string.Empty;
            eb[1] = string.Empty;
            eb[2] = string.Empty;
            eb[3] = string.Empty;
            eb[4] = string.Empty;
            eb[5] = string.Empty;
        }

        public void setValue2Dic(string key, object value)
        {
            if (a.ContainsKey(key))
                a[key] = value;
            else
                a.Add(key, value);
        }

        public string getValue4Dic(string key)
        {
            if (a.ContainsKey(key))
                return a[key].ToString();

            return string.Empty;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

         ~GlobalVars()
        {
            Dispose(false);
        }
 
        protected virtual void Dispose(bool disposedStatus)
        {
             if (!IsDisposed)
             {
                 IsDisposed = true;
                 // Released unmanaged Resources
                 if (disposedStatus)
                 {
                     // Released managed Resources
                     pubLevel = 0;
                     pubID = 0;
                     pubZGID = 0;
                     pubName = string.Empty;
                     pubEmail = string.Empty;
                     pubDept = string.Empty;
                     DebugSQL = string.Empty;
                     fullAlias = string.Empty;
                     pPageCurrent = 0;
                     pPageCount = 10;
                     page_start = 0;
                     page_end = 0;

                     a.Clear();
                     a = null;
                 }
             }
         }
    }
}
