using Helper;
using LinqToExcel;
using Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Supplier)]
    public class SupplierController : BaseController
    {
        ServiceClass sc = new ServiceClass();
        // GET: Admin/Supplier
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
            int endPage = CurrentPage + 4;
            int PagesToShow = 10;
            var SupplierModelList = Services.SupplierService.GetPaging(page, out TotalCount);
			foreach(var date in SupplierModelList)
			{
				var str = date.RegistrationDate;
                if(!string.IsNullOrEmpty(str))
				    str = str.Substring(0, str.Length - 9);
				date.RegistrationDate = str;
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

            return View(SupplierModelList);

        }
       
        public ActionResult _Index1(SupplierSearch supplierSearch, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            int endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.TotalCount = TotalCount;
            ViewBag.PageSize = pageSize;
                var body = JsonConvert.SerializeObject(supplierSearch);
                var SupplierModelList = Services.SupplierService.GetSearchData(supplierSearch, page, out TotalCount);
			foreach (var date in SupplierModelList)
			{
                var str = date.RegistrationDate;
                if (!string.IsNullOrEmpty(str)) { 
                    str = str.Substring(0, str.Length - 9);
                    date.RegistrationDate = str;
                }
			}
			var result = CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(SupplierModelList);
        }

        public ActionResult ExcelUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(SupplierModel supplier, HttpPostedFileBase ExcelFile)
        {
            var Code = new Dictionary<int, string>();
            Dictionary<string, string> process = new Dictionary<string, string>();
            Dictionary<int, SupplierModel> suppliers = new Dictionary<int, SupplierModel>();
            Dictionary<int, SupplierModel> Tempsuppliers = new Dictionary<int, SupplierModel>();
            Dictionary<int, SupplierModel> updatesuppliers = new Dictionary<int, SupplierModel>();
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
							var model = new SupplierModel();
                            var rowSplit = row.Split(',');
							model.Code = rowSplit[0];
							model.Name = rowSplit[1];
							model.PermanentAddress1 = rowSplit[2];
							model.PermanentAddress2 = rowSplit[3];
							model.PermanentAddress3 = rowSplit[4];
							model.PermanentCity = rowSplit[5];
							model.PermanentCountry = rowSplit[6];
							model.PermanentPostalCode = rowSplit[7];
							model.CorrespondanceAddress1 = rowSplit[8];
							model.CorrespondanceAddress2 = rowSplit[9];
							model.CorrespondanceAddress3 = rowSplit[10];
							model.CorrespondanceCity = rowSplit[11];
							model.CorrespondanceCountry = rowSplit[12];
							model.CorrespondancePostalCode = rowSplit[13];
							model.Limit = Convert.ToInt32(rowSplit[14]);
							model.ContactNumber = rowSplit[15];
							model.FaxNumber = rowSplit[16];
							model.RegistrationDate = rowSplit[17];
							model.IsActive = true;
                            Tempsuppliers.Add(i, model);
                            Code.Add(i, model.Code);
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
            var check = Services.SupplierService.SupplierCheckFilter(Code);//ServerResponse.Invoke<Dictionary<int, bool>>("api/product/getProductCheckFilter", body2, "POST");
            if (Tempsuppliers != null && Tempsuppliers.Count > 0)
            {
                foreach (var item in Tempsuppliers)
                {
                    if (check[item.Key] == false)
                    {
                        suppliers.Add(item.Key, item.Value);
                        process[item.Key + "#" + item.Value.Code] = "Add";
                    }
                    else
                    {
                        updatesuppliers.Add(item.Key, item.Value);
                        process[item.Key + "#" + item.Value.Code] = "Update";
                    }
                }
            }

            var addList = Services.SupplierService.CreateList(suppliers);
			var updateList = Services.SupplierService.UpdateList(updatesuppliers);
            var dictionaryFrom = new Dictionary<string, string>();

            dictionaryFrom = sc.getFilterData(addList, updateList, process);
            TempData["ProcessData"] = dictionaryFrom;

            TempData["Success"] = "Data Uploaded Successfully!";
			return RedirectToAction("Index", "Supplier");

		}


		public ActionResult ExportList()
        {
            var data = Services.SupplierService.GetAll();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 2].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;  filename=Supplier1.xlsx");
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
            SupplierModel SupplierModelById = Services.SupplierService.GetById(id);
			SupplierModelById.RegistrationDate = SupplierModelById.RegistrationDate.Substring(0, SupplierModelById.RegistrationDate.Length - 9);
			return View(SupplierModelById);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SupplierModel supplier)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                // user.IsActive = true;
                var SupplierCreate = Services.SupplierService.Create(supplier);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Supplier");
            }
            return View(supplier);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierModel SupplierModelById = Services.SupplierService.GetById(id);
            if (SupplierModelById.RegistrationDate != null)
            {
                SupplierModelById.RegistrationDate = SupplierModelById.RegistrationDate.Substring(0, SupplierModelById.RegistrationDate.Length - 9);
            }
            return View(SupplierModelById);
        }

        [HttpPost]
        public ActionResult Edit(SupplierModel supplier)
        {
            
                bool SupplierEdit = Services.SupplierService.Edit(supplier);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Supplier");
            
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierModel SupplierModelById = Services.SupplierService.GetById(id);

            if (SupplierModelById == null)
            {
                return HttpNotFound();
            }
            return View(SupplierModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(SupplierModel supplier)
        {
            if (supplier.Id > 0)
            {
                SupplierModel SupplierDelete = Services.SupplierService.Delete(supplier);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Supplier");
            }
            return View(supplier);
        }
        
        public ActionResult CheckExistingSupplier(int? Id, string Code)
        {
            int id = Id ?? 0;
            bool ifColorExist = Services.SupplierService.IsSupplierExist(id, Code);
            return Json(!ifColorExist, JsonRequestBehavior.AllowGet);
        }
    }
}