using EuroCMS.Admin.Common;
using EuroCMS.Core;
using EuroCMS.Configuration;
using EuroCMS.Management;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using EUroCMS.Configuration;

namespace EuroCMS.Admin.Controllers
{
    [CmsAuthorize(Roles = "Administrator")]
    public class SettingController : BaseController
    {
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
        ConfigManager manager = new ConfigManager();

        [CmsAuthorize(Permission = "View", ContentType = "Setting")]
        public ActionResult Index()
        {
            return RedirectToAction("General");
        }

        [CmsAuthorize(Permission = "View", ContentType = "Setting")]
        public ActionResult General()
        {
            ViewData["Mail"] = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");

            var globalization = (GlobalizationSection)config.GetSection("system.web/globalization");
            ViewData["Globalization"] = globalization;

            ViewBag.Encodings = GetEncodings(globalization.FileEncoding.HeaderName);

            ViewBag.Cultures = GetCultures(globalization.Culture);
            ViewBag.UICultures = GetCultures(globalization.UICulture);
            ViewBag.FileEncodings = new SelectList(System.Text.Encoding.GetEncodings(), "Name", "DisplayName", globalization.FileEncoding.HeaderName);
            ViewBag.RequestEncodings = new SelectList(System.Text.Encoding.GetEncodings(), "Name", "DisplayName", globalization.RequestEncoding.HeaderName);
            ViewBag.ResponseEncodings = new SelectList(System.Text.Encoding.GetEncodings(), "Name", "DisplayName", globalization.ResponseEncoding.HeaderName);

            return View();
        }

        [HttpPost]
        public ActionResult SendTestMail(string email)
        {
            try
            {
                var result = MailSender.SendMail(email, null, null, "Test Email", "SMTP config test.", null);
                TempData["Message"] = result.message;
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("General","Setting");
        }

        public List<SelectListItem> GetCultures(string selectedValue)
        {
            List<SelectListItem> cultureList = new List<SelectListItem>();
            foreach (System.Globalization.CultureInfo ci in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
            {
                string uiculture = "no one";
                try
                {
                    uiculture = System.Globalization.CultureInfo.CreateSpecificCulture(ci.Name).Name;
                }
                catch (Exception ex)
                {
                    CmsHelper.SaveErrorLog(ex, string.Empty, true);
                }

                cultureList.Add(new SelectListItem() { Text = ci.EnglishName, Value = ci.Name, Selected = (selectedValue == ci.Name) });
            }

            return cultureList;
        }

        public List<SelectListItem> GetEncodings(string selectedValue)
        {
            List<SelectListItem> encodingList = new List<SelectListItem>();

            foreach (System.Text.EncodingInfo ei in System.Text.Encoding.GetEncodings())
            {
                encodingList.Add(new SelectListItem() { Text = ei.DisplayName, Value = ei.Name, Selected = (selectedValue == ei.Name) });
            }

            return encodingList;
        }

        [CmsAuthorize(Permission = "Edit", ContentType = "Setting")]
        public ActionResult ProfileSetting()
        {
            var values = Enum.GetValues(typeof(System.Web.Configuration.SerializationMode));
            List<SelectListItem> enumList = new List<SelectListItem>();
            foreach (var c in values)
            {
                enumList.Add(new SelectListItem() { Text = ((System.Web.Configuration.SerializationMode)c).ToString(), Value = c.ToString() });
            }

            ViewData["SerializationModes"] = enumList;

            List<SelectListItem> types = new List<SelectListItem> { 
                    new SelectListItem() { Value = "System.String", Text = "System.String", },
					new SelectListItem() { Value = "System.Int16", Text = "System.Int16" },
					new SelectListItem() { Value = "System.Int32", Text = "System.Int32" },
					new SelectListItem() { Value = "System.Int64", Text = "System.Int64" },
					new SelectListItem() { Value = "System.Collections.Specialized.StringCollection", Text = "System.Collections.Specialized.StringCollection" },
					new SelectListItem() { Value = "System.DateTime", Text = "System.DateTime" },
					new SelectListItem() { Value = "System.TimeSpan", Text = "System.TimeSpan" },
					new SelectListItem() { Value = "System.Boolean", Text = "System.Boolean" },
					new SelectListItem() { Value = "System.Decimal", Text = "System.Decimal" },
					new SelectListItem() { Value = "System.Char", Text = "System.Char" },
					new SelectListItem() { Value = "System.Byte", Text = "System.Byte" },
					new SelectListItem() { Value = "Custom", Text = "Custom" }
             };

            ViewData["Types"] = types;

            var node = (ProfileSection)config.GetSection("system.web/profile");

            ViewData["ProfileProperties"] = (System.Web.Configuration.ProfileSection)config.GetSection("system.web/profile");
            ViewData["AnonymousIdentification"] = (AnonymousIdentificationSection)config.GetSection("system.web/anonymousIdentification");

            return View(node);
        }

        public ActionResult Application()
        {
            var appSettings = config.AppSettings.Settings;

            string connectionString = config.ConnectionStrings.ConnectionStrings["eurocms.db"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            ViewData["ConnectionString"] = builder;

            return View(appSettings);
        }

        public ActionResult Tracing()
        {
            var section = (TraceSection)config.GetSection("system.web/trace");

            ViewData["HealtMonitoring"] = (HealthMonitoringSection)config.GetSection("system.web/healthMonitoring");

            return View(section);
        }

        public ActionResult Caching()
        {
            var outputCacheSection = (System.Web.Configuration.OutputCacheSection)config.GetSection("system.web/caching/outputCache");
            ViewData["OutputCache"] = outputCacheSection;

            var outputCacheSettingsSection = (OutputCacheSettingsSection)config.GetSection("system.web/caching/outputCacheSettings");
            ViewData["OutputCacheSettings"] = outputCacheSettingsSection;

            var sqlCacheDependencySection = (SqlCacheDependencySection)config.GetSection("system.web/caching/sqlCacheDependency");
            ViewData["sqlCacheDependency"] = sqlCacheDependencySection;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveSqlDependency(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                bool _Enabled = false;
                bool.TryParse(collection["Enabled"].Split(',').FirstOrDefault(), out _Enabled);

                int _PollTime = 0;
                int.TryParse(collection["PollTime"], out _PollTime);

                manager.SaveSqlDependency(_Enabled, _PollTime);


                TempData["Message"] = "Sql Dependency Setting has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_CACHE, this));

            return RedirectToAction("Caching");
        }

        public ActionResult CreateCacheProfile(string id)
        {
            var outputCacheSettingsSection = (OutputCacheSettingsSection)config.GetSection("system.web/caching/outputCacheSettings");
            var result = outputCacheSettingsSection.OutputCacheProfiles[id];

            var values = Enum.GetValues(typeof(OutputCacheLocation));
            List<SelectListItem> enumList = new List<SelectListItem>();
            foreach (var c in values)
            {
                enumList.Add(new SelectListItem() { Text = ((OutputCacheLocation)c).ToString(), Value = c.ToString() });
            }

            ViewData["OutputCacheLocation"] = enumList;

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveCache(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                bool _Enabled = false;
                Boolean.TryParse(collection["EnableOutputCache"].Split(',').FirstOrDefault(), out _Enabled);

                bool _EnabledFragmentCache = false;
                Boolean.TryParse(collection["EnableFragmentCache"].Split(',').FirstOrDefault(), out _EnabledFragmentCache);

                bool _EnableKernelCacheForVaryByStar = false;
                Boolean.TryParse(collection["EnableKernelCacheForVaryByStar"].Split(',').FirstOrDefault(), out _EnableKernelCacheForVaryByStar);

                bool _OmitVaryStar = false;
                Boolean.TryParse(collection["OmitVaryStar"].Split(',').FirstOrDefault(), out _OmitVaryStar);

                bool _SendCacheControlHeader = false;
                Boolean.TryParse(collection["SendCacheControlHeader"].Split(',').FirstOrDefault(), out _SendCacheControlHeader);

                manager.SaveCache(_Enabled, _EnabledFragmentCache, _EnableKernelCacheForVaryByStar, _OmitVaryStar, _SendCacheControlHeader);

                TempData["Message"] = "Cache Setting has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_CACHE, this));

            return RedirectToAction("Caching");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult DeleteCacheProfile(string id)
        {
            TempData.Clear();

            try
            {
                manager.DeleteCacheProfile(id);

                TempData["Message"] = "Cache Profile item has been deleted.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_DELETE_CACHE_PROFILE, this));

            return RedirectToAction("Caching");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult UpdateCacheProfile(FormCollection collection)
        {
            TempData.Clear();

            try
            {

                string name = collection["Name"];
                string sqlDependency = collection["SqlDependency"];

                string _VaryByContentEncoding = "none";
                string _VaryByControl = "none";
                string _VaryByCustom = "none";
                string _VaryByHeader = "none";
                string _VaryByParam = "none";

                int _Duration = 0;
                int.TryParse(CleanInvalidXmlChars(collection["Duration"]), out _Duration);

                bool _Enabled = false;
                bool.TryParse(collection["Enabled"].Split(',').FirstOrDefault(), out _Enabled);

                bool _NoStore = false;
                bool.TryParse(collection["NoStore"].Split(',').FirstOrDefault(), out _NoStore);

                OutputCacheLocation _Location = OutputCacheLocation.Server;
                Enum.TryParse<OutputCacheLocation>(CleanInvalidXmlChars(collection["Location"]), out _Location);

                if (!string.IsNullOrEmpty(collection["VaryByContentEncoding"]))
                    _VaryByContentEncoding = collection["VaryByContentEncoding"];

                if (!string.IsNullOrEmpty(collection["VaryByControl"]))
                    _VaryByControl = collection["VaryByControl"];

                if (!string.IsNullOrEmpty(collection["VaryByCustom"]))
                    _VaryByCustom = collection["VaryByCustom"];

                if (!string.IsNullOrEmpty(collection["VaryByHeader"]))
                    _VaryByHeader = collection["VaryByHeader"];

                if (!string.IsNullOrEmpty(collection["VaryByParam"]))
                    _VaryByParam = collection["VaryByParam"];

                manager.UpdateCacheProfile(name, _Enabled, _Duration, _NoStore, _Location, sqlDependency, _VaryByContentEncoding, _VaryByControl, _VaryByCustom, _VaryByHeader, _VaryByParam);

                TempData["Message"] = "Cache Profile item has been saved.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_UPDATE_CACHE_PROFILE, this));

            return RedirectToAction("Caching");
        }

        public ActionResult CreateAppSetting(string id)
        {
            var appSettings = config.AppSettings.Settings;
            KeyValueConfigurationElement result = appSettings[id];
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public RedirectToRouteResult DeleteAppSetting(string id)
        {
            TempData.Clear();

            try
            {
                manager.DeleteAppSetting(id);

                TempData["Message"] = "Application Setting item has been deleted.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            //WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_DELETE_APP_SETTING, this));

            return RedirectToAction("Application");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public RedirectToRouteResult UpdateAppSetting(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                manager.UpdateAppSetting(collection["SettingKey"], collection["SettingValue"]);

                TempData["Message"] = "Application Setting item has been created.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            //WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_UPDATE_APP_SETTING, this));

            return RedirectToAction("Application");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveConnectionString(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                int _TimeOut = 30;
                Int32.TryParse(collection["TimeOut"], out _TimeOut);

                bool _IntegratedSecurity = false;
                Boolean.TryParse(collection["IntegratedSecurity"], out _IntegratedSecurity);

                manager.SaveConnectionString(
                    collection["ServerAddress"],
                    collection["DatabaseName"],
                    collection["DbUserName"],
                    collection["DbPassword"],
                    _TimeOut,
                    _IntegratedSecurity);

                TempData["Message"] = "Connection String has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_CONNECTION_STRING, this));

            return RedirectToAction("Application");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveAnonymousIdentification(FormCollection collection)
        {
            TempData.Clear();

            try
            {

                bool _Enabled = false;
                bool.TryParse(collection["Enabled"].Split(',').FirstOrDefault(), out _Enabled);

                bool _CookieSlidingExpiration = false;
                bool _CookieRequireSSL = false;
                string _CookiePath = "/";
                string _CookieName = ".CmsAnonymousIdentification";
                string _Domain = "*";
                int _CookieTimeout = 300;
                CookieProtection _CookieProtection = 0;
                HttpCookieMode _Cookieless = HttpCookieMode.AutoDetect;

                if (_Enabled)
                {
                    bool.TryParse(collection["CookieSlidingExpiration"].Split(',').FirstOrDefault(), out _CookieSlidingExpiration);
                    bool.TryParse(collection["CookieRequireSSL"].Split(',').FirstOrDefault(), out _CookieRequireSSL);
                    _CookiePath = collection["CookiePath"];
                    _CookieName = collection["CookieName"];
                    _Domain = collection["Domain"];
                    int.TryParse(collection["CookieTimeout.TotalMinutes"], out _CookieTimeout);
                    Enum.TryParse<CookieProtection>(collection["CookieProtection"], out _CookieProtection);
                    Enum.TryParse<HttpCookieMode>(collection["Cookieless"], out _Cookieless);
                }

                manager.SaveAnonymousIdentification(_Enabled, _CookieSlidingExpiration, _CookieRequireSSL, _CookiePath, _CookieName, _Domain, _CookieTimeout, _CookieProtection, _Cookieless);

                TempData["Message"] = "Trace Settings has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            //WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_ANONYMOUS_IDENTIFICATION, this));

            return RedirectToAction("ProfileSetting");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveTracing(FormCollection collection)
        {
            TempData.Clear();

            try
            {

                bool _Enabled = Convert.ToBoolean(collection["Enabled"].Split(',').FirstOrDefault());
                bool _Debug = Convert.ToBoolean(collection["Debug"].Split(',').FirstOrDefault());
                bool _LocalOnly = false;
                bool _WriteToDiagnosticsTrace = false;
                bool _MostRecent = false;
                TraceDisplayMode _TraceMode = TraceDisplayMode.SortByTime;
                bool _PageOutput = false;
                int _RequestLimit = 0;

                if (_Enabled)
                {
                    bool.TryParse(collection["LocalOnly"].Split(',').FirstOrDefault(), out _LocalOnly);
                    bool.TryParse(collection["WriteToDiagnosticsTrace"].Split(',').FirstOrDefault(), out _WriteToDiagnosticsTrace);
                    bool.TryParse(collection["MostRecent"].Split(',').FirstOrDefault(), out _MostRecent);
                    bool.TryParse(collection["PageOutput"].Split(',').FirstOrDefault(), out _PageOutput);
                    Enum.TryParse<TraceDisplayMode>(collection["TraceMode"], out _TraceMode);
                    int.TryParse(collection["RequestLimit"], out _RequestLimit);
                }

                manager.SaveTracing(_Enabled, _Debug, _LocalOnly, _WriteToDiagnosticsTrace, _MostRecent, _TraceMode, _PageOutput, _RequestLimit);

                TempData["Message"] = "Trace Settings has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            //WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_TRACING, this));

            return RedirectToAction("Tracing");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveHealtMonitoring(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                var healthMonitoringSection = (HealthMonitoringSection)config.GetSection("system.web/healthMonitoring");

                bool _Enabled = false;
                Boolean.TryParse(collection["Enabled"].Split(',').FirstOrDefault(), out _Enabled);

                int _HeartbeatInterval = 10;
                Int32.TryParse(collection["HeartbeatInterval"], out _HeartbeatInterval);

                TimeSpan ts = new TimeSpan(0, 0, _HeartbeatInterval);

                bool _MailEnabled = false;
                Boolean.TryParse(collection["Mail.Enabled"].Split(',').FirstOrDefault(), out _MailEnabled);

                string _MailFrom = CleanInvalidXmlChars(collection["Mail.from"]);
                string _MailTo = CleanInvalidXmlChars(collection["Mail.to"]);
                string _MailCc = CleanInvalidXmlChars(collection["Mail.cc"]);
                string _MailBcc = CleanInvalidXmlChars(collection["Mail.bcc"]);
                string _MailHeader = CleanInvalidXmlChars(collection["Mail.bodyHeader"]);
                string _MailFooter = CleanInvalidXmlChars(collection["Mail.bodyFooter"]);
                string _MailSubjectPrefix = CleanInvalidXmlChars(collection["Mail.subjectPrefix"]);
                string _MailBuffer = CleanInvalidXmlChars(collection["Mail.buffer"].Split(',').FirstOrDefault());
                string _MailBufferMode = CleanInvalidXmlChars(collection["Mail.bufferMode"]);
                string _MailMaxMessagePerNotification = CleanInvalidXmlChars(collection["Mail.maxMessagesPerNotification"] ?? "0");

                bool _SqlEnabled = false;
                Boolean.TryParse(collection["Sql.Enabled"].Split(',').FirstOrDefault(), out _SqlEnabled);

                string _SqlBuffer = CleanInvalidXmlChars(collection["SQL.buffer"].Split(',').FirstOrDefault());
                string _SqlBufferMode = CleanInvalidXmlChars(collection["SQL.bufferMode"]);
                string _SqlMaxEventDetailsLength = CleanInvalidXmlChars(string.IsNullOrEmpty(collection["SQL.maxEventDetailsLength"]) ? "0" : collection["SQL.maxEventDetailsLength"]);

                manager.SaveHealtMonitoring(_Enabled, _HeartbeatInterval, _MailEnabled, _MailFrom, _MailTo, _MailCc, _MailBcc, _MailHeader, _MailFooter, _MailSubjectPrefix, _MailBuffer, _MailBufferMode, _MailMaxMessagePerNotification, _SqlEnabled, _SqlBuffer, _SqlBufferMode, _SqlMaxEventDetailsLength);

                TempData["Message"] = "HealthMonitoring Settings has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_HEALT_MONITORING, this));

            return RedirectToAction("Tracing");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveSmtp(FormCollection collection)
        {
            TempData.Clear();

            try
            {

                string senderUserName = collection["userName"] ?? "";
                string senderPassword = collection["password"] ?? "";

                string defaultCredential = collection["authentication"] ?? "";

                if (!defaultCredential.Equals("Basic"))
                {
                    senderUserName = null;
                    senderPassword = null;
                }

                int _Port = 25;
                int.TryParse(collection["port"], out _Port);

                bool _EnableSsl = collection["enableSsl"].Contains("true") ? true : false;
                string _From = collection["from"];
                string _ClientDomain = collection["clientDomain"];
                string _Host = collection["host"];

                manager.SaveSmtp(senderUserName, senderPassword, defaultCredential, _Port, _Host, _EnableSsl, _From, _ClientDomain);

                TempData["Message"] = "SMTP Settings has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            //WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_SMTP, this));

            return RedirectToAction("General");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveProfileProperties(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                var profileSection = (System.Web.Configuration.ProfileSection)config.GetSection("system.web/profile");
                profileSection.AutomaticSaveEnabled = Convert.ToBoolean(collection["AutomaticSaveEnabled"].Split(',').FirstOrDefault());
                profileSection.Enabled = Convert.ToBoolean(collection["Enabled"].Split(',').FirstOrDefault());

                var profileProperties = profileSection.PropertySettings;
                profileProperties.Clear();

                ProfileGroupSettings systemGroup = profileProperties.GroupSettings["System"];
                profileProperties.GroupSettings.Clear();

                profileProperties.GroupSettings.Add(systemGroup);

                var GroupNames = collection["GroupName[]"].Split(',');

                for (int i = 0; i < GroupNames.Length; i++)
                {
                    var name = GroupNames[i].Trim();

                    if (!string.IsNullOrEmpty(name))
                    {
                        ProfileGroupSettings pg = new ProfileGroupSettings(CleanInvalidXmlChars(name));

                        if (profileProperties.GroupSettings[name] == null)
                        {
                            profileProperties.GroupSettings.Add(pg);
                        }
                        else
                        {
                            pg = profileProperties.GroupSettings[name];
                        }

                        var GNames = collection[name + ".Name[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var GTypes = collection[name + ".Type[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var GSerializeAs = collection[name + ".SerializeAs[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var GDefaultValues = collection[name + ".DefaultValue[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var GAllowAnonymous = collection[name + ".AllowAnonymous[]"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        for (int j = 0; j < GNames.Length; j++)
                        {
                            var _name = GNames[j].Trim();
                            if (!string.IsNullOrEmpty(_name))
                            {
                                ProfilePropertySettings p = new ProfilePropertySettings(_name);

                                if (pg.PropertySettings[_name] == null)
                                {

                                    pg.PropertySettings.Add(p);
                                }
                                else
                                {
                                    p = pg.PropertySettings[_name];
                                }

                                p.Type = CleanInvalidXmlChars(GTypes[j]);
                                p.SerializeAs = (SerializationMode)Enum.Parse(typeof(SerializationMode), GSerializeAs[j]);
                                p.DefaultValue = GDefaultValues[j];
                                p.AllowAnonymous = Convert.ToBoolean(GAllowAnonymous[j].Split(',').FirstOrDefault());
                            }
                        }
                    }
                }

                var Names = collection["Name[]"].Split(',');
                var Types = collection["Type[]"].Split(',');
                var SerializeAs = collection["SerializeAs[]"].Split(',');
                var DefaultValues = collection["DefaultValue[]"].Split(',');
                var AllowAnonymous = collection["AllowAnonymous[]"].Split(',');

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

                TempData["Message"] = "Profile Settings has been changed.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            // WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_PROFILE_PROPERTIES, this));

            return RedirectToAction("ProfileSetting");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SaveGlobalization(FormCollection collection)
        {
            TempData.Clear();

            try
            {
                manager.SaveGlobalization(collection["FileEncoding"],
                    collection["RequestEncoding"],
                    collection["ResponseEncoding"],
                    collection["Culture"],
                    collection["UICulture"]);

                TempData["Message"] = "Globalization setting has been saved.";
            }
            catch (Exception ex)
            {
                CmsHelper.SaveErrorLog(ex, string.Empty, true);
                TempData["HasError"] = true;
                TempData["Message"] = ex.Message;
            }

            //WebBaseEvent.Raise(new CmsWebEvent(CmsWebEventType.SETTING_SAVE_GLOBALIZATION, this));

            return RedirectToAction("General");
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
