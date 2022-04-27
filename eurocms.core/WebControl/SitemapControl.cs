using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("EuroCMS.WebControl", "cms")]

namespace EuroCMS.WebControl
{


    public enum SitemapType
    {
        DisplayOnlyZoneLevel = 0,
        DisplayZonesAndArticle = 1
    }

    public enum ItemOrdering
    {
        Article_StartDate_ASC = 0,
        Article_StartDate_DESC = 14,
        Article_CreateDate_ASC = 15,
        Article_CreateDate_DESC = 16,
        Article_LastUpdate = 1,
        Article_Headline = 2,
        Article_Order_Asc = 3,
        Article_Order_Desc = 11,
        Article_Custom_Date_1_DESC = 4,
        Article_Custom_Date_1_ASC = 12,
        Article_Custom_Date_2_DESC = 5,
        Article_Custom_Date_2_ASC = 13,
        Article_Custom_Flag_1 = 6,
        Article_Custom_Flag_2 = 7,
        Article_Custom_Flag_3 = 8,
        Article_Custom_Flag_4 = 9,
        Article_Custom_Flag_5 = 10,
        Article_Custom_1_ASC = 17,
        Article_Custom_1_DESC = 18,
        Article_Custom_2_ASC = 19,
        Article_Custom_2_DESC = 20,
        Article_Custom_3_ASC = 21,
        Article_Custom_3_DESC = 22,
        Article_Custom_4_ASC = 23,
        Article_Custom_4_DESC = 24,
        Article_Custom_5_ASC = 25,
        Article_Custom_5_DESC = 26
    }

    [PartialCaching(10)]
    [DefaultProperty("Zone")]
    [ToolboxData("<{0}:Sitemap runat=server></{0}:Sitemap>")]
    public class Sitemap : System.Web.UI.WebControls.WebControl, ICmsControl
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

        private int _Zone = 0;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int Zone
        {
            get
            {
                return _Zone;
            }

            set
            {
                _Zone = value;
            }
        }

        private int _MenuDepth = 0;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(1)]
        [Localizable(true)]
        public int MenuDepth
        {
            get
            {
                return _MenuDepth;
            }

            set
            {
                _MenuDepth = value;
            }
        }

        private ItemOrdering _ItemOrdering = ItemOrdering.Article_Order_Asc;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(1)]
        [Localizable(true)]
        public ItemOrdering ItemOrdering
        {
            get
            {
                return _ItemOrdering;
            }

            set
            {
                _ItemOrdering = value;
            }
        }

        private string _ClassName = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ClassName
        {
            get
            {
                return _ClassName;
            }

            set
            {
                _ClassName = value;
            }
        }

        private string _ContainerTag = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("ul")]
        [Localizable(true)]
        public string ContainerTag
        {
            get
            {
                return _ContainerTag;
            }

            set
            {
                _ContainerTag = value;
            }
        }

        private SitemapType _SitemapType = SitemapType.DisplayZonesAndArticle;

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public SitemapType SitemapType
        {
            get
            {
                return (_SitemapType == null ? SitemapType.DisplayZonesAndArticle : _SitemapType);
            }

            set
            {
                _SitemapType = value;
            }
        }


        private string _ExcludeZoneIds = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public string ExcludeZoneIds
        {
            get
            {
                return (string.IsNullOrEmpty(_ExcludeZoneIds) ? "" : _ExcludeZoneIds);
            }

            set
            {
                _ExcludeZoneIds = value;
            }
        }

        private string _ExcludeArticleIds = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public string ExcludeArticleIds
        {
            get
            {
                return (string.IsNullOrEmpty(_ExcludeArticleIds) ? "" : _ExcludeArticleIds);
            }

            set
            {
                _ExcludeArticleIds = value;
            }
        }


        public string DisplayName
        {
            get { return "EuroCMS Sitemap Control"; }
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
            get { return ""; }
        }



        // Render Class Start

        class SiteMapSubItems
        {
            public string Name { get; set; }
            public string URL { get; set; }
            public List<SiteMapSubItems> Items { get; set; }
        }



        // Render Class End


        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("");
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            // Eski Site Map Start


            //string menu_temp = Page.Items["template"].ToString();
            //string menu_id = "a";
            //string menu_out = string.Empty;
            //string menu_class = ClassName;
            //string menucontainertagid = string.Empty;
            //string menu_container = ContainerTag;

            //Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            //DateTime started = DateTime.Now;

            //menu_out = CmsHelper.getMenuData(menu_id.ToString(), Zone.ToString(), MenuDepth.ToString(), Convert.ToInt32(ItemOrdering).ToString(), Convert.ToInt32(SitemapType).ToString(), Convert.ToInt32(ArticleDetails["zone_id"].ToString()), "sitemap", "", "", "", "");

            //DateTime ended = DateTime.Now;
            //TimeSpan ts = ended - started;
            //menu_out = menu_out + Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms menu_temp: " + menu_temp.Replace(">", "").Replace("<", "").Replace("##", "") + " -->";


            //if (!string.IsNullOrEmpty(menu_class))
            //    menu_class = " class=\"" + menu_class + "\"";

            //if (!string.IsNullOrEmpty(menucontainertagid))
            //    menucontainertagid = " id=\"" + menucontainertagid + "\"";

            //if (menu_container.ToUpper().Equals("NA"))
            //{
            //    menu_temp = menu_temp.Replace(menu_temp, Environment.NewLine + menu_out + Environment.NewLine);
            //}
            //else
            //{
            //    //menu_temp = menu_temp.Replace(menu_temp, "<" + menu_container + menu_class + menucontainertagid + ">" + Environment.NewLine + menu_out + Environment.NewLine + "</" + menu_container + ">" + Environment.NewLine);
            //    menu_temp = menu_out;
            //}
            //output.Write(menu_temp);

            // Eski Site Map End

            var result = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            result = GetAllItems();
            //result = serializer.Serialize(listSiteMapSubItems);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=");
            sb.Append("\"");
            sb.Append("text/javascript");
            sb.Append("\"");
            sb.Append(">");
            sb.Append("var siteMapResultJson" + Zone.ToString() + " = ");
            //sb.Append("\"");
            sb.Append(result);
            //sb.Append("\"");
            sb.Append(";");
            sb.Append("</script>");
            output.Write(sb.ToString());

        }



        public string GetAllItems()
        {
            var result = "";
            CmsDbContext dbContext = new CmsDbContext();
            int zoneId = 0;
            List<int> listExcludeArticleIds = new List<int>();
            List<int> listExcludeZoneIds = new List<int>();

            if (Zone == 0)
            {
                return "";
            }

            zoneId = Zone;

            int menuDepth = MenuDepth;

            if (!string.IsNullOrEmpty(ExcludeArticleIds))
            {
                if (ExcludeArticleIds.Contains(","))
                {
                    listExcludeArticleIds = ExcludeArticleIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
                }
                else
                {
                    listExcludeArticleIds.Add(Convert.ToInt32(ExcludeArticleIds));
                }
            }
            else
            {
                listExcludeArticleIds = null;
            }

            if (!string.IsNullOrEmpty(ExcludeZoneIds))
            {
                if (ExcludeZoneIds.Contains(","))
                {
                    listExcludeZoneIds = ExcludeZoneIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
                }
                else
                {
                    listExcludeZoneIds.Add(Convert.ToInt32(ExcludeZoneIds));
                }
            }
            else
            {
                listExcludeZoneIds = null;
            }



            List<vArticlesZonesFull> listVArticleZoneFull = new List<vArticlesZonesFull>();
            listVArticleZoneFull = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ZoneID == zoneId && vaz.Status == 1 && vaz.ZoneStatus == "A" && vaz.NavigationDisplay > 0 && vaz.NavigationZoneID != zoneId).ToList();

            if (listVArticleZoneFull == null)
            {
                return "";
            }
            if (listExcludeZoneIds != null)
            {
                listVArticleZoneFull = listVArticleZoneFull.Where(vaz => !listExcludeZoneIds.Contains(vaz.ZoneID)).ToList();
            }
            if (listExcludeArticleIds != null)
            {
                listVArticleZoneFull = listVArticleZoneFull.Where(vaz => !listExcludeArticleIds.Contains(vaz.ArticleID)).ToList();
            }


            listVArticleZoneFull = ListOrder(ItemOrdering, listVArticleZoneFull);

            List<SiteMapSubItems> listSiteMapSubItems = new List<SiteMapSubItems>();

            foreach (vArticlesZonesFull item1 in listVArticleZoneFull)
            {
                SiteMapSubItems subItems1 = new SiteMapSubItems();
                int navigationZoneId = 0;
                string name = "";
                int index = 0;

                if (item1.NavigationZoneID > 0)
                {
                    navigationZoneId = item1.NavigationZoneID;
                }

                name = item1.Headline.Trim();
                subItems1.Name = name;
                subItems1.URL = GetItemURL(item1); //"ArticleId: " + item1.ArticleID.ToString() + " - ZoneId: " + item1.ZoneID.ToString() + " - NavigationZoneId: " + item1.NavigationZoneID.ToString();
                subItems1.Items = new List<SiteMapSubItems>();
                List<vArticlesZonesFull> listVAZ = new List<vArticlesZonesFull>();
                listVAZ = GetListVAZ(navigationZoneId, listVAZ, listExcludeZoneIds, listExcludeArticleIds);

                if (listVAZ != null && menuDepth >= 1)
                {
                    if (listVAZ.Count() > 0)
                    {
                        foreach (vArticlesZonesFull item2 in listVAZ)
                        {
                            //subItems1.Items = new List<SiteMapSubItems>();
                            int navigationZoneId2 = 0;
                            SiteMapSubItems subItems2 = new SiteMapSubItems();
                            subItems2.Name = item2.Headline.Trim();
                            subItems2.URL = GetItemURL(item2); //"ArticleId: " + item2.ArticleID.ToString() + " - ZoneId: " + item2.ZoneID.ToString() + " - NavigationZoneId: " + item2.NavigationZoneID.ToString();
                            subItems2.Items = new List<SiteMapSubItems>();
                            subItems1.Items.Add(subItems2);

                            if (item2.NavigationZoneID > 0)
                            {
                                navigationZoneId2 = item2.NavigationZoneID;
                            }

                            List<vArticlesZonesFull> listVAZ2 = new List<vArticlesZonesFull>();
                            listVAZ2 = GetListVAZ(navigationZoneId2, listVAZ2, listExcludeZoneIds, listExcludeArticleIds);

                            if (listVAZ2 != null && menuDepth >= 2)
                            {
                                if (listVAZ2.Count() > 0)
                                {
                                    foreach (vArticlesZonesFull item3 in listVAZ2)
                                    {
                                        int navigationZoneId3 = 0;
                                        SiteMapSubItems subItems3 = new SiteMapSubItems();
                                        subItems3.Name = item3.Headline.Trim();
                                        subItems3.URL = GetItemURL(item3); //"ArticleId: " + item3.ArticleID.ToString() + " - ZoneId: " + item3.ZoneID.ToString() + " - NavigationZoneId: " + item3.NavigationZoneID.ToString();
                                        subItems3.Items = new List<SiteMapSubItems>();
                                        subItems2.Items.Add(subItems3);

                                        if (item3.NavigationZoneID > 0)
                                        {
                                            navigationZoneId3 = item3.NavigationZoneID;
                                        }

                                        List<vArticlesZonesFull> listVAZ3 = new List<vArticlesZonesFull>();
                                        listVAZ3 = GetListVAZ(navigationZoneId3, listVAZ3, listExcludeZoneIds, listExcludeArticleIds);

                                        if (listVAZ3 != null && menuDepth >= 3)
                                        {
                                            if (listVAZ3.Count() > 0)
                                            {
                                                foreach (vArticlesZonesFull item4 in listVAZ3)
                                                {

                                                    int navigationZoneId4 = 0;
                                                    SiteMapSubItems subItems4 = new SiteMapSubItems();
                                                    subItems4.Name = item4.Headline.Trim();
                                                    subItems4.URL = GetItemURL(item4); //"ArticleId: " + item4.ArticleID.ToString() + " - ZoneId: " + item4.ZoneID.ToString() + " - NavigationZoneId: " + item4.NavigationZoneID.ToString();
                                                    subItems4.Items = new List<SiteMapSubItems>();
                                                    subItems3.Items.Add(subItems4);

                                                    if (item4.NavigationZoneID > 0)
                                                    {
                                                        navigationZoneId4 = item4.NavigationZoneID;
                                                    }

                                                    List<vArticlesZonesFull> listVAZ4 = new List<vArticlesZonesFull>();
                                                    listVAZ4 = GetListVAZ(navigationZoneId4, listVAZ4, listExcludeZoneIds, listExcludeArticleIds);
                                                }
                                            }
                                        }

                                    }
                                }
                            }

                        }
                    }
                }

                // For End
                listSiteMapSubItems.Add(subItems1);

            }

            // Foreach End

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            result = serializer.Serialize(listSiteMapSubItems);
            return result;

        }


        public string GetItemURL(vArticlesZonesFull vaz)
        {
            string returnUrl = "";

            returnUrl = vaz.ArticleZoneAlias; //HttpContext.Current.Request.Url.AbsolutePath;
            if (string.IsNullOrEmpty(returnUrl))
            {

                returnUrl = "/" + CmsHelper.getContentLinkAlias(vaz.ZoneID.ToString(),
                                                     vaz.ArticleID.ToString(), vaz.SiteName,
                                                     vaz.ZoneGroupName, vaz.ZoneName, vaz.Headline, "");

            }
            if (vaz.ArticleType == 6)
            {
                var newUrl = vaz.ArticleTypeDetail.Replace("href=\"", "").Replace("\"","");
                returnUrl = newUrl;
            }

            returnUrl = returnUrl.Trim();

            if (returnUrl.Substring(0, 2) == "//")
            {
                returnUrl = returnUrl.Substring(2, returnUrl.Length - 2);
            }

            returnUrl = (returnUrl.Substring(0, 1).ToString() == "/" ? returnUrl : "/" + returnUrl);



            return returnUrl;
        }

        public List<vArticlesZonesFull> GetListVAZ(int zoneId, List<vArticlesZonesFull> listVArticleZoneFull, List<int> listExcludeZoneIds, List<int> listExcludeArticleIds)
        {

            CmsDbContext dbContext = new CmsDbContext();

            listVArticleZoneFull = dbContext.vArticlesZonesFulls.Where(vaz => vaz.ZoneID == zoneId && vaz.Status == 1 && vaz.ZoneStatus == "A" && vaz.NavigationDisplay > 0 && vaz.NavigationZoneID != zoneId).ToList();

            if (listVArticleZoneFull == null)
            {
                return listVArticleZoneFull;
            }
            if (listExcludeZoneIds != null)
            {
                listVArticleZoneFull = listVArticleZoneFull.Where(vaz => !listExcludeZoneIds.Contains(vaz.ZoneID)).ToList();
            }
            if (listExcludeArticleIds != null)
            {
                listVArticleZoneFull = listVArticleZoneFull.Where(vaz => !listExcludeArticleIds.Contains(vaz.ArticleID)).ToList();
            }


            listVArticleZoneFull = ListOrder(ItemOrdering, listVArticleZoneFull);

            return listVArticleZoneFull;
        }

        public List<vArticlesZonesFull> ListOrder(ItemOrdering orderType, List<vArticlesZonesFull> listVArticleZoneFull)
        {

            switch (orderType)
            {
                case ItemOrdering.Article_StartDate_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.StartDate).ToList();
                    break;
                case ItemOrdering.Article_StartDate_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(od => od.StartDate).ToList();
                    break;
                case ItemOrdering.Article_CreateDate_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Created).ToList();
                    break;
                case ItemOrdering.Article_CreateDate_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(od => od.StartDate).ToList();
                    break;
                case ItemOrdering.Article_LastUpdate:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(od => od.Updated).ToList();
                    break;
                case ItemOrdering.Article_Headline:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Headline).ToList();
                    break;
                case ItemOrdering.Article_Order_Asc:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.AzOrder).ToList();
                    break;
                case ItemOrdering.Article_Order_Desc:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(od => od.AzOrder).ToList();
                    break;
                case ItemOrdering.Article_Custom_Date_1_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(od => od.Date1).ToList();
                    break;
                case ItemOrdering.Article_Custom_Date_1_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Date1).ToList();
                    break;
                case ItemOrdering.Article_Custom_Date_2_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(od => od.Date2).ToList();
                    break;
                case ItemOrdering.Article_Custom_Date_2_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Date2).ToList();
                    break;
                case ItemOrdering.Article_Custom_Flag_1:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Flag1).ToList();
                    break;
                case ItemOrdering.Article_Custom_Flag_2:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Flag2).ToList();
                    break;
                case ItemOrdering.Article_Custom_Flag_3:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Flag3).ToList();
                    break;
                case ItemOrdering.Article_Custom_Flag_4:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Flag4).ToList();
                    break;
                case ItemOrdering.Article_Custom_Flag_5:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Flag5).ToList();
                    break;
                case ItemOrdering.Article_Custom_1_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Custom1).ToList();
                    break;
                case ItemOrdering.Article_Custom_1_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(o => o.Custom1).ToList();
                    break;
                case ItemOrdering.Article_Custom_2_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Custom2).ToList();
                    break;
                case ItemOrdering.Article_Custom_2_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(o => o.Custom2).ToList();
                    break;
                case ItemOrdering.Article_Custom_3_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Custom3).ToList();
                    break;
                case ItemOrdering.Article_Custom_3_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(o => o.Custom3).ToList();
                    break;
                case ItemOrdering.Article_Custom_4_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Custom4).ToList();
                    break;
                case ItemOrdering.Article_Custom_4_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(o => o.Custom4).ToList();
                    break;
                case ItemOrdering.Article_Custom_5_ASC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderBy(o => o.Custom5).ToList();
                    break;
                case ItemOrdering.Article_Custom_5_DESC:
                    listVArticleZoneFull = listVArticleZoneFull.OrderByDescending(o => o.Custom5).ToList();
                    break;
                default:
                    break;
            }

            return listVArticleZoneFull;
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
    }
}
