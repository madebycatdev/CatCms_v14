using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace EuroCMS.FrontEnd.p
{
    /// <summary>
    /// Summary description for HttpRequest
    /// </summary>
    public class HttpRequest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Charset = "utf-8";
            context.Response.ContentType = "json";

            string url = "", httpType = "", responseType = "", charset = "";

            url = context.Request["__URL__"] ?? "";
            httpType = context.Request["__HTTP_TYPE__"] ?? "";
            responseType = context.Request["__RETURN_TYPE__"] ?? "";

            if (string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(context.Request.Form["__URL__"]))
            {
                url = context.Request.Form["__URL__"];
            }
            if (string.IsNullOrEmpty(httpType) && !string.IsNullOrEmpty(context.Request.Form["__HTTP_TYPE__"]))
            {
                httpType = context.Request.Form["__HTTP_TYPE__"];
            }
            if (string.IsNullOrEmpty(responseType) && !string.IsNullOrEmpty(context.Request.Form["__RETURN_TYPE__"]))
            {
                responseType = context.Request.Form["__RETURN_TYPE__"];
            }

            if (!string.IsNullOrEmpty(context.Request.Form["__CHARSET__"]))
            {
                charset = context.Request.Form["__CHARSET__"];
            }

            if (string.IsNullOrEmpty(charset))
            {
                charset = "utf-8";
            }


            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(httpType) || string.IsNullOrEmpty(responseType))
            {
                context.Response.Write("");
                //context.Response.Flush();
                context.Response.End();
            }

            if (responseType.Contains("xml"))
            {
                context.Response.ContentType = "text/xml";
            }
            if (responseType.Contains("json"))
            {
                context.Response.ContentType = "text/json";
            }
            if (responseType.Contains("text"))
            {
                context.Response.ContentType = "text/plain";
            }
            if (responseType.Contains("html"))
            {
                context.Response.ContentType = "text/html";
            }

            string requestForm = "";
            string queryStringData = "";
            string result = "";

            try
            {

                string[] allParameters = HttpContext.Current.Request.Form.AllKeys;
                string parameters = "";

                for (int i = 0; i < allParameters.Count(); i++)
                {
                    string parameterValue = HttpContext.Current.Request.Form[allParameters[i]];
                    parameterValue = HttpUtility.UrlDecode(parameterValue);
                    parameterValue = HttpUtility.HtmlDecode(parameterValue);

                    parameters += (i == 0 ? "" : "&") + allParameters[i] + "=" + parameterValue;
                }

                url = HttpUtility.UrlDecode(url);
                url = HttpUtility.HtmlDecode(url);

                url = url.Contains("?") ? (url + "&" + parameters) : (url + "?" + parameters);

                url = HttpUtility.UrlDecode(url);
                url = HttpUtility.HtmlDecode(url);

                StringBuilder sb = new StringBuilder();
                string weatherservice = url;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(weatherservice);
                request.ContentType = "charset=" + charset + ";";
                //request.Headers.Add(HttpRequestHeader.AcceptCharset, charset);
                //request.Headers.Add("Content-Encoding", charset);
                //request.TransferEncoding = charset;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
                StreamReader reader = new StreamReader(stream);
                Char[] readBuffer = new Char[256];
                int count = reader.Read(readBuffer, 0, 256);

                while (count > 0)
                {
                    String output = new String(readBuffer, 0, count);
                    sb.Append(output);
                    count = reader.Read(readBuffer, 0, 256);
                }

                result = sb.ToString();


                context.Response.Write(result);
                context.Response.End();


                // !!! EĞER YUKARIDAKİ KODLAR HAVA DURUMU DIŞINDA KULLANILACAK BİR WEBSERVICE ILE ÇALIŞMAZSA, AŞAĞIDAKİ KODLAR TEST EDİLECEK !!!

                //if (url.Contains("?"))
                //{
                //    string[] dataArray = url.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
                //    if (dataArray.Count() > 1)
                //    {
                //        url = dataArray[0];
                //        queryStringData = dataArray[1].Trim();
                //    }
                //}
                //WebRequest request = WebRequest.Create(url);
                //request.Method = httpType.ToUpper();
                //string postData = queryStringData;
                //postData = queryStringData;
                //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentLength = byteArray.Length;
                //Stream dataStream = request.GetRequestStream();
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //dataStream.Close();
                //WebResponse response = request.GetResponse();
                //dataStream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(dataStream);
                //result = reader.ReadToEnd();
                //reader.Close();
                //dataStream.Close();
                //response.Close();
                //context.Response.Write(result);
                //context.Response.End();
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    var httpEx = (WebException)ex;
                    if (httpEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = httpEx.Response as HttpWebResponse;
                        using (Stream stream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                            String responseString = reader.ReadToEnd();

                            context.Response.ContentType = "text/html";
                            context.Response.Write("<br /> Uzak sunucudan dönen response kodu: " + (int)response.StatusCode);
                            context.Response.Write("<br /> Uzak sunucudan dönen response çıktısı: <pre>" + responseString + "</pre>");
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            context.Response.Write(ex.Message + "---" + requestForm + " URL: " + url + " METHOD: " + httpType.ToUpper() + " QueryStringData: " + queryStringData);
                            context.Response.End();
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        context.Response.Write(ex.Message + "---" + requestForm + " URL: " + url + " METHOD: " + httpType.ToUpper() + " QueryStringData: " + queryStringData);
                        context.Response.End();
                    }
                }

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}