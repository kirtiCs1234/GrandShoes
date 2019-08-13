﻿using Helper;
using Model;
using OfficeOpenXml;
using POS.Areas.Admin.Models;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
	[CustomAuth(PageSession.StockTaKe)]
	public class StockTapeController : BaseController
	{
		// GET: Admin/StockTape
		public ActionResult Index()
		{
			var BranchModelList = Services.BranchService.GetAll();
			ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
			return View();
		}
		public ActionResult _Index(int? BranchId)
		{
			var StockTapeList = Services.StockTapeService.GetByBranchId(BranchId);
			ViewBag.BranchId = BranchId;
			return View(StockTapeList);
		}
		public JsonResult DeleteRecord(int? BranchId)
		{
			var stockTapeList = Services.StockTapeService.GetByBranchId(BranchId);
			bool deleteRecord = Services.StockTapeService.DeleteRecord(stockTapeList);
			return Json(deleteRecord, JsonRequestBehavior.AllowGet);
		}
		public ActionResult ExcelUpload(int? BranchId)
		{
			return View();
		}
		[HttpPost]
		public ActionResult ExcelUpload(StockTapeModel stock1, FormDataModel model)
		{
			// var BranchId1 = ConvertToInt(file["BranchId"]);
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
				var stockTapeList = new List<StockTapeModel>();
				Dictionary<string, StockTapeModel> dict = new Dictionary<string, StockTapeModel>();

				foreach (string row in csvData.Split('\n'))
				{
					if (!string.IsNullOrEmpty(row))
					{
						var barcodeString = row.Split(',')[0].Replace("\r", "");
						var productBarcode = barcodeString.Substring(0, 10);
						//stock.Barcode = productBarcode;

						var size = barcodeString.Substring((barcodeString.Length - 2), 2);
						var listBarcode = stockTapeList.Any(x => x.Barcode == productBarcode);
						StockTapeModel temp;
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
							temp = new StockTapeModel();
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
							temp.LogId = 2;
							temp.IsActive = true;
							temp.ProductId = StockBranchModel.Where(x => x.Barcode == temp.Barcode).Select(x => x.ProductId).FirstOrDefault();
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
					}

					var stockList = dict.Select(x => x.Value).ToList();
					foreach (var l in stockList)
					{
						bool stockTape = Services.StockTapeService.Create(l);
					}
					TempData["Success"] = "File Uploaded Successfully!";
					return RedirectToAction("Index", "StockTape");
				
			}
		
            return View(stock1);
	}
        public ActionResult ShowVariance(int? BranchId)
        {
            var ShowVarianceList = Services.StockTapeService.ShowVariance(BranchId);
			ViewBag.BranchId = BranchId;
			return View(ShowVarianceList);

        }
      
        public JsonResult Update(int? BranchId,StockBranchInventoryModel stock)
        {
            var updateList = new List<StockBranchInventoryModel>();
            var ShowVarianceList = Services.StockTapeService.ShowVariance(BranchId);
            var StockBranchInventoryList = Services.StockBranchInventoryService.GetByBranch(BranchId);
            foreach(var a in StockBranchInventoryList)
            {
                var stockBranch = new StockBranchInventoryModel();
                stockBranch.ProductId = a.ProductId;
                stockBranch.BranchId = a.BranchId;
                stockBranch.IsActive = a.IsActive;
                stockBranch.Quantity01 = a.Quantity01;
                stockBranch.Quantity02 = a.Quantity02;
                stockBranch.Quantity03 = a.Quantity03;
                stockBranch.Quantity04 = a.Quantity04;
                stockBranch.Quantity05 = a.Quantity05;
                stockBranch.Quantity06 = a.Quantity06;
                stockBranch.Quantity07 = a.Quantity07;
                stockBranch.Quantity08 = a.Quantity08;
                stockBranch.Quantity09 = a.Quantity09;
                stockBranch.Quantity10 = a.Quantity10;
                stockBranch.Quantity11 = a.Quantity11;
                stockBranch.Quantity12 = a.Quantity12;
                stockBranch.Quantity13 = a.Quantity13;
                stockBranch.Quantity14 = a.Quantity14;
                stockBranch.Quantity15 = a.Quantity15;
                stockBranch.Quantity16 = a.Quantity16;
                stockBranch.Quantity17 = a.Quantity17;
                stockBranch.Quantity18 = a.Quantity18;
                stockBranch.Quantity19 = a.Quantity19;
                stockBranch.Quantity20 = a.Quantity20;
                stockBranch.Quantity21 = a.Quantity21;
                stockBranch.Quantity22 = a.Quantity22;
                stockBranch.Quantity23 = a.Quantity23;
                stockBranch.Quantity24 = a.Quantity24;
                stockBranch.Quantity25 = a.Quantity25;
                stockBranch.Quantity26 = a.Quantity26;
                stockBranch.Quantity27 = a.Quantity27;
                stockBranch.Quantity28 = a.Quantity28;
                stockBranch.Quantity29 = a.Quantity29;
                stockBranch.Quantity30 = a.Quantity30;
                stockBranch.Id = a.Id;
                foreach(var b in ShowVarianceList)
                {
                    if (b.ProductId == stockBranch.ProductId )
                    {
                        stockBranch.Quantity01 = stockBranch.Quantity01 + b.Quantity01;
                        stockBranch.Quantity02 = stockBranch.Quantity02 + b.Quantity02;
                        stockBranch.Quantity03 = stockBranch.Quantity03 + b.Quantity03;
                        stockBranch.Quantity04 = stockBranch.Quantity04 + b.Quantity04;
                        stockBranch.Quantity05 = stockBranch.Quantity05 + b.Quantity05;
                        stockBranch.Quantity06 = stockBranch.Quantity06 + b.Quantity06;
                        stockBranch.Quantity07 = stockBranch.Quantity07 + b.Quantity07;
                        stockBranch.Quantity08 = stockBranch.Quantity08 + b.Quantity08;
                        stockBranch.Quantity09 = stockBranch.Quantity09 + b.Quantity09;
                        stockBranch.Quantity10 = stockBranch.Quantity10 + b.Quantity10;
                        stockBranch.Quantity11 = stockBranch.Quantity11 + b.Quantity11;
                        stockBranch.Quantity12 = stockBranch.Quantity12 + b.Quantity12;
                        stockBranch.Quantity13 = stockBranch.Quantity13 + b.Quantity13;
                        stockBranch.Quantity14 = stockBranch.Quantity14 + b.Quantity14;
                        stockBranch.Quantity15 = stockBranch.Quantity15 + b.Quantity15;
                        stockBranch.Quantity16 = stockBranch.Quantity16 + b.Quantity16;
                        stockBranch.Quantity17 = stockBranch.Quantity17 + b.Quantity17;
                        stockBranch.Quantity18 = stockBranch.Quantity18 + b.Quantity18;
                        stockBranch.Quantity19 = stockBranch.Quantity19 + b.Quantity19;
                        stockBranch.Quantity20 = stockBranch.Quantity20 + b.Quantity20;
                        stockBranch.Quantity21 = stockBranch.Quantity21 + b.Quantity21;
                        stockBranch.Quantity22 = stockBranch.Quantity22 + b.Quantity22;
                        stockBranch.Quantity23 = stockBranch.Quantity23 + b.Quantity23;
                        stockBranch.Quantity24 = stockBranch.Quantity24 + b.Quantity24;
                        stockBranch.Quantity25 = stockBranch.Quantity25 + b.Quantity25;
                        stockBranch.Quantity26 = stockBranch.Quantity26 + b.Quantity26;
                        stockBranch.Quantity27 = stockBranch.Quantity27 + b.Quantity27;
                        stockBranch.Quantity28 = stockBranch.Quantity28 + b.Quantity28;
                        stockBranch.Quantity29 = stockBranch.Quantity29 + b.Quantity29;
                        stockBranch.Quantity30 = stockBranch.Quantity30 + b.Quantity30;
                       
                    }
                }
                updateList.Add(stockBranch);
            }
           
                bool status = Services.StockBranchInventoryService.Update(BranchId, updateList);
          
            return Json(status,JsonRequestBehavior.AllowGet);
        }
    }
}