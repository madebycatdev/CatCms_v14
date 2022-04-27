using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroCMS.Core
{
    public sealed class Constansts
    {
 
        public const string DEBUG_SQL_KEY = "DEBUG_SQL";
        public const string CMS_COOKIE_NAME = "EUROCMS";
        public const string CMS_COOKIE_PUBLISHER_ID = "PUBLISHER_ID";
        public const string CMS_SESSION_PUBLISHER_ID = "EUROCMS_PUBLISHER_ID";
        public const string CMS_SESSION_PUBLISHER_NO = "EUROCMS_PUBLISHER_NO";
        public const string CMS_SESSION_PUBLISHER_LEVEL = "publisher_level";
        
        //Set Default Encryption Key
        public const string CMS_RC4_KEY = "#EuroMessage#CMSKey#";
        public const string CMS_LICENCE_RC4_KEY = "fEWrw2#$£#$t5TG3wy5h5@$@jh$ujh4qwujqw4jhu@q4ujh@@ujh$rykwe5uja";
        
        
        public const string ALIAS_CSS = "css";
        public const string ALIAS_XML = "xml";
        public const string ALIAS_FXML = "fxml";
        public const string ALIAS_WEB = "web";
        public const string ALIAS_CRON = "cmscron";
        public const string ALIAS_EMPTY = "";
        public const string ALIAS_SITEMAP = "sitemap";
        public const string ALIAS_RSS = "rss";
        public const string ALIAS_PLUGIN = "plugins";
        public const string ALIAS_CONTENT = "web";
        public const string ALIAS_STF = "stf";
        public const string ALIAS_STF_SEND = "stfsend";

        public const string DIR_CSS = "css";

        public const int MAX_CACHED_PAGES_COUNT = 10000;
        public const int MAX_CLEARABLE_ARTICLE_COUNT = 1000;
        public const int CACHE_CLEAR_TIMEOUT = 1;
        
        public const int CONST_MAX_MENU_DEPTH = 4;

        public const string CSRF_SECURITY_ENABLE = "N";

        public const string PCSS_BEGIN = "<!-- Portlet CSS -->";
        public const string PCSS_END = "<!-- EOF Portlet CSS -->";

        public const string CFG_CACHE_ACTIVE = "CFG_CACHE_ACTIVE";
        public const string CFG_404_ERROR_LOG = "CFG_404_ERROR_LOG";
        public const string CFG_404_ERROR_EXTENSIONS = "CFG_404_ERROR_EXTENSIONS";
        public const string CFG_ADMIN_CONTACT = "CFG_ADMIN_CONTACT";
        public const string CFG_HOMEPAGE_ = "CFG_HOMEPAGE_";
        public const string CFG_ERRORPAGE_ = "CFG_ERRORPAGE_";
        public const string CFG_AUTO_DAILY_RELOAD_CACHE = "CFG_AUTO_DAILY_RELOAD_CACHE";
        public const string CFG_AUTO_DAILY_RELOAD_CACHE_LAST_RELOAD = "CFG_AUTO_DAILY_RELOAD_CACHE_LAST_RELOAD";
        public const string CFG_PREVIEW_ALLOWED_IPS = "CFG_PREVIEW_ALLOWED_IPS";
        public const string CFG_PROXY_SERVER = "CFG_PROXY_SERVER";
        public const string CFG_PROXY_LOGIN = "CFG_PROXY_LOGIN";
        public const string CFG_PROXY_USERNAME = "CFG_PROXY_USERNAME";
        public const string CFG_PROXY_PASSWORD = "CFG_PROXY_PASSWORD";
        public const string CFG_CHARSET = "CFG_CHARSET";
        public const string CFG_CHILKAT_ZIP_KEY = "CFG_CHILKAT_ZIP_KEY";
        public const string CFG_TITLE_PREFIX = "CFG_TITLE_PREFIX";
        public const string CFG_TITLE_SUFFIX = "CFG_TITLE_SUFFIX";
        public const string CFG_DEBUG_MODE = "CFG_DEBUG_MODE";
        public const string CFG_CLEAR_EMPTY_LINES = "CFG_CLEAR_EMPTY_LINES";
        public const string CFG_CLEAR_TABS_AND_SPACES = "CFG_CLEAR_TABS_AND_SPACES";
        public const string CFG_OMNITURE_PAGE_CODE = "CFG_OMNITURE_PAGE_CODE";
        public const string CFG_OMNITURE_SCODE_FILENAME = "CFG_OMNITURE_SCODE_FILENAME";
        public const string CFG_OMNITURE_TESTNTARGET_FILENAME = "CFG_OMNITURE_TESTNTARGET_FILENAME";
        public const string CFG_NO_DEFAULT_META = "CFG_NO_DEFAULT_META";
        public const string CFG_REMOVE_EDITOR_LINKS = "CFG_REMOVE_EDITOR_LINKS";
        public const string CFG_BREADCRUMB_CACHE_ACTIVE = "CFG_BREADCRUMB_CACHE_ACTIVE";
        public const string CFG_ENABLE_MENU_MAINCONTENTLI = "CFG_ENABLE_MENU_MAINCONTENTLI";

        public const string CFG_HTML_MINIFY = "CFG_HTML_MINIFY";

        public const string HIDDEN_VALUES_VALUES = "HIDDEN_VALUES_VALUES";
        public const string HIDDEN_VALUES_TYPES = "HIDDEN_VALUES_TYPES";
            
        public const string CACHED_PAGES_COUNT = "CACHED_PAGES_COUNT";
        public const string CACHE_CLEAR_PROCESS_START = "CACHE_CLEAR_PROCESS_START";
        
        public const string CACHED_PAGES = "CACHED_PAGES";
        
        public const string SERVER_COMPUTER_NAME = "SERVER_COMPUTER_NAME";
        
        public const double SITEMAP_CREATE_TIMEOUT = 5; //minutes
        public const string LAST_UPDATE = "LAST_UPDATE";
        public const string PLUGIN_EXECUTER = "plugin_executer.asp";
    }
}
