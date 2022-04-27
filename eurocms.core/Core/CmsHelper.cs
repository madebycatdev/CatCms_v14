using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using EuroCMS.Data;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Linq;
using System.Data;
using System.Web.Security;
using System.Web.Caching;
using System.Configuration;
using System.Web.UI;
using EuroCMS.Model;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace EuroCMS.Core
{

    public class CmsHelper
    {


        public static string dependecyFile = ConfigurationManager.AppSettings["EuroCMS.CacheDependecyFile"] ?? "";
        #region RedimBreadCrumbItems
        public static void RedimBreadCrumbItems()
        {
            GlobalVars vars = new GlobalVars();
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
        public static GlobalVars getBreadCrumb(int articleID, int zoneID, GlobalVars vars)
        {

            //GlobalVars vars = new GlobalVars();
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
                getBreadCrumb(0, Convert.ToInt32(navigation_zone_id), vars);
            }

            return vars;

        }
        #endregion
        #region getStructureDefaultLink
        public static string getStructureDefaultLink(string p, string zone_default_article)
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

        #region trimBreadCrumbSeperator
        public static string trimBreadCrumbSeperator(string breadcrumb_final, string breadcrumb_seperator)
        {
            if (CmsHelper.Right(breadcrumb_final, 11 + breadcrumb_seperator.Length).Equals("<li>" + breadcrumb_seperator + "</li>" + Environment.NewLine))
            {
                return breadcrumb_final.Substring(breadcrumb_final.Length - (11 + breadcrumb_seperator.Length));
            }
            else
                return breadcrumb_final;
        }
        #endregion

        #region pruneBBCode
        public static string pruneBBCode(string s)
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
        public static string replaceBBCode(string s)
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
        #region SetApplication
        public static void SetApplication(string key, string value)
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

        #region getRandomChars (Tested)
        public static string getRandomChars(int hane)
        {
            return CmsHelper.RandomPassword.Generate(hane, false);
        }
        #endregion

        #region getAfiles
        public static string[] getAfiles(string alias, int articleID)
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


                #region File Titles

                #endregion


                fReturn[0] = file_title;
                fReturn[11] = file_comment;
            }
            return fReturn;
        }

        #endregion

        #region dotLeft
        public static string dotLeft(string s, int limit, string append)
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

        #region replacePortlets
        public static string replacePortlets(string template, int article_id, int zone_id)
        {
            string result = template;
            DateTime started = DateTime.Now;
            bool optioned = false;
            GlobalVars vars = new GlobalVars();
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
                        pager_html = CmsHelper.getPortletPagerHTML(portlet_temp, vars.pPageCurrent, vars.pPageCount, vars.page_start, vars.page_end, portlet_class, vars.pRecordsCount);

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
        public static bool gotAFiles(string html)
        {
            return (html.IndexOf("##afiles_") != -1);
        }

        protected static bool IsManager()
        {
            return HttpContext.Current.Request.IsAuthenticated
                 && Roles.IsUserInRole("Administrator")
                 || Roles.IsUserInRole("PowerUser")
                 || Roles.IsUserInRole("Editor")
                 || Roles.IsUserInRole("Autor")
                 || Roles.IsUserInRole("ContentManager")
                 || Roles.IsUserInRole("ContentEntry")
                 || Roles.IsUserInRole("UserCreator");
        }

        #region processAFiles
        public static string processAFiles(string html, int articleID)
        {
            bool isFound = CmsHelper.gotAFiles(html);
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
        public static string replaceAFiles(string html, int articleID)
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
                af_out = CmsHelper.getAfiles(af_datas[1], articleID);

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

                html = html.Replace("##afiles_" + af_datas[1] + "_title##", af_out[0]);
                html = html.Replace("##afiles_" + af_datas[1] + "_comment##", af_out[11]);

                #region Titles & Comments
                CmsDbContext dbContext = new CmsDbContext();
                string fileAlias = af_datas[1];
                FileType cType = dbContext.FileTypes.Where(x => x.Alias == fileAlias).FirstOrDefault();
                int fTypeId = -1;
                if (cType != null)
                {
                    fTypeId = cType.ID;
                }
                List<ArticleFile> files = dbContext.Files.Where(x => x.ArticleId == articleID && x.FileTypeId == fTypeId).ToList();

                for (int i = 1; i < files.Count; i++)
                {
                    if (files[i] != null)
                    {
                        html = html.Replace("##afiles_" + af_datas[1] + "_" + (i + 1) + "_title##", files[i].Title);
                        html = html.Replace("##afiles_" + af_datas[1] + "_" + (i + 1) + "_title##", files[i].Comment);
                    }
                }
                #endregion
            }

            if (af_temp.Length > 0)
            {
                html = html.Replace(af_temp, "");
            }

            return html;
        }

        #endregion

        #region  replaceArticleDetailsRows
        public static string replaceArticleDetailsRows(DataRow dr, string rsNames, string html, string zoneOw)
        {
            CmsDbContext dbContext = new CmsDbContext();
            string str = string.Empty;
            string itName = string.Empty;
            string customComboLalel = String.Empty;
            string customComboValue = string.Empty;
            str = html;
            GlobalVars vars = new GlobalVars();
            string[] names = rsNames.Split(',');

            foreach (string s in names)
            {
                itName = s;
                object val = null;

                if (dr[itName] != DBNull.Value)
                {
                    val = dr[itName];
                    vars.a[itName] = val;
                }
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
                    case "summary":
                        vars.a["summary"] = strVal;
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
                                if (Convert.ToInt32(regex_params) > 0 && tmpVal.Length > Convert.ToInt32(regex_params))
                                {
                                    //str = str.Replace("##" + itName + "," + regex_params + "##", dotLeft(tmpVal, Convert.ToInt32(regex_params), ""));
                                    str = str.Replace("##" + itName + "," + regex_params + "##", tmpVal.Substring(0, Convert.ToInt32(regex_params)));
                                }
                                else
                                {
                                    str = str.Replace("##" + itName + "," + regex_params + "##", tmpVal);
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
                            strVal = Regex.Replace(strVal, @"\t|\n|\r", "");
                            var clsf_combo_text = dbContext.ClassificationComboValues.FirstOrDefault(f => f.ComboValue == strVal);
                            if (clsf_combo_text != null)
                            {
                                str = str.Replace("##" + itName + "_text##", clsf_combo_text.ComboLabel);
                            }

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


            // Article Detail URL - Alias Control

            int articleId = Convert.ToInt32(vars.a["article_id"]);
            string azAlias = dbContext.ArticleZones.Where(az => az.ArticleID == articleId).FirstOrDefault().AzAlias;
            string articleDetailUrl = CmsHelper.getContentLinkAlias(vars.a["zone_id"].ToString(), vars.a["article_id"].ToString(), vars.a["site_name"].ToString(), vars.a["zone_group_name"].ToString(), vars.a["zone_name"].ToString(), vars.a["headline"].ToString(), "");
            articleDetailUrl = (string.IsNullOrEmpty(azAlias) ? articleDetailUrl : azAlias);
            articleDetailUrl = (articleDetailUrl.Substring(0, 1) == "/" ? articleDetailUrl.Substring(1, articleDetailUrl.Length - 1) : articleDetailUrl);
            vars.a["article_detail_url"] = articleDetailUrl;
            HttpContext.Current.Session["newArticleLink"] = vars.a["article_detail_url"];
            // Article Detail URL - Alias Control

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
            HttpContext.Current.Session["newArticleDetails"] = vars.a;
            return str;
        }

        private static string formatDate(string format, string strDate)
        {
            GlobalVars vars = new GlobalVars();
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
            format = format.Replace("%l", weekDayNames[(int)date.DayOfWeek]);
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

        #region getPortletMultiClassName
        private static string getPortletMultiClassName(string[] inArr, int inNo, string inDT)
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
        // Normal Zone List


        #region getFileExtension
        private static string getFileExtension(string filename)
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

        #region getRecordsetNames (Tested)
        public static string getRecordsetNames(DataTable dt)
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
        #region calculatePagings
        public static void calculatePagings(int inArrayCount, int itemCount, int inCurrentPage, int pageCount)
        {
            GlobalVars vars = new GlobalVars();

            if (itemCount > 0)
            {
                vars.pPageCurrent = inCurrentPage;
                vars.pRecordsCount = inArrayCount + 1;
                vars.pPageCount = (int)Math.Ceiling((double)vars.pRecordsCount / itemCount);

                if (vars.pPageCurrent > vars.pPageCount)
                    vars.pPageCurrent = vars.pPageCount;

                if (vars.pRecordsCount > 0)
                {
                    vars.page_start = (vars.pPageCurrent - 1) - itemCount;
                    vars.page_end = vars.page_start + itemCount - 1;
                }
                else
                {
                    vars.page_start = 0;
                    vars.page_end = 0;
                }

                if (vars.pPageCurrent == 1)
                {
                    vars.page_start = 0;
                }

                if (vars.page_end > inArrayCount)
                {
                    vars.page_end = inArrayCount;
                }
            }
            else
            {
                vars.page_start = 0;
                vars.page_end = inArrayCount;

            }

            HttpContext.Current.Session["page_start"] = itemCount * (inCurrentPage - 1);
            HttpContext.Current.Session["page_end"] = inArrayCount;
            HttpContext.Current.Session["page_count"] = vars.pPageCount;


            // New Calculate Start
            if (pageCount > 0)
            {
                int totalItemCount = (itemCount > 0 && itemCount <= inArrayCount ? itemCount : inArrayCount);
                int iPageCount = pageCount;
                int pageStart = 0;
                int pageEnd = 0;
                int iPagerCount = 0;

                iPagerCount = (int)Math.Ceiling(Convert.ToDouble(totalItemCount) / Convert.ToDouble(pageCount));

                if (inCurrentPage <= 0)
                {
                    inCurrentPage = 1;
                }

                if (inCurrentPage > iPagerCount)
                {
                    inCurrentPage = iPagerCount;
                }

                if (inCurrentPage > 1)
                {
                    pageStart = ((inCurrentPage - 1) * pageCount);
                    pageEnd = pageStart + pageCount;
                }
                else if (inCurrentPage <= 1)
                {
                    pageStart = 0;
                    pageEnd = pageStart + pageCount;
                }

                HttpContext.Current.Session["page_start"] = pageStart;
                HttpContext.Current.Session["page_end"] = pageEnd;
                HttpContext.Current.Session["page_count"] = iPagerCount;

                vars.pPageCount = iPagerCount;
                vars.pRecordsCount = totalItemCount;
            }
            // New Calculate End


        }
        #endregion
        #region applyPortletPager
        public static string applyPortletPager(string portlet_out, string pager_html, string pager_position)
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
        public static string getPortletPagerHTML(string src, int pCurrent, int pCount, int pStart, int pEnd, string pClass, int itemCount)
        {

            if (itemCount <= 0)
            {
                itemCount = (Convert.ToInt32(HttpContext.Current.Session["page_count"]) * Convert.ToInt32(HttpContext.Current.Session["page_end"])) + Convert.ToInt32(HttpContext.Current.Session["page_start"]);
            }



            GlobalVars vars = new GlobalVars();
            string[] QS = CmsHelper.GetQSArr();
            string fullAlias = string.Empty;
            vars.QS = QS;
            for (int i = 0; i < QS.Length; i++)
            {
                fullAlias += QS[i] + "/";
            }
            fullAlias = fullAlias.Substring(0, fullAlias.Length - 1);
            vars.fullAlias = fullAlias;
            string result = string.Empty;
            string seperator = string.Empty;
            string prevnext = string.Empty;
            string pagerheader = string.Empty;
            string pagerclass = pClass;
            string[] prevnexts = new string[] { };
            string pText = string.Empty;
            string nText = string.Empty;
            int pPage = 0;
            int nPage = 0;





            if (pCount > 0)
            {
                if (itemCount > pCount)
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
                    bool gotAlias = true;
                    string url = string.Empty;

                    //if (vars.QS.Length > 2)
                    //{
                    //    if (vars.QS[2].Contains(","))
                    //    {
                    //        ids = vars.QS[2].Split(',');
                    //    }
                    //    else
                    //    {
                    //        ids = vars.QS[2].Split('-');
                    //    }
                    //}
                    //else if (vars.QS.Length == 2)
                    //{
                    //    if (vars.QS[1].Contains(","))
                    //    {
                    //        ids = vars.QS[1].Split(',');
                    //    }
                    //    else
                    //    {
                    //        ids = vars.QS[1].Split('-');
                    //    }
                    //}
                    //else
                    //{ 

                    //}
                    //if (ids.Length > 3)
                    //{
                    //    // we found that this is aliased url
                    //    if (ids[4] == "a")
                    //        gotAlias = true;
                    //}

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
                        url += "/" + vars.fullAlias + "?p=##page##";
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
            }

            return result;
        }

        #endregion

        #region getPortletData
        public static string getPortletData(

             int pid, int zone_id, int icount, int iorder, int article_id, string portlet_class, bool optioned, string exself, string container, string pInclude, string pExclude, string pHeader, int page_count, int inZoneID

             )
        {
            Page pagem = HttpContext.Current.Handler as Page;
            Dictionary<string, object> oldArticleDetails = (Dictionary<string, object>)pagem.Items["Article_Details"];
            HttpContext.Current.Session["newArticleDetails"] = (Dictionary<string, object>)pagem.Items["Article_Details"];
            GlobalVars vars = new GlobalVars();
            string[] QS = CmsHelper.GetQSArr();
            vars.QS = QS;
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
                string pageNo = string.Empty;

                string[] pageAr = CmsHelper.GetQString();
                pageNo = string.Join(",", pageAr);
                if (pageNo.IndexOf('?') > -1)
                {
                    pageAr = pageNo.Split('?');
                }
                if (pageAr.Length > 1)
                {
                    if (pageNo.Contains("&"))
                    {
                        pageAr = pageNo.Split('&');
                        cur_page = pageAr[0].ToString().Substring(2, pageAr[0].ToString().Length - 2);
                    }
                    else
                    {
                        cur_page = pageAr[1].Substring(2, pageAr[1].Length - 2);
                    }
                }
            }

            if (string.IsNullOrEmpty(GetQSVal(HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString(), "p")))
            {
                cur_page = "1";
            }
            else
            {
                cur_page = GetQSVal(HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString(), "p");
            }


            if (!CmsHelper.IsNumeric(cur_page))
            {
                cur_page_ = 1;
                cur_page = "1";
            }
            else
            {
                cur_page_ = Convert.ToInt32(cur_page);
            }

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

                            CmsHelper.calculatePagings(dt.Rows.Count, icount, cur_page_, page_count);

                            string pageStart = "0", pageEnd = "0";
                            pageStart = HttpContext.Current.Session["page_start"].ToString();
                            pageEnd = HttpContext.Current.Session["page_end"].ToString();

                            // for (int x = vars.page_start; x < vars.page_end; x++)
                            //for (int x = Convert.ToInt32(HttpContext.Current.Session["page_start"]); x < Convert.ToInt32(HttpContext.Current.Session["page_end"]); x++)
                            for (int x = Convert.ToInt32(pageStart); x < Convert.ToInt32(pageEnd); x++)
                            {
                                pc++;

                                cur_portlet = portlet_html;

                                if ((x + 1) > dt.Rows.Count)
                                {
                                    break;
                                }

                                string type_alias = dt.Rows[x][15].ToString();

                                if (cur_portlet.Contains(type_alias))
                                {

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


                                    if (!string.IsNullOrEmpty(global_mapped_article_id) && Convert.ToInt32(global_mapped_article_id) == article_id)
                                    {
                                        portlet_file_article_id = Convert.ToInt32(global_mapped_article_id);
                                    }
                                    else
                                    {
                                        portlet_file_article_id = article_id;
                                    }


                                    bool Active = true;
                                    // if (cur_portlet.Contains("##afiles_" + type_alias + "_"))
                                    if (Active)
                                    {
                                        cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_title\"", "afiles_" + type_alias + "_title\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_comment\"", "afiles_" + type_alias + "_comment\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        if (!string.IsNullOrEmpty(file_name_1))
                                        {
                                            file_name_1 = "/i/content/" + portlet_file_article_id + "_" + file_name_1;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_1\"", "afiles_" + type_alias + "_1\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }

                                        if (!string.IsNullOrEmpty(file_name_2))
                                        {
                                            file_name_2 = "/i/content/" + portlet_file_article_id + "_" + file_name_2;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_2\"", "afiles_" + type_alias + "_2\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_3))
                                        {
                                            file_name_3 = "/i/content/" + portlet_file_article_id + "_" + file_name_3;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_3\"", "afiles_" + type_alias + "_3\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_4))
                                        {
                                            file_name_4 = "/i/content/" + portlet_file_article_id + "_" + file_name_4;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_4\"", "afiles_" + type_alias + "_4\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_5))
                                        {
                                            file_name_5 = "/i/content/" + portlet_file_article_id + "_" + file_name_5;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_5\"", "afiles_" + type_alias + "_5\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_6))
                                        {
                                            file_name_6 = "/i/content/" + portlet_file_article_id + "_" + file_name_6;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_6\"", "afiles_" + type_alias + "_6\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_7))
                                        {
                                            file_name_7 = "/i/content/" + portlet_file_article_id + "_" + file_name_7;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_7\"", "afiles_" + type_alias + "_7\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_8))
                                        {
                                            file_name_8 = "/i/content/" + portlet_file_article_id + "_" + file_name_8;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_8\"", "afiles_" + type_alias + "_8\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_9))
                                        {
                                            file_name_9 = "/i/content/" + portlet_file_article_id + "_" + file_name_9;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_9\"", "afiles_" + type_alias + "_9\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }
                                        if (!string.IsNullOrEmpty(file_name_10))
                                        {
                                            file_name_10 = "/i/content/" + portlet_file_article_id + "_" + file_name_10;
                                            cur_portlet = cur_portlet.Replace("afiles_" + type_alias + "_10\"", "afiles_" + type_alias + "_10\"" + " FileID=\"" + dt.Rows[x][0].ToString() + "\"");
                                        }

                                        #region Old CMS Replace To Render
                                        Control child = pagem.ParseControl(HttpUtility.HtmlDecode(cur_portlet));
                                        child.Page = pagem;
                                        cur_portlet = CmsHelper.RenderControl(child);


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
                                    #endregion



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
                                string[] azidArr = new string[100];
                                string[] idArr = new string[100];
                                string[] detailsArr = new string[100];
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
                                            if (maxA < 101)
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

                                        string names = CmsHelper.getRecordsetNames(dt3);

                                        calculatePagings(dt3.Rows.Count, icount, cur_page_, page_count);

                                        string pageStart = "0", pageEnd = "0";
                                        pageStart = HttpContext.Current.Session["page_start"].ToString();
                                        pageEnd = HttpContext.Current.Session["page_end"].ToString();

                                        //for (int x = Convert.ToInt32(HttpContext.Current.Session["page_start"]); x < Convert.ToInt32(HttpContext.Current.Session["page_end"]); x++)
                                        for (int x = Convert.ToInt32(pageStart); x < Convert.ToInt32(pageEnd); x++)
                                        {
                                            pc++;

                                            mapped_article = CmsHelper.checkMappedArticle(dt3.Rows[x], names);

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
                                                if (CmsHelper.IsManager() && vars.getValue4Dic("portlet_enable_shotcut") == "Y")
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


                                                Control child = pagem.ParseControl(HttpUtility.HtmlDecode(cur_portlet));
                                                child.Page = pagem;
                                                pagem.Items["Article_Details"] = HttpContext.Current.Session["newArticleDetails"];
                                                cur_portlet = CmsHelper.RenderControl(child);




                                                pagem.Items["Article_Details"] = oldArticleDetails;
                                                result = result + Environment.NewLine + cur_portlet;

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

                                        result = "";
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

                                    calculatePagings(dt5.Rows.Count, icount, Convert.ToInt32(cur_page), page_count);

                                    string pageStart = "0", pageEnd = "0";
                                    pageStart = HttpContext.Current.Session["page_start"].ToString();
                                    pageEnd = HttpContext.Current.Session["page_end"].ToString();

                                    for (int x = Convert.ToInt32(pageStart); x < Convert.ToInt32(pageEnd); x++)
                                    //for (int x = Convert.ToInt32(HttpContext.Current.Session["page_start"]); x < dt5.Rows.Count; x++)
                                    //for (int x = Convert.ToInt32(HttpContext.Current.Session["page_start"]); x < Convert.ToInt32(HttpContext.Current.Session["page_end"]); x++)
                                    {
                                        if (x >= dt5.Rows.Count)
                                            break;

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
                                                cur_portlet = CmsHelper.replaceArticleDetailsRows(dt5.Rows[x], names, portlet_html, tag_zone_ow);
                                            }

                                            if (cur_portlet.Contains("##afiles_"))
                                            {
                                                cur_portlet = CmsHelper.processAFiles(cur_portlet, Convert.ToInt32(dt5.Rows[x][2]));
                                            }

                                            if (cur_portlet.Contains("##portlet_"))
                                            {
                                                cur_portlet = CmsHelper.processSubPortlets(cur_portlet, Convert.ToInt32(dt5.Rows[x][2]));

                                            }

                                            // cur_portlet = processTags(cur_portlet, Convert.ToInt32(dt5.Rows[x][2]));

                                            if (multi_class)
                                            {
                                                cur_portlet = "<div class=\"" + getPortletMultiClassName(pctemp, pc, "name") + "\">" + Environment.NewLine + "<div class=\"" + pctemp[0] + "\">" + Environment.NewLine + cur_portlet + "</div>" + Environment.NewLine + "</div>" + getPortletMultiClassName(pctemp, pc, "seperator");
                                            }


                                            //Custom Control Render



                                            //     Dictionary<string, object> xxxx = (Dictionary<string, object>)HttpContext.Current.Session["newArticleDetails"];
                                            //if (xxxx["article_id"].ToString() == "226")
                                            //{
                                            //    string xxx= "asd";
                                            //}

                                            //RELATED PORTLET

                                            Control child = pagem.ParseControl(HttpUtility.HtmlDecode(cur_portlet));
                                            child.Page = pagem;
                                            pagem.Items["Article_Details"] = HttpContext.Current.Session["newArticleDetails"];
                                            cur_portlet = CmsHelper.RenderControl(child);

                                            for (int i = 0; i < 5; i++)
                                            {
                                                if (cur_portlet.Contains("##portlet_"))
                                                {
                                                    child = pagem.ParseControl(HttpUtility.HtmlDecode(cur_portlet));
                                                    child.Page = pagem;
                                                    pagem.Items["Article_Details"] = HttpContext.Current.Session["newArticleDetails"];
                                                    cur_portlet = CmsHelper.RenderControl(child);
                                                }
                                            }


                                            pagem.Items["Article_Details"] = oldArticleDetails;
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

        #endregion

        #region getSubEDLink
        public static string getSubEDLink(int article_id, string inType, bool inPre, string inRevID, int zone_id, int zone_group_id, int site_id)
        {
            GlobalVars vars = new GlobalVars();
            string result = "";
            int inRevID_ = 0;

            if (!CmsHelper.IsNumeric(inRevID))
                inRevID_ = 0;
            else
                inRevID_ = Convert.ToInt32(inRevID);

            if (inPre != true)
                inRevID_ = 0;

            string eb_flags = vars.eb[Convert.ToInt32(inType)];
            string eb_str = " id=\"editButton" + article_id + "\" onmouseover=\"showEditButtons(" + inType + ",'" + eb_flags + "'," + site_id + "," + zone_group_id + "," + zone_id + "," + article_id + "," + inRevID_ + ");\" onmouseout=\"hideEditButtons(" + inType + ");\" ";
            result = Environment.NewLine + "<!-- EDITOR-BUTTON-START --><a href=\"javascript:void(0);\" onClick=\"return false;\" title=\"Click here to edit this article\" " + eb_str + " target=\"editor\"><span class=\"edBut\">*</span></a><!-- EDITOR-BUTTON-END -->" + Environment.NewLine;

            //kapandi
            //Page.Trace.Write(string.Format("getSubEDLink Called! site_id:{0}, zone_group_id:{1}, zone_id:{2}, article_id:{3}, IsAuthenticated:{4}, IsManager:{5}", site_id, zone_group_id, zone_id, article_id, HttpContext.Current.User.Identity.IsAuthenticated, IsManager()));

            result = string.Format("<cms:EditorLink runat=\"server\" Type=\"{0}\" ArticleId=\"{1}\" RevId=\"{2}\" ZoneId=\"{3}\" ZoneGroupId=\"{4}\" SiteId=\"{5}\" Flags=\"{6}\"></cms:EditorLink>", inType, article_id, inRevID_, zone_id, zone_group_id, site_id, eb_flags);

            return result;
        }
        #endregion
        public static bool gotPortlet(string html)
        {
            return (html.IndexOf("##portlet_") != -1);
        }

        public static bool gotSubPortlet(string html)
        {
            return (html.IndexOf("##portlet_") != -1 && html.IndexOf("id=\"-1_") != -1);
        }

        #region processSubPortlets
        private static string processSubPortlets(string template, int article_id)
        {
            GlobalVars vars = new GlobalVars();
            string result = template;
            bool isFound = CmsHelper.gotSubPortlet(template);
            int loopLimit = 0;
            int zone_id = Convert.ToInt32(vars.a["zone_id"]);

            while (loopLimit < 5 && isFound == true)
            {
                loopLimit++;
                result = CmsHelper.replacePortlets(result, article_id, zone_id);
                isFound = gotSubPortlet(result);
            }
            return result;
        }
        #endregion
        #region checkMappedArticle
        public static string checkMappedArticle(DataRow dr, string names)
        {

            string result = string.Empty;
            string itName = string.Empty;
            GlobalVars vars = new GlobalVars();
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
        private static string excludeArticleSQL(string inS, string inArticles)
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


        #region getPortletHTML
        public static string getPortletHTML(int portlet_id, string type)
        {
            string result = string.Empty;
            string portlet_html = string.Empty;
            string portlet_css = string.Empty;
            string portlet_header = string.Empty;
            string portlet_footer = string.Empty;
            string portlet_enable_shotcut = string.Empty;
            GlobalVars vars = new GlobalVars();
            string[] QS = CmsHelper.GetQSArr();
            vars.QS = QS;
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




        #region replaceMenus
        public static string replaceMenus(string template, int rmaid, int rmzid, string inType)
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
                    menu_out = getMenuData(menu_id, d1, d2, d3, d4, rmzid, inType, menu_exclude, menu_include, menusingleitem, menuonclickfunction);

                    if (inType == "menu")
                    {
                        menu_out = openMenuData(menu_out, d1, rmaid, rmzid, menuselectedcls, menunotselectedcls);
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
        public static string openMenuData(string menu_out, string d1, int article_id, int zone_id, string menuselectedcls, string menunotselectedcls)
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
        private static string openTopMenuData(string menu_out, int article_id, string menuselectedcls)
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


        #region IsCacheActive (Tested)
        public static bool IsCacheActive()
        {
            return GetApplication(Constansts.CFG_CACHE_ACTIVE).Equals("1");
        }
        #endregion

        #region getMenuData
        public static string getMenuData(string menu_id, string d1, string d2, string d3, string d4, int zone_id, string inType, string menu_exclude, string menu_include, string menusingleitem, string menuonclickfunction)
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

        #region GetApplication
        public static string GetApplication(string key)
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



        #region getContentLinkAlias
        public static string getContentLinkAlias(string zoneId, string articleId, string siteName, string zoneGroupName, string zoneName, string iheadline, string alias)
        {
            string str = string.Empty;

            if (!string.IsNullOrEmpty(alias))
            {
                str = "/" + alias;
            }
            else
            {

                string site_name = "", zone_group_name = "", zone_name = "", headline = "";

                if (siteName != "") site_name = "/" + CmsHelper.c2QS(HttpUtility.HtmlDecode(siteName));
                if (zoneGroupName != "") zone_group_name = "/" + CmsHelper.c2QS(HttpUtility.HtmlDecode(zoneGroupName));
                if (zoneName != "") zone_name = "/" + CmsHelper.c2QS(HttpUtility.HtmlDecode(zoneName));
                if (iheadline != "") headline = "/" + CmsHelper.c2QS(HttpUtility.HtmlDecode(iheadline));

                str = "/" + Constansts.ALIAS_WEB + "/" + zoneId.ToString() + "-" + articleId.ToString() + "-1-1" + site_name + zone_group_name + zone_name + headline;

                str = str.Replace(",", "-");
                str = str.Replace("--", "-");
                str = str.Replace("--", "-");
            }
            return str;
        }
        #endregion


        #region getMenuSubRows
        private static string getMenuSubRows(string menu_id, string inZID, string d2, string inOrder, string inPos, string inMT, string inExcluder, string inIncluder, string menusingleitem, string menuonclickfunction, int p)
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
                        azid2 = azid2.Substring(0, azid2.Length - 1);
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
                        // burası freetext alanından geliyor 
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

                            string article_link = string.Empty;
                            if (article_type == 2)
                            {
                                string[] typeDetail = article_type_detail.Split('-').ToArray();
                                string zoneId = typeDetail[0];
                                int articleId = Convert.ToInt32(typeDetail[1]);
                                article_link = GetArticleAliasOrURL(articleId, zoneId);
                            }
                            else
                            {
                                article_link = getContentLinkAlias(zone_id.ToString(), article_id.ToString(), site_name, zone_group_name, zone_name, headline, az_alias);
                            }


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

        #region subStrCount
        public static int subStrCount(string a, string termine)
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
        #region regOp
        public static string regOp(string type, string html)
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

        #region getOrderColumn (Tested)
        public static string getOrderColumn(int orderID)
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
                case 17:
                    orderColumn = "Cast(custom_1 as nvarchar(max)) asc";
                    break;
                case 18:
                    orderColumn = "Cast(custom_1 as nvarchar(max)) desc";
                    break;
                case 19:
                    orderColumn = "Cast(custom_2 as nvarchar(max)) asc";
                    break;
                case 20:
                    orderColumn = "Cast(custom_2 as nvarchar(max)) desc";
                    break;
                case 21:
                    orderColumn = "Cast(custom_3 as nvarchar(max)) asc";
                    break;
                case 22:
                    orderColumn = "Cast(custom_3 as nvarchar(max)) desc";
                    break;
                case 23:
                    orderColumn = "Cast(custom_4 as nvarchar(max)) asc";
                    break;
                case 24:
                    orderColumn = "Cast(custom_4 as nvarchar(max)) desc";
                    break;
                case 25:
                    orderColumn = "Cast(custom_5 as nvarchar(max)) asc";
                    break;
                case 26:
                    orderColumn = "Cast(custom_5 as nvarchar(max)) desc";
                    break;
                default:
                    orderColumn = "updated desc";
                    break;
            }
            return orderColumn;
        }
        #endregion


        #region Random Password (String) Generator
        public class RandomPassword
        {
            // Define default min and max password lengths.
            private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
            private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

            // Define supported password characters divided into groups.
            // You can add (or remove) characters to (from) these groups.
            private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
            private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
            private static string PASSWORD_CHARS_NUMERIC = "23456789";
            private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

            /// <summary>
            /// Generates a random password.
            /// </summary>
            /// <returns>
            /// Randomly generated password.
            /// </returns>
            /// <remarks>
            /// The length of the generated password will be determined at
            /// random. It will be no shorter than the minimum default and
            /// no longer than maximum default.
            /// </remarks>
            public static string Generate()
            {
                return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                                DEFAULT_MAX_PASSWORD_LENGTH,
                                true,
                                true);
            }

            /// <summary>
            /// Generates a random password of the exact length.
            /// </summary>
            /// <param name="length">
            /// Exact password length.
            /// </param>
            /// <returns>
            /// Randomly generated password.
            /// </returns>
            public static string Generate(int length, bool hasSpecial)
            {
                return Generate(length, length, hasSpecial, true);
            }

            public static string Generate(int length, bool hasSpecial, bool hasAlphaNumeric)
            {
                return Generate(length, length, hasSpecial, hasAlphaNumeric);
            }

            /// <summary>
            /// Generates a random password.
            /// </summary>
            /// <param name="minLength">
            /// Minimum password length.
            /// </param>
            /// <param name="maxLength">
            /// Maximum password length.
            /// </param>
            /// <returns>
            /// Randomly generated password.
            /// </returns>
            /// <remarks>
            /// The length of the generated password will be determined at
            /// random and it will fall with the range determined by the
            /// function parameters.
            /// </remarks>
            public static string Generate(int minLength,
                                          int maxLength,
                                          bool hasSpecial,
                                          bool hasAlphaNumeric)
            {
                // Make sure that input parameters are valid.
                if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                    return null;

                // Create a local array containing supported password characters
                // grouped by types. You can remove character groups from this
                // array, but doing so will weaken the password strength.
                char[][] charGroups;

                if (hasSpecial)
                {
                    if (hasAlphaNumeric)
                    {
                        charGroups = new char[][]
                    {
                        PASSWORD_CHARS_LCASE.ToCharArray(),
                        PASSWORD_CHARS_UCASE.ToCharArray(),
                        PASSWORD_CHARS_NUMERIC.ToCharArray(),
                        PASSWORD_CHARS_SPECIAL.ToCharArray()
                    };
                    }
                    else
                    {
                        charGroups = new char[][]
                    {
                        PASSWORD_CHARS_NUMERIC.ToCharArray(),
                        PASSWORD_CHARS_SPECIAL.ToCharArray()
                    };
                    }
                }
                else
                {
                    if (hasAlphaNumeric)
                    {
                        charGroups = new char[][]
                    {
                        PASSWORD_CHARS_LCASE.ToCharArray(),
                        PASSWORD_CHARS_UCASE.ToCharArray(),
                        PASSWORD_CHARS_NUMERIC.ToCharArray()
                    };
                    }
                    else
                    {
                        charGroups = new char[][]
                    {
                        PASSWORD_CHARS_NUMERIC.ToCharArray()
                    };
                    }
                }


                // Use this array to track the number of unused characters in each
                // character group.
                int[] charsLeftInGroup = new int[charGroups.Length];

                // Initially, all characters in each group are not used.
                for (int i = 0; i < charsLeftInGroup.Length; i++)
                    charsLeftInGroup[i] = charGroups[i].Length;

                // Use this array to track (iterate through) unused character groups.
                int[] leftGroupsOrder = new int[charGroups.Length];

                // Initially, all character groups are not used.
                for (int i = 0; i < leftGroupsOrder.Length; i++)
                    leftGroupsOrder[i] = i;

                // Because we cannot use the default randomizer, which is based on the
                // current time (it will produce the same "random" number within a
                // second), we will use a random number generator to seed the
                // randomizer.

                // Use a 4-byte array to fill it with random bytes and convert it then
                // to an integer value.
                byte[] randomBytes = new byte[4];

                // Generate 4 random bytes.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(randomBytes);

                // Convert 4 bytes into a 32-bit integer value.
                int seed = (randomBytes[0] & 0x7f) << 24 |
                            randomBytes[1] << 16 |
                            randomBytes[2] << 8 |
                            randomBytes[3];

                // Now, this is real randomization.
                Random random = new Random(seed);

                // This array will hold password characters.
                char[] password = null;

                // Allocate appropriate memory for the password.
                if (minLength < maxLength)
                    password = new char[random.Next(minLength, maxLength + 1)];
                else
                    password = new char[minLength];

                // Index of the next character to be added to password.
                int nextCharIdx;

                // Index of the next character group to be processed.
                int nextGroupIdx;

                // Index which will be used to track not processed character groups.
                int nextLeftGroupsOrderIdx;

                // Index of the last non-processed character in a group.
                int lastCharIdx;

                // Index of the last non-processed group.
                int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

                // Generate password characters one at a time.
                for (int i = 0; i < password.Length; i++)
                {
                    // If only one character group remained unprocessed, process it;
                    // otherwise, pick a random character group from the unprocessed
                    // group list. To allow a special character to appear in the
                    // first position, increment the second parameter of the Next
                    // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                    if (lastLeftGroupsOrderIdx == 0)
                        nextLeftGroupsOrderIdx = 0;
                    else
                        nextLeftGroupsOrderIdx = random.Next(0,
                                                             lastLeftGroupsOrderIdx);

                    // Get the actual index of the character group, from which we will
                    // pick the next character.
                    nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                    // Get the index of the last unprocessed characters in this group.
                    lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                    // If only one unprocessed character is left, pick it; otherwise,
                    // get a random character from the unused character list.
                    if (lastCharIdx == 0)
                        nextCharIdx = 0;
                    else
                        nextCharIdx = random.Next(0, lastCharIdx + 1);

                    // Add this character to the password.
                    password[i] = charGroups[nextGroupIdx][nextCharIdx];

                    // If we processed the last character in this group, start over.
                    if (lastCharIdx == 0)
                        charsLeftInGroup[nextGroupIdx] =
                                                  charGroups[nextGroupIdx].Length;
                    // There are more unprocessed characters left.
                    else
                    {
                        // Swap processed character with the last unprocessed character
                        // so that we don't pick it until we process all characters in
                        // this group.
                        if (lastCharIdx != nextCharIdx)
                        {
                            char temp = charGroups[nextGroupIdx][lastCharIdx];
                            charGroups[nextGroupIdx][lastCharIdx] =
                                        charGroups[nextGroupIdx][nextCharIdx];
                            charGroups[nextGroupIdx][nextCharIdx] = temp;
                        }
                        // Decrement the number of unprocessed characters in
                        // this group.
                        charsLeftInGroup[nextGroupIdx]--;
                    }

                    // If we processed the last group, start all over.
                    if (lastLeftGroupsOrderIdx == 0)
                        lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                    // There are more unprocessed groups left.
                    else
                    {
                        // Swap processed group with the last unprocessed group
                        // so that we don't pick it until we process all groups.
                        if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                        {
                            int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                            leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                        leftGroupsOrder[nextLeftGroupsOrderIdx];
                            leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                        }
                        // Decrement the number of unprocessed groups.
                        lastLeftGroupsOrderIdx--;
                    }
                }

                // Convert password characters into a string and return the result.
                return new string(password);
            }
        }
        #endregion

        public static bool IsRegistered = false;

        public CmsHelper()
        {
            if (!IsRegistered)
                throw new Exception("Invalid CMS Licence");
        }

        public static void UnlockCMS(string key, string domanin)
        {
            string curDomain = domanin;

            RC4Encrypt rc4 = new RC4Encrypt(Constansts.CMS_LICENCE_RC4_KEY);
            rc4.PlainText = CmsHelper.DecodeBase64(key);

            string domainKey = rc4.EnDeCrypt();

            if (domainKey.Split(',').Contains(curDomain))
                IsRegistered = true;
            else
                IsRegistered = false;

            if (!IsRegistered)
            {
                IsRegistered = checkDomainWildCard(curDomain, domainKey);
            }

            if (!IsRegistered)
            {
                //LogInvalidLicence(curDomain, domainKey, key);
            }
        }

        public static bool CheckLicence()
        {
            return IsRegistered;
        }

        #region c2QS
        public static string c2QSv2(string s)
        {
            string tmp = string.Empty;
            tmp = s.ToLowerInvariant();
            tmp = tmp.Replace("İ", "i");
            tmp = tmp.Replace("ı", "i");
            tmp = tmp.Replace("ğ", "g");
            tmp = tmp.Replace("ü", "u");
            tmp = tmp.Replace("ş", "s");
            tmp = tmp.Replace("ö", "o");
            tmp = tmp.Replace("ç", "c");
            tmp = CmsHelper.RegexReplace(tmp, "&([a-z].*);", "_", true, true);
            tmp = tmp.Replace(" ", "-");
            tmp = tmp.Replace("?", "-");
            tmp = tmp.Replace(".", "-");
            tmp = tmp.Replace("&", "-");
            tmp = CmsHelper.RegexReplace(tmp, "[^a-z0-9_-]", "", true, true);
            return tmp;
        }
        #endregion

        #region c2QS
        public static string c2QS(string s)
        {
            s = s.TrimEnd().TrimStart();
            string tmp = string.Empty;
            tmp = s.ToLower();
            tmp = tmp.Replace("-", "__________");
            tmp = tmp.Replace("İ", "i");
            tmp = tmp.Replace("ı", "i");
            tmp = tmp.Replace("ğ", "g");
            tmp = tmp.Replace("ü", "u");
            tmp = tmp.Replace("ş", "s");
            tmp = tmp.Replace("ö", "o");
            tmp = tmp.Replace("ç", "c");
            tmp = CmsHelper.RegexReplace(tmp, "&([a-z].*);", "_", true, true);
            tmp = tmp.Replace(" ", "-");
            tmp = tmp.Replace("?", "-");
            tmp = tmp.Replace(".", "-");
            tmp = tmp.Replace("&", "-");
            tmp = CmsHelper.RegexReplace(tmp, "[^a-z0-9/_-]", "", true, true);
            tmp = tmp.Replace("__________", "-");
            return tmp;
        }
        #endregion

        private static string RepeatedCharacters(int l, char s)
        {
            string str = string.Empty;
            return str.PadLeft(l, s);
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string EncodeBase64(string data)
        {
            try
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                return System.Convert.ToBase64String(bytes);
            }
            catch { return string.Empty; }
        }

        public static string DecodeBase64(string data)
        {
            try
            {
                return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(data));
            }
            catch { return string.Empty; }
        }
        public static string RenderControl(Control control)
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
        private static void LogInvalidLicence(string curDomain, string domainKey, string curKey)
        {
            string serverValues = string.Empty;
            string logDir = HttpContext.Current.Server.MapPath("/i/Logs");

            //Create Log Directory if not exist
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            logDir = logDir + "\\" + HttpContext.Current.Request.ServerVariables["INSTANCE_ID"].ToString();

            //Create IIS Instance ID Directory if not exist
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            string fileName = logDir + "\\" + DateTime.Now.ToString().Replace("-", "").Replace(".", "").Replace(" ", "_").Replace(":", "") + System.Guid.NewGuid().ToString() + ".log";

            for (int i = 0; i < HttpContext.Current.Request.ServerVariables.Count; i++)
            {
                string key = HttpContext.Current.Request.ServerVariables.Keys[i].ToString();
                string val = HttpContext.Current.Request.ServerVariables[i].ToString();
                serverValues += key.ToUpper() + " - " + val + Environment.NewLine;
            }

            string LogValue = "EuroCMS Licence Logging @" + DateTime.Now.ToString() + " ****************************" + Environment.NewLine +
                "Current Domain: " + curDomain + Environment.NewLine +
                "Current Key: " + curKey + Environment.NewLine +
                "Server Variables: " + Environment.NewLine +
                "****************************" + Environment.NewLine;

            if (File.Exists(fileName))
            {
                try
                {
                    FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(LogValue);
                    sw.Close();
                    fs.Close();
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    FileStream fs = File.Create(fileName);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(LogValue);
                    sw.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {

                }

            }
        }

        private static bool checkDomainWildCard(string curDomain, string domainKey)
        {
            bool result = false;

            string[] allowedDomains;
            string fullDomain;
            string currentFullDomain;

            allowedDomains = domainKey.Split(',');
            foreach (string allowedDomain in allowedDomains)
            {
                if (allowedDomain == "*.*")
                {
                    result = true;
                    break;
                }
                else if (allowedDomain.Length > 2 && CmsHelper.Left(allowedDomain, 2) == "*.")
                {
                    //fullDomain = CmsHelper.Mid(allowedDomain, 3);
                    fullDomain = CmsHelper.Mid(allowedDomain, 2);

                    if (fullDomain == curDomain)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        currentFullDomain = CmsHelper.Mid(curDomain, curDomain.IndexOf(".") + 1);
                        if (fullDomain == currentFullDomain)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public static string ConvertStringToMD5(string ClearText)
        {
            byte[] ByteData = Encoding.ASCII.GetBytes(ClearText);
            MD5 oMd5 = MD5.Create();
            byte[] HashData = oMd5.ComputeHash(ByteData);
            StringBuilder oSb = new StringBuilder();
            for (int x = 0; x < HashData.Length; x++)
            {
                oSb.Append(HashData[x].ToString("x2"));
            }
            return oSb.ToString();
        }

        public static string Left(string original, int length)
        {
            return original.Substring(0, length);
        }

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(numberCharacters > original.Length ? 0 : original.Length - numberCharacters);
        }

        public static string Mid(string original, int start)
        {
            return original.Substring(start, original.Length - start);
        }

        public static string Mid(string original, int start, int length)
        {
            return original.Substring(start, original.Length < length ? original.Length : length);
        }

        public static int Instr(string s1, string s2, CompareMethod method)
        {
            return Strings.InStr(Conversions.ToString(s1), Conversions.ToString(s2), method);
        }

        public static int Instr(int start, string s1, string s2, CompareMethod method)
        {
            return Strings.InStr(start, Conversions.ToString(s1), Conversions.ToString(s2), method);
        }

        public static string dDay(string inD)
        {
            string strR = string.Empty;
            if (inD.Length > 0)
            {
                strR = Right("0" + inD, 2);
            }
            return strR;
        }

        public static void AddCustomValues(string customValues)
        {
            HttpContext.Current.Session["___custom_values___"] = customValues;
        }

        public static string[] GetQSArr()
        {
            string UrlFull = HttpContext.Current.Request.Url.ToString();
            string QS = "", hede = "";
            string WebPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();
            if (UrlFull.Contains("?404;"))
            {
                //This is IIS 6
                QS = UrlFull.Split(new string[] { ":" + WebPort + "/" }, StringSplitOptions.None)[1].Replace("?", "&");
            }
            else
            {
                //This is IIS 7 or newer
                QS = HttpContext.Current.Request.QueryString.ToString().Replace("%2f", "/");
            }

            if (QS.IndexOf("&") > 0)
                QS = QS.Substring(0, QS.IndexOf("&"));

            if (QS.EndsWith("/"))
                QS = QS.Substring(0, QS.Length - 1);

            QS = QS.Replace("Page.aspx", "");

            if (QS.Contains("?"))
            {
                QS = QS.Substring(0, QS.IndexOf("?"));
            }

            //HttpContext.Current.Response.Write("HEDE: " + hede + " - " + "Gelen değer: " + QS + " - " + "Gelen URL: " + HttpContext.Current.Request.Url.Host);

            return QS.Split(new string[] { "/" }, StringSplitOptions.None);
        }


        public static string[] GetQString()
        {
            string UrlFull = HttpContext.Current.Request.Url.ToString();
            string QS = UrlFull;
            string WebPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();
            if (UrlFull.Contains("?404;"))
            {
                //This is IIS 6
                QS = UrlFull.Split(new string[] { ":" + WebPort + "/" }, StringSplitOptions.None)[1].ToString();
            }
            else
            {
                //This is IIS 7 or newer
                QS = HttpContext.Current.Request.QueryString.ToString().Replace("%2f", "/");

            }

            if (QS.IndexOf("&") > 0)
                QS = QS.Substring(0, QS.IndexOf("&"));


            if (QS.EndsWith("/"))
                QS = QS.Substring(0, QS.Length - 1);

            QS = QS.Replace("Page.aspx", "");

            if (QS.Contains("?"))
            {
                QS = QS.Substring(0, QS.IndexOf("?"));
            }

            return QS.Split(new string[] { "/" }, StringSplitOptions.None);
        }

        public static string GetQS()
        {
            return "/" + String.Join("/", GetQSArr());
        }

        public static string RegexGet(string OriginalString, string Pattern, bool CaseSensitive)
        {
            string output = string.Empty;
            RegexOptions ExpOpt = new RegexOptions();
            ExpOpt |= RegexOptions.Multiline;
            if (!CaseSensitive) ExpOpt |= RegexOptions.IgnoreCase;
            Regex regex = new Regex(Pattern, ExpOpt);
            MatchCollection matches = regex.Matches(OriginalString);
            foreach (Match m in matches)
            {
                output = m.Value;
            }
            return output;
        }

        public static string RegexReplace(string OriginalString, string ReplacePattern, string ReplaceString, bool SingleLine, bool CaseSensitive, bool MultiLine, bool global)
        {
            string output = OriginalString;
            RegexOptions ExpOpt = new RegexOptions();
            if (!CaseSensitive) ExpOpt |= RegexOptions.IgnoreCase;
            if (MultiLine) ExpOpt |= RegexOptions.Multiline;
            if (SingleLine) ExpOpt |= RegexOptions.Singleline;

            MatchCollection matches = Regex.Matches(OriginalString, ReplacePattern, ExpOpt);
            if (matches.Count > 0)
                output = Regex.Replace(OriginalString, ReplacePattern, ReplaceString, ExpOpt);
            else if (ReplaceString.Contains("$"))
                output = "";
            return output;
        }

        public static string RegexReplace(string OriginalString, string ReplacePattern, string ReplaceString, bool CaseSensitive, bool MultiLine)
        {
            return RegexReplace(OriginalString, ReplacePattern, ReplaceString, true, false, true, false);
        }

        public static string RegexReplace(string OriginalString, string ReplacePattern, string ReplaceString)
        {
            return RegexReplace(OriginalString, ReplacePattern, ReplaceString, false, true);
        }

        public static bool IsValidEmail(string EMailAddress)
        {
            // Check if e-mail address is ok.
            Regex r = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            // Find a single match in the string.
            Match m = r.Match(EMailAddress);
            return m.Success;
        }

        public static bool IsNumeric(string value)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static bool IsGuid(string ID)
        {
            Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}(\-){0,1}[0-9a-fA-F]{4}(\-){0,1}[0-9a-fA-F]{4}(\-){0,1}[0-9a-fA-F]{4}(\-){0,1}[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            bool isValid = false;
            if (ID != null)
            {
                if (isGuid.IsMatch(ID))
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public static string GetCurrentIP()
        {
            string CurrentIP = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_RLNCLIENTIPADDR"] != null)
                CurrentIP = HttpContext.Current.Request.ServerVariables["HTTP_RLNCLIENTIPADDR"].ToString();
            if (string.IsNullOrEmpty(CurrentIP) || CurrentIP == "unknown")
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    CurrentIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            if (string.IsNullOrEmpty(CurrentIP) || CurrentIP == "unknown")
            {
                if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    CurrentIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return CurrentIP;
        }

        public static string GetMD5Hash(string input)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string GenerateGuidString()
        {
            return Guid.NewGuid().ToString();
        }

        public static string ClearInjection(string str)
        {
            string temp = str;
            if (string.IsNullOrEmpty(temp))
                return string.Empty;
            else
            {
                temp = temp.Replace("'", "''");
                temp = temp.Replace("<", "&lt;");
                temp = temp.Replace(">", "&gt;");
                temp = temp.Trim();
            }
            return temp;
        }

        public static string GetRequest(string reguestKey, char requestType)
        {
            string ReturnValue = string.Empty;
            switch (requestType)
            {
                case 'F':
                    ReturnValue = (HttpContext.Current.Request.Form[reguestKey] != null) ?
                       ClearInjection(HttpContext.Current.Request.Form[reguestKey]) : string.Empty;
                    break;
                case 'Q':
                    ReturnValue = (HttpContext.Current.Request.QueryString[reguestKey] != null) ?
                       ClearInjection(HttpContext.Current.Request.QueryString[reguestKey]) : string.Empty;
                    break;
            }
            return ReturnValue;
        }

        public void DrawBasicSecurityCode()
        {
            Random Rand = new Random();
            Bitmap Resim;
            Graphics Grfk;
            Font Fnt;
            int RandNum = Rand.Next(10000, 99999);
            Resim = new Bitmap(80, 30);
            Grfk = Graphics.FromImage(Resim);
            Grfk.Clear(Color.Orange);
            Fnt = new Font("Verdana", 13, FontStyle.Bold);
            Grfk.DrawString(RandNum.ToString(), Fnt, Brushes.White, 5, 5);
            int RandY1 = Rand.Next(0, 30);
            int RandY2 = Rand.Next(0, 30);
            Grfk.DrawLine(Pens.Gray, 0, RandY1, 80, RandY2);
            int i;
            for (i = 0; i <= 80; i++)
            {
                int RandPixelX = Rand.Next(0, 80);
                int RandPixelY = Rand.Next(0, 30);
                Resim.SetPixel(RandPixelX, RandPixelY, Color.Green);
            }
            RandY1 = Rand.Next(0, 30);
            RandY2 = Rand.Next(0, 30);
            Grfk.DrawLine(Pens.Gray, 0, RandY1, 80, RandY2);
            HttpContext.Current.Response.ContentType = "image/gif";
            Resim.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
            HttpContext.Current.Session["SECURITY_CODE"] = RandNum.ToString();
        }

        private int GetHashInt(Hashtable thisHash, string hashKey)
        {
            if (hashKey != null && hashKey != "" && thisHash[hashKey] != null)
                return (Int32)thisHash[hashKey];
            return 0;
        }

        private string GetHash(Hashtable thisHash, string hashKey)
        {
            if (hashKey != null && hashKey != "" && thisHash[hashKey] != null)
                return thisHash[hashKey].ToString();
            return "";
        }

        private object GetHash(Hashtable thisHash, string hashKey, string returnType)
        {
            if (hashKey != null && hashKey != "" && thisHash[hashKey] != null)
            {
                switch (returnType)
                {
                    case "int":
                        return (Int32)thisHash[hashKey];
                    case "bool":
                        return (bool)thisHash[hashKey];
                    default:
                        return thisHash[hashKey];
                }
            }
            return "";
        }

        public string GetQSVal(string reqKey)
        {
            string returnStr = "";
            string[] valueArr;
            string QS = HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString() + '&';
            if (QS.IndexOf("?") > 0)
            {
                string[] ParamString = QS.Split(new char[1] { '?' });
                if (ParamString.Length < 1) return "";
                string[] ParamArr = ParamString[1].ToString().Split(new char[1] { '&' });
                foreach (string paramKey in ParamArr)
                {
                    if (paramKey.IndexOf("=") > 0)
                    {
                        valueArr = paramKey.Split(new char[1] { '=' });
                        if (valueArr[0].ToString().ToLowerInvariant() == reqKey.ToLowerInvariant())
                        {
                            if (valueArr.Length == 2)
                            {
                                returnStr = valueArr[1].ToString();
                            }
                            else
                            {
                                returnStr = "";
                            }
                            return returnStr;
                        }
                    }
                }
            }
            return returnStr;
        }

        public static string GetQSVal(string QueryString, string reqKey)
        {
            string returnStr = "";
            string[] ParamArr;
            QueryString = NoInj(QueryString);
            QueryString += "&";

            if (QueryString.Contains("?"))
            {
                string[] ParamString = QueryString.Split(new char[1] { '?' });

                if (ParamString.Length < 1) return "";

                ParamArr = ParamString.Length > 2 ?
                              ParamString[2].ToString().Split(new char[1] { '&' })
                            : ParamString[1].ToString().Split(new char[1] { '&' });
            }
            else
                ParamArr = QueryString.Split(new char[1] { '&' });

            reqKey = reqKey.ToLowerInvariant();

            //Loop in params for match
            foreach (string paramKey in ParamArr)
            {
                if (paramKey.Contains("="))
                {
                    if (paramKey.ToLowerInvariant().StartsWith(reqKey + "="))
                        return paramKey.Substring(reqKey.Length + 1);
                }
            }

            return returnStr;
        }

        public void WriteDebug(string str)
        {
            HttpContext.Current.Response.Write(str + "\n<hr>\n");
        }

        public void ScriptInclude(string scriptPath)
        {
            string alert = string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", scriptPath);
            HttpContext.Current.Response.Write(alert);
        }

        public void ScriptIncludeCode(string scriptCode)
        {
            string alert = string.Format("<script type=\"text/javascript\">{0}</script>", scriptCode);
            HttpContext.Current.Response.Write(alert);
        }

        public void ScriptIncludeAlert(string message)
        {
            string alert = string.Format("<script type=\"text/javascript\">alert(\"{0}\");</script>", message);
            HttpContext.Current.Response.Write(alert);
        }

        public void WriteXMLError(string error)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<error>").Append(error).Append("</error>\n");
            WriteXML(xml.ToString());
        }

        public void WriteXML(string inner_xml)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<data>\n")
               .Append(inner_xml)
               .Append("</data>");
            HttpContext.Current.Response.ContentType = "text/xml";
        }

        public void WriteSingleResponse(string error)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<result>").Append(error).Append("</result>\n");
            WriteXML(xml.ToString());
        }

        public string noInjHTML(string str)
        {
            string tmp = str;
            if (tmp == null)
                return "";
            else
            {
                tmp = tmp.Replace("'", "''");
                tmp = RegexReplace(tmp, @"<script[^.]*\/script>|<script(.)*</script>", "", false, true);
                tmp = RegexReplace(tmp, @"<style[^.]*\/style>|<style(.)*</style>", "", false, true);
                //tmp = tmp.Replace("<", "&lt;");
                //tmp = tmp.Replace(">", "&gt;");
                //tmp = tmp.Replace("--", "");
                //tmp = tmp.Replace("'", "");
                //tmp = tmp.Replace("%", "");
                //tmp = tmp.Replace("\\", "");
                //tmp = tmp.Replace("..", "");
                //tmp = tmp.Replace("exec", "");
                //tmp = tmp.Replace("cmdshell", "");
                //tmp = tmp.Replace("shutdown", "");
                tmp = tmp.Trim();
            }
            return tmp;
        }

        public static string GetWithNoInj(string str)
        {
            string tmp = "";
            if (HttpContext.Current.Request.Form[str] == null)
                return tmp;
            else
            {
                tmp = HttpContext.Current.Request.Form[str];
                tmp = tmp.Replace("'", "''");
                tmp = tmp.Replace("--", "");
                tmp = tmp.Replace("<", "");
                tmp = tmp.Replace(">", "");
                tmp = tmp.Replace("'", "");
                tmp = tmp.Replace("%", "");
                tmp = tmp.Replace("\\", "");
                tmp = tmp.Replace("..", "");
                tmp = tmp.Replace("exec", "");
                tmp = tmp.Replace("cmdshell", "");
                tmp = tmp.Replace("shutdown", "");
                tmp = tmp.Trim();
            }
            return tmp;
        }

        public static string NoInj(string text)
        {
            string temp = text.Trim().ToLower();

            if (!string.IsNullOrEmpty(temp))
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

            //if (temp.Contains("applet") || temp.Contains("body") ||
            //    temp.Contains("embed")  ||  temp.Contains("frame") ||
            //    temp.Contains("script") || temp.Contains("frameset") ||
            //    temp.Contains("html")   ||   temp.Contains("iframe") ||
            //    temp.Contains("img")    ||    temp.Contains("style") ||
            //    temp.Contains("layer")  ||  temp.Contains("link") ||
            //    temp.Contains("ilayer") || temp.Contains("meta") ||
            //    temp.Contains("object"))
            //{
            //    string url = HttpContext.Current.Request.Url.Host;
            //    HttpContext.Current.Response.Redirect(url);  
            //}

            if (temp.Contains("exec") || temp.Contains("cmdshell") ||
                temp.Contains("shutdown") || temp.Contains("drop table") ||
                temp.Contains("drop procedure") || temp.Contains("drop view") ||
                temp.Contains("create table") || temp.Contains("truncate table") ||
                temp.Contains("@@") || temp.Contains("--") ||
                temp.Contains("../") || temp.Contains("..\\") ||
                temp.Contains("/*") || temp.Contains("javascript") ||
                temp.Contains("alert") || temp.Contains("script") ||
                temp.Contains("prompt") ||
                temp.Contains("onmouseover"))
            {
                //string url = HttpContext.Current.Request.Url.Host;
                HttpContext.Current.Response.Redirect("/",false);
            }

            return temp;
        }

        public string GetSQLDateFormat(string sdate)
        {
            Regex dateRegex = new Regex(@"^(\d{1,2})(\/|-|\.)(\d{1,2})(\/|-|\.)(\d{4})$", RegexOptions.Compiled);

            Match matches = dateRegex.Match(sdate);

            StringBuilder sql_date = new StringBuilder();
            sql_date.Append(matches.Groups[5].ToString())
                    .Append("-")
                    .Append(matches.Groups[3].ToString())
                    .Append("-")
                    .Append(matches.Groups[1].ToString());
            return sql_date.ToString();
        }

        public string GetValidFileName(string inFileName)
        {
            string returner = inFileName;
            returner = returner.Replace("ı", "i")
                               .Replace("ğ", "g")
                               .Replace("ü", "u")
                               .Replace("ş", "s")
                               .Replace("ö", "o")
                               .Replace("ç", "c")
                               .Replace("İ", "I")
                               .Replace("Ğ", "G")
                               .Replace("Ü", "U")
                               .Replace("Ş", "S")
                               .Replace("Ö", "O")
                               .Replace("Ç", "C");
            string regexSearch = string.Format("{0}{1}",
                                 new string(Path.GetInvalidFileNameChars()),
                                 new string(Path.GetInvalidPathChars()));
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            returner = r.Replace(returner, "_");
            return returner;
        }

        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " Gb";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " Mb";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " Kb";
            else
                size = String.Format("{0:##.##}", byteCount) + " Byte";

            return size;
        }

        public static bool IsDate2(string sdate)
        {
            DateTime dt;
            return DateTime.TryParse(sdate, out dt);
        }

        public static string ReadFile(string path)
        {
            StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open, System.IO.FileAccess.Read), Encoding.GetEncoding("utf-8"));
            FileInfo fi = new FileInfo(path);
            return File.Exists(path) ? sr.ReadToEnd() : string.Empty;
        }

        public static string ReadFile(string path, Encoding encoding)
        {
            StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open, System.IO.FileAccess.Read), encoding);
            FileInfo fi = new FileInfo(path);
            return File.Exists(path) ? sr.ReadToEnd() : string.Empty;
        }

        public static string GetMimeType(string extension)
        {
            string ret = string.Empty;
            switch (extension)
            {
                case ".doc":
                    ret = "application/msword";
                    break;
                case ".docx":
                    ret = "application/msword";
                    break;
                case ".xls":
                    ret = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    ret = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                    ret = "image/jpeg";
                    break;
                case ".gif":
                    ret = "image/gi";
                    break;
                case ".bmp":
                    ret = "image/bmp";
                    break;
                case ".pdf":
                    ret = "application/pdf";
                    break;
                case ".txt":
                    ret = "text/plain";
                    break;
                default:
                    ret = "text/plain";
                    break;
            }
            return ret;
        }

        public static DateTime GetSQLDate(string value)
        {
            string pattern = @"^(\d{1,2})(/|-|.)(\d{1,2})(/|-|.)(\d{4})$";
            int year = Convert.ToInt32(CmsHelper.RegexReplace(value, pattern, "$5"));
            int month = Convert.ToInt32(CmsHelper.RegexReplace(value, pattern, "$3"));
            int day = Convert.ToInt32(CmsHelper.RegexReplace(value, pattern, "$1"));
            return new DateTime(year, month, day);
        }

        public static string EncodeURL(string filename)
        {
            filename = filename.Replace("İ", "I");
            filename = filename.Replace("ı", "i");
            filename = filename.Replace("ç", "c");
            filename = filename.Replace("Ç", "C");
            filename = filename.Replace("ğ", "g");
            filename = filename.Replace("Ğ", "G");
            filename = filename.Replace("ş", "s");
            filename = filename.Replace("Ş", "S");
            filename = filename.Replace("ü", "u");
            filename = filename.Replace("Ü", "U");
            filename = filename.Replace("Ö", "O");
            filename = filename.Replace("ö", "o");
            filename = filename.Replace("&", "%and");
            filename = filename.Replace(" ", "%spc");
            return filename;
        }

        public static string[] GetUrlParams(System.Web.HttpRequest Request)
        {
            string UrlFull = Request.Url.ToString();
            string QS = "";
            string WebPort = Request.ServerVariables["SERVER_PORT"].ToString();
            if (UrlFull.Contains("?404;"))
            {
                //This is IIS 6
                QS = UrlFull.Split(new string[] { ":" + WebPort + "/" }, StringSplitOptions.None)[1].Replace("?", "&");
            }
            else
            {
                //This is IIS 7 or newer
                QS = Request.QueryString.ToString().Replace("%2f", "/");

            }

            if (QS.EndsWith("/"))
                QS = QS.Substring(0, QS.Length - 1);

            return QS.Split(new string[] { "/" }, StringSplitOptions.None);
        }

        public static List<EuroCMS.Model.vAspNetMembershipUser> GetUsersByPermission(string contentType, string contentId, string permission)
        {
            List<EuroCMS.Model.vAspNetMembershipUser> returnList = new List<EuroCMS.Model.vAspNetMembershipUser>();

            string content = "";
            content = contentType.Trim().ToLower();
            permission = permission.Trim().ToLower();

            EuroCMS.Model.vAspNetMembershipUserRepository vamur = new EuroCMS.Model.vAspNetMembershipUserRepository();
            EuroCMS.Model.vAspNetMembershipUserService vMembershipUserService = new EuroCMS.Model.vAspNetMembershipUserService(vamur);

            List<EuroCMS.Model.vAspNetMembershipUser> listAllUser = new List<EuroCMS.Model.vAspNetMembershipUser>();
            listAllUser = vMembershipUserService.GetAll().Where(mu => mu.IsApproved && !mu.IsLockedOut).ToList();
            EuroCMS.Model.BaseDbContext baseContext = new EuroCMS.Model.BaseDbContext();
            foreach (EuroCMS.Model.vAspNetMembershipUser user in listAllUser)
            {
                bool isAdd = false;

                List<Guid> listUserInRoles = new List<Guid>();
                listUserInRoles = baseContext.GetAllUsersInRoles().Where(ur => ur.UserId == user.UserId).Select(s => s.RoleId).ToList();

                List<EuroCMS.Model.vw_aspnet_Roles> listUserRoles = new List<EuroCMS.Model.vw_aspnet_Roles>();
                listUserRoles = baseContext.GetAllRoles().Where(r => listUserInRoles.Contains(r.RoleId)).ToList();

                string[] userRoles = listUserRoles.Select(s => s.RoleName).ToArray();

                if (userRoles.Contains("Administrator"))
                {
                    isAdd = true;
                }
                else
                {
                    string id = contentId;

                    if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(permission) && !string.IsNullOrEmpty(id) && !id.Equals("-1"))
                    {
                        EuroCMS.Model.BaseDbContext baseDBContext = new EuroCMS.Model.BaseDbContext();
                        isAdd = baseDBContext.HasPermission(String.Join(",", userRoles), permission.ToLower(), id.ToLower(), content.ToLower()).Count() > 0 ? true : false;

                    }
                }

                if (isAdd)
                {
                    if (!returnList.Contains(user))
                    {
                        returnList.Add(user);
                    }
                }
            }

            return returnList;
        }

        public static string GetArticleAliasOrURL(int articleId, string zoneId)
        {
            string returnVal = "";
            int zoneID = 0;
            zoneId = zoneId.Trim();
            if (!string.IsNullOrEmpty(zoneId))
            {
                if (IsNumeric(zoneId))
                {
                    zoneID = Convert.ToInt32(zoneId);
                }
            }
            CmsDbContext dbContext = new CmsDbContext();

            if (articleId != null)
            {
                if (articleId > 0)
                {
                    vArticlesZonesFull articleZoneFull = new vArticlesZonesFull();
                    if (zoneID > 0)
                    {
                        articleZoneFull = dbContext.vArticlesZonesFulls.Where(az => az.ArticleID == articleId && az.ZoneID == zoneID).FirstOrDefault();
                        if (articleZoneFull == null)
                        {
                            articleZoneFull = dbContext.vArticlesZonesFulls.Where(az => az.ArticleID == articleId).FirstOrDefault();
                        }
                    }
                    else
                    {
                        articleZoneFull = dbContext.vArticlesZonesFulls.Where(az => az.ArticleID == articleId).FirstOrDefault();
                    }

                    if (articleZoneFull != null)
                    {
                        string azAlias = (string.IsNullOrEmpty(articleZoneFull.ArticleZoneAlias) ? "" : articleZoneFull.ArticleZoneAlias);
                        string articleDetailUrl = "";

                        if (zoneID > 0)
                        {
                            articleDetailUrl = CmsHelper.getContentLinkAlias(zoneID.ToString(), articleId.ToString(), articleZoneFull.SiteName.ToString(), articleZoneFull.ZoneGroupName.ToString(), articleZoneFull.ZoneName.ToString(), articleZoneFull.Headline.ToString(), "");
                        }
                        else
                        {
                            articleDetailUrl = CmsHelper.getContentLinkAlias(articleZoneFull.ZoneID.ToString(), articleId.ToString(), articleZoneFull.SiteName.ToString(), articleZoneFull.ZoneGroupName.ToString(), articleZoneFull.ZoneName.ToString(), articleZoneFull.Headline.ToString(), "");
                        }

                        articleDetailUrl = (string.IsNullOrEmpty(azAlias) ? articleDetailUrl : azAlias);
                        if (articleDetailUrl.Length > 1)
                        {
                            articleDetailUrl = (articleDetailUrl.Substring(0, 1) == "/" ? articleDetailUrl.Substring(1, articleDetailUrl.Length - 1) : articleDetailUrl);
                        }
                        returnVal = articleDetailUrl;
                    }
                }
            }

            return returnVal;
        }

        public static void SaveErrorLog(Exception ex, string comment, bool inCms)
        {
            try
            {
                ErrorLog el = new ErrorLog();
                el.Comment = comment;
                /*el.ControllerName = filterContext.RouteData.Values["controller"].ToString();
                el.ActionName = filterContext.RouteData.Values["action"].ToString();*/

                #region Line Number
                try
                {
                    var st = new System.Diagnostics.StackTrace(ex, true);
                    var frame = st.GetFrame(0);
                    el.LineNumber = frame.GetFileLineNumber();
                    if (el.LineNumber <= 0)
                    {
                        el.LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                    }
                }
                catch
                {
                    //Global hatalarda line number alamıyor 
                }
                #endregion

                el.Message = ex.Message;
                el.InnerException = string.Empty;
                if (ex.InnerException != null)
                {
                    el.InnerException = ex.InnerException.Message;
                }
                el.AbsolutePath = HttpContext.Current.Request.Url.AbsoluteUri;

                el.IsInCms = inCms;
                el.LogDate = DateTime.Now;
                el.StackTrace = ex.StackTrace;
                #region User boş olabilir
                //try
                //{
                //    el.UserId = (Guid)Membership.GetUser().ProviderUserKey;
                //}
                //catch
                //{
                //}
                #endregion
                el.IP = GetCurrentIP();

                if (el.IsInCms)
                {
                    #region Action & Controller
                    if (!string.IsNullOrEmpty(el.AbsolutePath))
                    {
                        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.Segments[2]))
                        {
                            el.ControllerName = HttpContext.Current.Request.Url.Segments[2];
                            if (el.ControllerName.Contains("/"))
                            {
                                el.ControllerName = el.ControllerName.Replace("/", "");
                            }
                        }
                        if (HttpContext.Current.Request.Url.Segments.Count() > 3)
                        {
                            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.Segments[3]))
                            {
                                el.ActionName = HttpContext.Current.Request.Url.Segments[3];
                                if (el.ActionName.Contains("/"))
                                {
                                    el.ActionName = el.ActionName.Replace("/", "");
                                }
                            }
                        }
                        else
                        {
                            el.ActionName = "Index";
                        }
                    }
                    #endregion
                }

                //kaydet

                CmsDbContext dbContext = new CmsDbContext();
                dbContext.ErrorLogs.Add(el);
                dbContext.SaveChanges();
            }
            catch
            {
                //hata oluşursa işlem yapma
            }
        }

        public static string StringToAlphaNumeric(string text, bool isRegex)
        {
            string returnVal = "";
            try
            {
                var config = WebConfigurationManager.OpenWebConfiguration("~/");
                string urlRegexValue = config.AppSettings.Settings["URLRegex"] != null && !string.IsNullOrEmpty(config.AppSettings.Settings["URLRegex"].Value) ? config.AppSettings.Settings["URLRegex"].Value : "";

                text = HttpUtility.HtmlDecode(text);
                text = HttpUtility.UrlDecode(text);

                returnVal = text.ToLower().Trim();

                string cleanText = "abcdefghijklmnoprstuvyzqwx0123456789/" + (isRegex ? urlRegexValue : "");

                returnVal = returnVal.Replace("ı", "i");
                returnVal = returnVal.Replace("ğ", "g");
                returnVal = returnVal.Replace("ü", "u");
                returnVal = returnVal.Replace("ş", "s");
                returnVal = returnVal.Replace("ö", "o");
                returnVal = returnVal.Replace("ç", "c");

                foreach (char item in returnVal)
                {
                    if (!cleanText.Contains(item))
                    {
                        returnVal = returnVal.Replace(item.ToString(), "-");
                    }
                }


                while (returnVal.Contains("--"))
                {
                    returnVal = returnVal.Replace("--", "-");
                }

                while (returnVal.Contains("//"))
                {
                    returnVal = returnVal.Replace("//", "/");
                }

                while (!cleanText.Contains(returnVal.Substring(returnVal.Length - 1, 1)))
                {
                    returnVal = returnVal.Remove(returnVal.Length - 1, 1);
                }

                while (!cleanText.Contains(returnVal.Substring(0, 1)))
                {
                    returnVal = returnVal.Remove(0, 1);
                }

            }
            catch (Exception ex)
            {

                return "";
            }

            return returnVal;
        }

        #region SQL Backup
        public static bool SqlBackup(string filePath)
        {
            bool result = false;
            try
            {
                //System.Configuration.ConfigurationManager.AppSettings["EuroMsgWebServiceUserName"]
                if (string.IsNullOrEmpty(filePath))
                {
                    filePath = System.Configuration.ConfigurationManager.AppSettings["BackupPath"];
                }
                string fileName = string.Empty;
                fileName = "Backup - " + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".bak";
                if (filePath.EndsWith("\\"))
                {
                    filePath += fileName;
                }
                else
                {
                    filePath = filePath + "\\" + fileName;
                }

                #region Get DbName
                string dbName = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["eurocms.db"].ConnectionString;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                dbName = builder.InitialCatalog;
                #endregion

                string command = "BACKUP DATABASE " + dbName + " TO  DISK = '" + filePath + "'";
                DataSet ds = DbHelper.ExecuteSQLString(command);

                result = true;
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex, "Cannot Backup Database", true);
                result = false;
            }

            return result;
        }
        public static bool SqlBackup()
        {
            bool result = false;
            try
            {
                result = SqlBackup(string.Empty);
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex, "Cannot Backup Database", true);
                result = false;
            }

            return result;
        }
        #endregion
        /// <summary>
        /// Açıklaması olan ilk method! Belirlenen url yapısına göre alias oluşturur.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="zoneId"></param>
        public static string CreateAliasWithUrlStructure(int articleId, int zoneId)
        {
            CmsDbContext dbContext = new CmsDbContext();
            string alias = string.Empty;
            string zoneAlias = string.Empty;
            try
            {
                vArticlesZonesFull vArticle = dbContext.vArticlesZonesFulls.AsNoTracking().Where(x => x.ArticleID == articleId && x.ZoneID == zoneId).FirstOrDefault();
                if (vArticle != null)
                {
                    zoneAlias = vArticle.ZoneAlias;
                }
                else
                {
                    vArticle = dbContext.vArticlesZonesFulls.AsNoTracking().Where(x => x.ArticleID == articleId).FirstOrDefault();   //rastgele bi tane geti
                    zoneAlias = dbContext.Zones.Where(x => x.Id == zoneId).FirstOrDefault().Alias;
                }

                #region vArticle var
                int domainId = dbContext.Domains.Where(x => x.Names.Contains(vArticle.DomainName)).FirstOrDefault() != null ? dbContext.Domains.Where(x => x.Names.Contains(vArticle.DomainName)).FirstOrDefault().Id : -1;
                if (domainId != -1)
                {
                    URLStructure srlStr = dbContext.URLStructures.Where(x => x.DomainID == domainId).FirstOrDefault();
                    string urlStructure = srlStr != null ? srlStr.Structure : string.Empty;
                    string prefix = srlStr != null ? srlStr.Prefix : string.Empty;
                    if (!string.IsNullOrEmpty(urlStructure))
                    {
                        #region Url Structure
                        urlStructure = urlStructure.ToLower().Trim();
                        List<string> structureList = new List<string>();
                        structureList = urlStructure.Replace("#", "").Split('/').ToList();

                        //string 
                        string langAlias = vArticle.LanguageAlias;
                        string siteAlias = vArticle.SiteAlias;
                        string zgAlias = vArticle.ZoneGroupAlias;
                        string articleAlias = CmsHelper.StringToAlphaNumeric(vArticle.Headline, false);

                        alias = urlStructure;
                        alias = alias.Replace("##lang##", langAlias);
                        alias = alias.Replace("##site##", siteAlias);
                        alias = alias.Replace("##zonegroup##", zgAlias);
                        alias = alias.Replace("##zone##", zoneAlias);
                        alias = alias.Replace("##article##", articleAlias);

                        if (!string.IsNullOrEmpty(prefix.Trim()))
                        {
                            alias = prefix + "/" + alias;
                        }

                        while (alias.Contains("//"))
                        {
                            alias = alias.Replace("//", "/");
                        }
                        #endregion

                        #region Çakışma Kontrol
                        List<ArticleZone> aZones = dbContext.ArticleZones.Where(x => x.AzAlias == alias).ToList();
                        aZones.Remove(aZones.Where(x => x.ArticleID == articleId && x.ZoneID == zoneId).FirstOrDefault());   //çakışanlar içinde varsa kendisini çıkar

                        if (aZones == null || aZones.Count == 0)
                        {
                            //ok  
                        }
                        else
                        {
                            //çakışma var 
                            int counter = 2;
                            while (dbContext.ArticleZones.Where(x => x.AzAlias == alias + "-" + counter).ToList().Count > 0)
                            {
                                counter++;
                            }
                            //son - cleanText + "-" + counter
                            alias = alias + "-" + counter;
                        }
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                alias = string.Empty;
                SaveErrorLog(ex, "Cannot create alias", true);
            }

            return alias;
        }

        public static string processLangRelations(int article_id, int zone_id, string template)
        {
            string result = template;
            string varText = string.Empty;
            string varCombo = string.Empty;

            if (result.Contains("##article_languages"))
            {
                DataTable dt = Dal.Instance.SelectArticleLanguageRelations(zone_id, article_id);
                if (dt.Rows.Count > 0)
                {
                    string atdurl = getContentLinkAlias(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][11].ToString());

                    varText += "<li><a href=\"" + atdurl + "\">" + dt.Rows[0][6].ToString() + "</a></li>" + Environment.NewLine;
                    varCombo += "<option value=\"" + atdurl + "\">" + dt.Rows[0][6].ToString() + "</option>" + Environment.NewLine;
                }
            }

            if (!string.IsNullOrEmpty(varText))
                varText = "<ul>" + Environment.NewLine + varText + Environment.NewLine + "</ul>" + Environment.NewLine;

            // Replace text version
            if (result.Contains("##article_languages_text##"))
                result = result.Replace("##article_languages_text##", varText);

            // Replace combo version
            if (result.Contains("##article_languages_combo##"))
                result = result.Replace("##article_languages_combo##", varCombo);

            return result;
        }

        #region  getSTFHTML
        public static string[] getSTFHTML(string stft_id)
        {
            string s = GetApplication("STF_FORM_" + stft_id);
            string stft_form_html = string.Empty;
            string stft_mail_html = string.Empty;
            string stft_subject = string.Empty;
            string stft_from_name = string.Empty;
            string stft_thanks = string.Empty;
            string stft_wh = string.Empty;
            string stft_status = string.Empty;
            string stft_omniture_function = string.Empty;
            string[] returns = new string[10];

            if (string.IsNullOrEmpty(s) || !IsCacheActive())
            {
                DataTable dt = Dal.Instance.SelectStfTemplateHtml(Convert.ToInt32(stft_id));
                if (dt.Rows.Count > 0)
                {
                    stft_form_html = dt.Rows[0][2].ToString();
                    stft_mail_html = dt.Rows[0][4].ToString();
                    stft_subject = dt.Rows[0][5].ToString();
                    stft_from_name = dt.Rows[0][6].ToString();
                    stft_thanks = dt.Rows[0][3].ToString();
                    stft_wh = dt.Rows[0][7].ToString();
                    stft_status = dt.Rows[0][1].ToString();
                    stft_omniture_function = dt.Rows[0][8].ToString();

                    if (stft_status.Equals("A"))
                    {
                        stft_form_html = "'NA";
                        stft_mail_html = "'NA";
                    }

                    if (IsCacheActive())
                    {
                        SetApplication("STF_FORM_" + stft_id, stft_form_html);
                        SetApplication("STF_MAIL_" + stft_id, stft_mail_html);
                        SetApplication("STF_SUBJECT_" + stft_id, stft_subject);
                        SetApplication("STF_FROM_NAME_" + stft_id, stft_from_name);
                        SetApplication("STF_THANKS_" + stft_id, stft_thanks);
                        SetApplication("STF_WH_" + stft_id, stft_wh);
                        SetApplication("STF_OMNITURE_FUNCTION_" + stft_id, stft_omniture_function);
                    }
                }
            }
            else
            {
                stft_form_html = GetApplication("STF_FORM_" + stft_id);
                stft_mail_html = GetApplication("STF_MAIL_" + stft_id);
                stft_subject = GetApplication("STF_SUBJECT_" + stft_id);
                stft_from_name = GetApplication("STF_FROM_NAME_" + stft_id);
                stft_thanks = GetApplication("STF_THANKS_" + stft_id);
                stft_wh = GetApplication("STF_WH_" + stft_id);
                stft_omniture_function = GetApplication("STF_OMNITURE_FUNCTION_" + stft_id);
            }

            returns[0] = stft_mail_html;
            returns[1] = stft_form_html;
            returns[2] = stft_subject;
            returns[3] = stft_from_name;
            returns[4] = stft_thanks;
            returns[5] = stft_wh;
            returns[6] = stft_omniture_function;

            return returns;
        }
        #endregion


        public static string GetUserName(Guid userId)
        {
            string userName = "";
            CmsDbContext dbContext = new CmsDbContext();
            vAspNetMembershipUser user = new vAspNetMembershipUser();
            user = dbContext.vAspNetMembershipUsers.Where(s => s.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                userName = user.UserName;
            }
            return userName;
        }

        public static string GetUserFullName(Guid userId)
        {
            string userFullName = "";
            CmsDbContext dbContext = new CmsDbContext();
            vAspNetMembershipUser user = new vAspNetMembershipUser();
            user = dbContext.vAspNetMembershipUsers.Where(s => s.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                var userProfile = System.Web.Profile.ProfileBase.Create(user.UserName, false);
                userFullName = userProfile.GetPropertyValue("System.FullName").ToString().Trim();
            }
            return userFullName;
        }

        public static string HtmlAndUrlDecode(string text)
        {
            text = HttpUtility.HtmlDecode(text);
            text = HttpUtility.UrlDecode(text);
            return text;
        }

    }
}
