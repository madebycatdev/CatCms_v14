﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EuroCMS.FrontEnd.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://login.salesforce.com/services/Soap/c/41.0/0DF36000000UUb2")]
        public string EuroCMS_FrontEnd_Salesforce_SforceService {
            get {
                return ((string)(this["EuroCMS_FrontEnd_Salesforce_SforceService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.relateddigital.com/live/member.asmx")]
        public string EuroCMS_FrontEnd_com_euromsg_member_Member {
            get {
                return ((string)(this["EuroCMS_FrontEnd_com_euromsg_member_Member"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.relateddigital.com/live/sendlist.asmx")]
        public string EuroCMS_FrontEnd_com_euromsg_sendlist_SendList {
            get {
                return ((string)(this["EuroCMS_FrontEnd_com_euromsg_sendlist_SendList"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.relateddigital.com/live/post.asmx")]
        public string EuroCMS_FrontEnd_com_euromsg_post_Post {
            get {
                return ((string)(this["EuroCMS_FrontEnd_com_euromsg_post_Post"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.relateddigital.com/live/campaign.asmx")]
        public string EuroCMS_FrontEnd_com_euromsg_campaign_Campaign {
            get {
                return ((string)(this["EuroCMS_FrontEnd_com_euromsg_campaign_Campaign"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.relateddigital.com/live/report.asmx")]
        public string EuroCMS_FrontEnd_com_euromsg_report_Report {
            get {
                return ((string)(this["EuroCMS_FrontEnd_com_euromsg_report_Report"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ws.euromsg.com/liveb/auth.asmx")]
        public string EuroCMS_FrontEnd_com_euromsg_auth_Auth {
            get {
                return ((string)(this["EuroCMS_FrontEnd_com_euromsg_auth_Auth"]));
            }
        }
    }
}
