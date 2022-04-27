using EuroCMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EuroCMS.WebControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:EditorLink runat=server></{0}:EditorLink>")]
    public class EditorLink : System.Web.UI.WebControls.WebControl
    {
        #region Properties
        private int _ArticleId;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int ArticleId
        {
            get
            {
                return _ArticleId;
            }

            set
            {
                _ArticleId = value;
            }
        }

        private int _SiteId;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int SiteId
        {
            get
            {
                return _SiteId;
            }

            set
            {
                _SiteId = value;
            }
        }

        private int _ZoneGroupId;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int ZoneGroupId
        {
            get
            {
                return _ZoneGroupId;
            }

            set
            {
                _ZoneGroupId = value;
            }
        }

        private int _ZoneId;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int ZoneId
        {
            get
            {
                return _ZoneId;
            }

            set
            {
                _ZoneId = value;
            }
        }

        private long _RevId;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public long RevId
        {
            get
            {
                return _RevId;
            }

            set
            {
                _RevId = value;
            }
        }

        private string _Type;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("A")]
        [Localizable(true)]
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = value;
            }
        }

        private string _Flags;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Flags
        {
            get
            {
                return _Flags;
            }

            set
            {
                _Flags = value;
            }
        }
        #endregion

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            // Sayfadaki article edit butonlarını aktif etmek için aşağıdaki yorum satırlarını açmak gerekiyor

            HttpContext.Current.Trace.Write(string.Format("getEDLink EditorLink Control Called! IsAuthenticated:{0} ", HttpContext.Current.User.Identity.IsAuthenticated));

            if (HttpContext.Current.User.Identity.IsAuthenticated && !GetApplication(Constansts.CFG_REMOVE_EDITOR_LINKS).Equals("Y"))
            {
                HttpContext.Current.Trace.Write(string.Format("getEDLink EditorLink Control Rendering! IsAuthenticated:{0} ", HttpContext.Current.User.Identity.IsAuthenticated));

                string eb_str = " id=\"editButton" + _ArticleId + "\" onmouseover=\"showEditButtons(" + _Type + ",'" + _Flags + "'," + _SiteId + "," + _ZoneGroupId + "," + _ZoneId + "," + _ArticleId + "," + _RevId + ", '" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "');\" onmouseout=\"hideEditButtons(" + _Type + ");\" ";
                output.Write("<a href=\"javascript:void(0);\" class=\"editButton\" onClick=\"return false;\" title=\"Click here to edit this article\" " + eb_str + " target=\"editor\"><span class=\"edBut\">" + _Type + "</span></a>");
            }
        }
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
    }
}
