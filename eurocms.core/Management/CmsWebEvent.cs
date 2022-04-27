using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Management;
using System.Web.Security;

namespace EuroCMS.Management
{
    public enum CmsWebEventType
    {
        LOGIN,
        LOGOUT,
        USER_DELETE_ROLE,
        USER_SAVE_ROLE,
        USER_ADD_TO_ROLE,
        USER_CHANGE_USER_PROFILE,
        USER_REMOVE_FROM_ROLE,
        USER_RESET_PASSWORD,
        USER_UNLOCK,
        USER_CHANGE_APPROVAL,
        USER_USER_DELETE,
        USER_FORGOT_PASSWORD,
        USER_CHANGE_PASSWORD,
        USER_REGISTER,
        USER_DELETE,
        CREATE_APP_SETTING,
        DELETE_APP_SETTING,
        UPDATE_APP_SETTING,
        SAVE_CONNECTION_STRING,
        SETTING_SAVE_ANONYMOUS_IDENTIFICATION,
        SETTING_SAVE_TRACING,
        SETTING_SAVE_HEALT_MONITORING,
        SETTING_SAVE_SMTP,
        SETTING_SAVE_PROFILE_PROPERTIES,
        SETTING_SAVE_GLOBALIZATION,
        ARTICLE_EDIT,
        ARTICLE_CREATE,
        ARTICLE_DELETE,
        ARTICLE_APPROVE,
        ARTICLE_DISCARD,
        ARTICLE_DUPLICATE,
        ARTICLE_FILE_DISCARD,
        ARTICLE_FILE_APPROVE,
        ARTICLE_FILE_SAVE,
        ARTICLE_FILE_EDIT,
        ARTICLE_FILE_DELETE,
        ARTICLE_FILE_SENDTOAPPROVE,
        BREADCRUMB_CREATE,
        BREADCRUMB_EDIT,
        BREADCRUMB_DELETE,
        CLASSIFICATION_CREATE,
        CLASSIFICATION_EDIT,
        CLASSIFICATION_DELETE,
        CONFIGURATION_CREATE,
        CONFIGURATION_EDIT,
        CONFIGURATION_DELETE,
        CSS_CREATE,
        CSS_EDIT,
        CSS_DELETE,
        CUSTOM_CONTENT_CREATE,
        CUSTOM_CONTENT_EDIT,
        CUSTOM_CONTENT_DELETE,
        DOMAIN_CREATE,
        DOMAIN_EDIT,
        DOMAIN_DELETE,
        FILE_TYPE_CREATE,
        FILE_TYPE_EDIT,
        FILE_TYPE_DELETE,
        HIDDEN_VALUE_CREATE,
        HIDDEN_VALUE_EDIT,
        LANGUAGE_DELETE,
        LANGUAGE_EDIT,
        LANGUAGE_CREATE,
        LAYOUT_CREATE,
        LAYOUT_EDIT,
        LAYOUT_DELETE,
        PORTLET_DELETE,
        PORTLET_EDIT,
        PORTLET_CREATE,
        RSS_FEED_CREATE,
        RSS_FEED_EDIT,
        RSS_FEED_DELETE,
        SITE_CREATE,
        SITE_EDIT,
        SITE_DELETE,
        SITEMAP_CREATE,
        SITEMAP_EDIT,
        SITEMAP_DELETE,
        SITEMAP_RECREATE,
        STRUCTURE_GROUP_CREATE,
        STRUCTURE_GROUP_EDIT,
        STRUCTURE_GROUP_DELETE,
        URL_REDIRECT_CREATE,
        URL_REDIRECT_EDIT,
        URL_REDIRECT_DELETE,
        XML_GENERATOR_CREATE,
        XML_GENERATOR_EDIT,
        XML_GENERATOR_DELETE,
        ZONE_CREATE,
        ZONE_DELETE,
        ZONE_APPROVE,
        ZONE_DISCARD,
        ZONE_GROUP_CREATE,
        ZONE_GROUP_EDIT,
        ZONE_GROUP_DELETE,
        SETTING_SAVE_CACHE,
        DELETE_CACHE_PROFILE,
        UPDATE_CACHE_PROFILE,
        SETTING_DELETE_CACHE_PROFILE,
        SETTING_UPDATE_CACHE_PROFILE,
        SETTING_DELETE_APP_SETTING,
        SETTING_UPDATE_APP_SETTING,
        SETTING_SAVE_CONNECTION_STRING,
        ACCESS_RULE_SAVE,
        ACCESS_RULE_DELETE,
        REDIRECTON_CREATE,
        REDIRECTION_EDIT,
        SPLASH_DELETE,
        SPLASH_EDIT,
        SPLASH_CREATE,
        TAG,
        TAG_CREATE,
        TAG_EDIT
    }

    public class CmsWebEvent : WebBaseEvent
    {
        private string customCreatedMsg;
        private string customRaisedMsg;
        private CmsWebEventType CmsEventCode;

        private string userID;
        private string authType;
        private bool isAuthenticated;
        private string roles;
        private string headerData;
        private string bodyData;

        public CmsWebEvent(CmsWebEventType eventCode, object source) :
            base(eventCode.ToString(), source, WebEventCodes.WebExtendedBase + (int)eventCode)
        {
            CmsEventCode = eventCode;

            customCreatedMsg = string.Format("{0} created at: {1}",
                EventCode.ToString(), EventTime.ToString());

            userID = HttpContext.Current.User.Identity.Name;
            authType = HttpContext.Current.User.Identity.AuthenticationType;
            isAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            headerData = HttpContext.Current.Request["ALL_RAW"].ToString();
            roles = string.Join(",", Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name));
            //bodyData = new System.IO.StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();

            if (string.IsNullOrEmpty(userID) && HttpContext.Current.Session["userName"] != null)
            {
                userID = HttpContext.Current.Session["userName"].ToString();
                isAuthenticated = true;
                roles = string.Join(",", Roles.GetRolesForUser(userID));
            }
        }

        public override void Raise()
        {
            customRaisedMsg = string.Format("{0} raised at: {1}",
                EventCode.ToString(), EventTime.ToString());

            base.Raise();
        }

        public override void FormatCustomEventDetails(WebEventFormatter formatter)
        {
            base.FormatCustomEventDetails(formatter);

            formatter.IndentationLevel += 1;
            formatter.AppendLine(customCreatedMsg);
            formatter.AppendLine(customRaisedMsg);
            formatter.AppendLine("*********************************");
            formatter.AppendLine("Header Data:");
            formatter.AppendLine(HttpUtility.HtmlDecode(headerData));
            formatter.AppendLine("*********************************");
            formatter.AppendLine("Body Data:");
            formatter.AppendLine(HttpUtility.HtmlDecode(bodyData));

            formatter.AppendLine("*********** USER DATA **********************");
            formatter.AppendLine("User ID: " + userID);
            formatter.AppendLine("Authentication Type: " + authType);
            formatter.AppendLine("User Authenticated: " + isAuthenticated.ToString());
            formatter.AppendLine("User Roles: " + roles);
            formatter.IndentationLevel -= 1;
            
        }
    }
}
