using EuroCMS.Core;
using EuroCMS.Data;
using EuroCMS.Helper;
using EuroCMS.Model;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;

namespace EuroCMS.Web
{
    public class PageHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return true; }
        }

        string dependecyFile = ConfigurationManager.AppSettings["EuroCMS.CacheDependecyFile"] ?? "";

        public string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    control.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        string html = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            #region OLD CMS FUNCTIONS
            string[] QS = CmsHelper.GetQSArr();
            vars.QS = QS;

            // to-do Kodlar sıkıştırılmalı. Deseriazlize ıcın onlem alınmalı.

            // checkAutoDailyReloadCache();
            getPubLevel(false);
            detectMobileDevice();

            if (QS != null)
            {
                switch (QS[0].ToLower())
                {
                    case Constansts.ALIAS_CSS:
                        html = renderCSS(QS);
                        break;
                    case Constansts.ALIAS_XML:
                        html = renderArticleXML(QS);
                        break;
                    case Constansts.ALIAS_FXML:
                        html = renderFileXML(QS);
                        break;
                    case Constansts.ALIAS_CONTENT:
                        UpdateIISCache();
                        // GatherHiddenValues();
                        html = renderPage(QS);
                        break;
                    //case Constansts.ALIAS_PLUGIN:
                    //    renderPlugin(QS);
                    //    break;
                    case Constansts.ALIAS_RSS:
                        html = renderRSS(QS);
                        break;
                    case Constansts.ALIAS_CRON:
                        updateSiteMap(QS);
                        break;
                    case Constansts.ALIAS_SITEMAP:
                        html = renderSiteMap(QS);
                        break;
                    case Constansts.ALIAS_EMPTY:
                        UpdateIISCache();
                        // GatherHiddenValues();
                        html = renderHome(string.Empty);
                        break;
                    default:
                        UpdateIISCache();
                        // GatherHiddenValues();
                        html = checkRedirectionAlias(QS);
                        break;
                }
            }
            #endregion

            // Default Daily caching
            TimeSpan t = new TimeSpan(1, 0, 0, 0);
            // Article publishing End Date sets to Timespan object
            if (vars.a["enddate"].ToString() != "" && vars.a["enddate"] != DBNull.Value && Convert.ToDateTime(vars.a["enddate"]) > DateTime.Now) t = Convert.ToDateTime(vars.a["enddate"]) - DateTime.Now;
            // Sets the Cache-Control HTTP header. he Cache-Control HTTP header controls how documents are to be cached on the network.
            context.Response.Cache.SetCacheability(IsManager() ? HttpCacheability.NoCache : HttpCacheability.ServerAndPrivate);
            // Sets the Expires HTTP header to an absolute date and time.
            context.Response.Cache.SetExpires(DateTime.Now.Add(t));
            // Sets the Cache-Control: max-age HTTP header based on the specified time span.
            context.Response.Cache.SetMaxAge(t);
            // Add file dependency
            context.Response.AddFileDependency(string.IsNullOrEmpty(dependecyFile) ? context.Server.MapPath("/App_Data/cache.dat") : dependecyFile);
            // Create new ASP.NET Page instance
            Page page = (Page)PageParser.GetCompiledPageInstance(context.Request.Path, context.Server.MapPath("~/Page.aspx"), context);

            // Parsing control on the page
            Control child = page.ParseControl(html);
            // Adding Rendered controls to the page
            page.Controls.Add(child);
            // Other processes sends to specific ASP.NET Page
            ((IHttpHandler)page).ProcessRequest(context);
        }

        //// when the page is fully initialized
        //public void InitComplete(object sender, System.EventArgs e)
        //{
        //    webPartManager = WebPartManager.GetCurrentWebPartManager(Page);

        //    String browseModeName = WebPartManager.BrowseDisplayMode.Name;

        //    foreach (WebPartDisplayMode mode in
        //      webPartManager.SupportedDisplayModes)
        //    {
        //        String modeName = mode.Name;
        //        if (mode.IsEnabled(webPartManager))
        //        {
        //            ListItem listItem = new ListItem(modeName, modeName);
        //            //ddlDisplayMode.Items.Add(listItem);
        //        }
        //    }
        //}

        // Change the page to the selected display mode.
        //public void ddlDisplayMode_SelectedIndexChanged(object sender,
        //  EventArgs e)
        //{
        //    // String selectedMode = ddlDisplayMode.SelectedValue;
        //    //String selectedMode = "1";

        //    //WebPartDisplayMode mode =
        //    // webPartManager.SupportedDisplayModes[selectedMode];
        //    //if (mode != null)
        //    //{
        //    //    webPartManager.DisplayMode = mode;
        //    //}
        //}

        //// Set the selected item equal to the current display mode.
        //public void Page_PreRender(object sender, EventArgs e)
        //{
        //    //ListItemCollection items = ddlDisplayMode.Items;
        //    //int selectedIndex =
        //    //  items.IndexOf(items.FindByText(webPartManager.DisplayMode.Name));
        //    //ddlDisplayMode.SelectedIndex = selectedIndex;
        //}

        #region OLD CMS FUNCTIONS

        GlobalVars vars = new GlobalVars();

        RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_RC4_KEY);

        #region getPubLevel
        public void getPubLevel(bool goLogin)
        {
            string pubName = string.Empty;

            string pubID = string.Empty;
            pubID = GetCookieValue(Constansts.CMS_COOKIE_NAME, Constansts.CMS_COOKIE_PUBLISHER_ID);
            //if (string.IsNullOrEmpty(level))
            //    level = GetSession("publisher_level");

            //Response.Write("<!--" + level + "-->");

            //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName);

            if (IsManager())
            {
                //vars.pubID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                // vars.pubID =  Membership.Provider.GetUser(HttpContext.Current.User.Identity.Name);
                //rc4.PlainText = level;
                //level = rc4.EnDeCrypt();
                //if (CmsHelper.IsNumeric(level))
                //{

                //if (string.IsNullOrEmpty(GetCookieValue(Constansts.CMS_COOKIE_NAME, Constansts.CMS_SESSION_PUBLISHER_NO)))
                //{
                //    rc4.PlainText = GetSession(Constansts.CMS_SESSION_PUBLISHER_NO);
                //    pubID = rc4.EnDeCrypt();
                //}
                //else 
                //{
                //    rc4.PlainText = GetCookieValue(Constansts.CMS_COOKIE_NAME, Constansts.CMS_SESSION_PUBLISHER_NO);
                //    pubID = rc4.EnDeCrypt();
                //}

                //if(string.IsNullOrEmpty(pubID))
                //    pubID = "0";

                //DataTable dt = Dal.Instance.SelectPublisherDetails(vars.pubID);

                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows[0]["publisher_status"].ToString().Equals("A"))
                //    {

                //        vars.pubLevel = Convert.ToInt32(dt.Rows[0]["publisher_level"]);
                //        vars.pubZGID = Convert.ToInt32(dt.Rows[0]["publisher_zg"]);
                //        vars.pubName = dt.Rows[0]["publisher_name"].ToString();
                //        vars.pubEmail = dt.Rows[0]["publisher_email"].ToString();
                //        vars.pubDept = dt.Rows[0]["publisher_department"].ToString();
                //        SetSession("LAST_TIMER", DateTime.Now.ToLongTimeString());

                //    }
                //}
                //}
            }

            //if (goLogin && vars.pubLevel == 0)
            //    Response.Redirect(string.Format("login.asp?level={0}", level));
            // if (goLogin && vars.pubLevel == 0)
            if (goLogin)
                HttpContext.Current.Response.Redirect(string.Format("login.asp?level={0}", vars.pubLevel));
        }
        #endregion
        #region detectMobileDevice
        public void detectMobileDevice()
        {
            string userAgent = HttpContext.Current.Request.UserAgent ?? "";

            if (userAgent.Contains("avantgo") ||
                userAgent.Contains("windows ce") ||
                userAgent.Contains("netfront") ||
                userAgent.Contains("blackberry") ||
                userAgent.Contains("nokia") ||
                userAgent.Contains("motorola") ||
                userAgent.Contains("mot-") ||
                userAgent.Contains("samsung") ||
                userAgent.Contains("sec-") ||
                userAgent.Contains("lg-") ||
                userAgent.Contains("sonyericsson") ||
                userAgent.Contains("sie-") ||
                userAgent.Contains("up.b") ||
                userAgent.Contains("up/") ||
                userAgent.Contains("iphone") ||
                userAgent.Contains("smartphone") ||
                userAgent.Contains("palm") ||
                userAgent.Contains("symbian"))
            {
                vars.IsMobile = true;
            }
        }
        #endregion
        #region renderCSS
        public string renderCSS(string[] QS)
        {
            int cssID = 0;
            string cssCode = string.Empty;
            string cssFix = string.Empty;
            if (QS.Length == 2)
            {
                if (CmsHelper.IsNumeric(QS[1]))
                    cssID = Convert.ToInt32(QS[1]);

                // HttpContext.Current.Response.Clear();
                // HttpContext.Current.Response.ContentType = "text/css";
                // HttpContext.Current.Response.Write(getCSS(cssID, "code"));
                return getCSS(cssID, "code");
                //Response.End();
            }
            else
            {
                return render404(QS);
            }
        }
        #endregion
        #region getCSS
        public string getCSS(int cssID, string type)
        {
            string cssCode = string.Empty;
            string cssFix = string.Empty;

            if (string.IsNullOrEmpty(GetApplication("CSS_" + cssID)) || GetApplication(Constansts.CFG_CACHE_ACTIVE) != "1")
            {
                DataTable dt = Dal.Instance.SelectCssCode(cssID);
                if (dt.Rows.Count > 0)
                {
                    cssCode = dt.Rows[0][1].ToString();
                    cssFix = dt.Rows[0][3].ToString();

                    if (string.IsNullOrEmpty(cssCode))
                        cssCode = string.Format("/* No CSS {0} */", cssID);
                    if (string.IsNullOrEmpty(cssFix))
                        cssFix = string.Format("<!-- No Fix {0} -->", cssID);

                    if (GetApplication(Constansts.CFG_CACHE_ACTIVE) != "1")
                    {
                        SetApplication(string.Format("CSS_{0}", cssID), cssCode);
                        SetApplication(string.Format("CSS_FIX_{0}", cssID), cssFix);
                    }

                    cssCode = string.Format("/* From DB */\n{0}", cssCode);
                }
            }
            return type.Equals("code") ? cssCode : cssFix;
        }

        #endregion
        #region renderXML
        public string renderXML(string[] QS, string type)
        {
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ContentType = "text/xml";

            string xmlTemp = string.Empty;
            string[] xmlIDs;
            int xmlID = 0;
            int zoneID = 0;
            int order = 0;
            string xmlCode = string.Empty;

            if (QS.Length == 2)
            {
                xmlTemp = QS[1];
                xmlIDs = xmlTemp.Split('.');
                xmlTemp = xmlIDs[0];
                xmlIDs = xmlTemp.Split('_');
                if (xmlIDs.Length > 1)
                {
                    if (CmsHelper.IsNumeric(xmlIDs[0]) && CmsHelper.IsNumeric(xmlIDs[1]) && CmsHelper.IsNumeric(xmlIDs[2]))
                    {
                        xmlID = Convert.ToInt32(xmlIDs[0]);
                        zoneID = Convert.ToInt32(xmlIDs[1]);
                        order = Convert.ToInt32(xmlIDs[2]);
                        xmlCode = getXML(xmlID, zoneID, order, type);

                        //HttpContext.Current.Response.Write(xmlCode);
                        //Response.End();

                        return xmlCode;
                    }
                }
            }
            else
            {
                return render404(QS);
            }

            return string.Empty;
        }

        #region renderArticleXML
        public string renderArticleXML(string[] QS)
        {
            return renderXML(QS, "xml");
        }
        #endregion
        #region renderFileXML
        public string renderFileXML(string[] QS)
        {

            return renderXML(QS, "fxml");
        }
        #endregion
        #endregion
        #region getXML
        public string getXML(int xmlID, int zoneID, int orderNo, string type)
        {
            string orderColumn = string.Empty;
            int fileLineCount = 0;
            string[] fileAliases = new string[30];
            string[] fileLines = new string[30];

            string xml = string.Empty;
            string xmlMainNode = string.Empty;
            string xmlMainNoeAttr = string.Empty;
            string xmlPerNode = string.Empty;
            string xmlPerNodeAttr = string.Empty;
            string xmlSubNode = string.Empty;
            int xmlSubTemplate = 0;
            string xmlRelatedLine = string.Empty;

            string xmlUrl = string.Empty;
            string xmlSub = string.Empty;
            string curTypes = string.Empty;

            StringBuilder files_per = new StringBuilder();
            StringBuilder line_per = new StringBuilder();
            StringBuilder xml_per = new StringBuilder();
            StringBuilder related_per = new StringBuilder();
            StringBuilder xml_out = new StringBuilder();
            string data_per = string.Empty;

            string curAzID = string.Empty;
            string perNodeAttrb = string.Empty;

            string xmlCacheKey = string.Format("XML_{0}_{1}_{2}_{3}", xmlID, zoneID, orderNo, type);
            if (string.IsNullOrEmpty(GetApplication(xmlCacheKey)) || !IsCacheActive())
            {
                DataTable dt = Dal.Instance.SelectXmlDetails(1);
                if (dt.Rows.Count > 0)
                {
                    xml = dt.Rows[0][10].ToString();
                    xmlMainNode = dt.Rows[0][2].ToString();
                    xmlMainNoeAttr = dt.Rows[0][3].ToString();
                    xmlPerNode = dt.Rows[0][4].ToString();
                    xmlPerNodeAttr = dt.Rows[0][5].ToString();
                    xmlSubNode = dt.Rows[0][6].ToString();
                    xmlSubTemplate = Convert.ToInt32(dt.Rows[0][7]);
                    xmlRelatedLine = dt.Rows[0][9].ToString();

                    if (xmlSubTemplate == 0)
                        xmlSubTemplate = 1;

                    if (!string.IsNullOrEmpty(xml))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xml);

                        XmlNodeList nodes = xmlDoc.SelectNodes("//xml_xml/xml_data");
                        foreach (XmlNode node in nodes)
                        {
                            StringBuilder inner_xml = new StringBuilder();
                            string name = string.Empty;
                            name = node.SelectSingleNode("name").InnerText;

                            if (!string.IsNullOrEmpty(name))
                            {
                                inner_xml.AppendFormat("<{0}", name);

                                string attr = node.SelectSingleNode("attribute").InnerText;
                                if (!string.IsNullOrEmpty(attr))
                                    inner_xml.AppendFormat(" {0}", attr);

                                inner_xml.Append(">");

                                string cdata = string.Empty;
                                cdata = node.SelectSingleNode("cdata").InnerText;
                                string value = string.Empty;
                                value = node.SelectSingleNode("value").InnerText;

                                if (cdata.Equals("1"))
                                    inner_xml.AppendFormat("<![CDATA[{0}]]>", HttpUtility.HtmlDecode(value.Trim()));
                                else
                                    inner_xml.AppendFormat(value.Trim());

                                inner_xml.AppendFormat("</{0}>\n", name);

                                if (type.Equals("fxml"))
                                {
                                    line_per.AppendFormat("\t\t{0}", inner_xml);
                                }
                                else
                                {
                                    string afiles = node.SelectSingleNode("afiles").InnerText;
                                    if (afiles.Equals("1"))
                                    {// test gerekli
                                        fileLineCount++;
                                        fileAliases[fileLineCount] = name;
                                        fileLines[fileLineCount] = inner_xml.ToString();
                                    }
                                    else
                                        line_per.AppendFormat("\t\t{0}", inner_xml);
                                }

                                if (inner_xml.Length > 0)
                                    inner_xml.Remove(0, inner_xml.Length - 1);
                            }
                        }
                    }
                }

                if (line_per.Length > 0 || fileLineCount > 0)
                {
                    if (type.Equals("xml"))
                    {
                        orderColumn = getOrderColumn(orderNo);
                        DataTable dt1 = Dal.Instance.SelectGetXMLData(zoneID, orderColumn);
                        if (dt1.Rows.Count > 1)
                        {
                            string names = getRecordsetNames(dt1);

                            foreach (DataRow dr in dt1.Rows)
                            {
                                int curID = Convert.ToInt32(dr[2]);
                                data_per = replaceArticleDetailsRows(dr, names, line_per.ToString(), "");
                                data_per = processAFiles(data_per, curID);

                                perNodeAttrb = xmlPerNodeAttr;
                                if (perNodeAttrb.IndexOf("##") != -1)
                                {
                                    perNodeAttrb = replaceArticleDetailsRows(dr, names, perNodeAttrb, "");
                                    perNodeAttrb = processAFiles(perNodeAttrb, curID);
                                }

                                if (perNodeAttrb != "")
                                    perNodeAttrb = " " + perNodeAttrb;

                                if ((dr[13].ToString().Equals("2") || dr[13].ToString().Equals("3")) && Convert.ToInt32(dr[14]) > 0)
                                { // 'navigation_display, navigation_zone_id

                                    xmlUrl = "/" + Constansts.ALIAS_XML + "/" + xmlSubTemplate + "_" + dr[14].ToString() + "_" + orderNo + ".xml";
                                    xmlSub = xmlSubNode.Replace("##xml_url##", xmlUrl);
                                    if (xmlSub.IndexOf("##") != -1)
                                        xmlSub = replaceArticleDetailsRows(dr, names, xmlSub, "");

                                    data_per += "\r\r" + xmlSub + "\n";
                                }

                                if (fileLineCount > 0)
                                {
                                    for (int i = 1; i <= fileLineCount; i++)
                                    {
                                        curTypes = curTypes + ",'" + fileAliases[i].Replace("'", "''") + "'";
                                    }
                                    if (curTypes.StartsWith(","))
                                        curTypes = curTypes.Substring(1, curTypes.Length - 1);

                                    if (files_per.Length > 0)
                                        files_per.Remove(0, files_per.Length - 1);

                                    DataTable dt3 = Dal.Instance.SelectGetAFilesData2(curTypes, curID);
                                    if (dt3.Rows.Count > 0)
                                    {
                                        line_per.Remove(0, line_per.Length - 1);

                                        string file_title = dt.Rows[0][10].ToString();
                                        string file_name_1 = dt.Rows[0][0].ToString();
                                        string file_name_2 = dt.Rows[0][1].ToString();
                                        string file_name_3 = dt.Rows[0][2].ToString();
                                        string file_name_4 = dt.Rows[0][3].ToString();
                                        string file_name_5 = dt.Rows[0][4].ToString();
                                        string file_name_6 = dt.Rows[0][5].ToString();
                                        string file_name_7 = dt.Rows[0][6].ToString();
                                        string file_name_8 = dt.Rows[0][7].ToString();
                                        string file_name_9 = dt.Rows[0][8].ToString();
                                        string file_name_10 = dt.Rows[0][9].ToString();
                                        string file_comment = dt.Rows[0][11].ToString();
                                        string type_alias = dt.Rows[0][12].ToString();

                                        for (int i = 1; i <= fileLineCount; i++)
                                        {
                                            if (type_alias == fileAliases[i])
                                            {
                                                line_per.Append(fileLines[i]);
                                            }
                                        }

                                        if (line_per.Length > 0)
                                        {
                                            if (!string.IsNullOrEmpty(file_name_1))
                                            {
                                                file_name_1 = "/i/content/" + curID + "_" + file_name_1;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_1_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_2))
                                            {
                                                file_name_2 = "/i/content/" + curID + "_" + file_name_2;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_2_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_3))
                                            {
                                                file_name_3 = "/i/content/" + curID + "_" + file_name_3;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_3_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_4))
                                            {
                                                file_name_4 = "/i/content/" + curID + "_" + file_name_4;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_4_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_5))
                                            {
                                                file_name_5 = "/i/content/" + curID + "_" + file_name_5;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_5_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_6))
                                            {
                                                file_name_6 = "/i/content/" + curID + "_" + file_name_6;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_6_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_7))
                                            {
                                                file_name_7 = "/i/content/" + curID + "_" + file_name_7;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_7_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_8))
                                            {
                                                file_name_8 = "/i/content/" + curID + "_" + file_name_8;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_8_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_9))
                                            {
                                                file_name_9 = "/i/content/" + curID + "_" + file_name_9;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_9_exists##", "exists");
                                            }
                                            if (!string.IsNullOrEmpty(file_name_10))
                                            {
                                                file_name_10 = "/i/content/" + curID + "_" + file_name_10;
                                                line_per = line_per.Replace("##afiles_" + type_alias + "_10_exists##", "exists");
                                            }

                                            line_per = line_per.Replace("##afiles_" + type_alias + "_1##", file_name_1);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_2##", file_name_2);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_3##", file_name_3);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_4##", file_name_4);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_5##", file_name_5);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_6##", file_name_6);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_7##", file_name_7);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_8##", file_name_8);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_9##", file_name_9);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_10##", file_name_10);
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_1_extension##", getFileExtension(file_name_1));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_2_extension##", getFileExtension(file_name_2));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_3_extension##", getFileExtension(file_name_3));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_4_extension##", getFileExtension(file_name_4));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_5_extension##", getFileExtension(file_name_5));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_6_extension##", getFileExtension(file_name_6));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_7_extension##", getFileExtension(file_name_7));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_8_extension##", getFileExtension(file_name_8));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_9_extension##", getFileExtension(file_name_9));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_10_extension##", getFileExtension(file_name_10));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_title##", getFileExtension(file_title));
                                            line_per = line_per.Replace("##afiles_" + type_alias + "_comment##", getFileExtension(file_comment));
                                        }

                                        files_per.AppendFormat("\t\t{0}\n", line_per.ToString());
                                    }

                                    if (related_per.Length > 0)
                                        related_per.Remove(0, related_per.Length - 1);

                                    if (!string.IsNullOrEmpty(xmlRelatedLine))
                                    {
                                        string azID = string.Empty;
                                        string relatedline = string.Empty;

                                        DataTable dt5 = Dal.Instance.SelectXmlRelatedArticles(curID);
                                        if (dt5.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr5 in dt5.Rows)
                                            {
                                                azID += "or (article_id = " + dr[1].ToString() + " and zone_id = " + dr[3].ToString() + ") ";
                                            }
                                        }
                                        else
                                            azID = "";

                                        if (azID.StartsWith("or "))
                                            azID = azID.Substring(3, azID.Length - 3);

                                        if (!string.IsNullOrEmpty(azID))
                                        {
                                            DataTable dt6 = Dal.Instance.SelectXmlRelatedArticles2(azID, orderColumn);
                                            string names2 = getRecordsetNames(dt6);
                                            foreach (DataRow dr6 in dt6.Rows)
                                            {
                                                relatedline = xmlRelatedLine;
                                                relatedline = replaceArticleDetailsRows(dr6, names2, relatedline, "");
                                                related_per.AppendFormat("\t\t{0}\n", relatedline.Replace(Environment.NewLine, ""));
                                            }
                                        }
                                    }
                                }

                                xml_per.AppendFormat("\t<{0}{1}>\n{2}{3}{4}\t</{5}>\n", xmlPerNode, perNodeAttrb, data_per, files_per.ToString(), related_per.ToString(), xmlPerNode);
                            }
                        }
                        else
                        {
                            if (xml_per.Length > 0)
                                xml_per.Remove(0, xml_per.Length - 1);
                        }
                    }
                    else
                    {
                        fileLineCount = 0;
                        if (xml_per.ToString().IndexOf("##afiles_") != -1)
                        {

                            string tmp = xml_per.ToString();
                            bool isFound = gotAFiles(tmp);
                            string aliasTemp = string.Empty;
                            string[] aliasTemps;

                            while (fileLineCount < 20 && isFound == true)
                            {
                                aliasTemp = regOp("afiles_get_alias_temp", tmp);
                                aliasTemps = aliasTemp.Split('_');
                                if (aliasTemps.Length > 1)
                                {
                                    fileLineCount++;
                                    fileAliases[fileLineCount] = aliasTemps[1];
                                    tmp = tmp.Replace("##afiles_" + aliasTemps[1] + "_", "");
                                }
                                isFound = gotAFiles(tmp);
                            }
                        }

                        if (fileLineCount > 0)
                        {
                            curAzID = vars.getValue4Dic("zone_id");
                            curTypes = "";
                            for (int i = 1; i <= fileLineCount; i++)
                            {
                                curTypes = curTypes + "," + fileAliases[i].Replace("'", "''") + "'";
                            }
                            curTypes = curTypes.Substring(2, curTypes.Length - 2);
                            if (!string.IsNullOrEmpty(curTypes))
                                curTypes = "and cft.type_alias in (" + curTypes + ") ";

                            DataTable dt7 = Dal.Instance.SelectGetAFilesData3(curTypes, Convert.ToInt32(curAzID));
                            if (dt7.Rows.Count > 0)
                            {
                                foreach (DataRow dr7 in dt7.Rows)
                                {
                                    string file_title = dt.Rows[0][10].ToString();
                                    string file_name_1 = dt.Rows[0][0].ToString();
                                    string file_name_2 = dt.Rows[0][1].ToString();
                                    string file_name_3 = dt.Rows[0][2].ToString();
                                    string file_name_4 = dt.Rows[0][3].ToString();
                                    string file_name_5 = dt.Rows[0][4].ToString();
                                    string file_name_6 = dt.Rows[0][5].ToString();
                                    string file_name_7 = dt.Rows[0][6].ToString();
                                    string file_name_8 = dt.Rows[0][7].ToString();
                                    string file_name_9 = dt.Rows[0][8].ToString();
                                    string file_name_10 = dt.Rows[0][9].ToString();
                                    string file_comment = dt.Rows[0][11].ToString();
                                    string type_alias = dt.Rows[0][12].ToString();

                                    data_per = xml_per.ToString();

                                    if (!string.IsNullOrEmpty(data_per))
                                    {
                                        if (!string.IsNullOrEmpty(file_name_1))
                                        {
                                            file_name_1 = "/i/content/" + curAzID + "_" + file_name_1;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_1_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_2))
                                        {
                                            file_name_2 = "/i/content/" + curAzID + "_" + file_name_2;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_2_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_3))
                                        {
                                            file_name_3 = "/i/content/" + curAzID + "_" + file_name_3;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_3_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_4))
                                        {
                                            file_name_4 = "/i/content/" + curAzID + "_" + file_name_4;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_4_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_5))
                                        {
                                            file_name_5 = "/i/content/" + curAzID + "_" + file_name_5;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_5_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_6))
                                        {
                                            file_name_6 = "/i/content/" + curAzID + "_" + file_name_6;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_6_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_7))
                                        {
                                            file_name_7 = "/i/content/" + curAzID + "_" + file_name_7;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_7_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_8))
                                        {
                                            file_name_8 = "/i/content/" + curAzID + "_" + file_name_8;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_8_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_9))
                                        {
                                            file_name_9 = "/i/content/" + curAzID + "_" + file_name_9;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_9_exists##", "exists");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_10))
                                        {
                                            file_name_10 = "/i/content/" + curAzID + "_" + file_name_10;
                                            data_per = data_per.Replace("##afiles_" + type_alias + "_10_exists##", "exists");
                                        }

                                        data_per = data_per.Replace("##afiles_" + type_alias + "_1##", file_name_1);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_2##", file_name_2);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_3##", file_name_3);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_4##", file_name_4);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_5##", file_name_5);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_6##", file_name_6);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_7##", file_name_7);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_8##", file_name_8);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_9##", file_name_9);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_10##", file_name_10);
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_1_extension##", getFileExtension(file_name_1));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_2_extension##", getFileExtension(file_name_2));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_3_extension##", getFileExtension(file_name_3));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_4_extension##", getFileExtension(file_name_4));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_5_extension##", getFileExtension(file_name_5));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_6_extension##", getFileExtension(file_name_6));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_7_extension##", getFileExtension(file_name_7));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_8_extension##", getFileExtension(file_name_8));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_9_extension##", getFileExtension(file_name_9));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_10_extension##", getFileExtension(file_name_10));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_title##", getFileExtension(file_title));
                                        data_per = data_per.Replace("##afiles_" + type_alias + "_comment##", getFileExtension(file_comment));
                                    }
                                    xml_per.AppendFormat("\t<{0}{1}>\n{2}{3}{4}\t</{5}>\n", xmlPerNode, perNodeAttrb, data_per, files_per.ToString(), related_per.ToString(), xmlPerNode);
                                }
                            }
                            else
                            {
                                data_per = "";
                            }
                        }
                        else
                        {
                            if (xml_out.Length > 0)
                                xml_out.Remove(0, xml_out.Length - 1);
                        }
                    }

                    xml_out.AppendFormat("{0}\n<{1} {2}>\n{3}</{4}>\n", "<?xml version=\"1.0\" encoding=\"utf-8\"?>", xmlMainNode, xmlMainNoeAttr, HttpUtility.HtmlDecode(xml_per.ToString()), xmlMainNode);

                    SetApplication(xmlCacheKey, xml_out.ToString());
                }
            }
            else
            {
                xml_out.Append(GetApplication(xmlCacheKey));
            }
            return xml_out.ToString();
        }
        #endregion
        #region UpdateIISCache
        public void UpdateIISCache()
        {
            //String LIP = string.Format("{0} - {1}", System.Environment.MachineName, HttpContext.Current.Request.ServerVariables["INSTANCE_ID"]);

            //int UpdateStatus = 0;
            //DataTable dt = Dal.Instance.CacheCheckUpdateStatus(LIP);

            //if (dt.Rows.Count > 0)
            //{
            //    UpdateStatus = Convert.ToInt32(dt.Rows[0][0]);
            //}

            //if (UpdateStatus == 1)
            //{
            //int result = Dal.Instance.CacheUpdateUpdateStatus(LIP, 2, 1);

            //int keyCount = HttpContext.Current.Application.Contents.AllKeys.Length;
            //HttpContext.Current.Application.Lock();

            //for (int x = keyCount -1; x >= 0; x--)
            //{
            //    string key = HttpContext.Current.Application.Contents.Keys[x];
            //    if (
            //        //key.StartsWith("CFG_") ||
            //        key.StartsWith("PAGE_") ||
            //        key.StartsWith("PAGE_UT_") ||
            //        key.StartsWith("TEMPLATE_") ||
            //        key.StartsWith("CSS_") ||
            //        key.StartsWith("MENU_") ||
            //        key.StartsWith("PORTLET_") ||
            //        key.StartsWith("PLUGIN_") ||
            //        key.StartsWith("STF_FORM_") ||
            //        key.StartsWith("STF_MAIL_") ||
            //        key.StartsWith("XML_") ||
            //        key.StartsWith("HIDDEN_VALUES_") ||
            //        key.StartsWith("SITEMAP_") ||
            //        key.StartsWith("BREADCRUMB_")
            //        )
            //    {
            //        HttpContext.Current.Application.Contents.Remove(key);
            //    }
            //}
            //HttpContext.Current.Application.UnLock();

            if (!string.IsNullOrEmpty(GetApplication("CFG_HOMEPAGE_" + HttpContext.Current.Request.Url.Host)))
                SetApplication("CFG_HOMEPAGE_" + HttpContext.Current.Request.Url.Host, string.Empty);

            if (!string.IsNullOrEmpty(GetApplication("CFG_ERRORPAGE_" + HttpContext.Current.Request.Url.Host)))
                SetApplication("CFG_ERRORPAGE_" + HttpContext.Current.Request.Url.Host, string.Empty);

            DataTable dt2 = Dal.Instance.ConfigSelectConfigParameters();
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    string configValue = string.Empty;
                    if (ConfigurationManager.AppSettings["EuroCMS.WS"].ToString().Equals("local"))
                    {
                        configValue = dr[2].ToString();
                    }
                    else
                    {
                        configValue = dr[3].ToString();
                    }

                    SetApplication("CFG_" + dr[1].ToString(), configValue);
                }
            }

            DataTable dt3 = Dal.Instance.SelectHiddenValues();
            if (dt3.Rows.Count > 0)
            {
                string hiddenValue = string.Empty;
                string hiddenType = string.Empty;
                foreach (DataRow dr in dt3.Rows)
                {
                    hiddenValue += "{|}" + dr[1].ToString();
                    hiddenType += "{|}" + dr[2].ToString();
                }

                if (hiddenValue.Equals("{|}"))
                    hiddenValue = "";

                if (hiddenType.Equals("{|}"))
                    hiddenType = "";

                if (hiddenValue.StartsWith("{|}"))
                    hiddenValue = hiddenValue.Substring(3, hiddenValue.Length - 3);

                if (hiddenType.StartsWith("{|}"))
                    hiddenType = hiddenType.Substring(3, hiddenType.Length - 3);

                SetApplication("HIDDEN_VALUES_VALUES", hiddenValue);
                SetApplication("HIDDEN_VALUES_TYPES", hiddenType);
            }

            DataTable dt4 = Dal.Instance.AdminSelectSitemaps(0);
            if (dt4.Rows.Count > 0)
            {
                string sitemapIds = string.Empty;
                foreach (DataRow dr in dt4.Rows)
                {
                    if (dr["enabled"].ToString().Equals("Y"))
                    {
                        sitemapIds += dr["smap_id"].ToString() + ",";
                        SetApplication("SITEMAP_" + dr["smap_id"].ToString() + "_LAST_UPDATE", dr["last_update"].ToString());
                        SetApplication("SITEMAP_" + dr["smap_id"].ToString() + "_LAST_GENERATE", dr["last_generate"].ToString());
                        SetApplication("SITEMAP_" + dr["smap_id"].ToString() + "_INTERVAL", dr["interval"].ToString());
                        SetApplication("SITEMAP_" + dr["smap_id"].ToString() + "_STATUS", dr["status"].ToString());
                    }
                }
                if (!string.IsNullOrEmpty(sitemapIds))
                    sitemapIds = sitemapIds.Substring(0, sitemapIds.Length - 1);

                SetApplication("SITEMAP_IDS", sitemapIds);
            }

            if (IsCacheActive())
            {
                //DataTable dt5 = Dal.Instance.AdminSelectCachedArticles();
                //if (dt5.Rows.Count > 0)
                //{
                //    string ids = ",";
                //    foreach (DataRow dr in dt5.Rows)
                //    {
                //        ids += dr[1].ToString() + "-" + dr[0].ToString() + ",";
                //    }
                //    SetApplication("CACHED_PAGES", ids);   
                //}

                DataTable dt6 = Dal.Instance.SelectAllTemplates();
                if (dt6.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt6.Rows)
                    {
                        SetApplication("TEMPLATE_" + dr[0].ToString(), dr[2].ToString());
                        SetApplication("TEMPLATE_" + dr[0].ToString() + "_DOCTYPE", dr[3].ToString());
                    }
                }

                DataTable dt7 = Dal.Instance.SelectAllCss();
                if (dt7.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt7.Rows)
                    {
                        SetApplication("CSS_" + dr[0].ToString(), dr[2].ToString());
                        SetApplication("CSS_FIX_" + dr[0].ToString(), dr[3].ToString());
                    }
                }

                DataTable dt8 = Dal.Instance.SelectAllPortlets();
                if (dt8.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt8.Rows)
                    {
                        SetApplication("PORTLET_" + dr[0].ToString(), HttpUtility.HtmlDecode(dr[2].ToString()));
                        SetApplication("PORTLET_CSS_" + dr[0].ToString(), HttpUtility.HtmlDecode(dr[3].ToString()));
                        SetApplication("PORTLET_HEADER_" + dr[0].ToString(), HttpUtility.HtmlDecode(dr[4].ToString()));
                        SetApplication("PORTLET_FOOTER_" + dr[0].ToString(), HttpUtility.HtmlDecode(dr[5].ToString()));
                        SetApplication("PORTLET_SHORTCUT_ENABLE_" + dr[0].ToString(), HttpUtility.HtmlDecode(dr[6].ToString()));
                    }
                }

                DataTable dt9 = Dal.Instance.SelectAllPlugins();
                if (dt9.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt9.Rows)
                    {
                        SetApplication("PLUGIN_" + dr[0].ToString(), dr[2].ToString());
                    }
                }
            }

            SetApplication("CACHED_PAGES_COUNT", "0");
            SetApplication("LAST_UPDATE", vars.TodayFull);

            //Dal.Instance.CacheUpdateUpdateStatus(LIP, 0, 0);

            add2Log(1, 0, "0", "RELOAD_CACHE_COMPLETE", "Time: " + DateTime.Now.ToString());

            if (!string.IsNullOrEmpty(GetApplication("SITEMAP_IDS")))
            {
                string[] sitemapIds = GetApplication("SITEMAP_IDS").Split(',');
                int sitemapstatus = 0;
                bool createSiteMap = false;
                foreach (string sitemapId in sitemapIds)
                {

                    string sitemap_last_update = GetApplication("SITEMAP_" + sitemapId + "_LAST_UPDATE");
                    string sitemap_last_generate = GetApplication("SITEMAP_" + sitemapId + "_LAST_GENERATE");
                    string sitemap_interval = GetApplication("SITEMAP_" + sitemapId + "_INTERVAL");
                    string sitemap_status = GetApplication("SITEMAP_" + sitemapId + "_STATUS");

                    if (CmsHelper.IsNumeric(sitemap_status))
                        sitemapstatus = Convert.ToInt32(sitemap_status);
                    else
                        sitemapstatus = 1;

                    CheckSitemapStatus(sitemap_last_update, sitemap_last_generate, sitemap_interval, sitemap_status, ref createSiteMap);
                }

                createSiteMap = true;

                if (createSiteMap)
                {
                    DataTable dt10 = Dal.Instance.AdminSelectSitemaps(0);
                    if (dt10.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt10.Rows)
                        {
                            bool createSiteMapSub = false;
                            string SiteMapID = dr["smap_id"].ToString();
                            string sitemap_last_update = dr["last_update"].ToString();
                            string last_generate = dr["last_generate"].ToString();
                            string interval = dr["interval"].ToString();
                            string status = dr["status"].ToString();

                            CheckSitemapStatus(sitemap_last_update, last_generate, interval, status, ref createSiteMapSub);

                            createSiteMapSub = true;
                            if (createSiteMapSub)
                            {
                                string param1 = getRandomChars(32);
                                string param2 = getRandomChars(32);

                                SetApplication(param1, param2);

                                string strURL = "http://" + HttpContext.Current.Request.Url.Host + "/" + Constansts.ALIAS_CRON + "/" + SiteMapID + "/" + param1 + "/" + param2;


                                if (GetApplication(Constansts.CFG_PROXY_SERVER).Equals("Y"))
                                {
                                    if (GetApplication(Constansts.CFG_PROXY_LOGIN).Equals("Y"))
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), GetApplication(Constansts.CFG_PROXY_USERNAME), GetApplication(Constansts.CFG_PROXY_PASSWORD));
                                    }
                                    else
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), "", "");
                                    }
                                }
                                else
                                {
                                    RequestHelper.SendGet(strURL);
                                }

                                SetApplication("SITEMAP_" + SiteMapID + "__LAST_UPDATE", DateTime.Now.ToString());
                                SetApplication("SITEMAP_" + SiteMapID + "_STATUS", "1");
                            }
                            else
                            {
                                SetApplication("SITEMAP_" + SiteMapID + "__LAST_UPDATE", sitemap_last_update);
                                SetApplication("SITEMAP_" + SiteMapID + "_LAST_GENERATE", last_generate);
                                SetApplication("SITEMAP_" + SiteMapID + "_INTERVAL", interval);
                                SetApplication("SITEMAP_" + SiteMapID + "_STATUS", status);
                            }
                        }
                    }
                }
            }
            //}
        }

        #region CheckSitemapStatus
        public void CheckSitemapStatus(string sitemap_last_update, string sitemap_last_generate, string sitemap_interval, string sitemap_status, ref bool createSiteMap)
        {
            DateTime lastGenerate = DateTime.Parse(sitemap_last_generate);
            DateTime lastUpdate = DateTime.Parse(sitemap_last_update);
            if (sitemap_status.Equals("1"))
            {
                createSiteMap = true;
            }
            else if (!(CmsHelper.IsDate2(sitemap_last_update) || CmsHelper.IsDate2(sitemap_last_generate)))
            {
                createSiteMap = true;
            }
            else if (
                sitemap_status.Equals("0") &&
                lastGenerate.AddHours(Convert.ToInt32(sitemap_interval)) <= DateTime.Now ||
                (sitemap_status.Equals("2") && lastUpdate.AddMinutes(Constansts.SITEMAP_CREATE_TIMEOUT) <= DateTime.Now)
            )
            {
                createSiteMap = true;
            }
        }
        #endregion
        #endregion
        #region checkRedirectionAlias
        public string checkRedirectionAlias(string[] QS)
        {
            string articleLink = string.Empty;
            bool gotAlias = false;
            string alias = string.Empty;
            string page = string.Empty;
            string[] newQS = new string[] { };
            int pageID = 1;
            string pid = string.Empty;

            alias = noInj(QS[0].ToLower());

            DataTable dt = Dal.Instance.SelectRedirectionByAlias(alias, HttpContext.Current.Request.Url.Host);
            if (dt.Rows.Count > 0)
            {
                string zoneID = dt.Rows[0][1].ToString();
                string articleID = dt.Rows[0][0].ToString();
                string siteName = dt.Rows[0][2].ToString();
                string zoneGroupName = dt.Rows[0][3].ToString();
                string zoneName = dt.Rows[0][4].ToString();
                string headline = dt.Rows[0][5].ToString();
                bool permanentRedirection = Convert.ToBoolean(dt.Rows[0][6]);

                articleLink = getContentLink(zoneID, articleID, siteName, zoneGroupName, zoneName, headline);

                if (permanentRedirection)
                {
                    HttpContext.Current.Response.Status = "301 Moved Permanently";
                    HttpContext.Current.Response.AddHeader("Location", articleLink);
                    HttpContext.Current.Response.End();
                }

                gotAlias = true;
                newQS = articleLink.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (gotAlias == false)
            {
                string fullAlias = string.Join("/", QS);

                if (fullAlias.IndexOf("?") != -1)
                {
                    string[] arrFullAlias = fullAlias.Split('?');
                    fullAlias = arrFullAlias[0];
                }

                page = QS[QS.Length - 1].ToLower();
                if (string.IsNullOrEmpty(page))
                    page = QS[QS.Length - 2].ToLower();

                if (page.StartsWith("p:") && page.Length > 2)
                {
                    page = page.Substring(2);
                    if (CmsHelper.IsNumeric(page))
                    {
                        pageID = Convert.ToInt32(page);
                        pid = "/p:" + page;
                    }
                }

                if (fullAlias.StartsWith("/"))
                    fullAlias = fullAlias.Substring(1);
                if (fullAlias.EndsWith("/"))
                    fullAlias = fullAlias.Substring(0, fullAlias.Length - 2);
                if (!string.IsNullOrEmpty(pid))
                    fullAlias = fullAlias.Replace(pid, "");

                vars.fullAlias = fullAlias;

                //DataTable dt2 = Dal.Instance.SelectArticleByAlias(noInj(fullAlias.ToLower()), Request.Url.Host);
                DataTable dt2 = Dal.Instance.SelectArticleByAlias(noInj(fullAlias.ToLower()), HttpContext.Current.Request.Url.Host);
                if (dt2.Rows.Count > 0)
                {
                    string zoneID = dt2.Rows[0][1].ToString();
                    string articleID = dt2.Rows[0][0].ToString();

                    articleLink = Constansts.ALIAS_CONTENT + "/" + zoneID + "-" + articleID + "-1-" + pageID.ToString() + "-1/" + fullAlias + "/";
                    gotAlias = true;
                    newQS = articleLink.Split('/');
                }
            }

            if (gotAlias)
                return renderPage(newQS);
            else
                return render404(QS);
        }
        #endregion
        #region renderSiteMap
        public string renderSiteMap(string[] QS)
        {
            string defaultXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine +
                  "<?xml-stylesheet type=\"text/xsl\" href=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/h/sitemap.xsl\"?>" + Environment.NewLine +
                  "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">" + Environment.NewLine +
                  "\t" + "<url>" + Environment.NewLine +
                  "\t" + "\t" + "<loc>" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/</loc>" + Environment.NewLine;
            if (!UrlHelper.IsSitemapOnlyURL())
            {
                defaultXml += "\t" + "\t" + "<lastmod>" + cAD2RssAtomD(DateTime.Now) + "</lastmod>" + Environment.NewLine +
                  "\t" + "\t" + "<changefreq>daily</changefreq>" + Environment.NewLine +
                  "\t" + "\t" + "<priority>1.0</priority>" + Environment.NewLine;
            }
            defaultXml += "\t" + "</url>" + Environment.NewLine + "</urlset>";

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ContentType = "text/xml";

            if (QS.Length < 2)
                return defaultXml;
            else
            {
                if (QS[1].IndexOf(".") == -1)
                    return defaultXml;
                else
                {
                    string xml = string.Empty;
                    byte[] gz = new byte[] { };
                    string gzEnabled = string.Empty;

                    DataTable dt = Dal.Instance.AdminSelectSitemapByDomain(HttpContext.Current.Request.Url.Host);
                    if (dt.Rows.Count > 0)
                    {
                        xml = dt.Rows[0]["xml"].ToString();
                        if (dt.Rows[0]["gz"] != DBNull.Value)
                            gz = (byte[])dt.Rows[0]["gz"];
                        gzEnabled = dt.Rows[0]["gzip_enabled"].ToString();
                    }
                    if (string.IsNullOrEmpty(xml))
                        xml = defaultXml;

                    string ext = QS[1].Split('.')[1].ToLower();
                    switch (ext)
                    {
                        case "xml":
                            return xml;
                        //break;
                        case "gz":
                            if (!gzEnabled.Equals("Y"))
                                return defaultXml;
                            else
                            {
                                HttpContext.Current.Response.AddHeader("content-transfer-encoding", "binary");
                                HttpContext.Current.Response.AddHeader("Content-Encoding", "gzip");
                                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=sitemap.xml.gz");
                                if (gz.Length > 0)
                                    HttpContext.Current.Response.BinaryWrite(gz);
                                HttpContext.Current.Response.Flush();
                            }
                            break;

                            ////return "";
                    }
                }
            }
            // HttpContext.Current.Response.End();
            return string.Empty;
        }
        #endregion
        #region updateSiteMap
        public void updateSiteMap(string[] QS)
        {

            if (QS.Length < 4)
                return;

            string rCode = string.Empty;
            string INCLUDED_SITES = string.Empty;
            string EXCLUDED_ZONEGROUPS = string.Empty;
            string EXCLUDED_ZONES = string.Empty;
            string EXCLUDED_ARTICLES = string.Empty;
            string AFILES = string.Empty;
            string DOMAIN_ALIAS = string.Empty;
            string NOTIFY_GOOGLE = string.Empty;
            string NOTIFY_MSN = string.Empty;
            string NOTIFY_ASK = string.Empty;
            string NOTIFY_YAHOO = string.Empty;
            string YAHOO_ID = string.Empty;
            string GZIP_ENABLED = string.Empty;


            string param1 = noInj(QS[2]);
            string param2 = noInj(QS[3]);
            string sMapId = noInj(QS[1]);
            int siteMapID = 0;

            if (CmsHelper.IsNumeric(sMapId))
                siteMapID = Convert.ToInt32(sMapId);

            if (GetApplication(param1) == param2 || ConfigurationManager.AppSettings["EuroCMS.WS"].Equals("local"))
            {
                DataTable dt = Dal.Instance.AdminSelectSitemapStatus(siteMapID);
                if (dt.Rows.Count > 0)
                {
                    rCode = dt.Rows[0]["rCode"].ToString();
                    if (rCode.Equals("OK"))
                    {
                        SetApplication("SITEMAP_" + siteMapID + "_LAST_UPDATE", dt.Rows[0]["last_update"].ToString());
                        SetApplication("SITEMAP_" + siteMapID + "_STATUS", "2");

                        INCLUDED_SITES = dt.Rows[0]["included_sites"].ToString().Trim();
                        EXCLUDED_ZONEGROUPS = dt.Rows[0]["excluded_zonegroups"].ToString().Trim();
                        EXCLUDED_ZONES = dt.Rows[0]["excluded_zones"].ToString().Trim();
                        EXCLUDED_ARTICLES = dt.Rows[0]["excluded_articles"].ToString().Trim();
                        AFILES = dt.Rows[0]["afiles"].ToString();
                        DOMAIN_ALIAS = dt.Rows[0]["domain_alias"].ToString();
                        NOTIFY_GOOGLE = dt.Rows[0]["notify_google"].ToString();
                        NOTIFY_MSN = dt.Rows[0]["notify_msn"].ToString();
                        NOTIFY_ASK = dt.Rows[0]["notify_ask"].ToString();
                        NOTIFY_YAHOO = dt.Rows[0]["notify_yahoo"].ToString();
                        YAHOO_ID = dt.Rows[0]["yahoo_id"].ToString();
                        GZIP_ENABLED = dt.Rows[0]["gzip_enabled"].ToString();
                    }
                }

                if (rCode.Equals("OK"))
                {
                    if (!string.IsNullOrEmpty(INCLUDED_SITES))
                    {
                        string str_xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<?xml-stylesheet type=\"text/xsl\" href=\"" + UrlHelper.GetUriScheme() + DOMAIN_ALIAS + "/h/sitemap.xsl\"?>" + Environment.NewLine + "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">" + Environment.NewLine + "<url>" + Environment.NewLine + "<loc>" + UrlHelper.GetUriScheme() + DOMAIN_ALIAS + "/</loc>" + Environment.NewLine;
                        if (!UrlHelper.IsSitemapOnlyURL())
                        {
                            str_xml += "<lastmod>" + cAD2RssAtomD(DateTime.Now) + "</lastmod>" + Environment.NewLine + "<changefreq>daily</changefreq>" + Environment.NewLine + "<priority>1.0</priority>" + Environment.NewLine;
                        }
                        str_xml += "</url>" + Environment.NewLine;

                        //DataTable dt = Dal.Instance.SelectSitemapIncludeSites(INCLUDED_SITES);

                        string strSQL = "Select zone_id, article_id, site_name, zone_group_name, zone_name, headline, az_alias, updated " +
                         "FROM dbo.vArticlesZonesFull with (nolock) " +
                         "WHERE status = 1 AND zone_type_id = 0 AND article_type = 0 AND ((startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate())) AND site_id in (" + INCLUDED_SITES + ") ";

                        if (!string.IsNullOrEmpty(EXCLUDED_ZONEGROUPS))
                            strSQL = strSQL + "AND zone_group_id not in (" + EXCLUDED_ZONEGROUPS + ") ";

                        if (!string.IsNullOrEmpty(EXCLUDED_ZONES))
                            strSQL = strSQL + "AND zone_id not in (" + EXCLUDED_ZONES + ") ";

                        if (!string.IsNullOrEmpty(EXCLUDED_ARTICLES))
                        {
                            EXCLUDED_ARTICLES = EXCLUDED_ARTICLES.Replace(",", "','");
                            strSQL = strSQL + "AND (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10)) not in ('" + EXCLUDED_ARTICLES + "'))";
                        }

                        DataTable dt1 = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                string articleLink = getContentLinkAlias(dr1[0].ToString(), dr1[1].ToString(), dr1[2].ToString(), dr1[3].ToString(), dr1[4].ToString(), dr1[5].ToString(), dr1[6].ToString());

                                str_xml = str_xml + "<url><loc>" + UrlHelper.GetUriScheme() + DOMAIN_ALIAS + articleLink + "/</loc>";
                                if (!UrlHelper.IsSitemapOnlyURL())
                                    str_xml += "<lastmod>" + cAD2RssAtomD(Convert.ToDateTime(dr1[7])) + "</lastmod><changefreq>daily</changefreq><priority>0.9</priority>";
                                str_xml += "</url>" + Environment.NewLine;
                            }
                        }

                        if (AFILES.Equals("Y"))
                        {
                            strSQL = "Select af.article_id, af.file_name_1, af.file_name_2, af.file_name_3, af.file_name_4, af.file_name_5, af.file_name_6, af.file_name_7, af.file_name_8, af.file_name_9, af.file_name_10, afr.approval_date from dbo.cms_article_files af with (nolock)  inner join dbo.cms_article_files_revision afr with (nolock) on afr.article_id = af.article_id AND afr.revision_status='L' where af.article_id in (" +
                             "Select article_id " +
                             "FROM dbo.vArticlesZonesFull with (nolock) " +
                              "WHERE status = 1 AND zone_type_id = 0 AND article_type = 0 AND ((startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate())) AND site_id in (" + INCLUDED_SITES + ") ";

                            if (!string.IsNullOrEmpty(EXCLUDED_ZONEGROUPS))
                                strSQL = strSQL + "AND zone_group_id not in (" + EXCLUDED_ZONEGROUPS + ") ";

                            if (!string.IsNullOrEmpty(EXCLUDED_ZONES))
                                strSQL = strSQL + "AND zone_id not in (" + EXCLUDED_ZONES + ") ";

                            if (!string.IsNullOrEmpty(EXCLUDED_ARTICLES))
                            {
                                EXCLUDED_ARTICLES = EXCLUDED_ARTICLES.Replace(",", "','");
                                strSQL = strSQL + "AND (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10)) not in ('" + EXCLUDED_ARTICLES + "'))";
                            }
                            strSQL = strSQL + ")";

                            DataTable dt2 = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr3 in dt2.Rows)
                                {
                                    for (int f = 1; f <= 10; f++)
                                    {
                                        if (!string.IsNullOrEmpty(dr3[f].ToString()))
                                        {
                                            str_xml = str_xml +
                                              "\n" + "<url>" + Environment.NewLine +
                                              "\n" + "\n" + "<loc>" + UrlHelper.GetUriScheme() + DOMAIN_ALIAS + "/i/content/" + dr3[0].ToString() + "_" + dr3[f].ToString() + "</loc>" + Environment.NewLine;

                                            if (!UrlHelper.IsSitemapOnlyURL())
                                            {
                                                str_xml += "\n" + "\n" + "<lastmod>" + cAD2RssAtomD(Convert.ToDateTime(dr3[6])) + "</lastmod>" + Environment.NewLine +
                                                "\n" + "\n" + "<changefreq>daily</changefreq>" + Environment.NewLine +
                                                "\n" + "\n" + "<priority>0.75</priority>" + Environment.NewLine;
                                            }

                                            str_xml += "\n" + "</url>" + Environment.NewLine;
                                        }
                                    }
                                }
                            }
                        }

                        str_xml = str_xml + "</urlset>";

                        DataTable dt4 = Dal.Instance.AdminUpdateSitemapStatus(siteMapID, 0, str_xml);
                        if (dt4.Rows.Count > 0)
                        {
                            SetApplication("SITEMAP_" + siteMapID + "_LAST_UPDATE", dt4.Rows[0]["rDate"].ToString());
                            SetApplication("SITEMAP_" + siteMapID + "_LAST_GENERATE", dt4.Rows[0]["rDate"].ToString());
                            SetApplication("SITEMAP_" + siteMapID + "_STATUS", "0");
                        }

                        if (GZIP_ENABLED.Equals("Y"))
                        {
                            //Chilkat.Gzip gzip = new Chilkat.Gzip();
                            //gzip.UnlockComponent(GetApplication(Constansts.CFG_CHILKAT_ZIP_KEY));
                            //byte[] gzBytes = gzip.CompressString(str_xml, getCharset());
                            //Dal.Instance.AdminUpdateSitemapGZ(siteMapID, gzBytes);
                        }


                        if (!ConfigurationManager.AppSettings["EuroCMS.WS"].ToString().Equals("local"))
                        {
                            if (NOTIFY_GOOGLE.Equals("Y"))
                            {
                                string strURL = "http://www.google.com/webmasters/sitemaps/ping?sitemap=http://" + DOMAIN_ALIAS + "/sitemap/sitemap.gz";
                                if (GZIP_ENABLED.Equals("Y"))
                                {
                                    strURL = "http://www.google.com/webmasters/sitemaps/ping?sitemap=http://" + DOMAIN_ALIAS + "/sitemap/sitemap.xml";
                                }

                                if (GetApplication(Constansts.CFG_PROXY_SERVER).Equals("Y"))
                                {
                                    if (GetApplication(Constansts.CFG_PROXY_LOGIN).Equals("Y"))
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), GetApplication(Constansts.CFG_PROXY_USERNAME), GetApplication(Constansts.CFG_PROXY_PASSWORD));
                                    }
                                    else
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), "", "");
                                    }
                                }
                                else
                                {
                                    RequestHelper.SendGet(strURL);
                                }
                            }

                            if (NOTIFY_YAHOO.Equals("Y"))
                            {
                                string strURL = "http://search.yahooapis.com/SiteExplorerService/V1/updateNotification?appid=" + YAHOO_ID + "&url=http://" + DOMAIN_ALIAS + "/sitemap/sitemap.xml";
                                if (GZIP_ENABLED.Equals("Y"))
                                {
                                    strURL = "http://search.yahooapis.com/SiteExplorerService/V1/updateNotification?appid=" + YAHOO_ID + "&url=http://" + DOMAIN_ALIAS + "/sitemap/sitemap.gz";
                                }

                                if (GetApplication(Constansts.CFG_PROXY_SERVER).Equals("Y"))
                                {
                                    if (GetApplication(Constansts.CFG_PROXY_LOGIN).Equals("Y"))
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), GetApplication(Constansts.CFG_PROXY_USERNAME), GetApplication(Constansts.CFG_PROXY_PASSWORD));
                                    }
                                    else
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), "", "");
                                    }
                                }
                                else
                                {
                                    RequestHelper.SendGet(strURL);
                                }
                            }

                            if (NOTIFY_MSN.Equals("Y"))
                            {
                                string strURL = "http://webmaster.live.com/ping.aspx?siteMap=http://" + DOMAIN_ALIAS + "/sitemap/sitemap.xml";
                                if (GetApplication(Constansts.CFG_PROXY_SERVER).Equals("Y"))
                                {
                                    if (GetApplication(Constansts.CFG_PROXY_LOGIN).Equals("Y"))
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), GetApplication(Constansts.CFG_PROXY_USERNAME), GetApplication(Constansts.CFG_PROXY_PASSWORD));
                                    }
                                    else
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), "", "");
                                    }
                                }
                                else
                                {
                                    RequestHelper.SendGet(strURL);
                                }
                            }

                            if (NOTIFY_ASK.Equals("Y"))
                            {
                                string strURL = "http://submissions.ask.com/ping?sitemap=http://" + DOMAIN_ALIAS + "/sitemap/sitemap.xml";
                                if (GetApplication(Constansts.CFG_PROXY_SERVER).Equals("Y"))
                                {
                                    if (GetApplication(Constansts.CFG_PROXY_LOGIN).Equals("Y"))
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), GetApplication(Constansts.CFG_PROXY_USERNAME), GetApplication(Constansts.CFG_PROXY_PASSWORD));
                                    }
                                    else
                                    {
                                        RequestHelper.SendGetWithProxy(strURL, GetApplication(Constansts.CFG_PROXY_SERVER), "", "");
                                    }
                                }
                                else
                                {
                                    RequestHelper.SendGet(strURL);
                                }
                            }

                            HttpContext.Current.Response.Write("sitemap created");
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Write(rCode);
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("sitemap is already in creating status. no work done");
                }
            }
            else
            {
                HttpContext.Current.Response.Write("security parameters incorrect");
            }
        }


        #endregion
        #region renderRSS
        public string renderRSS(string[] QS)
        {
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.ContentType = "text/xml";
            HttpContext.Current.Response.Charset = "utf-8";

            string version, channel_id = string.Empty;
            StringWriter stringWriter = new StringWriter();

            if (QS.Length > 2)
            {
                version = QS[1];
                channel_id = QS[2];
                if (CmsHelper.IsNumeric(channel_id))
                {

                    using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
                    {
                        renderRSSHeader(Convert.ToInt32(channel_id), version, writer);
                        renderRSSContent(Convert.ToInt32(channel_id), version, writer);
                        renderRSSFooter(Convert.ToInt32(channel_id), version, writer);
                        return stringWriter.ToString();
                    }
                    // HttpContext.Current.Response.End();
                }
                else
                {
                    return renderHome(string.Empty);
                }
            }

            return string.Empty;
        }

        #region renderRSSHeader
        public void renderRSSHeader(int channelID, string version, HtmlTextWriter writer)
        {
            string pubDate, updated, lastBuildDateStr;
            DateTime lastBuildDate;
            string rssHeader = string.Empty;

            DataTable dt = Dal.Instance.SelectRssChannelDetails(channelID);
            if (dt.Rows.Count > 0)
            {
                string channel_name = dt.Rows[0][1].ToString();
                string channel_url = dt.Rows[0][2].ToString();
                string channel_description = dt.Rows[0][3].ToString();
                string lang_id = dt.Rows[0][4].ToString().ToLower();
                string managing_editor = dt.Rows[0][5].ToString();
                string copyright = dt.Rows[0][6].ToString();
                string channel_status = dt.Rows[0][11].ToString();
                string rss_chanel_summary_content_field = dt.Rows[0][14].ToString();
                string rss_channel_content_template = dt.Rows[0][15].ToString();
                string rss_singularize_articles = dt.Rows[0][17].ToString();

                vars.setValue2Dic("channel_name", channel_name);
                vars.setValue2Dic("channel_url", channel_url);
                vars.setValue2Dic("channel_description", channel_description);
                vars.setValue2Dic("lang_id", lang_id);
                vars.setValue2Dic("managing_editor", managing_editor);
                vars.setValue2Dic("copyright", copyright);
                vars.setValue2Dic("channel_status", channel_status);
                vars.setValue2Dic("rss_chanel_summary_content_field", rss_chanel_summary_content_field);
                vars.setValue2Dic("rss_channel_content_template", rss_channel_content_template);
                vars.setValue2Dic("rss_singularize_articles", rss_singularize_articles);

                string names = getRecordsetNames(dt);
                string[] arrNames = names.Split(',');

                foreach (string n in arrNames)
                {
                    vars.setValue2Dic(n, dt.Rows[0][n]);
                }

                if (!channel_status.Equals("A"))
                    renderHome(string.Empty);

                pubDate = cAD2Rss20D(DateTime.Now, "en");
                lastBuildDateStr = GetApplication(Constansts.LAST_UPDATE);
                if (!CmsHelper.IsDate2(lastBuildDateStr))
                    lastBuildDate = DateTime.Now;
                else
                    lastBuildDate = DateTime.Parse(lastBuildDateStr);

                updated = cAD2Rss20D(lastBuildDate, "en");
                lastBuildDateStr = cAD2Rss20D(lastBuildDate, "en");

                switch (version)
                {
                    case "20":
                        rssHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine +
                                    "<!-- generator=\"eurocms/1.0.1\" -->" + Environment.NewLine +
                                    "<rss version=\"2.0\"" + Environment.NewLine +
                                    "	xmlns:content=\"http://purl.org/rss/1.0/modules/content/\"" + Environment.NewLine +
                                    "	xmlns:wfw=\"http://wellformedweb.org/CommentAPI/\"" + Environment.NewLine +
                                    "	xmlns:dc=\"http://purl.org/dc/elements/1.1/\"" + Environment.NewLine +
                                    "	>" + Environment.NewLine +
                                    "<channel>" + Environment.NewLine +
                                    "	<title>" + channel_name + "</title>" + Environment.NewLine +
                                    "	<link>" + channel_url + "</link>" + Environment.NewLine +
                                    "	<description>" + channel_description + "</description>" + Environment.NewLine +
                                    "	<managingEditor>" + managing_editor + "</managingEditor>" + Environment.NewLine +
                                    "	<copyright>" + copyright + "</copyright>" + Environment.NewLine +
                                    "	<pubDate>" + pubDate + "</pubDate>" + Environment.NewLine +
                                    "	<lastBuildDate>" + lastBuildDate + "</lastBuildDate>" + Environment.NewLine +
                                    "	<generator>" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/cms/rss/?v=1.0.1</generator>" + Environment.NewLine +
                                    "	<language>" + lang_id + "</language>" + Environment.NewLine;
                        break;
                    case "atom":
                        rssHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><feed version=\"0.3\"" + Environment.NewLine +
                            "	xmlns=\"http://purl.org/atom/ns#\"" + Environment.NewLine +
                            "	xmlns:dc=\"http://purl.org/dc/elements/1.1/\"" + Environment.NewLine +
                            "	xml:lang=\"" + lang_id + "\"" + Environment.NewLine +
                            "	>" + Environment.NewLine +
                            "	<title>" + channel_name + "</title>" + Environment.NewLine +
                            "	<link rel=\"alternate\" type=\"text/html\" href=\"" + UrlHelper.UriReplacer(channel_url) + "\" />" + Environment.NewLine +
                            "	<subtitle>" + channel_description + "</subtitle>" + Environment.NewLine +
                            "	<modified>" + updated + "</modified>" + Environment.NewLine +
                            "	<updated>" + updated + "</updated>" + Environment.NewLine +
                            "	<copyright>" + copyright + "</copyright>" + Environment.NewLine +
                            "	<generator url=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/cms/rss/?v=1.0.1\" version=\"1.0.1\">EuroCMS</generator>" + Environment.NewLine;
                        break;
                }
            }

            writer.Write(rssHeader);
        }
        #endregion
        #region getTRDayName
        private string cAD2Rss20D(DateTime dateTime, string lang)
        {
            return string.Format("{0},{1} {2} {3} {4}:{5}:{6}+0000",
                emSWD(dateTime.DayOfWeek, lang),
                CmsHelper.dDay(dateTime.Day.ToString()),
                emSMN(dateTime.Month, lang, "S"),
                CmsHelper.dDay(dateTime.Year.ToString()),
                CmsHelper.dDay(dateTime.Hour.ToString()),
                CmsHelper.dDay(dateTime.Minute.ToString()),
                CmsHelper.dDay(dateTime.Second.ToString()));
        }
        #endregion
        #region getTRDayName
        public string getTRDayName(DayOfWeek day)
        {
            return emSWD(day, "TR");
        }
        #endregion
        #region emSWD
        public string emSWD(DayOfWeek day, string lang)
        {
            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
            return dtfi.GetAbbreviatedDayName(day);
        }
        #endregion
        #region emSMN
        public string emSMN(int monthIndex, string lang, string s)
        {
            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(lang).DateTimeFormat;
            return s.Equals("S") ? dtfi.GetAbbreviatedMonthName(monthIndex) : dtfi.GetMonthName(monthIndex);
        }
        #endregion
        #region renderRSSContent
        public void renderRSSContent(int channelID, string version, HtmlTextWriter writer)
        {
            string full_rss_dom = UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host;

            string writed_article_ids = ",";
            DataTable dt = Dal.Instance.SelectRssChannelContents(channelID);
            if (dt.Rows.Count > 0)
            {
                string names = getRecordsetNames(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string[] arrNames = names.Split(',');

                    foreach (string n in arrNames)
                    {
                        vars.setValue2Dic(n, convertAmpersands(dr[n].ToString()).Replace("\"[br]\"", "\" \""));
                    }

                    if ((vars.getValue4Dic("rss_singularize_articles").Equals("Y") &&
                            writed_article_ids.IndexOf("," + vars.getValue4Dic("article_id") + ",") != -1) ||
                           vars.getValue4Dic("rss_singularize_articles").Equals("N"))
                    {
                        if (vars.getValue4Dic("rss_singularize_articles").Equals("Y"))
                            writed_article_ids += vars.getValue4Dic("article_id") + ",";

                        string summary_content_field = string.Empty;
                        string rss_chanel_summary_content_field = vars.getValue4Dic("rss_chanel_summary_content_field");

                        if (!rss_chanel_summary_content_field.Equals("none"))
                            summary_content_field = replaceRSSContent(rss_chanel_summary_content_field.ToString(), full_rss_dom);

                        string rss_channel_content_template = replaceRSSContent(vars.getValue4Dic("rss_channel_content_template"), full_rss_dom);

                        if (gotPlugin(rss_channel_content_template))
                            rss_channel_content_template = regOp("rss_clean_plugins", rss_channel_content_template);
                        if (gotMenu(rss_channel_content_template))
                            rss_channel_content_template = regOp("rss_clean_menus", rss_channel_content_template);
                        if (gotPortlet(rss_channel_content_template))
                            rss_channel_content_template = regOp("rss_clean_portlets", rss_channel_content_template);

                        string rss_channel_content = replaceArticleDetailsRows(dr, names, vars.getValue4Dic("rss_channel_content_template"), "");

                        if (gotAFiles(rss_channel_content))
                            rss_channel_content = convertAmpersands(processAFiles(rss_channel_content, Convert.ToInt32(vars.getValue4Dic("article_id"))));

                        string article_url = full_rss_dom + getContentLinkAlias(vars.getValue4Dic("zone_id"),
                            vars.getValue4Dic("article_id"),
                            vars.getValue4Dic("site_name"),
                            vars.getValue4Dic("zone_group_name"),
                            vars.getValue4Dic("zone_name"),
                            vars.getValue4Dic("headline"),
                            vars.getValue4Dic("az_alias"));

                        rss_channel_content = replaceRSSContent(rss_channel_content, full_rss_dom);

                        if (rss_chanel_summary_content_field.Equals("template"))
                            summary_content_field = rss_channel_content;

                        string startdate = string.Empty;
                        string updated = string.Empty;

                        switch (version)
                        {
                            case "20":
                                startdate = cAD2Rss20D(Convert.ToDateTime(vars.a["startdate"]), "en");
                                updated = cAD2Rss20D(Convert.ToDateTime(vars.a["updated"]), "en");
                                writer.Write(" <item>");
                                writer.Write("  <title><![CDATA[" + HttpUtility.HtmlDecode(vars.a["headline"].ToString()) + "]]></title>");
                                writer.Write("  <link>" + article_url + "</link>");
                                writer.Write("  <pubDate>" + startdate + "</pubDate>");
                                writer.Write("  <category><![CDATA[" + HttpUtility.HtmlDecode(vars.a["zone_name"].ToString()) + "]]></category>");
                                writer.Write("  <guid isPermaLink=\"false\"><![CDATA[" + article_url + "]]></guid>");
                                writer.Write("  <description><![CDATA[" + HttpUtility.HtmlDecode(summary_content_field) + "]]></description>");
                                writer.Write("     <content:encoded><![CDATA[" + HttpUtility.HtmlDecode(rss_channel_content) + "]]></content:encoded>");
                                writer.Write("  </item>		");
                                break;
                            case "atom":
                                startdate = cAD2RssAtomD(Convert.ToDateTime(vars.a["startdate"]));
                                updated = cAD2RssAtomD(Convert.ToDateTime(vars.a["updated"]));
                                writer.Write(" <entry>");
                                writer.Write("      <title type=\"text/html\" mode=\"escaped\"><![CDATA[" + HttpUtility.HtmlDecode(vars.a["headline"].ToString()) + "]]></title>");
                                writer.Write("      <link rel=\"alternate\" type=\"text/html\" href=\"" + UrlHelper.UriReplacer(article_url) + "\"/>");
                                writer.Write("      <id>" + article_url + "</id>");
                                writer.Write("      <modified>" + updated + "</modified>");
                                writer.Write("      <updated>" + updated + "</updated>");
                                writer.Write("      <issued>" + startdate + "</issued>");
                                writer.Write("      <dc:subject><![CDATA[" + HttpUtility.HtmlDecode(vars.a["zone_name"].ToString()) + "]]></dc:subject>");
                                writer.Write("      <summary type=\"text/html\" mode=\"escaped\"><![CDATA[" + HttpUtility.HtmlDecode(summary_content_field) + "]]></summary>");
                                writer.Write("      <content type=\"text/html\" mode=\"escaped\" xml:base=\"" + article_url + "\"><![CDATA[" + HttpUtility.HtmlDecode(rss_channel_content) + "]]></content>");
                                writer.Write("  </entry>");
                                break;
                        }
                    }
                    writer.Flush();
                }

            }
        }
        #endregion
        #region renderRSSFooter
        public void renderRSSFooter(int channelID, string version, HtmlTextWriter writer)
        {
            if (version.Equals("20"))
            {
                writer.Write("</channel></rss>");
            }
            else
            {
                writer.Write("</feed>");
            }
        }
        #endregion
        #region replaceRSSContent
        public string replaceRSSContent(string contentField, string full_rss_dom)
        {
            string contentFieldTemp = contentField;
            contentFieldTemp = contentFieldTemp.Replace("src=\"./", "src=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("SRC=\"./", "src=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("src=\"/", "src=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("SRC=\"/", "src=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("href=\"./", "href=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("HREF=\"./", "href=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("href=\"/", "href=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("HREF=\"/", "href=\"" + full_rss_dom + "/");
            contentFieldTemp = contentFieldTemp.Replace("+nbsp;", "+#160;");
            contentFieldTemp = contentFieldTemp.Replace("+euro;", "+#8364;");
            contentFieldTemp = contentFieldTemp.Replace("Î\"", "+#916;");
            contentFieldTemp = contentFieldTemp.Replace("+reg;", "+#174;");
            return contentFieldTemp;
        }
        #endregion
        #endregion
        #region renderHome
        public string renderHome(string errorText)
        {
            string hpa = string.Empty;
            string[] hpb;
            string[] newQS = new string[] { };

            //rhp++;

            //if (rhp > 10)
            //{
            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.Write(string.Format("Unexpected Loop on Home Page Display. This error can be caused by a lot of 404 page. Please contact with {0} <br> {1}", GetApplication(Constansts.CFG_ADMIN_CONTACT), errorText));
            //    //Response.End();
            //}

            hpa = GetApplication("CFG_HOMEPAGE_" + HttpContext.Current.Request.Url.Host);

            if (string.IsNullOrEmpty(hpa))
                updateDomainCache();

            hpa = GetApplication("CFG_HOMEPAGE_" + HttpContext.Current.Request.Url.Host);

            if (CmsHelper.IsNumeric(hpa.Replace("-", "")))
            {
                hpb = hpa.Split('-');
                newQS = (Constansts.ALIAS_CONTENT + "/" + hpb[0] + "-" + hpb[1] + "").Split('/');
                return renderPage(newQS);
            }
            else
            {
                //HttpContext.Current.Response.Clear();
                return string.Format("Config Error: Please contact with {0} ", GetApplication(Constansts.CFG_ADMIN_CONTACT));
                //HttpContext.Current.Response.End();
            }

            return string.Empty;
        }

        private void updateDomainCache()
        {
            HttpContext.Current.Application.Lock();
            foreach (String key in HttpContext.Current.Application.Contents.AllKeys)
            {
                if (HttpContext.Current.Application[key] != null)
                {
                    if (key.StartsWith("CFG_HOMEPAGE_") || key.StartsWith("CFG_ERRORPAGE_"))
                        SetApplication(key, string.Empty);
                }
            }
            HttpContext.Current.Application.UnLock();

            DataTable dt = Dal.Instance.SelectUpdateDomainCacheDomains();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string[] domains = dr[0].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    string hpa = dr[1].ToString();
                    string epa = dr[2].ToString();

                    foreach (string domain in domains)
                    {
                        if (!string.IsNullOrEmpty(domain.Trim()))
                        {
                            SetApplication("CFG_HOMEPAGE_" + domain, hpa);
                            SetApplication("CFG_ERRORPAGE_" + domain, epa);
                        }
                    }
                }
            }
        }
        #endregion
        #region render404
        public string render404(string[] QS)
        {
            string epa = GetApplication("CFG_ERRORPAGE_" + HttpContext.Current.Request.Url.Host);

            if (string.IsNullOrEmpty(epa))
                updateDomainCache();

            epa = GetApplication("CFG_ERRORPAGE_" + HttpContext.Current.Request.Url.Host);

            if (!string.IsNullOrEmpty(epa))
            {
                if (CmsHelper.IsNumeric(epa.Replace("-", "")))
                {
                    string[] epb = epa.Split('-');
                    string[] newQS = (Constansts.ALIAS_CONTENT + "/" + epb[0] + "," + epb[1]).Split('/');

                    string url404 = UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/" + noInj(string.Join("/", QS));
                    if (GetApplication(Constansts.CFG_404_ERROR_LOG).Equals("Y"))
                    {
                        string qstr = noInj(string.Join("/", QS));
                        if (qstr.IndexOf(".") != -1)
                        {
                            string[] qstrArr = qstr.Split('.');
                            string notAllowedExtensions = GetApplication(Constansts.CFG_404_ERROR_EXTENSIONS).Replace(";", "");

                            if (!notAllowedExtensions.StartsWith(","))
                                notAllowedExtensions = "," + notAllowedExtensions;

                            if (!notAllowedExtensions.EndsWith(","))
                                notAllowedExtensions = notAllowedExtensions + ",";

                            string url404Extension = "," + qstrArr[qstrArr.Length - 1] + ",";
                            if (notAllowedExtensions.IndexOf(url404Extension) == 1)
                                add2Log(1, 0, "url", "404_ERROR", "<b>URL:</b> " + url404 + Environment.NewLine + "<b>IP:</b> " + CmsHelper.GetCurrentIP() + Environment.NewLine + "<b>ALL_RAW DATA:</b> " + noInj(HttpContext.Current.Request.ServerVariables["ALL_RAW"].ToString()));
                        }
                        else
                        {
                            add2Log(1, 0, "url", "404_ERROR", "<b>URL:</b> " + url404 + Environment.NewLine + "<b>IP:</b> " + CmsHelper.GetCurrentIP() + Environment.NewLine + "<b>ALL_RAW DATA:</b> " + noInj(HttpContext.Current.Request.ServerVariables["ALL_RAW"].ToString()));
                        }
                    }

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Status = "404 Not Found";
                    HttpContext.Current.Response.StatusCode = 404;
                    return renderPage(newQS);
                    //Response.End();
                }
                else
                {
                    return renderHome("Config Error on 404 page: Please contact with " + GetApplication(Constansts.CFG_ADMIN_CONTACT));
                }
            }
            else
            {
                return renderHome("");
            }
        }
        #endregion
        #region renderPage
        //int rhp = 0;
        public string renderPage(string[] QS)
        {
            //rhp++;
            //if (rhp > 10)
            //{
            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.Write(string.Format("Unexpected Loop on This Page Display: Please contact with {0} <!--\n Last Article ID: {1} \n-->", GetApplication(Constansts.CFG_ADMIN_CONTACT), vars.a["article_id"].ToString()));
            //    HttpContext.Current.Response.End();
            //}

            String LIP = string.Empty;
            string computerName = GetApplication(Constansts.SERVER_COMPUTER_NAME);

            if (string.IsNullOrEmpty(computerName))
            {
                //LIP = string.Format("{0} - {1}", System.Environment.MachineName, HttpContext.Current.Request.ServerVariables["INSTANCE_ID"]);
                LIP = string.Format("{0} - {1}", System.Environment.MachineName, AppDomain.CurrentDomain.Id);

                SetApplication(Constansts.SERVER_COMPUTER_NAME, LIP);
            }
            else
            {
                LIP = computerName;
            }

            int QSLen = QS.Length;
            if (QSLen > 1)
            {
                string qsi = QS[1];

                if (qsi.IndexOf(",") != -1 || qsi.IndexOf("-") != -1)
                {
                    string[] ids = new string[] { };
                    if (qsi.IndexOf(",") != -1)
                    {
                        // old way
                        ids = qsi.Split(',');
                    }
                    else
                    {
                        // new way
                        ids = qsi.Split('-');
                    }

                    string template = string.Empty;

                    string zoneID = ids[0];
                    string articleID = ids[1];
                    string revID = "0";
                    string portletPageID = "1";
                    bool aPre = false;
                    bool zPre = false;
                    bool isSTF = false;
                    bool POSTED_FORM_DATA_EXIST = false;
                    bool CACHE_RELOAD_F5 = false;
                    string fullPage = string.Empty;

                    if (ids.Length > 2)
                        revID = ids[2];

                    if (ids.Length > 3)
                        portletPageID = ids[3];

                    if (string.IsNullOrEmpty(portletPageID))
                        portletPageID = "1";

                    if (zoneID.Equals("-1") && articleID.Equals("-1") && CmsHelper.IsNumeric(revID) && (CheckIp(CmsHelper.GetCurrentIP(), GetApplication(Constansts.CFG_PREVIEW_ALLOWED_IPS))) && IsManager())
                        aPre = true;

                    if (zoneID.Equals("-1") && articleID.Equals("-2") && CmsHelper.IsNumeric(revID) && (CheckIp(CmsHelper.GetCurrentIP(), GetApplication(Constansts.CFG_PREVIEW_ALLOWED_IPS))) && IsManager())
                        zPre = true;

                    string pre = QS[1];

                    if (pre == Constansts.ALIAS_STF && CmsHelper.IsNumeric(revID))
                        isSTF = true;

                    if ((CmsHelper.IsNumeric(zoneID) && CmsHelper.IsNumeric(articleID)) || aPre || zPre)
                    {
                        // BOF Check posted data is exist for plugin

                        foreach (string k in HttpContext.Current.Request.Form)
                        {
                            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[k]))
                            {
                                POSTED_FORM_DATA_EXIST = true;
                                break;
                            }
                        }
                        //EOF Check posted data is exist for plugin

                        string qsForCache = string.Join("/", QS);
                        string qsParams = string.Empty;

                        if (qsForCache.IndexOf('?') != -1)
                            qsParams = qsForCache.Substring(0, qsForCache.IndexOf('?'));
                        else
                        {
                            string qstr = HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
                            if (!string.IsNullOrEmpty(qstr))
                                if (qstr.StartsWith("404;http"))
                                    qsParams = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(qsParams) && qsParams.Length > 200)
                            qsParams = CmsHelper.ConvertStringToMD5(qsParams);

                        if (!string.IsNullOrEmpty(qsParams))
                            qsParams = qsParams + "_" + qsParams;

                        // EOF Check if QueryString is exist

                        string cacheKey = HttpContext.Current.Request.Url.Host + qsForCache;

                        bool cacheEnabled = IsCacheActive() && GetApplication(Constansts.CACHED_PAGES).IndexOf(string.Format(",{0}-{1},", zoneID, articleID)) != -1 && !aPre && !zPre && !isSTF && !POSTED_FORM_DATA_EXIST && HttpContext.Current.Response.Status != "404 Not Found";

                        //if (vars.pubLevel > 0 && GetServerVariables("HTTP_CACHE_CONTROL").Equals("no-cache"))
                        if (IsManager() && GetServerVariables("HTTP_CACHE_CONTROL").Equals("no-cache"))
                            CACHE_RELOAD_F5 = true;

                        //if (cacheEnabled && !string.IsNullOrEmpty(GetApplication("PAGE_" + cacheKey)) && !CACHE_RELOAD_F5)
                        //{
                        //    template = GetApplication("PAGE_" + cacheKey) + Environment.NewLine + "<!-- HTML From Cache " + LIP + " -->";
                        //    template = CmsHelper.RegexReplace(template, "(<base href=\"[^\"]+\" />)", "<base href=\"" + GetHttpPrefix() + HttpContext.Current.Request.Url.Host + "\" />");
                        //    //set last access time to second parameter
                        //    SetApplication("PAGE_UT_" + cacheKey, DateTime.Now.Year + CmsHelper.Right("0" + DateTime.Now.Month, 2) + CmsHelper.Right("0" + DateTime.Now.Date, 2) + CmsHelper.Right("0" + DateTime.Now.Hour, 2) + CmsHelper.Right("0" + DateTime.Now.Minute, 2) + CmsHelper.Right("0" + DateTime.Now.Second, 2));
                        //}
                        //else
                        //{
                        DataTable dt = null;
                        if (zPre)
                            dt = Dal.Instance.SelectZonesRevisionsDetails(Convert.ToInt32(revID));
                        if (aPre)
                            dt = Dal.Instance.SelectArticleRevisionDetails(Convert.ToInt32(revID));
                        else
                            dt = Dal.Instance.SelectArticleDetails(Convert.ToInt32(zoneID), Convert.ToInt32(articleID));


                        string names = string.Empty;
                        string[] arrNames = new string[] { };

                        string[] stf_template = new string[] { };
                        string wh = string.Empty;
                        string stf_omniture_function = string.Empty;

                        int mapped_zone_id = 0;
                        int mapped_article_id = 0;

                        if (dt.Rows.Count > 0)
                        {
                            names = getRecordsetNames(dt);

                            arrNames = names.Split(',');

                            foreach (string n in arrNames)
                            {
                                vars.setValue2Dic(n, dt.Rows[0][n]);
                            }

                            int article_type = Convert.ToInt32(vars.a["article_type"]);
                            string article_type_detail = vars.a["article_type"].ToString();

                            vars.setValue2Dic("global_article_type", article_type);


                            if (article_type == 9)
                            {
                                if (article_type_detail.IndexOf("-") == -1)
                                {
                                    return renderHome("Mapped article detail invalid");
                                }
                                else
                                {
                                    string[] arr_article_type_detail = vars.a["article_type_detail"].ToString().Split('-');

                                    mapped_zone_id = Convert.ToInt32(arr_article_type_detail[0]);
                                    mapped_article_id = Convert.ToInt32(arr_article_type_detail[1]);

                                    int global_mapped_article_id = mapped_article_id;
                                    int global_current_article_id = Convert.ToInt32(vars.a["article_id"]);

                                    vars.setValue2Dic("global_mapped_article_id", global_mapped_article_id);
                                    vars.setValue2Dic("global_current_article_id", global_current_article_id);

                                    vars.setValue2Dic("mapped_article_id", mapped_article_id);

                                    DataTable dt1 = Dal.Instance.SelectArticleDetails(mapped_zone_id, mapped_article_id);

                                    string names2 = getRecordsetNames(dt1);

                                    string[] arrNames2 = names2.Split(',');

                                    foreach (string n2 in arrNames2)
                                    {
                                        switch (n2)
                                        {
                                            #region fields
                                            case "article_1":
                                            case "article_2":
                                            case "article_3":
                                            case "article_4":
                                            case "article_5":
                                            case "custom_1":
                                            case "custom_2":
                                            case "custom_3":
                                            case "custom_4":
                                            case "custom_5":
                                            case "custom_6":
                                            case "custom_7":
                                            case "custom_8":
                                            case "custom_9":
                                            case "custom_10":
                                            case "custom_11":
                                            case "custom_12":
                                            case "custom_13":
                                            case "custom_14":
                                            case "custom_15":
                                            case "custom_16":
                                            case "custom_17":
                                            case "custom_18":
                                            case "custom_19":
                                            case "custom_20":
                                            case "summary":
                                            case "headline":
                                            case "menu_text":
                                            case "article_type":
                                            case "article_type_detail":
                                            case "flag_1":
                                            case "flag_2":
                                            case "flag_3":
                                            case "flag_4":
                                            case "flag_5":
                                            case "date_1":
                                            case "date_2":
                                            case "date_3":
                                            case "date_4":
                                            case "date_5":
                                            case "a_custom_body":
                                            case "keywords":
                                            case "meta_description":
                                            case "rating":
                                            case "rating_count":
                                                #endregion
                                                vars.setValue2Dic(n2, dt1.Rows[0][n2]);
                                                break;
                                        }
                                    }

                                    int article_type2 = Convert.ToInt32(vars.a["article_type"]);
                                    if (article_type2 == 9)
                                    {
                                        renderHome("Mapped article type wrong.");
                                    }
                                }
                            }


                            int status = Convert.ToInt32(vars.a["status"]);
                            string search_index_headline = vars.a["headline"].ToString();
                            string search_index_summary = vars.a["summary"].ToString();
                            DateTime startdate = DateTime.Parse("01.01.1900 00:00:00"); ;
                            if (vars.a["startdate"] != DBNull.Value && vars.a["startdate"] != null)
                                startdate = Convert.ToDateTime(vars.a["startdate"]);
                            DateTime enddate = DateTime.Parse("31.12.2200 23:59:00"); ;
                            if (vars.a["enddate"] != DBNull.Value && vars.a["enddate"] != null)
                                enddate = Convert.ToDateTime(vars.a["enddate"]);
                            int article_id = Convert.ToInt32(vars.a["article_id"]);
                            object publisher_id = vars.a["publisher_id"];
                            int clicks = Convert.ToInt32(vars.a["clicks"]);
                            int orderno = Convert.ToInt32(vars.a["az_order"]);
                            int navigation_display = Convert.ToInt32(vars.a["navigation_display"]);
                            int navigation_zone_id = Convert.ToInt32(vars.a["navigation_zone_id"]);
                            int flag_1 = Convert.ToInt32(vars.a["flag_1"]);
                            int flag_2 = Convert.ToInt32(vars.a["flag_2"]);
                            int flag_3 = Convert.ToInt32(vars.a["flag_3"]);
                            int flag_4 = Convert.ToInt32(vars.a["flag_4"]);
                            int flag_5 = Convert.ToInt32(vars.a["flag_5"]);
                            int cl_1 = Convert.ToInt32(vars.a["cl_1"]);
                            int cl_2 = Convert.ToInt32(vars.a["cl_2"]);
                            int cl_3 = Convert.ToInt32(vars.a["cl_3"]);
                            int cl_4 = Convert.ToInt32(vars.a["cl_4"]);
                            int cl_5 = Convert.ToInt32(vars.a["cl_5"]);
                            int zone_id = Convert.ToInt32(vars.a["zone_id"]);
                            int zone_group_id = Convert.ToInt32(vars.a["zone_group_id"]);
                            int zone_css_merge = Convert.ToInt32(vars.a["zone_css_merge"]);
                            int zone_css_id = Convert.ToInt32(vars.a["zone_css_id"]);
                            int zone_css_id_mobile = Convert.ToInt32(vars.a["zone_css_id_mobile"]);
                            int zone_css_id_print = Convert.ToInt32(vars.a["zone_css_id_print"]);
                            int zone_template_id = Convert.ToInt32(vars.a["zone_template_id"]);
                            int zone_template_id_mobile = Convert.ToInt32(vars.a["zone_template_id_mobile"]);
                            int append_1 = Convert.ToInt32(vars.a["append_1"]);
                            int append_2 = Convert.ToInt32(vars.a["append_2"]);
                            int append_3 = Convert.ToInt32(vars.a["append_3"]);
                            int append_4 = Convert.ToInt32(vars.a["append_4"]);
                            int append_5 = Convert.ToInt32(vars.a["append_5"]);
                            int zg_append_1 = Convert.ToInt32(vars.a["zg_append_1"]);
                            int zg_append_2 = Convert.ToInt32(vars.a["zg_append_2"]);
                            int zg_append_3 = Convert.ToInt32(vars.a["zg_append_3"]);
                            int zg_append_4 = Convert.ToInt32(vars.a["zg_append_4"]);
                            int zg_append_5 = Convert.ToInt32(vars.a["zg_append_5"]);
                            object zone_publisher_id = vars.a["zone_publisher_id"];
                            int site_id = Convert.ToInt32(vars.a["site_id"]);
                            int zg_css_merge = Convert.ToInt32(vars.a["zg_css_merge"]);
                            int zg_css_id = Convert.ToInt32(vars.a["zg_css_id"]);
                            int zg_css_id_mobile = Convert.ToInt32(vars.a["zg_css_id_mobile"]);
                            int zg_css_id_print = Convert.ToInt32(vars.a["zg_css_id_print"]);
                            int zg_template_id = Convert.ToInt32(vars.a["zg_template_id"]);
                            int zg_template_id_mobile = Convert.ToInt32(vars.a["zg_template_id_mobile"]);
                            object zg_publisher_id = vars.a["zg_publisher_id"];
                            int site_css_id = Convert.ToInt32(vars.a["site_css_id"]);
                            int site_css_id_mobile = Convert.ToInt32(vars.a["site_css_id_mobile"]);
                            int site_css_id_print = Convert.ToInt32(vars.a["site_css_id_print"]);
                            int site_template_id = Convert.ToInt32(vars.a["site_template_id"]);
                            int site_template_id_mobile = Convert.ToInt32(vars.a["site_template_id_mobile"]);
                            object site_publisher_id = vars.a["site_publisher_id"];
                            string zone_status = vars.a["zone_status"].ToString();
                            string custom_setting = vars.a["custom_setting"].ToString();
                            string article_1 = vars.a["article_1"].ToString();
                            string article_2 = vars.a["article_2"].ToString();
                            string article_3 = vars.a["article_3"].ToString();
                            string article_4 = vars.a["article_4"].ToString();
                            string article_5 = vars.a["article_5"].ToString();
                            string headline = vars.a["headline"].ToString();
                            int rating = Convert.ToInt32(vars.a["rating"]);
                            int ratingcount = Convert.ToInt32(vars.a["ratingcount"]);

                            if (custom_setting.IndexOf(";") == -1)
                                custom_setting = custom_setting + ";";

                            string[] arr_custom_setting = custom_setting.Split(';');

                            vars.setValue2Dic("arr_custom_setting", arr_custom_setting);

                            //if ((status == 1 && zone_status == "A" && startdate < DateTime.Now && enddate > DateTime.Now) || vars.pubLevel > 0)
                            //{
                            //if ((status == 1 && zone_status == "A" && startdate < DateTime.Now && enddate > DateTime.Now) || vars.pubLevel > 0)
                            if ((status == 1 && zone_status == "A" && startdate < DateTime.Now && enddate > DateTime.Now) || IsManager())
                            {

                                string site_omniture_code = vars.a["site_omniture_code"].ToString();
                                string zone_group_omniture_code = vars.a["zone_group_omniture_code"].ToString();
                                string zone_omniture_code = vars.a["zone_omniture_code"].ToString();
                                string article_omniture_code = vars.a["article_omniture_code"].ToString();
                                string full_omniture_code = site_omniture_code;

                                if (!string.IsNullOrEmpty(zone_group_omniture_code))
                                    full_omniture_code += Environment.NewLine + zone_group_omniture_code;

                                if (!string.IsNullOrEmpty(zone_omniture_code))
                                    full_omniture_code += Environment.NewLine + zone_omniture_code;

                                if (!string.IsNullOrEmpty(article_omniture_code))
                                    full_omniture_code += Environment.NewLine + article_omniture_code;


                                vars.setValue2Dic("full_omniture_code", full_omniture_code);

                                string custom_body = vars.a["s_custom_body"].ToString().Trim();
                                string zg_custom_body = vars.a["zg_custom_body"].ToString().Trim();
                                string zone_custom_body = vars.a["zone_custom_body"].ToString().Trim();
                                string a_custom_body = vars.a["a_custom_body"].ToString().Trim();

                                if (!string.IsNullOrEmpty(zg_custom_body))
                                    custom_body = zg_custom_body;

                                if (!string.IsNullOrEmpty(zone_custom_body))
                                    custom_body = zone_custom_body;

                                if (!string.IsNullOrEmpty(a_custom_body))
                                    custom_body = a_custom_body;

                                vars.setValue2Dic("custom_body", custom_body);

                                if (article_type > 0 && !isSTF)
                                    processArticleRedirection(string.Empty);

                                //if (!isSTF)
                                //{

                                getProperPageTemplate();

                                int template_id = Convert.ToInt32(vars.a["template_id"]);

                                template = getTemplateHTML(template_id);
                                template = checkTemplateForHeaders(template);

                                getProperPageCSS();

                                if (template.IndexOf("##HEADERS##") != -1)
                                    template = replaceHTMLHeaders(template);

                                template = processContentAreas(template);

                                template = bindEditorButtons(aPre, Convert.ToInt32(revID), template);

                                if (QS[QS.Length - 1].StartsWith("s:"))
                                {
                                    string searched = QS[QS.Length - 1].Substring(2);
                                    article_1 = CmsHelper.RegexReplace(article_1, "(" + searched + "(?![a-zA-Z0-9]*)(?![\x22\x2F\x3D\x2E\x5F\x3E]))", "<span class=\"highlight\">$1</span>", false, false, true, true);
                                }

                                template = processContent(template);

                                template = processAnalytics(template);

                                template = processSplash(template);

                                template = processBeforeHead(template);

                                if (!zPre)
                                    template = CmsHelper.processLangRelations(article_id, zone_id, template);

                                // template = processPlugins(template);

                                template = processPortlets(template);

                                //template = processTags(template, article_id);

                                template = processMenus(template);
                                template = processSitemaps(template);
                                template = processBreadCrumbs(template);
                                // template = processSTFLinks(template);
                                // template = processSessions(template);
                                // template = processCookies(template);

                                template = template.Replace("##page_title##", GetApplication(Constansts.CFG_TITLE_PREFIX) + headline + GetApplication(Constansts.CFG_TITLE_SUFFIX));
                                template = template.Replace("##today##", vars.Today);
                                template = template.Replace("##today_time##", vars.TodayTime);
                                template = template.Replace("##today_full##", vars.TodayFull);

                                if (ratingcount > 0)
                                    template = template.Replace("##rating##", string.Format("{0:n}", ((rating / ratingcount) * 20)));
                                else
                                    template = template.Replace("##rating##", "0");
                                //}
                                //else
                                //{
                                //    stf_template = getSTFHTML(revID);
                                //    template = stf_template[1];
                                //    wh = stf_template[5];
                                //    stf_omniture_function = stf_template[6];
                                //    template = processSTFForm(template, revID, article_id, zone_id, wh, stf_omniture_function);
                                //}

                                template = replaceArticleDetailsRows(dt.Rows[0], names, template, string.Empty);

                                if (mapped_article_id > 0)
                                    template = processAFiles(template, mapped_article_id);
                                else
                                    template = processAFiles(template, article_id);

                                // template = ReplaceHiddenValues(template);

                                // template = processAnchors(template);

                                //if (!isSTF)
                                //template = template.Replace("</body>", "<div id=\"plbg\"></div><div id=\"pl\"><div id=\"plc\" style=\"width:760px; height:450px;\"></div></div>" + Environment.NewLine + "</body>");

                                // Remove comment tags
                                if (GetApplication(Constansts.CFG_DEBUG_MODE) == "N")
                                    template = CmsHelper.RegexReplace(template, @"<!--(?!\[)(.|.*\n|.*\r\n)*?-->", "", false, false, true, true);

                                // Clear empty lines
                                if (GetApplication(Constansts.CFG_CLEAR_EMPTY_LINES) == "Y")
                                    template = CmsHelper.RegexReplace(template, @"^[ \t]*$\r?\n", "", false, false, true, true);

                                // Clear tabs and spaces
                                if (GetApplication(Constansts.CFG_CLEAR_TABS_AND_SPACES) == "Y")
                                    template = CmsHelper.RegexReplace(template, @"^[ \t]+|[ \t]+$", "", false, false, true, true);

                                // template = pruneBBCode(template);

                                template = processBeforeBody(template);

                                //if (cacheEnabled && (status == 1 && zone_status == "A" && startdate <= DateTime.Now && enddate > DateTime.Now))
                                //{
                                //    do_ClearOldCachedPages();
                                //    SetApplication("PAGE_" + cacheKey, template);
                                //    //  Set update time to second parameter
                                //    SetApplication("PAGE_UT_" + cacheKey, DateTime.Now.Year + CmsHelper.Right("0" + DateTime.Now.Month, 2) + CmsHelper.Right("0" + DateTime.Now.Date, 2) + CmsHelper.Right("0" + DateTime.Now.Hour, 2) + CmsHelper.Right("0" + DateTime.Now.Minute, 2) + CmsHelper.Right("0" + DateTime.Now.Second, 2));

                                //    int cachedPagesCount = 0;
                                //    if (CmsHelper.IsNumeric(GetApplication(Constansts.CACHED_PAGES_COUNT)))
                                //    {
                                //        cachedPagesCount = Convert.ToInt32(GetApplication(Constansts.CACHED_PAGES_COUNT));
                                //        HttpContext.Current.Application.Lock();
                                //        SetApplication(Constansts.CACHED_PAGES_COUNT, (cachedPagesCount + 1).ToString());
                                //        HttpContext.Current.Application.UnLock();
                                //    }
                                //    else
                                //    {
                                //        HttpContext.Current.Application.Lock();
                                //        SetApplication(Constansts.CACHED_PAGES_COUNT, "1");
                                //        HttpContext.Current.Application.UnLock();
                                //    }
                                //}
                                //template = template + Environment.NewLine + "<!-- HTML From DB " + LIP + " -->";
                            }
                            else
                            {
                                return render404(QS);
                            }
                        }
                        else
                        {
                            DataTable dt2 = Dal.Instance.SelectArticleDetailsForBreadcrumb(0, Convert.ToInt32(articleID));
                            if (dt2.Rows.Count > 0)
                            {
                                string zone_id = dt2.Rows[0][9].ToString();
                                string article_id = dt2.Rows[0][10].ToString();
                                string site_name = dt2.Rows[0][0].ToString();
                                string zone_group_name = dt2.Rows[0][1].ToString();
                                string zone_name = dt2.Rows[0][3].ToString();
                                string headline = dt2.Rows[0][5].ToString();
                                string alias = dt2.Rows[0][12].ToString();
                                HttpContext.Current.Response.Redirect(getContentLinkAlias(zone_id, article_id, site_name, zone_group_name, zone_name, headline, alias));
                            }
                            else
                            {
                                return renderHome("Article is not related to this zone or not found. " + zoneID + "/" + articleID);
                            }
                        }

                        // Remove edit buttons
                        template = processEditorLinks(template);
                        //}

                        //if (!isSTF)
                        //   Dal.Instance.UpdateArticleClicks(Convert.ToInt32(articleID));

                        //Response.Clear();
                        //Response.ContentType = "text/html";
                        // HttpContext.Current.Response.Write(template);
                        return template;
                        //Response.End(); --> for asp.net rendering
                    }
                    else
                    {
                        return renderHome("Article ID or Zone ID is not numeric." + zoneID + "/" + articleID);
                    }
                }
                else
                {
                    return renderHome("QS is not contain comma. " + qsi);
                }
            }
            else
            {
                return renderHome("Not enough parameters.");
            }
        }

        #region processEditorLinks
        private string processEditorLinks(string template)
        {
            string result = template;
            bool remove_editor_buttons = true;
            int head_start = -1;
            int head_end = -1;
            string head_text = string.Empty;


            //if (vars.pubID > 0)
            //{
            //    if (checkPermissions(vars.pubID.ToString(), Convert.ToInt32(vars.a["zone_id"]), "ZA", "EDIT_APPROVE_ARTICLE"))
            //    {
            //        remove_editor_buttons = false;
            //        result = result.Replace(">.edBut{display:none;}<", ">.edBut{display:block;}<");
            //        result = result.Replace("var cmsPath = '';", "var cmsPath = '" + System.Configuration.ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "';");
            //    }else
            //        result = result.Replace("var cmsPath = '';", "var cmsPath = '';");

            //    if (remove_editor_buttons == true || GetApplication(Constansts.CFG_REMOVE_EDITOR_LINKS).Equals("Y"))
            //    {
            //        result = CmsHelper.RegexReplace(result, "<!-- EDITOR-BUTTON-START -->.*?<!-- EDITOR-BUTTON-END -->", "", false, false, true, true);
            //    }

            //    head_start = result.ToLower().IndexOf("<head");
            //    if (head_start != -1)
            //    {
            //        head_end = result.ToLower().IndexOf("</head>");
            //        if (head_end != -1)
            //        {
            //            head_text = result.Substring(head_start, (head_end - head_start) + 7);
            //            head_text = CmsHelper.RegexReplace(head_text, "<!-- EDITOR-BUTTON-START -->.*?<!-- EDITOR-BUTTON-END -->", "", false, false, true, true);
            //            result = result.Substring(0, head_start - 1) + head_text + result.Substring(head_end + 7);
            //        }
            //    }
            //}



            return result;
        }
        #endregion
        #region rjsC
        private string rjsC(string p)
        {
            string strR = string.Empty;
            strR = "<script type=\"text/javascript\">" + Environment.NewLine +
                    p + Environment.NewLine +
                    "</script>" + Environment.NewLine;
            return strR;
        }
        #endregion

        #region processBreadCrumbs
        private string processBreadCrumbs(string template)
        {
            string result = template;
            bool isFound = gotBreadCrumb(result);
            int loopLimit = 0;
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceBreadCrumbs(result);
                isFound = gotBreadCrumb(result);
            }
            return result;
        }
        #endregion
        #region replaceBreadCrumbs
        private string replaceBreadCrumbs(string template)
        {
            string result = template;
            string breadcrumb_template = regOp("breadcrumb_get_temp", result);
            string breadcrumb_id = regOp("breadcrumb_get_id", breadcrumb_template);
            string breadcrumb_template_omniture = CmsHelper.RegexGet(result, "\x23\x23breadcrumb_([^\x23]+)\x23\x23", true);
            string breadcrumb_id_omniture = CmsHelper.RegexReplace(breadcrumb_template_omniture, "(.)*\x23\x23breadcrumb_([0-9]{1,5})_omniture##(.)*", "$2", false, false, true, false);

            int breadcrumb_id_ = 0;

            DateTime started = DateTime.Now;

            string breadcrumb_final = string.Empty;
            string breadcrumb_final_omniture = string.Empty;

            string breadcrumb_name = string.Empty;

            string include_site = string.Empty;
            string include_zone_group = string.Empty;
            string include_headline = string.Empty;
            string EXCLUDED_SITES = string.Empty;
            string EXCLUDED_ZONEGROUPS = string.Empty;
            string EXCLUDED_ZONES = string.Empty;
            string breadcrumb_seperator = string.Empty;
            string breadcrumb_ul_class = string.Empty;
            string include_submenus = string.Empty;
            string breadcrumb_main_container = string.Empty;
            string breadcrumb_main_item_container = string.Empty;
            string breadcrumb_sub_container = string.Empty;
            string breadcrumb_sub_item_container = string.Empty;
            bool bc_first_li = true;
            bool bc_last_li = true;
            string bc_sub_selected = string.Empty;

            if (string.IsNullOrEmpty(breadcrumb_id) || !CmsHelper.IsNumeric(breadcrumb_id) && string.IsNullOrEmpty(breadcrumb_id_omniture)
                && CmsHelper.IsNumeric(breadcrumb_id_omniture) &&
                !string.IsNullOrEmpty(breadcrumb_template_omniture))
            {
                breadcrumb_template = breadcrumb_template_omniture;
                breadcrumb_id = breadcrumb_id_omniture;
            }

            if (CmsHelper.IsNumeric(breadcrumb_id))
            {
                breadcrumb_id_ = Convert.ToInt32(breadcrumb_id);

                if (breadcrumb_id_ > 0)
                {
                    string zone_id = vars.a["zone_id"].ToString();
                    string article_id = vars.a["article_id"].ToString();
                    string site_id = vars.a["site_id"].ToString();
                    string site_name = vars.a["site_name"].ToString();
                    string zone_group_id = vars.a["zone_group_id"].ToString();
                    string zone_group_name = vars.a["zone_group_name"].ToString();
                    string default_site_link = string.Empty;
                    string default_zone_gruop_link = string.Empty;

                    string b1 = GetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id);
                    string b2 = GetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id + "_OMNITURE");

                    if (!string.IsNullOrEmpty(b1) && GetApplication(Constansts.CFG_BREADCRUMB_CACHE_ACTIVE).Equals("Y") && !string.IsNullOrEmpty(b2))
                    {
                        breadcrumb_final = b1;
                        breadcrumb_final_omniture = b2;
                    }
                    else
                    {
                        DataTable dt = Dal.Instance.SelectBreadCrumb(breadcrumb_id_);
                        if (dt.Rows.Count > 0)
                        {
                            breadcrumb_name = dt.Rows[0]["breadcrumb_name"].ToString();
                            vars.breadcrumb_deep_level = Convert.ToInt32(dt.Rows[0]["deep_level"]);
                            include_site = dt.Rows[0]["include_site"].ToString();
                            include_zone_group = dt.Rows[0]["include_zonegroup"].ToString();
                            include_headline = dt.Rows[0]["include_headline"].ToString();
                            EXCLUDED_SITES = "," + dt.Rows[0]["excluded_sites"].ToString() + ",";
                            EXCLUDED_ZONEGROUPS = "," + dt.Rows[0]["excluded_zonegroups"].ToString() + ",";
                            EXCLUDED_ZONES = "," + dt.Rows[0]["excluded_zones"].ToString() + ",";
                            breadcrumb_seperator = dt.Rows[0]["seperator"].ToString();
                            breadcrumb_ul_class = dt.Rows[0]["ul_class"].ToString();
                            include_submenus = dt.Rows[0]["include_submenus"].ToString();
                            breadcrumb_main_container = dt.Rows[0]["breadcrumb_main_container"].ToString();
                            breadcrumb_main_item_container = dt.Rows[0]["breadcrumb_main_item_container"].ToString();
                            breadcrumb_sub_container = dt.Rows[0]["breadcrumb_sub_container"].ToString();
                            breadcrumb_sub_item_container = dt.Rows[0]["breadcrumb_sub_item_container"].ToString();

                            RedimBreadCrumbItems();
                            getBreadCrumb(0, Convert.ToInt32(zone_id));

                            breadcrumb_final = Environment.NewLine + "<" + breadcrumb_main_container;
                            breadcrumb_final_omniture = "";
                            if (!string.IsNullOrEmpty(breadcrumb_ul_class))
                            {
                                breadcrumb_final = breadcrumb_final + " class=\"" + breadcrumb_ul_class + "\">" + Environment.NewLine;
                            }
                            else
                                breadcrumb_final = breadcrumb_final + ">";

                            bc_first_li = true;
                            bc_last_li = true;

                            if (include_headline.Equals("Y"))
                                bc_last_li = false;

                            // Include Site Name
                            if (include_site.Equals("Y") && !EXCLUDED_SITES.Contains("," + site_id + ","))
                            {
                                DataTable dt1 = Dal.Instance.SelectDefaultArticleForBreadCrumb("S", Convert.ToInt32(site_id));
                                if (dt1.Rows.Count > 0)
                                {
                                    default_site_link = getStructureDefaultLink(default_site_link, dt1.Rows[0][0].ToString());

                                    if (!string.IsNullOrEmpty(default_site_link))
                                    {
                                        breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_start\" id=\"bc_main_id_" + site_id + "\"><a href=\"" + default_site_link + "\"><span>" + site_name + "</span></a></" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                        breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(site_name) + ">";
                                        bc_first_li = false;

                                        if (!string.IsNullOrEmpty(breadcrumb_seperator.Trim()))
                                        {
                                            breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " >" + breadcrumb_seperator + "</" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                        }
                                    }
                                }
                            }

                            // Include Zone Group Name
                            if (include_zone_group.Equals("Y") && !EXCLUDED_ZONEGROUPS.Contains("," + zone_group_id + ","))
                            {
                                DataTable dt2 = Dal.Instance.SelectDefaultArticleForBreadCrumb("ZG", Convert.ToInt32(zone_group_id));
                                if (dt2.Rows.Count > 0)
                                {
                                    default_site_link = getStructureDefaultLink(default_site_link, dt2.Rows[0][0].ToString());

                                    if (!string.IsNullOrEmpty(default_site_link))
                                    {
                                        if (include_site.Equals("Y"))
                                        {
                                            if (zone_group_name != site_name)
                                                breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + "><a href=\"" + default_site_link + "\"><span>" + zone_group_name + "</span></a></" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_start\" id=\"bc_main_id_" + zone_group_id + "\"><a href=\"" + default_zone_gruop_link + "\"><span>" + zone_group_name + "</span></a></" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                            breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(site_name) + ">";
                                            bc_first_li = false;
                                        }

                                        if (!string.IsNullOrEmpty(breadcrumb_seperator.Trim()))
                                        {
                                            breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " >" + breadcrumb_seperator + "</" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                        }
                                    }
                                }
                            }

                            // Create breadcrumb
                            string temp_bc_headline = string.Empty;
                            int ii = 0;

                            string bc_li_class = string.Empty;

                            for (int i = 20; i >= 1; i--)
                            {
                                ii = i - 1;
                                if (ii < 0) ii = 0;

                                if (!string.IsNullOrEmpty(vars.BreadCrumbItems[i, 0]))
                                {
                                    if (
                                        !EXCLUDED_SITES.Contains("," + vars.BreadCrumbItems[i, 3] + ",")
                                        &&
                                        !EXCLUDED_ZONEGROUPS.Contains("," + vars.BreadCrumbItems[i, 2] + ",")
                                        &&
                                        !EXCLUDED_ZONES.Contains("," + vars.BreadCrumbItems[i, 1] + ",")
                                        &&
                                       Convert.ToInt32(vars.BreadCrumbItems[i, 6]) != 0
                                        )
                                    {

                                        if (bc_first_li)
                                        {
                                            bc_li_class = " class=\"li_start\"";
                                            bc_first_li = false;
                                        }
                                        else
                                        {
                                            bc_li_class = string.Empty;
                                        }

                                        if (bc_last_li)
                                        {
                                            if (i == 1)
                                            {
                                                bc_li_class = " class=\"li_end\"";
                                            }
                                            else if (i != 20 && i != 1)
                                            {
                                                if (string.IsNullOrEmpty(vars.BreadCrumbItems[ii, 0]))
                                                    bc_li_class = " class=\"li_end\"";
                                            }
                                        }

                                        if (temp_bc_headline != vars.BreadCrumbItems[i, 5])
                                        {
                                            if (vars.BreadCrumbItems[i, 4].Contains("href=\""))
                                                breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + "" + bc_li_class + ">" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<a " + vars.BreadCrumbItems[i, 4] + "><span>" + vars.BreadCrumbItems[i, 5] + "</span></a>" + Environment.NewLine;
                                            else
                                                breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + "" + bc_li_class + ">" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<a href=\"" + vars.BreadCrumbItems[i, 4] + "\"><span>" + vars.BreadCrumbItems[i, 5] + "</span></a>" + Environment.NewLine;

                                            temp_bc_headline = vars.BreadCrumbItems[i, 5];
                                        }

                                        breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(vars.BreadCrumbItems[i, 5]) + ">";

                                        if (include_submenus.Equals("Y") && !string.IsNullOrEmpty(vars.BreadCrumbItems[ii, 0]) && ii != i)
                                        {
                                            DataTable dt3;
                                            if (ii == 0)
                                            {
                                                dt3 = Dal.Instance.SelectArticlesByZoneForBreadcrumb(Convert.ToInt32(zone_id));
                                            }
                                            else
                                            {
                                                dt3 = Dal.Instance.SelectArticlesByZoneForBreadcrumb(Convert.ToInt32(vars.BreadCrumbItems[ii, 1]));
                                            }

                                            if (dt3.Rows.Count > 0)
                                            {
                                                breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + "<div><" + breadcrumb_sub_container + ">" + Environment.NewLine;

                                                string bc_sub_menu_text = string.Empty;
                                                string bc_sub_headline = string.Empty;
                                                int bc_sub_article_id = 0;

                                                string bc_sub_headline_temp = string.Empty;

                                                foreach (DataRow dr3 in dt3.Rows)
                                                {
                                                    bc_sub_menu_text = dr3[6].ToString();
                                                    bc_sub_headline = dr3[2].ToString();
                                                    bc_sub_article_id = Convert.ToInt32(dr3[0]);

                                                    if ((ii > 0 && bc_sub_article_id == Convert.ToInt32(vars.BreadCrumbItems[ii, 0]))
                                                        ||
                                                          (ii == 0 && bc_sub_article_id == Convert.ToInt32(article_id))
                                                        )
                                                    {
                                                        bc_sub_selected = " class=\"active\"";
                                                    }
                                                    else
                                                        bc_sub_selected = string.Empty;

                                                    bc_sub_headline_temp = bc_sub_headline;
                                                    if (!string.IsNullOrEmpty(bc_sub_menu_text.Trim()))
                                                        bc_sub_headline_temp = bc_sub_menu_text;

                                                    if (zone_group_name != bc_sub_headline_temp)
                                                    {
                                                        breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + breadcrumb_sub_item_container + "><a href=\"" + getContentLinkAlias(dr3[1].ToString(), bc_sub_article_id.ToString(), dr3[5].ToString(), dr3[4].ToString(), dr3[3].ToString(), bc_sub_headline, dr3[7].ToString()) + "\"><span" + bc_sub_selected + ">";

                                                        if (!string.IsNullOrEmpty(bc_sub_menu_text.Trim()))
                                                        {
                                                            breadcrumb_final = breadcrumb_final + bc_sub_menu_text;
                                                        }
                                                        else
                                                        {
                                                            breadcrumb_final = breadcrumb_final + bc_sub_headline;
                                                        }

                                                        breadcrumb_final = breadcrumb_final + "</span></a></" + breadcrumb_sub_item_container + ">" + Environment.NewLine;
                                                    }

                                                    if (!string.IsNullOrEmpty(breadcrumb_seperator.Trim()))
                                                    {
                                                        breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + breadcrumb_sub_item_container + ">" + breadcrumb_seperator + "</" + breadcrumb_sub_item_container + ">" + Environment.NewLine;
                                                    }
                                                }

                                                breadcrumb_final = trimBreadCrumbSeperator(breadcrumb_final, breadcrumb_seperator);
                                                breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + "</" + breadcrumb_sub_container + "></div>" + Environment.NewLine;
                                            }
                                        }

                                        breadcrumb_final = breadcrumb_final + Environment.NewLine + "</" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                        if (!string.IsNullOrEmpty(breadcrumb_seperator.Trim()))
                                        {
                                            breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + breadcrumb_sub_item_container + ">" + breadcrumb_seperator + "</" + breadcrumb_sub_item_container + ">" + Environment.NewLine;
                                        }
                                    }
                                }
                            }

                            // Include Current Article Headline
                            if (include_headline.Equals("Y"))
                            {

                                // bu değerler bir step once calısan getBreadCrumbData fonksiyonunda dolmus olabilir.
                                string menu_text = vars.a["menu_text"].ToString();
                                string headline = vars.a["headline"].ToString();

                                if (!string.IsNullOrEmpty(menu_text))
                                {
                                    breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_end breadcrumb_headline\"><span>" + menu_text + "</span></" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                    breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(menu_text) + ">";
                                }
                                else
                                {
                                    breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_end breadcrumb_headline\"><span>" + headline + "</span></" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                    breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(headline) + ">";
                                }

                                if (!string.IsNullOrEmpty(breadcrumb_seperator.Trim()))
                                {
                                    breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + breadcrumb_sub_item_container + ">" + breadcrumb_seperator + "</" + breadcrumb_sub_item_container + ">" + Environment.NewLine;
                                }
                            }

                            breadcrumb_final = trimBreadCrumbSeperator(breadcrumb_final, breadcrumb_seperator);

                            if (CmsHelper.Right(breadcrumb_final_omniture, 1).Equals(">"))
                            {
                                breadcrumb_final_omniture = breadcrumb_final_omniture.Substring(breadcrumb_final_omniture.Length - 2);
                            }

                            breadcrumb_final = breadcrumb_final + " </" + breadcrumb_main_container + ">" + Environment.NewLine;

                            DateTime ended = DateTime.Now;
                            TimeSpan ts = ended - started;

                            breadcrumb_final = breadcrumb_final + Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";
                            SetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id, breadcrumb_final);
                            SetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id + "_OMNITURE", breadcrumb_final_omniture);
                        }
                    }
                }
            }

            if (CmsHelper.IsNumeric(breadcrumb_id_omniture))
                result = result.Replace(breadcrumb_template_omniture, breadcrumb_final_omniture);

            if (!string.IsNullOrEmpty(breadcrumb_template))
                result = result.Replace(breadcrumb_template, breadcrumb_final);

            return result;
        }
        #endregion
        #region trimBreadCrumbSeperator
        private string trimBreadCrumbSeperator(string breadcrumb_final, string breadcrumb_seperator)
        {
            if (CmsHelper.Right(breadcrumb_final, 11 + breadcrumb_seperator.Length).Equals("<li>" + breadcrumb_seperator + "</li>" + Environment.NewLine))
            {
                return breadcrumb_final.Substring(breadcrumb_final.Length - (11 + breadcrumb_seperator.Length));
            }
            else
                return breadcrumb_final;
        }
        #endregion
        #region RedimBreadCrumbItems
        private void RedimBreadCrumbItems()
        {
            for (int i = 0; i < vars.BreadCrumbItems.Length / 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    vars.BreadCrumbItems[i, j] = "0";
                }
            }
        }
        #endregion
        #region getBreadCrumb
        public void getBreadCrumb(int articleID, int zoneID)
        {
            // Bu değerler Global degıller. o yuzden GlobalVars'a atanmadılar. replaceBreadCrumbs methodu global degıskenleri kullanır.
            string site_name = string.Empty;
            string zone_group_name = string.Empty;
            string zone_group_name_display = string.Empty;
            string zone_name = string.Empty;
            string zone_name_display = string.Empty;
            string headline = string.Empty;
            string menu_text = string.Empty;
            string site_id = string.Empty;
            string zone_group_id = string.Empty;
            string zone_id = string.Empty;
            string article_id = string.Empty;
            string navigation_zone_id = string.Empty;
            string az_alias = string.Empty;
            string zone_default_article = string.Empty;
            string article_type = string.Empty;
            string article_type_detail = string.Empty;
            string navigation_display = string.Empty;

            DataTable dt = Dal.Instance.SelectArticleDetailsForBreadcrumb(zoneID, articleID);
            if (dt.Rows.Count > 0)
            {
                site_name = dt.Rows[0][0].ToString();
                zone_group_name = dt.Rows[0][1].ToString();
                zone_group_name_display = dt.Rows[0][2].ToString();
                zone_name = dt.Rows[0][3].ToString();
                zone_name_display = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(dt.Rows[0][4].ToString()));
                headline = dt.Rows[0][5].ToString();
                menu_text = dt.Rows[0][6].ToString();
                site_id = dt.Rows[0][7].ToString();
                zone_group_id = dt.Rows[0][8].ToString();
                zone_id = dt.Rows[0][9].ToString();
                article_id = dt.Rows[0][10].ToString();
                navigation_zone_id = dt.Rows[0][11].ToString();
                az_alias = dt.Rows[0][12].ToString();
                zone_default_article = dt.Rows[0][13].ToString();
                article_type = dt.Rows[0][14].ToString();
                article_type_detail = dt.Rows[0][15].ToString();
                navigation_display = dt.Rows[0][16].ToString();
            }

            if (vars.bc_lastStep > 0)
                navigation_zone_id = zoneID.ToString(); //ilk çalıştığında navigation zone dan, sonrasında normal zone dan yukarı doğru çıkıyoruz

            // BreadCrumbItems(30,7) 'Second index items| 0: article_id, 1: zone_id, 2: zone_group_id, 3: site_id, 4: article link, 5: menu text (or headline)

            vars.BreadCrumbItems[vars.bc_lastStep, 0] = article_id;
            vars.BreadCrumbItems[vars.bc_lastStep, 1] = zone_id;
            vars.BreadCrumbItems[vars.bc_lastStep, 2] = zone_group_id;
            vars.BreadCrumbItems[vars.bc_lastStep, 3] = site_id;
            vars.BreadCrumbItems[vars.bc_lastStep, 6] = navigation_display;

            if (string.IsNullOrEmpty(navigation_zone_id))
                navigation_zone_id = "0";

            if (article_type == "6")
            {
                vars.BreadCrumbItems[vars.bc_lastStep, 4] = article_type_detail;
            }
            else
            {
                vars.BreadCrumbItems[vars.bc_lastStep, 4] = getContentLinkAlias(zone_id, article_id, site_name, zone_group_name, zone_name, headline, az_alias);
            }

            if (!string.IsNullOrEmpty(menu_text.Trim()))
            {
                vars.BreadCrumbItems[vars.bc_lastStep, 5] = menu_text;
            }
            else
            {
                vars.BreadCrumbItems[vars.bc_lastStep, 5] = headline;
            }

            if (!string.IsNullOrEmpty(zone_default_article))
            {
                vars.BreadCrumbItems[vars.bc_lastStep, 4] = getStructureDefaultLink(vars.BreadCrumbItems[vars.bc_lastStep, 4], zone_default_article);
            }

            if (Convert.ToInt32(navigation_zone_id) > 0 && vars.bc_lastStep < 20 && vars.bc_lastStep <= vars.breadcrumb_deep_level)
            {
                vars.bc_lastStep++;
                getBreadCrumb(0, Convert.ToInt32(navigation_zone_id));
            }
        }
        #endregion
        #region getStructureDefaultLink
        private string getStructureDefaultLink(string p, string zone_default_article)
        {
            string[] arr_zone_default_article;
            string result = string.Empty;

            if (!string.IsNullOrEmpty(zone_default_article))
            {
                if (zone_default_article.Contains("-"))
                {
                    arr_zone_default_article = zone_default_article.Split('-');

                    if (arr_zone_default_article.Length == 2)
                    {
                        if (CmsHelper.IsNumeric(arr_zone_default_article[0]) && CmsHelper.IsNumeric(arr_zone_default_article[1]))
                        {
                            DataTable dt = Dal.Instance.SelectArticleDetailsForBreadcrumb(Convert.ToInt32(arr_zone_default_article[0]), Convert.ToInt32(arr_zone_default_article[1]));
                            if (dt.Rows.Count > 0)
                            {
                                result = getContentLinkAlias(
                                    dt.Rows[9].ToString(),
                                    dt.Rows[10].ToString(),
                                    dt.Rows[0].ToString(),
                                    dt.Rows[1].ToString(),
                                    dt.Rows[3].ToString(),
                                    dt.Rows[5].ToString(),
                                    dt.Rows[12].ToString()); // Set default article of zone link
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion
        #region processCookies
        private string processCookies(string template)
        {
            string result = template;
            bool isFound = gotCookie(result);
            int loopLimit = 0;
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceCookie(result);
                isFound = gotCookie(result);
            }
            return result;
        }
        #endregion
        #region replaceCookie
        private string replaceCookie(string template)
        {
            string result = template;
            string cookie_tmp = string.Empty;
            cookie_tmp = CmsHelper.RegexGet(result, "\x23\x23cookie_(.)*\x23\x23", true);
            cookie_tmp = cookie_tmp.Replace("##cookie_", "");
            cookie_tmp = cookie_tmp.Replace("##", "");
            result = result.Replace("##cookie_" + cookie_tmp + "##", GetCookieValue(Constansts.CMS_COOKIE_NAME, cookie_tmp));
            return result;
        }
        #endregion
        #region processSessions
        private string processSessions(string template)
        {
            string result = template;
            bool isFound = gotSession(result);
            int loopLimit = 0;
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceSession(result);
                isFound = gotSession(result);
            }
            return result;
        }
        #endregion
        #region replaceSession
        private string replaceSession(string template)
        {
            string result = template;
            string session_tmp = string.Empty;
            session_tmp = CmsHelper.RegexGet(result, "\x23\x23session_(.)*\x23\x23", true);
            session_tmp = session_tmp.Replace("##session_", "");
            session_tmp = session_tmp.Replace("##", "");
            result = result.Replace("##session_" + session_tmp + "##", GetSession(session_tmp));
            return result;
        }
        #endregion
        #region processSTFLinks
        private string processSTFLinks(string template)
        {
            string result = template;
            bool isFound = gotSTF(result);
            int loopLimit = 0;
            int article_id = Convert.ToInt32(vars.a["article_id"]);
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceSTF(result, article_id, zone_id);
                isFound = gotSTF(result);
            }
            return result;
        }
        #endregion
        #region replaceSTF
        private string replaceSTF(string template, int article_id, int zone_id)
        {
            string result = template;
            string stf_temp = CmsHelper.RegexGet(result, "\x23\x23stf_link_(.)*", true);
            string stf_id = CmsHelper.RegexReplace(stf_temp, "\x23\x23stf_link_([0-9]{1,5})##(.)*", "$1", false, false, true, false);
            if (CmsHelper.IsNumeric(stf_id))
            {
                result = result.Replace("##stf_link_" + stf_id + "##", "javascript:void(0);\" onclick=\"return openSTF(" + stf_id + "," + zone_id.ToString() + "," + article_id.ToString() + "),false;");
            }
            return template;
        }
        #endregion
        #region processSitemaps
        private string processSitemaps(string template)
        {
            string result = template;
            bool isFound = gotSitemap(result);
            int loopLimit = 0;
            int article_id = Convert.ToInt32(vars.a["article_id"]);
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceMenus(result, article_id, zone_id, "sitemap");
                isFound = gotSitemap(result);
            }
            return result;
        }
        #endregion
        #region processMenus
        private string processMenus(string template)
        {
            string result = template;
            bool isFound = gotMenu(result);
            int loopLimit = 0;
            int article_id = Convert.ToInt32(vars.a["article_id"]);
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceMenus(result, article_id, zone_id, "menu");
                isFound = gotMenu(result);
            }
            return result;
        }
        #endregion
        #region replaceMenus
        private string replaceMenus(string template, int article_id, int zone_id, string inType)
        {
            string result = template;
            string menu_temp = string.Empty;
            string menu_id = string.Empty;
            string menu_exclude = string.Empty;
            string menu_include = string.Empty;
            string menu_container = string.Empty;
            string menucontainertagid = string.Empty;
            string menusingleitem = string.Empty;
            string menuonclickfunction = string.Empty;
            string menuselectedcls = string.Empty;
            string menunotselectedcls = string.Empty;
            string menu_data = string.Empty;
            string[] menu_datas = new string[] { };
            string menu_style = string.Empty;
            string menu_class = string.Empty;
            string menu_out = string.Empty;

            DateTime started = DateTime.Now;

            if (inType == "menu")
            {
                menu_temp = regOp("menu_get_temp", result);
                menu_id = regOp("menu_get_id", menu_temp);
                if (menu_temp.Contains("exclude="))
                    menu_exclude = regOp("menu_get_exclude", menu_temp);
                if (menu_temp.Contains("include="))
                    menu_include = regOp("portlet_get_include", menu_temp);

                if (menu_temp.Contains("lang="))
                    menu_container = regOp("portlet_get_container", menu_temp);
                if (menu_temp.Contains("menucontainertagid="))
                    menucontainertagid = CmsHelper.RegexReplace(menu_temp, @"(.)*menucontainertagid=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", false, false, true, false);
                if (menu_temp.Contains("menusingleitem="))
                    menusingleitem = CmsHelper.RegexReplace(menu_temp, @"(.)*menusingleitem=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", false, false, true, false);
                if (menu_temp.Contains("menuonclickfunction="))
                    menuonclickfunction = CmsHelper.RegexReplace(menu_temp, @"(.)*menuonclickfunction=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", false, false, true, false);
                if (menu_temp.Contains("menuselectedcls="))
                    menuselectedcls = CmsHelper.RegexReplace(menu_temp, @"(.)*menuselectedcls=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", false, false, true, false);
                if (menu_temp.Contains("menunotselectedcls="))
                    menunotselectedcls = CmsHelper.RegexReplace(menu_temp, @"(.)*menunotselectedcls=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", false, false, true, false);

                menu_exclude = menu_exclude.Replace(" ", "");
                if (!CmsHelper.IsNumeric(menu_exclude.Replace(",", "")))
                    menu_exclude = string.Empty;

                menu_include = menu_include.Replace(" ", "");
                if (!CmsHelper.IsNumeric(menu_include.Replace(",", "")))
                    menu_include = string.Empty;

                if (string.IsNullOrEmpty(menu_container))
                    menu_container = "div";
            }
            else
            {
                menu_temp = regOp("sitemap_get_temp", result);
                menu_id = regOp("sitemap_get_id", menu_temp);

                if (menu_temp.Contains("lang="))
                    menu_container = regOp("portlet_get_container", menu_temp);

                if (string.IsNullOrEmpty(menu_container))
                    menu_container = "div";
            }

            menu_data = CmsHelper.RegexReplace(menu_temp, @"(.)* id=\x22([0-9a-zA-Z\._-]*)\x22(.)*", "$2", false, false, true, false);
            menu_datas = menu_data.Split('_');

            if (menu_temp.Contains("class="))
                menu_class = regOp("menu_get_class", menu_temp);
            else
                menu_class = string.Empty;

            if (menu_temp.Contains("style="))
                menu_style = regOp("menu_get_style", menu_temp);
            else
                menu_style = string.Empty;

            if (menu_datas.Length > 2)
            {
                string d1 = menu_datas[0];
                string d2 = menu_datas[1];
                string d3 = menu_datas[2];
                string d4 = menu_datas[3];

                if (CmsHelper.IsNumeric(d1) && CmsHelper.IsNumeric(d2) && CmsHelper.IsNumeric(d3))
                {
                    menu_out = getMenuData(menu_id, d1, d2, d3, d4, zone_id, inType, menu_exclude, menu_include, menusingleitem, menuonclickfunction);

                    if (inType == "menu")
                    {
                        menu_out = openMenuData(menu_out, d1, article_id, zone_id, menuselectedcls, menunotselectedcls);
                    }

                    DateTime ended = DateTime.Now;
                    TimeSpan ts = ended - started;
                    menu_out = menu_out + Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms menu_temp: " + menu_temp.Replace(">", "").Replace("<", "").Replace("##", "") + " -->";

                    if (!string.IsNullOrEmpty(menu_class))
                        menu_class = " class=\"" + menu_class + "\"";

                    if (!string.IsNullOrEmpty(menucontainertagid))
                        menucontainertagid = " id=\"" + menucontainertagid + "\"";

                    if (menu_container.ToUpper().Equals("NA"))
                    {
                        result = result.Replace(menu_temp, Environment.NewLine + menu_out + Environment.NewLine);
                    }
                    else
                    {
                        result = result.Replace(menu_temp, "<" + menu_container + menu_class + menucontainertagid + ">" + Environment.NewLine + menu_out + Environment.NewLine + "</" + menu_container + ">" + Environment.NewLine);
                    }
                }
            }
            return result;
        }
        #endregion
        #region openMenuData
        private string openMenuData(string menu_out, string d1, int article_id, int zone_id, string menuselectedcls, string menunotselectedcls)
        {
            string result = menu_out;

            if (Convert.ToInt32(d1) == 0)
                d1 = zone_id.ToString();

            if (string.IsNullOrEmpty(menuselectedcls))
                menuselectedcls = "active";

            if (GetApplication(Constansts.CFG_ENABLE_MENU_MAINCONTENTLI).Equals("Y"))
            {
                result = result.Replace("<li id=\"article_" + article_id + "\">", "<li id=\"article_" + article_id + "\" class=\"" + menuselectedcls + "\" maincontentli=\"Y\">");
                result = result.Replace("<li id=\"article_" + article_id + "\" class=\"lie\">", "<li id=\"article_" + article_id + "\" class=\"" + menuselectedcls + " lie\" maincontentli=\"Y\">");
            }
            else
            {
                result = result.Replace("<li id=\"article_" + article_id + "\">", "<li id=\"article_" + article_id + "\" class=\"" + menuselectedcls + "\">");
                result = result.Replace("<li id=\"article_" + article_id + "\" class=\"lie\">", "<li id=\"article_" + article_id + "\" class=\"" + menuselectedcls + " lie\">");
            }

            if (!string.IsNullOrEmpty(menunotselectedcls))
            {
                result = CmsHelper.RegexReplace(result, ".*<li (id=\"article_[0-9]*\") class=\"lie\">", "<li $1 class=\"lie " + menunotselectedcls + "\">", false, false, true, true);
                result = CmsHelper.RegexReplace(result, ".*<li (id=\"article_[0-9]*\")>", "<li $1 class=\"" + menunotselectedcls + "\">", false, false, true, true);
            }

            if (Convert.ToInt32(d1) != zone_id && zone_id != 0)
                result = openTopMenuData(result, article_id, menuselectedcls);

            return result;
        }
        #endregion
        #region openTopMenuData
        private string openTopMenuData(string menu_out, int article_id, string menuselectedcls)
        {
            string result = menu_out;

            string naid = "0";

            DataTable dt = Dal.Instance.MBuilderSelectUpperLevelAzidChain(article_id);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    naid = dr[0].ToString();

                    result = result.Replace("<li id=\"article_" + naid + "\">", "<li id=\"article_" + naid + "\" class=\"" + menuselectedcls + "\">");
                    result = result.Replace("<li id=\"article_" + naid + "\" class=\"lie\">", "<li id=\"article_" + naid + "\" class=\"" + menuselectedcls + " lie\">");

                }
            }
            return result;
        }
        #endregion
        #region getMenuData
        private string getMenuData(string menu_id, string d1, string d2, string d3, string d4, int zone_id, string inType, string menu_exclude, string menu_include, string menusingleitem, string menuonclickfunction)
        {
            string result = string.Empty;
            string menu_cache = string.Empty;

            if (Convert.ToInt32(d1) == 0)
                d1 = zone_id.ToString();

            menu_cache = GetApplication("MENU_" + menu_id + "_" + d1 + "_" + d2 + "_" + d3 + "_" + d4);

            if (string.IsNullOrEmpty(menu_cache) || !IsCacheActive() || inType == "sitemap")
            {
                if (Convert.ToInt32(d1) < 0)
                {
                    result = getMenuSubRows(menu_id, d1, d2, d3, d4, inType, menu_exclude, menu_include, menusingleitem, menuonclickfunction, 1);
                }
                else
                {
                    result = getMenuSubRows(menu_id, d1, d2, d3, d4, inType, menu_exclude, "", menusingleitem, menuonclickfunction, 1);
                }
            }
            else
            {
                result = menu_cache;
            }

            return result;
        }

        #endregion
        #region getMenuSubRows
        private string getMenuSubRows(string menu_id, string inZID, string d2, string inOrder, string inPos, string inMT, string inExcluder, string inIncluder, string menusingleitem, string menuonclickfunction, int p)
        {
            string result = string.Empty;

            int inDepth = Convert.ToInt32(d2);
            int inCurrentDepth = Convert.ToInt32(p);
            string linespacer = " ".PadLeft(inCurrentDepth * 4);
            int maxA = 0;
            string azid = string.Empty;
            string azid2 = string.Empty;

            string porder = getOrderColumn(Convert.ToInt32(inOrder));

            if ((inPos.Equals("v") && inCurrentDepth <= Constansts.CONST_MAX_MENU_DEPTH && inCurrentDepth <= inDepth)
                ||
                (inPos.Equals("h") && inCurrentDepth <= 2 && inCurrentDepth <= inDepth)
                ||
                (inMT.Equals("sitemap") && inCurrentDepth <= inDepth)
                )
            {

                if (!string.IsNullOrEmpty(inIncluder.Trim()))
                {
                    string[] inIncluders = inIncluder.Split(',');
                    foreach (string zaid in inIncluders)
                    {
                        string[] azarr = zaid.Split('-');
                        if (azarr.Length == 2)
                        {
                            if (CmsHelper.IsNumeric(azarr[0]) && CmsHelper.IsNumeric(azarr[1]))
                            {
                                maxA++;
                                if (maxA < 21)
                                {
                                    azid += azid + "(zone_id = " + azarr[0] + " and article_id = " + azarr[1] + ")";
                                    azid2 += azarr[0] + ",";
                                }
                            }
                        }
                    }

                    azid = azid.Replace(")(", ") or (");
                    if (CmsHelper.Right(azid2, 1) == ",")
                        azid2 = azid2.Substring(azid2.Length - 2);
                }

                string strSQL = string.Empty;

                if (!string.IsNullOrEmpty(azid))
                {
                    strSQL = "select * from dbo.vArticlesZones with (nolock) where (" + azid + ") and status = 1 and zone_status = 'A' and navigation_display > 0 and navigation_zone_id not in (" + azid2 + ") and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) ";
                }
                else
                {
                    strSQL = "select * from dbo.vArticlesZones with (nolock) where zone_id = " + inZID + " and status = 1 and zone_status = 'A' and navigation_display > 0 and navigation_zone_id <> " + inZID + " and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) ";
                }

                if (!string.IsNullOrEmpty(inExcluder))
                {
                    strSQL = strSQL + " and article_id not in (" + inExcluder + ") ";
                }

                strSQL = strSQL + " order by " + porder;

                int licount = 1;
                DataTable dt = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                int limax = dt.Rows.Count + 1;

                string liclass = string.Empty;
                int zone_id = 0;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        licount += 1;
                        int article_id = Convert.ToInt32(dr[2]);
                        int article_type = Convert.ToInt32(dr[21]);
                        string article_type_detail = dr[22].ToString();
                        string headline = dr[5].ToString();
                        string menu_text = dr[58].ToString();
                        int navigation_display = Convert.ToInt32(dr[59]);
                        int navigation_zone_id = Convert.ToInt32(dr[60]);
                        string site_name = dr[8].ToString();
                        string zone_group_name = dr[7].ToString();
                        zone_id = Convert.ToInt32(dr[3]);
                        string zone_name = dr[6].ToString();
                        string az_alias = dr[76].ToString();

                        if (inMT.Equals("menu") || (inMT.Equals("sitemap") && inPos.Equals("1") || (inMT.Equals("sitemap") && inPos.Equals("0") && navigation_display > 1)))
                        {
                            if (string.IsNullOrEmpty(menu_text.Trim()))
                                menu_text = headline;

                            string article_link = article_link = getContentLinkAlias(zone_id.ToString(), article_id.ToString(), site_name, zone_group_name, zone_name, headline, az_alias);
                            if (article_type == 1 && (article_type_detail.StartsWith("http://") || article_type_detail.StartsWith("https://")))
                            {
                                article_link = article_type_detail;
                                if (article_link.Contains(" "))
                                    article_link = article_link.Replace(" ", "\" target=\"\"");
                            }

                            article_link = "href=\"" + article_link + "\"";

                            if (article_type == 6)
                                article_link = article_type_detail;

                            if (navigation_display == 2 && inMT.Equals("menu"))
                            {
                                if (!menuonclickfunction.Equals("N"))
                                    article_link = "href=\"javascript:void(0);\"";
                                else
                                    article_link = "href=\"javascript:void(0);\"";
                            }

                            // Put different class to last <li> for closing
                            if (limax == licount)
                                liclass = " class=\"lie\"";
                            else
                                liclass = "";

                            if (inMT.Equals("sitemap"))
                            {
                                if ((navigation_display == 2 || navigation_display == 3) || navigation_zone_id > 0)
                                {
                                    if (string.IsNullOrEmpty(liclass))
                                        liclass = " class=\"zone\"";
                                    else
                                        liclass = liclass.Replace("lie", "zone lie");
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(liclass))
                                        liclass = " class=\"article\"";
                                    else
                                        liclass = liclass.Replace("lie", "article lie");
                                }
                            }

                            string menu_sub_items = string.Empty;

                            if ((navigation_display == 2 || navigation_display == 3) && navigation_zone_id > 0 && (inCurrentDepth + 1) <= inDepth)
                            {
                                if (menusingleitem.Equals("Y"))
                                {
                                    menu_sub_items += linespacer + "<!-- SubZone: " + navigation_zone_id + " -->" + Environment.NewLine + getMenuSubRows(menu_id, navigation_zone_id.ToString(), inDepth.ToString(), inOrder.ToString(), inPos, inMT, inExcluder, "", menusingleitem, menuonclickfunction, (inCurrentDepth + 1)) + Environment.NewLine + linespacer + "<!-- EOF SubZone: " + navigation_zone_id + " -->" + Environment.NewLine;

                                    if (subStrCount(menu_sub_items, "<li ") > 1)
                                    {
                                        result = result + linespacer + "<li id=\"article_" + article_id + "\"" + liclass + "><a title=\"" + headline.Replace("\"", "'") + "\" " + article_link + "><span>" + menu_text + "</span></a>" + Environment.NewLine;
                                        result = result + Environment.NewLine + menu_sub_items;
                                    }
                                    else
                                    {
                                        article_link = "href=\"" + CmsHelper.RegexReplace(CmsHelper.RegexGet(menu_sub_items, @"(.)*href=\x22(.*)\x22(.)*", true), "(.)*href=\x22(.*)\x22(.)*", "$2", false, false, true, true) + "\"";
                                        result = result + linespacer + "<li id=\"article_" + article_id + "\"" + liclass + "><a title=\"" + headline.Replace("\"", "'") + "\" " + article_link + "><span>" + menu_text + "</span></a>" + Environment.NewLine;
                                    }
                                }
                                else
                                {
                                    result = result + linespacer + "<li id=\"article_" + article_id + "\"" + liclass + "><a title=\"" + headline.Replace("\"", "'") + "\" " + article_link + "><span>" + menu_text + "</span></a>" + Environment.NewLine;
                                    result = result + Environment.NewLine + linespacer + "<!-- SubZone: " + navigation_zone_id + " -->" + Environment.NewLine + getMenuSubRows(menu_id, navigation_zone_id.ToString(), inDepth.ToString(), inOrder, inPos, inMT, inExcluder, "", menusingleitem, menuonclickfunction, (inCurrentDepth + 1)) + Environment.NewLine + linespacer + "<!-- EOF SubZone: " + navigation_zone_id + " -->" + Environment.NewLine;
                                }
                            }
                            else
                            {
                                result = result + linespacer + "<li id=\"article_" + article_id + "\"" + liclass + "><a title=\"" + headline.Replace("\"", "'") + "\" " + article_link + "><span>" + menu_text + "</span></a>" + Environment.NewLine;
                            }

                            result = result + linespacer + "</li>" + Environment.NewLine;
                        }
                    }

                    result = linespacer + "<ul id=\"zone_" + zone_id + "\">" + Environment.NewLine + result + Environment.NewLine + linespacer + "</ul>";
                }

            }

            return result;
        }

        #endregion
        #region processPortlets
        private string processPortlets(string template)
        {
            string result = template;
            bool isFound = gotPortlet(result);
            int loopLimit = 0;
            int article_id = Convert.ToInt32(vars.a["article_id"]);
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);

            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replacePortlets(result, article_id, zone_id);
                isFound = gotPortlet(result);
            }
            return result;
        }
        #endregion
        #region replacePortlets
        private string replacePortlets(string template, int article_id, int zone_id)
        {
            string result = template;
            DateTime started = DateTime.Now;
            bool optioned = false;

            string portlet_temp = regOp("portlet_get_temp", result);
            //option portlet
            if (string.IsNullOrEmpty(portlet_temp))
            {
                portlet_temp = regOp("portlet_option_get_temp", result);
                optioned = true;
            }

            string portlet_id = regOp("portlet_get_id", portlet_temp);
            string portlet_data = regOp("portlet_get_data", portlet_temp);

            string portlet_exself = string.Empty;
            string portlet_container = string.Empty;
            string portlet_include = string.Empty;
            string portlet_exclude = string.Empty;
            string portlet_header = string.Empty;
            string pager_count = string.Empty;
            string pager_position = string.Empty;
            string portlet_class = string.Empty;
            string portlet_style = string.Empty;
            string portlet_out = string.Empty;
            string pager_html = string.Empty;
            string[] pc_temp = new string[] { };
            string listed_zone_name = string.Empty;

            int pager_count_ = 0;
            int pager_position_ = 0;

            if (portlet_temp.Contains("hspace="))
                portlet_exself = regOp("portlet_get_exself", portlet_temp);
            if (portlet_temp.Contains("lang="))
                portlet_container = regOp("portlet_get_container", portlet_temp);
            if (portlet_temp.Contains("include="))
                portlet_include = regOp("portlet_get_include", portlet_temp);
            if (portlet_temp.Contains("exclude="))
                portlet_exclude = regOp("portlet_get_exclude", portlet_temp);
            if (portlet_temp.Contains("pheader="))
                portlet_header = regOp("portlet_get_header", portlet_temp);
            if (portlet_temp.Contains("pagercount="))
                pager_count = regOp("portlet_get_pager_count", portlet_temp);
            if (portlet_temp.Contains("pagerlocation="))
                pager_position = regOp("portlet_get_pager_position", portlet_temp);


            portlet_include = portlet_include.Replace(" ", "");
            portlet_exclude = portlet_exclude.Replace(" ", "");

            if (!CmsHelper.IsNumeric(portlet_include.Replace("-", "").Replace(",", "")))
            {
                portlet_include = "";
            }

            if (!CmsHelper.IsNumeric(portlet_exclude.Replace(",", "")))
            {
                portlet_exclude = "";
            }

            if (CmsHelper.IsNumeric(pager_count))
            {
                pager_count_ = Convert.ToInt32(pager_count);
            }

            if (CmsHelper.IsNumeric(pager_position))
            {
                pager_position_ = Convert.ToInt32(pager_position);
            }

            // Limit to correct values
            if (pager_position_ != 1 && pager_position_ != 2)
                pager_position_ = 0;

            // Disables Pager Viewing
            if (pager_count_ == 1)
                pager_position_ = 0;

            if (string.IsNullOrEmpty(portlet_container))
                portlet_container = "div";

            string[] portlet_datas = portlet_data.Split('_');

            if (portlet_temp.Contains("class="))
            {
                portlet_class = regOp("portlet_get_class", portlet_temp);
            }

            if (portlet_temp.Contains("style="))
            {
                portlet_style = regOp("portlet_get_style", portlet_temp);
            }

            if (CmsHelper.IsNumeric(portlet_id) && portlet_datas.Length > 1)
            {
                if (CmsHelper.IsNumeric(portlet_datas[0]) && CmsHelper.IsNumeric(portlet_datas[1]) && CmsHelper.IsNumeric(portlet_datas[1]))
                {

                    portlet_out = getPortletData(Convert.ToInt32(portlet_id), Convert.ToInt32(portlet_datas[0]), Convert.ToInt32(portlet_datas[1]), Convert.ToInt32(portlet_datas[2]), article_id, portlet_class, optioned, portlet_exself, portlet_container, portlet_include, portlet_exclude, portlet_header, Convert.ToInt32(pager_count), zone_id);

                    if (pager_count_ > 0)
                        pager_html = getPortletPagerHTML(portlet_temp, vars.pPageCurrent, vars.pPageCount, vars.page_start, vars.page_end, portlet_class);

                    pc_temp = portlet_class.Split(';');

                    DateTime ended = DateTime.Now;
                    TimeSpan ts = ended - started;

                    if (optioned)
                    {
                        // option value.. no DIV
                        portlet_out = "<!-- Processed in " + ts.TotalMilliseconds + "ms -->" + portlet_out.Replace("##listed_zone_name##", listed_zone_name);
                        result = result.Replace(portlet_temp, portlet_out + Environment.NewLine); // Final
                    }
                    else
                    {
                        if (pc_temp.Length > 0)// Got multi class
                        {
                            // Apply Portlet Header
                            if (!string.IsNullOrEmpty(portlet_header) && !string.IsNullOrEmpty(portlet_out))
                                portlet_out = "<div class=\"" + pc_temp[0] + "_header\">" + portlet_header + "</div>" + Environment.NewLine + portlet_out;

                            // Apply Pager
                            portlet_out = applyPortletPager(portlet_out, pager_html, pager_position);

                            // Adding header and footer (Murat HOŞVER)

                            portlet_out = getPortletHTML(Convert.ToInt32(portlet_id), "header") + portlet_out + getPortletHTML(Convert.ToInt32(portlet_id), "footer");

                            // End

                            portlet_out = portlet_out.Replace("##listed_zone_name##", listed_zone_name);

                            ended = DateTime.Now;
                            ts = ended - started;

                            portlet_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                            result = result.Replace(portlet_temp, Environment.NewLine + portlet_out + Environment.NewLine); // Final
                        }
                        else
                        {
                            // Apply Portlet Header
                            if (!string.IsNullOrEmpty(portlet_header) && !string.IsNullOrEmpty(portlet_out))
                                portlet_out = "<div class=\"" + portlet_class + "_header\">" + portlet_header + "</div>" + Environment.NewLine + portlet_out;

                            if (portlet_container.Equals("NA")) //No container
                            {
                                portlet_out = applyPortletPager(portlet_out, pager_html, pager_position);

                                // Adding header and footer (Murat HOŞVER)
                                portlet_out = getPortletHTML(Convert.ToInt32(portlet_id), "header") + portlet_out + getPortletHTML(Convert.ToInt32(portlet_id), "footer");

                                // End

                                // Replace Portlet
                                portlet_out = portlet_out.Replace("##listed_zone_name##", listed_zone_name);

                                ended = DateTime.Now;
                                ts = ended - started;

                                portlet_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                                result = result.Replace(portlet_temp, Environment.NewLine + portlet_out + Environment.NewLine); // Final
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(portlet_class))
                                    portlet_class = " class=\"" + portlet_class + "\"";

                                portlet_out = "<" + portlet_container + portlet_class + ">" + Environment.NewLine + portlet_out + Environment.NewLine + "</" + portlet_container + ">" + Environment.NewLine;

                                // Apply Pager
                                portlet_out = applyPortletPager(portlet_out, pager_html, pager_position);

                                // Adding header and footer (Murat HOŞVER)
                                portlet_out = getPortletHTML(Convert.ToInt32(portlet_id), "header") + portlet_out + getPortletHTML(Convert.ToInt32(portlet_id), "footer");

                                // End

                                // Replace Portlet
                                portlet_out = portlet_out.Replace("##listed_zone_name##", listed_zone_name);

                                portlet_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                                result = result.Replace(portlet_temp, Environment.NewLine + portlet_out + Environment.NewLine); // Final
                            }
                        }
                    }
                    result = result.Replace(Constansts.PCSS_END, getPortletHTML(Convert.ToInt32(portlet_id), "css") + Environment.NewLine + Constansts.PCSS_END);
                }
            }
            return result;
        }

        #endregion
        #region getPortletHTML
        private string getPortletHTML(int portlet_id, string type)
        {
            string result = string.Empty;
            string portlet_html = string.Empty;
            string portlet_css = string.Empty;
            string portlet_header = string.Empty;
            string portlet_footer = string.Empty;
            string portlet_enable_shotcut = string.Empty;

            if (string.IsNullOrEmpty(GetApplication("PORTLET_" + portlet_id)) || !IsCacheActive())
            {
                DataTable dt = Dal.Instance.SelectPortletDetails(portlet_id);
                if (dt.Rows.Count > 0)
                {
                    portlet_html = dt.Rows[0][7].ToString();
                    portlet_css = dt.Rows[0][8].ToString();
                    portlet_header = dt.Rows[0][10].ToString();
                    portlet_footer = dt.Rows[0][11].ToString();
                    portlet_enable_shotcut = dt.Rows[0][14].ToString();

                    vars.setValue2Dic("portlet_enable_shotcut", portlet_enable_shotcut);

                    SetApplication("PORTLET_" + portlet_id, HttpUtility.HtmlDecode(portlet_html));
                    SetApplication("PORTLET_CSS_" + portlet_id, HttpUtility.HtmlDecode(portlet_css));
                    SetApplication("PORTLET_HEADER_" + portlet_id, HttpUtility.HtmlDecode(portlet_header));
                    SetApplication("PORTLET_FOOTER_" + portlet_id, HttpUtility.HtmlDecode(portlet_footer));
                    SetApplication("PORTLET_SHORTCUT_ENABLE_" + portlet_id, HttpUtility.HtmlDecode(portlet_enable_shotcut));
                }
            }
            else
            {
                portlet_html = GetApplication("PORTLET_" + portlet_id);
                portlet_css = GetApplication("PORTLET_CSS_" + portlet_id);
                portlet_header = GetApplication("PORTLET_HEADER_" + portlet_id);
                portlet_footer = GetApplication("PORTLET_FOOTER_" + portlet_id);
                portlet_enable_shotcut = GetApplication("PORTLET_SHORTCUT_ENABLE_" + portlet_id);

                //portlet_html = "<!-- Portlet Cached: " + GetApplication("LAST_UPDATE") + " -->" + Environment.NewLine + portlet_html;
                portlet_css = "<!-- Portlet CSS Cached: " + GetApplication("LAST_UPDATE") + " -->" + Environment.NewLine + portlet_css;
                //portlet_header = "<!-- Portlet HEADER Cached: " + GetApplication("LAST_UPDATE") + " -->" + Environment.NewLine + portlet_header;
                //portlet_footer = "<!-- Portlet FOOTER Cached: " + GetApplication("LAST_UPDATE") + " -->" + Environment.NewLine + portlet_footer;
            }

            switch (type)
            {
                case "html":
                    result = portlet_html;
                    break;
                case "header":
                    result = portlet_header;
                    break;
                case "footer":
                    result = portlet_footer;
                    break;
                default:
                    result = portlet_css;
                    break;
            }

            return result;
        }

        #endregion
        #region applyPortletPager
        private string applyPortletPager(string portlet_out, string pager_html, string pager_position)
        {
            string result = portlet_out;
            switch (Convert.ToInt32(pager_position))
            {
                case 0:
                    result = pager_html + Environment.NewLine + result;
                    break;
                case 1:
                    result = result + Environment.NewLine + pager_html;
                    break;
                case 2:
                    result = pager_html + Environment.NewLine + result + Environment.NewLine + pager_html;
                    break;
            }
            return result;
        }

        #endregion
        #region getPortletPagerHTML
        private string getPortletPagerHTML(string src, int pCurrent, int pCount, int pStart, int pEnd, string pClass)
        {
            string result = string.Empty;
            string seperator = string.Empty;
            string prevnext = string.Empty;
            string pagerheader = string.Empty;
            string pagerclass = string.Empty;
            string[] prevnexts = new string[] { };
            string pText = string.Empty;
            string nText = string.Empty;
            int pPage = 0;
            int nPage = 0;

            if (pCount > 0)
            {
                if (src.Contains("seperator="))
                    seperator = regOp("portlet_get_seperator", src);

                if (src.Contains("prevnext="))
                    prevnext = regOp("portlet_get_prevnext", src);

                if (src.Contains("pagerheader="))
                    pagerheader = regOp("portlet_get_pagerheader", src);

                if (src.Contains("classpager="))
                    pagerclass = regOp("portlet_get_pagerclass", src);

                seperator = HttpUtility.UrlDecode(seperator);
                prevnext = HttpUtility.UrlDecode(prevnext);
                pagerheader = HttpUtility.UrlDecode(pagerheader);

                if (prevnext.Contains("|"))
                {
                    prevnexts = prevnext.Split('|');
                    pText = prevnexts[0];
                    if (prevnexts.Length > 0)
                        nText = prevnexts[1];
                }

                if (!string.IsNullOrEmpty(pagerheader))
                    pagerheader = "<p>" + pagerheader + "</p>";

                // Build ID part of url
                string[] ids = new string[] { };
                bool gotAlias = false;
                string url = string.Empty;

                if (vars.QS[2].Contains(","))
                {
                    ids = vars.QS[2].Split(',');
                }
                else
                {
                    ids = vars.QS[2].Split('-');
                }

                if (ids.Length > 3)
                {
                    // we found that this is aliased url
                    if (ids[4] == "a")
                        gotAlias = true;
                }

                if (gotAlias == false)
                {
                    // Old classic way
                    string nids = ids[0] + "-" + ids[1] + "-"; //'Put zone & article

                    if (ids.Length > 1)
                        nids += ids[2] + "-";
                    else
                        nids += "1-"; // Put revision id

                    nids += "##page##"; // Put page tag

                    if (ids.Length > 3)
                    {
                        // Put rest of string
                        for (int x = 4; x < ids.Length; x++)
                        {
                            nids += "-" + ids[x];
                        }
                    }

                    // Build url
                    url = "/" + vars.QS[1] + "/" + nids + "/";
                    if (ids.Length > 3)
                    {
                        // Put rest of string
                        for (int x = 3; x < ids.Length; x++)
                        {
                            url += vars.QS[x] + "/";
                        }
                    }

                    url = url.Replace("//", "/");
                }
                else
                {
                    // New Alias way
                    url += "/" + vars.fullAlias + "/p:##page##/";
                }

                // Calculate Prev Next Pages
                pPage = pCurrent - 1;
                nPage = pCurrent + 1;

                string pagelink = string.Empty;

                result = "<div class=\"" + pagerclass + "\">" + pagerheader + "<ul>" + Environment.NewLine;

                if (!string.IsNullOrEmpty(pText))
                {
                    if (pPage < 1)
                    {
                        result += "<li class=\"prev disabled\">" + pText + seperator + "</li>" + Environment.NewLine;
                    }
                    else
                    {
                        pagelink = url.Replace("##page##", pPage.ToString());
                        result += "<li class=\"prev\"><a hidefocus href=\"" + pagelink + "\" title=\"" + pPage + "\">" + pText + "</a>" + seperator + "</li>" + Environment.NewLine;
                    }
                }

                string act = string.Empty;
                string sepe = string.Empty;

                for (int x = 1; x <= pCount; x++)
                {
                    if (x == pCurrent)
                        act = " class=\"active\"";
                    else
                        act = "";

                    if (x == pCount && nText == "")
                        sepe = "";
                    else
                        sepe = seperator;

                    pagelink = url.Replace("##page##", x.ToString());

                    result += "<li" + act + "><a hidefocus href=\"" + pagelink + "\" title=\"" + x + "\">" + x + "</a>" + sepe + "</li>" + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(nText))
                {
                    if (pPage > pCount)
                    {
                        result += "<li class=\"next disabled\">" + nText + "</li>" + Environment.NewLine;
                    }
                    else
                    {
                        pagelink = url.Replace("##page##", nPage.ToString());
                        result += "<li class=\"next\"><a hidefocus href=\"" + pagelink + "\" title=\"" + nPage + "\">" + nText + "</a></li>" + Environment.NewLine;
                    }
                }

                result += "</ul></div>";
            }

            return result;
        }

        #endregion
        #region getPortletData
        private string getPortletData(

            int pid, int zone_id, int icount, int iorder, int article_id, string portlet_class, bool optioned, string exself, string container, string pInclude, string pExclude, string pHeader, int page_count, int inZoneID

            )
        {
            string[] url_arr = new string[] { };
            string result = "";
            string cur_page = string.Empty;
            int cur_page_ = 1;
            string mapped_article_id = vars.getValue4Dic("mapped_article_id");
            string global_mapped_article_id = vars.getValue4Dic("global_mapped_article_id");
            string global_current_article_id = vars.getValue4Dic("global_current_article_id");
            string strSQL = string.Empty;
            int portlet_file_article_id = 0;
            string cur_portlet = string.Empty;
            string azid = string.Empty;
            string mapped_article = string.Empty;
            string mapped_articles = string.Empty;
            int npc = 0;
            int pc = 0;
            int maxA = 0;
            string tag_zone_ow = "";

            // Proper zone_id for selection of "current zone"   
            if (zone_id == 0)
                zone_id = inZoneID;

            if (page_count > 0)
            {
                if (vars.QS[1].IndexOf(",") != -1)
                {
                    url_arr = vars.QS[1].Split(',');
                }
                else
                {
                    url_arr = vars.QS[1].Split(',');
                }

                if (url_arr.Length > 2)
                {
                    cur_page = url_arr[3];
                    cur_page = cur_page.Replace("-", "");
                }
            }

            if (!CmsHelper.IsNumeric(cur_page))
                cur_page_ = 1;

            string[] pctemp = portlet_class.Split(';');
            bool multi_class = false;
            int exs = 0;

            if (pctemp.Length > 1)
                multi_class = true;
            else
                multi_class = false;

            if (optioned == true)
                multi_class = false;

            string return_count = icount.ToString();

            if (exself.Equals("1"))
                exs = 1;
            else
                exs = 0;

            string portlet_cache = "PORTLET_DATA_" + pid + "_" + zone_id + "_" + return_count + "_" + iorder + "_" + exs + "_" + pInclude + "_" + pExclude;

            if (multi_class == true)
                portlet_cache += portlet_class;

            if (optioned == true)
                portlet_cache += "_optioned";

            if (string.IsNullOrEmpty(GetApplication(portlet_cache)) || !IsCacheActive() || page_count > 0)
            {
                string portlet_html = getPortletHTML(pid, "html");

                if (optioned == true)
                {
                    // Replace [option] tags
                    portlet_html = portlet_html.Replace("}", ">");
                    portlet_html = portlet_html.Replace("{", ">");
                }

                if (!string.IsNullOrEmpty(portlet_html))
                {
                    string porder = getOrderColumn(iorder);

                    if (Convert.ToInt32(return_count) > 0)
                        return_count = "top " + return_count;
                    else
                        return_count = "";

                    if (zone_id == -1)
                    {
                        // Display Article Files on this portlet

                        if (!string.IsNullOrEmpty(mapped_article_id))
                            strSQL = "exec dbo.cms_asp_select_article_files " + mapped_article_id;
                        else if (!string.IsNullOrEmpty(global_mapped_article_id) && Convert.ToInt32(global_current_article_id) == article_id)
                            strSQL = "exec dbo.cms_asp_select_article_files " + global_mapped_article_id;
                        else
                            strSQL = "exec dbo.cms_asp_select_article_files " + article_id;


                        DataTable dt = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            pc = 0;

                            calculatePagings(dt.Rows.Count, page_count, cur_page_);

                            for (int x = vars.page_start; x < vars.page_end; x++)
                            {
                                pc++;

                                cur_portlet = portlet_html;

                                string file_title = dt.Rows[x][1].ToString();
                                string file_name_1 = dt.Rows[x][3].ToString();
                                string file_name_2 = dt.Rows[x][4].ToString();
                                string file_name_3 = dt.Rows[x][5].ToString();
                                string file_name_4 = dt.Rows[x][6].ToString();
                                string file_name_5 = dt.Rows[x][7].ToString();
                                string file_name_6 = dt.Rows[x][8].ToString();
                                string file_name_7 = dt.Rows[x][9].ToString();
                                string file_name_8 = dt.Rows[x][10].ToString();
                                string file_name_9 = dt.Rows[x][11].ToString();
                                string file_name_10 = dt.Rows[x][12].ToString();
                                string file_comment = dt.Rows[x][14].ToString();
                                string type_alias = dt.Rows[x][15].ToString();

                                if (!string.IsNullOrEmpty(global_mapped_article_id) && Convert.ToInt32(global_mapped_article_id) == article_id)
                                {
                                    portlet_file_article_id = Convert.ToInt32(global_mapped_article_id);
                                }
                                else
                                {
                                    portlet_file_article_id = article_id;
                                }

                                if (cur_portlet.Contains("##afiles_" + type_alias + "_"))
                                {
                                    if (!string.IsNullOrEmpty(file_name_1))
                                    {
                                        file_name_1 = "/i/content/" + portlet_file_article_id + "_" + file_name_1;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_1_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_2))
                                    {
                                        file_name_2 = "/i/content/" + portlet_file_article_id + "_" + file_name_2;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_2_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_3))
                                    {
                                        file_name_3 = "/i/content/" + portlet_file_article_id + "_" + file_name_3;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_3_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_4))
                                    {
                                        file_name_4 = "/i/content/" + portlet_file_article_id + "_" + file_name_4;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_4_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_5))
                                    {
                                        file_name_5 = "/i/content/" + portlet_file_article_id + "_" + file_name_5;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_5_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_6))
                                    {
                                        file_name_6 = "/i/content/" + portlet_file_article_id + "_" + file_name_6;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_6_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_7))
                                    {
                                        file_name_7 = "/i/content/" + portlet_file_article_id + "_" + file_name_7;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_7_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_8))
                                    {
                                        file_name_8 = "/i/content/" + portlet_file_article_id + "_" + file_name_8;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_8_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_9))
                                    {
                                        file_name_9 = "/i/content/" + portlet_file_article_id + "_" + file_name_9;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_9_exists##", "exists");
                                    }
                                    if (!string.IsNullOrEmpty(file_name_10))
                                    {
                                        file_name_10 = "/i/content/" + portlet_file_article_id + "_" + file_name_10;
                                        cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_10_exists##", "exists");
                                    }


                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_1##", file_name_1);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_2##", file_name_2);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_3##", file_name_3);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_4##", file_name_4);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_5##", file_name_5);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_6##", file_name_6);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_7##", file_name_7);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_8##", file_name_8);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_9##", file_name_9);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_10##", file_name_10);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_1_extension##", getFileExtension(file_name_1));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_2_extension##", getFileExtension(file_name_2));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_3_extension##", getFileExtension(file_name_3));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_4_extension##", getFileExtension(file_name_4));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_5_extension##", getFileExtension(file_name_5));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_6_extension##", getFileExtension(file_name_6));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_7_extension##", getFileExtension(file_name_7));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_8_extension##", getFileExtension(file_name_8));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_9_extension##", getFileExtension(file_name_9));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_10_extension##", getFileExtension(file_name_10));
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_title##", file_title);
                                    cur_portlet = cur_portlet.Replace("##afiles_" + type_alias + "_comment##", file_comment);

                                    if (multi_class)
                                    {
                                        cur_portlet = "<div class=\"" + getPortletMultiClassName(pctemp, pc, "name") + "\">" + Environment.NewLine + "<div class=\"" + pctemp[0] + "\">" + Environment.NewLine + cur_portlet + "</div>" + Environment.NewLine + "</div>" + getPortletMultiClassName(pctemp, pc, "seperator");
                                    }

                                    result = result + Environment.NewLine + cur_portlet;
                                }
                            }
                        }
                        else
                        {
                            result = string.Empty;
                        }
                    }
                    else
                    {
                        if (zone_id == -2)
                        {
                            // Display Related Articles on this portlet
                            strSQL = "select related_zone_id, related_article_id from dbo.cms_article_relation with (nolock) where article_id = " + article_id;

                            DataTable dt2 = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr2 in dt2.Rows)
                                {
                                    azid = azid + "or (article_id = " + dr2[1].ToString() + " and zone_id = " + dr2[0].ToString() + ") ";
                                }
                            }
                            else
                            {
                                azid = "";
                            }

                            if (azid.StartsWith("or "))
                            {
                                azid = azid.Substring(2);
                            }

                            if (!string.IsNullOrEmpty(azid))
                            {
                                strSQL = "select " + return_count + " * from dbo.vArticlesZones with (nolock) where ( " + azid + ") and status = 1 and zone_status = 'A' and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) order by " + porder;
                            }
                            else
                            {
                                strSQL = "";
                            }
                        }
                        else if (zone_id == -3)
                        {
                            // Display selected articles on this portlet

                            strSQL = "select * from dbo.vArticlesZones with (nolock) where status = 1 and zone_status = 'A' and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) ";
                        }
                        else if (zone_id == -5)
                        {
                            // Display custom content portlet
                            strSQL = "select";
                        }
                        else
                        {
                            //Display Articles on this zone
                            strSQL = "select " + return_count + " * from dbo.vArticlesZones with (nolock) where status = 1 and zone_status = 'A' ";

                            if (zone_id == -4)
                            {
                                // Display articles with tags, just replace zone_id with the proper value from url..

                                if (vars.QS[1].IndexOf(",") > 0)
                                    url_arr = vars.QS[1].Split(',');
                                else
                                    url_arr = vars.QS[1].Split('-');

                                if (url_arr.Length > 3)
                                {
                                    String tag_id = url_arr[4];
                                    bool tag_zg = false;

                                    if (tag_id.StartsWith("_"))
                                        tag_zg = true;
                                    else
                                        tag_zg = false;

                                    tag_id = tag_id.Replace("_", "");

                                    if (CmsHelper.IsNumeric(tag_id))
                                    {
                                        // Valid tag id found on url
                                        if (tag_zg)
                                        {
                                            //Tag Zone Group Listing
                                            strSQL = strSQL + " and zone_group_id = " + tag_id + " and zone_type_id = 2 ";
                                        }
                                        else
                                        {
                                            //Tag Zone Listing
                                            strSQL = strSQL + " and zone_id = " + tag_id + " and zone_type_id = 2 ";
                                        }

                                        // Overwrite zone_id to "0" on generated article detail links..
                                        tag_zone_ow = "0";
                                    }
                                    else
                                    {
                                        // Wrong selection.. nothing will be displayed
                                        strSQL = strSQL + " and zone_id = 0 ";
                                    }
                                }
                                else
                                {
                                    // Wrong selection.. nothing will be displayed
                                    strSQL = strSQL + " and zone_id = 0 ";
                                }
                            }
                            else
                            {
                                // Normal Zone Listing
                                strSQL = strSQL + " and zone_id = " + zone_id + " ";
                            }

                            if (icount < -1)
                            {
                                // Date limited list
                                strSQL = strSQL + " and ";

                                switch (iorder)
                                {
                                    case 0:
                                        strSQL = strSQL + " ( (startdate > getDate() - " + Math.Abs(icount) + " and enddate is null) or (startdate > getDate() - " + Math.Abs(icount) + " and enddate > getDate()) ) ";
                                        break;
                                    case 1:
                                        strSQL = strSQL + " ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) " +
                                            " and (updated > getDate() - " + Math.Abs(icount) + ") ";
                                        break;
                                    case 4:
                                    case 12:
                                        strSQL = strSQL + " ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) " +
                                            " and (date_1 is NOT NULL and date_1 > getDate() - " + Math.Abs(icount) + ") ";
                                        break;
                                    case 5:
                                    case 13:
                                        strSQL = strSQL + " ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) " +
                                            " and (date_2 is NOT NULL and date_2 > getDate() - " + Math.Abs(icount) + ") ";
                                        break;
                                    default:
                                        strSQL = strSQL + " ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) ";
                                        break;
                                }

                                // is Exclude itself?

                                if (exs == 1)
                                    pExclude += "," + article_id;

                                if (!string.IsNullOrEmpty(pExclude))
                                    strSQL = excludeArticleSQL(strSQL, pExclude);

                                strSQL = strSQL + " order by " + porder;
                            }
                            else
                            {
                                // Normal Zone List
                                strSQL = strSQL + " and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) ";
                                if (exs == 1)
                                    pExclude += "," + article_id;

                                if (!string.IsNullOrEmpty(pExclude))
                                    strSQL = excludeArticleSQL(strSQL, pExclude);

                                strSQL = strSQL + " order by " + porder;
                            }
                        }

                        if (!string.IsNullOrEmpty(strSQL))
                        {
                            if (zone_id == -3)
                            {
                                //Display Selected Articles here
                                string[] azidArr = new string[20];
                                string[] idArr = new string[20];
                                string[] detailsArr = new string[20];
                                maxA = 0;
                                azid = "";

                                string[] pIncludes = pInclude.Split(',');

                                foreach (string zaid in pIncludes)
                                {
                                    string[] azArr = zaid.Split('-');
                                    if (azArr.Length == 2)
                                    {
                                        if (CmsHelper.IsNumeric(azArr[0]) && CmsHelper.IsNumeric(azArr[1]))
                                        {
                                            maxA++;
                                            if (maxA < 21)
                                            {
                                                azid += "(zone_id = " + azArr[0] + " and article_id = " + azArr[1] + ")";
                                                azidArr[maxA] = zaid;
                                            }
                                        }
                                    }
                                }

                                azid = azid.Replace(")(", ") or (");

                                if (!string.IsNullOrEmpty(azid))
                                {
                                    strSQL = strSQL + " and (" + azid + ") ";

                                    DataTable dt3 = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                                    if (dt3.Rows.Count > 0)
                                    {
                                        pc = 0;
                                        mapped_article = "";
                                        mapped_articles = "";

                                        string names = getRecordsetNames(dt3);

                                        calculatePagings(dt3.Rows.Count, page_count, cur_page_);

                                        for (int x = vars.page_start; x < vars.page_end; x++)
                                        {
                                            pc++;

                                            mapped_article = checkMappedArticle(dt3.Rows[x], names);

                                            if (!string.IsNullOrEmpty(mapped_article))
                                            {
                                                string[] arr_mapped_article = mapped_article.Split(new string[] { "|~-~-~|" }, StringSplitOptions.None);

                                                vars.setValue2Dic("mapped_article_url_" + arr_mapped_article[0] + "_" + arr_mapped_article[1], arr_mapped_article[2]);
                                                vars.setValue2Dic("pc_" + arr_mapped_article[0] + "_" + arr_mapped_article[1], pc);

                                                if (string.IsNullOrEmpty(mapped_articles))
                                                {
                                                    mapped_articles = arr_mapped_article[0] + "-" + arr_mapped_article[1];
                                                }
                                                else
                                                {
                                                    mapped_articles += "," + arr_mapped_article[0] + "-" + arr_mapped_article[1];
                                                }

                                                cur_portlet = "##mapped_" + arr_mapped_article[0] + "-" + arr_mapped_article[1] + "##";
                                            }
                                            else
                                            {
                                                if (IsManager() && vars.getValue4Dic("portlet_enable_shotcut") == "Y")
                                                {
                                                    cur_portlet = getSubEDLink(Convert.ToInt32(dt3.Rows[x][2]), "0", false, "0", Convert.ToInt32(dt3.Rows[x][3]), Convert.ToInt32(dt3.Rows[x][53]), Convert.ToInt32(dt3.Rows[x][4])) + replaceArticleDetailsRows(dt3.Rows[x], names, portlet_html, "");
                                                }
                                                else
                                                {
                                                    cur_portlet = replaceArticleDetailsRows(dt3.Rows[x], names, portlet_html, "");
                                                }

                                                if (cur_portlet.Contains("##afiles_"))
                                                {
                                                    cur_portlet = processAFiles(cur_portlet, Convert.ToInt32(dt3.Rows[x][2]));
                                                }

                                                if (cur_portlet.Contains("##portlet_"))
                                                {
                                                    cur_portlet = processSubPortlets(cur_portlet, Convert.ToInt32(dt3.Rows[x][2]));
                                                }

                                                //cur_portlet = processTags(cur_portlet, Convert.ToInt32(dt3.Rows[x][2]));
                                            }

                                            idArr[pc] = dt3.Rows[x][3].ToString() + "-" + dt3.Rows[x][2].ToString();
                                            detailsArr[pc] = cur_portlet;

                                        }

                                        if (!string.IsNullOrEmpty(mapped_articles))
                                        {
                                            if (mapped_articles.IndexOf(",") != -1)
                                            {
                                                azid = "";
                                                pIncludes = pInclude.Split(',');
                                                foreach (string zaid in pIncludes)
                                                {
                                                    string[] azArr = azid.Split('-');
                                                    if (azArr.Length == 2)
                                                    {
                                                        if (CmsHelper.IsNumeric(azArr[0]) && CmsHelper.IsNumeric(azArr[1]))
                                                        {
                                                            azid = azid + "(zone_id = " + azArr[0] + " and article_id = " + azArr[1] + ")";
                                                        }
                                                    }
                                                }

                                                azid = azid.Replace(")(", ") or (");
                                            }
                                            else
                                            {
                                                string[] arr_mapped_article = mapped_articles.Split('-');
                                                azid = "(zone_id = " + arr_mapped_article[0] + " and article_id = " + arr_mapped_article[1] + ")";
                                            }

                                            strSQL = "select * from dbo.vArticlesZones with (nolock) where status = 1 and zone_status = 'A'  and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) AND " + azid;

                                            DataTable dt4 = DbHelper.ExecuteSQLString(strSQL).Tables[0];

                                            if (dt4.Rows.Count > 0)
                                            {
                                                foreach (DataRow dr4 in dt4.Rows)
                                                {
                                                    cur_portlet = "";

                                                    vars.setValue2Dic("mapped_article_url", vars.a["mapped_article_url_" + dr4[3].ToString() + "_" + dr4[2].ToString()]);
                                                    vars.setValue2Dic("tmp_pc", vars.a["pc_" + dr4[3].ToString() + "_" + dr4[2].ToString()]);

                                                    if (IsManager() && vars.a["portlet_enable_shotcut"].ToString() == "Y")
                                                    {
                                                        cur_portlet = getSubEDLink(Convert.ToInt32(dr4[2]), "0", false, "0", Convert.ToInt32(dr4[3]), Convert.ToInt32(dr4[53]), Convert.ToInt32(dr4[4])) + replaceArticleDetailsRows(dr4, names, portlet_html, "");
                                                    }
                                                    else
                                                    {
                                                        cur_portlet = replaceArticleDetailsRows(dr4, names, portlet_html, "");
                                                    }

                                                    vars.setValue2Dic("mapped_article_url", "");

                                                    if (cur_portlet.Contains("##afiles_"))
                                                    {
                                                        cur_portlet = processAFiles(cur_portlet, Convert.ToInt32(dr4[2]));
                                                    }

                                                    if (cur_portlet.Contains("##portlet_"))
                                                    {
                                                        cur_portlet = processSubPortlets(cur_portlet, Convert.ToInt32(dr4[2]));
                                                    }

                                                    // cur_portlet = processTags(cur_portlet, Convert.ToInt32(dr4[2]));

                                                    detailsArr[Convert.ToInt32(vars.a["tmp_pc"])] = detailsArr[Convert.ToInt32(vars.a["tmp_pc"])].Replace("##mapped_" + dr4[3].ToString() + "_" + dr4[2].ToString() + "##", cur_portlet);
                                                }
                                            }
                                        }


                                        for (int acount = 1; acount <= maxA; acount++)
                                        {
                                            cur_portlet = "";
                                            for (int dcount = 1; dcount <= pc; dcount++)
                                            {
                                                if (idArr[dcount] == azidArr[acount])
                                                {
                                                    cur_portlet = detailsArr[dcount];
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(cur_portlet))
                                            {
                                                npc++;
                                                if (multi_class)
                                                {
                                                    cur_portlet = "<div class=\"" + getPortletMultiClassName(pctemp, npc, "name") + "\">" + Environment.NewLine + "<div class=\"" + pctemp[0] + "\">" + Environment.NewLine + cur_portlet + "</div>" + Environment.NewLine + "</div>" + getPortletMultiClassName(pctemp, npc, "seperator");
                                                }

                                                result = result + Environment.NewLine + cur_portlet;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        result = string.Empty;
                                    }
                                }
                                else
                                {
                                    // Not Found
                                    result = "<!-- SELECTED ARTICLES NOT FOUND -->";
                                }
                            }
                            else if (zone_id == -5)
                            {
                                result = getPortletHTML(pid, "html");
                            }
                            else
                            {
                                DataTable dt5 = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                                if (dt5.Rows.Count > 0)
                                {
                                    string names = getRecordsetNames(dt5);

                                    calculatePagings(dt5.Rows.Count, page_count, cur_page_);

                                    for (int x = vars.page_start; x < vars.page_end; x++)
                                    {
                                        mapped_article = checkMappedArticle(dt5.Rows[x], names);

                                        if (!string.IsNullOrEmpty(mapped_article))
                                        {
                                            string[] arr_mapped_article = mapped_article.Split(new string[] { "|~-~-~|" }, StringSplitOptions.None);

                                            vars.setValue2Dic("mapped_article_url_" + arr_mapped_article[0] + "_" + arr_mapped_article[1], arr_mapped_article[2]);

                                            result = result + Environment.NewLine + "##mapped_" + arr_mapped_article[0] + "-" + arr_mapped_article[1] + "##";

                                            if (string.IsNullOrEmpty(mapped_articles))
                                            {
                                                mapped_articles = arr_mapped_article[0] + "-" + arr_mapped_article[1];
                                            }
                                            else
                                            {
                                                mapped_articles += "," + arr_mapped_article[0] + "-" + arr_mapped_article[1];
                                            }

                                            cur_portlet = "##mapped_" + arr_mapped_article[0] + "-" + arr_mapped_article[1] + "##";

                                        }
                                        else
                                        {
                                            if (IsManager() && vars.getValue4Dic("portlet_enable_shotcut") == "Y")
                                            {
                                                cur_portlet = replaceArticleDetailsRows(dt5.Rows[x], names, getSubEDLink(Convert.ToInt32(dt5.Rows[x][2]), "0", false, "0", Convert.ToInt32(dt5.Rows[x][3]), Convert.ToInt32(dt5.Rows[x][53]), Convert.ToInt32(dt5.Rows[x][4])) + portlet_html, tag_zone_ow);
                                            }
                                            else
                                            {
                                                cur_portlet = replaceArticleDetailsRows(dt5.Rows[x], names, portlet_html, tag_zone_ow);
                                            }

                                            if (cur_portlet.Contains("##afiles_"))
                                            {
                                                cur_portlet = processAFiles(cur_portlet, Convert.ToInt32(dt5.Rows[x][2]));
                                            }

                                            if (cur_portlet.Contains("##portlet_"))
                                            {
                                                cur_portlet = processSubPortlets(cur_portlet, Convert.ToInt32(dt5.Rows[x][2]));
                                            }

                                            // cur_portlet = processTags(cur_portlet, Convert.ToInt32(dt5.Rows[x][2]));

                                            if (multi_class)
                                            {
                                                cur_portlet = "<div class=\"" + getPortletMultiClassName(pctemp, pc, "name") + "\">" + Environment.NewLine + "<div class=\"" + pctemp[0] + "\">" + Environment.NewLine + cur_portlet + "</div>" + Environment.NewLine + "</div>" + getPortletMultiClassName(pctemp, pc, "seperator");
                                            }

                                            result = result + Environment.NewLine + cur_portlet;
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(mapped_articles))
                                    {
                                        if (mapped_articles.IndexOf(",") != -1)
                                        {
                                            azid = "";
                                            string[] pIncludes = pInclude.Split(',');
                                            foreach (string zaid in pIncludes)
                                            {
                                                string[] azArr = azid.Split('-');
                                                if (azArr.Length == 2)
                                                {
                                                    if (CmsHelper.IsNumeric(azArr[0]) && CmsHelper.IsNumeric(azArr[1]))
                                                    {
                                                        azid = azid + "(zone_id = " + azArr[0] + " and article_id = " + azArr[1] + ")";
                                                    }
                                                }
                                            }

                                            azid = azid.Replace(")(", ") or (");
                                        }
                                        else
                                        {
                                            string[] arr_mapped_article = mapped_articles.Split('-');
                                            azid = "(zone_id = " + arr_mapped_article[0] + " and article_id = " + arr_mapped_article[1] + ")";
                                        }

                                        strSQL = "select * from dbo.vArticlesZones with (nolock) where status = 1 and zone_status = 'A'  and ( (startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()) ) AND " + azid;

                                        DataTable dt6 = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                                        if (dt6.Rows.Count > 0)
                                        {
                                            string names6 = getRecordsetNames(dt6);

                                            foreach (DataRow dr6 in dt6.Rows)
                                            {
                                                cur_portlet = "";

                                                vars.setValue2Dic("mapped_article_url", vars.a["mapped_article_url_" + dr6[3].ToString() + "_" + dr6[2].ToString()]);

                                                if (IsManager() && vars.a["portlet_enable_shotcut"].ToString() == "Y")
                                                {
                                                    cur_portlet = replaceArticleDetailsRows(dr6, names, getSubEDLink(Convert.ToInt32(dr6[2]), "0", false, "0", Convert.ToInt32(dr6[3]), Convert.ToInt32(dr6[53]), Convert.ToInt32(dr6[4])) + portlet_html, tag_zone_ow);
                                                }
                                                else
                                                {
                                                    cur_portlet = replaceArticleDetailsRows(dr6, names, portlet_html, tag_zone_ow);
                                                }

                                                vars.setValue2Dic("mapped_article_url", "");

                                                if (cur_portlet.Contains("##afiles_"))
                                                {
                                                    cur_portlet = processAFiles(cur_portlet, Convert.ToInt32(dr6[2]));
                                                }

                                                if (cur_portlet.Contains("##portlet_"))
                                                {
                                                    cur_portlet = processSubPortlets(cur_portlet, Convert.ToInt32(dr6[2]));
                                                }

                                                // cur_portlet = processTags(cur_portlet, Convert.ToInt32(dr6[2]));

                                                if (multi_class)
                                                {
                                                    cur_portlet = "<div class=\"" + getPortletMultiClassName(pctemp, pc, "name") + "\">" + Environment.NewLine + "<div class=\"" + pctemp[0] + "\">" + Environment.NewLine + cur_portlet + "</div>" + Environment.NewLine + "</div>" + getPortletMultiClassName(pctemp, pc, "seperator");
                                                }

                                                result = result.Replace("##mapped_" + dr6[3].ToString() + "-" + dr6[2].ToString() + "##", cur_portlet);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    result = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            result = "<!-- NOT FOUND -->";
                        }
                    }
                }
                else
                {
                    result = string.Empty;
                }
            }
            else
            {
                result = GetApplication(portlet_cache);
            }
            return result;
        }

        #region processSubPortlets
        private string processSubPortlets(string template, int article_id)
        {
            string result = template;
            bool isFound = gotSubPortlet(template);
            int loopLimit = 0;
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);

            while (loopLimit < 5 && isFound == true)
            {
                loopLimit++;
                result = replacePortlets(result, article_id, zone_id);
                isFound = gotSubPortlet(result);
            }
            return result;
        }
        #endregion
        #region checkMappedArticle
        private string checkMappedArticle(DataRow dr, string names)
        {
            string result = string.Empty;
            string itName = string.Empty;

            foreach (string s in names.Split(','))
            {
                itName = s;
                object val = null;

                if (dr[itName] != DBNull.Value)
                    val = dr[itName];
                else
                    val = string.Empty;

                string strVal = string.Empty;
                if (val.GetType() == typeof(bool))
                    strVal = Convert.ToBoolean(val) == true ? "1" : "0";
                else
                    strVal = val.ToString();

                switch (itName)
                {
                    case "article_type":
                        vars.a["article_type"] = strVal;
                        break;
                    case "article_type_detail":
                        vars.a["article_type_detail"] = strVal;
                        break;
                    case "zone_id":
                        vars.a["zone_id"] = strVal;
                        break;
                    case "article_id":
                        vars.a["article_id"] = strVal;
                        break;
                    case "site_name":
                        vars.a["site_name"] = strVal;
                        vars.a["site_name_backup"] = strVal;
                        break;
                    case "zone_group_name":
                        vars.a["zone_group_name"] = strVal;
                        vars.a["zone_group_name_backup"] = strVal;
                        break;
                    case "zone_name":
                        vars.a["zone_name"] = strVal;
                        vars.a["listed_zone_name"] = strVal;
                        vars.a["zone_name_backup"] = strVal;
                        break;
                    case "zone_name_display":
                        if (string.IsNullOrEmpty(strVal)) strVal = vars.a["zone_name_backup"].ToString();
                        vars.a["zone_name_display"] = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(strVal));
                        break;
                    case "zone_group_name_display":
                        if (string.IsNullOrEmpty(strVal)) strVal = vars.a["zone_group_name_backup"].ToString();
                        vars.a["zone_group_name_display"] = strVal;
                        break;
                    case "site_name_display":
                        if (string.IsNullOrEmpty(strVal)) strVal = vars.a["site_name_backup"].ToString();
                        vars.a["site_name_display"] = strVal;
                        break;
                    case "headline":
                        vars.a["headline"] = strVal;
                        break;
                    case "publisher_id":
                        vars.a["publisher_id"] = strVal;
                        break;
                    case "az_alias":
                        vars.a["az_alias"] = strVal;
                        break;
                }
            }

            int article_type = Convert.ToInt32(vars.a["article_type"]);
            string article_type_detail = vars.a["article_type_detail"].ToString();
            if (article_type == 9)
            {
                result = article_type_detail.Substring(0, article_type_detail.IndexOf("-") - 1)
                    + "|~-~-~|" + article_type_detail.Substring(article_type_detail.IndexOf("-") + 1)
                    + "|~-~-~|" + getContentLinkAlias(vars.a["zone_id"].ToString(), vars.a["article_id"].ToString(), vars.a["site_name"].ToString(), vars.a["zone_group_name"].ToString(), vars.a["zone_name"].ToString(), vars.a["headline"].ToString(), vars.a["az_alias"].ToString());
            }

            return result;
        }
        #endregion
        #region excludeArticleSQL
        private string excludeArticleSQL(string inS, string inArticles)
        {
            string result = inS;
            string aids = inArticles.Replace(",,", ",");
            if (aids == ",")
                aids = "";
            if (aids.Length > 0)
            {
                if (aids.StartsWith(","))
                    aids = aids.Substring(1);
                if (CmsHelper.Right(aids, 1) == ",")
                    aids = aids.Substring(aids.Length - 2);
                if (aids.Length > 0)
                    result += " and article_id NOT in (" + aids + ") ";
            }
            return result;
        }
        #endregion
        #region getPortletMultiClassName
        private string getPortletMultiClassName(string[] inArr, int inNo, string inDT)
        {
            int max = 0;
            string result = string.Empty;
            string[] nArr = new string[50];

            foreach (string i in inArr)
            {
                string[] pair = i.Split(':');
                if (pair.Length > 0)
                {
                    if (CmsHelper.IsNumeric(pair[0]))
                    {
                        if (Convert.ToInt32(pair[0]) < 50)
                        {
                            if (Convert.ToInt32(pair[0]) > max)
                                max = Convert.ToInt32(pair[0]);
                            nArr[Convert.ToInt32(pair[0])] = pair[1];
                        }
                    }
                }
            }

            if (max > 0)
            {
                if (inDT.Equals("name"))
                {
                    if (inNo > max)
                    {
                        inNo = inNo - ((int)Math.Ceiling((double)inNo / max) * max);
                    }

                    result = nArr[inNo];
                }
                else
                {
                    if (inNo == max && nArr[0] != "")
                    {
                        result = Environment.NewLine + "<div class=\"" + nArr[0] + "\"></div>";
                    }
                    else
                    {
                        result = string.Empty;
                    }
                }
            }

            return result;
        }
        #endregion
        #endregion
        #region calculatePagings
        private void calculatePagings(int inArrayCount, int inPageCount, int inCurrentPage)
        {
            if (inPageCount > 0)
            {
                vars.pPageCurrent = inCurrentPage;
                vars.pRecordsCount = inArrayCount + 1;
                vars.pPageCount = (int)Math.Ceiling((double)vars.pRecordsCount / inPageCount);

                if (vars.pPageCurrent > vars.pPageCount)
                    vars.pPageCurrent = vars.pPageCount;

                if (vars.pRecordsCount > 0)
                {
                    vars.page_start = (vars.pPageCurrent - 1) - inPageCount;
                    vars.page_end = vars.page_start + inPageCount - 1;
                }
                else
                {
                    vars.page_start = 0;
                    vars.page_end = 0;
                }

                if (vars.pPageCurrent == 1)
                    vars.page_start = 0;

                if (vars.page_end > inArrayCount)
                    vars.page_end = inArrayCount;
            }
            else
            {
                vars.page_start = 0;
                vars.page_end = inArrayCount;
            }
        }
        #endregion
        #region getSubEDLink
        public string getSubEDLink(int article_id, string inType, bool inPre, string inRevID, int zone_id, int zone_group_id, int site_id)
        {
            string result = "";
            int inRevID_ = 0;

            if (!CmsHelper.IsNumeric(inRevID))
                inRevID_ = 0;
            else
                inRevID_ = Convert.ToInt32(inRevID);

            if (inPre != true)
                inRevID_ = 0;

            string eb_flags = vars.eb[Convert.ToInt32(inType)];
            //string eb_str = " id=\"editButton" + article_id + "\" onmouseover=\"showEditButtons(" + inType + ",'" + eb_flags + "'," + site_id + "," + zone_group_id + "," + zone_id + "," + article_id + "," + inRevID_ + ");\" onmouseout=\"hideEditButtons(" + inType + ");\" ";
            // result = Environment.NewLine + "<!-- EDITOR-BUTTON-START --><a href=\"javascript:void(0);\" onClick=\"return false;\" title=\"Click here to edit this article\" " + eb_str + " target=\"editor\"><span class=\"edBut\">*</span></a><!-- EDITOR-BUTTON-END -->" + Environment.NewLine;

            Trace.TraceInformation("getSubEDLink Called! site_id:{0}, zone_group_id:{1}, zone_id:{2}, article_id:{3}, IsAuthenticated:{4}, IsManager:{5}", site_id, zone_group_id, zone_id, article_id, HttpContext.Current.User.Identity.IsAuthenticated, IsManager());

            result = string.Format("<cms:EditorLink runat=\"server\" Type=\"{0}\" ArticleId=\"{1}\" RevId=\"{2}\" ZoneId=\"{3}\" ZoneGroupId=\"{4}\" SiteId=\"{5}\" Flags=\"{6}\"></cms:EditorLink>", inType, article_id, inRevID_, zone_id, zone_group_id, site_id, eb_flags);

            return result;
        }
        #endregion
        #region getPluginCode
        private string getPluginCode(int plugin_id, string plugin_param)
        {
            string plugin_code = string.Empty;
            string plugin_status = string.Empty;

            if (string.IsNullOrEmpty(GetApplication("PLUGIN_" + plugin_id + "_" + CmsHelper.c2QS(plugin_param))) || !IsCacheActive())
            {
                DataTable dt = Dal.Instance.SelectPluginCode(plugin_id);
                if (dt.Rows.Count > 0)
                {
                    plugin_code = dt.Rows[0][1].ToString();
                    plugin_status = dt.Rows[0][2].ToString();

                    if (!plugin_status.Equals("1"))
                        plugin_code = "'NA";

                    if (!IsCacheActive())
                    {
                        SetApplication("PLUGIN_" + plugin_id + "_" + CmsHelper.c2QS(plugin_param), plugin_code);

                    }
                }
            }
            else
            {
                plugin_code = GetApplication("PLUGIN_" + plugin_id + "_" + CmsHelper.c2QS(plugin_param));
            }
            return plugin_code;
        }
        #endregion
        #region processAnalytics
        private string processAnalytics(string template)
        {
            string result = template;
            string site_analytics = vars.a["site_analytics"].ToString();
            string zg_analytics = vars.a["zg_analytics"].ToString();
            string zone_analytics = vars.a["zone_analytics"].ToString();

            result = result.Replace("</head>", Environment.NewLine + site_analytics + Environment.NewLine + zg_analytics + Environment.NewLine + zone_analytics + Environment.NewLine + "</head>");

            return result;
        }

        #endregion
        #region processBeforeHead
        private string processBeforeHead(string template)
        {
            string result = template;
            string arBeforeHead = vars.a["a_before_head"].ToString();
            string zoneBeforeHead = vars.a["zone_before_head"].ToString();
            string zgBeforeHead = vars.a["zg_before_head"].ToString();
            string arDescription = "<!-- Article Before Head -->";
            string zoneDescription = "<!-- Zone Before Head -->";
            string zgDescription = "<!-- Zone Group Before Head -->";
            result = result.Replace("</head>", Environment.NewLine + zgDescription + Environment.NewLine + zgBeforeHead + Environment.NewLine + zoneDescription + Environment.NewLine + zoneBeforeHead + Environment.NewLine + arDescription + Environment.NewLine + arBeforeHead + Environment.NewLine + "</head>");

            return result;
        }
        #endregion
        #region processBeforeBody
        private string processBeforeBody(string template)
        {
            string result = template;
            string arBeforeHead = vars.a["a_before_body"].ToString();
            string zoneBeforeHead = vars.a["zone_before_body"].ToString();
            string zgBeforeHead = vars.a["zg_before_body"].ToString();
            string arDescription = "<!-- Article Before Body -->";
            string zoneDescription = "<!-- Zone Before Body -->";
            string zgDescription = "<!-- Zone Group Before Body -->";
            result = result.Replace("</body>", Environment.NewLine + zgDescription + Environment.NewLine + zgBeforeHead + Environment.NewLine + zoneDescription + Environment.NewLine + zoneBeforeHead + Environment.NewLine + arDescription + Environment.NewLine + arBeforeHead + Environment.NewLine + "</body>");

            return result;
        }
        #endregion
        #region processContent
        private string processContent(string template)
        {
            string result = template;

            int article_type = Convert.ToInt32(vars.a["article_type"]);
            string article_type_detail = vars.a["article_type_detail"].ToString();
            string article_1 = vars.a["article_1"].ToString();
            string article_2 = vars.a["article_2"].ToString();
            string article_3 = vars.a["article_3"].ToString();
            string article_4 = vars.a["article_4"].ToString();
            string article_5 = vars.a["article_5"].ToString();

            if (article_type == 0)
            {
                result = result.Replace("##article_1##", HttpUtility.HtmlDecode(article_1));
            }
            else if (article_type == 5)
            {
                result = result.Replace("##article_1##", "<iframe id=\"content_iframe\" class=\"article_iframe\" src=\"" + article_type_detail + "\"></iframe>");
            }
            result = result.Replace("##article_2##", HttpUtility.HtmlDecode(article_2));
            result = result.Replace("##article_3##", HttpUtility.HtmlDecode(article_3));
            result = result.Replace("##article_4##", HttpUtility.HtmlDecode(article_4));
            result = result.Replace("##article_5##", HttpUtility.HtmlDecode(article_5));

            return result;
        }
        #endregion
        #region processSplash
        private string processSplash(string template)
        {
            int splashId = 0;
            string splash = "", splashIDs = "", splashID = "", tmp = "";


            CmsDbContext dbContext = new CmsDbContext();
            while (template.Contains("##splash_"))
            {
                tmp = template.Substring(template.Trim().IndexOf("##splash_") + 9).Trim();
                splashID = tmp.Substring(0, tmp.IndexOf("##")).Trim();
                //splashId = Convert.ToInt32(splashID);

                //Splash getSplash = new Splash();
                //getSplash = dbContext.Splashes.Where(s => s.ID == splashId && s.Status == 1).FirstOrDefault();

                //if (getSplash != null)
                //{
                //    splash += "<script>" + Environment.NewLine;
                //    splash += "$(document).ready(function () {" + Environment.NewLine;
                //    splash += " $(\"body\").mbcSplash({" + Environment.NewLine;
                //    splash += "url: \"/p/Plugins.ashx\"," + Environment.NewLine;
                //    splash += "methodName: \"GetSplashContent\"," + Environment.NewLine;
                //    splash += string.IsNullOrEmpty(getSplash.Width) || getSplash.Width == "720" ? "" : ("width: " + getSplash.Width + "," + Environment.NewLine);
                //    splash += string.IsNullOrEmpty(getSplash.Height) || getSplash.Height == "500" ? "" : ("height: " + getSplash.Height + "," + Environment.NewLine);
                //    splash += string.IsNullOrEmpty(getSplash.OpenTime) || getSplash.OpenTime == "0" ? "" : ("openDelayTime: " + getSplash.OpenTime + "," + Environment.NewLine);
                //    splash += string.IsNullOrEmpty(getSplash.CloseTime) || getSplash.CloseTime == "0" ? "" : ("closeDelayTime: " + getSplash.CloseTime + "," + Environment.NewLine);
                //    splash += !getSplash.IsModal ? "" : "modal: true," + Environment.NewLine;
                //    splash += getSplash.CloseButton ? "" : "closeButton: false," + Environment.NewLine;
                //    splash += !getSplash.Cookie ? "" : "cookie: true," + Environment.NewLine;
                //    splash += string.IsNullOrEmpty(getSplash.CookieExpire) || getSplash.CookieExpire == "1" ? "" : ("cookieExpire: " + getSplash.CookieExpire + "," + Environment.NewLine);
                //    splash += "customSplashID: " + getSplash.CustomSplashID + "," + Environment.NewLine;
                //    splash += string.IsNullOrEmpty(getSplash.StartDate) || getSplash.StartDate == "null" ? "" : ("startDate: " + getSplash.StartDate + "," + Environment.NewLine);
                //    splash += string.IsNullOrEmpty(getSplash.EndDate) || getSplash.EndDate == "null" ? "" : ("endDate: " + getSplash.EndDate + "," + Environment.NewLine);
                //    splash += "afterOpen:" + (string.IsNullOrEmpty(getSplash.AfterOpen) ? "function () { }," + Environment.NewLine : ("function () { " + getSplash.AfterOpen + " }," + Environment.NewLine));
                //    splash += "afterClose:" + (string.IsNullOrEmpty(getSplash.AfterClose) ? "function () { }," + Environment.NewLine : ("function () { " + getSplash.AfterClose + " }," + Environment.NewLine));
                //    splash = splash.Trim().EndsWith(",") ? splash.Substring(0, splash.Trim().Length - 1) + Environment.NewLine : splash;
                //    splash += "});" + Environment.NewLine;
                //    splash += "});" + Environment.NewLine;
                //    splash += "</script>" + Environment.NewLine;
                //}

                splashIDs += splashID.Trim() + ",";

                template = template.Replace("##splash_" + splashID + "##", "");
            }

            if (!string.IsNullOrEmpty(splashIDs))
            {
                splashIDs = splashIDs.EndsWith(",") ? splashIDs.Substring(0, splashIDs.Length - 1) : splashIDs;
                splash = "<!-- Splash -->" + Environment.NewLine;
                splash += "<script>" + Environment.NewLine;
                splash += "var SplashIDs = [" + splashIDs + "];" + Environment.NewLine;
                splash += "</script>" + Environment.NewLine;

                template = template.Replace("</head>", splash + "</head>");
            }

            return template;

        }
        #endregion
        #region bindEditorButtons
        private string bindEditorButtons(bool aPre, int revID, string template)
        {
            string result = template;
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);
            int article_id = Convert.ToInt32(vars.a["article_id"]);
            if (IsManager())
            {
                //if (checkPermissions(vars.pubID.ToString(), zone_id, "ZA", "EDIT_APPROVE_ARTICLE"))
                //{
                //result = result.Replace("##headline##", getEDLink(article_id, "1", aPre, revID) + "##headline##");
                result = result.Replace("##article_1##", getEDLink(article_id, 1, aPre, revID) + "##article_1##");
                result = result.Replace("##article_2##", getEDLink(article_id, 2, aPre, revID) + "##article_2##");
                result = result.Replace("##article_3##", getEDLink(article_id, 3, aPre, revID) + "##article_3##");
                result = result.Replace("##article_4##", getEDLink(article_id, 4, aPre, revID) + "##article_4##");
                result = result.Replace("##article_5##", getEDLink(article_id, 5, aPre, revID) + "##article_5##");
                //}
            }
            return result;
        }
        #endregion
        #region getEDLink
        private string getEDLink(int article_1, int type, bool aPre, int revID)
        {
            if (aPre != true)
                revID = 0;

            string result = string.Empty;

            int site_id = Convert.ToInt32(vars.a["site_id"]);
            int zone_group_id = Convert.ToInt32(vars.a["zone_group_id"]);
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);
            int article_id = Convert.ToInt32(vars.a["article_id"]);

            string eb_flags = vars.eb[type];
            //string eb_str = " id=\"editButton" + type + "\" onmouseover=\"showEditButtons(" + type + ",'" + eb_flags + "'," + site_id + "," + zone_group_id + "," + zone_id + "," + article_id + "," + revID + ");\" onmouseout=\"hideEditButtons(" + type + ");\" ";

            //result = Environment.NewLine + "<!-- EDITOR-BUTTON-START --><a href=\"javascript:void(0);\" onClick=\"return false;\" title=\"Click here to edit this article\" " + eb_str + " target=\"editor\"><span class=\"edBut\">" + type + "</span></a><!-- EDITOR-BUTTON-END -->" + Environment.NewLine;
            Trace.TraceInformation("getEDLink Called! site_id:{0}, zone_group_id:{1}, zone_id:{2}, article_id:{3}, IsAuthenticated:{4}, IsManager:{5}", site_id, zone_group_id, zone_id, article_id, HttpContext.Current.User.Identity.IsAuthenticated, IsManager());

            result = string.Format("<cms:EditorLink runat=\"server\" Type=\"{0}\" ArticleId=\"{1}\" RevId=\"{2}\" ZoneId=\"{3}\" ZoneGroupId=\"{4}\" SiteId=\"{5}\" Flags=\"{6}\"></cms:EditorLink>", type, article_id, revID, zone_id, zone_group_id, site_id, eb_flags);

            return result;
        }
        #endregion
        #region  checkPermissions
        public bool checkPermissions(string inPubID, int inID, string inZA, string inType)
        {
            //'******** inZA parameters *****************
            //'SA = Site Access
            //'ZG = Zone Group Access
            //'ZR = Zone Revision Access
            //'AR = Article Revision Access
            //'ZA = Zone Access
            //'AA = Article Access
            //'
            //'******** inType parameters ***************
            //'VIEW_ARTICLE, CREATE_ARTICLE, EDIT_ARTICLE, DELETE_ARTICLE, APPROVE_ARTICLE, EDIT_APPROVE_ARTICLE
            //'CREATE_ZONE, EDIT_ZONE, DELETE_ZONE, APPROVE_ZONE
            //'CREATE_ZONE_GROUP, EDIT_ZONE_GROUP, DELETE_ZONE_GROUP,
            //'EDIT_SITE, CREATE_SITE, DELETE_SITE
            //'******************************************

            string[] arrInPubIDCustom;
            bool boolR = false;
            int publisher_level;
            string auth, publisher_name, rel_type, publisher_department, item_owner, item_owner_custom = string.Empty;
            if (inPubID.Contains("-"))
            {
                arrInPubIDCustom = inPubID.Split('-');

                inPubID = arrInPubIDCustom[0];
                item_owner_custom = arrInPubIDCustom[1];

            }

            if (item_owner_custom == "" || string.IsNullOrEmpty(item_owner_custom))
            {
                item_owner_custom = "0";
            }

            DataTable dt = DbHelper.ExecProc("cms_asp_select_permissions", inPubID, inID, inZA).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    auth = dt.Rows[i][0].ToString();
                    item_owner = dt.Rows[i][6].ToString();
                    publisher_department = dt.Rows[i][5].ToString();
                    publisher_level = Convert.ToInt32(dt.Rows[i][4]);
                    publisher_name = dt.Rows[i][3].ToString();
                    rel_type = dt.Rows[i][1].ToString();

                    switch (inType)
                    {
                        case "VIEW_ARTICLE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else
                            {
                                if (auth.Substring(30, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "CREATE_ARTICLE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else
                            {
                                if (auth.Substring(23, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "EDIT_ARTICLE":
                            if (inZA == "ZA")
                            {
                                item_owner = item_owner_custom;
                            }
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else
                            {
                                if ((auth.Substring(24, 1) == "1" && item_owner == inPubID) || auth.Substring(25, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "DELETE_ARTICLE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else
                            {
                                if ((auth.Substring(26, 1) == "1" && item_owner == inPubID) || auth.Substring(27, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "APPROVE_ARTICLE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else
                            {
                                if ((auth.Substring(24, 1) == "1" && item_owner == inPubID) || auth.Substring(25, 1) == "1" || (auth.Substring(28, 1) == "1" && item_owner == inPubID) || auth.Substring(29, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "EDIT_APPROVE_ARTICLE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else
                            {
                                if ((auth.Substring(24, 1) == "1" && item_owner == inPubID) || auth.Substring(25, 1) == "1" || (auth.Substring(28, 1) == "1" && item_owner == inPubID) || auth.Substring(29, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "CREATE_ZONE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (Convert.ToInt32(rel_type) > 0)
                            {
                                if (auth.Substring(12, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "EDIT_ZONE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (rel_type == "0")
                            {
                                if (auth.Substring(21, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            else if (Convert.ToInt32(rel_type) > 0)
                            {
                                if ((auth.Substring(13, 1) == "1" && item_owner == inPubID) || auth.Substring(14, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "DELETE_ZONE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (Convert.ToInt32(rel_type) > 0)
                            {
                                if ((auth.Substring(15, 1) == "1" && item_owner == inPubID) || auth.Substring(16, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "APPROVE_ZONE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (rel_type == "0")
                            {
                                if (auth.Substring(22, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;

                                }
                            }
                            else if (Convert.ToInt32(rel_type) > 0)
                            {
                                if ((auth.Substring(17, 1) == "1" && item_owner == inPubID) || auth.Substring(18, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "CREATE_ZONE_GROUP":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (Convert.ToInt32(rel_type) > 1)
                            {
                                if (auth.Substring(2, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "EDIT_ZONE_GROUP":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (rel_type == "1")
                            {
                                if (auth.Substring(11, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            else if (Convert.ToInt32(rel_type) > 1)
                            {

                                if ((auth.Substring(3, 1) == "1" && item_owner == inPubID) || auth.Substring(4, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "DELETE_ZONE_GROUP":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (Convert.ToInt32(rel_type) > 1)
                            {
                                if ((auth.Substring(5, 1) == "1" && item_owner == inPubID) || auth.Substring(6, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "EDIT_SITE":
                            if (rel_type == "10")
                            {
                                boolR = true;
                            }
                            else if (Convert.ToInt32(rel_type) > 1)
                            {
                                if (auth.Substring(1, 1) == "1")
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "CREATE_SITE":
                            if (rel_type == "10")
                            {
                                if (publisher_level == 100)
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        case "DELETE_SITE":
                            if (rel_type == "10")
                            {
                                if (publisher_level == 100)
                                {
                                    boolR = true;
                                }
                                else
                                {
                                    boolR = false;
                                }
                            }
                            else if (Convert.ToInt32(rel_type) > 1)
                            {
                                if (auth.Substring(1, 1) == "0")
                                {
                                    boolR = false;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }


            return boolR;
        }
        #endregion
        #region processContentAreas
        private string processContentAreas(string template)
        {
            bool debugOn = true;
            string sub_template = string.Empty;
            string result = template;

            string s_article_1 = vars.a["s_article_1"].ToString();
            string s_article_2 = vars.a["s_article_2"].ToString();
            string s_article_3 = vars.a["s_article_3"].ToString();
            string s_article_4 = vars.a["s_article_4"].ToString();
            string s_article_5 = vars.a["s_article_5"].ToString();

            string zg_article_1 = vars.a["zg_article_1"].ToString();
            string zg_article_2 = vars.a["zg_article_2"].ToString();
            string zg_article_3 = vars.a["zg_article_3"].ToString();
            string zg_article_4 = vars.a["zg_article_4"].ToString();
            string zg_article_5 = vars.a["zg_article_5"].ToString();

            string zone_article_1 = vars.a["zone_article_1"].ToString();
            string zone_article_2 = vars.a["zone_article_2"].ToString();
            string zone_article_3 = vars.a["zone_article_3"].ToString();
            string zone_article_4 = vars.a["zone_article_4"].ToString();
            string zone_article_5 = vars.a["zone_article_5"].ToString();

            string article_1 = vars.a["article_1"].ToString();
            string article_2 = vars.a["article_2"].ToString();
            string article_3 = vars.a["article_3"].ToString();
            string article_4 = vars.a["article_4"].ToString();
            string article_5 = vars.a["article_5"].ToString();

            if (debugOn == true)
            {
                if (!string.IsNullOrEmpty(s_article_1))
                {
                    s_article_1 = "<!-- Site Container #1 -->" + Environment.NewLine + s_article_1;
                    vars.eb[1] = vars.eb[1] + ",S";
                }

                if (!string.IsNullOrEmpty(s_article_2))
                {
                    s_article_2 = "<!-- Site Container #2 -->" + Environment.NewLine + s_article_2;
                    vars.eb[2] = vars.eb[2] + ",S";
                }

                if (!string.IsNullOrEmpty(s_article_3))
                {
                    s_article_3 = "<!-- Site Container #3 -->" + Environment.NewLine + s_article_3;
                    vars.eb[3] = vars.eb[3] + ",S";
                }

                if (!string.IsNullOrEmpty(s_article_4))
                {
                    s_article_4 = "<!-- Site Container #4 -->" + Environment.NewLine + s_article_4;
                    vars.eb[4] = vars.eb[4] + ",S";
                }


                if (!string.IsNullOrEmpty(s_article_5))
                {
                    s_article_5 = "<!-- Site Container #5 -->" + Environment.NewLine + s_article_5;
                    vars.eb[5] = vars.eb[5] + ",S";
                }

                if (!string.IsNullOrEmpty(zg_article_1))
                {
                    zg_article_1 = "<!-- ZG Container #1 -->" + Environment.NewLine + zg_article_1;
                    vars.eb[1] = vars.eb[1] + ",G";
                }

                if (!string.IsNullOrEmpty(zg_article_2))
                {
                    zg_article_2 = "<!-- ZG Container #2 -->" + Environment.NewLine + zg_article_2;
                    vars.eb[2] = vars.eb[2] + ",G";
                }

                if (!string.IsNullOrEmpty(zg_article_3))
                {
                    zg_article_3 = "<!-- ZG Container #3 -->" + Environment.NewLine + zg_article_3;
                    vars.eb[3] = vars.eb[3] + ",G";
                }

                if (!string.IsNullOrEmpty(zg_article_4))
                {
                    zg_article_4 = "<!-- ZG Container #4 -->" + Environment.NewLine + zg_article_4;
                    vars.eb[4] = vars.eb[4] + ",G";
                }

                if (!string.IsNullOrEmpty(zg_article_5))
                {
                    zg_article_5 = "<!-- ZG Container #5 -->" + Environment.NewLine + zg_article_5;
                    vars.eb[5] = vars.eb[5] + ",G";
                }

                if (!string.IsNullOrEmpty(zone_article_1))
                {
                    zone_article_1 = "<!-- Zone Container #1 -->" + Environment.NewLine + zone_article_1;
                    vars.eb[1] = vars.eb[1] + ",Z";
                }

                if (!string.IsNullOrEmpty(zone_article_2))
                {
                    zone_article_2 = "<!-- Zone Container #2 -->" + Environment.NewLine + zone_article_2;
                    vars.eb[2] = vars.eb[2] + ",Z";
                }

                if (!string.IsNullOrEmpty(zone_article_3))
                {
                    zone_article_3 = "<!-- Zone Container #3 -->" + Environment.NewLine + zone_article_3;
                    vars.eb[3] = vars.eb[3] + ",Z";
                }

                if (!string.IsNullOrEmpty(zone_article_4))
                {
                    zone_article_4 = "<!-- Zone Container #4 -->" + Environment.NewLine + zone_article_4;
                    vars.eb[4] = vars.eb[4] + ",Z";
                }

                if (!string.IsNullOrEmpty(zone_article_5))
                {
                    zone_article_5 = "<!-- Zone Container #5 -->" + Environment.NewLine + zone_article_5;
                    vars.eb[5] = vars.eb[5] + ",Z";
                }

                if (!string.IsNullOrEmpty(article_1))
                {
                    article_1 = "<!-- Article #1 -->" + Environment.NewLine + article_1;
                    vars.eb[1] = vars.eb[1] + ",A";
                }

                if (!string.IsNullOrEmpty(article_2))
                {
                    article_2 = "<!-- Article #2 -->" + Environment.NewLine + article_2;
                    vars.eb[2] = vars.eb[2] + ",A";
                }

                if (!string.IsNullOrEmpty(article_3))
                {
                    article_3 = "<!-- Article #3 -->" + Environment.NewLine + article_3;
                    vars.eb[3] = vars.eb[3] + ",A";
                }

                if (!string.IsNullOrEmpty(article_4))
                {
                    article_4 = "<!-- Article #4 -->" + Environment.NewLine + article_4;
                    vars.eb[4] = vars.eb[4] + ",A";
                }

                if (!string.IsNullOrEmpty(article_5))
                {
                    article_5 = "<!-- Article #5 -->" + Environment.NewLine + article_5;
                    vars.eb[5] = vars.eb[5] + ",A";
                }

            }

            int cl_1 = Convert.ToInt32(vars.a["cl_1"]);
            int cl_2 = Convert.ToInt32(vars.a["cl_2"]);
            int cl_3 = Convert.ToInt32(vars.a["cl_3"]);
            int cl_4 = Convert.ToInt32(vars.a["cl_4"]);
            int cl_5 = Convert.ToInt32(vars.a["cl_5"]);

            int zg_append_1 = Convert.ToInt32(vars.a["zg_append_1"]);
            int zg_append_2 = Convert.ToInt32(vars.a["zg_append_2"]);
            int zg_append_3 = Convert.ToInt32(vars.a["zg_append_3"]);
            int zg_append_4 = Convert.ToInt32(vars.a["zg_append_4"]);
            int zg_append_5 = Convert.ToInt32(vars.a["zg_append_5"]);

            int append_1 = Convert.ToInt32(vars.a["append_1"]);
            int append_2 = Convert.ToInt32(vars.a["append_2"]);
            int append_3 = Convert.ToInt32(vars.a["append_3"]);
            int append_4 = Convert.ToInt32(vars.a["append_4"]);
            int append_5 = Convert.ToInt32(vars.a["append_5"]);

            // Put Content Areas on proper location on template..
            if (result.IndexOf("##article_1##") != -1 && cl_1 < 3)
            {
                sub_template = s_article_1;
                if (cl_1 < 2)
                {
                    switch (zg_append_1)
                    {
                        case 1:
                            sub_template = sub_template + Environment.NewLine + zg_article_1;
                            break;
                        case 2:
                            sub_template = zg_article_1 + Environment.NewLine + sub_template;
                            break;
                        case 3:
                            sub_template = zg_article_1;
                            // Owerwrite, can not contain site container
                            vars.eb[1] = vars.eb[1].Replace(",S", "");
                            break;
                    }

                    if (cl_1 < 1)
                    {
                        switch (append_1)
                        {
                            case 1:
                                sub_template = sub_template + Environment.NewLine + zone_article_1;
                                break;
                            case 2:
                                sub_template = zone_article_1 + Environment.NewLine + sub_template;
                                break;
                            case 3:
                                sub_template = zone_article_1;
                                // Owerwrite, can not contain site container
                                vars.eb[1] = vars.eb[1].Replace(",S", "");
                                vars.eb[1] = vars.eb[1].Replace(",G", "");
                                break;
                        }
                    }
                    else
                    {
                        // Zone skipped, can not contain zone container
                        vars.eb[1] = vars.eb[1].Replace(",Z", "");
                    }
                }
                else
                {
                    // Zone Group skipped, can not contain zone group or zone container
                    vars.eb[1] = vars.eb[1].Replace(",G", "");
                    vars.eb[1] = vars.eb[1].Replace(",Z", "");
                }

                if (!string.IsNullOrEmpty(sub_template))
                    result = result.Replace("##article_1##", HttpUtility.HtmlDecode(sub_template));
            }
            else
            {
                // Uses template directly or no content found, can not contain site, zone group or zone container
                vars.eb[1] = vars.eb[1].Replace(",S", "");
                vars.eb[1] = vars.eb[1].Replace(",G", "");
                vars.eb[1] = vars.eb[1].Replace(",Z", "");
            }

            if (result.IndexOf("##article_2##") != -1 && cl_2 < 3)
            {
                sub_template = s_article_2;
                if (cl_2 < 2)
                {
                    switch (zg_append_2)
                    {
                        case 1:
                            sub_template = sub_template + Environment.NewLine + zg_article_2;
                            break;
                        case 2:
                            sub_template = zg_article_2 + Environment.NewLine + sub_template;
                            break;
                        case 3:
                            sub_template = zg_article_2;
                            // Owerwrite, can not contain site container
                            vars.eb[2] = vars.eb[2].Replace(",S", "");
                            break;
                    }

                    if (cl_2 < 1)
                    {
                        switch (append_2)
                        {
                            case 1:
                                sub_template = sub_template + Environment.NewLine + zone_article_2;
                                break;
                            case 2:
                                sub_template = zone_article_2 + Environment.NewLine + sub_template;
                                break;
                            case 3:
                                sub_template = zone_article_2;
                                // Owerwrite, can not contain site container
                                vars.eb[2] = vars.eb[2].Replace(",S", "");
                                vars.eb[2] = vars.eb[2].Replace(",G", "");
                                break;
                        }
                    }
                    else
                    {
                        // Zone skipped, can not contain zone container
                        vars.eb[2] = vars.eb[2].Replace(",Z", "");
                    }
                }
                else
                {
                    // Zone Group skipped, can not contain zone group or zone container
                    vars.eb[2] = vars.eb[2].Replace(",G", "");
                    vars.eb[2] = vars.eb[2].Replace(",Z", "");
                }

                if (!string.IsNullOrEmpty(sub_template))
                    result = result.Replace("##article_2##", HttpUtility.HtmlDecode(sub_template));
            }
            else
            {
                // Uses template directly or no content found, can not contain site, zone group or zone container
                vars.eb[2] = vars.eb[2].Replace(",S", "");
                vars.eb[2] = vars.eb[2].Replace(",G", "");
                vars.eb[2] = vars.eb[2].Replace(",Z", "");
            }

            if (result.IndexOf("##article_3##") != -1 && cl_3 < 3)
            {
                sub_template = s_article_3;
                if (cl_3 < 2)
                {
                    switch (zg_append_3)
                    {
                        case 1:
                            sub_template = sub_template + Environment.NewLine + zg_article_3;
                            break;
                        case 2:
                            sub_template = zg_article_3 + Environment.NewLine + sub_template;
                            break;
                        case 3:
                            sub_template = zg_article_3;
                            // Owerwrite, can not contain site container
                            vars.eb[3] = vars.eb[3].Replace(",S", "");
                            break;
                    }

                    if (cl_3 < 1)
                    {
                        switch (append_3)
                        {
                            case 1:
                                sub_template = sub_template + Environment.NewLine + zone_article_3;
                                break;
                            case 2:
                                sub_template = zone_article_3 + Environment.NewLine + sub_template;
                                break;
                            case 3:
                                sub_template = zone_article_3;
                                // Owerwrite, can not contain site container
                                vars.eb[3] = vars.eb[3].Replace(",S", "");
                                vars.eb[3] = vars.eb[3].Replace(",G", "");
                                break;
                        }
                    }
                    else
                    {
                        // Zone skipped, can not contain zone container
                        vars.eb[3] = vars.eb[3].Replace(",Z", "");
                    }
                }
                else
                {
                    // Zone Group skipped, can not contain zone group or zone container
                    vars.eb[3] = vars.eb[3].Replace(",G", "");
                    vars.eb[3] = vars.eb[3].Replace(",Z", "");
                }

                if (!string.IsNullOrEmpty(sub_template))
                    result = result.Replace("##article_3##", HttpUtility.HtmlDecode(sub_template));
            }
            else
            {
                // Uses template directly or no content found, can not contain site, zone group or zone container
                vars.eb[3] = vars.eb[3].Replace(",S", "");
                vars.eb[3] = vars.eb[3].Replace(",G", "");
                vars.eb[3] = vars.eb[3].Replace(",Z", "");
            }


            if (result.IndexOf("##article_4##") != -1 && cl_4 < 3)
            {
                sub_template = s_article_4;
                if (cl_4 < 2)
                {
                    switch (zg_append_4)
                    {
                        case 1:
                            sub_template = sub_template + Environment.NewLine + zg_article_4;
                            break;
                        case 2:
                            sub_template = zg_article_4 + Environment.NewLine + sub_template;
                            break;
                        case 3:
                            sub_template = zg_article_4;
                            // Owerwrite, can not contain site container
                            vars.eb[4] = vars.eb[4].Replace(",S", "");
                            break;
                    }

                    if (cl_4 < 1)
                    {
                        switch (append_4)
                        {
                            case 1:
                                sub_template = sub_template + Environment.NewLine + zone_article_4;
                                break;
                            case 2:
                                sub_template = zone_article_4 + Environment.NewLine + sub_template;
                                break;
                            case 3:
                                sub_template = zone_article_4;
                                // Owerwrite, can not contain site container
                                vars.eb[4] = vars.eb[4].Replace(",S", "");
                                vars.eb[4] = vars.eb[4].Replace(",G", "");
                                break;
                        }
                    }
                    else
                    {
                        // Zone skipped, can not contain zone container
                        vars.eb[4] = vars.eb[4].Replace(",Z", "");
                    }
                }
                else
                {
                    // Zone Group skipped, can not contain zone group or zone container
                    vars.eb[4] = vars.eb[4].Replace(",G", "");
                    vars.eb[4] = vars.eb[4].Replace(",Z", "");
                }

                if (!string.IsNullOrEmpty(sub_template))
                    result = result.Replace("##article_4##", HttpUtility.HtmlDecode(sub_template));
            }
            else
            {
                // Uses template directly or no content found, can not contain site, zone group or zone container
                vars.eb[4] = vars.eb[4].Replace(",S", "");
                vars.eb[4] = vars.eb[4].Replace(",G", "");
                vars.eb[4] = vars.eb[4].Replace(",Z", "");
            }


            if (result.IndexOf("##article_5##") != -1 && cl_5 < 3)
            {
                sub_template = s_article_5;
                if (cl_5 < 2)
                {
                    switch (zg_append_5)
                    {
                        case 1:
                            sub_template = sub_template + Environment.NewLine + zg_article_5;
                            break;
                        case 2:
                            sub_template = zg_article_5 + Environment.NewLine + sub_template;
                            break;
                        case 3:
                            sub_template = zg_article_5;
                            // Owerwrite, can not contain site container
                            vars.eb[5] = vars.eb[5].Replace(",S", "");
                            break;
                    }

                    if (cl_5 < 1)
                    {
                        switch (append_5)
                        {
                            case 1:
                                sub_template = sub_template + Environment.NewLine + zone_article_5;
                                break;
                            case 2:
                                sub_template = zone_article_5 + Environment.NewLine + sub_template;
                                break;
                            case 3:
                                sub_template = zone_article_5;
                                // Owerwrite, can not contain site container
                                vars.eb[5] = vars.eb[5].Replace(",S", "");
                                vars.eb[5] = vars.eb[5].Replace(",G", "");
                                break;
                        }
                    }
                    else
                    {
                        // Zone skipped, can not contain zone container
                        vars.eb[5] = vars.eb[5].Replace(",Z", "");
                    }
                }
                else
                {
                    // Zone Group skipped, can not contain zone group or zone container
                    vars.eb[5] = vars.eb[5].Replace(",G", "");
                    vars.eb[5] = vars.eb[5].Replace(",Z", "");
                }

                if (!string.IsNullOrEmpty(sub_template))
                    result = result.Replace("##article_5##", HttpUtility.HtmlDecode(sub_template));
            }
            else
            {
                // Uses template directly or no content found, can not contain site, zone group or zone container
                vars.eb[5] = vars.eb[5].Replace(",S", "");
                vars.eb[5] = vars.eb[5].Replace(",G", "");
                vars.eb[5] = vars.eb[5].Replace(",Z", "");
            }

            vars.setValue2Dic("s_article_1", s_article_1);
            vars.setValue2Dic("s_article_2", s_article_2);
            vars.setValue2Dic("s_article_3", s_article_3);
            vars.setValue2Dic("s_article_4", s_article_4);
            vars.setValue2Dic("s_article_5", s_article_5);

            vars.setValue2Dic("zg_article_1", zg_article_1);
            vars.setValue2Dic("zg_article_2", zg_article_2);
            vars.setValue2Dic("zg_article_3", zg_article_3);
            vars.setValue2Dic("zg_article_4", zg_article_4);
            vars.setValue2Dic("zg_article_5", zg_article_5);

            vars.setValue2Dic("zone_article_1", zone_article_1);
            vars.setValue2Dic("zone_article_2", zone_article_2);
            vars.setValue2Dic("zone_article_3", zone_article_3);
            vars.setValue2Dic("zone_article_4", zone_article_4);
            vars.setValue2Dic("zone_article_5", zone_article_5);

            vars.setValue2Dic("article_1", article_1);
            vars.setValue2Dic("article_2", article_2);
            vars.setValue2Dic("article_3", article_3);
            vars.setValue2Dic("article_4", article_4);
            vars.setValue2Dic("article_5", article_5);

            return result;
        }

        #endregion
        #region replaceHTMLHeaders
        private string replaceHTMLHeaders(string template)
        {
            string js_line = string.Empty;
            string def_css = string.Empty;
            string[] rel_type = new string[7];
            string full_keywords = string.Empty;
            string full_title = string.Empty;
            string full_charset = string.Empty;
            string favicon = string.Empty;
            string rss_line = string.Empty;
            string basehref = string.Empty;
            string headers = string.Empty;
            string result = template;
            string css_line = string.Empty;
            string css_fix = string.Empty;

            if (!GetApplication(Constansts.CFG_NO_DEFAULT_META).Equals("Y"))
            {

                //js_line = Environment.NewLine + "<script type=\"text/javascript\">var cmsPath = '';</script>" + Environment.NewLine + "<script type=\"text/javascript\" src=\"/h/functions.js\"></script>" + Environment.NewLine;
                js_line = Environment.NewLine + "<script type=\"text/javascript\" src=\"/h/functions.js\"></script><script type=\"text/javascript\" src=\"/h/ckeditor/ckeditor.js\"></script>" + Environment.NewLine;
                def_css = "<link rel=\"stylesheet\" type=\"text/css\" href=\"/h/main.css\" />";
            }

            js_line += writeJSConstants();

            if (!GetApplication(Constansts.CFG_NO_DEFAULT_META).Equals("Y"))
            {
                int css_id_1 = Convert.ToInt32(vars.a["css_id_1"]);
                int css_id_2 = Convert.ToInt32(vars.a["css_id_2"]);
                int css_id_3 = Convert.ToInt32(vars.a["css_id_3"]);

                int css_print_1 = Convert.ToInt32(vars.a["css_print_1"]);
                int css_print_2 = Convert.ToInt32(vars.a["css_print_2"]);
                int css_print_3 = Convert.ToInt32(vars.a["css_print_3"]);

                if (css_id_1 > 0)
                {
                    // Silly way to read proper parameters..
                    DataTable dt = Dal.Instance.SelectCSSRelType(css_id_1, css_id_2, css_id_3, css_print_1, css_print_2, css_print_3);

                    int rel_id = 0;
                    string rel_types = string.Empty;

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            rel_id = Convert.ToInt32(dr[0]);
                            rel_types = dr[1].ToString();

                            if (rel_id == css_id_1) rel_type[1] = rel_types;
                            if (rel_id == css_id_2) rel_type[2] = rel_types;
                            if (rel_id == css_id_3) rel_type[3] = rel_types;
                            if (rel_id == css_print_1) rel_type[4] = rel_types;
                            if (rel_id == css_print_2) rel_type[5] = rel_types;
                            if (rel_id == css_print_3) rel_type[6] = rel_types;
                        }
                    }
                }

                if (css_id_1 > 0)
                {
                    css_line += "<link " + rel_type[1] + " href=\"/" + Constansts.DIR_CSS + "/" + css_id_1 + ".css\" media=\"screen,projection\" />" + Environment.NewLine;
                    if (!vars.IsMobile)
                    {
                        css_fix = getCSS(css_id_1, "fix");

                        if (!string.IsNullOrEmpty(css_fix))
                            css_line += css_fix + Environment.NewLine;
                    }
                }

                if (!vars.IsMobile && css_print_1 > 0)
                {
                    css_line += "<link " + rel_type[4] + " href=\"/" + Constansts.DIR_CSS + "/" + css_print_1 + ".css\" media=\"print\" />" + Environment.NewLine;
                }

                if (css_id_2 > 0)
                {
                    css_line += "<link " + rel_type[2] + " href=\"/" + Constansts.DIR_CSS + "/" + css_id_2 + ".css\" media=\"screen,projection\" />" + Environment.NewLine;
                    if (!vars.IsMobile)
                    {
                        css_fix = getCSS(css_id_2, "fix");

                        if (!string.IsNullOrEmpty(css_fix))
                            css_line += css_fix + Environment.NewLine;
                    }
                }

                if (!vars.IsMobile && css_print_2 > 0)
                {
                    css_line += "<link " + rel_type[5] + " href=\"/" + Constansts.DIR_CSS + "/" + css_print_2 + ".css\" media=\"print\" />" + Environment.NewLine;
                }


                if (css_id_3 > 0)
                {
                    css_line += "<link " + rel_type[3] + " href=\"/" + Constansts.DIR_CSS + "/" + css_id_3 + ".css\" media=\"screen,projection\" />" + Environment.NewLine;
                    if (!vars.IsMobile)
                    {
                        css_fix = getCSS(css_id_3, "fix");

                        if (!string.IsNullOrEmpty(css_fix))
                            css_line += css_fix + Environment.NewLine;
                    }
                }

                if (!vars.IsMobile && css_print_3 > 0)
                {
                    css_line += "<link " + rel_type[6] + " href=\"/" + Constansts.DIR_CSS + "/" + css_print_3 + ".css\" media=\"print\" />" + Environment.NewLine;
                }

                // Bookmark for portlet css

                // css_line += Environment.NewLine + Constansts.PCSS_BEGIN + Environment.NewLine + Constansts.PCSS_END + Environment.NewLine + "<style type=\"text/css\">.edBut{display:none;}</style>" + Environment.NewLine;
                css_line += Environment.NewLine + Constansts.PCSS_BEGIN + Environment.NewLine + Constansts.PCSS_END + Environment.NewLine;
            }

            // KEYWORDS
            string site_keywords = vars.a["site_keywords"].ToString();
            string zone_group_keywords = vars.a["zone_group_keywords"].ToString();
            string zone_keywords = vars.a["zone_keywords"].ToString();
            string keywords = vars.a["keywords"].ToString();
            string site_meta_description = vars.a["site_meta_description"].ToString();
            string zone_group_meta_description = vars.a["zone_group_meta_description"].ToString();
            string zone_meta_description = vars.a["zone_meta_description"].ToString();
            string meta_description = vars.a["meta_description"].ToString();
            string az_alias = vars.a["az_alias"].ToString();
            string site_header = vars.a["site_header"].ToString();
            string headline = vars.a["headline"].ToString();
            string site_icon = vars.a["site_icon"].ToString();
            int site_id = Convert.ToInt32(vars.a["site_id"]);
            int zone_group_id = Convert.ToInt32(vars.a["zone_group_id"]);
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);

            full_keywords = site_keywords;

            if (!string.IsNullOrEmpty(zone_group_keywords))
            {
                if (zone_group_keywords.StartsWith("|"))
                {
                    full_keywords = zone_group_keywords.Substring(1);
                }
                else
                {
                    full_keywords += ", " + zone_group_keywords;
                }
            }

            if (!string.IsNullOrEmpty(zone_keywords))
            {
                if (zone_keywords.StartsWith("|"))
                {
                    full_keywords = zone_keywords.Substring(1);
                }
                else
                {
                    full_keywords += ", " + zone_keywords;
                }
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                if (keywords.StartsWith("|"))
                {
                    full_keywords = keywords.Substring(1);
                }
                else
                {
                    full_keywords += ", " + keywords;
                }
            }

            if (full_keywords.StartsWith(", "))
                full_keywords = full_keywords.Substring(1);

            string search_index_keywords = full_keywords;

            string full_meta_description = site_meta_description;

            if (!string.IsNullOrEmpty(zone_group_meta_description))
                full_meta_description = zone_group_meta_description;

            if (!string.IsNullOrEmpty(zone_meta_description))
                full_meta_description = zone_meta_description;

            if (!string.IsNullOrEmpty(meta_description))
                full_meta_description = meta_description;

            string search_index_meta_description = meta_description;


            full_keywords = "<meta name=\"DESCRIPTION\" content=\"" + full_meta_description + "\" />" + Environment.NewLine + "<meta name=\"KEYWORDS\" content=\"" + full_keywords + "\" />" + Environment.NewLine;

            if (!string.IsNullOrEmpty(az_alias))
            {
                full_keywords += "<link rel=\"canonical\" href=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/" + az_alias + "\" />" + Environment.NewLine;
            }
            else
            {
                full_keywords += "<link rel=\"canonical\" href=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/" + getQSURLDecoded() + "\" />" + Environment.NewLine;
            }

            // Title
            if (site_header.ToLower().IndexOf("<title>") == -1)
            {
                full_title = "<title>" + GetApplication(Constansts.CFG_TITLE_PREFIX) + headline + GetApplication(Constansts.CFG_TITLE_SUFFIX) + "</title>" + Environment.NewLine;
            }

            // Charset
            if (!string.IsNullOrEmpty(GetApplication(Constansts.CFG_CHARSET)))
            {
                full_charset = "<meta http-equiv=\"content-type\" content=\"text/html; charset=" + GetApplication(Constansts.CFG_CHARSET) + "\" />" + Environment.NewLine;
            }
            else
            {
                full_charset = "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />" + Environment.NewLine;
            }

            //Favourite Icon
            if (!string.IsNullOrEmpty(site_icon))
            {
                favicon = "<link rel=\"shortcut icon\" href=\"" + site_icon + "\" type=\"image/x-icon\" />" + Environment.NewLine + "<link rel=\"icon\" href=\"" + site_icon + "\" type=\"image/x-icon\" />" + Environment.NewLine;
            }

            // Rss Feeds

            DataTable dt1 = Dal.Instance.SelectRssChannels(-1, site_id, zone_group_id, zone_id);
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    string channel_status = dr1[7].ToString();
                    if (channel_status.Equals("A"))
                    {
                        string channel_name = dr1[1].ToString().Replace(@"""", "'");

                        rss_line += "<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" + channel_name + " - RSS 2.0\" href=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/" + Constansts.ALIAS_RSS + "/20/" + dr1[0].ToString() + "/\" />" + Environment.NewLine +
                                     "<link rel=\"alternate\" type=\"application/atom+xml\" title=\"" + channel_name + " - Atom\" href=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/" + Constansts.ALIAS_RSS + "/atom/" + dr1[0].ToString() + "/\" />" + Environment.NewLine;
                    }
                }
            }


            // Force Https Control

            //CmsConfigRepository ccr = new CmsConfigRepository();
            //CmsConfigService configService = new CmsConfigService(ccr);
            //
            //CmsConfig cmsConfig = new CmsConfig();
            //cmsConfig = ccr.GetAll().Where(c => c.Name == "FORCE_HTTPS").FirstOrDefault();
            //
            //string remoteLocal = System.Configuration.ConfigurationManager.AppSettings["EuroCMS.WS"] != null ? System.Configuration.ConfigurationManager.AppSettings["EuroCMS.WS"].ToString() : "";
            string protocol = UrlHelper.GetUriScheme();
            //if (!protocol.Contains("https"))
            //{
            //    if (cmsConfig != null)
            //    {
            //        if (remoteLocal == "remote")
            //        {
            //            if (cmsConfig.RemoteValue.Contains("N"))
            //            {
            //                protocol = "http://";
            //            }
            //            else if (cmsConfig.RemoteValue.Contains("Y"))
            //            {
            //                protocol = "https://";
            //            }
            //        }
            //        else
            //        {
            //            if (cmsConfig.LocalValue.Contains("N"))
            //            {
            //                protocol = "http://";
            //            }
            //            else if (cmsConfig.LocalValue.Contains("Y"))
            //            {
            //                protocol = "https://";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    protocol = "https://";
            //
            //}


            basehref = "<base href=\"" + protocol + HttpContext.Current.Request.Url.Host + "\" />" + Environment.NewLine;

            headers = Environment.NewLine + full_charset + basehref + site_header + Environment.NewLine + full_title + "<script src='tahsin'></script>" + full_keywords + favicon + rss_line + js_line + def_css + Environment.NewLine + css_line + "<script src='emre'></script>";

            result = template.Replace("##HEADERS##", headers);

            return result;
        }


        #endregion
        #region writeJSConstants
        private string writeJSConstants()
        {
            string strR = string.Empty;
            //strR = "<script type=\"text/javascript\">" + Environment.NewLine +
            //    "var CONST_PLUGIN = '" + Constansts.ALIAS_PLUGIN + "';" + Environment.NewLine +
            //    "var CONST_CONTENT = '" +  Constansts.ALIAS_CONTENT + "';" + Environment.NewLine +
            //    "var CONST_CSS = '" +  Constansts.ALIAS_CSS + "';" + Environment.NewLine +
            //    "var CONST_XML = '" +  Constansts.ALIAS_XML + "';" + Environment.NewLine +
            //    "var CONST_FILE_XML = '" +  Constansts.ALIAS_FXML + "';" + Environment.NewLine +
            //    "var CONST_STF = '" +  Constansts.ALIAS_STF + "';" + Environment.NewLine +
            //    "var CONST_STF_SEND = '" +  Constansts.ALIAS_STF_SEND + "';" + Environment.NewLine +
            //    "</script>" + Environment.NewLine;
            return strR;
        }
        #endregion
        #region getProperPageCSS
        private void getProperPageCSS()
        {
            int css_id_1 = 0;
            int css_id_2 = 0;
            int css_id_3 = 0;

            int css_print_1 = 0;
            int css_print_2 = 0;
            int css_print_3 = 0;

            if (vars.IsMobile)
            {
                css_id_1 = Convert.ToInt32(vars.a["site_css_id_mobile"]);
                css_id_2 = Convert.ToInt32(vars.a["zg_css_id_mobile"]); ;
                css_id_3 = Convert.ToInt32(vars.a["zone_css_id_mobile"]);
            }
            else
            {
                css_id_1 = Convert.ToInt32(vars.a["site_css_id"]);
                css_id_2 = Convert.ToInt32(vars.a["zg_css_id"]); ;
                css_id_3 = Convert.ToInt32(vars.a["zone_css_id"]);
            }

            css_print_1 = Convert.ToInt32(vars.a["site_css_id_print"]);
            css_print_2 = Convert.ToInt32(vars.a["zg_css_id_print"]); ;
            css_print_3 = Convert.ToInt32(vars.a["zone_css_id_print"]);

            int zone_css_merge = Convert.ToInt32(vars.a["zone_css_merge"]);
            if (zone_css_merge == 1)
            {
                css_id_1 = 0;
                css_id_2 = 0;
                css_print_1 = 0;
                css_print_2 = 0;
            }

            int zg_css_merge = Convert.ToInt32(vars.a["zg_css_merge"]);
            if (zg_css_merge == 1)
            {
                css_id_1 = 0;
                css_print_1 = 0;
            }

            vars.setValue2Dic("css_id_1", css_id_1);
            vars.setValue2Dic("css_id_2", css_id_2);
            vars.setValue2Dic("css_id_3", css_id_3);
            vars.setValue2Dic("css_print_1", css_print_1);
            vars.setValue2Dic("css_print_2", css_print_2);
            vars.setValue2Dic("css_print_3", css_print_3);
        }
        #endregion
        #region checkTemplateForHeaders
        private string checkTemplateForHeaders(string template)
        {
            string str = string.Empty;
            str = template;
            string full_omniture_code = vars.a["full_omniture_code"].ToString().Trim();
            string omniture = string.Empty;
            string lang = vars.a["lang_id"].ToString().ToLower();
            string custom_body = vars.a["custom_body"].ToString();

            if (!string.IsNullOrEmpty(full_omniture_code))
            {
                omniture = "<script language=\"JavaScript\" type=\"text/javascript\"src=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/i/" + GetApplication(Constansts.CFG_OMNITURE_SCODE_FILENAME) + "?rc=" + CmsHelper.c2QS(GetApplication(Constansts.LAST_UPDATE)) + "\"></script>" + Environment.NewLine + "<script language=\"JavaScript\" type=\"text/javascript\">" + Environment.NewLine + full_omniture_code + Environment.NewLine + "var s_code=s.t();if(s_code)document.write(s_code)" + Environment.NewLine + "</script>" + Environment.NewLine + GetApplication(Constansts.CFG_OMNITURE_PAGE_CODE);
            }

            if (!string.IsNullOrEmpty(GetApplication(Constansts.CFG_OMNITURE_TESTNTARGET_FILENAME)))
            {
                omniture += "<script language=\"JavaScript\" type=\"text/javascript\" src=\"" + UrlHelper.GetUriScheme() + HttpContext.Current.Request.Url.Host + "/i/" + GetApplication(Constansts.CFG_OMNITURE_TESTNTARGET_FILENAME) + "?rc=" + CmsHelper.c2QS(GetApplication(Constansts.LAST_UPDATE)) + "\"></script>";
            }

            if (template.IndexOf("<html") == -1 && template.IndexOf("<head") == -1)
            {
                // No head or html.. insert defaults
                //str = "<html xmlns=\"http://www.w3.org/1999/xhtml\" lang=\"" + lang + "\" xml:lang=\"" + lang + "\" ><head>" + Environment.NewLine + Environment.NewLine + "##HEADERS##" + Environment.NewLine + Environment.NewLine + "</head>" + Environment.NewLine + Environment.NewLine + str + Environment.NewLine + Environment.NewLine + "</html>";
                str = "<html xmlns=\"http://www.w3.org/1999/xhtml\" lang=\"" + lang + "\" xml:lang=\"" + lang + "\">" + Environment.NewLine + "<head>" + Environment.NewLine + "##HEADERS##" + Environment.NewLine + "</head>" + Environment.NewLine + str + Environment.NewLine + "</html>";
            }

            if (template.IndexOf("<body") == -1)
            {
                // No body.. insert 
                str = str.Replace("</head>", Environment.NewLine + "</head>" + Environment.NewLine + "<body lang=\"" + lang + "\" xml:lang=\"" + lang + "\" " + custom_body.Replace(Environment.NewLine, " ") + ">" + Environment.NewLine + omniture + Environment.NewLine);
                str = str.Replace("</html>", Environment.NewLine + "</body>" + Environment.NewLine + "</html>" + Environment.NewLine);
            }

            if (template.IndexOf("!<doctype") == -1)
            {
                // No doc type
                string template_html_doctype = vars.a["template_html_doctype"].ToString();
                if (string.IsNullOrEmpty(template_html_doctype.Trim()))
                {
                    str = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">  " + Environment.NewLine + Environment.NewLine + str;
                }
                else
                {
                    str = HttpUtility.HtmlDecode(template_html_doctype) + Environment.NewLine + Environment.NewLine + str;
                }
            }

            return str;
        }
        #endregion
        #region getTemplateHTML
        private string getTemplateHTML(int template_id)
        {
            string template = GetApplication("TEMPLATE_" + template_id);
            string template_html = string.Empty;
            string template_html_doctype = string.Empty;

            if (string.IsNullOrEmpty(template) || !IsCacheActive())
            {
                DataTable dt = Dal.Instance.SelectTemplateHtml(template_id);
                if (dt.Rows.Count > 0)
                {
                    template_html = dt.Rows[0][1].ToString();
                    template_html_doctype = dt.Rows[0][6].ToString();

                    //if (!string.IsNullOrEmpty(template_html))
                    //    template_html = HttpUtility.HtmlDecode(template_html);

                    //if (!string.IsNullOrEmpty(template_html_doctype))
                    //    template_html_doctype = HttpUtility.HtmlDecode(template_html_doctype);

                    if (!IsCacheActive())
                    {
                        SetApplication("TEMPLATE_" + template_id, template_html);
                        SetApplication("TEMPLATE_" + template_id + "_DOCTYPE", template_html_doctype);
                    }

                    template_html = "<!-- From DB -->" + Environment.NewLine + template_html;
                }
            }
            else
            {
                template_html = GetApplication("TEMPLATE_" + template_id);
                template_html_doctype = GetApplication("TEMPLATE_" + template_id + "_DOCTYPE");
                template_html = "<!-- Cached: " + GetApplication(Constansts.LAST_UPDATE) + " -->" + Environment.NewLine + template_html;
            }
            vars.setValue2Dic("template_html", HttpUtility.HtmlDecode(template_html));
            vars.setValue2Dic("template_html_doctype", HttpUtility.HtmlDecode(template_html_doctype));
            return HttpUtility.HtmlDecode(template_html);
        }
        #endregion
        #region getProperPageTemplate
        private void getProperPageTemplate()
        {
            int template_id = 0;

            if (vars.IsMobile)
            {
                template_id = Convert.ToInt32(vars.a["zone_template_id_mobile"]);
                if (template_id < 1) template_id = Convert.ToInt32(vars.a["zone_template_id_mobile"]);
                if (template_id < 1) template_id = Convert.ToInt32(vars.a["site_template_id_mobile"]);
            }

            // No Mobile Template found or NOT Mobile
            if ((vars.IsMobile && template_id < 1) || !vars.IsMobile)
            {
                template_id = Convert.ToInt32(vars.a["zone_template_id"]);
                if (template_id < 1) template_id = Convert.ToInt32(vars.a["zg_template_id"]);
                if (template_id < 1) template_id = Convert.ToInt32(vars.a["site_template_id"]);
            }

            if (template_id < 1)
                renderHome("No Template Selected. Please Contact: " + GetApplication(Constansts.CFG_ADMIN_CONTACT));

            vars.setValue2Dic("template_id", template_id);
        }
        #endregion
        #region processArticleRedirection
        private void processArticleRedirection(string p)
        {
            string[] arr_custom_setting;
            Type t = vars.a["arr_custom_setting"].GetType();
            if (!t.IsArray)
            {

                string custom_setting = vars.a["arr_custom_setting"].ToString();
                if (custom_setting.IndexOf(";") == -1)
                    custom_setting += ";";

                arr_custom_setting = custom_setting.Split(';');
            }
            else
                arr_custom_setting = (string[])vars.a["arr_custom_setting"];

            string permanent_redirection = arr_custom_setting[0];

            if (permanent_redirection == "Y")
                HttpContext.Current.Response.Status = "301 Moved Permanently";

            int article_type = Convert.ToInt32(vars.a["article_type"]);
            string article_type_detail = vars.a["article_type_detail"].ToString();

            switch (article_type)
            {
                case 1:
                    if (permanent_redirection == "Y")
                        HttpContext.Current.Response.AddHeader("Location", article_type_detail);
                    else
                        HttpContext.Current.Response.Redirect(article_type_detail);
                    break;
                //Response.End();
                case 2:
                    string[] atda = article_type_detail.Split('-');
                    if (atda.Length == 2)
                    {
                        if (CmsHelper.IsNumeric(atda[0]) && CmsHelper.IsNumeric(atda[1]))
                        {
                            string strSQL = "select top 1 az.zone_id, az.article_id, az.site_name, az.zone_group_name, az.zone_name, az.headline, az.az_alias from dbo.vArticlesZones az with (nolock) where az.zone_id = " + atda[0] + " and az.article_id = " + atda[1];
                            DataTable dt = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string atdurl = string.Empty;
                                if (p == "ajax")
                                {
                                    atdurl = "/" + Constansts.ALIAS_PLUGIN + "/pure_content/" + dt.Rows[0][1].ToString();
                                }
                                else if (p == "pure_content")
                                {
                                    atdurl = "/" + Constansts.ALIAS_PLUGIN + "/pc/" + dt.Rows[0][1].ToString() + "/" + dt.Rows[0][0].ToString();
                                }
                                else
                                {
                                    atdurl = getContentLinkAlias(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString());
                                }

                                if (permanent_redirection == "Y")
                                {
                                    HttpContext.Current.Response.AddHeader("Location", atdurl);
                                }
                                else
                                {
                                    HttpContext.Current.Response.Redirect(atdurl);
                                }
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                    break;
                case 3:
                case 4:
                case 7:
                case 8:
                    if (!CmsHelper.IsNumeric(article_type_detail))
                    {
                        rwe("Cannot redirect to undefined zones. Please Contact: " + GetApplication(Constansts.CFG_ADMIN_CONTACT));
                    }
                    else if (article_type_detail.IndexOf("-") != -1)
                    {
                        rwe("Invalid zone selection for redirection. Please Contact: " + GetApplication(Constansts.CFG_ADMIN_CONTACT));
                    }
                    else
                    {
                        string strSQL = "select top 1 az.zone_id, az.article_id, az.site_name, az.zone_group_name, az.zone_name, az.headline, az.az_alias from dbo.vArticlesZones az with (nolock) where az.zone_id = " + article_type_detail + " order by ";
                        if (article_type == 3) strSQL += " updated desc";
                        if (article_type == 4) strSQL += " az_order asc, headline asc";
                        if (article_type == 7) strSQL += " updated asc";
                        if (article_type == 8) strSQL += " az_order desc, headline asc";

                        DataTable dt = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            string atdurl = string.Empty;

                            if (p == "ajax")
                            {
                                atdurl = "/" + Constansts.ALIAS_PLUGIN + "/pure_content/" + dt.Rows[0][1].ToString();
                            }
                            else
                            {
                                atdurl = getContentLinkAlias(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString());
                            }

                            if (permanent_redirection == "Y")
                            {
                                HttpContext.Current.Response.AddHeader("Location", atdurl);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect(atdurl);
                            }
                            HttpContext.Current.Response.End();
                        }
                        else
                        {
                            rwe("Redirection zone note found. Please Contact: " + GetApplication(Constansts.CFG_ADMIN_CONTACT));
                        }
                    }
                    break;
                case 6:
                    rwe("Cannot display Custom URL'ed Articles. Please Contact: " + GetApplication(Constansts.CFG_ADMIN_CONTACT));
                    break;
            }
        }
        #endregion
        #endregion
        #region Shared
        #region getQSURLDecoded
        public string getQSURLDecoded()
        {
            return noInj(HttpUtility.UrlDecode(CmsHelper.GetQS()));
        }
        #endregion
        #region rwe
        public void rwe(string s)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
        #endregion
        #region GetSession
        public string GetSession(string key)
        {
            return HttpContext.Current.Session[key] != null ? HttpContext.Current.Session[key].ToString() : string.Empty;
        }
        #endregion
        #region SetSession
        public void SetSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }
        #endregion
        #region GetCookieValue
        public string GetCookieValue(string cookieName, string key)
        {
            string returnValue = string.Empty;
            if (HttpContext.Current.Request.Browser.Cookies)
            {
                if (HttpContext.Current.Request.Cookies[cookieName] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                    returnValue = cookie[key];
                    // cookie.Expires = DateTime.Today.AddYears(2);
                }
            }
            return returnValue;
        }
        #endregion
        #region SetCookieValue
        public void SetCookieValue(string cookieName, string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(cookie[key]))
                    cookie.Values.Remove(key);

                cookie.Values.Add(key, value);
                cookie.Expires = DateTime.Today.AddYears(2);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            else
            {
                cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Today.AddYears(2);
                cookie.Values.Add(key, value);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        #endregion
        #region GetApplication
        public string GetApplication(string key)
        {
            //object instance = (object) Cache["thekey"];
            //if (instance == null)
            //{
            //    instance = GetNewValueAndInsert(); // Get new data to insert into the cache
            //    Cache.Insert(key, instance, ...);
            //}
            //return instance;
            return HttpContext.Current.Cache[key] != null ? HttpContext.Current.Cache[key].ToString() : string.Empty;
        }
        #endregion
        #region SetApplication
        public void SetApplication(string key, string value)
        {
            string file = string.IsNullOrEmpty(dependecyFile) ? HttpContext.Current.Server.MapPath("~/App_Data/cache.dat") : dependecyFile;
            HttpContext.Current.Cache.Insert(key, value, new CacheDependency(file),
                DateTime.Now.AddDays(1),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void SetApplication(string key, string value, CacheItemUpdateCallback updateCallback)
        {
            string file = string.IsNullOrEmpty(dependecyFile) ? HttpContext.Current.Server.MapPath("~/App_Data/cache.dat") : dependecyFile;
            HttpContext.Current.Cache.Insert(key, value, new CacheDependency(file),
                DateTime.Now.AddDays(1),
                System.Web.Caching.Cache.NoSlidingExpiration,
                updateCallback);
        }
        //private void ConfigCacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out object value, out CacheDependency dependency, out DateTime exipriation, out TimeSpan slidingExpiration)
        //{
        //    string file = string.IsNullOrEmpty(dependecyFile) ? Server.MapPath("~/App_Data/cache.dat") : dependecyFile;
        //    dependency = new CacheDependency(file);
        //    exipriation = DateTime.Now.AddDays(1);
        //    slidingExpiration = Cache.NoSlidingExpiration;
        //    value = doc;
        //}
        #endregion
        #region GetServerVariables
        public string GetServerVariables(string key)
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_CACHE_CONTROL"] ?? string.Empty;
        }
        #endregion
        #region SetDebugSQL
        public void SetDebugSQL(string value)
        {
            HttpContext.Current.Session[Constansts.DEBUG_SQL_KEY] = value;
        }
        #endregion
        #region GetHttpPrefix
        public string GetHttpPrefix()
        {
            return string.Format("http{0}://", HttpContext.Current.Request.IsSecureConnection ? "s" : "");
        }
        #endregion
        #region convertAmpersands
        public string convertAmpersands(string s)
        {
            return !string.IsNullOrEmpty(s) ? CmsHelper.RegexReplace(s, @"&(?!(\w+;)|(\x23\w+;))", "&amp;", false, false, true, true) : string.Empty;
        }
        #endregion
        #endregion
        #region Common
        #region IsCacheActive (Tested)
        public bool IsCacheActive()
        {
            return GetApplication(Constansts.CFG_CACHE_ACTIVE).Equals("1");
        }
        #endregion
        #region add2Log (Tested)
        public static void add2Log(object pubID, int noteID, string idType, string type, string note)
        {

            if (idType.IndexOf("_id") == -1)
                idType = "";

            if (idType.Equals("rev_id"))
            {
                if (idType.StartsWith("ZONE"))
                    idType = "arev_id";
                else
                    idType = "zrev_id";
            }

            if (idType.Equals("history_id"))
            {
                if (idType.StartsWith("CSS"))
                    idType = "chistory_id";
                else
                    idType = "thistory_id";
            }

            // Dal.Instance.AdminInsertLog(pubID, noteID, type, note, CmsHelper.GetCurrentIP(), idType);
        }

        #endregion
        #region gotInHtml (Tested)
        public bool gotCookie(string html)
        {
            return (html.IndexOf("##cookie_") != -1);
        }

        public bool gotSession(string html)
        {
            return (html.IndexOf("##session_") != -1);
        }

        public bool gotSTF(string html)
        {
            return (html.IndexOf("##stf_link_") != -1);
        }

        public bool gotPlugin(string html)
        {
            return (html.IndexOf("##plugin_") != -1);
        }

        public bool gotMenu(string html)
        {
            return (html.IndexOf("##menu_") != -1);
        }

        public bool gotPortlet(string html)
        {
            return (html.IndexOf("##portlet_") != -1);
        }

        public bool gotSubPortlet(string html)
        {
            return (html.IndexOf("##portlet_") != -1 && html.IndexOf("id=\"-1_") != -1);
        }

        public bool gotAFiles(string html)
        {
            return (html.IndexOf("##afiles_") != -1);
        }

        public bool gotSitemap(string html)
        {
            return (html.IndexOf("##sitemap_") != -1);
        }

        public bool gotBreadCrumb(string html)
        {
            return (html.IndexOf("##breadcrumb_") != -1);
        }
        #endregion
        #region getOrderColumn (Tested)
        public string getOrderColumn(int orderID)
        {
            string orderColumn = string.Empty;
            switch (orderID)
            {
                case 0:
                    orderColumn = "startdate asc";
                    break;
                case 1:
                    orderColumn = "updated desc";
                    break;
                case 2:
                    orderColumn = "headline asc";
                    break;
                case 3:
                    orderColumn = "az_order asc";
                    break;
                case 4:
                    orderColumn = "date_1 desc";
                    break;
                case 5:
                    orderColumn = "date_2 desc";
                    break;
                case 6:
                    orderColumn = "flag_1 desc";
                    break;
                case 7:
                    orderColumn = "flag_2 desc";
                    break;
                case 8:
                    orderColumn = "flag_3 desc";
                    break;
                case 9:
                    orderColumn = "flag_4 desc";
                    break;
                case 10:
                    orderColumn = "flag_5 desc";
                    break;
                case 11:
                    orderColumn = "az_order desc";
                    break;
                case 12:
                    orderColumn = "date_1 asc";
                    break;
                case 13:
                    orderColumn = "date_2 asc";
                    break;
                case 14:
                    orderColumn = "startdate desc";
                    break;
                case 15:
                    orderColumn = "created asc";
                    break;
                case 16:
                    orderColumn = "created desc";
                    break;
                default:
                    orderColumn = "updated desc";
                    break;
            }
            return orderColumn;
        }
        #endregion
        #region getRecordsetNames (Tested)
        public string getRecordsetNames(DataTable dt)
        {
            string str = string.Empty;
            string it = string.Empty;

            foreach (DataColumn dc in dt.Columns)
            {
                str += string.Format(",{0}", dc.ColumnName);
            }
            if (str.StartsWith(","))
                str = str.Substring(1);
            return str;
        }
        #endregion
        #region getRandomChars (Tested)
        public string getRandomChars(int hane)
        {
            return CmsHelper.RandomPassword.Generate(hane, false);
        }
        #endregion
        #region noInj (Tested)
        public string noInj(string s)
        {
            string temp = s;
            temp = utf2iso(temp);
            if (!string.IsNullOrEmpty(s))
            {
                temp = temp.Replace("--", "");
                temp = temp.Replace("<", "");
                temp = temp.Replace(">", "");
                temp = temp.Replace("'", "''");
                temp = temp.Replace("&#8217;", "''");
                temp = temp.Replace("&#8216;", "''");
                temp = temp.Replace("exec ", "");
                temp = temp.Replace("exec%20", "");
                temp = temp.Replace("cmdshell", "");
                temp = temp.Replace("shutdown", "");
                temp = temp.Replace("RegisterUser", "");
                temp = temp.Replace("VerifyCredentials", ""); ;
                temp = temp.Replace("ChangePassword", "");
                temp = CmsHelper.RegexReplace(temp, @";\s*delete\s*", "", false, false, true, true);
                temp = CmsHelper.RegexReplace(temp, @"drop\s*table*", "", false, false, true, true);
                temp = CmsHelper.RegexReplace(temp, @"create\s*table", "", false, false, true, true);
                temp = temp.Replace("Â", "");
                temp = temp.Replace("Ã", "");
            }
            return temp;
        }
        #endregion
        #region Utf2Iso (Tested)
        public string utf2iso(string s)
        {
            string temp = s;
            if (!string.IsNullOrEmpty(s))
            {
                temp = temp.Replace("ÄŸ", "ğ");
                temp = temp.Replace("Ã¼", "ü");
                temp = temp.Replace("ÅŸ", "ş");
                temp = temp.Replace("Ã¶", "ö");
                temp = temp.Replace("Ã§", "ç");
                temp = temp.Replace("Ä", "Ğ");
                temp = temp.Replace("Ãœ", "Ü");
                temp = temp.Replace("Å", "Ş");
                temp = temp.Replace("Ä°", "İ");
                temp = temp.Replace("Ã–", "Ö");
                temp = temp.Replace("Ã‡", "Ç");
                temp = temp.Replace("Ä±", "ı");
                temp = temp.Replace("Ã©", "&eacute;");
                temp = temp.Replace("â‚¬", "&euro;");
                temp = temp.Replace("â„¢", "&trade;");
                temp = temp.Replace("¤", "&auml;");
                temp = temp.Replace("©", "&copy;");
                temp = temp.Replace("é", "&eacute;");
                temp = temp.Replace("Î»", "&#955;");
                temp = temp.Replace("Î\"", "&#916;");
                temp = temp.Replace("â€™", "'");
                temp = temp.Replace("â€˜", "'");
                temp = temp.Replace("â€", "\"");
                temp = temp.Replace("â€œ", "\"");
                temp = temp.Replace("â€\"", "-");
            }
            return temp;
        }
        #endregion
        #region CheckIP
        public bool CheckIp(string currentIP, string rangeIps)
        {
            bool retVal = false;
            string tempRangeIps = rangeIps;
            if (!string.IsNullOrEmpty(rangeIps))
            {
                tempRangeIps = tempRangeIps.Trim();
                tempRangeIps = tempRangeIps.Replace(Environment.NewLine, "|~|").Replace("\r", "|~|").Replace("\n", "|~|").Replace(";", "|~|").Replace(",", "|~|");

                string[] arrtempRangeIps = tempRangeIps.Split(new string[] { "|~|" }, StringSplitOptions.None);
                foreach (string ip in arrtempRangeIps)
                {
                    if (ip.IndexOf("-") == -1 && ip.IndexOf("*") == -1)
                    {
                        if (ip == currentIP)
                        {
                            retVal = true;
                            break;
                        }
                    }
                    else if (ip.IndexOf("*") != -1)
                    {
                        if (ip.Substring(0, ip.IndexOf("*") - 1) == currentIP.Substring(0, ip.IndexOf("*") - 1))
                        {
                            retVal = true;
                            break;
                        }
                    }
                    else if (subStrCount(ip, "-") == 1)
                    {
                        int currentIPlong = Ip2Long(ip);
                        if (currentIPlong > 0)
                        {
                            string[] ips = ip.Split('-');
                            int minIP = Ip2Long(ips[0]);
                            int maxIP = Ip2Long(ips[0]);
                            if (minIP > 0 && maxIP > 0 && currentIPlong >= minIP && currentIPlong <= maxIP)
                            {
                                retVal = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
                retVal = true;
            return retVal;
        }
        #endregion
        #region cAD2RssAtomD
        private string cAD2RssAtomD(DateTime dateTime)
        {
            return DateTime.Now.Year + "-" + CmsHelper.dDay(DateTime.Now.Month.ToString()) + "-" + CmsHelper.dDay(DateTime.Now.Day.ToString()) + "-" + CmsHelper.dDay(DateTime.Now.Month.ToString()) + "-" + CmsHelper.dDay(DateTime.Now.Minute.ToString()) + "-" + CmsHelper.dDay(DateTime.Now.Second.ToString()) + "Z";
        }
        #endregion
        #region subStrCount
        public int subStrCount(string a, string termine)
        {
            int retVal = 0;
            a = a.Trim() + "";
            termine = termine.Trim() + "";

            if (a.Length > 0 && termine.Length > 0)
            {
                Regex reg = new Regex(termine, RegexOptions.IgnoreCase);
                retVal = reg.Matches(a).Count;
            }
            return retVal;
        }
        #endregion
        #region Ip2Long
        public int Ip2Long(string ip)
        {
            int retVal = 0;
            string[] faktorer = ip.Split('.');
            for (int x = 0; x <= 3; x++)
            {
                int xpn = 3 - x;
                if (CmsHelper.IsNumeric(faktorer[x]))
                {
                    retVal = retVal + Convert.ToInt32(faktorer[x]) * 256 ^ xpn;
                }
                else
                    break;
            }
            return retVal;
        }
        #endregion
        #region getCharset (Tested)
        public string getCharset()
        {
            return string.IsNullOrEmpty(GetApplication(Constansts.CFG_CHARSET)) ? "utf-8" : GetApplication(Constansts.CFG_CHARSET);
        }
        #endregion
        #region  replaceArticleDetailsRows
        public string replaceArticleDetailsRows(DataRow dr, string rsNames, string html, string zoneOw)
        {
            string str = string.Empty;
            string itName = string.Empty;
            string customComboLalel = String.Empty;
            string customComboValue = string.Empty;
            str = html;

            string[] names = rsNames.Split(',');

            foreach (string s in names)
            {
                itName = s;
                object val = null;

                if (dr[itName] != DBNull.Value)
                    val = dr[itName];
                else
                    val = string.Empty;

                string strVal = string.Empty;
                if (val.GetType() == typeof(bool))
                    strVal = Convert.ToBoolean(val) == true ? "1" : "0";
                else
                    strVal = val.ToString();

                switch (itName)
                {
                    case "article_type":
                        vars.a["article_type"] = strVal;
                        break;
                    case "article_type_detail":
                        vars.a["article_type_detail"] = strVal;
                        break;
                    case "zone_id":
                        vars.a["zone_id"] = strVal;
                        if (CmsHelper.IsNumeric(zoneOw)) vars.a["zone_id"] = zoneOw.ToString();
                        break;
                    case "article_id":
                        vars.a["article_id"] = strVal;
                        break;
                    case "site_name":
                        vars.a["site_name"] = strVal;
                        vars.a["site_name_backup"] = strVal;
                        break;
                    case "zone_group_name":
                        vars.a["zone_group_name"] = strVal;
                        vars.a["zone_group_name_backup"] = strVal;
                        break;
                    case "zone_name":
                        vars.a["zone_name"] = strVal;
                        vars.a["listed_zone_name"] = strVal;
                        vars.a["zone_name_backup"] = strVal;
                        break;
                    case "zone_name_display":
                        if (string.IsNullOrEmpty(strVal)) strVal = vars.a["zone_name_backup"].ToString();
                        vars.a["zone_name_display"] = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(strVal));
                        break;
                    case "zone_group_name_display":
                        if (string.IsNullOrEmpty(strVal)) strVal = vars.a["zone_group_name_backup"].ToString();
                        vars.a["zone_group_name_display"] = strVal;
                        break;
                    case "site_name_display":
                        if (string.IsNullOrEmpty(strVal)) strVal = vars.a["site_name_backup"].ToString();
                        vars.a["site_name_display"] = strVal;
                        break;
                    case "headline":
                        vars.a["headline"] = strVal;
                        break;
                    case "publisher_id":
                        vars.a["publisher_id"] = strVal;
                        break;
                    case "az_alias":
                        vars.a["az_alias"] = strVal;
                        break;
                    case "menu_text":
                        vars.a["menu_text"] = strVal;
                        break;
                }

                Regex regex = new Regex("##" + itName + ",([^#]+)##", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                if (regex.IsMatch(html))
                {
                    MatchCollection matches = regex.Matches(html);
                    foreach (Match m in matches)
                    {
                        string tmpVal = strVal;
                        //Match nm = m.NextMatch();
                        string regex_params = m.Groups[1].Value;

                        if (s.IndexOf("date") != -1)
                        {
                            if (CmsHelper.IsDate2(strVal))
                                str = str.Replace("##" + itName + "," + regex_params + "##", formatDate(regex_params, strVal));
                            else
                                str = str.Replace("##" + itName + "," + regex_params + "##", strVal);
                        }
                        else if (regex_params.IndexOf(",") != -1)
                        {
                            string[] arr = regex_params.Split(',');
                            if (CmsHelper.IsNumeric(arr[0]))
                            {
                                if (Convert.ToInt32(arr[0]) > 0)
                                {
                                    tmpVal = dotLeft(tmpVal, Convert.ToInt32(arr[0]), "");
                                }
                            }

                            if (arr[1].Equals("Y"))
                                tmpVal = replaceBBCode(tmpVal);
                            else
                                tmpVal = pruneBBCode(tmpVal);

                            str = str.Replace("##" + itName + "," + regex_params + "##", strVal);
                        }
                        else
                        {
                            if (CmsHelper.IsNumeric(regex_params))
                            {
                                if (Convert.ToInt32(regex_params) > 0)
                                {
                                    str = str.Replace("##" + itName + "," + regex_params + "##", dotLeft(tmpVal, Convert.ToInt32(regex_params), ""));
                                }
                            }
                        }
                    }
                }

                switch (itName)
                {
                    case "custom_1":
                    case "custom_2":
                    case "custom_3":
                    case "custom_4":
                    case "custom_5":
                    case "custom_6":
                    case "custom_7":
                    case "custom_8":
                    case "custom_9":
                    case "custom_10":
                    case "custom_11":
                    case "custom_12":
                    case "custom_13":
                    case "custom_14":
                    case "custom_15":
                    case "custom_16":
                    case "custom_17":
                    case "custom_18":
                    case "custom_19":
                    case "custom_20":
                        if (strVal.IndexOf("|~|") != -1)
                        {
                            customComboLalel = strVal.Substring(strVal.IndexOf("|~|") + 3, strVal.Length - strVal.IndexOf("|~|") - 3);
                            customComboValue = strVal.Substring(0, strVal.IndexOf("|~|"));
                            str = str.Replace("##" + itName + "_value##", customComboValue);
                            str = str.Replace("##" + itName + "##", customComboLalel);
                        }
                        else
                        {
                            str = str.Replace("##" + itName + "_value##", strVal);
                            str = str.Replace("##" + itName + "##", strVal);
                        }
                        break;
                    case "headline":
                        str = str.Replace("##" + itName + "##", strVal);
                        break;
                    default:
                        str = str.Replace("##" + itName + "##", strVal);
                        break;
                }

                switch (itName)
                {
                    case "article_1":
                    case "article_2":
                    case "article_3":
                    case "article_4":
                    case "article_5":
                    case "custom_1":
                    case "custom_2":
                    case "custom_3":
                    case "custom_4":
                    case "custom_5":
                    case "custom_6":
                    case "custom_7":
                    case "custom_8":
                    case "custom_9":
                    case "custom_10":
                    case "custom_11":
                    case "custom_12":
                    case "custom_13":
                    case "custom_14":
                    case "custom_15":
                    case "custom_16":
                    case "custom_17":
                    case "custom_18":
                    case "custom_19":
                    case "custom_20":
                    case "summary":
                    case "headline":
                    case "menu_text":
                    case "date_1":
                    case "date_2":
                    case "date_3":
                    case "date_4":
                    case "date_5":

                        if (!string.IsNullOrEmpty(vars.a[itName].ToString()))
                            vars.a[itName + "_exist"] = "exist";
                        break;
                }
            }

            int rce = str.IndexOf("##random,");
            if (rce != -1)
            {
                int rcee = str.IndexOf("##", rce + 2);
                if (rcee != -1)
                {
                    string arr = str.Substring(rce, rcee - rce);
                    arr = arr.Replace("##", "");
                    arr = arr.Replace("random,", "");
                    if (CmsHelper.IsNumeric(arr))
                    {
                        if (Convert.ToInt32(arr) > 0)
                        {
                            str = str.Replace("##random," + arr + "##", getRandomChars(Convert.ToInt32(arr)));
                        }
                    }
                }
            }

            str = str.Replace("Î", "&#916;");
            str = str.Replace("Î\"", "&#916;");
            //str = CmsHelper.RegexReplace(str, "[script.*src=\x22(.*?)\x22.*][\x2Fscript]", "<script type=\"text/javascript\" src=\"$1\"></script>", false, false, true, false);
            //str = str.Replace("/[script/]", "<script type=\"text/javascript\">");
            //str = str.Replace("/[/script/]", "</script>");
            //str = str.Replace("/[style/]", "<style type=\"text/css\">");
            //str = str.Replace("/[/style/]", "</style>");
            str = str.Replace("##current_year##", DateTime.Now.Year.ToString());
            str = str.Replace("##current_month##", DateTime.Now.Month.ToString());
            str = str.Replace("##current_day##", DateTime.Now.Day.ToString());

            if (str.IndexOf("[code]") != -1)
            {
                Regex reg = new Regex("[code](.|.*\n|.*\r\n)*?[/code]", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                MatchCollection matches = reg.Matches(str);
                foreach (Match m in matches)
                {
                    str = str.Replace(m.Value, HttpUtility.HtmlEncode(m.Value.Replace("[code]", "").Replace("[/code]", "")));
                }
            }

            if (str.IndexOf("##omniture_") != -1)
            {
                str = str.Replace("##omniture_site_name##", CmsHelper.c2QS(vars.a["site_name"].ToString()));
                str = str.Replace("##omniture_zone_group_name##", CmsHelper.c2QS(vars.a["zone_group_name"].ToString()));
                str = str.Replace("##omniture_zone_group_name_display##", CmsHelper.c2QS(vars.a["zone_group_name_display"].ToString()));
                str = str.Replace("##omniture_zone_name##", CmsHelper.c2QS(vars.a["zone_name"].ToString()));
                str = str.Replace("##omniture_zone_name_display##", HttpUtility.UrlDecode(HttpUtility.HtmlDecode(CmsHelper.c2QS(vars.a["zone_name_display"].ToString()))));
                str = str.Replace("##omniture_headline##", CmsHelper.c2QS(vars.a["headline"].ToString()));
            }


            if (vars.a["article_type"].ToString().Equals("1") && vars.a["article_type_detail"].ToString().StartsWith("http")
                && str.IndexOf("href=\"##article_detail_url##\"") != -1)
            {
                str = str.Replace("href=\"##article_detail_url##\"", "href=\"" + vars.a["article_type_detail"].ToString() + "\" target=\"_blank\"");
            }
            else if (vars.a["article_type"].ToString().Equals("1") && vars.a["article_type_detail"].ToString().StartsWith("http")
               && str.IndexOf("value=\"##article_detail_url##\"") != -1)
            {
                str = str.Replace("value=\"##article_detail_url##\"", "value=\"" + vars.a["article_type_detail"].ToString() + "\"");
            }
            else if (vars.a["article_type"].ToString().Equals("6"))
            {
                str = str.Replace("##article_detail_url##", "" + vars.a["article_type_detail"].ToString() + "");
            }
            else
            {
                if (!string.IsNullOrEmpty(vars.a["mapped_article_url"].ToString()))
                {
                    str = str.Replace("##article_detail_url##", "" + vars.a["mapped_article_url"].ToString() + "");
                }
                else
                {
                    str = str.Replace("##article_detail_url##", getContentLinkAlias(vars.a["zone_id"].ToString(), vars.a["article_id"].ToString(), vars.a["site_name"].ToString(), vars.a["zone_group_name"].ToString(), vars.a["zone_name"].ToString(), vars.a["headline"].ToString(), vars.a["az_alias"].ToString()));
                }
            }

            //if (str.IndexOf("##article_owner_") != -1)
            //{
            //    DataTable dt = Dal.Instance.SelectPublisherDetails(vars.pubID);
            //    if (dt.Rows.Count > 0)
            //    {
            //        vars.pubName = dt.Rows[0][1].ToString();
            //        vars.pubEmail = dt.Rows[0][5].ToString();
            //        vars.pubDept = dt.Rows[0][7].ToString();
            //    }

            //    str = str.Replace("##article_owner_name##", "" + vars.pubName);
            //    str = str.Replace("##article_owner_email##", "" + vars.pubEmail);
            //    str = str.Replace("##article_owner_dept##", "" + vars.pubDept);
            //}

            if (str.IndexOf("##site_url") != -1)
            {
                str = str.Replace("##site_url##", (HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://") + HttpContext.Current.Request.Url.Host);
            }

            if (str.IndexOf("##menu_text_headline") != -1)
            {
                if (vars.a["menu_text_headline"].ToString() != "")
                {
                    str = str.Replace("##menu_text_headline##", vars.a["menu_text"].ToString());
                }
                else
                {
                    str = str.Replace("##menu_text_headline##", vars.a["headline"].ToString());
                }
            }

            str = str.Replace("##article_1_exist##", vars.a["article_1_exist"].ToString());
            str = str.Replace("##article_2_exist##", vars.a["article_2_exist"].ToString());
            str = str.Replace("##article_3_exist##", vars.a["article_3_exist"].ToString());
            str = str.Replace("##article_4_exist##", vars.a["article_4_exist"].ToString());
            str = str.Replace("##article_5_exist##", vars.a["article_5_exist"].ToString());
            str = str.Replace("##custom_1_exist##", vars.a["custom_1_exist"].ToString());
            str = str.Replace("##custom_2_exist##", vars.a["custom_2_exist"].ToString());
            str = str.Replace("##custom_3_exist##", vars.a["custom_3_exist"].ToString());
            str = str.Replace("##custom_4_exist##", vars.a["custom_4_exist"].ToString());
            str = str.Replace("##custom_5_exist##", vars.a["custom_5_exist"].ToString());
            str = str.Replace("##custom_6_exist##", vars.a["custom_6_exist"].ToString());
            str = str.Replace("##custom_7_exist##", vars.a["custom_7_exist"].ToString());
            str = str.Replace("##custom_8_exist##", vars.a["custom_8_exist"].ToString());
            str = str.Replace("##custom_9_exist##", vars.a["custom_9_exist"].ToString());
            str = str.Replace("##custom_10_exist##", vars.a["custom_10_exist"].ToString());
            str = str.Replace("##custom_11_exist##", vars.a["custom_11_exist"].ToString());
            str = str.Replace("##custom_12_exist##", vars.a["custom_12_exist"].ToString());
            str = str.Replace("##custom_13_exist##", vars.a["custom_13_exist"].ToString());
            str = str.Replace("##custom_14_exist##", vars.a["custom_14_exist"].ToString());
            str = str.Replace("##custom_15_exist##", vars.a["custom_15_exist"].ToString());
            str = str.Replace("##custom_16_exist##", vars.a["custom_16_exist"].ToString());
            str = str.Replace("##custom_17_exist##", vars.a["custom_17_exist"].ToString());
            str = str.Replace("##custom_18_exist##", vars.a["custom_18_exist"].ToString());
            str = str.Replace("##custom_19_exist##", vars.a["custom_19_exist"].ToString());
            str = str.Replace("##custom_20_exist##", vars.a["custom_20_exist"].ToString());
            str = str.Replace("##summary_exist##", vars.a["summary_exist"].ToString());
            str = str.Replace("##headline_exist##", vars.a["headline_exist"].ToString());
            str = str.Replace("##menu_text_exist##", vars.a["menu_text_exist"].ToString());
            str = str.Replace("##date_1_exist##", vars.a["date_1_exist"].ToString());
            str = str.Replace("##date_2_exist##", vars.a["date_2_exist"].ToString());
            str = str.Replace("##date_3_exist##", vars.a["date_3_exist"].ToString());
            str = str.Replace("##date_4_exist##", vars.a["date_4_exist"].ToString());
            str = str.Replace("##date_5_exist##", vars.a["date_5_exist"].ToString());
            return str;
        }

        private string formatDate(string format, string strDate)
        {
            string defaultMonthNames = "Ocak,Şubat,Mart,Nisan,Mayıs,Haziran,Temmuz,Ağustos,Eylül,Ekim,Kasım,Aralık";
            string defaultDayNames = "Pazar,Pazartesi,Salı,Çarşamba,Perşembe,Cuma,Cumartesi";

            string[] monthNames;
            string[] weekDayNames;

            if (!string.IsNullOrEmpty(GetApplication("CFG_MONTH_NAMES_" + vars.a["lang_id"].ToString())))
                monthNames = ("," + GetApplication("CFG_MONTH_NAMES_" + vars.a["lang_id"].ToString()) + ",").Split(',');
            else
                monthNames = ("," + defaultMonthNames + ",").Split(',');

            if (!string.IsNullOrEmpty(GetApplication("CFG_WEEKDAY_NAMES_" + vars.a["lang_id"].ToString())))
                weekDayNames = ("," + GetApplication("CFG_WEEKDAY_NAMES_" + vars.a["lang_id"].ToString()) + ",").Split(',');
            else
                weekDayNames = ("," + defaultDayNames + ",").Split(',');

            DateTime date = DateTime.Now;
            try
            {
                date = DateTime.Parse(strDate);
            }
            catch
            {
                date = DateTime.Now;
            }

            int dateDay = date.Day;
            int dateMonth = date.Month;
            int dateYear = date.Year;
            int dateHour = date.Hour;
            int dateMinute = date.Minute;
            int dateSecond = date.Second;


            format = format.Replace("%Y", dateYear.ToString());
            format = format.Replace("%y", CmsHelper.Right(dateYear.ToString(), 2));
            format = format.Replace("%m", CmsHelper.Right("0" + dateMonth.ToString(), 2));
            format = format.Replace("%n", dateMonth.ToString());
            format = format.Replace("%F", monthNames[dateMonth]);
            format = format.Replace("%M", monthNames[dateMonth].Substring(0, 3));
            format = format.Replace("%d", CmsHelper.Right("0" + dateDay.ToString(), 2));
            format = format.Replace("%j", dateDay.ToString());
            format = format.Replace("%h", CmsHelper.Right("0" + dateHour.ToString(), 2));
            format = format.Replace("%g", dateHour.ToString());

            string A = string.Empty;
            if (dateHour > 12)
                A = "PM";
            else
                A = "AM";

            format = format.Replace("%A", A);
            format = format.Replace("%a", A.ToLower());

            if (A.Equals("AM"))
                format = format.Replace("%H", ("0" + (dateHour - 12).ToString()).Substring(0, 2));
            format = format.Replace("%H", dateHour.ToString());

            if (A.Equals("PM"))
                format = format.Replace("%G", ("0" + (dateHour - 12).ToString()).Substring(0, 2));
            format = format.Replace("%G", dateHour.ToString());

            format = format.Replace("%i", dateMinute.ToString());
            format = format.Replace("%I", CmsHelper.Right("0" + dateMinute.ToString(), 2));
            format = format.Replace("%s", dateSecond.ToString());
            format = format.Replace("%S", CmsHelper.Right("0" + dateSecond.ToString(), 2));
            //format = format.Replace("%L", dateHour.ToString());
            //format = format.Replace("%D", dateHour.ToString());
            //format = format.Replace("%l", dateHour.ToString());
            //format = format.Replace("%U", dateHour.ToString());
            format = format.Replace("11%O", "11th");
            format = format.Replace("1%O", "1st");
            format = format.Replace("12%O", "12th");
            format = format.Replace("2%O", "2nd");
            format = format.Replace("13%O", "13th");
            format = format.Replace("3%O", "3rd");
            format = format.Replace("%O", "th");

            return format;
        }

        #endregion
        #region dotLeft
        public string dotLeft(string s, int limit, string append)
        {
            string[] arr;
            if (s.Length > limit)
            {

                string ts = CmsHelper.Mid(s, limit + 1);
                arr = ts.Split(' ');
                return CmsHelper.Left(s, limit) + arr[0] + append;
            }
            else
                return s;
        }
        #endregion
        #region getContentLinkAlias
        public string getContentLinkAlias(string zoneId, string articleId, string siteName, string zoneGroupName, string zoneName, string iheadline, string alias)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(alias))
            {
                str = "/" + alias;
            }
            else
            {

                string site_name = "", zone_group_name = "", zone_name = "", headline = "";

                if (siteName != "") site_name = "/" + CmsHelper.c2QS(siteName);
                if (zoneGroupName != "") zone_group_name = "/" + CmsHelper.c2QS(zoneGroupName);
                if (zoneName != "") zone_name = "/" + CmsHelper.c2QS(zoneName);
                if (iheadline != "") headline = "/" + CmsHelper.c2QS(iheadline);

                str = "/" + Constansts.ALIAS_WEB + "/" + zoneId.ToString() + "-" + articleId.ToString() + "-1-1" + site_name + zone_group_name + zone_name + headline;

                str = str.Replace(",", "-");
                str = str.Replace("--", "-");
                str = str.Replace("--", "-");
            }
            return str;
        }
        #endregion
        #region getContentLink
        public string getContentLink(string zoneId, string articleId, string siteName, string zoneGroupName, string zoneName, string iheadline)
        {
            return getContentLinkAlias(zoneId, articleId, siteName, zoneGroupName, zoneName, iheadline, string.Empty);
        }
        #endregion
        #region processAFiles
        public string processAFiles(string html, int articleID)
        {
            bool isFound = gotAFiles(html);
            int loopLimit = 0;
            string result = html;
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replaceAFiles(result, articleID);
                isFound = gotAFiles(result);
            }
            if (string.IsNullOrEmpty(result))
                result = html;
            return result;
        }
        #endregion
        #region replaceAFiles
        public string replaceAFiles(string html, int articleID)
        {
            string af_temp = string.Empty;
            string af_data = string.Empty;
            string[] af_datas;
            string[] af_out;

            string result = html;
            af_temp = regOp("afiles_get_temp", html);
            af_data = af_temp.Replace("##", "");
            af_datas = af_data.Split('_');

            if (af_data.Length >= 2)
            {
                af_out = getAfiles(af_datas[1], articleID);

                if (!string.IsNullOrEmpty(af_out[1]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_1_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[2]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_2_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[3]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_3_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[4]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_4_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[5]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_5_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[6]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_6_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[7]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_7_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[8]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_8_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[9]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_9_exists##", "exists");

                if (!string.IsNullOrEmpty(af_out[10]))
                    html = html.Replace("##afiles_" + af_datas[1] + "_10_exists##", "exists");


                html = html.Replace("##afiles_" + af_datas[1] + "_1##", af_out[1]);
                html = html.Replace("##afiles_" + af_datas[1] + "_2##", af_out[2]);
                html = html.Replace("##afiles_" + af_datas[1] + "_3##", af_out[3]);
                html = html.Replace("##afiles_" + af_datas[1] + "_4##", af_out[4]);
                html = html.Replace("##afiles_" + af_datas[1] + "_5##", af_out[5]);
                html = html.Replace("##afiles_" + af_datas[1] + "_6##", af_out[6]);
                html = html.Replace("##afiles_" + af_datas[1] + "_7##", af_out[7]);
                html = html.Replace("##afiles_" + af_datas[1] + "_8##", af_out[8]);
                html = html.Replace("##afiles_" + af_datas[1] + "_9##", af_out[9]);
                html = html.Replace("##afiles_" + af_datas[1] + "_10##", af_out[10]);
                html = html.Replace("##afiles_" + af_datas[1] + "_1_extension##", getFileExtension(af_out[1]));
                html = html.Replace("##afiles_" + af_datas[1] + "_2_extension##", getFileExtension(af_out[2]));
                html = html.Replace("##afiles_" + af_datas[1] + "_3_extension##", getFileExtension(af_out[3]));
                html = html.Replace("##afiles_" + af_datas[1] + "_4_extension##", getFileExtension(af_out[4]));
                html = html.Replace("##afiles_" + af_datas[1] + "_5_extension##", getFileExtension(af_out[5]));
                html = html.Replace("##afiles_" + af_datas[1] + "_6_extension##", getFileExtension(af_out[6]));
                html = html.Replace("##afiles_" + af_datas[1] + "_7_extension##", getFileExtension(af_out[7]));
                html = html.Replace("##afiles_" + af_datas[1] + "_8_extension##", getFileExtension(af_out[8]));
                html = html.Replace("##afiles_" + af_datas[1] + "_9_extension##", getFileExtension(af_out[9]));
                html = html.Replace("##afiles_" + af_datas[1] + "_10_extension##", getFileExtension(af_out[10]));
                html = html.Replace("##afiles_" + af_datas[1] + "_title##", getFileExtension(af_out[0]));
                html = html.Replace("##afiles_" + af_datas[1] + "_comment##", getFileExtension(af_out[11]));
            }

            html = html.Replace(af_temp, "");

            return html;
        }

        #endregion
        #region getFileExtension
        private string getFileExtension(string filename)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(filename) && filename.IndexOf(".") != -1)
            {
                string[] arr = filename.Split('.');
                result = arr[arr.Length - 1].ToLower();
            }
            else
            {
                result = "unknown";
            }
            return result;
        }
        #endregion
        #region regOp
        public string regOp(string type, string html)
        {
            string result = string.Empty;
            switch (type)
            {
                case "menu_get_temp":
                    result = CmsHelper.RegexGet(html, "<img (.*)alt=\"\x23\x23menu_(.)* />", true);
                    break;
                case "menu_get_id":
                    result = CmsHelper.RegexReplace(html, @"(.)*\x23\x23menu_([a-zA-Z]{1,5})##(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_exclude":
                    result = CmsHelper.RegexReplace(html, @"(.)* exclude=\x22([0-9,]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_include":
                    result = CmsHelper.RegexReplace(html, @"(.)* include=\x22([0-9,]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "  ":
                    result = CmsHelper.RegexGet(html, "<img (.*)alt=\"\x23\x23sitemap_(.)* />", true);
                    break;
                case "sitemap_get_id":
                    result = CmsHelper.RegexReplace(html, @"(.)*\x23\x23sitemap_([a-zA-Z]{1,5})##(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_data":
                    result = CmsHelper.RegexReplace(html, @"(.)* id=\x22([0-9a-zA-Z\._-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_class":
                    result = CmsHelper.RegexReplace(html, @"(.)*class=\x22([0-9a-zA-Z\._\-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_style":
                    result = CmsHelper.RegexReplace(html, @"(.)*style=\x22([.\w\d\s\x3A\x3B-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_temp":
                    result = CmsHelper.RegexGet(html, "<img (.*)alt=\"\x23\x23tag_(.)* />", true);
                    break;
                case "tag_get_id":
                    result = CmsHelper.RegexReplace(html, "(.)*\x23\x23tag_([0-9]{1,5})##(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_data":
                    result = CmsHelper.RegexReplace(html, @"(.)* id=\x22([0-9\._-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_class":
                    result = CmsHelper.RegexReplace(html, @"(.)*class=\x22([\s0-9a-zA-Z\._:;-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_style":
                    result = CmsHelper.RegexReplace(html, @"(.)*style=\x22([.\w\d\s\x3A\x3B-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_exself":
                    result = CmsHelper.RegexReplace(html, @"(.)* hspace=\x22([0-9]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_container":
                    result = CmsHelper.RegexReplace(html, @"(.)* lang=\x22([a-zA-Z0-9]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_percontainer":
                    result = CmsHelper.RegexReplace(html, @"(.)* percontainer=\x22([a-zA-Z0-9]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "tag_get_seperator":
                    result = CmsHelper.RegexReplace(html, @"(.)* seperator=\x22(([\x00-\x21\x23-xFFığüşöçİĞÜŞÖÇ])*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_temp":
                    result = CmsHelper.RegexGet(html, "<img (.*)alt=\"\x23\x23portlet_(.)* />", true);
                    break;
                case "portlet_option_get_temp":
                    result = CmsHelper.RegexGet(html, "<option (.*)alt=\"\x23\x23portlet_(.)*></option>", true);
                    break;
                case "portlet_get_id":
                    result = CmsHelper.RegexReplace(html, "(.)*\x23\x23portlet_([0-9]{1,5})##(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_data":
                    result = CmsHelper.RegexReplace(html, @"(.)* id=\x22([0-9\._-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_exself":
                    result = CmsHelper.RegexReplace(html, @"(.)* hspace=\x22([0-9]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_container":
                    result = CmsHelper.RegexReplace(html, @"(.)* lang=\x22([a-zA-Z0-9]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_include":
                    result = CmsHelper.RegexReplace(html, @"(.)* include=\x22([0-9,-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_exclude":
                    result = CmsHelper.RegexReplace(html, @"(.)* exclude=\x22([0-9,]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_header":
                    result = CmsHelper.RegexReplace(html, @"(.)* pheader=\x22(([\x00-\x21\x23-xFFığüşöçİĞÜŞÖÇ])*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_pager_count":
                    result = CmsHelper.RegexReplace(html, @"(.)* pagercount=\x22([0-9,]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_pager_position":
                    result = CmsHelper.RegexReplace(html, @"(.)* pagerlocation=\x22([0-9,]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_class":
                    result = CmsHelper.RegexReplace(html, @"(.)*class=\x22([\s0-9a-zA-Z\._:;-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_style":
                    result = CmsHelper.RegexReplace(html, @"(.)*style=\x22([.\w\d\s\x3A\x3B-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_seperator":
                    result = CmsHelper.RegexReplace(html, @"(.)* seperator=\x22(([\x00-\x21\x23-xFF\x7CığüşöçİĞÜŞÖÇ])*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_prevnext":
                    result = CmsHelper.RegexReplace(html, @"(.)* prevnext=\x22(([\x00-\x21\x23-xFF\x7CığüşöçİĞÜŞÖÇ])*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_pagerheader":
                    result = CmsHelper.RegexReplace(html, @"(.)* pagerheader=\x22(([\x00-\x21\x23-xFF\x7CığüşöçİĞÜŞÖÇ])*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "portlet_get_pagerclass":
                    result = CmsHelper.RegexReplace(html, @"(.)* classpager=\x22(([\x00-\x21\x23-xFF\x7CığüşöçİĞÜŞÖÇ])*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "afiles_get_temp":
                    result = CmsHelper.RegexGet(html, "##afiles_([a-zA-Z0-9_-]*)##", false);
                    break;
                case "afiles_get_alias_temp":
                    result = CmsHelper.RegexGet(html, "\x23\x23afiles_(.)*\x23\x23", false);
                    break;
                case "rss_clean_plughtml":
                    result = CmsHelper.RegexReplace(html, "<img (.*)alt=\x22\x23\x23plugin_(.)* />", "", true, true, false, false);
                    break;
                case "rss_clean_menus":
                    result = CmsHelper.RegexReplace(html, "<img (.*)alt=\x22\x23\x23menu_(.)* />", "", true, true, false, false);
                    break;
                case "rss_clean_portlets":
                    result = CmsHelper.RegexReplace(html, "<img (.*)alt=\x22\x23\x23portlet_(.)* />", "", true, true, false, false);
                    break;
                case "rss_clean_afiles":
                    result = CmsHelper.RegexReplace(html, "\x23\x23afiles_(.)*\x23\x23", "", true, true, false, false);
                    break;
                case "process_anchors":
                    result = CmsHelper.RegexReplace(html, "<a name=\x22([a-zA-Z0-9]*)\x22>", "<a name=\"$1\" id=\"$1\">", true, true, false, true);//Strict rules for anchors
                    break;
                case "plugin_get_temp":
                    result = CmsHelper.RegexGet(html, "<img (.*)alt=\x22\x23\x23plugin_(.)* />", true);
                    break;
                case "plugin_get_id":
                    result = CmsHelper.RegexReplace(html, @"(.)*\x23\x23plugin_([0-9]{1,5})##(.)*", "$2", true, true, false, false);
                    break;
                case "plugin_get_param":
                    result = CmsHelper.RegexReplace(html, @"(.)* id=\x22([0-9a-zA-Z,\x3D\x3F\x3A\x2F\x2E\x2D\x5F\x26\x25\x3B\x7E\x7C]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "plugin_get_style":
                    result = CmsHelper.RegexReplace(html, @"(.)*style=\x22([.\w\d\s\x3A\x3B-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "breadcrumb_get_temp":
                    result = CmsHelper.RegexGet(html, @"<img (.*)alt=\""\x23\x23breadcrumb_(.)* />", true);
                    break;
                case "breadcrumb_get_id":
                    result = CmsHelper.RegexReplace(html, @"(.)*\x23\x23breadcrumb_([0-9]{1,5})##(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_container_tagid":
                    result = CmsHelper.RegexReplace(html, @"(.)*menucontainertagid=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_single_item":
                    result = CmsHelper.RegexReplace(html, @"(.)*menusingleitem=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_onclick_function":
                    result = CmsHelper.RegexReplace(html, @"(.)*menuonclickfunction=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_selected_cls":
                    result = CmsHelper.RegexReplace(html, @"(.)*menuselectedcls=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "menu_get_not_selected_cls":
                    result = CmsHelper.RegexReplace(html, @"(.)*menunotselectedcls=\x22([0-9a-zA-Z_\-]*)\x22(.)*", "$2", true, true, false, false);
                    break;
                case "article_get_link":
                    result = CmsHelper.RegexReplace(CmsHelper.RegexGet(html, "(.)*href=\x22(.*)\x22(.)*", true), "(.)*href=\x22(.*)\x22(.)*", "$2", true, true, false, true);
                    break;
                case "article_js_replace":
                    result = CmsHelper.RegexReplace(html, @"\[script.*src=\x22(.*?)\x22.*\]\[\x2Fscript\]", "<script type=\"text/javascript\" src=\"$1\"></script>", true, true, false, true);
                    break;
                case "article_cleanup_comments":
                    result = CmsHelper.RegexReplace(html, @"<!--(?!\[)(.|.*\n|.*\r\n)*?-->", "", true, true, true, false);
                    break;
                case "article_cleanup_empty_lines":
                    result = CmsHelper.RegexReplace(html, @"^[ \t]*$\r?\n", "", true, true, true, false);
                    break;
                case "article_cleanup_tabs_spaces":
                    result = CmsHelper.RegexReplace(html, @"^[ \t]+|[ \t]+$", "", true, true, true, false);
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }
        #endregion
        #region getAfiles
        public string[] getAfiles(string alias, int articleID)
        {
            string[] fReturn = new string[15];
            DataTable dt = Dal.Instance.SelectGetAFilesData(alias, articleID);
            if (dt.Rows.Count > 0)
            {
                string file_title = dt.Rows[0][0].ToString();
                string file_comment = dt.Rows[0][1].ToString();
                string file_name_1 = dt.Rows[0][2].ToString();
                string file_name_2 = dt.Rows[0][3].ToString();
                string file_name_3 = dt.Rows[0][4].ToString();
                string file_name_4 = dt.Rows[0][5].ToString();
                string file_name_5 = dt.Rows[0][6].ToString();
                string file_name_6 = dt.Rows[0][7].ToString();
                string file_name_7 = dt.Rows[0][8].ToString();
                string file_name_8 = dt.Rows[0][9].ToString();
                string file_name_9 = dt.Rows[0][10].ToString();
                string file_name_10 = dt.Rows[0][11].ToString();

                if (!string.IsNullOrEmpty(file_name_1))
                    fReturn[1] = "/i/content/" + articleID + "_" + file_name_1;

                if (!string.IsNullOrEmpty(file_name_2))
                    fReturn[2] = "/i/content/" + articleID + "_" + file_name_2;

                if (!string.IsNullOrEmpty(file_name_3))
                    fReturn[3] = "/i/content/" + articleID + "_" + file_name_3;

                if (!string.IsNullOrEmpty(file_name_4))
                    fReturn[4] = "/i/content/" + articleID + "_" + file_name_4;

                if (!string.IsNullOrEmpty(file_name_5))
                    fReturn[5] = "/i/content/" + articleID + "_" + file_name_5;

                if (!string.IsNullOrEmpty(file_name_6))
                    fReturn[6] = "/i/content/" + articleID + "_" + file_name_6;

                if (!string.IsNullOrEmpty(file_name_7))
                    fReturn[7] = "/i/content/" + articleID + "_" + file_name_7;

                if (!string.IsNullOrEmpty(file_name_8))
                    fReturn[8] = "/i/content/" + articleID + "_" + file_name_8;

                if (!string.IsNullOrEmpty(file_name_9))
                    fReturn[9] = "/i/content/" + articleID + "_" + file_name_9;

                if (!string.IsNullOrEmpty(file_name_10))
                    fReturn[10] = "/i/content/" + articleID + "_" + file_name_10;


                fReturn[0] = file_title;
                fReturn[11] = file_comment;
            }
            return fReturn;
        }

        #endregion
        #region SetFormValue
        protected void SetFormValue(string key, string value)
        {
            var collection = HttpContext.Current.Request.Form;

            // Get the "IsReadOnly" protected instance property.
            var propInfo = collection.GetType().GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

            // Mark the collection as NOT "IsReadOnly"
            propInfo.SetValue(collection, false, new object[] { });

            // Change the value of the key.
            collection[key] = value;

            // Mark the collection back as "IsReadOnly"     
            propInfo.SetValue(collection, true, new object[] { });
        }
        #endregion
        #endregion
        #endregion

        #region OldCodes
        #region processTags
        private string processTags(string template, int article_id)
        {
            string result = template;
            string valid_tags = ",ul,li,div,span,a,";
            string tag_count = string.Empty;
            string tag_order = string.Empty;
            string tag_zg = string.Empty;
            int tag_order_ = 0;
            string tag_dzg = string.Empty;
            bool tag_dzg_ = false;
            string ozg_name = string.Empty;
            string tag_link = string.Empty;
            string tag_out = string.Empty;
            int tag_exself_;

            if (result.Contains("##tag_"))
            {
                string tag_temp = regOp("tag_get_temp", result);
                string tag_id = regOp("tag_get_id", tag_temp);
                string tag_data = regOp("tag_get_data", tag_temp);

                string tag_class = string.Empty;
                string tag_style = string.Empty;
                string tag_exself = string.Empty;
                string tag_container = string.Empty;
                string tag_percontainer = string.Empty;
                string tag_seperator = string.Empty;

                if (tag_temp.Contains("class="))
                    tag_class = regOp("tag_get_class", tag_temp);
                if (tag_temp.Contains("style="))
                    tag_style = regOp("tag_get_style", tag_temp);
                if (tag_temp.Contains("hspace="))
                    tag_exself = regOp("tag_get_exself", tag_temp);
                if (tag_temp.Contains("lang="))
                    tag_container = regOp("tag_get_container", tag_temp);
                if (tag_temp.Contains("percontainer="))
                    tag_percontainer = regOp("tag_get_percontainer", tag_temp);
                if (tag_temp.Contains("seperator="))
                    tag_seperator = regOp("tag_get_seperator", tag_temp);

                if (string.IsNullOrEmpty(tag_container) || valid_tags.IndexOf("," + tag_container + ",") == -1)
                    tag_container = "ul";

                if (string.IsNullOrEmpty(tag_percontainer) || valid_tags.IndexOf("," + tag_percontainer + ",") == -1)
                    tag_percontainer = "li";

                if (tag_exself.Equals("1"))
                    tag_exself_ = 1;
                else
                    tag_exself_ = 0;

                tag_seperator = HttpUtility.UrlDecode(tag_seperator);

                string[] tag_datas = tag_data.Split('_');
                if (tag_datas.Length > 0)
                    tag_count = tag_datas[1];
                if (tag_datas.Length > 1)
                    tag_order = tag_datas[2];
                if (tag_datas.Length > 2)
                    tag_dzg = tag_datas[3];

                if (CmsHelper.IsNumeric(tag_count))
                    tag_count = " top " + tag_count;
                else
                    tag_count = "";

                if (CmsHelper.IsNumeric(tag_order))
                    tag_order_ = Convert.ToInt32(tag_order);
                else
                    tag_order_ = 0;

                if (tag_dzg == "1")
                    tag_dzg_ = true;
                else
                    tag_dzg_ = false;

                string strSQL = "" +
                      "select " + tag_count + " cz.zone_id, zg.zone_group_name, zg.zone_group_id, cz.zone_name, zg.tag_detail_article as zg_tag_detail_article, s.tag_detail_article as s_tag_detail_article " +
                      "from dbo.cms_article_zones az with (nolock), dbo.cms_zones cz with (nolock), dbo.cms_zone_groups zg with (nolock), dbo.cms_sites s with (nolock) " +
                      "where " +
                      "	cz.zone_id = az.zone_id " +
                      "	and zg.zone_group_id = cz.zone_group_id " +
                      "	and s.site_id = zg.site_id " +
                      "	and az.article_id = " + article_id.ToString() + " " +
                      "	and cz.zone_type_id = 2 " +
                      "order by zg.zone_group_name asc, ";

                switch (tag_order_)
                {
                    case 0:
                        strSQL = strSQL + " az.rel_id asc";
                        break;
                    case 1:
                        strSQL = strSQL + " cz.zone_name asc";
                        break;
                    case 2:
                        strSQL = strSQL + " cz.zone_name desc";
                        break;
                    case 3:
                        strSQL = strSQL + " cz.zone_order asc";
                        break;
                    case 4:
                        strSQL = strSQL + " cz.zone_order desc";
                        break;
                }

                DataTable dt = DbHelper.ExecuteSQLString(strSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tag_id = dt.Rows[i][0].ToString();
                        string zg_name = dt.Rows[i][1].ToString();
                        string zg_id = dt.Rows[i][2].ToString();
                        string tag_name = dt.Rows[i][3].ToString();
                        string zg_aid = dt.Rows[i][4].ToString();
                        string s_aid = dt.Rows[i][5].ToString();

                        // Zone Group tag view settings override
                        if (CmsHelper.IsNumeric(zg_aid.Replace("-", "")) && zg_aid.IndexOf("-") != -1)
                        {
                            s_aid = zg_aid;
                        }

                        // Display only if valid tag view settings
                        if (CmsHelper.IsNumeric(s_aid.Replace("-", "")) && s_aid.IndexOf("-") != -1)
                        {
                            string[] azid = s_aid.Split('-');

                            if (tag_dzg_ && ozg_name != zg_name)
                            {
                                tag_link = "/" + Constansts.ALIAS_CONTENT + "/" + azid[0] + "-" + azid[1] + "-1-1-_" + zg_id + "/" + CmsHelper.c2QS(zg_name) + "/";
                                tag_out = tag_out + "<" + tag_percontainer + "><a hidefocus href=\"" + tag_link + "\">" + zg_name;
                                if (!string.IsNullOrEmpty(tag_seperator))
                                    tag_out = tag_out + tag_seperator;
                                tag_out = tag_out + "</" + tag_percontainer + ">" + Environment.NewLine;
                            }

                            tag_link = "/" + Constansts.ALIAS_CONTENT + "/" + azid[0] + "-" + azid[1] + "-1-1-" + tag_id + "/" + CmsHelper.c2QS(tag_name) + "/";
                            tag_out = tag_out + "<" + tag_percontainer + "><a hidefocus href=\"" + tag_link + "\">" + tag_name;
                            if (i < dt.Rows.Count && !string.IsNullOrEmpty(tag_seperator))
                                tag_out = tag_out + tag_seperator;
                            tag_out = tag_out + "</" + tag_percontainer + ">" + Environment.NewLine;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(tag_class))
                    tag_class = " class=\"" + tag_class + "\" ";
                if (!string.IsNullOrEmpty(tag_style))
                    tag_style = " style=\"" + tag_style + "\" ";

                result = result.Replace(tag_temp, "<" + tag_container + tag_class + tag_style + ">" + Environment.NewLine + tag_out + Environment.NewLine + "</" + tag_container + ">" + Environment.NewLine);
            }

            return result;
        }
        #endregion
        #region processPlugins
        private string processPlugins(string template)
        {
            string result = template;
            bool isFound = gotPlugin(template);
            int loopLimit = 0;
            while (loopLimit < 30 && isFound == true)
            {
                loopLimit++;
                result = replacePlugins(result);
                isFound = gotPlugin(result);
            }
            return result;
        }
        #endregion
        #region replacePlugins
        private string replacePlugins(string template)
        {
            string result = template;
            string plugin_temp = regOp("plugin_get_temp", result);
            string plugin_id = regOp("plugin_get_id", result);
            string plugin_param = regOp("plugin_get_param", result);
            string plugin_style = string.Empty;
            string plugin_code = string.Empty;
            string plugin_out = string.Empty;

            DateTime started = DateTime.Now;

            if (plugin_temp.Contains("style"))
            {
                plugin_style = regOp("plugin_get_style", result);
            }

            if (CmsHelper.IsNumeric(plugin_id))
            {
                plugin_code = getPluginCode(Convert.ToInt32(plugin_id), plugin_param);

                //// add to current request collection
                //foreach (KeyValuePair<string, object> f in vars.a)
                //    SetFormValue(f.Key, f.Value.ToString());

                //// add to current request 
                //SetFormValue("plugin_code", plugin_code);

                //StringWriter sw = new StringWriter();
                //Server.Execute("plugin_executer.asp", sw, true);
                //plugin_out = sw.ToString();

                //Dictionary<string, string> dic = new Dictionary<string,string>();
                //foreach (KeyValuePair<string, object> f in vars.a)
                //    dic.Add(f.Key, f.Value.ToString());

                //string endpoint = GetHttpPrefix() + HttpContext.Current.Request.Url.Host + "/plugin_executer.asp";

                //plugin_out = RequestHelper.SendPost(endpoint, dic); 
                // to-do

                DateTime ended = DateTime.Now;
                TimeSpan ts = ended - started;

                plugin_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                if (!string.IsNullOrEmpty(plugin_style.Trim()))
                {
                    result = result.Replace(plugin_temp, "<div style=\"" + plugin_style + "\">" + Environment.NewLine + plugin_out + Environment.NewLine + "</div>" + Environment.NewLine);
                }
                else
                {
                    result = result.Replace(plugin_temp, plugin_out);
                }
            }
            return result;
        }
        #endregion
        #region checkAutoDailyReloadCache
        public void checkAutoDailyReloadCache()
        {
            bool reload_cache = false;
            string AutoDailyReloadCache = GetApplication(Constansts.CFG_AUTO_DAILY_RELOAD_CACHE);
            if (AutoDailyReloadCache.Equals("Y"))
            {
                if (GetApplication(Constansts.CFG_AUTO_DAILY_RELOAD_CACHE_LAST_RELOAD).Equals("") ||
                    !CmsHelper.IsDate2(GetApplication(Constansts.CFG_AUTO_DAILY_RELOAD_CACHE_LAST_RELOAD)))
                {
                    reload_cache = true;
                }
                else
                {
                    if (Convert.ToDateTime(GetApplication(Constansts.CFG_AUTO_DAILY_RELOAD_CACHE_LAST_RELOAD)) != DateTime.Today)
                    {
                        reload_cache = true;
                    }
                }

                if (reload_cache)
                {
                    // Triggering IIS Servers to reload their local cache
                    Dal.Instance.CacheUpdateUpdateStatus("", 1, 100);
                    SetApplication(Constansts.CFG_AUTO_DAILY_RELOAD_CACHE_LAST_RELOAD, DateTime.Today.ToString());
                    // add2Log 1, 0, "0", "DAILY_AUTO_RELOAD_CACHE", "Time: " & now()
                    add2Log(1, 0, "0", "DAILY_AUTO_RELOAD_CACHE", string.Format("Time: {0:d}", DateTime.Now));
                }
            }
        }
        #endregion
        #region processSTFForm
        private string processSTFForm(string template, string stft_id, int article_id, int zone_id, string wh, string stf_omniture_function)
        {
            string result = template;
            string RJS = string.Empty;

            if (result.Contains("<form"))
            {
                result = "<form name=\"stf_form\" id=\"stf_form\" method=\"post\" action=\"javascript:void(0);\" onSubmit=\"return false;\"><input type=\"hidden\" name=\"id\" value=\"" + stft_id + "," + zone_id + "," + article_id + "\">" + Environment.NewLine + result + Environment.NewLine + "</form><div align=\"center\" id=\"stf_loader\"></div>" + Environment.NewLine;
            }

            // Corrections

            result = result.Replace("\"sendSTF();\"", "return sendSTF(this),false;");
            result = result.Replace("\"return sendSTF();\"", "return sendSTF(this),false;");
            result = result.Replace("sendSTF()", "sendSTF(this)");
            result = result.Replace("##stf_submit##", "javascript:void(0);\" onclick=\"return sendSTF(this),false;");

            if (!string.IsNullOrEmpty(stf_omniture_function))
                result = result.Replace("sendSTF(this)", "sendSTF(this)," + stf_omniture_function);

            result = result.Replace("sendSTF(this)", "sendSTF(this,'" + vars.a["lang_id"].ToString() + "')");

            if (!string.IsNullOrEmpty(wh))
            {
                string[] wha = wh.Split('x');
                if (wha.Length == 2)
                {
                    if (CmsHelper.IsNumeric(wha[0]) && CmsHelper.IsNumeric(wha[1]))
                    {
                        RJS = "resizeSTF(" + wha[0] + "," + wha[1] + ");";
                    }
                }
            }

            result += Environment.NewLine + rjsC(RJS + "exchange('pl',true);");
            return result;
        }

        #endregion
        #region pruneBBCode
        public string pruneBBCode(string s)
        {
            string tmp = s;
            tmp = tmp.Replace("[br]", "");
            tmp = tmp.Replace("[b]", "");
            tmp = tmp.Replace("[i]", "");
            tmp = tmp.Replace("[strong]", "");
            tmp = tmp.Replace("[em]", "");
            tmp = tmp.Replace("[/b]", "");
            tmp = tmp.Replace("[/i]", "");
            tmp = tmp.Replace("[/strong]", "");
            tmp = tmp.Replace("[/em]", "");
            return tmp;
        }
        #endregion
        #region replaceBBCode
        public string replaceBBCode(string s)
        {
            string tmp = s;
            tmp = tmp.Replace("[br]", "<br/>");
            tmp = tmp.Replace("[b]", "<b>");
            tmp = tmp.Replace("[i]", "<i>");
            tmp = tmp.Replace("[strong]", "<strong>");
            tmp = tmp.Replace("[em]", "<em>");
            tmp = tmp.Replace("[/b]", "</b>");
            tmp = tmp.Replace("[/i]", "</i>");
            tmp = tmp.Replace("[/strong]", "</strong>");
            tmp = tmp.Replace("[/em]", "</em>");
            return tmp;
        }
        #endregion
        #region processAnchors
        private string processAnchors(string template)
        {
            string result = template;
            if (result.Contains("href=\"#\""))
                result = result.Replace("href=\"#\"", "href=\"" + HttpUtility.UrlDecode(CmsHelper.GetQS()) + "#\"");
            return result;
        }
        #endregion
        #region GatherHiddenValues
        public void GatherHiddenValues()
        {
            string[] hiddenValues;
            string[] hiddenTypes;
            string hiddenValue = string.Empty;
            string hiddenType = string.Empty;
            string executer = string.Empty;

            string values = GetApplication(Constansts.HIDDEN_VALUES_VALUES);
            string types = GetApplication(Constansts.HIDDEN_VALUES_TYPES);

            if (!string.IsNullOrEmpty(values) && !string.IsNullOrEmpty(types))
            {
                hiddenValues = values.Split(new string[] { "{|}" }, StringSplitOptions.None);
                hiddenTypes = types.Split(new string[] { "{|}" }, StringSplitOptions.None);

                if (hiddenValues.Length == hiddenTypes.Length)
                {
                    for (int i = 0; i < hiddenValues.Length - 1; i++)
                    {
                        hiddenValue = hiddenValues[i];
                        hiddenType = hiddenTypes[i];
                        switch (hiddenType)
                        {
                            case "0":
                                string queryStrings = CmsHelper.GetQS();
                                int startIndex = queryStrings.IndexOf(hiddenValue + "=");
                                int endIndex = 0;
                                int len = 0;
                                if (startIndex != -1)
                                {
                                    endIndex = queryStrings.IndexOf("&", startIndex);
                                    if (endIndex != -1)
                                    {
                                        executer = queryStrings.Substring(startIndex, endIndex - startIndex);
                                    }
                                    else
                                    {
                                        executer = queryStrings.Substring(startIndex);
                                    }

                                    len = hiddenValue.Length + 1;

                                    if (executer.Length > len)
                                    {
                                        executer = executer.Substring(len);
                                    }
                                    else
                                    {
                                        executer = "";
                                    }
                                }
                                break;
                            case "1":
                                executer = GetCookieValue("EUROCMS", "EUROCMS_" + hiddenValue);
                                break;
                            case "2":
                                executer = GetSession("EUROCMS_" + hiddenValue);
                                break;
                        }
                        if (!string.IsNullOrEmpty(executer))
                        {
                            if (hiddenType.Equals("0"))
                            {
                                SetSession("EUROCMS_QS_" + hiddenValue, executer);
                            }
                            vars.h.Add("HIDDEN_VALUE_" + hiddenValue, executer);
                        }
                    }
                }
            }
        }
        #endregion
        #region ReplaceHiddenValues
        public string ReplaceHiddenValues(string s)
        {
            string[] hiddenValues;
            string[] hiddenTypes;
            string hiddenValue = string.Empty;
            string hiddenType = string.Empty;
            string executer = string.Empty;
            string hiddenResult = string.Empty;
            string temp = s;

            string values = GetApplication("HIDDEN_VALUES_VALUES");
            string types = GetApplication("HIDDEN_VALUES_TYPES");

            if (!string.IsNullOrEmpty(values) && !string.IsNullOrEmpty(types))
            {
                hiddenValues = values.Split(new string[] { "{|}" }, StringSplitOptions.None);
                hiddenTypes = types.Split(new string[] { "{|}" }, StringSplitOptions.None);

                if (hiddenValues.Length == hiddenTypes.Length)
                {
                    for (int i = 0; i < hiddenValues.Length - 1; i++)
                    {
                        hiddenValue = hiddenValues[i];
                        hiddenType = hiddenTypes[i];

                        if (vars.h.ContainsKey("HIDDEN_VALUE_" + hiddenValue))
                            hiddenResult = vars.h["HIDDEN_VALUE_" + hiddenValue].ToString();

                        if (Convert.ToInt32(hiddenType) == 0 && string.IsNullOrEmpty(hiddenResult))
                            hiddenResult = GetSession("EUROCMS_QS_" + hiddenValue);

                        temp = temp.Replace("##EUROCMS_" + hiddenValue + "##", hiddenResult);
                    }
                }
            }

            return temp;
        }
        #endregion
        #region renderPlugin
        public void renderPlugin(string[] QS)
        {
            string pluginName = string.Empty;
            string pluginQS = string.Empty;
            if (QS.Length > 1)
            {
                if (QS[1].IndexOf("?") != -1)
                {
                    pluginName = QS[1].Split('?')[0];
                    pluginQS = QS[1].Split('?')[1];
                }
                else if (QS.Length > 2)
                {
                    pluginName = QS[1];
                    pluginQS = QS[2];
                }
                else
                {
                    pluginName = QS[1];
                }

                string pluginPath = String.Format("p/{0}.asp", pluginName);
                string pluginPhysicalPath = HttpContext.Current.Server.MapPath(pluginPath);
                if (File.Exists(pluginPhysicalPath))
                {
                    HttpContext.Current.Server.TransferRequest(pluginPath + "?" + pluginQS);
                }
                else
                    renderHome(string.Empty);
            }
            else
                renderHome(string.Empty);
        }
        #endregion
        #region do_ClearOldCachedPages
        private void do_ClearOldCachedPages()
        {
            bool start_process = false;
            string cpc = GetApplication(Constansts.CACHED_PAGES_COUNT);
            string ccps = GetApplication(Constansts.CACHE_CLEAR_PROCESS_START);
            int CACHED_PAGES_UPDATE_TIMES_COUNT = 0;

            if (CmsHelper.IsNumeric(cpc))
            {
                int cached_pages_count = Convert.ToInt32(cpc);
                if (cached_pages_count >= Constansts.MAX_CACHED_PAGES_COUNT)
                {
                    if (!CmsHelper.IsDate2(ccps))
                    {
                        start_process = true;
                    }
                    else if (DateTime.Parse(ccps).AddMinutes(Constansts.CACHE_CLEAR_TIMEOUT) <= DateTime.Now)
                    {

                    }
                    else { start_process = false; }

                    if (start_process)
                    {
                        SetApplication(Constansts.CACHE_CLEAR_PROCESS_START, DateTime.Now.ToString());

                        int i = 0;
                        HttpContext.Current.Application.Lock();
                        foreach (String key in HttpContext.Current.Application.Contents.AllKeys)
                        {
                            if (i < Constansts.MAX_CACHED_PAGES_COUNT)
                            {
                                if (key.Contains("PAGE_UT_") && !string.IsNullOrEmpty(GetApplication(key)))
                                {
                                    vars.CACHED_PAGES_UPDATE_TIMES.Values[i] = GetApplication(key);
                                    vars.CACHED_PAGES_UPDATE_TIMES.Keys[i] = "PAGE_" + key.Substring(9);
                                    i++;
                                }
                            }
                            else
                                break;
                        }
                        HttpContext.Current.Application.UnLock();

                        CACHED_PAGES_UPDATE_TIMES_COUNT = vars.CACHED_PAGES_UPDATE_TIMES.Count;


                        if (CACHED_PAGES_UPDATE_TIMES_COUNT > Constansts.MAX_CLEARABLE_ARTICLE_COUNT)
                        {
                            for (int j = 0; j <= Constansts.MAX_CLEARABLE_ARTICLE_COUNT; j++)
                            {
                                string k = GetApplication(vars.CACHED_PAGES_UPDATE_TIMES.Keys[j]);
                                if (!string.IsNullOrEmpty(k))
                                {
                                    HttpContext.Current.Application.Contents.Remove(k);
                                    HttpContext.Current.Application.Contents.Remove("PAGE_UT_" + k.Substring(6));
                                    HttpContext.Current.Application.Lock();
                                    SetApplication(Constansts.CACHED_PAGES_COUNT, (Convert.ToInt32(GetApplication(Constansts.CACHED_PAGES_COUNT)) - 1).ToString());
                                    HttpContext.Current.Application.UnLock();
                                }
                            }
                        }

                        HttpContext.Current.Application.Contents.Remove(Constansts.CACHE_CLEAR_PROCESS_START);

                    }
                }
            }
        }
        #endregion
        #region sendSTF
        public void sendSTF()
        {
            // to-do    
        }
        #endregion
        #endregion

        bool IsManager()
        {
            return HttpContext.Current.Request.IsAuthenticated
                 && Roles.IsUserInRole("Administrator")
                 || Roles.IsUserInRole("Editor")
                 || Roles.IsUserInRole("ContentManager")
                 || Roles.IsUserInRole("ContentEntry")
                 || Roles.IsUserInRole("UserCreator");
        }
    }
}
