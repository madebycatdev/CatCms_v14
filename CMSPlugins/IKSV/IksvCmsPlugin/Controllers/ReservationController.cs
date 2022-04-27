using EuroCMS.CMSPlugin.IKSV.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.CMSPlugin.IKSV.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            return View("~/Views/CMSPlugins/IKSV/Reservation/index.cshtml");
        }

      
        public ActionResult excel()
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor_HrForm");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Ad");
            sheetRow.CreateCell(2).SetCellValue("Soyad");
            sheetRow.CreateCell(3).SetCellValue("Email");
            sheetRow.CreateCell(4).SetCellValue("Telefon");
            sheetRow.CreateCell(5).SetCellValue("Etkinlik");
            sheetRow.CreateCell(6).SetCellValue("EtkinlikId");
            sheetRow.CreateCell(7).SetCellValue("Kvkk");
            sheetRow.CreateCell(8).SetCellValue("Koşul");
            sheetRow.CreateCell(9).SetCellValue("Oluşturma Tarihi");

            sheetRowIndex++;

            using (IksvDbContext context = new IksvDbContext())
            {
                var data = context.Reservations.OrderByDescending(o => o.Id).ToList();
                foreach (var item in data)
                {
                    sheetRow = sheet.CreateRow(sheetRowIndex);
                    sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                    sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Name)));
                    sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Surname)));
                    sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Email)));
                    sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                    sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Event)));
                    sheetRow.CreateCell(6).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.EventId.ToString())));
                    sheetRow.CreateCell(7).SetCellValue(item.Kvkk);
                    sheetRow.CreateCell(8).SetCellValue(item.IsAccept);
                    sheetRow.CreateCell(9).SetCellValue(item.CreateDt.ToString());
                    sheetRowIndex++;
                }
            }

            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {
                workbook.Write(exportData);
                string saveAsFileName = string.Format("rezervasyon_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }

            return View("~/Views/CMSPlugins/IKSV/Reservation/index.cshtml");
        }
    }
}