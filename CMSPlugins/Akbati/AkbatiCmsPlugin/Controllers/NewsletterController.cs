using EuroCMS.Model;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.CMSPlugin.AKBATI.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: NewsletterEmail
        public ActionResult Index()
        {
            var list = new List<NewsletterEmail>();
            using (CmsDbContext dbContext = new CmsDbContext())
            {
                list = dbContext.NewsletterEmails.OrderByDescending(o => o.Id).ToList();
            }
            return View("~/Views/CMSPlugins/Akbati/Newsletter/Index.cshtml", list);
        }


        public ActionResult Export(string startDate, string endDate)
        {

            var data = new List<NewsletterEmail>();
            using (CmsDbContext dbContext = new CmsDbContext())
            {
                var query = dbContext.NewsletterEmails.AsQueryable();
                if (!string.IsNullOrEmpty(startDate))
                {
                    DateTime start = new DateTime();
                    DateTime.TryParse(startDate, out start);
                    query = query.Where(x => x.CreatedDate >= start);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime end = new DateTime();
                    DateTime.TryParse(endDate, out end);
                    query = query.Where(x => x.CreatedDate <= end);
                }

                data = query.OrderByDescending(o => o.Id).ToList();
            }

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Email");
            sheetRow.CreateCell(2).SetCellValue("MembershipPermission");
            sheetRow.CreateCell(3).SetCellValue("eBulletinPermission");
            sheetRow.CreateCell(4).SetCellValue("CreatedDate");
            sheetRow.CreateCell(5).SetCellValue("IpAddress");


            sheetRowIndex++;
            foreach (var item in data)
            {
                sheetRow = sheet.CreateRow(sheetRowIndex);
                sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                sheetRow.CreateCell(2).SetCellValue(item.MembershipPermission);
                sheetRow.CreateCell(3).SetCellValue(item.eBulletinPermission);
                sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CreatedDate.ToString())));
                sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.IpAddress)));

                sheetRowIndex++;
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
            return RedirectToAction("index", "Newsletter");
        }
    }
}