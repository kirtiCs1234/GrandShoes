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
    [CustomAuth(PageSession.Branch)]
    public class BranchController : BaseController
    {
        ServiceClass sc = new ServiceClass();
        // GET: Admin/Branch
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
            var BranchModelList = Services.BranchService.GetPaging(page, out TotalCount);
			foreach(var date1 in BranchModelList)
			{
				if (date1.DateOpen != null && date1.DateClosed != null)
				{
					var DateOpen = date1.DateOpen.Substring(0, date1.DateOpen.Length - 9);
					date1.DateOpen = DateOpen;
					var DateClosed = date1.DateClosed.Substring(0, date1.DateClosed.Length - 9);
					date1.DateClosed = DateClosed;
				}
			}
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(BranchModelList);

        }
       
        public ActionResult _Index1(BranchSearch branchSearch,int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
           
            
             var BranchModelList = Services.BranchService.GetSearchData(branchSearch, page, out TotalCount);
			foreach (var date1 in BranchModelList)
			{
				if (date1.DateOpen != null && date1.DateClosed != null)
				{
					var DateOpen = date1.DateOpen.Substring(0, date1.DateOpen.Length - 9);
					date1.DateOpen = DateOpen;
					var DateClosed = date1.DateClosed.Substring(0, date1.DateClosed.Length - 9);
					date1.DateClosed = DateClosed;
				}
			}
			ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(BranchModelList);

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            BranchModel BranchModelById = Services.BranchService.GetById(id);
			BranchModelById.DateClosed = BranchModelById.DateClosed.Substring(0, BranchModelById.DateClosed.Length - 9);
			BranchModelById.DateOpen = BranchModelById.DateOpen.Substring(0, BranchModelById.DateOpen.Length - 9);
            return View(BranchModelById);
        }
        public ActionResult ExcelUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(BranchModel branch, HttpPostedFileBase ExcelFile)
        {
            Dictionary<string, string> process = new Dictionary<string, string>();
            Dictionary<int, BranchModel> branches = new Dictionary<int, BranchModel>();
            Dictionary<int, BranchModel> updatebranches = new Dictionary<int, BranchModel>();
            List<BranchModel> branches1 = new List<BranchModel>();
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
							var model = new BranchModel();
							model.BranchCode = row.Split(',')[0];
							model.Name = row.Split(',')[1];
							model.Telephone = row.Split(',')[2];
							model.DateOpen = row.Split(',')[3];
							model.DateClosed =row.Split(',')[4];
							model.AddressLine1 = row.Split(',')[5];
							model.AddressLine2 = row.Split(',')[6];
							model.AddressLine3 = row.Split(',')[7];
							model.PostalCode = row.Split(',')[8];
							model.AreaCode = row.Split(',')[9];
							model.IsSendStock = Convert.ToBoolean(row.Split(',')[10]);
							model.IsClosed = Convert.ToBoolean(row.Split(',')[11]);
							model.IsHeadOffice = Convert.ToBoolean(row.Split(',')[12]);
							model.StoreSize = row.Split(',')[13];
							model.IsActive = true;
							string chk = row.Split(',')[0];
							bool isexists = Services.BranchService.CheckBranchCode(chk);
							if (!isexists)
							{
								branches.Add(i,model);
                                process[i + "#" + row.Split(',')[0]] = "Add";
                            }
							else
							{
                                updatebranches.Add(i, model);
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
			var addList = Services.BranchService.CreateList(branches);
			var updateList = Services.BranchService.UpdateList(updatebranches);
            var dictionaryFrom = new Dictionary<string, string>();

            dictionaryFrom = sc.getFilterData(addList, updateList, process);
            TempData["ProcessData"] = dictionaryFrom;

            TempData["Success"] = "Data Uploaded Successfully!";
			return RedirectToAction("Index", "Branch");
		}

        public ActionResult ExportList()
        {
            var data = Services.BranchService.GetAll();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 2].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;  filename=BranchExport.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                //Response.End();
            }
            return View();
        }
        public ActionResult Create()
        {
            List<UserModel> UserModelList = Services.UserService.GetAll();
            ViewBag.ManagerId = new SelectList(UserModelList, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(BranchModel branch)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

                branch.IsActive = true;
                 bool BranchCreate = Services.BranchService.Create(branch);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Branch");
            }
            return View(branch);

        }
        public ActionResult CheckBranchCode(BranchModel branch)
        {
            var iExist = Services.BranchService.CheckBranchCode(branch);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckBranchName(BranchModel branch)
        {
            var iExist = Services.BranchService.CheckBranchName(branch);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            BranchModel BranchModelById = Services.BranchService.GetById(id);
			BranchModelById.DateClosed = BranchModelById.DateClosed.Substring(0, BranchModelById.DateClosed.Length - 9);
			BranchModelById.DateOpen = BranchModelById.DateOpen.Substring(0, BranchModelById.DateOpen.Length - 9);
			return View(BranchModelById);
        }

        [HttpPost]
        public ActionResult Edit(BranchModel branch)
        {
            if (ModelState.IsValid)
            {

                bool BranchEdit = Services.BranchService.Edit(branch);
                TempData["Success"] = "Data Saved Successfully!";
                return RedirectToAction("Index", "Branch");
            }
            return View(branch);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchModel BranchModelById = Services.BranchService.GetById(id);

            if (BranchModelById == null)
            {
                return HttpNotFound();
            }
            return View(BranchModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(BranchModel branch)
        {
            if (branch.Id > 0)
            {
                BranchModel BranchDelete = Services.BranchService.Delete(branch);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "Branch");
            }
            return View(branch);
        }
    
}
}