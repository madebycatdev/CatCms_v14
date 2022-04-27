using EuroCMS.Admin.Common;
using EuroCMS.CMSPlugin.StandardProfil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Text;
using EuroCMS.Model;
using EuroCMS.Core;
using NPOI.HSSF.UserModel;
using System.IO;
using ExcelDataReader;
using System.Data;

namespace EuroCMS.CMSPlugin.StandardProfil.Controllers
{
    public class InvestorRelationsController : BaseController
    {
        // GET: InvesterRelations

        string rejectedMailText = "<p>Thank you for your interest in Standard Profil Investor Relations.</p><p>Due to our information privacy policy we are unable to approve your request.<br/>Thank you for your understanding.</p><p>Standard Profil Investor Relations</p>";

        string approvedMailText = "<p>Thank you for your interest in Standard Profil Investor Relations.<br/>Your username and password to log in to the Standard Profil investor portal are as follows:/p><p> Username : <span stle=\"text-decoration:none;\">{{username}}</span> <br/> Password : {{password}}</p><p>For all questions relating to Standard Profil Investor Relations please reach out to <span stle=\"text-decoration:none;\">IR@standardprofil.com.</span></p>";

        string resetPasswordText = "<p>Your password has been reset.. New password: {{password}}</p>";


        public ActionResult ExportUsers(int status, string companyName, string email, string startDate, string endDate)
        {
            StandardProfilDbContext context = new StandardProfilDbContext();
            var query = context.InvestorUsers.AsQueryable();
            string statusText = string.Empty;
            query = query.Where(x => x.Status == status);
            switch (status)
            {
                default:
                case 1:
                    statusText = "Approved";
                    break;
                case 0:
                    statusText = "Waiting";
                    break;
                case -1:
                    statusText = "Rejected";
                    break;
                case -2:
                    statusText = "Deleted";
                    break;
            }

            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime startDateTime;
                DateTime.TryParse(startDate, out startDateTime);
                if (startDateTime > DateTime.MinValue)
                {
                    query = query.Where(x => x.CreatedDate >= startDateTime);
                }
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime endDateTime;
                DateTime.TryParse(endDate, out endDateTime);
                if (endDateTime > DateTime.MinValue)
                {
                    query = query.Where(x => x.CreatedDate <= endDateTime);
                }
            }

            if (!string.IsNullOrEmpty(companyName))
                query = query.Where(x => x.CompanyName.ToLower().Contains(companyName.ToLower()));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));

            var users = query.OrderBy(x => x.CreatedDate).ToList();

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Email");
            sheetRow.CreateCell(2).SetCellValue("Name");
            sheetRow.CreateCell(3).SetCellValue("Surname");
            sheetRow.CreateCell(4).SetCellValue("CompanyName");
            sheetRow.CreateCell(5).SetCellValue("Status");
            sheetRow.CreateCell(6).SetCellValue("CreatedDate");


            sheetRowIndex++;
            foreach (var item in users)
            {
                sheetRow = sheet.CreateRow(sheetRowIndex);
                sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Name)));
                sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Surname)));
                sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CompanyName)));
                sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(statusText)));
                sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CreatedDate.ToString())));
                sheetRowIndex++;
            }
            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {

                workbook.Write(exportData);
                string saveAsFileName = string.Format("InvestorUsers_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
            return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/exportusers.cshtml");
        }
        public ActionResult Users(int? page, int? status, string companyName, string email, string startDate, string endDate)
        {
            StandardProfilDbContext context = new StandardProfilDbContext();

            status = status == null ? (int)UserStatus.Approved : status;
            var query = context.InvestorUsers.Where(x => x.Status == status).AsQueryable();

            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime startDateTime;
                DateTime.TryParse(startDate, out startDateTime);
                if (startDateTime > DateTime.MinValue)
                {
                    query = query.Where(x => x.CreatedDate >= startDateTime);
                }
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime endDateTime;
                DateTime.TryParse(endDate, out endDateTime);
                if (endDateTime > DateTime.MinValue)
                {
                    query = query.Where(x => x.CreatedDate <= endDateTime);
                }
            }

            if (!string.IsNullOrEmpty(companyName))
                query = query.Where(x => x.CompanyName.ToLower().Contains(companyName.ToLower()));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));


            int pageNum = (page == null) ? 1 : Convert.ToInt32(page);
            ViewBag.ListStatus = status;
            ViewBag.Email = email;
            ViewBag.CompanyName = companyName;
            ViewBag.UsersCount = query.Count();

            return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/Users.cshtml", query.OrderByDescending(x => x.CreatedDate).ToPagedList(pageNum, 30));
        }

        public ActionResult ImportUsers(string groupName)
        {
            StandardProfilDbContext context = new StandardProfilDbContext();
            var groupNames = context.ImportInvestorUsers.Select(x => x.ImportGroupName).Distinct().ToList();
            groupName = string.IsNullOrEmpty(groupName) && groupNames != null ? groupNames.FirstOrDefault() : groupName;

            ViewBag.GroupName = groupName;
            ViewBag.GroupNames = groupNames;
            var users = context.ImportInvestorUsers.Where(x => x.ImportGroupName == groupName).OrderByDescending(x => x.CompanyName).ToList();

            return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/ImportUsers.cshtml", users);

        }

        [HttpPost]
        public ActionResult ImportUsers(HttpPostedFileBase file)
        {
            string groupName = "Import-File-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm");
            if (file != null && file.ContentLength > 0 && Path.GetExtension(file.FileName).Contains("xlsx"))
            {
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
                var dataSet = excelReader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true // Use first row is ColumnName here :D
                    }
                });
                if (dataSet.Tables.Count > 0)
                {
                    StandardProfilDbContext context = new StandardProfilDbContext();

                    var dtData = dataSet.Tables[0];
                    List<ImportInvestorUser> list = new List<ImportInvestorUser>();

                    foreach (DataRow item in dtData.Rows)
                    {
                        ImportInvestorUser user = new ImportInvestorUser();
                        user.Name = item[0].ToString();
                        user.Surname = item[1].ToString();
                        user.Email = item[2].ToString();
                        user.CompanyName = item[3].ToString();
                        user.Status = (int)UserStatus.Waiting;
                        user.CreatedDate = DateTime.Now;
                        user.ImportGroupName = groupName;
                        list.Add(user);

                    }

                    context.ImportInvestorUsers.AddRange(list);
                    context.SaveChanges();
                }
            }
            else
                ViewBag.Status = JsonHelper.Serialize(new { success = false, status = 400, message = "The file must be excel file." });

            return RedirectToAction("ImportUsers", new { groupName = groupName });
        }

        public ActionResult AddUser()
        {
            InvestorUser user = new InvestorUser();

            return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/AddUser.cshtml", user);
        }

        [HttpPost]
        public ActionResult AddUser(InvestorUser user)
        {
            try
            {
                StandardProfilDbContext context = new StandardProfilDbContext();
                int existUser = context.InvestorUsers.Count(x => x.Email == user.Email);

                if (existUser > 0)
                {
                    ViewBag.Status = JsonHelper.Serialize(new { success = false, status = 400, message = "User is exist." });
                    return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/AddUser.cshtml", user);
                }

                user.CreatedDate = DateTime.Now;
                var mailingArticle = context.Articles.Where(x => x.Id == 395).FirstOrDefault();

                if (user.Status == (int)UserStatus.Approved)
                {
                    string password = CreateRandomPassword();
                    user.Password = Utility.Crypt.Encrypt(password);

                    SendMail(mailingArticle, user.Email, user.Name, user.Surname, 1, password);
                }
                else if (user.Status == (int)UserStatus.Rejected)
                {
                    SendMail(mailingArticle, user.Email, user.Name, user.Surname, 2);
                }

                user.isExcelUser = false;
                context.InvestorUsers.Add(user);
                context.SaveChanges();

                ViewBag.Status = JsonHelper.Serialize(new { success = true, status = 200, message = "User was created." });
                return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/AddUser.cshtml", new InvestorUser());
            }
            catch (Exception)
            {
                ViewBag.Status = JsonHelper.Serialize(new { success = false, status = 400, message = "User was not created." });
            }

            return View("~/Views/CMSPlugins/StandardProfil/InvestorRelations/AddUser.cshtml", user);
        }

        private int[] ConvertToIntArray(string key)
        {
            var keys = key.Trim().Split(',');
            List<int> intArray = new List<int>();
            foreach (var item in keys)
            {
                int val = 0;
                Int32.TryParse(item, out val);
                if (val > 0)
                    intArray.Add(val);
            }

            return intArray.ToArray();
        }
        public string ChangeUsersStatus(string ids, int status)
        {
            try
            {

                var intIds = ConvertToIntArray(ids);
                StandardProfilDbContext context = new StandardProfilDbContext();
                var mailingArticle = context.Articles.Where(x => x.Id == 395).FirstOrDefault();
                if (mailingArticle != null)
                {
                    string fromAddress = HtmlAndUrlDecode(mailingArticle.Custom1.Trim());
                }

                var users = context.InvestorUsers.Where(x => intIds.Contains(x.Id)).ToList();
                foreach (var item in users)
                {
                    item.Status = status;
                    item.UpdatedDate = DateTime.Now;

                    if (status == 1)
                    {
                        string password = CreateRandomPassword();
                        item.DecryptPassword = password;
                        item.Password = Utility.Crypt.Encrypt(password);
                    }

                    context.SaveChanges();
                }

                string mailBody = string.Empty;


                foreach (var user in users)
                {
                    if (status == 1)
                    {
                        SendMail(mailingArticle, user.Email, user.Name, user.Surname, 1, user.DecryptPassword);
                    }
                    else if (status == -1)
                    {
                        SendMail(mailingArticle, user.Email, user.Name, user.Surname, 2);
                    }


                }

                return JsonHelper.Serialize(new { success = true, status = 200, message = "Users updated." });
            }
            catch (Exception ex)
            {
                return JsonHelper.Serialize(new { success = false, status = 500, message = "An error occured! " + ex.Message });
            }
        }

        public string ChangeImportUsersStatus(string ids, int status)
        {
            try
            {
                var intIds = ConvertToIntArray(ids);
                StandardProfilDbContext context = new StandardProfilDbContext();
                var existUsers = context.InvestorUsers.ToList();
                var mailingArticle = context.Articles.Where(x => x.Id == 395).FirstOrDefault();
                var users = context.ImportInvestorUsers.Where(x => intIds.Contains(x.Id)).ToList();
                List<InvestorUser> investorUsers = new List<InvestorUser>();
                foreach (var user in users)
                {
                    var userExist = existUsers.Count(x => x.Email == user.Email);
                    if (userExist == 0)
                    {
                        InvestorUser investorUser = new InvestorUser();
                        investorUser.Name = user.Name;
                        investorUser.Surname = user.Surname;
                        investorUser.Email = user.Email;
                        investorUser.CompanyName = user.CompanyName;
                        investorUser.CreatedDate = DateTime.Now;
                        investorUser.Status = status;
                        investorUser.isExcelUser = true;

                        user.Status = status;
                        user.UpdatedDate = DateTime.Now;

                        if (status == 1)
                        {
                            string password = CreateRandomPassword();
                            investorUser.DecryptPassword = password;
                            investorUser.Password = Utility.Crypt.Encrypt(password);


                            context.InvestorUsers.Add(investorUser);
                            investorUsers.Add(investorUser);
                            context.SaveChanges();
                        }
                    }

                    string mailBody = string.Empty;

                }

                foreach (var user in investorUsers)
                {
                    if (user.Status == 1)
                    {
                        SendMail(mailingArticle, user.Email, user.Name, user.Surname, 1, user.DecryptPassword);
                    }
                    else if (user.Status == -1)
                    {
                        SendMail(mailingArticle, user.Email, user.Name, user.Surname, 2);
                    }


                }

                return JsonHelper.Serialize(new { success = true, status = 200, message = "Users updated." });
            }
            catch (Exception ex)
            {
                return JsonHelper.Serialize(new { success = false, status = 500, message = "An error occured! " + ex.Message });
            }
        }
        public string ChangeImportUserStatus(int id, int status)
        {

            StandardProfilDbContext context = new StandardProfilDbContext();
            var user = context.ImportInvestorUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return JsonHelper.Serialize(new { success = false, status = 400, message = "User Not found!" });

            var existUser = context.ImportInvestorUsers.Count(x => x.Email == user.Email);
            if (existUser > 0)
                return JsonHelper.Serialize(new { success = false, status = 400, message = "User Email is exist!" });

            var mailingArticle = context.Articles.Where(x => x.Id == 395).FirstOrDefault();
            InvestorUser investorUser = new InvestorUser();
            investorUser.Name = user.Name;
            investorUser.Surname = user.Surname;
            investorUser.Email = user.Email;
            investorUser.CompanyName = user.CompanyName;
            investorUser.CreatedDate = DateTime.Now;
            investorUser.Status = status;

            user.Status = status;

            try
            {
                investorUser.UpdatedDate = DateTime.Now;
                investorUser.isExcelUser = true;

                if (status == 1)
                {
                    string password = CreateRandomPassword();
                    investorUser.Password = Utility.Crypt.Encrypt(password);

                    SendMail(mailingArticle, investorUser.Email, investorUser.Name, investorUser.Surname, 1, password);
                }
                else if (status == -1)
                {
                    SendMail(mailingArticle, investorUser.Email, investorUser.Name, investorUser.Surname, 2);
                }

                context.InvestorUsers.Add(investorUser);
                context.SaveChanges();
                return JsonHelper.Serialize(new { success = true, status = 200, message = "User updated." });
            }
            catch (Exception ex)
            {
                return JsonHelper.Serialize(new { success = false, status = 500, message = "An error occured! " + ex.Message });
            }

        }

        public string ChangeStatus(int id, int status)
        {

            StandardProfilDbContext context = new StandardProfilDbContext();
            var user = context.InvestorUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return JsonHelper.Serialize(new { success = false, status = 400, message = "User Not found!" });

            var mailingArticle = context.Articles.Where(x => x.Id == 395).FirstOrDefault();
            user.Status = status;

            try
            {

                user.UpdatedDate = DateTime.Now;

                if (status == 1)
                {
                    string password = CreateRandomPassword();
                    user.Password = Utility.Crypt.Encrypt(password);

                    SendMail(mailingArticle, user.Email, user.Name, user.Surname, 1, password);
                }
                else if (status == -1)
                {
                    SendMail(mailingArticle, user.Email, user.Name, user.Surname, 2);
                }

                context.SaveChanges();
                return JsonHelper.Serialize(new { success = true, status = 200, message = "User updated." });
            }
            catch (Exception ex)
            {
                return JsonHelper.Serialize(new { success = false, status = 500, message = "An error occured! " + ex.Message });
            }

        }

        public string SendPassword(int id)
        {
            StandardProfilDbContext context = new StandardProfilDbContext();
            var user = context.InvestorUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
                JsonHelper.Serialize(new { success = false, status = 400, message = "User Not found!" });

            try
            {
                string password = CreateRandomPassword();
                user.Password = Utility.Crypt.Encrypt(password);
                user.UpdatedDate = DateTime.Now;
                context.SaveChanges();

                var mailingArticle = context.Articles.Where(x => x.Id == 395).FirstOrDefault();
                SendMail(mailingArticle, user.Email, user.Name, user.Surname, 0, password);

                return JsonHelper.Serialize(new { success = true, status = 200, message = "User password was sent. Password is :" + password });


            }
            catch (Exception ex)
            {
                return JsonHelper.Serialize(new { success = false, status = 500, message = "An error occured! " + ex.Message });
            }

        }

        private string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private string HtmlAndUrlDecode(string value)
        {
            value = HttpUtility.UrlDecode(HttpUtility.HtmlDecode(value)).Trim();
            return value;
        }

        private int SendMail(Article mailingArticle, string email, string name, string surname, int mailType, string password = null)
        {
            try
            {
                if (mailingArticle != null)
                {
                    string mailBody = string.Empty;
                    switch (mailType)
                    {
                        case 1: //approved mail
                            mailBody = mailingArticle.Custom6.Trim();
                            mailBody = mailBody.Replace("{{username}}", email).Replace("{{password}}", password);
                            break;
                        case 0: //renew password
                            mailBody = mailingArticle.Custom8.Trim();
                            mailBody = mailBody.Replace("{{password}}", password);
                            break;
                        case 2: //rejected mail
                            mailBody = mailingArticle.Custom7.Trim();
                            break;
                    }

                    string fromAddress = HtmlAndUrlDecode(mailingArticle.Custom1.Trim());
                    string toAddress = email;
                    string ccAddress = HtmlAndUrlDecode(mailingArticle.Custom3.Trim());
                    string bccAddress = HtmlAndUrlDecode(mailingArticle.Custom4.Trim());
                    string subject = HtmlAndUrlDecode(mailingArticle.Custom5.Trim());
                    string mailTemplate = HtmlAndUrlDecode(mailingArticle.Article1.Trim());

                    List<string> ccList = new List<string>();
                    List<string> bccList = new List<string>();
                    #region CC
                    if (ccAddress.Contains(","))
                    {
                        ccList = ccAddress.Split(',').ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ccAddress))
                        {
                            ccList.Add(ccAddress);
                        }
                    }
                    #endregion
                    #region Bcc
                    if (bccAddress.Contains(","))
                    {
                        bccList = bccAddress.Split(',').ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(bccAddress))
                        {
                            bccList.Add(bccAddress);
                        }
                    }
                    #endregion

                    mailTemplate = mailTemplate.Replace("##NameSuename##", name + " " + surname);
                    mailTemplate = mailTemplate.Replace("##Message##", mailBody);


                    var mailResult = MailSender.SendMail(toAddress, string.Join(",", ccList), string.Join(",", bccList), subject, mailTemplate, null);
                    if (!mailResult.status)
                    {
                        return 505;
                    }
                }
            }
            catch (Exception ex)
            {
                return 505;
            }

            return 200;
        }

        public enum UserStatus
        {
            Waiting = 0,
            Approved = 1,
            Rejected = -1,
            Deleted = -2
        }
    }
}