﻿using Helper;
using Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.SizeGrid)]
    public class SizeGridController : BaseController
    {
        ServiceClass sc = new ServiceClass();
        // GET: Admin/SizeGrid
        public ActionResult Index(int? page)
        {
            var pData = TempData["ProcessData"];
            if (pData != null)
            {
                ViewBag.processData = pData;
            }
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;

			var mainDict = new Dictionary<string, Dictionary<string, string>>();
			ViewBag.PageSize = pageSize;
			var SizeGridModelList = Services.SizeGridService.GetPaging(page, out TotalCount);
			foreach (var item in SizeGridModelList)
			{
				var dict = new Dictionary<string, string>();
				var keyval = Utility.getKeyVaue(item).Where(x => x.Key.Contains("Z")).ToList();
				foreach (var item2 in keyval)
				{
					if (!string.IsNullOrEmpty(item2.Value))
					{
						if (item2.Value.Contains(".0"))
						{
							//item2[item2.Key] = 	item2.Value.Replace(".0", "");
							dict.Add(item2.Key, item2.Value.Replace(".0", ""));
						}
						else
						{
							dict.Add(item2.Key, item2.Value);
						}
					}

				}
				mainDict.Add(item.Id + "#" + item.GridNumber, dict);
			}
			ViewBag.TotalCount = TotalCount;

            var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(mainDict); ;

        }
        public ActionResult _Index1(SizeGridSearch sizeGridSearch,int? page)

        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;

			var mainDict = new Dictionary<string, Dictionary<string, string>>();
			ViewBag.PageSize = pageSize;
			var SizeGridModelList = Services.SizeGridService.GetPaging(page, out TotalCount);
			foreach (var item in SizeGridModelList)
			{
				var dict = new Dictionary<string, string>();
				var keyval = Utility.getKeyVaue(item).Where(x => x.Key.Contains("Z")).ToList();
				foreach (var item2 in keyval)
				{
					if (!string.IsNullOrEmpty(item2.Value))
					{
						if (item2.Value.Contains(".0"))
						{
							//item2[item2.Key] = 	item2.Value.Replace(".0", "");
							dict.Add(item2.Key, item2.Value.Replace(".0", ""));
						}
						else
						{
							dict.Add(item2.Key, item2.Value);
						}
					}

				}
				mainDict.Add(item.Id + "#" + item.GridNumber, dict);
			}
			ViewBag.TotalCount = TotalCount;

            ViewBag.TotalCount = TotalCount;
            var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(mainDict);

        }

        public ActionResult ExcelUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(SizeGridModel sizeGrid, HttpPostedFileBase ExcelFile)
        {
            Dictionary<string, string> process = new Dictionary<string, string>();
            Dictionary<int, SizeGridModel> sizeGrids = new Dictionary<int, SizeGridModel>();
            Dictionary<int, SizeGridModel> updatesizeGrids = new Dictionary<int, SizeGridModel>();

            //List<SizeGridModel> sizeGrids = new List<SizeGridModel>();
			string filePath = string.Empty;
			if (ExcelFile != null)
			{
				string path = Server.MapPath("~/File/");
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				filePath = path + Path.GetFileName(ExcelFile.FileName);
				string extension = Path.GetExtension(ExcelFile.FileName);
				ExcelFile.SaveAs(filePath);
				string csvData = System.IO.File.ReadAllText(filePath);
				var csv = csvData.Split('\n').Length;
                int i = 0;
				foreach (string row in csvData.Split('\n'))
				{
					try
					{
						if (!string.IsNullOrEmpty(row) && row.Split(',')[0].All(char.IsDigit) && !row.StartsWith(",,,,,"))
						{
                            i++;
							var model = new SizeGridModel();
							model.GridNumber = row.Split(',')[0];
							model.Z01 = Convert.ToInt32(row.Split(',')[1]);
							model.Z02 = Convert.ToInt32(row.Split(',')[2]);
							model.Z03 = Convert.ToInt32(row.Split(',')[3]);
							model.Z04 = Convert.ToInt32(row.Split(',')[4]);
							model.Z05 = Convert.ToInt32(row.Split(',')[5]);
							model.Z06 = Convert.ToInt32(row.Split(',')[6]);
							model.Z07 = Convert.ToInt32(row.Split(',')[7]);
							model.Z08 = Convert.ToInt32(row.Split(',')[8]);
							model.Z09 = Convert.ToInt32(row.Split(',')[9]);
							model.Z10 = Convert.ToInt32(row.Split(',')[10]);
							model.Z11 = Convert.ToInt32(row.Split(',')[11]);
							model.Z12 = Convert.ToInt32(row.Split(',')[12]);
							model.Z13 = Convert.ToInt32(row.Split(',')[13]);
							model.Z14 = Convert.ToInt32(row.Split(',')[14]);
							model.Z15 = Convert.ToInt32(row.Split(',')[15]);
							model.Z16 = Convert.ToInt32(row.Split(',')[16]);
							model.Z17 = Convert.ToInt32(row.Split(',')[17]);
							model.Z18 = Convert.ToInt32(row.Split(',')[18]);
							model.Z19 = Convert.ToInt32(row.Split(',')[19]);
							model.Z20 = Convert.ToInt32(row.Split(',')[20]);
							model.Z21 = Convert.ToInt32(row.Split(',')[21]);
							model.Z22 = Convert.ToInt32(row.Split(',')[22]);
							model.Z23 = Convert.ToInt32(row.Split(',')[23]);
							model.Z24 = Convert.ToInt32(row.Split(',')[24]);
							model.Z25 = Convert.ToInt32(row.Split(',')[25]);
							model.Z26 = Convert.ToInt32(row.Split(',')[26]);
							model.Z27 = Convert.ToInt32(row.Split(',')[27]);
							model.Z28 = Convert.ToInt32(row.Split(',')[28]);
							model.Z29 = Convert.ToInt32(row.Split(',')[29]);
							model.Z30 = Convert.ToInt32(row.Split(',')[30]);
							model.IsActive = true;
							string chk = row.Split(',')[0];
							bool isexists = Services.SizeGridService.CheckGridNo1(chk);
							if (!isexists)
							{
								sizeGrids.Add(i,model);
                                process[i + "#" + row.Split(',')[0]] = "Add";
                            }
							else
							{
                                updatesizeGrids.Add(i, model);
                                process[i + "#" + row.Split(',')[0]] = "Update";
                            }

						}
					}
					catch (Exception ex)
					{
                        //error loging stuff
                        if (ex.Message != null)
                        {
                            process[i + "#" + row.Split(',')[0]] = ex.Message;
                        }
                    }
				}
			}
			var addList = Services.SizeGridService.CreateList(sizeGrids);
			var updateList = Services.SizeGridService.UpdateList(updatesizeGrids);
            var dictionaryFrom = new Dictionary<string, string>();

            dictionaryFrom = sc.getFilterData(addList, updateList, process);
            TempData["ProcessData"] = dictionaryFrom;

            TempData["Success"] = "Data Uploaded Successfully!";
			return RedirectToAction("Index", "SizeGrid");
		}

        public ActionResult ExportList()
        {
            var data = Services.SizeGridService.GetAll();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;  filename=SizeGrid.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                //Response.End();
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGridModel SizeGridModelById = Services.SizeGridService.GetById(id);
            var dict = new Dictionary<string, string>();
            var keyvalue = Utility.getKeyVaue(SizeGridModelById).Where(x => x.Key.Contains("Z")).ToList();
            foreach (var item2 in keyvalue)
            {
                if (!string.IsNullOrEmpty(item2.Value))
                {
                    if (item2.Value.Contains(".0"))
                    {
                        //item2[item2.Key] = 	item2.Value.Replace(".0", "");
                        dict.Add(item2.Key, item2.Value.Replace(".0", ""));
                    }
                    else
                    {
                        dict.Add(item2.Key, item2.Value);
                    }
                }

            }
            ViewData["Sizes"] = dict;
            return View(SizeGridModelById);
        }
        public ActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(SizeGridModel sizeGrid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                // user.IsActive = true;
                bool SizeGridCreate = Services.SizeGridService.Create(sizeGrid);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "SizeGrid");
            }
            return View(sizeGrid);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGridModel SizeGridModelById = Services.SizeGridService.GetById(id);
            var dict = new Dictionary<string, string>();
            var keyvalue = Utility.getKeyVaue(SizeGridModelById).Where(x=>x.Key.Contains("Z")).ToList();
            foreach (var item2 in keyvalue)
            {
                if (!string.IsNullOrEmpty(item2.Value))
                {
                    if (item2.Value.Contains(".0"))
                    {
                        //item2[item2.Key] = 	item2.Value.Replace(".0", "");
                        dict.Add(item2.Key, item2.Value.Replace(".0", ""));
                    }
                    else
                    {
                        dict.Add(item2.Key, item2.Value);
                    }
                }

            }
            ViewData["Sizes"] = dict;
            return View(SizeGridModelById);
        }

        [HttpPost]
        public ActionResult Edit(SizeGridModel sizeGrid)
        {
            if (ModelState.IsValid)
            {
                bool SizeGridEdit = Services.SizeGridService.Edit(sizeGrid);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "SizeGrid");
            }
            return View(sizeGrid);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGridModel SizeGridModelById = Services.SizeGridService.GetById(id);

            if  (SizeGridModelById == null)
            {
                return HttpNotFound();
            }
            return View(SizeGridModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(SizeGridModel sizeGrid)
        {
            if (sizeGrid.Id > 0)
            {
                SizeGridModel SizeGridDelete = Services.SizeGridService.Delete(sizeGrid);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "SizeGrid");
            }
            return View(sizeGrid);
        }
        public JsonResult CheckGridNo(SizeGridModel model)
        {
            var iExist = Services.SizeGridService.CheckGridNo(model);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
    }
}