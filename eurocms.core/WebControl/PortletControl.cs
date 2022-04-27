using EuroCMS.Core;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// <img alt="##portlet_9##" classpager="" exclude="" hspace="" id="101_10_0" include="" lang="" menuonclickfunction="" menusingleitem="" pagercount="0" pagerheader="" pagerlocation="0" pheader="" prevnext="" seperator="" src="/cms/Content/img/icon_article.gif" title="Haberler" />​
// <cms:Portlet  PortletID="9" Zone="101" ItemCount="10" ItemOrdering="1" Header="" ClassName="" ContainerTag="" DisplayArticles="" ExternalArticles="" PagerClassName="" PagerCount="" PagerPosition="" PagerHeader="" PagerCaptions="" PagerSeperator=""></cms:Portlet>
[assembly: TagPrefix("EuroCMS.WebControl", "cms")]
namespace EuroCMS.WebControl
{
    [PartialCaching(10)]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Portlet runat=server></{0}:Portlet>")]

    public class Portlet : System.Web.UI.WebControls.WebControl, ICmsControl
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
        private string _PortletID = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PortletID
        {
            get
            {
                return _PortletID;
            }

            set
            {
                _PortletID = value;
            }
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

        private int _ItemCount = 0;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(1)]
        [Localizable(true)]
        public int ItemCount
        {
            get
            {
                return _ItemCount;
            }

            set
            {
                _ItemCount = value;
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

        private string _Header = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Header
        {
            get
            {
                return _Header;
            }

            set
            {
                _Header = value;
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

        private string _DisplayArticles = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string DisplayArticles
        {
            get
            {
                return _DisplayArticles;
            }

            set
            {
                _DisplayArticles = value;
            }
        }

        private string _ExternalArticles = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ExternalArticles
        {
            get
            {
                return _ExternalArticles;
            }

            set
            {
                _ExternalArticles = value;
            }
        }


        //Pager

        private string _PagerClassName = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PagerClassName
        {
            get
            {
                return _PagerClassName;
            }

            set
            {
                _PagerClassName = value;
            }
        }

        private string _PagerCount = "";


        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PagerCount
        {
            get
            {
                return _PagerCount;
            }

            set
            {
                _PagerCount = value;
            }
        }


        private string _PagerPosition = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PagerPosition
        {
            get
            {
                return _PagerPosition;
            }

            set
            {
                _PagerPosition = value;
            }
        }
        //ufuk
        private string _ExcludeSelf = "0";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ExcludeSelf
        {
            get
            {
                return _ExcludeSelf;
            }

            set
            {
                _ExcludeSelf = value;
            }
        }
        //ufuk

        private string _PagerHeader = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PagerHeader
        {
            get
            {
                return _PagerHeader;
            }

            set
            {
                _PagerHeader = value;
            }
        }

        private string _PagerCaptions = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PagerCaptions
        {
            get
            {
                return _PagerCaptions;
            }

            set
            {
                _PagerCaptions = value;
            }
        }


        private string _PagerSeperator = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PagerSeperator
        {
            get
            {
                return _PagerSeperator;
            }

            set
            {
                _PagerSeperator = value;
            }
        }

        public string DisplayName
        {
            get { return "EuroCMS Portlet Control"; }
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
            get { return "Ahmet Kunduracı"; }
        }


        //private bool _viewStateEnabled = false;
        //protected override void LoadViewState(object savedState)
        //{
        //    if (_viewStateEnabled)
        //        base.LoadViewState(savedState);
        //}

        //public override bool EnableViewState
        //{
        //    get
        //    {
        //        return base.EnableViewState;
        //    }
        //    set
        //    {
        //        _viewStateEnabled = value;
        //        base.EnableViewState = value;
        //    }
        //}
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


            GlobalVars vars = new GlobalVars();
            string portlet_container = "div";
            string result = string.Empty;
            string portlet_out = string.Empty;
            string pager_html = string.Empty;
            string portlet_temp = PortletID;
            bool optioned = false;
            string[] pc_temp;
            string portlet_exself = string.Empty;
            DateTime started = DateTime.Now;
            if (PagerCount == "")
                PagerCount = "0";
            if (PagerPosition == "")
                PagerPosition = "v";
            int PageCurrent = 1;
            string pageStr = CmsHelper.GetQSVal(HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString(), "p");
            if (!string.IsNullOrEmpty(pageStr))
            {
                if (CmsHelper.IsNumeric(pageStr))
                {
                    PageCurrent = Convert.ToInt32(pageStr);

                }
                else
                {
                    PageCurrent = 1;
                }

            }
            if (string.IsNullOrEmpty(PagerClassName))
            {
                PagerClassName = "pager";
            }

            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            portlet_out = CmsHelper.getPortletData(Convert.ToInt32(PortletID), Convert.ToInt32(Zone), Convert.ToInt32(ItemCount), Convert.ToInt32(ItemOrdering), Convert.ToInt32(ArticleDetails["article_id"]), ClassName, optioned, ExcludeSelf.ToString(), ContainerTag, DisplayArticles, ExternalArticles, PagerHeader, Convert.ToInt32(PagerCount), Convert.ToInt32(ArticleDetails["zone_id"]));
            string PageEnd = string.Empty;


            // Portlet Status Control
            int iPortletId = Convert.ToInt32(PortletID);
       

            CmsDbContext dbContext = new CmsDbContext();
            EuroCMS.Model.Portlet currentPortlet = new EuroCMS.Model.Portlet();
            currentPortlet = dbContext.Portlets.Where(s => s.ID == iPortletId).FirstOrDefault();

            if (currentPortlet == null)
            {
                Control child = Page.ParseControl("");
                child.Page = Page;
                output.Write(CmsHelper.RenderControl(child));
                return;
            }

            if (currentPortlet.Status == 0)
            {
                Control child = Page.ParseControl("");
                child.Page = Page;
                output.Write(CmsHelper.RenderControl(child));
                return;
            }

            if (Convert.ToInt32(PagerCount) > 0)
            {
                PageEnd = PagerCount;
            }
            else
            {
                PageEnd = "0";
            }

            if (Convert.ToInt32(PagerCount) > 0)
                pager_html = CmsHelper.getPortletPagerHTML(portlet_temp, PageCurrent, Convert.ToInt32(HttpContext.Current.Session["page_count"]), 1, Convert.ToInt32(PageEnd), PagerClassName, ItemCount);

            pc_temp = PagerClassName.Split(';');

            DateTime ended = DateTime.Now;
            TimeSpan ts = ended - started;

            if (optioned)
            {
                // option value.. no DIV
                portlet_out = "<!-- Processed in " + ts.TotalMilliseconds + "ms -->" + portlet_out.Replace("##listed_zone_name##", PagerClassName);
                result = result.Replace(portlet_temp, portlet_out + Environment.NewLine); // Final
            }
            else
            {
                if (pc_temp.Length > 0)// Got multi class
                {
                    // Apply Portlet Header
                    if (!string.IsNullOrEmpty(Header) && !string.IsNullOrEmpty(portlet_out))
                        portlet_out = "<div class=\"" + pc_temp[0] + "_header\">" + Header + "</div>" + Environment.NewLine + portlet_out;

                    // Apply Pager
                    portlet_out = CmsHelper.applyPortletPager(portlet_out, pager_html, PagerPosition);

                    // Adding header and footer (Murat HOŞVER)

                    portlet_out = CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "header") + portlet_out + CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "footer");

                    // End

                    portlet_out = portlet_out.Replace("##listed_zone_name##", ClassName);

                    ended = DateTime.Now;
                    ts = ended - started;

                    portlet_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                    result = result.Replace(portlet_temp, Environment.NewLine + portlet_out + Environment.NewLine); // Final
                }
                else
                {
                    // Apply Portlet Header
                    if (!string.IsNullOrEmpty(Header) && !string.IsNullOrEmpty(portlet_out))
                        portlet_out = "<div class=\"" + ClassName + "_header\">" + Header + "</div>" + Environment.NewLine + portlet_out;

                    if (portlet_container.Equals("NA")) //No container
                    {
                        portlet_out = CmsHelper.applyPortletPager(portlet_out, pager_html, PagerPosition);

                        // Adding header and footer (Murat HOŞVER)
                        portlet_out = CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "header") + portlet_out + CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "footer");

                        // End

                        // Replace Portlet
                        portlet_out = portlet_out.Replace("##listed_zone_name##", ClassName);

                        ended = DateTime.Now;
                        ts = ended - started;

                        portlet_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                        result = result.Replace(portlet_temp, Environment.NewLine + portlet_out + Environment.NewLine); // Final
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ClassName))
                            ClassName = " class=\"" + ClassName + "\"";

                        portlet_out = "<" + portlet_container + ClassName + ">" + Environment.NewLine + portlet_out + Environment.NewLine + "</" + portlet_container + ">" + Environment.NewLine;

                        // Apply Pager
                        portlet_out = CmsHelper.applyPortletPager(portlet_out, pager_html, PagerPosition);

                        // Adding header and footer (Murat HOŞVER)
                        portlet_out = CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "header") + portlet_out + CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "footer");

                        // End

                        // Replace Portlet
                        portlet_out = portlet_out.Replace("##listed_zone_name##", ClassName);

                        portlet_out += Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms -->";

                        result = result.Replace(portlet_temp, Environment.NewLine + portlet_out + Environment.NewLine); // Final
                    }
                }
            }
            result = portlet_out;
            result = result.Replace(Constansts.PCSS_END, CmsHelper.getPortletHTML(Convert.ToInt32(PortletID), "css") + Environment.NewLine + Constansts.PCSS_END);

            Control childLast = Page.ParseControl(HttpUtility.HtmlDecode(result));

            childLast.Page = Page;


            output.Write(CmsHelper.RenderControl(childLast));

        }
    }


}
