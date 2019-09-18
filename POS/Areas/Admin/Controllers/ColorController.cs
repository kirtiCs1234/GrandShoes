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
    [CustomAuth(PageSession.Color)]
    public class ColorController : BaseController
    {
        ServiceClass sc = new ServiceClass();
        // GET: Admin/Color
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
            var ColorModelList = Services.ColorService.GetPaging(page, out TotalCount);
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            ViewBag.result = result;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.CurrentPage = CurrentPage;
            ViewBag.endPage = endPage;

            return View(ColorModelList);

        }
       
        public ActionResult _Index1(ColorSearch colorSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var body = JsonConvert.SerializeObject(colorSearch);
             var ColorModelList = Services.ColorService.GetSearchData(colorSearch, page, out TotalCount);
          
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
            return View(ColorModelList);

        }
        public ActionResult ExcelUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(ColorModel color, HttpPostedFileBase ExcelFile)
        {
            var Code = new Dictionary<int, string>();
            Dictionary<string, string> process = new Dictionary<string, string>();
           Dictionary<int, ColorModel> Tempcolors = new Dictionary<int,ColorModel>();
           Dictionary<int, ColorModel> colors = new Dictionary<int, ColorModel>();
           Dictionary<int, ColorModel> updatecolors = new Dictionary<int,ColorModel>();
            Dictionary<int, ColorModel> filter = new Dictionary<int,ColorModel>();
            var msgList = new List<Message>();
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
					var msg = new Message();
					try
					{
						if (!string.IsNullOrEmpty(row) && row.Split(',')[0].All(char.IsDigit) && !row.StartsWith(",,,,,"))
						{
                            i++;
                            var model = new ColorModel();
                            var rowSplit = row.Split(',');
                            model.Code = rowSplit[0].ToString();
                            model.ColorLong = rowSplit[1].ToString();
                            model.ColorShort = rowSplit[2].ToString();
                            model.IsActive = true;
							var count = rowSplit[0].Length;
                            if (count == 3)
                            {
                                //bool isexists = Services.ColorService.CheckColorCode(chk);
                                Tempcolors.Add(i, model);
                                Code.Add(i, model.Code);
                            }
                            else
                            {
                                process[i + "#" + rowSplit[0]] = "Check the Color Code Length";
                            }
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
            var isexists = Services.ColorService.CheckColor(Code);
            if (Tempcolors!= null && Tempcolors.Count > 0)
            {
                foreach (var item in Tempcolors)
                {
                    if (isexists[item.Key] == false)
                    {
                        colors.Add(item.Key, item.Value);
                        process[item.Key + "#" + item.Value.Code] = "Add";
                    }
                    else
                    {
                        updatecolors.Add(item.Key, item.Value);
                        process[item.Key + "#" + item.Value.Code] = "Update";
                    }
                }
            }
            var addList = new Dictionary<string, string>();//Services.ProductService.CreateList(products);
            var updateList = new Dictionary<string, string>();//Services.ProductService.UpdateList(updateProducts);

            if (colors.Count > 0)
            {
                addList = Services.ColorService.CreateList(colors);
            }
            if (updatecolors.Count > 0)
            {
                updateList = Services.ColorService.UpdateList(updatecolors);
            }
            var dictionaryFrom = new Dictionary<string, string>();
            dictionaryFrom = sc.getFilterData(addList, updateList, process);
            //updateList.ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));
            TempData["ProcessData"] = dictionaryFrom;
            return RedirectToAction("Index", "Color");
		}

        public ActionResult ExportList()
        {
            var data = Services.ColorService.GetAllDAL();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;  filename=Color.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                //Response.End();
            }
            return View();
        }
      

        public ActionResult Create()
        {
            return View(new ColorModel());
        }
        //Add Color
        public ActionResult AddColor(ColorModel Color)
        {
            bool status = Services.ColorService.AddColor(Color);
            TempData["Success"] = "Data Saved Successfully!";
            return RedirectToAction("Index");
        }

        //
        public ActionResult Delete(int id)
        {
            ColorModel Color = Services.ColorService.GetColorByColorId(id);
            return View(Color);
        }

        //confirm Delete Color
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteColor(ColorModel color)
        {
            Services.ColorService.DeleteColor(color.Id);
            TempData["Success"] = "Data Deleted Successfully!";
            return RedirectToAction("Index");
        }


        //Get Particular color(Edit(By ColorId))
        public ActionResult Edit(int Id)
        {
            ColorModel Color = Services.ColorService.GetColorByColorId(Id);
            return View(Color);
        }

        //Search color
        public ActionResult Search(ColorModel SearchColor)
        {
            List<ColorModel> Color = Services.ColorService.SearchColor(SearchColor);
            return PartialView(Color);
        }

        //Update Color
        [HttpPost]
        public ActionResult UpdateColor(ColorModel Color)
        {
            Color.IsActive = true;
            Services.ColorService.UpdateColor(Color);
            TempData["Success"] = "Data Saved Successfully!";
            return RedirectToAction("Index");
        }

        //get Details of color
        public ActionResult Details(int id)
        {
            
            ColorModel Color = Services.ColorService.GetColorByColorId(id);
            return View(Color);
        }

        //Remote validations for checking existing color code
        public ActionResult CheckExistingColor(int Id, string Code)
        {
            bool ifColorExist = Services.ColorService.IsColorExist(Id, Code);
            return Json(!ifColorExist, JsonRequestBehavior.AllowGet);
        }
    }
}