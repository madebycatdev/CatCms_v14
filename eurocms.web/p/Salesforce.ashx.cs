using EuroCMS.FrontEnd.Salesforce;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;


namespace EuroCMS.FrontEnd.p
{
    public class Salesforce : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string methodName = string.Empty;
            string scc = string.Empty;
            string result = string.Empty;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SforceService sfdcBinding = new SforceService();
            LoginResult currentLoginResult = new LoginResult();



            Salesforce.SalesForceResult createResult = new SalesForceResult();
            createResult.Code = "0";
            createResult.Status = "NOK";
            createResult.Message = "methodname required.";
            createResult.ErrorMessage = "methodname required.";

            context.Response.Clear();

            methodName = !string.IsNullOrEmpty(context.Request.Form["methodname"]) ? HtmlAndUrlDecode(context.Request.Form["methodname"]) : string.Empty;

            
            if (string.IsNullOrEmpty(methodName))
            {
                result = jss.Serialize(createResult);
                context.Response.Write(result);
                context.Response.End();
            }
            scc = !string.IsNullOrEmpty(context.Request.Form["scc"]) ? HtmlAndUrlDecode(context.Request.Form["scc"]).ToLower() : string.Empty;
            if (!string.IsNullOrEmpty(scc))
            {
                createResult.Message = "Form security control error.";
                createResult.ErrorMessage = "Form security control error.";
                result = jss.Serialize(createResult);
                context.Response.Write(result);

                CreateSalesforceLogs(context, "SCC Prevention", context.Request.ServerVariables["REMOTE_ADDR"], false);
                context.Response.End();
            }


            #region Captcha Control

            string captchaid = (!string.IsNullOrEmpty(context.Request.Form["captchaid"])) ? context.Request.Form["captchaid"] : string.Empty;
            string captchavalue = (!string.IsNullOrEmpty(context.Request.Form["captchavalue"])) ? context.Request.Form["captchavalue"] : string.Empty;
            if (string.IsNullOrEmpty(captchavalue))
            {
                createResult.Code = "13";
                createResult.Status = "CNOK";
                createResult.Message = "Captcha Error";
                createResult.ErrorMessage = "Captcha Error";
                result = jss.Serialize(createResult);
                context.Response.Write(result);
                context.Response.End();
            }

            if (context.Session["CaptchaImageText" + captchaid] == null)
            {
                createResult.Code = "13";
                createResult.Status = "CNOK";
                createResult.Message = "Captcha Error";
                createResult.ErrorMessage = "Captcha Error";
                result = jss.Serialize(createResult);
                context.Response.Write(result);
                context.Response.End();
            }

            if (context.Session["CaptchaImageText" + captchaid].ToString().Trim().ToLower() != captchavalue.Trim().ToLower())
            {
                createResult.Code = "13";
                createResult.Status = "CNOK";
                createResult.Message = "Captcha Error";
                createResult.ErrorMessage = "Captcha Error";
                result = jss.Serialize(createResult);
                context.Response.Write(result);
                context.Response.End();
            }

            context.Session["CaptchaImageText" + captchaid] = null;
            context.Session.Remove("CaptchaImageText" + captchaid);
            #endregion



            try
            {
                //Login ol
                currentLoginResult = sfdcBinding.login(System.Configuration.ConfigurationManager.AppSettings["SForceUsername"], System.Configuration.ConfigurationManager.AppSettings["SForcePassword"] + System.Configuration.ConfigurationManager.AppSettings["SForceToken"]);

            }
            catch (Exception ex)
            {
                createResult.Message = "Login error.";
                createResult.ErrorMessage = ex.Message + ex.InnerException + ex.StackTrace;

                sfdcBinding = null;
                CreateSalesforceLogs(context, ex.Message, ex.StackTrace, false);
            }

            if (sfdcBinding != null)
            {
                sfdcBinding.Url = currentLoginResult.serverUrl;
                sfdcBinding.SessionHeaderValue = new SessionHeader();
                sfdcBinding.SessionHeaderValue.sessionId = currentLoginResult.sessionId;

                try
                {
                    #region Metodlar
                    switch (methodName)
                    {
                        case "addtoleads":
                            createResult = addToLeads(context, sfdcBinding, createResult);
                            break;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    createResult.Message = "Login error.";
                    createResult.ErrorMessage = ex.Message + ex.InnerException + ex.StackTrace;

                    CreateSalesforceLogs(context, ex.Message, ex.StackTrace, false);
                }
            }
            result = jss.Serialize(createResult);
            context.Response.Write(result);
            context.Response.End();
        }
        #region Add To Leads
        private SalesForceResult addToLeads(HttpContext context, SforceService sfdcBinding, Salesforce.SalesForceResult createResult)
        {
            try
            {
                string Name = !string.IsNullOrEmpty(context.Request.Form["Name"]) ? HtmlAndUrlDecode(context.Request.Form["Name"]) : string.Empty;
                string Surname = !string.IsNullOrEmpty(context.Request.Form["Surname"]) ? HtmlAndUrlDecode(context.Request.Form["Surname"]) : string.Empty;
                string Email = !string.IsNullOrEmpty(context.Request.Form["Email"]) ? HtmlAndUrlDecode(context.Request.Form["Email"]) : string.Empty;

                if ((string.IsNullOrEmpty(Name)) || (string.IsNullOrEmpty(Surname)) || (string.IsNullOrEmpty(Email)))
                {
                    createResult.Message = "Name, Surname, Email required.";
                    createResult.ErrorMessage = createResult.Message;
                }
                else
                {
                    string Phone = !string.IsNullOrEmpty(context.Request.Form["Phone"]) ? HtmlAndUrlDecode(context.Request.Form["Phone"]) : string.Empty;
                    string Company = !string.IsNullOrEmpty(context.Request.Form["Company"]) ? HtmlAndUrlDecode(context.Request.Form["Company"]) : string.Empty;
                    string Country = !string.IsNullOrEmpty(context.Request.Form["Country"]) ? HtmlAndUrlDecode(context.Request.Form["Country"]) : string.Empty;
                    string ProductInterest = !string.IsNullOrEmpty(context.Request.Form["ProductInterest"]) ? HtmlAndUrlDecode(context.Request.Form["ProductInterest"]) : string.Empty;

                    bool Subscribe = (!string.IsNullOrEmpty(context.Request.Form["Subscribe"]) && Boolean.TryParse(context.Request.Form["Subscribe"], out Subscribe)) ? Convert.ToBoolean(context.Request.Form["Subscribe"]) : true;
                    string Lang = !string.IsNullOrEmpty(context.Request.Form["Lang"]) ? HtmlAndUrlDecode(context.Request.Form["Lang"]) : string.Empty;
                    string gclid = !string.IsNullOrEmpty(context.Request.Form["gclid"]) ? HtmlAndUrlDecode(context.Request.Form["gclid"]) : string.Empty;
                    string pageName = !string.IsNullOrEmpty(context.Request.Form["pageName"]) ? HtmlAndUrlDecode(context.Request.Form["pageName"]) : string.Empty;
                    string Description = !string.IsNullOrEmpty(context.Request.Form["Description"]) ? HtmlAndUrlDecode(context.Request.Form["Description"]) : string.Empty;
                    string gad_keyword__c = !string.IsNullOrEmpty(context.Request.Form["gad_keyword__c"]) ? HtmlAndUrlDecode(context.Request.Form["gad_keyword__c"]) : string.Empty;
                    string gad__c = !string.IsNullOrEmpty(context.Request.Form["gad__c"]) ? HtmlAndUrlDecode(context.Request.Form["gad__c"]) : string.Empty;
                    string LeadSource = !string.IsNullOrEmpty(context.Request.Form["LeadSource"]) ? HtmlAndUrlDecode(context.Request.Form["LeadSource"]) : string.Empty;
                    string Related_Company__c = !string.IsNullOrEmpty(context.Request.Form["Related_Company__c"]) ? HtmlAndUrlDecode(context.Request.Form["Related_Company__c"]) : string.Empty;

                    bool Agency__c = (!string.IsNullOrEmpty(context.Request.Form["Agency__c"]) && Boolean.TryParse(context.Request.Form["Agency__c"], out Agency__c)) ? Convert.ToBoolean(context.Request.Form["Agency__c"]) : true;
                    //string DataVolume = context.Request.Form["DataVolume"] ?? string.Empty;
                    //string SendingVolume = context.Request.Form["SendingVolume"] ?? string.Empty;
                    //string PersonTitle = context.Request.Form["PersonTitle"] ?? string.Empty;
                    string Website = context.Request.Form["Website"] ?? string.Empty;
                    int NumberOfEmployees = !string.IsNullOrEmpty(context.Request.Form["NumberOfEmployees"]) ? Convert.ToInt32(context.Request.Form["NumberOfEmployees"]) : 0 ;
                    //string Reference = context.Request.Form["Reference"] ?? string.Empty;
                    //string Note = context.Request.Form["Note"] ?? string.Empty;

                    Lead sfdcLead = new Lead();

                    sfdcLead.FirstName = Name;
                    sfdcLead.LastName = Surname;
                    sfdcLead.Email = Email;
                    sfdcLead.gad_keyword__c = gad_keyword__c;
                    sfdcLead.gad__c = gad__c;
                    sfdcLead.LeadSource = LeadSource;

                    sfdcLead.NumberOfEmployees = NumberOfEmployees;
                    sfdcLead.NumberOfEmployeesSpecified = true;

                    sfdcLead.Phone = Phone;
                    sfdcLead.Company = Company;
                    sfdcLead.Country = Country;
                    sfdcLead.Product_Interest__c = ProductInterest;

                    //sfdcLead.Data_Volume__c = DataVolume;
                    //sfdcLead.Sending_Volume__c = SendingVolume;
                    //sfdcLead.Title = PersonTitle;
                    sfdcLead.Website = Website;
                    //sfdcLead.LeadSource = Reference;
                    //sfdcLead.Notes__c = Note;


                    sfdcLead.HasOptedOutOfEmail = Subscribe;
                    sfdcLead.HasOptedOutOfEmailSpecified = Subscribe;
                    sfdcLead.Language__c = Lang;
                    sfdcLead.gclid__c = gclid;
                    sfdcLead.created_reference__c = pageName;
                    sfdcLead.Description = Description;
                    sfdcLead.Related_Company__c = Related_Company__c;

                    sfdcLead.Agency__c = Agency__c;
                    sfdcLead.Agency__cSpecified = true;

                    AssignmentRuleHeader arh = new AssignmentRuleHeader();
                    arh.useDefaultRule = true;
                    sfdcBinding.AssignmentRuleHeaderValue = arh;

                    SaveResult[] saveResults = sfdcBinding.create(new sObject[] { sfdcLead });

                    if (saveResults != null && saveResults[0].success)
                    {
                        string Id = "";
                        Id = saveResults[0].id;
                        createResult.Code = "1";
                        createResult.Status = "OK";
                        createResult.Message = "Record created succesfully. ID:" + Id;
                        createResult.ErrorMessage = Agency__c + " - " + Description ;

                    }
                    else
                    {
                        CreateSalesforceLogs(context, saveResults[0].errors[0].statusCode.ToString(), saveResults[0].errors[0].message.ToString(), false);
                        
                        createResult.Message = "Record could not created.";
                        if (saveResults == null)
                        {
                            createResult.ErrorMessage = "saveResult is null";
                            return createResult;
                        }

                        if (saveResults.Length <= 0)
                        {
                            createResult.ErrorMessage = "saveResult Array is null";
                            return createResult;
                        }
                        if (saveResults[0].errors.Length <= 0)
                        {
                            createResult.ErrorMessage = "saveResult Errors array is null";
                            return createResult;
                        }
                        createResult.ErrorMessage = saveResults[0].errors[0].statusCode + " - InnerException : " + saveResults[0].errors[0].message;
                   }

                }
            }
            catch (Exception ex)
            {
                createResult.Message = ex.Message;
                createResult.ErrorMessage = ex.InnerException + ex.StackTrace;

                CreateSalesforceLogs(context, ex.Message, ex.StackTrace, false);
            }

            return createResult;
        }

        #region Create Log
        private void CreateSalesforceLogs(HttpContext context, string errorType, string detail, bool isSuccess)
        {


            List<string> formKeys = new List<string>();
            formKeys = context.Request.Form.AllKeys.ToList();
            string allValues = "";
            foreach (string key in formKeys)
            {
                allValues += Environment.NewLine + key + ": " + context.Request.Form[key].ToString();
            }

            detail += errorType + Environment.NewLine;
            detail += allValues;
            string folderPath = "SalesforceLogs";

            bool isLog = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SalesforceLogs"]) ? false : (ConfigurationManager.AppSettings["SalesforceLogs"] == "on" ? true : false);

            if (!isLog)
            {
                return;
            }

            bool existsFolder = Directory.Exists(context.Server.MapPath("~/" + folderPath));

            if (!existsFolder)
            {
                Directory.CreateDirectory(context.Server.MapPath("~/" + folderPath));
            }

            string nowFileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss");//DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + (string.IsNullOrEmpty(errorType) ? "-" : "-" + errorType + "-") + (isSuccess ? "Success" : "Error");

            string filePath = context.Server.MapPath("~/" + folderPath + "/" + nowFileName + ".txt");

            if (!File.Exists(filePath))
            {
                FileStream f = File.Create(filePath);
                f.Close();
            }

            TextWriter tw = new StreamWriter(filePath, true, System.Text.Encoding.UTF8);

            if (!string.IsNullOrEmpty(detail))
            {
                tw.WriteLine("Detay: " + detail);
            }

            tw.Close();

        }
        #endregion

        #endregion
        #region Salesforce Result
        public class SalesForceResult
        {
            public string Status { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
            public string ErrorMessage { get; set; }
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
        }
    }


}