using EuroCMS.Admin.entity;
using EuroCMS.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace EuroCMS.Admin.Common
{
    public static class UpdateExtensions
    {

        public delegate void Func<TArg0>(TArg0 element);

        /// <summary>
        /// Executes an Update statement block on all elements in an IEnumerable<T> sequence.
        /// </summary>
        /// <typeparam name="TSource">The source element type.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="update">The update statement to execute for each element.</param>
        /// <returns>The numer of records affected.</returns>
        public static int Update<TSource>(this IEnumerable<TSource> source, Func<TSource> update)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (update == null) throw new ArgumentNullException("update");
            if (typeof(TSource).IsValueType)
                throw new NotSupportedException("value type elements are not supported by update.");

            int count = 0;
            foreach (TSource element in source)
            {
                update(element);
                count++;
            }
            return count;
        }
    }

    public static class EnumerableExtensions
    {
        public static String InHtmlNewLine(this IEnumerable<string> enumerable)
        {
            return "<br />" + AsJoined(enumerable, "<br />");
        }

        public static String InComboValue(this IEnumerable<cms_asp_select_combo_values_Result> enumerable)
        {
            return AsJoined(enumerable, "\n");
        }

        public static String AsJoined(this IEnumerable<string> enumerable, String separator)
        {
            return String.Join(separator, enumerable.ToArray());
        }

        public static String AsJoined(this IEnumerable<cms_asp_select_combo_values_Result> enumerable, String separator)
        {
            return String.Join(separator, enumerable.Select(t => t.combo_supid + "|~|" + t.combo_value + "|~|" + t.combo_label).ToArray());
        }
    }

    public class Group<T, K>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}

namespace EuroCMS.Admin.Common
{

    public static class StringExtension
    {
        public static string GetRevisionStatus(this string inS)
        {
            string retVal = string.Empty;
            switch (inS)
            {
                case "L":
                    retVal = "Live";
                    break;
                case "A":
                    retVal = "Approved";
                    break;
                case "N":
                    retVal = "Not Approved";
                    break;
                case "E":
                    retVal = "Ex-Live";
                    break;
                case "W":
                    retVal = "Waiting for Approval";
                    break;
                case "X":
                    retVal = "Discarded";
                    break;
            }
            return retVal;
        }

        public static bool IsNumeric(this String helper)
        {
            bool ret = false;
            try
            {
                int a = int.Parse(helper.ToString());
            }
            catch { }
            return ret;
        }

        public static string GetValidAlias(this string helper)
        {
            string returner = helper.ToString();
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
                               .Replace("Ç", "C")
                               .Replace(" ", "_")
                               .Replace(".", "")
                               .Replace("ã", "a")
                               .Replace("é", "e")
                               .Replace("í", "i")
                               .Replace("&", "and")
                               ;

            string regexSearch = string.Format("{0}{1}",
                                 new string(Path.GetInvalidFileNameChars()),
                                 new string(Path.GetInvalidPathChars()));
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            returner = r.Replace(returner, "-");
            Regex r1 = new Regex(string.Format("[^a-zA-Z0-9-_]", returner));
            returner = r1.Replace(returner, "");

            // boşluklar _ yapılmıştı
            returner = returner.Replace("_", "-");

            return returner.ToLowerInvariant();
        }
    }

}

namespace System.Web.Mvc.Html
{
    public static class StringExtensions
    {
        public static string Left(this string s, int length)
        {
            return s.Length > length ?
                string.Format("{0}...", s.Substring(0, length))
                :
                s;
        }
    }

    public class GroupDropListItem
    {
        public string Name { get; set; }
        public List<OptionItem> Items { get; set; }
    }

    public class OptionItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    public static class CoreHelper
    {
        static UrlRedirectDbContext urContext = new UrlRedirectDbContext();

        public static string CheckRedirectionAlias(int ZoneID, int ArticleID, string Alias, int n)
        {
            string newAlias = Alias;
            if (!string.IsNullOrEmpty(Alias))
            {
                if (
                    (Alias == EuroCMS.Core.Constansts.ALIAS_CONTENT
                    || Alias == EuroCMS.Core.Constansts.ALIAS_CRON
                    || Alias == EuroCMS.Core.Constansts.ALIAS_CSS
                    || Alias == EuroCMS.Core.Constansts.ALIAS_EMPTY
                    || Alias == EuroCMS.Core.Constansts.ALIAS_FXML
                    || Alias == EuroCMS.Core.Constansts.ALIAS_PLUGIN
                    || Alias == EuroCMS.Core.Constansts.ALIAS_RSS
                    || Alias == EuroCMS.Core.Constansts.ALIAS_SITEMAP
                    || Alias == EuroCMS.Core.Constansts.ALIAS_STF
                    || Alias == EuroCMS.Core.Constansts.ALIAS_STF_SEND
                    || Alias == EuroCMS.Core.Constansts.ALIAS_WEB
                    || Alias == EuroCMS.Core.Constansts.ALIAS_XML)
                    )
                    return CheckRedirectionAlias(ZoneID, ArticleID, Alias, n++);
                else
                {
                    if (n > 0)
                        newAlias = Alias + "-" + n;

                    var result = urContext.SelectAzCheck(ZoneID, ArticleID, newAlias).FirstOrDefault();
                    if (result != null && result.rType == "REV")
                    {
                        n++;
                        return CheckRedirectionAlias(ZoneID, ArticleID, Alias, n);
                    }
                    else
                        return Alias;
                }
            }
            return string.Empty;
        }

        public static bool CheckAlias(int ZoneID, int ArticleID, string Alias)
        {
            string newAlias = Alias;
            if (!string.IsNullOrEmpty(Alias))
            {
                if (
                    (Alias == EuroCMS.Core.Constansts.ALIAS_CONTENT
                    || Alias == EuroCMS.Core.Constansts.ALIAS_CRON
                    || Alias == EuroCMS.Core.Constansts.ALIAS_CSS
                    || Alias == EuroCMS.Core.Constansts.ALIAS_EMPTY
                    || Alias == EuroCMS.Core.Constansts.ALIAS_FXML
                    || Alias == EuroCMS.Core.Constansts.ALIAS_PLUGIN
                    || Alias == EuroCMS.Core.Constansts.ALIAS_RSS
                    || Alias == EuroCMS.Core.Constansts.ALIAS_SITEMAP
                    || Alias == EuroCMS.Core.Constansts.ALIAS_STF
                    || Alias == EuroCMS.Core.Constansts.ALIAS_STF_SEND
                    || Alias == EuroCMS.Core.Constansts.ALIAS_WEB
                    || Alias == EuroCMS.Core.Constansts.ALIAS_XML)
                    )
                    return false;
                else
                {

                    var result = urContext.SelectAzCheck(ZoneID, ArticleID, newAlias).FirstOrDefault();
                    if (result != null) //&& result.rType != "REV"
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }

            return false;
        }

        public static string GetStructureGroupName(byte s)
        {
            string result = string.Empty;
            switch (Convert.ToInt32(s))
            {
                case 0: result = "Not Grouped"; break;
                case 1: result = "CSS"; break;
                case 2: result = "Sites"; break;
                case 3: result = "Templates"; break;
                case 4: result = "Portlets"; break;
                case 5: result = "Plugins"; break;
                case 6: result = "XML"; break;
                case 7: result = "RSS Channels"; break;
                case 8: result = "Classification"; break;
                case 9: result = "File Types"; break;
                case 10: result = "URL Redirects"; break;
                case 11: result = "Custom Contents"; break;
            }
            return result;
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
    }

    public static class CmsUrlHelper
    {
        public static MvcHtmlString ActionLinkSite(this HtmlHelper helper, string linkText, string actionName, string controllerName)
        {
            if (HttpContext.Current.Session["CurrentSiteID"] != null)
                return helper.ActionLink(linkText, actionName, controllerName, new { SiteID = HttpContext.Current.Session["CurrentSiteID"] }, null);
            else
                return helper.ActionLink(linkText, actionName, controllerName);
        }

        public static string ActionSite(this UrlHelper helper, string actionName, string controllerName)
        {
            if (HttpContext.Current.Session["CurrentSiteID"] != null)
                return helper.
                     Action(actionName, controllerName, new { SiteID = HttpContext.Current.Session["CurrentSiteID"] });
            else
                return helper.Action(actionName, controllerName);
        }

    }

    public static class CmsHtmlHelper
    {

        public static IHtmlString AsJoined(this IEnumerable<string> enumerable, String separator)
        {
            return new HtmlString(String.Join(separator, enumerable.ToArray()));
        }

        public static IHtmlString InOptionComboValue(this IEnumerable<cms_asp_select_combo_values_Result> enumerable, object selected)
        {
            return new HtmlString(enumerable != null ? String.Join("\n", enumerable.Select(t => "<option  " + ((string)selected == t.combo_value ? "selected" : "") + " value=\"" + t.combo_value + "\">" + t.combo_label + "</option>").ToArray()) : "");
        }

        public static IHtmlString CmsValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            StringBuilder summaryHtml = new StringBuilder();

            if (helper.ViewData.ModelState.IsValid)
                return new HtmlString(string.Empty);

            summaryHtml.Append("<div class=\"alert alert-error\">");
            if (!String.IsNullOrEmpty(validationMessage))
                summaryHtml.Append(helper.Encode(validationMessage));

            summaryHtml.Append("<ul>");

            foreach (var key in helper.ViewData.ModelState.Keys)
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                    summaryHtml.AppendFormat("<li>{0}</li>", helper.Encode(err.ErrorMessage));
            }

            summaryHtml.Append("</ul></div>");
            return new HtmlString(summaryHtml.ToString());
        }

        public static IHtmlString RevisionFlagImg(this HtmlHelper helper, string inS, string inSta)
        {
            var img = new TagBuilder("img");

            if (inSta == "D" || inSta == "2")
            {
                img.Attributes.Add("src", "/" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "/Content/img/icon_flagred.gif");
                img.Attributes.Add("title", "Waiting Delete Approval");
            }
            else
            {
                switch (inS)
                {
                    case "L":
                        img.Attributes.Add("src", "/" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "/Content/img/icon_flaggreen.gif");
                        img.Attributes.Add("title", GetRevisionStatus(inS));
                        break;
                    case "A":
                        img.Attributes.Add("src", "/" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "/Content/img/icon_flagblue.gif");
                        img.Attributes.Add("title", GetRevisionStatus(inS));
                        break;
                    case "N":
                    case "E":
                        img.Attributes.Add("src", "/" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "/Content/img/icon_flagblank.gif");
                        img.Attributes.Add("title", GetRevisionStatus(inS));
                        break;
                    case "W":
                        img.Attributes.Add("src", "/" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "/Content/img/icon_flagorange.gif");
                        img.Attributes.Add("title", GetRevisionStatus(inS));
                        break;
                    case "X":
                        img.Attributes.Add("src", "/" + ConfigurationManager.AppSettings["EuroCMS.AreaName"] + "/Content/img/icon_flagred.gif");
                        img.Attributes.Add("title", GetRevisionStatus(inS));
                        break;
                }

            }

            return new HtmlString(HttpUtility.HtmlDecode(img.ToString(TagRenderMode.SelfClosing)));
        }

        public static string GetRevisionStatus(string inS)
        {
            string retVal = string.Empty;
            switch (inS)
            {
                case "L":
                    retVal = "Live";
                    break;
                case "A":
                    retVal = "Approved";
                    break;
                case "N":
                    retVal = "Not Approved";
                    break;
                case "E":
                    retVal = "Ex-Live";
                    break;
                case "W":
                    retVal = "Waiting for Approval";
                    break;
                case "X":
                    retVal = "Discarded";
                    break;
            }
            return retVal;
        }

        public static IHtmlString GroupDropList(this HtmlHelper helper, string name, IEnumerable<GroupDropListItem> data, string SelectedValue, object htmlAttributes, string optionLabel)
        {
            if (data == null && helper.ViewData != null)
                data = helper.ViewData.Eval(name) as IEnumerable<GroupDropListItem>;
            if (data == null) return null;

            var select = new TagBuilder("select");

            var optHtml = new StringBuilder();
            var optionDefault = new TagBuilder("option");
            optionDefault.Attributes.Add("value", "0");
            optionDefault.InnerHtml = helper.Encode(optionLabel);

            if (htmlAttributes != null)
                select.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            select.GenerateId(name);
            select.Attributes.Add("name", name);

            var optgroupHtml = new StringBuilder();
            var groups = data.ToList();
            foreach (var group in data)
            {
                optHtml.Clear();

                var groupTag = new TagBuilder("optgroup");
                groupTag.Attributes.Add("label", string.IsNullOrEmpty(group.Name) ? "Not Grouped" : HttpUtility.HtmlDecode(group.Name));

                foreach (var item in group.Items)
                {
                    var option = new TagBuilder("option");
                    option.Attributes.Add("value", helper.Encode(item.Value));
                    if (SelectedValue != null && item.Value == SelectedValue)
                        option.Attributes.Add("selected", "selected");
                    option.InnerHtml = helper.Encode(item.Text);
                    optHtml.AppendLine(option.ToString(TagRenderMode.Normal));
                }
                groupTag.InnerHtml = optHtml.ToString();
                optgroupHtml.AppendLine(groupTag.ToString(TagRenderMode.Normal));
            }
            select.InnerHtml = optionDefault.ToString() + optgroupHtml.ToString();
            return new HtmlString(HttpUtility.HtmlDecode(select.ToString()));
        }
    }

}

