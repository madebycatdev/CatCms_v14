using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

namespace EUroCMS.Configuration
{
    public class ConfigManager
    {
        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
        
        public ConfigManager() {  }

        public void SaveSqlDependency(bool enabled, int poolTime)
        { 
            var sqlDependencySection = (SqlCacheDependencySection)config.GetSection("system.web/caching/sqlCacheDependency");
            sqlDependencySection.Enabled = enabled;
            sqlDependencySection.PollTime = poolTime;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.web/caching/sqlCacheDependency");
        }

        public void SaveCache(bool enabled, bool enabledFragment, bool enableKernelCache, bool omitVaryStar, bool sendCacheControlHeader)
        {
            var outputCache = (OutputCacheSection)config.GetSection("system.web/caching/outputCache");
            outputCache.EnableOutputCache = enabled;
            outputCache.EnableFragmentCache = enabledFragment;
            outputCache.EnableKernelCacheForVaryByStar = enableKernelCache;
            outputCache.OmitVaryStar = omitVaryStar;
            outputCache.SendCacheControlHeader = sendCacheControlHeader;

            var outputCacheSettings = (OutputCacheSettingsSection)config.GetSection("system.web/caching/outputCacheSettings");

            if (outputCache.EnableOutputCache)
            {
                if (outputCacheSettings.OutputCacheProfiles["DefaultCacheProfile"] == null)
                {
                    outputCacheSettings.OutputCacheProfiles.Clear();
                    outputCacheSettings.OutputCacheProfiles.Add(
                        new OutputCacheProfile("DefaultCacheProfile")
                        {
                            Duration = 86400,
                            VaryByControl = "*",
                            VaryByParam = "none",
                            VaryByCustom = "key",
                            SqlDependency = "EuroCMS:cms_Sitemap"
                        }
                    );
                }
            }
            else
            {
                outputCacheSettings.OutputCacheProfiles.Clear();
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.web/caching");
        }

        public void DeleteCacheProfile(string name)
        {
            var outputCacheSettingsSection = (OutputCacheSettingsSection)config.GetSection("system.web/caching/outputCacheSettings");

            if (outputCacheSettingsSection.OutputCacheProfiles[name] != null)
                outputCacheSettingsSection.OutputCacheProfiles.Remove(name);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.web/caching/outputCacheSettings");
        }

        public void UpdateCacheProfile(string name, bool enabled, int duration, bool noStore, OutputCacheLocation location,
            string sqlDependency, 
            string varyByContentEncoding,
            string varyByControl,
            string varyByCustom,
            string varyByHeader,
            string varyByParam)
        {
            var outputCacheSettingsSection = (OutputCacheSettingsSection)config.GetSection("system.web/caching/outputCacheSettings");
            var cacheProfileName = CleanInvalidXmlChars(name);
            var cacheProfile = outputCacheSettingsSection.OutputCacheProfiles[cacheProfileName];

            if (cacheProfile == null)
                cacheProfile = new OutputCacheProfile(cacheProfileName);

            cacheProfile.Duration = duration;
            cacheProfile.Enabled = enabled;
            cacheProfile.Location = location;
            cacheProfile.NoStore = noStore;
            cacheProfile.SqlDependency = CleanInvalidXmlChars(sqlDependency);
            cacheProfile.VaryByContentEncoding = CleanInvalidXmlChars(varyByContentEncoding);
            cacheProfile.VaryByControl = CleanInvalidXmlChars(varyByControl);
            cacheProfile.VaryByCustom = CleanInvalidXmlChars(varyByCustom);
            cacheProfile.VaryByHeader = CleanInvalidXmlChars(varyByHeader);
            cacheProfile.VaryByParam = CleanInvalidXmlChars(varyByParam);

            outputCacheSettingsSection.OutputCacheProfiles.Add(cacheProfile);

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }


        public void DeleteAppSetting(string key)
        { 
            var appSettings = config.AppSettings.Settings;
            appSettings.Remove(HttpUtility.HtmlDecode(key));

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public void UpdateAppSetting(string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add( CleanInvalidXmlChars(HttpUtility.HtmlDecode(key)), CleanInvalidXmlChars(HttpUtility.HtmlDecode(value)));
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }


        public void SaveConnectionString(string server, string catalog, string user, string password, int timeOut, bool integratedSecurity)
        {
            var connectionString = config.ConnectionStrings.ConnectionStrings["eurocms.db"];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = CleanInvalidXmlChars(server);
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = CleanInvalidXmlChars(catalog);
            builder.UserID = CleanInvalidXmlChars(user);
            builder.Password = CleanInvalidXmlChars(password);
            builder.ConnectTimeout = timeOut;
            builder.IntegratedSecurity = integratedSecurity;
            connectionString.ConnectionString = builder.ConnectionString;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public void SaveAnonymousIdentification(bool enabled, 
            bool cookieSlidingExpiration,
            bool cookieRequireSSL, 
            string cookiePath, 
            string cookieName,
            string domain,
            int timeOut,
            CookieProtection cookieProtection,
            HttpCookieMode cookieless)
        {
            var anonymousIdentificationSection = (AnonymousIdentificationSection)config.GetSection("system.web/anonymousIdentification");

            bool _cookieSlidingExpiration = false;
            bool _cookieRequireSSL = false;
            string _cookiePath = "/";
            string _cookieName = ".CmsAnonymousIdentification";
            string _Domain = "*";
            int _CookieTimeout = 300;
            CookieProtection _CookieProtection = 0;
            HttpCookieMode _Cookieless = HttpCookieMode.AutoDetect;
            
            if (enabled)
            {
                _cookieSlidingExpiration = cookieSlidingExpiration;
                _cookieRequireSSL = cookieRequireSSL;
                _cookiePath = string.IsNullOrEmpty(cookiePath) ? _cookiePath : CleanInvalidXmlChars( cookiePath);
                _cookieName = string.IsNullOrEmpty(cookieName) ? _cookieName : CleanInvalidXmlChars(cookieName);
                _Domain = string.IsNullOrEmpty(domain) ? _Domain : domain;
                _CookieTimeout = timeOut;
                _CookieProtection = cookieProtection;
                _Cookieless = cookieless;
            }

            anonymousIdentificationSection.Enabled = enabled;
            anonymousIdentificationSection.CookieSlidingExpiration = _cookieSlidingExpiration;
            anonymousIdentificationSection.CookieRequireSSL = _cookieRequireSSL;
            anonymousIdentificationSection.CookiePath = _cookiePath;
            anonymousIdentificationSection.CookieName = _cookieName;
            anonymousIdentificationSection.Domain = _Domain;
            anonymousIdentificationSection.CookieTimeout = new TimeSpan(0, _CookieTimeout, 0);
            anonymousIdentificationSection.CookieProtection = _CookieProtection;
            anonymousIdentificationSection.Cookieless = _Cookieless;
 
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("system.web/anonymousIdentification");
        }


        public void SaveTracing(bool enabled, bool debuging, bool localOnly, bool writeToDiognastic, bool mostRecent, 
            TraceDisplayMode traceMode, bool pageOutput, int requestLimit)
        {
            var compilationSection = (CompilationSection)config.GetSection("system.web/compilation");
            compilationSection.Debug = Convert.ToBoolean(debuging);

            bool _Enabled = Convert.ToBoolean(enabled);
            bool _LocalOnly = false;
            bool _WriteToDiagnosticsTrace = false;
            bool _MostRecent = false;
            TraceDisplayMode _TraceMode = TraceDisplayMode.SortByTime;
            bool _PageOutput = false;
            int _RequestLimit = 0;

            var traceSection = (TraceSection)config.GetSection("system.web/trace");
            traceSection.Enabled = _Enabled;

            if (_Enabled)
            {
                _LocalOnly = localOnly;
                _WriteToDiagnosticsTrace = writeToDiognastic;
                _MostRecent = mostRecent;
                _PageOutput = pageOutput;
                _TraceMode = traceMode;
                _RequestLimit = requestLimit;
            }

            traceSection.LocalOnly = localOnly;
            traceSection.WriteToDiagnosticsTrace = writeToDiognastic;
            traceSection.MostRecent = mostRecent;
            traceSection.TraceMode = traceMode;
            traceSection.PageOutput = pageOutput;
            traceSection.RequestLimit = requestLimit;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("system.web/compilation");
            ConfigurationManager.RefreshSection("system.web/trace");
        }


        public void SaveHealtMonitoring(bool enabled, int heartbeatInterval, bool mailEnabled,
            string mailFrom,
            string mailTo,
            string mailCc,
            string mailBcc,
            string mailHeader,
            string mailFooter,
            string mailSubjectPrefix,
            string mailBuffer,
            string mailBufferMode,
            string mailMaxMessagesPerNotification,
            bool sqlEnabled,
            string sqlBuffer,
            string sqlBufferMode,
            string sqlMaxMessagesPerNotification
            )
        {
            var healthMonitoringSection = (HealthMonitoringSection)config.GetSection("system.web/healthMonitoring");
            healthMonitoringSection.Enabled = Convert.ToBoolean(enabled);
            healthMonitoringSection.HeartbeatInterval = new TimeSpan(0, 0, heartbeatInterval);

            if (healthMonitoringSection.Enabled)
            {
                if (mailEnabled)
                {
                    if (healthMonitoringSection.Providers["SimpleMailWebEventProvider"] == null)
                    {
                        healthMonitoringSection.Providers.Add(new ProviderSettings() { Name = "SimpleMailWebEventProvider", Type = "System.Web.Management.SimpleMailWebEventProvider" });
                    }

                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["from"] = CleanInvalidXmlChars(mailTo);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["to"] = CleanInvalidXmlChars(mailTo);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["cc"] = CleanInvalidXmlChars(mailCc);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["bcc"] = CleanInvalidXmlChars(mailBcc);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["bodyHeader"] = CleanInvalidXmlChars(mailHeader);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["bodyFooter"] = CleanInvalidXmlChars(mailFooter);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["subjectPrefix"] = CleanInvalidXmlChars(mailSubjectPrefix);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["buffer"] = CleanInvalidXmlChars(mailBuffer);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["bufferMode"] = CleanInvalidXmlChars(mailBufferMode);
                    healthMonitoringSection.Providers["SimpleMailWebEventProvider"].Parameters["maxMessagesPerNotification"] = CleanInvalidXmlChars(sqlMaxMessagesPerNotification);;

                    if (healthMonitoringSection.Rules["All Errors Email"] == null)
                    {
                        healthMonitoringSection.Rules.Add(new RuleSettings("All Errors Email", "All Errors", "SimpleMailWebEventProvider"));
                    }

                    if (healthMonitoringSection.Rules["Failure Audits Email"] == null)
                    {
                        healthMonitoringSection.Rules.Add(new RuleSettings("Failure Audits Email", "Failure Audits", "SimpleMailWebEventProvider"));
                    }
                }
                else
                {
                    healthMonitoringSection.Rules.Remove("All Errors Email");
                    healthMonitoringSection.Rules.Remove("Failure Audits Email");
                    healthMonitoringSection.Providers.Remove("SimpleMailWebEventProvider");
                }
 
                if (sqlEnabled)
                {
                    if (healthMonitoringSection.Providers["SqlWebEventProvider"] == null)
                    {
                        healthMonitoringSection.Providers.Add(new ProviderSettings() { Name = "SqlWebEventProvider", Type = "System.Web.Management.SqlWebEventProvider" });
                    }

                    healthMonitoringSection.Providers["SqlWebEventProvider"].Parameters["buffer"] = CleanInvalidXmlChars(sqlBuffer);
                    healthMonitoringSection.Providers["SqlWebEventProvider"].Parameters["bufferMode"] = CleanInvalidXmlChars(sqlBufferMode);
                    healthMonitoringSection.Providers["SqlWebEventProvider"].Parameters["maxEventDetailsLength"] = CleanInvalidXmlChars(sqlMaxMessagesPerNotification);
                    healthMonitoringSection.Providers["SqlWebEventProvider"].Parameters["connectionStringName"] = "eurocms.db";

                    if (healthMonitoringSection.Rules["All CMS Events SQL"] == null)
                    {
                        healthMonitoringSection.Rules.Add(new RuleSettings("All CMS Events SQL", "CmsWebEvent", "SqlWebEventProvider"));
                    }

                    if (healthMonitoringSection.EventMappings["CmsWebEvent"] == null)
                    {
                        healthMonitoringSection.EventMappings.Add(new EventMappingSettings("CmsWebEvent", "EuroCMS.Management.CmsWebEvent"));
                    }

                    if (healthMonitoringSection.Rules["All Errors SQL"] == null)
                    {
                        healthMonitoringSection.Rules.Add(new RuleSettings("All Errors SQL", "All Errors", "SqlWebEventProvider"));
                    }

                    if (healthMonitoringSection.Rules["Failure Audits SQL"] == null)
                    {
                        healthMonitoringSection.Rules.Add(new RuleSettings("Failure Audits SQL", "Failure Audits", "SqlWebEventProvider"));
                    }

                    if (healthMonitoringSection.Rules["Request Processing Errors SQL"] == null)
                    {
                        healthMonitoringSection.Rules.Add(new RuleSettings("Request Processing Errors SQL", "Request Processing Errors", "SqlWebEventProvider"));
                    }
                }
                else
                {
                    healthMonitoringSection.Rules.Remove("All Errors SQL");
                    healthMonitoringSection.Rules.Remove("Failure Audits SQL");
                    healthMonitoringSection.Rules.Remove("Request Processing Errors SQL");
                    healthMonitoringSection.Rules.Remove("All CMS Events SQL");
                    healthMonitoringSection.Providers.Remove("SqlWebEventProvider");
                    healthMonitoringSection.EventMappings.Remove("CmsWebEvent");
                }


                //if (healthMonitoringSection.Rules["Request Processing Trace"] == null)
                //{
                //    healthMonitoringSection.Rules.Add(new RuleSettings("Request Processing Trace", "Request Processing Events", "SqlWebEventProvider"));
                //}
            }
            else
            {
                healthMonitoringSection.Providers.Clear();
                healthMonitoringSection.Rules.Clear();
                healthMonitoringSection.EventMappings.Remove("CmsWebEvent");
            }

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("system.web/healthMonitoring");
        }

        public void SaveSmtp(string userName, string password, string authenticationType, 
            int port, string host, bool enableSsl,
            string from, string clientDomain)
        {
            var node = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");

            bool _DefaultCredential = false;
            string senderUserName = userName ?? "";
            string senderPassword = password ?? "";

            string defaultCredential = authenticationType ?? "";

            if (!defaultCredential.Equals("Basic"))
            {
                senderUserName = null;
                senderPassword = null;
            }

            if (defaultCredential.Equals("Ntlm"))
            {
                _DefaultCredential = true;
            }
             
            node.Network.DefaultCredentials = _DefaultCredential;
            node.Network.EnableSsl = enableSsl;
            node.Network.Host = CleanInvalidXmlChars(host);
            node.Network.Port = port;
            node.Network.ClientDomain = CleanInvalidXmlChars(clientDomain);
            node.Network.UserName = senderUserName;
            node.Network.Password = senderPassword;
            node.From = CleanInvalidXmlChars(from);
            node.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            node.SectionInformation.ForceSave = true;
            //node.SectionInformation.ProtectSection("RSAProtectedConfigurationProvider");
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.net/mailSettings/smtp");
        }

        public void SaveGlobalization(string fileEncoding, string requestEncoding, string responseEncoding, string culture, string uiCulture)
        {
            var globalizationSection = (GlobalizationSection)config.GetSection("system.web/globalization");
            globalizationSection.FileEncoding = Encoding.GetEncoding(CleanInvalidXmlChars(fileEncoding));
            globalizationSection.RequestEncoding = Encoding.GetEncoding(CleanInvalidXmlChars(requestEncoding));
            globalizationSection.ResponseEncoding = Encoding.GetEncoding(CleanInvalidXmlChars(responseEncoding));
            globalizationSection.Culture = CleanInvalidXmlChars(culture);
            globalizationSection.UICulture = CleanInvalidXmlChars(uiCulture);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.web/globalization");
        }

        public void SaveProfileProperties(bool enabled, bool automaticSaveEnabled, string fieldNames, string fieldTypes, string fieldSerializeAs, string fieldDefaultValues, string fieldAllowAnonymous)
        {
            var profileSection = (System.Web.Configuration.ProfileSection)config.GetSection("system.web/profile");
            profileSection.AutomaticSaveEnabled = Convert.ToBoolean(automaticSaveEnabled);
            profileSection.Enabled = Convert.ToBoolean(enabled);

            var profileProperties = profileSection.PropertySettings;
            profileProperties.Clear();

            ProfileGroupSettings systemGroup = profileProperties.GroupSettings["System"];
            profileProperties.GroupSettings.Clear();
            profileProperties.GroupSettings.Add(systemGroup);

            //var GroupNames = collection["GroupName[]"].Split(',');

            //for (int i = 0; i < GroupNames.Length; i++)
            //{
            //    var name = GroupNames[i].Trim();

            //    if (!string.IsNullOrEmpty(name))
            //    {
            //        ProfileGroupSettings pg = new ProfileGroupSettings(CleanInvalidXmlChars(name));

            //        if (profileProperties.GroupSettings[name] == null)
            //        {
            //            profileProperties.GroupSettings.Add(pg);
            //        }
            //        else
            //        {
            //            pg = profileProperties.GroupSettings[name];
            //        }

            //        var GNames = collection[name + ".Name[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //        var GTypes = collection[name + ".Type[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //        var GSerializeAs = collection[name + ".SerializeAs[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //        var GDefaultValues = collection[name + ".DefaultValue[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //        var GAllowAnonymous = collection[name + ".AllowAnonymous[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            //        for (int j = 0; j < GNames.Length; j++)
            //        {
            //            var _name = GNames[j].Trim();
            //            if (!string.IsNullOrEmpty(_name))
            //            {
            //                ProfilePropertySettings p = new ProfilePropertySettings(_name);

            //                if (pg.PropertySettings[_name] == null)
            //                {

            //                    pg.PropertySettings.Add(p);
            //                }
            //                else
            //                {
            //                    p = pg.PropertySettings[_name];
            //                }

            //                p.Type = CleanInvalidXmlChars(GTypes[j]);
            //                p.SerializeAs = (SerializationMode)Enum.Parse(typeof(SerializationMode), GSerializeAs[j]);
            //                p.DefaultValue = GDefaultValues[j];
            //                p.AllowAnonymous = Convert.ToBoolean(GAllowAnonymous[j].Split(',').FirstOrDefault());
            //            }
            //        }
            //    }
            //}

            var Names = fieldNames.Split(',');
            var Types = fieldTypes.Split(',');
            var SerializeAs = fieldSerializeAs.Split(',');
            var DefaultValues = fieldDefaultValues.Split(',');
            var AllowAnonymous = fieldAllowAnonymous.Split(',');

            for (int i = 0; i < Names.Length; i++)
            {
                var name = Names[i].Trim();
                if (!string.IsNullOrEmpty(name) && profileProperties.GroupSettings[name] == null)
                {
                    ProfilePropertySettings p = new ProfilePropertySettings(CleanInvalidXmlChars(name));
                    p.Type = CleanInvalidXmlChars(Types[i]);
                    p.SerializeAs = (SerializationMode)Enum.Parse(typeof(SerializationMode), SerializeAs[i]);
                    p.DefaultValue = DefaultValues[i];
                    p.AllowAnonymous = Convert.ToBoolean(AllowAnonymous[i].Split(',').FirstOrDefault());
                    profileProperties.Add(p);
                }
            }

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.web/profile");
        }

        public static string CleanInvalidXmlChars(string text)
        {
            // From xml spec valid chars: 
            // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]     
            // any Unicode character, excluding the surrogate blocks, FFFE, and FFFF. 
            string re = @"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-u10FFFF]";
            return Regex.Replace(text, re, "");
        }
    }
}
