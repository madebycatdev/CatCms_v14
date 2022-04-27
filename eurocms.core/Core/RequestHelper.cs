using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace EuroCMS.Core
{
    #region WebRequestException
    public class WebRequestException : Exception
    {
        public string ResponseBody
        {
            get;
            private set;
        }

        public HttpStatusCode StatusCode
        {
            get;
            private set;
        }

        public WebRequestException()
            : base()
        { }

        public WebRequestException(string addr)
            : base("Unable to connect: " + addr)
        { }

        public WebRequestException(HttpWebResponse response, string responseBody)
            : base(string.Format("The service returned '{0}' with the status code {1} ({2:d}).", response.StatusDescription, response.StatusCode, response.StatusCode))
        {
            this.ResponseBody = responseBody;
            this.StatusCode = response.StatusCode;
        }
    }
    #endregion
    #region RequestHelper
    public static class RequestHelper
    {
        #region DowloadFile
        public static void Download(string fileRemoteUrl, string outputPath)
        {
            using (WebClient request = new WebClient())
            {
                byte[] fileData = request.DownloadData(HttpUtility.UrlDecode(fileRemoteUrl));
                using (FileStream file = File.Create(outputPath))
                {
                    file.Write(fileData, 0, fileData.Length);
                }
            }
        }
        #endregion
        #region SendGet
        public static string SendGet(String endPoint)
        {
            return SendGet(endPoint, string.Empty, string.Empty);
        }

        public static string SendGet(String endPoint, string host, string userAgent)
        {
            try
            {
                HttpWebRequest webRequest = CreateGETRequest(endPoint, host, userAgent);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                /*asyncResult.AsyncWaitHandle.WaitOne();*/

                // get the response from the completed web request.
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    // if not connected to serviceAddr, throw WebException with that message: "The remote name could not be resolved: xxx"
                    // this message will be send to parent catch block

                    HttpWebResponse Response = (webResponse as HttpWebResponse);
                    string ResponseBody = ReadStream(Response.GetResponseStream());

                    return ResponseBody;
                }
            }
            catch (Exception e) // 500 hatalı buraya düşer.
            {
                if (e is WebException && ((WebException)e).Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errResp = (((WebException)e).Response) as HttpWebResponse;
                    throw new WebRequestException(errResp, ReadStream(errResp.GetResponseStream()));
                }
                else
                    throw new Exception(e.Message + " -- " + e.StackTrace);
            }
        }

        public static string SendGetWithProxy(String endPoint, string proxyServer, string proxyUser, string proxyPass)
        {
            try
            {
                HttpWebRequest webRequest = CreateGetWebRequest(endPoint, false, "", "", null, "", "", proxyServer, proxyUser, proxyPass);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                /*asyncResult.AsyncWaitHandle.WaitOne();*/

                // get the response from the completed web request.
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    // if not connected to serviceAddr, throw WebException with that message: "The remote name could not be resolved: xxx"
                    // this message will be send to parent catch block

                    HttpWebResponse Response = (webResponse as HttpWebResponse);
                    string ResponseBody = ReadStream(Response.GetResponseStream());

                    return ResponseBody;
                }
            }
            catch (Exception e) // 500 hatalı buraya düşer.
            {
                if (e is WebException && ((WebException)e).Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errResp = (((WebException)e).Response) as HttpWebResponse;
                    throw new WebRequestException(errResp, ReadStream(errResp.GetResponseStream()));
                }
                else
                    throw new Exception(e.Message + " -- " + e.StackTrace);
            }
        }
        #endregion
        #region SendPost
        public static string SendPost(String endPoint, Dictionary<string, string> parameters)
        {
            return SendPost(endPoint, parameters, string.Empty, string.Empty);
        }
        public static string SendPost(String endPoint, Dictionary<string, string> parameters, string host, string userAgent)
        {
            try
            {
                HttpWebRequest webRequest = CreatePOSTRequest(endPoint, host, userAgent);

                string content = string.Empty;
                foreach (string k in parameters.Keys)
                {
                    content += k + "=" + parameters[k] + "&";
                }
                content = content.TrimEnd(new char[] { '&' });

                webRequest.ContentLength = content.Length;

                Byte[] byteData = UTF8Encoding.UTF8.GetBytes(content);

                Stream requestStream;
                using (requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(byteData, 0, byteData.Length);
                    requestStream.Close();
                }

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                /*asyncResult.AsyncWaitHandle.WaitOne();*/

                // get the response from the completed web request.
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    // if not connected to serviceAddr, throw WebException with that message: "The remote name could not be resolved: xxx"
                    // this message will be send to parent catch block


                    HttpWebResponse Response = (webResponse as HttpWebResponse);
                    string ResponseBody = ReadStream(Response.GetResponseStream());

                    return ResponseBody;
                }
            }
            catch (Exception e) // 500 hatalı buraya düşer.
            {
                if (e is WebException && ((WebException)e).Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errResp = (((WebException)e).Response) as HttpWebResponse;
                    throw new WebRequestException(errResp, ReadStream(errResp.GetResponseStream()));
                }
                else
                    throw new Exception(e.Message + " -- " + e.StackTrace);
            }
        }
        #endregion
        #region SendMultipartPost
        public static string SendMultipartPost(String endPoint, Dictionary<string, string> parameters, string fileName, FileStream fileStream)
        {
            try
            {
                HttpWebRequest webRequest = CreateMultipartPostWebRequest(endPoint, parameters, fileName, fileStream);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                /*asyncResult.AsyncWaitHandle.WaitOne();*/

                // get the response from the completed web request.
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    // if not connected to serviceAddr, throw WebException with that message: "The remote name could not be resolved: xxx"
                    // this message will be send to parent catch block

                    HttpWebResponse Response = (webResponse as HttpWebResponse);
                    string ResponseBody = ReadStream(Response.GetResponseStream());

                    return ResponseBody;
                }
            }
            catch (Exception e) // 500 hatalı buraya düşer.
            {
                if (e is WebException && ((WebException)e).Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errResp = (((WebException)e).Response) as HttpWebResponse;
                    throw new WebRequestException(errResp, ReadStream(errResp.GetResponseStream()));
                }
                else
                    throw new Exception(e.Message + " -- " + e.StackTrace);
            }
        }
        #endregion
        #region Create Web Request
        static HttpWebRequest CreateGETRequest(string url)
        {
            return CreateGetWebRequest(url, false, string.Empty, string.Empty, null, string.Empty, string.Empty,string.Empty,string.Empty,string.Empty);
        }
        static HttpWebRequest CreateGETRequest(string url, string host, string userAgent)
        {
            return CreateGetWebRequest(url, false, string.Empty, string.Empty, null, host, userAgent, string.Empty, string.Empty, string.Empty);
        }
        static HttpWebRequest CreatePOSTRequest(string url)
        {
            return CreatePostWebRequest(url, false, string.Empty, string.Empty, null, string.Empty, string.Empty);
        }
        static HttpWebRequest CreatePOSTRequest(string url, string host, string userAgent)
        {
            return CreatePostWebRequest(url, false, string.Empty, string.Empty, null, host, userAgent);
        }
        static HttpWebRequest CreateGetWebRequest(string url, bool useAuth, string user, string pass, List<KeyValuePair<string, string>> headers, string host, string userAgent, string proxyServer, string proxyUser, string proxyPass)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.KeepAlive = false;
            webRequest.Timeout = System.Threading.Timeout.Infinite;
            webRequest.AllowWriteStreamBuffering = false;
            webRequest.ProtocolVersion = System.Net.HttpVersion.Version11;
            webRequest.Timeout = 120 * 1000;
            if (!string.IsNullOrEmpty(proxyServer))
            {
                WebProxy proxy = new WebProxy();
                proxy.Address = new Uri(proxyServer);
                if (!string.IsNullOrEmpty(proxyUser) && !string.IsNullOrEmpty(proxyPass))
                    proxy.Credentials = new NetworkCredential(proxyUser, proxyPass);

                webRequest.Proxy = proxy;
            }

            if (headers != null)
                foreach (KeyValuePair<string, string> p in headers)
                    webRequest.Headers.Add(p.Key, p.Value);

            //if (!string.IsNullOrEmpty(host))
            //    webRequest.Host = host;

            if (!string.IsNullOrEmpty(userAgent))
                webRequest.UserAgent = userAgent;

            if (useAuth) webRequest.Credentials = new NetworkCredential(user, pass);
            return webRequest;
        }
        static HttpWebRequest CreatePostWebRequest(string url, bool useAuth, string user, string pass, List<KeyValuePair<string, string>> headers, string host, string userAgent)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.KeepAlive = false;
            webRequest.Timeout = System.Threading.Timeout.Infinite;
            webRequest.AllowWriteStreamBuffering = false;
            webRequest.ProtocolVersion = System.Net.HttpVersion.Version11;
            webRequest.Timeout = 120 * 1000;
            if (headers != null)
                foreach (KeyValuePair<string, string> p in headers)
                    webRequest.Headers.Add(p.Key, p.Value);

            //if (!string.IsNullOrEmpty(host))
            //    webRequest.Host = host;

            if (!string.IsNullOrEmpty(userAgent))
                webRequest.UserAgent = userAgent;

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            if (useAuth) webRequest.Credentials = new NetworkCredential(user, pass);
            return webRequest;
        }
        static HttpWebRequest CreateMultipartPostWebRequest(string url, Dictionary<string, string> Parameters, string FileName, FileStream fileStream)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Timeout = 120 * 1000;
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Stream rs = webRequest.GetRequestStream();
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (Parameters != null && Parameters.Count > 0)
            {
                foreach (string key in Parameters.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, Parameters[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "jpeg", FileName, "image/jpeg");
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();
            return webRequest;
        }
        #endregion
        #region ReadStream
        public static string ReadStream(Stream stream)
        {
            StringBuilder body = new StringBuilder();
            byte[] buf = new byte[8192];
            int count = 0;
            do
            {
                count = stream.Read(buf, 0, buf.Length);
                if (count != 0)
                    body.Append(Encoding.ASCII.GetString(buf, 0, count));
            }
            while (count > 0);
            return body.ToString();
        }
        #endregion    
    }
    #endregion
}
