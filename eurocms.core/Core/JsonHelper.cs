using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EuroCMS.Core
{
    public static class JsonHelper
    {
        #region JSONSerializer
        public static string Serializer<T>(Dictionary<string, T> DictionaryObject)
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            return JSONSerializer.Serialize(DictionaryObject);
        }
        #endregion
        #region Serialize
        public static string Serialize<T>(T Obj)
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            return JSONSerializer.Serialize(Obj);
        }
        #endregion
        #region JSONDeserializer
        public static T Deserializer<T>(string json)
        {
            if (!json.Equals("No Server Response"))
            {
                JavaScriptSerializer JSONDeserializer = new JavaScriptSerializer();
                return (T)JSONDeserializer.Deserialize<T>(json);
            }
            return default(T);
        }
        #endregion
        #region GetDictionaryValue
        public static T GetDictionaryValue<T>(Dictionary<string, object> JSONDictionary, string Key)
        {
            if (JSONDictionary.ContainsKey(Key))
            {
                if (!string.IsNullOrEmpty(JSONDictionary[Key].ToString()))
                {
                    return GetValue<T>(JSONDictionary[Key]);
                }
            }
            return default(T);
        }
        #endregion
        #region GetValue
        public static T GetValue<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        #endregion
        #region ExtractDictionary
        public static Dictionary<string, object> ExtractDictionary<T>(Dictionary<string, object> JSONDictionary, string DictionaryPath)
        {
            string Key = "";
            Dictionary<string, object> DictionaryObject = JSONDictionary;

            while (DictionaryPath.Length > 0)
            {
                if (DictionaryPath.Contains(":"))
                {
                    Key = DictionaryPath.Substring(0, DictionaryPath.IndexOf(":"));
                    DictionaryPath = DictionaryPath.Substring(DictionaryPath.IndexOf(":") + 1);
                    if (DictionaryObject.ContainsKey(Key))
                    {
                        DictionaryObject = (Dictionary<string, object>)DictionaryObject[Key];
                    }
                    else
                    {
                        return DictionaryObject;
                    }
                }
                else
                {
                    Key = DictionaryPath;
                    DictionaryPath = "";
                    if (DictionaryObject.ContainsKey(Key))
                    {
                        DictionaryObject = (Dictionary<string, object>)DictionaryObject[Key];
                    }
                    else
                    {
                        return DictionaryObject;
                    }
                }
            }
            return DictionaryObject;
        }
        #endregion
    }
}
