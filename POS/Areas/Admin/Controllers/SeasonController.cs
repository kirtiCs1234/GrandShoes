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
    [CustomAuth(PageSession.Season)]
    public class SeasonController : BaseController
    {
        ServiceClass sc = new ServiceClass();
        // GET: Admin/Season
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
            var SeasonModelList = Services.SeasonService.GetPaging(page, out TotalCount);
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

            return View(SeasonModelList);

        }
        
        public ActionResult _Index1(SeasonSearch seasonSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
              var body = JsonConvert.SerializeObject(seasonSearch);
                var SeasonModelList = Services.SeasonService.GetSearchData(seasonSearch, page, out TotalCount);
           
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;

            return View(SeasonModelList);

        }
        public ActionResult ExcelUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(SeasonModel season, HttpPostedFileBase ExcelFile)
        {
            Dictionary<string, string> process = new Dictionary<string, string>();
            Dictionary<int, SeasonModel> seasons = new Dictionary<int, SeasonModel>();
            Dictionary<int, SeasonModel> updateseasons = new Dictionary<int, SeasonModel>();

           // List<SeasonModel> seasons = new List<SeasonModel>();
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
							var model = new SeasonModel();
							model.Code = row.Split(',')[0];
							model.Description = row.Split(',')[1];
							model.IsActive = true;
							string chk = row.Split(',')[0];
							bool isexists = Services.SeasonService.CheckSeasonCode1(chk);
							if (!isexists)
							{
								seasons.Add(i,model);
                                process[i + "#" + row.Split(',')[0]] = "Add";
                            }
							else
							{
                                updateseasons.Add(i, model);
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
			var addList = Services.SeasonService.CreateList(seasons);
			var updateList = Services.SeasonService.UpdateList(updateseasons);
            var dictionaryFrom = new Dictionary<string, string>();

            dictionaryFrom = sc.getFilterData(addList, updateList, process);
            TempData["ProcessData"] = dictionaryFrom;

            TempData["Success"] = "Data Uploaded Successfully!";
			return RedirectToAction("Index", "Season");
		}

        public ActionResult ExportList()
        {
            var data = Services.ColorService.GetAll();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;  filename=Season.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                //Response.End();
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            var body = JsonConvert.SerializeObject(id);
            var result = Services.SeasonService.GetById(id);

            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SeasonModel seasonModel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                seasonModel.IsActive = true;
                var body = JsonConvert.SerializeObject(seasonModel);
                var result = Services.SeasonService.Create(seasonModel);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Season");
            }
            return View(seasonModel);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            SeasonModel SeasonModelById = Services.SeasonService.GetById(id);
            return View(SeasonModelById);
        }

        [HttpPost]
        public ActionResult Edit(SeasonModel season)
        {
            if (ModelState.IsValid)
            {
                SeasonModel Edit = Services.SeasonService.Edit(season);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Season");
            }
            return View(season);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeasonModel SeasonModelById = Services.SeasonService.GetById(id);

            if (SeasonModelById == null)
            {
                return HttpNotFound();
            }
            return View(SeasonModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(SeasonModel season)
        {
            if (season.Id > 0)
            {
                SeasonModel UserDelete = Services.SeasonService.Delete(season);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Season");
            }
            return View(season);
        }
        public ActionResult CheckSeasonCode(SeasonModel season)
      {
            var iExist = Services.SeasonService.CheckSeasonCode(season);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
    }
}