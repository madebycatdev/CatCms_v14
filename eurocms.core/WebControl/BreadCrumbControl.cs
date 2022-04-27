using EuroCMS.Core;
using EuroCMS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{
    [PartialCaching(5)]
    [DefaultProperty("Seperator")]
    [ToolboxData("<{0}:Breadcrumb runat=server></{0}:Breadcrumb>")]
    public class Breadcrumb : System.Web.UI.WebControls.WebControl, ICmsControl
    {



        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        private ITemplate _content;
        protected override void CreateChildControls()
        {
            if (this.Content != null)
            {
                this.Controls.Clear();
                this.Content.InstantiateIn(this);
            }
            base.CreateChildControls();
        }

        //private string _Seperator = string.Empty;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue(">")]
        //[Localizable(true)]
        //public string Seperator
        //{
        //    get
        //    {

        //        return _Seperator;
        //    }

        //    set
        //    {
        //        _Seperator = value;
        //    }
        //}

        //private int _DeepLevel = 1;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue(1)]
        //[Localizable(true)]
        //public int DeepLevel
        //{
        //    get
        //    {
        //        return _DeepLevel;
        //    }

        //    set
        //    {
        //        _DeepLevel = value;
        //    }
        //}

        //private string _MainContainerClass = string.Empty;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("breadcrumb")]
        //[Localizable(true)]
        //public string MainContainerClass
        //{
        //    get
        //    {
        //        return _MainContainerClass;
        //    }

        //    set
        //    {
        //        _MainContainerClass = value;
        //    }
        //}

        //private string _MainContainerTag = string.Empty;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("ul")]
        //[Localizable(true)]
        //public string MainContainerTag
        //{
        //    get
        //    {
        //        return _MainContainerTag;
        //    }

        //    set
        //    {
        //        _MainContainerTag = value;
        //    }
        //}

        //private string _MainItemTag = string.Empty;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("li")]
        //[Localizable(true)]
        //public string MainItemTag
        //{
        //    get
        //    {
        //        return _MainItemTag;
        //    }

        //    set
        //    {
        //        _MainItemTag = value;
        //    }
        //}

        //private string _SubItemContainerTag = string.Empty;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("ul")]
        //[Localizable(true)]
        //public string SubItemContainerTag
        //{
        //    get
        //    {
        //        return _SubItemContainerTag;
        //    }

        //    set
        //    {
        //        _SubItemContainerTag = value;
        //    }
        //}

        //private string _SubItemTag = string.Empty;

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("li")]
        //[Localizable(true)]
        //public string SubItemTag
        //{
        //    get
        //    {
        //        return _SubItemTag;
        //    }

        //    set
        //    {
        //        _SubItemTag = value;
        //    }
        //}


        //private string _SubMenus = "0";

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("li")]
        //[Localizable(true)]
        //public string SubMenus
        //{
        //    get
        //    {
        //        return _SubMenus;
        //    }

        //    set
        //    {
        //        _SubMenus = value;
        //    }
        //}

        //private string _IncludeSiteName = "1";

        //[Bindable(true)]
        //[Category("Appearance")]
        //[Localizable(true)]
        //public string IncludeSiteName
        //{
        //    get
        //    {
        //        return _IncludeSiteName;
        //    }

        //    set
        //    {
        //        _IncludeSiteName = value;
        //    }
        //}

        //private string _IncludeZoneGName = "1";

        //[Bindable(true)]
        //[Category("Appearance")]
        //[Localizable(true)]
        //public string IncludeZoneGName
        //{
        //    get
        //    {
        //        return _IncludeZoneGName;
        //    }

        //    set
        //    {
        //        _IncludeZoneGName = value;
        //    }
        //}



        //private string _IncludeHeadline = "1";

        //[Bindable(true)]
        //[Category("Appearance")]
        //[Localizable(true)]
        //public string IncludeHeadline
        //{
        //    get
        //    {
        //        return _IncludeHeadline;
        //    }

        //    set
        //    {
        //        _IncludeHeadline = value;
        //    }
        //}
        public string DisplayName
        {
            get { return "EuroCMS Breadcrumb Control"; }
        }

        public string VersionName
        {
            get { return "v1.0"; }
        }

        public int VersionLevel
        {
            get { return 1; }
        }

        public string Author
        {
            get { return "Ramazan Dönmez"; }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Ul;
            }
        }

        protected override string TagName
        {
            get
            {
                return "";
            }
        }
        private string _BreadCrumbID = "1";

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public string BreadCrumbID
        {
            get
            {
                return _BreadCrumbID;
            }

            set
            {
                _BreadCrumbID = value;
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        private bool _viewStateEnabled = false;
        protected override void LoadViewState(object savedState)
        {
            if (_viewStateEnabled)
                base.LoadViewState(savedState);
        }

        public override bool EnableViewState
        {
            get
            {
                return base.EnableViewState;
            }
            set
            {
                _viewStateEnabled = value;
                base.EnableViewState = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {

            GlobalVars vars = new GlobalVars();
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            string result = Page.Items["template"].ToString();
            string breadcrumb_template = CmsHelper.regOp("breadcrumb_get_temp", result);
            string breadcrumb_id = BreadCrumbID;
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
                    string zone_id = ArticleDetails["zone_id"].ToString();
                    string article_id = ArticleDetails["article_id"].ToString();
                    string site_id = ArticleDetails["site_id"].ToString();
                    string site_name = ArticleDetails["site_name"].ToString();
                    string zone_group_id = ArticleDetails["zone_group_id"].ToString();
                    string zone_group_name = ArticleDetails["zone_group_name"].ToString();
                    string default_site_link = string.Empty;
                    string default_zone_gruop_link = string.Empty;

                    string b1 = CmsHelper.GetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id);
                    string b2 = CmsHelper.GetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id + "_OMNITURE");

                    if (!string.IsNullOrEmpty(b1) && CmsHelper.GetApplication(Constansts.CFG_BREADCRUMB_CACHE_ACTIVE).Equals("Y") && !string.IsNullOrEmpty(b2))
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




                            for (int i = 0; i < vars.BreadCrumbItems.Length / 7; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    vars.BreadCrumbItems[i, j] = "0";
                                }
                            }

                            vars = CmsHelper.getBreadCrumb(0, Convert.ToInt32(zone_id), vars);

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
                                    default_site_link = CmsHelper.getStructureDefaultLink(default_site_link, dt1.Rows[0][0].ToString());

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
                                    default_site_link = CmsHelper.getStructureDefaultLink(default_site_link, dt2.Rows[0][0].ToString());

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
                                                        breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + breadcrumb_sub_item_container + "><a href=\"" + CmsHelper.getContentLinkAlias(dr3[1].ToString(), bc_sub_article_id.ToString(), dr3[5].ToString(), dr3[4].ToString(), dr3[3].ToString(), bc_sub_headline, dr3[7].ToString()) + "\"><span" + bc_sub_selected + ">";

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

                                                breadcrumb_final = CmsHelper.trimBreadCrumbSeperator(breadcrumb_final, breadcrumb_seperator);
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
                                string menu_text = ArticleDetails["menu_text"].ToString();
                                string headline = ArticleDetails["headline"].ToString();

                                if (!string.IsNullOrEmpty(menu_text))
                                {
                                    //   breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_end breadcrumb_headline\"><span>" + menu_text + "</span></" + breadcrumb_main_item_container + ">" + Environment.NewLine;  without TSKB breadcrumb
                                    breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_end breadcrumb_headline\"><a><span>" + menu_text + "</span></a>" + ExtraBR(zone_id, article_id) + "</" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                    breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(menu_text) + ">";
                                }
                                else
                                {
                                    //   breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_end breadcrumb_headline\"><span>" + headline + "</span></" + breadcrumb_main_item_container + ">" + Environment.NewLine;  without TSKB breadcrumb
                                    breadcrumb_final_omniture = breadcrumb_final_omniture + CmsHelper.c2QSv2(headline) + ">";
                                    breadcrumb_final = breadcrumb_final + Environment.NewLine + "<" + breadcrumb_main_item_container + " class=\"breadcrumb_li_end breadcrumb_headline\"><a><span>" + headline + "</span></a>" + ExtraBR(zone_id, article_id) + "</" + breadcrumb_main_item_container + ">" + Environment.NewLine;
                                }

                                if (!string.IsNullOrEmpty(breadcrumb_seperator.Trim()))
                                {
                                    breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + breadcrumb_sub_item_container + ">" + breadcrumb_seperator + "</" + breadcrumb_sub_item_container + ">" + Environment.NewLine;
                                }
                            }

                            //breadcrumb_final = CmsHelper.trimBreadCrumbSeperator(breadcrumb_final, breadcrumb_seperator);

                            if (CmsHelper.Right(breadcrumb_final_omniture, 1).Equals(">"))
                            {
                                breadcrumb_final_omniture = breadcrumb_final_omniture.Substring(breadcrumb_final_omniture.Length - 2);
                            }

                            breadcrumb_final = breadcrumb_final + " </" + breadcrumb_main_container + ">" + Environment.NewLine;

                            DateTime ended = DateTime.Now;
                            TimeSpan ts = ended - started;

                            breadcrumb_final = breadcrumb_final + Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";
                            CmsHelper.SetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id, breadcrumb_final);
                            CmsHelper.SetApplication("BREADCRUMB_" + breadcrumb_id + "_" + zone_id + "_" + article_id + "_OMNITURE", breadcrumb_final_omniture);
                        }
                    }
                }
            }
            result = breadcrumb_final;
            if (CmsHelper.IsNumeric(breadcrumb_id_omniture))
                result = result.Replace(breadcrumb_template_omniture, breadcrumb_final_omniture);

            if (!string.IsNullOrEmpty(breadcrumb_template))
                result = result.Replace(breadcrumb_template, breadcrumb_final);



            output.Write(result);
        }

        private string ExtraBR(string ZoneID, string ArticleID)
        {
            string breadcrumb_final = string.Empty;

            DataTable dt3 = Dal.Instance.SelectArticlesByZoneForBreadcrumb(Convert.ToInt32(ZoneID));


            if (dt3.Rows.Count > 0)
            {

                string bc_sub_menu_text = string.Empty;
                string bc_sub_headline = string.Empty;
                string zone_group_name = string.Empty;
                int bc_sub_article_id = 0;

                string bc_sub_headline_temp = string.Empty;

                foreach (DataRow dr3 in dt3.Rows)
                {
                    bc_sub_menu_text = dr3[6].ToString();
                    bc_sub_headline = dr3[2].ToString();
                    bc_sub_article_id = Convert.ToInt32(dr3[0]);
                    zone_group_name = dr3[4].ToString();
                    bc_sub_headline_temp = bc_sub_headline;
                    if (zone_group_name != bc_sub_headline_temp && ArticleID != dr3[0].ToString())
                    {
                        breadcrumb_final = breadcrumb_final + Environment.NewLine + Environment.NewLine + Environment.NewLine + "<" + "ul" + "><li><a href=\"" + CmsHelper.getContentLinkAlias(dr3[1].ToString(), bc_sub_article_id.ToString(), dr3[5].ToString(), dr3[4].ToString(), dr3[3].ToString(), bc_sub_headline, dr3[7].ToString()) + "\"><span>";

                        if (!string.IsNullOrEmpty(bc_sub_menu_text.Trim()))
                        {
                            breadcrumb_final = breadcrumb_final + bc_sub_menu_text;
                        }
                        else
                        {
                            breadcrumb_final = breadcrumb_final + bc_sub_headline;
                        }

                        breadcrumb_final = breadcrumb_final + "</span></a></" + "ul" + ">" + Environment.NewLine;
                    }

                }

            }


            return breadcrumb_final;

        }


    }
}
