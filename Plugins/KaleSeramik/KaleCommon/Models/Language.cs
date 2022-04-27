using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Language
    {

        #region Properties
        public static LanguageItems _LanguageItems;

        public static LanguageItems CurrentLanguage
        {
            get
            {
                if (_LanguageItems == null)
                {
                    _LanguageItems = new LanguageItems();
                    InitializeLanguages();
                }
                else
                {
                    if (_LanguageItems.Count == 0)
                    {
                        _LanguageItems = new LanguageItems();
                        InitializeLanguages();
                    }
                }

                //Language ReturnLang = null;
                //foreach (Language l in _LanguageItems)
                //    if (l.ID == ID)
                //        ReturnLang = l;

                return _LanguageItems;
            }
        }



        /// <summary>
        /// 2 char, Language ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }


        /// <summary>
        /// string, name of language
        /// </summary>
        public string Name
        {
            get;
            set;
        }


        /// <summary>
        /// string, xml content of language
        /// </summary>
        public XmlDocument Xml
        {
            get;
            set;
        }


        #endregion

        #region Constructors

        public Language() { }

        public Language(string LangID)
        {
            this.ID = LangID.ToLower(new CultureInfo("en-US"));
        }

        public Language(string LangID, string langName) : this(LangID)
        {
            this.Name = langName;
            this.Xml = new XmlDocument();
        }

        #endregion

        #region Initialize Languages

        private static void InitializeLanguages()
        {

            DataTable dt = DbHelper.ExecuteSQLString("select l.lang_id, l.lang_name, l.lang_xml from dbo.cms_languages l with(nolock)").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string langId = dr["lang_id"].ToString().ToUpper();
                string langName = dr["lang_name"].ToString();
                string langXML = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(dr["lang_xml"].ToString()));
                if (!string.IsNullOrEmpty(langXML))
                {
                    
                    Language lang = new Language(langId, langName);
                    lang.Xml.LoadXml(langXML);
                    _LanguageItems.Add(lang);
                }

            }

        }

        #endregion

        #region Select Language Items 

        public String SelectLabel(string Key)
        {
            if (this.Xml != null && this.Xml.DocumentElement != null)
            {
                XmlNode root = this.Xml.DocumentElement;

                string node = "//language/label[@name='" + Key + "']";
                if (this.Xml.SelectSingleNode(node) != null)
                    return this.Xml.SelectSingleNode(node).InnerText.Trim();
                else
                    return String.Empty;
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion   
    }

    public class LanguageItems : CollectionBase
    {
        public Language this[int index]
        {
            get
            {
                return (Language)this.List[index];
            }
        }

        public Language this[string langId]
        {
            get
            {
                Language litem = new Language();
                foreach (Language c in this.List)
                {
                    if (c.ID.ToUpper() == langId.ToUpper())
                        litem = c;
                }
                return litem;
            }
        }

        public void Add(Language item)
        {
            base.List.Add(item);
        }

        public override string ToString()
        {
            return string.Join(",", base.List.Cast<Language>().Select(t => t.Name + "-" + t.ID));
        }
    }
}