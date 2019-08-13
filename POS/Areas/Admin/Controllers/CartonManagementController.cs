using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth]
    public class CartonManagementController : BaseController
    {
        // GET: Admin/CartonManagement
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            CartonManagementStockModel model = new CartonManagementStockModel();
            var StockDistributionSummaryId = Services.StockDistributionSummaryService.GetAllid();
            ViewBag.StockDistributionSummaryId = new SelectList(StockDistributionSummaryId, "Id", "Id");
            var BranchModelList = Services.BranchService.GetAll();
            
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
            var CartonManagemenetList = Services.CartonManagementService.GetPaging(page, out TotalCount);
            if (CartonManagemenetList.Count!=0)
            {
                ViewBag.CartonManagementID = CartonManagemenetList.LastOrDefault().Id;
            }
            
            model.CartonList = CartonManagemenetList;
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(model);
        }
        public ActionResult _Index1(CartonManagementModel search, int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            CartonManagementStockModel model = new CartonManagementStockModel();
            var StockDistributionSummaryId = Services.StockDistributionSummaryService.GetAllid();
            ViewBag.StockDistributionSummaryId = new SelectList(StockDistributionSummaryId, "Id", "Id");
            var BranchModelList = Services.BranchService.GetAll();

            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");

            var CartonManagementList = Services.CartonManagementService.GetSearchData(search, page, out TotalCount);
            if (CartonManagementList!=null)
            {
                ViewBag.CartonManagementID = CartonManagementList.LastOrDefault().Id;
            }

            model.CartonList = CartonManagementList;
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(model);

        }
        public ActionResult Create(SearchForCarton model)
        {
            bool createCartonManagement = Services.CartonManagementService.Create(model);
            ViewBag.StockDistributionSummaryId = model.StockDistributionSummaryId;
            ViewBag.BranchId = model.BranchId;
            var CartonManagementID = Services.CartonManagementService.GetAll();
            ViewBag.CartonManagementID = CartonManagementID.LastOrDefault().Id;
            var stockDistributionList = Services.StockDistributionService.GetProductForCarton(model);
            return View(stockDistributionList);
        }
        public JsonResult productSelectList(int? StockDistributionSummaryId, int? BranchId)
        {
            var ProductList = Services.StockDistributionService.GetProducts(StockDistributionSummaryId, BranchId);
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddCartonItem(CartonManagementDetailModel model)
        {
            var list = Services.StockDistributionService.GetAll();
            var ProductId = list.Where(x => x.Id == model.StockDistributionId).FirstOrDefault().ProductId;
            model.ProductID = ProductId;
            bool create = Services.CartonManagementDetailService.Create(model);
            return RedirectToAction("Index","CartonManagement");
        }
        public ActionResult AddCarton(int? CartonManagementId)
        {
            bool AddReceipt = Services.CartonManagementService.AddCartonOrder(CartonManagementId);
            return RedirectToAction("Index", "CartonManagement");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartonManagementModel CartonManagementModelById = Services.CartonManagementService.GetById(id);

            if (CartonManagementModelById == null)
            {
                return HttpNotFound();
            }
            return View(CartonManagementModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(CartonManagementModel model)
        {
            if (model.Id > 0)
            {
               var CartonDelete = Services.CartonManagementService.Delete(model);
                TempData["Success"] = "Data Deleted Successfully!";
                return RedirectToAction("Index", "CartonManagement");
            }
            return View(model);
        }
        public ActionResult Edit(int? id, SearchForCarton model)
        {
            var CartonManagementForEdit = new CartonManagementForEdit();
            var CartonManagementById = Services.CartonManagementService.GetById(id);
            model.StockDistributionSummaryId = CartonManagementById.DistributionSummaryID;
            ViewBag.StockDistributionSummaryId = model.StockDistributionSummaryId;
            var StockDataList = Services.StockDistributionService.GetProductForCarton(model);
            ViewBag.CartonManagementID = id;
            CartonManagementForEdit.StockDictributionList = StockDataList;
            var CartonDetailList = Services.CartonManagementDetailService.GetAll();
            ViewBag.BranchId = model.BranchId;

            CartonManagementForEdit.CartonManagementDetailList = CartonDetailList.Where(x=>x.CartonManagementID==id).ToList();
            ViewBag.Count = CartonManagementForEdit.CartonManagementDetailList.Count;
            var ProductList = Services.StockDistributionService.GetAll();
            var ProductListByBranch = ProductList.Where(x=>x.BranchId==model.BranchId && x.StockDistributionSummaryId==CartonManagementById.DistributionSummaryID).ToList();
            ViewBag.ProductID = new SelectList(ProductListByBranch, "Id", "ProductSKU");
            return View(CartonManagementForEdit);
        }
        public ActionResult GetCartonDetailByCartonId(int? id)
        {
           
            var CartonDetailList = Services.CartonManagementDetailService.GetAll();
            var CartonByCartonId = CartonDetailList.Where(x => x.CartonManagementID == id).ToList();
            return View(CartonByCartonId);
        }
        [HttpPost]
        public ActionResult EditCartonItem(CartonManagementDetailModel model)
        {
            bool edit = Services.CartonManagementDetailService.Edit(model);
            return RedirectToAction("Index", "CartonManagement");
        }
        public ActionResult EditCartonManagement(int? Id)
        {
            bool Addcarton = Services.CartonManagementService.EditCartonManagement(Id);
            return RedirectToAction("Index", "CartonManagement");
        }
    }
}