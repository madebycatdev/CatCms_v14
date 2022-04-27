using EuroCMS.CMSPlugin.GarantiLeasing.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.CMSPlugin.GarantiLeasing.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }

        public ActionResult LeasingForm(string startDate, string endDate)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_LeasingForm");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);

            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Ürün Cinsi");
            sheetRow.CreateCell(2).SetCellValue("Ürün Bedeli");
            sheetRow.CreateCell(3).SetCellValue("Ürünün Vadesi");
            sheetRow.CreateCell(4).SetCellValue("Döviz Cinsi");
            sheetRow.CreateCell(5).SetCellValue("Açıklama");
            sheetRow.CreateCell(6).SetCellValue("Firma Adı");
            sheetRow.CreateCell(7).SetCellValue("Ad Soyad");
            sheetRow.CreateCell(8).SetCellValue("Telefon");
            sheetRow.CreateCell(9).SetCellValue("E-posta");
            sheetRow.CreateCell(10).SetCellValue("İl");
            sheetRow.CreateCell(11).SetCellValue("İlçe");
            sheetRow.CreateCell(12).SetCellValue("Adres");
            sheetRow.CreateCell(13).SetCellValue("Kvkk");
            sheetRow.CreateCell(14).SetCellValue("Oluşturulma Tarihi");
            sheetRow.CreateCell(15).SetCellValue("Kaynak");

            sheetRowIndex++;

          
            using (GarantiLeasingDbContext context = new GarantiLeasingDbContext())
            {
                var query = context.LeasingApplications.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreateDt >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreateDt <= end);
                }

                var data = query.OrderByDescending(o => o.Id).ToList();
                foreach (var item in data)
                {
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.ProductType)));
                    sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.ProductPrice)));
                    sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Expiration.ToString())));
                    sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Currency)));
                    sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Description)));
                    sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Company)));
                    sheetRow.CreateCell(7).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Person)));
                    sheetRow.CreateCell(8).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                    sheetRow.CreateCell(9).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Mail)));
                    sheetRow.CreateCell(10).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.City)));
                    sheetRow.CreateCell(11).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Town)));
                    sheetRow.CreateCell(12).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Address)));
                    sheetRow.CreateCell(13).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.IsAccept.ToString())));
                    sheetRow.CreateCell(14).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CreateDt.ToString())));
                    sheetRow.CreateCell(15).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Source)));

                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_callme_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }

        public ActionResult CallMeForm(string startDate, string endDate)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_CallmeForm");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Ad");
            sheetRow.CreateCell(2).SetCellValue("Soyad");
            sheetRow.CreateCell(3).SetCellValue("İl");
            sheetRow.CreateCell(4).SetCellValue("İlçe");
            sheetRow.CreateCell(5).SetCellValue("Email");
            sheetRow.CreateCell(6).SetCellValue("Phone");
            sheetRow.CreateCell(7).SetCellValue("Kvkk");
            sheetRow.CreateCell(8).SetCellValue("Oluşturma Tarihi");

            sheetRowIndex++;

            using (GarantiLeasingDbContext context = new GarantiLeasingDbContext())
            {
                var query = context.CallMeForms.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreateDate >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreateDate <= end);
                }

                var data = query.OrderByDescending(o => o.Id).ToList();
                foreach (var item in data)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Name)));
                    sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Surname)));
                    sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.City)));
                    sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Town)));
                    sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                    sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                    sheetRow.CreateCell(7).SetCellValue(item.Kvkk);
                    sheetRow.CreateCell(8).SetCellValue(item.CreateDate);
                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_callme_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }

        public ActionResult ComplaintForm(string startDate, string endDate)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_ComplaintForm");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Ad");
            sheetRow.CreateCell(2).SetCellValue("Soyad");
            sheetRow.CreateCell(3).SetCellValue("Şirket");
            sheetRow.CreateCell(4).SetCellValue("Email");
            sheetRow.CreateCell(5).SetCellValue("Telefon");
            sheetRow.CreateCell(6).SetCellValue("Mesaj");
            sheetRow.CreateCell(7).SetCellValue("MarsNo-SözleşmeNo");
            sheetRow.CreateCell(8).SetCellValue("Kvkk");
            sheetRow.CreateCell(9).SetCellValue("Oluşturma Tarihi");

            sheetRowIndex++;

            using (GarantiLeasingDbContext context = new GarantiLeasingDbContext())
            {
                var query = context.Complaints.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreateDate >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreateDate <= end);
                }

                var data = query.OrderByDescending(o => o.Id).ToList();
                foreach (var item in data)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Name)));
                    sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Surname)));
                    sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CompanyName)));
                    sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                    sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                    sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Message)));
                    sheetRow.CreateCell(7).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.MarsNo)));
                    sheetRow.CreateCell(8).SetCellValue(item.Kvkk);
                    sheetRow.CreateCell(9).SetCellValue(item.CreateDate);
                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_complaint_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }

        public ActionResult ContactForm(string startDate, string endDate)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_ContactForm");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Ad-Soyad");
            sheetRow.CreateCell(2).SetCellValue("Şirket");
            sheetRow.CreateCell(3).SetCellValue("İl-İlçe");
            sheetRow.CreateCell(4).SetCellValue("Email");
            sheetRow.CreateCell(5).SetCellValue("Telefon");
            sheetRow.CreateCell(6).SetCellValue("Mesaj");
            sheetRow.CreateCell(7).SetCellValue("Kvkk");
            sheetRow.CreateCell(8).SetCellValue("Oluşturma Tarihi");

            sheetRowIndex++;

            using (GarantiLeasingDbContext context = new GarantiLeasingDbContext())
            {
                var query = context.ContactForms.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreateDate >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreateDate <= end);
                }

                var data = query.OrderByDescending(o => o.Id).ToList();

                foreach (var item in data)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.NameSurname)));
                    sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CompanyName)));
                    sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CityTown)));
                    sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                    sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                    sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Message)));
                    sheetRow.CreateCell(7).SetCellValue(item.Kvkk);
                    sheetRow.CreateCell(8).SetCellValue(item.CreateDate);
                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_contact_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }

        public ActionResult HrForm(string startDate, string endDate)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_HrForm");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Ad-Soyad");
            sheetRow.CreateCell(2).SetCellValue("Telefon");
            sheetRow.CreateCell(3).SetCellValue("Email");
            sheetRow.CreateCell(4).SetCellValue("Pozisyon");
            sheetRow.CreateCell(5).SetCellValue("Dosya Yolu");
            sheetRow.CreateCell(6).SetCellValue("Adres");
            sheetRow.CreateCell(7).SetCellValue("Kvkk");
            sheetRow.CreateCell(8).SetCellValue("Oluşturma Tarihi");

            sheetRowIndex++;

            using (GarantiLeasingDbContext context = new GarantiLeasingDbContext())
            {
                var query = context.HrForms.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreateDate >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreateDate <= end);
                }

                var data = query.OrderByDescending(o => o.Id).ToList();

                foreach (var item in data)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.NameSurname)));
                    sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                    sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                    sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Position)));
                    sheetRow.CreateCell(5).SetCellValue("https://www.garantileasing.com.tr/i/content/hr/" + HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.FileUrl)));
                    sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Address)));
                    sheetRow.CreateCell(7).SetCellValue(item.Kvkk);
                    sheetRow.CreateCell(8).SetCellValue(item.CreateDate);
                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_hr_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }

        public ActionResult Newsletter(string startDate, string endDate)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_Newsletter");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Email");
            sheetRow.CreateCell(2).SetCellValue("Kvkk");
            sheetRow.CreateCell(3).SetCellValue("Oluşturma Tarihi");

            sheetRowIndex++;

            using (GarantiLeasingDbContext context = new GarantiLeasingDbContext())
            {
                var query = context.Newsletters.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreateDate >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreateDate <= end);
                }

                var data = query.OrderByDescending(o => o.Id).ToList();

                foreach (var item in data)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                    sheetRow.CreateCell(2).SetCellValue(item.Kvkk);
                    sheetRow.CreateCell(3).SetCellValue(item.CreateDate);
                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_newsletter_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/GarantiLeasing/Report/index.cshtml");
        }
    }
}