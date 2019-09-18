using Helper;
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
            Dictionary<int,string> GridNumber = new Dictionary<int, string>();
            Dictionary<string, string> process = new Dictionary<string, string>();
            Dictionary<int, SizeGridModel> sizeGrids = new Dictionary<int, SizeGridModel>();
            Dictionary<int, SizeGridModel> TempsizeGrids = new Dictionary<int, SizeGridModel>();
            Dictionary<int, SizeGridModel> updatesizeGrids = new Dictionary<int, SizeGridModel>();
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
                            var rowSplit = row.Split(',');
							model.GridNumber = rowSplit[0].ToString();
                            if (model.GridNumber.Length < 2)
                            {
                                var s = model.GridNumber.Length;
                                while (s < 2)
                                {
                                    model.GridNumber = "0" + model.GridNumber;
                                    s++;
                                }
                            }
                            model.Z01 = Convert.ToInt32(rowSplit[1].ToString());
							model.Z02 = Convert.ToInt32(rowSplit[2].ToString());
							model.Z03 = Convert.ToInt32(rowSplit[3].ToString());
							model.Z04 = Convert.ToInt32(rowSplit[4].ToString());
							model.Z05 = Convert.ToInt32(rowSplit[5].ToString());
							model.Z06 = Convert.ToInt32(rowSplit[6].ToString());
							model.Z07 = Convert.ToInt32(rowSplit[7].ToString());
							model.Z08 = Convert.ToInt32(rowSplit[8].ToString());
							model.Z09 = Convert.ToInt32(rowSplit[9].ToString());
							model.Z10 = Convert.ToInt32(rowSplit[10].ToString());
							model.Z11 = Convert.ToInt32(rowSplit[11].ToString());
							model.Z12 = Convert.ToInt32(rowSplit[12].ToString());
							model.Z13 = Convert.ToInt32(rowSplit[13].ToString());
							model.Z14 = Convert.ToInt32(rowSplit[14].ToString());
							model.Z15 = Convert.ToInt32(rowSplit[15].ToString());
							model.Z16 = Convert.ToInt32(rowSplit[16].ToString());
							model.Z17 = Convert.ToInt32(rowSplit[17].ToString());
							model.Z18 = Convert.ToInt32(rowSplit[18].ToString());
							model.Z19 = Convert.ToInt32(rowSplit[19].ToString());
							model.Z20 = Convert.ToInt32(rowSplit[20].ToString());
							model.Z21 = Convert.ToInt32(rowSplit[21].ToString());
							model.Z22 = Convert.ToInt32(rowSplit[22].ToString());
							model.Z23 = Convert.ToInt32(rowSplit[23].ToString());
							model.Z24 = Convert.ToInt32(rowSplit[24].ToString());
							model.Z25 = Convert.ToInt32(rowSplit[25].ToString());
							model.Z26 = Convert.ToInt32(rowSplit[26].ToString());
							model.Z27 = Convert.ToInt32(rowSplit[27].ToString());
							model.Z28 = Convert.ToInt32(rowSplit[28].ToString());
							model.Z29 = Convert.ToInt32(rowSplit[29].ToString());
							model.Z30 = Convert.ToInt32(rowSplit[30].ToString());
							model.IsActive = true;
                            TempsizeGrids.Add(i, model);
                            GridNumber.Add(i, model.GridNumber);
                        }
					}
                    catch (Exception ex)
                    {
                        if (ex.Message != null)
                        {
                            process[i + "#" + row.Split(',')[0]] = ex.Message;
                        }
                        else
                        {
                            process[i + "#" + row.Split(',')[0]] = "Error Adding the row";
                        }
                    }
                }
			}
            var isexists = Services.SizeGridService.CheckGridNumber(GridNumber);
            if (TempsizeGrids != null && TempsizeGrids.Count > 0)
            {
                foreach (var item in TempsizeGrids)
                {
                    if (isexists[item.Key] == false)
                    {
                        sizeGrids.Add(item.Key, item.Value);
                        process[item.Key + "#" + item.Value.GridNumber] = "Add";
                    }
                    else
                    {
                        updatesizeGrids.Add(item.Key, item.Value);
                        process[item.Key + "#" + item.Value.GridNumber] = "Update";
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