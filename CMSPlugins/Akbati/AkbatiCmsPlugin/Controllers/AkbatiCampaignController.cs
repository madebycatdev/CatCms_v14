using EuroCMS.CMSPlugin.AKBATI.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroCMS.CMSPlugin.AKBATI.Controllers
{
    public class AkbatiCampaignController : Controller
    {
        // GET: AkbatiCampaign
        public ActionResult Index(int? id)
        {
            var list = new List<AkbatiCampaign>();
            using (AkbatiCampaignDbContext dbContext = new AkbatiCampaignDbContext())
            {
                if (id.HasValue)
                {
                    list = dbContext.AkbatiCampaigns.Where(w => w.Status == id.Value).OrderByDescending(o => o.Id).ToList();
                }
                else
                {
                    list = dbContext.AkbatiCampaigns.OrderByDescending(o => o.Id).ToList();
                }
            }
            return View("~/Views/CMSPlugins/Akbati/AkbatiCampaign/Index.cshtml", list);
        }

        public ActionResult Approve(int id)
        {
            var data = new AkbatiCampaign();
            using (AkbatiCampaignDbContext dbContext = new AkbatiCampaignDbContext())
            {
                data = dbContext.AkbatiCampaigns.FirstOrDefault(f => f.Id == id);
                data.Status = 1;
                dbContext.Entry(data).State = EntityState.Modified;
                dbContext.SaveChanges();
                TempData["Message"] = "İçerik onaylandı.";
            }
            return RedirectToAction("index", "akbaticampaign");
        }


        public ActionResult Reject(int id)
        {
            var data = new AkbatiCampaign();
            using (AkbatiCampaignDbContext dbContext = new AkbatiCampaignDbContext())
            {
                data = dbContext.AkbatiCampaigns.FirstOrDefault(f => f.Id == id);
                data.Status = 2;
                dbContext.Entry(data).State = EntityState.Modified;
                dbContext.SaveChanges();
                TempData["Message"] = "İçerik onaylanmadı.";
            }
            return RedirectToAction("index", "akbaticampaign");
        }

        public ActionResult Export(int? id)
        {
            var data = new List<AkbatiCampaign>();
            using (AkbatiCampaignDbContext dbContext = new AkbatiCampaignDbContext())
            {
                if (id.HasValue)
                {
                    data = dbContext.AkbatiCampaigns.Where(w => w.Status == id.Value).OrderByDescending(o => o.Id).ToList();
                }
                else
                {
                    data = dbContext.AkbatiCampaigns.OrderByDescending(o => o.Id).ToList();
                }
            }
            
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("rapor");
            var sheetRowIndex = 0;
            var sheetRow = sheet.CreateRow(sheetRowIndex);
            sheetRow.CreateCell(0).SetCellValue("Id");
            sheetRow.CreateCell(1).SetCellValue("Name");
            sheetRow.CreateCell(2).SetCellValue("Surname");
            sheetRow.CreateCell(3).SetCellValue("Phone");
            sheetRow.CreateCell(4).SetCellValue("Mail");
            sheetRow.CreateCell(5).SetCellValue("Town");
            sheetRow.CreateCell(6).SetCellValue("Photo");
            sheetRow.CreateCell(7).SetCellValue("CreateDt");

            sheetRowIndex++;
            foreach (var item in data)
            {
                sheetRow = sheet.CreateRow(sheetRowIndex);
                sheetRow.CreateCell(0).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Id.ToString())));
                sheetRow.CreateCell(1).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Name)));
                sheetRow.CreateCell(2).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Surname)));
                sheetRow.CreateCell(3).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Phone)));
                sheetRow.CreateCell(4).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Mail)));
                sheetRow.CreateCell(5).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Town)));
                sheetRow.CreateCell(6).SetCellValue("http://www.akbati.com/i/content/campaign/" + HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.Photo)));
                sheetRow.CreateCell(7).SetCellValue(HttpUtility.UrlDecode(HttpUtility.HtmlDecode(item.CreateDt.ToString())));
                sheetRowIndex++;
            }
            // Save the Excel spreadsheet to a MemoryStream and return it to the client
            using (var exportData = new MemoryStream())
            {

                workbook.Write(exportData);
                string saveAsFileName = string.Format("rapor_{0:d}.xls", DateTime.Now.ToString("d_M_yyyy_HH_mm_ss"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
            return RedirectToAction("index", "akbaticampaign");
        }
    }
}