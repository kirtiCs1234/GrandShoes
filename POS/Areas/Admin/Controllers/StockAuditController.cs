﻿using Model;
using OfficeOpenXml;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Helper;
using POS.Areas.Admin.Models;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.StockAudit)]
    public class StockAuditController : BaseController
    {
        // GET: Admin/StockAudit
        public ActionResult Index()
        {
            var BranchModelList = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
           
            return View();
        }
        public ActionResult _Index(int? BranchId)
        {
            var StockTapeList = Services.StockAuditService.GetByBranchId(BranchId);
            ViewBag.BranchId = BranchId;
            return View(StockTapeList);
        }
        public ActionResult DeleteRecord(int? BranchId)
        {
            var stockAuditList = Services.StockAuditService.GetByBranchId(BranchId);

            foreach (var item in stockAuditList)
            {
                var deleteRecord = Services.StockAuditService.DeleteRecord(item);
            }

            return RedirectToAction("Index", "StockTape");
        }
        public ActionResult ExcelUpload(int? BranchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(StockAuditModel stock1, FormDataModel model)
        {
			var file = model.file;
			var BranchId = model.BranchId;
			string filePath = string.Empty;
			var StockBranchModel = Services.StockBranchInventoryService.GetAll();
			if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
			{
				string path = Server.MapPath("~/File/");
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				filePath = path + Path.GetFileName(file.FileName);
				string extension = Path.GetExtension(file.FileName);
				file.SaveAs(filePath);
				string csvData = System.IO.File.ReadAllText(filePath);
				var stockAuditList = new List<StockAuditModel>();
				Dictionary<string, StockAuditModel> dict = new Dictionary<string, StockAuditModel>();

				foreach (string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						var barcodeString = row.Split(',')[0].Replace("\r", "");
						var productBarcode = barcodeString.Substring(0, 10);
						//stock.Barcode = productBarcode;

						var size = barcodeString.Substring((barcodeString.Length - 2), 2);
						var listBarcode = stockAuditList.Any(x => x.Barcode == productBarcode);
						StockAuditModel temp;
                                dict.TryGetValue(productBarcode, out temp);
                                if (temp != null)
                                {
                                    if (size == "01")
                                    {
                                        temp.Quantity01 += 1;
                                    }
                                    if (size == "02")
                                    {
                                        temp.Quantity02 += 1;
                                    }
                                    if (size == "03")
                                    {
                                        temp.Quantity03 += 1;
                                    }
                                    if (size == "04")
                                    {
                                        temp.Quantity04 += 1;
                                    }
                                    if (size == "05")
                                    {
                                        temp.Quantity05 += 1;
                                    }
                                    if (size == "06")
                                    {
                                        temp.Quantity06 += 1;
                                    }
                                    if (size == "07")
                                    {
                                        temp.Quantity07 += 1;
                                    }
                                    if (size == "08")
                                    {
                                        temp.Quantity08 += 1;
                                    }
                                    if (size == "09")
                                    {
                                        temp.Quantity09 += 1;
                                    }
                                    if (size == "10")
                                    {
                                        temp.Quantity10 += 1;
                                    }
                                    if (size == "11")
                                    {
                                        temp.Quantity11 += 1;
                                    }
                                    if (size == "12")
                                    {
                                        temp.Quantity12 += 1;
                                    }
                                    if (size == "13")
                                    {
                                        temp.Quantity13 += 1;
                                    }
                                    if (size == "14")
                                    {
                                        temp.Quantity14 += 1;
                                    }
                                    if (size == "15")
                                    {
                                        temp.Quantity15 += 1;
                                    }
                                    if (size == "16")
                                    {
                                        temp.Quantity16 += 1;
                                    }
                                    if (size == "17")
                                    {
                                        temp.Quantity17 += 1;
                                    }
                                    if (size == "18")
                                    {
                                        temp.Quantity18 += 1;
                                    }
                                    if (size == "19")
                                    {
                                        temp.Quantity19 += 1;
                                    }
                                    if (size == "20")
                                    {
                                        temp.Quantity20 += 1;
                                    }
                                    if (size == "21")
                                    {
                                        temp.Quantity21 += 1;
                                    }
                                    if (size == "22")
                                    {
                                        temp.Quantity22 += 1;
                                    }
                                    if (size == "23")
                                    {
                                        temp.Quantity23 += 1;
                                    }
                                    if (size == "24")
                                    {
                                        temp.Quantity24 += 1;
                                    }
                                    if (size == "25")
                                    {
                                        temp.Quantity25 += 1;
                                    }
                                    if (size == "26")
                                    {
                                        temp.Quantity26 += 1;
                                    }
                                    if (size == "27")
                                    {
                                        temp.Quantity27 += 1;
                                    }
                                    if (size == "28")
                                    {
                                        temp.Quantity28 += 1;
                                    }
                                    if (size == "29")
                                    {
                                        temp.Quantity29 += 1;
                                    }
                                    if (size == "30")
                                    {
                                        temp.Quantity30 += 1;
                                    }

                                }
                                else
                                {
                                    temp = new StockAuditModel();
                                    temp.Quantity01 = 0;
                                    temp.Quantity02 = 0;
                                    temp.Quantity03 = 0;
                                    temp.Quantity04 = 0;
                                    temp.Quantity05 = 0;
                                    temp.Quantity06 = 0;
                                    temp.Quantity07 = 0;
                                    temp.Quantity08 = 0;
                                    temp.Quantity09 = 0;
                                    temp.Quantity10 = 0;
                                    temp.Quantity11 = 0;
                                    temp.Quantity12 = 0;
                                    temp.Quantity13 = 0;
                                    temp.Quantity14 = 0;
                                    temp.Quantity15 = 0;
                                    temp.Quantity16 = 0;
                                    temp.Quantity17 = 0;
                                    temp.Quantity18 = 0;
                                    temp.Quantity19 = 0;
                                    temp.Quantity20 = 0;
                                    temp.Quantity21 = 0;
                                    temp.Quantity22 = 0;
                                    temp.Quantity23 = 0;
                                    temp.Quantity24 = 0;
                                    temp.Quantity25 = 0;
                                    temp.Quantity26 = 0;
                                    temp.Quantity27 = 0;
                                    temp.Quantity28 = 0;
                                    temp.Quantity29 = 0;
                                    temp.Quantity30 = 0;
                                    temp.Barcode = productBarcode;
                                    temp.BranchId = BranchId;
                                    temp.Date = DateTime.Now;
                                    temp.LogId = 1;
                                    temp.IsActive = true;
                                    temp.ProductId = StockBranchModel.Where(x => x.Barcode == productBarcode).Select(x => x.ProductId).FirstOrDefault();
                                    if (size == "01")
                                    {
                                        temp.Quantity01 += 1;
                                    }
                                    if (size == "02")
                                    {
                                        temp.Quantity02 += 1;
                                    }
                                    if (size == "03")
                                    {
                                        temp.Quantity03 += 1;
                                    }
                                    if (size == "04")
                                    {
                                        temp.Quantity04 += 1;
                                    }
                                    if (size == "05")
                                    {
                                        temp.Quantity05 += 1;
                                    }
                                    if (size == "06")
                                    {
                                        temp.Quantity06 += 1;
                                    }
                                    if (size == "07")
                                    {
                                        temp.Quantity07 += 1;
                                    }
                                    if (size == "08")
                                    {
                                        temp.Quantity08 += 1;
                                    }
                                    if (size == "09")
                                    {
                                        temp.Quantity09 += 1;
                                    }
                                    if (size == "10")
                                    {
                                        temp.Quantity10 += 1;
                                    }
                                    if (size == "11")
                                    {
                                        temp.Quantity11 += 1;
                                    }
                                    if (size == "12")
                                    {
                                        temp.Quantity12 += 1;
                                    }
                                    if (size == "13")
                                    {
                                        temp.Quantity13 += 1;
                                    }
                                    if (size == "14")
                                    {
                                        temp.Quantity14 += 1;
                                    }
                                    if (size == "15")
                                    {
                                        temp.Quantity15 += 1;
                                    }
                                    if (size == "16")
                                    {
                                        temp.Quantity16 += 1;
                                    }
                                    if (size == "17")
                                    {
                                        temp.Quantity17 += 1;
                                    }
                                    if (size == "18")
                                    {
                                        temp.Quantity18 += 1;
                                    }
                                    if (size == "19")
                                    {
                                        temp.Quantity19 += 1;
                                    }
                                    if (size == "20")
                                    {
                                        temp.Quantity20 += 1;
                                    }
                                    if (size == "21")
                                    {
                                        temp.Quantity21 += 1;
                                    }
                                    if (size == "22")
                                    {
                                        temp.Quantity22 += 1;
                                    }
                                    if (size == "23")
                                    {
                                        temp.Quantity23 += 1;
                                    }
                                    if (size == "24")
                                    {
                                        temp.Quantity24 += 1;
                                    }
                                    if (size == "25")
                                    {
                                        temp.Quantity25 += 1;
                                    }
                                    if (size == "26")
                                    {
                                        temp.Quantity26 += 1;
                                    }
                                    if (size == "27")
                                    {
                                        temp.Quantity27 += 1;
                                    }
                                    if (size == "28")
                                    {
                                        temp.Quantity28 += 1;
                                    }
                                    if (size == "29")
                                    {
                                        temp.Quantity29 += 1;
                                    }
                                    if (size == "30")
                                    {
                                        temp.Quantity30 += 1;
                                    }
                                    dict.Add(productBarcode, temp);
                                }
                            }
                            var stockList = dict.Select(x => x.Value).ToList();
                            foreach (var l in stockList)
                            {
                                var stockAudit = Services.StockAuditService.Create(l);
                            }
                        }
                        TempData["Success"] = "File Uploaded Successfully!";
                        return RedirectToAction("Index","StockAudit");
                    }
            return View(stock1);
        }
        public ActionResult ShowVariance(int? BranchId)
        {
            var ShowVarianceList = Services.StockAuditService.ShowVariance(BranchId);
            return View(ShowVarianceList);

        }

    }
}