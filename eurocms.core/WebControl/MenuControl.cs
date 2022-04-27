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
    [PartialCaching(30)]
    [DefaultProperty("Zone")]
    [ToolboxData("<{0}:Menu runat=server></{0}:Menu>")]
    
    public class Menu : System.Web.UI.WebControls.WebControl, ICmsControl
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

        private string _MenuType = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string MenuType
        {
            get
            {
                return _MenuType;
            }

            set
            {
                _MenuType = value;
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

        private string _Position = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("v")]
        [Localizable(true)]
        public string Position
        {
            get
            {
                return _Position;
            }

            set
            {
                _Position = value;
            }
        }

        private string _ContainerTagId = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ContainerTagId
        {
            get
            {
                return _ContainerTagId;
            }

            set
            {
                _ContainerTagId = value;
            }
        }

        private bool _EliminateSignleItems = false;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool EliminateSignleItems
        {
            get
            {
                return _EliminateSignleItems;
            }

            set
            {
                _EliminateSignleItems = value;
            }
        }

        private bool _EliminateOnclikFunction = false;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool EliminateOnclikFunction
        {
            get
            {
                return _EliminateOnclikFunction;
            }

            set
            {
                _EliminateOnclikFunction = value;
            }
        }

        private string _SelectedItemClass = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string SelectedItemClass
        {
            get
            {
                return _SelectedItemClass;
            }

            set
            {
                _SelectedItemClass = value;
            }
        }

        private string _NotSelectedItemClass = string.Empty;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string NotSelectedItemClass
        {
            get
            {
                return _NotSelectedItemClass;
            }

            set
            {
                _NotSelectedItemClass = value;
            }
        }

        public string DisplayName
        {
            get { return "EuroCMS Menu Control"; }
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
        ////
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

            string result = Page.Items["template"].ToString();
            string menu_temp = string.Empty;
            string menu_id = "a";
            string menu_out = string.Empty;
            string menu_class = ClassName;
            string menucontainertagid = string.Empty;
            string menu_container = ContainerTag;
            Dictionary<string, object> ArticleDetails = (Dictionary<string, object>)Page.Items["Article_Details"];
            DateTime started = DateTime.Now;

            menu_out = CmsHelper.getMenuData(menu_id.ToString(), Zone.ToString(), MenuDepth.ToString(), Convert.ToInt32(ItemOrdering).ToString(), Position.ToString(), Convert.ToInt32(ArticleDetails["zone_id"].ToString()), MenuType.ToString(), ExternalArticles.ToString(), DisplayArticles.ToString(), EliminateSignleItems.ToString(), EliminateOnclikFunction.ToString());

            if (MenuType == "menu")
            {
                menu_out = CmsHelper.openMenuData(menu_out, Zone.ToString(), Convert.ToInt32(ArticleDetails["article_id"].ToString()), Convert.ToInt32(ArticleDetails["zone_id"].ToString()), SelectedItemClass, NotSelectedItemClass);
            }


            DateTime ended = DateTime.Now;
            TimeSpan ts = ended - started;
            menu_out = menu_out + Environment.NewLine + "<!-- Processed in " + ts.TotalMilliseconds + " ms menu_temp: " + menu_temp.Replace(">", "").Replace("<", "").Replace("##", "") + " -->";

            if (!string.IsNullOrEmpty(ClassName))
                menu_class = " class=\"" + ClassName + "\"";

            //string replaceClass = "<ul id=\"zone_" + ArticleDetails["zone_id"].ToString() + "\"";

            //if (menu_out.Contains(replaceClass))
            //{
            //    menu_out = menu_out.Replace(replaceClass, replaceClass + menu_class);
            //}

            if (!string.IsNullOrEmpty(menucontainertagid))
                menucontainertagid = " id=\"" + menucontainertagid + "\"";

            if (menu_container.ToUpper().Equals("NA"))
            {
                result = result.Replace(menu_out, Environment.NewLine + menu_out + Environment.NewLine);
            }
            else
            {
                if (!string.IsNullOrEmpty(menu_container))
                {
                    menu_out = menu_out.Replace(menu_out, "<" + menu_container + menu_class + menucontainertagid + ">" + Environment.NewLine + menu_out + Environment.NewLine + "</" + menu_container + ">" + Environment.NewLine);
                }

            }

            if (!string.IsNullOrEmpty(ClassName) && !menu_out.Contains("class=\"" + ClassName + "\""))
            {
                menu_out = menu_out.Replace("<ul", "<ul class=\"" + ClassName + "\"");
            }          

            output.Write(menu_out);

        }
    }
}
