using EuroCMS.FrontEnd.com.euromsg.auth;
using EuroCMS.FrontEnd.com.euromsg.member;
using EuroCMS.FrontEnd.com.euromsg.post;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace EuroCMS.FrontEnd.p
{
    /// <summary>
    /// Summary description for EuroMsg
    /// </summary>
    public class EuroMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string methodName = string.Empty;
            string result = string.Empty;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            methodName = (!string.IsNullOrEmpty(context.Request.Form["methodname"])) ? context.Request.Form["methodname"].ToLower().Trim() : string.Empty;

            if (string.IsNullOrEmpty(methodName))
            {
                context.Response.Write("");
                context.Response.End();
            }

            Auth a = new Auth();
            EmAuthResult emAuth = new EmAuthResult();
            try
            {
                emAuth = a.Login(System.Configuration.ConfigurationManager.AppSettings["EuroMsgWebServiceUserName"], System.Configuration.ConfigurationManager.AppSettings["EuroMsgWebServicePassword"]);
            }
            catch (Exception ex)
            {
                result = jss.Serialize("NOK - Login Yapılamadı - Message : " + ex.Message + " - InnerException : " + ex.InnerException);
                context.Response.Write(result.ToString());
                context.Response.End();
            }

            if (string.IsNullOrEmpty(emAuth.ServiceTicket))
            {
                //giriş yapılamadı
                result = jss.Serialize("NOK - Message : " + emAuth.Message + " - DetailedMessage : " + emAuth.DetailedMessage);

                context.Response.Write(result.ToString());
                context.Response.End();
            }
            else
            {
                try
                {
                    switch (methodName)
                    {
                        case "addtosendlists":
                            result = AddToSendLists(context, emAuth.ServiceTicket);
                            break;
                        case "posthtml":
                            result = HtmlPost(context, emAuth.ServiceTicket);
                            break;
                        case "insertmemberdemography":
                            result = InsertMemberDemography(context, emAuth.ServiceTicket);
                            break;
                        case "sendongoingmail":
                            result = SendOnGoingMail(context, emAuth.ServiceTicket);
                            break;
                        case "posthtmlwitharticle":
                        case "posthtmlwitharticlecaptcha":
                            result = HtmlPostWithArticle(context, emAuth.ServiceTicket);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    result = jss.Serialize("NOK - Message : " + ex.Message + " - InnerException : " + ex.InnerException);
                }
            }

            a.Logout(emAuth.ServiceTicket);

            context.Response.Write(result.ToString());
            context.Response.End();
        }


        private string HtmlPostWithArticle(HttpContext context, string serviceTicket)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string result = jss.Serialize("NOK");
            int articleId = -1;

            string pluginName = context.Request.Form["methodname"].Trim().ToLower();
            if (pluginName == "posthtmlwitharticlecaptcha")
            {
                #region Captcha Control
                string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
                string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
                if (string.IsNullOrEmpty(captchavalue))
                {
                    return result = jss.Serialize(new { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                }

                if (context.Session["CaptchaImageText" + captchaid] == null)
                {
                    return jss.Serialize(new { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                }

                if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
                {
                    return jss.Serialize(new { Code = "13", Message = "Captcha Error", Status = "CNOK" }); //jss.Serialize("CNOK");
                }

                context.Session["CaptchaImageText" + captchaid] = null;
                context.Session.Remove("CaptchaImageText" + captchaid);
                #endregion

            }

            #region Keys & Values
            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            List<string> keywords = new List<string> { "methodname", "articleid", "filaname" };

            keys = context.Request.Form.AllKeys.Where(x => !keywords.Contains(x)).ToList();
            foreach (string s in keys)
            {
                values.Add(HtmlAndUrlDecode(context.Request[s]));
            }
            #endregion

            string _articleid = (!string.IsNullOrEmpty(context.Request.Form["articleid"])) ? context.Request.Form["articleid"] : string.Empty;
            if (string.IsNullOrEmpty(_articleid))
            {
                string jsonResult = jss.Serialize(new { Code = "10", Message = "articleid Is Required", Status = "NOK" });
                return jsonResult;
            }
            else
            {
                try
                {
                    articleId = Convert.ToInt32(_articleid);
                }
                catch (Exception ex)
                {
                    string jsonResult = jsonResult = jss.Serialize(new { Code = "11", Message = "articleid Is Not In The Correct Format", ErrorMessage = ex.Message, Status = "NOK" });
                    return jsonResult;
                }
            }

            vArticlesZonesFull arBase = null;

            using (CmsDbContext dbContext = new CmsDbContext())
            {
                arBase = dbContext.vArticlesZonesFulls.Where(x => x.ArticleID == articleId).FirstOrDefault();
            }

            #region Variables
            string fromname = HtmlAndUrlDecode(arBase.Custom1.Trim());
            string fromaddress = HtmlAndUrlDecode(arBase.Custom2.Trim());
            string replyaddress = HtmlAndUrlDecode(arBase.Custom3.Trim());
            string subject = HtmlAndUrlDecode(arBase.Custom4.Trim());
            //string htmlbody = HtmlAndUrlDecode(arBase.Custom5.Trim());
            string charset = HtmlAndUrlDecode(arBase.Custom5.Trim());
            string toname = HtmlAndUrlDecode(arBase.Custom6.Trim());
            string toemailaddress = HtmlAndUrlDecode(arBase.Custom7.Trim());
            string templatepath = arBase.Custom8.Trim();
            List<EmAttachment> attachments = new List<EmAttachment>();
            #endregion

            string mailTemplate = "";

            if (!string.IsNullOrEmpty(templatepath))
            {
                mailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath(templatepath));
                //byte[] bytes = Encoding.Default.GetBytes(mailTemplate);
                //mailTemplate = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                mailTemplate =  WebUtility.HtmlEncode(arBase.Article1.Trim());
            }

            #region Create Attachments
            foreach (string fileName in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[fileName];
                byte[] fileBytes = null;
                //BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    //bf.Serialize(ms, file);
                    file.InputStream.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                attachments.Add(new EmAttachment { Content = fileBytes, Name = file.FileName, Type = file.ContentType });
            }
            #endregion

            Post p = new Post();
            EmPostResult emResult = new EmPostResult();

            for (int i = 0; i < keys.Count; i++)
            {
                if (mailTemplate.Contains("##" + keys[i] + "##"))
                {
                    mailTemplate = mailTemplate.Replace("##" + keys[i] + "##", values[i]);
                }
            }

            //mailTemplate = WebUtility.HtmlEncode(mailTemplate);

            emResult = p.PostHtml(serviceTicket, fromname, fromaddress, replyaddress, subject, mailTemplate, charset, toname, toemailaddress, attachments.ToArray());
            if (!string.IsNullOrEmpty(emResult.Message))
            {
                result = jss.Serialize("NOK - Message : " + emResult.Message + " - Error Code:" + emResult.Code + " - DetailedMessage : " + emResult.DetailedMessage +"  "+ mailTemplate);
                return result;
            }
            else
            {
                result = jss.Serialize("OK");
            }

            return result;
        }

        private string InsertMemberDemography(HttpContext context, string serviceTicket)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string result = jss.Serialize("NOK");
            string memberId = string.Empty;

            try
            {
                #region Keys & Values
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                List<string> keywords = new List<string> { "forceupdate", "methodname", "move" };
                keys = context.Request.Form.AllKeys.Where(x => !keywords.Contains(x)).ToList();
                foreach (string s in keys)
                {
                    values.Add(HtmlAndUrlDecode(context.Request.Form[s]));
                }
                #endregion
                bool forceUpdate = (context.Request.Form["forceupdate"] == null) ? false : Convert.ToBoolean(context.Request.Form["forceupdate"].ToString().Trim());
                //Eğer keys içinde EMAIL yoksa hata!
                if (!keys.Contains("EMAIL"))
                {
                    result = jss.Serialize("NOK - EMAIL zorunlu alan!");
                }
                else
                {
                    Member m = new Member();
                    string email = context.Request.Form["EMAIL"].ToString().Trim();
                    List<com.euromsg.member.EmKeyValue> keyValues = new List<com.euromsg.member.EmKeyValue>();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (keys[i] != "EMAIL" && keys[i] != "sendlistsgroupname" && keys[i] != "sendlistslistname")
                        {
                            keyValues.Add(new com.euromsg.member.EmKeyValue { Key = keys[i], Value = values[i] });
                        }
                    }

                    EmMemberResult emResult = m.InsertMemberDemography(serviceTicket, "EMAIL", email, keyValues.ToArray(), forceUpdate, out memberId);
                    if (!string.IsNullOrEmpty(emResult.Message))
                    {
                        result = jss.Serialize("NOK - Message : " + emResult.Message + " - DetailedMessage : " + emResult.DetailedMessage);
                    }
                    else
                    {
                        //kullanıcı oluşturuldu
                        #region Add to send list
                        if (keys.Contains("sendlistsgroupname") && keys.Contains("sendlistslistname"))
                        {
                            //kullanıcı kaydedildi - listeye eklenecek
                            if (!AddToList(context, serviceTicket, email))
                            {
                                //kullanıcı kaydedildi, listeye taşınamadı
                                result = jss.Serialize("NOK - Eklenen kullanıcı listeye taşınamadı");
                            }
                            else
                            {
                                //OK 
                                result = jss.Serialize("OK");
                            }
                        }
                        else
                        {
                            result = jss.Serialize("OK");
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                result = jss.Serialize("NOK - Message : " + ex.Message + " - InnerException : " + ex.InnerException);
            }

            return result;
        }

        private bool AddToList(HttpContext context, string serviceTicket, string email)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            bool result = false;
            #region SendList
            string sendlistsgroupname = HtmlAndUrlDecode((context.Request.Form["sendlistsgroupname"] == null) ? string.Empty : context.Request.Form["sendlistsgroupname"].ToString().Trim());
            string sendlistslistname = HtmlAndUrlDecode((context.Request.Form["sendlistslistname"] == null) ? string.Empty : context.Request.Form["sendlistslistname"].ToString().Trim());
            List<EmSendList> sendList = new List<EmSendList>();
            if (sendlistsgroupname.Contains(','))
            {
                for (int i = 0; i < sendlistslistname.Split(',').Length; i++)
                {
                    sendList.Add(new EmSendList { GroupName = sendlistsgroupname.Split(',')[i], ListName = sendlistslistname.Split(',')[i] });
                }
            }
            else
            {
                sendList.Add(new EmSendList { GroupName = sendlistsgroupname, ListName = sendlistslistname });
            }

            #endregion
            bool move = (context.Request.Form["move"] == null) ? false : Convert.ToBoolean(context.Request.Form["move"].ToString().Trim());

            Member m = new Member();
            EmMemberResult emResult = m.AddToSendLists(serviceTicket, "EMAIL", email, sendList.ToArray(), move);
            if (!string.IsNullOrEmpty(emResult.Message))
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }



        private string HtmlPost(HttpContext context, string serviceTicket)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string result = jss.Serialize("NOK");

            #region Variables
            string fromname = (context.Request.Form["fromname"] == null) ? string.Empty : context.Request.Form["fromname"].ToString().Trim();
            string fromaddress = (context.Request.Form["fromaddress"] == null) ? string.Empty : context.Request.Form["fromaddress"].ToString().Trim();
            string replyaddress = (context.Request.Form["replyaddress"] == null) ? string.Empty : context.Request.Form["replyaddress"].ToString().Trim();
            string subject = (context.Request.Form["subject"] == null) ? string.Empty : context.Request.Form["subject"].ToString().Trim();
            string htmlbody = (context.Request.Form["htmlbody"] == null) ? string.Empty : context.Request.Form["htmlbody"].ToString().Trim();
            string charset = (context.Request.Form["charset"] == null) ? string.Empty : context.Request.Form["charset"].ToString().Trim();
            string toname = (context.Request.Form["toname"] == null) ? string.Empty : context.Request.Form["toname"].ToString().Trim();
            string toemailaddress = (context.Request.Form["toemailaddress"] == null) ? string.Empty : context.Request.Form["toemailaddress"].ToString().Trim();
            List<EmAttachment> attachments = new List<EmAttachment>();
            #endregion

            #region Create Attachments
            foreach (string fileName in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[fileName];
                byte[] fileBytes = null;
                //BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    //bf.Serialize(ms, file);
                    file.InputStream.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                attachments.Add(new EmAttachment { Content = fileBytes, Name = file.FileName, Type = file.ContentType });
            }
            #endregion

            Post p = new Post();
            EmPostResult emResult = new EmPostResult();

            //htmlbody = WebUtility.HtmlEncode(htmlbody);

            emResult = p.PostHtml(serviceTicket, fromname, fromaddress, replyaddress, subject, htmlbody, charset, toname, toemailaddress, attachments.ToArray());
            if (!string.IsNullOrEmpty(emResult.Message))
            {
                result = jss.Serialize("NOK - Message : " + emResult.Message + " - Error Code:" + emResult.Code + " - DetailedMessage : " + emResult.DetailedMessage);
                return result;
            }
            else
            {
                result = jss.Serialize("OK");
            }

            return result;
        }

        private string AddToSendLists(HttpContext context, string serviceTicket)
        {
            /*
             * Gönderilecek Parametreler
             * move -> true/false
             * sendlistsgroupname -> group adları
             * sendlistslistname -> liste adları
             * keys / values
             */

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string result = jss.Serialize("NOK");
            try
            {
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                bool move = (context.Request.Form["move"] == null) ? false : Convert.ToBoolean(context.Request.Form["move"].ToString().Trim());
                List<string> keywords = new List<string> { "sendlistsgroupname", "sendlistslistname", "move", "methodname" };

                #region SendList
                string sendlistsgroupname = HtmlAndUrlDecode((context.Request.Form["sendlistsgroupname"] == null) ? string.Empty : context.Request.Form["sendlistsgroupname"].ToString().Trim());
                string sendlistslistname = HtmlAndUrlDecode((context.Request.Form["sendlistslistname"] == null) ? string.Empty : context.Request.Form["sendlistslistname"].ToString().Trim());
                List<EmSendList> sendList = new List<EmSendList>();
                if (sendlistsgroupname.Contains(','))
                {
                    for (int i = 0; i < sendlistslistname.Split(',').Length; i++)
                    {
                        sendList.Add(new EmSendList { GroupName = sendlistsgroupname.Split(',')[i], ListName = sendlistslistname.Split(',')[i] });
                    }
                }
                else
                {
                    sendList.Add(new EmSendList { GroupName = sendlistsgroupname, ListName = sendlistslistname });
                }

                #endregion
                #region Keys & Values
                keys = context.Request.Form.AllKeys.Where(x => !keywords.Contains(x)).ToList();
                foreach (string s in keys)
                {
                    values.Add(HtmlAndUrlDecode(context.Request.Form[s]));
                }
                #endregion

                if (keys.Count != values.Count)
                {
                    result = jss.Serialize("NOK - Key sayısı ve Value sayısı eşit değil");
                    return result;
                }

                EmMemberResult em = new EmMemberResult();
                Member m = new Member();

                for (int i = 0; i < keys.Count; i++)
                {
                    em = m.AddToSendLists(serviceTicket, keys[i], values[i], sendList.ToArray(), move);
                    if (!string.IsNullOrEmpty(em.Message))
                    {
                        result = jss.Serialize("NOK - Message : " + em.Message + " - DetailedMessage : " + em.DetailedMessage);
                        return result;
                    }
                    else
                    {
                        result = jss.Serialize("OK");
                    }
                }
            }
            catch (Exception ex)
            {
                result = jss.Serialize("NOK - Message : " + ex.Message + " - InnerException : " + ex.InnerException);
                return result;
            }

            return result;
        }

        private string SendOnGoingMail(HttpContext context, string serviceTicket)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string result = jss.Serialize("NOK");
            string memberId = string.Empty;
            string transActionID = string.Empty;
            try
            {
                #region Keys & Values
                List<string> keys = new List<string>();
                List<string> values = new List<string>();
                keys = context.Request.Form.AllKeys.ToList();
                foreach (string s in keys)
                {
                    values.Add(HtmlAndUrlDecode(context.Request.Form[s]));
                }
                #endregion
                bool forceUpdate = string.IsNullOrEmpty(context.Request.Form["forceupdate"]) ? false : Convert.ToBoolean(context.Request.Form["forceupdate"].ToString().Trim());
                string campaignID = !string.IsNullOrEmpty(context.Request.Form["campaignid"]) ? HtmlAndUrlDecode(context.Request.Form["campaignid"].Trim()) : "";
                memberId = !string.IsNullOrEmpty(context.Request.Form["memberid"]) ? HtmlAndUrlDecode(context.Request.Form["memberid"].Trim()) : "";
                //transActionID = !string.IsNullOrEmpty(context.Request.Form["transactionid"]) ? HtmlAndUrlDecode(context.Request.Form["transactionid"].Trim()) : "";
                if (string.IsNullOrEmpty(campaignID))
                {
                    result = jss.Serialize("NOK - CAMPAIGNID zorunlu alan!");
                    return result;
                }
                if (!keys.Contains("EMAIL") && string.IsNullOrEmpty(memberId))
                {
                    result = jss.Serialize("NOK - EMAIL veya MEMBERID zorunlu alan!");
                    return result;
                }
                else
                {
                    Member m = new Member();
                    string email = context.Request.Form["EMAIL"].ToString().Trim();
                    List<com.euromsg.member.EmKeyValue> keyValues = new List<com.euromsg.member.EmKeyValue>();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (keys[i] != "EMAIL" && keys[i] != "campaignid" && keys[i] != "memberid" && keys[i] != "transactionid" && keys[i] != "methodname" && keys[i] != "forceupdate")
                        {
                            com.euromsg.member.EmKeyValue addValue = new com.euromsg.member.EmKeyValue();
                            addValue.Key = HtmlAndUrlDecode(keys[i]);
                            addValue.Value = HtmlAndUrlDecode(context.Request.Form[keys[i]]);
                            keyValues.Add(addValue);
                            //keyValues.Add(new com.euromsg.member.EmKeyValue { Key = HtmlAndUrlDecode(keys[i]), Value = HtmlAndUrlDecode(values[i]) });
                        }
                    }

                    EuroCMS.FrontEnd.com.euromsg.member.EmKeyValue[] EmKeysArray = keyValues.ToArray();

                    EmMemberResult emResult = m.SendOngoingEMail(serviceTicket, "EMAIL", email, EmKeysArray, forceUpdate, campaignID, out memberId, out transActionID);
                    if (emResult.Code != "00")
                    {
                        result = jss.Serialize("NOK - Message : " + emResult.Message + " - DetailedMessage : " + emResult.DetailedMessage + " - ErrorCode : " + emResult.Code);
                    }
                    else
                    {
                        result = jss.Serialize("OK - " + transActionID);
                    }

                }
            }
            catch (Exception ex)
            {
                result = jss.Serialize("NOK - Message : " + ex.Message + " - InnerException : " + ex.InnerException);
            }

            return result;
        }

        public string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
        }
        public string HtmlAndUrlEncode(string value)
        {
            value = HttpUtility.HtmlEncode(value).Trim();
            return value;
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