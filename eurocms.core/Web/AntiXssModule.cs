using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

//using Encoder = Microsoft.Security.Application.Encoder;

namespace EuroCMS.Web
{
    public class AntiXssModule : IHttpModule, ICmsHttpModule
    {
        private static readonly Regex _inputCleaner = new Regex("<[^>]+>", RegexOptions.Compiled);

        public const string POTENTIAL_XSS_ATTACK_EXPRESSION_V3 = "(javascript[^*(%3a)]*(%3a|:))|(%3C*|<)[^*]?script|(document*(%2e|.))|(setInterval[^*(%28)]*(%28|\\())|(setTimeout[^*(%28)]*(%28|\\())|(alert[^*(%28)]*(%28|\\())|(((\\%3C) <)[^\n]+((\\%3E) >))";


        public static bool IsXSSAttcak(Regex regex, string inputValue)
        {
            return regex.IsMatch(inputValue);
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += CleanUpInput;
        }


        public void Dispose()
        {
        }

        private static void CleanUpInput(object sender, EventArgs e)
        {
            HttpRequest request = ((HttpApplication)sender).Request;

            if (request.QueryString.Count > 0)
            {
                CleanUpCollection(request.QueryString);
            }

            if (request.HttpMethod == "POST")
            {
                if (request.Form.Count > 0)
                {
                    CleanUpCollection(request.Form);
                }
            }
        }

        private static void CleanUpCollection(NameValueCollection collection)
        {
            // Both the form and query string collections are read-only by 
            // default, so use Reflection to make them writable:
            PropertyInfo readonlyProperty = collection.GetType()
                .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

            readonlyProperty.SetValue(collection, false, null);

            for (int index = 0; index < collection.Count; index++)
            {
                if (string.IsNullOrWhiteSpace(collection[index]))
                {
                    continue;
                }

                collection[collection.Keys[index]] =
                    HttpUtility.HtmlEncode(_inputCleaner.Replace(collection[index], string.Empty));

                //collection[collection.Keys[index]] = Encoder.JavaScriptEncode(collection[index]);

                //collection[collection.Keys[index]] = Encoder.HtmlEncode(collection[index]);

                //collection[collection.Keys[index]] = Encoder.HtmlAttributeEncode(collection[index]);

                //collection[collection.Keys[index]] = Encoder.JavaScriptEncode(collection[index]);
            }

            readonlyProperty.SetValue(collection, true, null);
        }

        public string DisplayName
        {
            get { return "EuroCMS Anti XSS Module"; }
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
            get { return "Ramazan Dönmez";  }
        }
    }
}
