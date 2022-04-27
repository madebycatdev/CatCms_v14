using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace EuroCMS.Core
{
    public class Pager : CompositeControl
    {
        #region Style

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string InnerLinkCsslass
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string PreviousButtonText
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string LastButtonText
        {
            get;
            set;
        }


        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string NextButtonText
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string FirstButtonText
        {
            get;
            set;
        }


        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string CurrentLinkCssClass
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string LastButtonCssClass
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string FirstButtonCssClass
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string NextButtonCssClass
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Style")]
        [Localizable(true)]
        public string PreviousButtonCssClass
        {
            get;
            set;
        }

        #endregion
        #region Pager


        [Bindable(true)]
        [Category("Keys")]
        [Localizable(true)]
        public readonly string QueryStringKey = "PageIndex";



        /// <summary>
        /// Pager'da görüncek iç link sayısı
        /// </summary>
        private int displayLinkCount = 5;
        [Bindable(true)]
        [Category("Pager")]
        [DefaultValue("5")]
        [Localizable(true)]
        public int DisplayLinkCount
        {
            get
            {
                return this.displayLinkCount;
            }
            set
            {
                if (value > 3)
                    displayLinkCount = value;
            }
        }


        /// <summary>
        /// 1 sayfada görünecek kayıt sayısı
        /// </summary>
        private int pageSize = 10;
        [Bindable(true)]
        [Category("Pager")]
        [DefaultValue("10")]
        [Localizable(true)]
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                if (value < 0)
                    pageSize = 0;
                pageSize = value;
            }
        }

        /// <summary>
        /// Datadaki toplam kayıt sayısı
        /// </summary>
        private int totalRow = 0;
        [Bindable(true)]
        [Category("Pager")]
        [DefaultValue("0")]
        [Localizable(true)]
        public int TotalRow
        {
            get
            {
                return totalRow;
            }
            set
            {
                if (value < 0)
                    totalRow = 0;
                totalRow = value;
            }
        }

        #endregion
        #region Compute

        private int currentPageIndex;
        /// <summary>
        ///  İstenilen sayfanın index değeri
        /// </summary>
        [DefaultValue(0)]
        public int CurrentPageIndex
        {
            get;
            set;
        }

        /// <summary>
        ///  Hesaplanan Toplam sayfa
        /// </summary>
        [DefaultValue(0)]
        public int PageCount
        {
            get
            {
                int retVal = 0;
                if (totalRow > 0 && (pageSize > 0 && totalRow > pageSize))
                    retVal = (int)Math.Ceiling((double)TotalRow / PageSize);
                return retVal;
            }
        }

        /// <summary>
        ///  Bulunan kayıtlardaki başlagıçın index değeri
        /// </summary>
        [DefaultValue(0)]
        public int RecordStartValue
        {
            get
            {
                return ((CurrentPageIndex - 1) * PageSize) + 1;
            }
        }

        /// <summary>
        ///  Bulunan kayıtlardaki bitişin index değeri
        /// </summary>
        [DefaultValue(0)]
        public int RecorEndValue
        {
            get
            {
                int retVal = (RecordStartValue - 1 + PageSize);
                return retVal >= TotalRow ? TotalRow : retVal;
            }
        }

        /// <summary>
        ///  İç linklerin grup toplamı
        /// </summary>
        [DefaultValue(0)]
        public int LinkGroupCount
        {
            get
            {
                return (int)Math.Ceiling((double)PageCount / DisplayLinkCount);
            }
        }

        /// <summary>
        ///  İç linklerin grup oranı
        /// </summary>
        [DefaultValue(0)]
        public float LinkGroupRate
        {
            get
            {
                return ((float)LinkGroupCount / DisplayLinkCount);
            }
        }

        /// <summary>
        ///  Görünen iç linklerin oranı
        /// </summary>
        [DefaultValue(0)]
        public double DisplayLinkRate
        {
            get
            {
                return Math.Round((double)DisplayLinkCount / 2);
            }
        }

        /// <summary>
        ///  İç link grubunun başlangıç değeri
        /// </summary>
        [DefaultValue(0)]
        public int LinkGroupStartIndex
        {
            get
            {
                int retVal = (int)Math.Floor((double)(CurrentPageIndex - (DisplayLinkRate)));

                // control retval
                if (IsFirstLinkGroup)
                    retVal = 1;
                else if (IsLastLinkGroup)
                    retVal = PageCount - (DisplayLinkCount - 1);

                return retVal;
            }
        }

        /// <summary>
        /// İç link grubunun bitiş değeri
        /// </summary>
        [DefaultValue(0)]
        public int LinkGroupEndIndex
        {
            get
            {
                int retVal = (LinkGroupStartIndex - 1) + DisplayLinkCount;
                return retVal <= PageCount ? retVal : PageCount;
            }
        }

        /// <summary>
        ///  İç link grubunun başı olup olmadığı değerini verir.
        /// </summary>
        [DefaultValue(false)]
        public bool IsFirstLinkGroup
        {
            get
            {
                return (CurrentPageIndex < DisplayLinkCount);
            }
        }

        /// <summary>
        /// İç link grubunun sonu olup olmadığı değerini verir.
        /// </summary>
        [DefaultValue(false)]
        public bool IsLastLinkGroup
        {
            get
            {
                return (CurrentPageIndex > (PageCount - DisplayLinkCount + 1));
            }
        }

        #endregion
        #region Utility

        protected string GetCurrentPagePath()
        {
            return this.Page.Request.Url.AbsolutePath;
        }

        protected string GetCurrentQueryStrings()
        {
            string _queryStringKeyName = string.Empty;
            string _queryStringKeyValue = string.Empty;
            NameValueCollection keyList = this.Page.Request.QueryString;
            string _queryStrings = string.Empty;
            int _keyCount = keyList.Keys.Count;

            string _pagePath = this.Page.Request.Url.AbsolutePath;

            for (int qindex = 0; qindex < _keyCount; qindex++)
            {

                _queryStringKeyName = keyList.Keys[qindex];
                _queryStringKeyValue = keyList[_queryStringKeyName];

                if (_queryStringKeyName != QueryStringKey && _queryStringKeyName != "" && _queryStringKeyValue != "")
                    _queryStrings +=
                        string.Format("{0}={1}&", _queryStringKeyName, _queryStringKeyValue);
            }

            return _queryStrings;
        }

        public static bool IsNumeric(string Text)
        {
            if (Text == "" || Text == null)
            {
                return false;
            }
            Text = Text.Trim();
            bool bResult = Regex.IsMatch(Text, @"^\d+$");
            return bResult;
        }


        #endregion
    }
}



