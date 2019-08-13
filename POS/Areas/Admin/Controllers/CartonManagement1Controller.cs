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
    public class CartonManagement1Controller : BaseController
    {
        // GET: Admin/CartonManagement
        public ActionResult Index()
        {
            var StockDistributionSummaryId = Services.StockDistributionSummaryService.GetAllid();
            ViewBag.StockDistributionSummaryId = new SelectList(StockDistributionSummaryId, "Id", "Id");
            var BranchModelList = Services.BranchService.GetAll();
            ViewBag.BranchId = new SelectList(BranchModelList, "Id", "Name");
            return View();

        }
        [HttpPost]
        public ActionResult _Index(Model.SearchData searchData)


        {
            var StockDistributionModelList = Services.StockDistributionService.GetValue(searchData);

            return View(StockDistributionModelList);
        }

        //public ActionResult Start(CartonManagementModel CartonMangement, int? StockDistributionSummaryId, int? BranchId)
        //{
        //    bool status = Services.CartonManagementService.Create(CartonMangement);

        //    return View();
        //}

        public ActionResult Create(CartonManagementModel CartonMangement, int? StockDistributionSummaryId, int? BranchId)
        {
           // bool status = Services.CartonManagementService.Create(CartonMangement);
            var carton = Services.CartonManagementService.GetAll();
            ViewBag.TotalItems = carton.FirstOrDefault().TotalItems;
            var CartonManagement = Services.CartonManagementService.GetAll();
            var CartonManagementID = CartonManagement.LastOrDefault().Id;
            ViewBag.CartonManagementID = CartonManagementID;
            var list = Services.CartonManagementDetailService.GetAll();
            var list2 = list.Where(x => x.CartonManagementID == CartonManagementID);
            int? Total = 0;
            foreach(var item in list2)
            {
                Total +=item.Total;
            }
            ViewBag.Total = Total;
            var ProductList = Services.StockDistributionService.GetProducts(StockDistributionSummaryId, BranchId);
            ViewBag.ProductID = new SelectList(ProductList, "ProductId", "ProductName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Model.CartonManagementDetailModel CartonManagementDetail)
        {
            //var List = Services.CartonManagementDetailService.GetAll();
           
          
            //var data = List.Where(x=>x.CartonManagementID==CartonManagementDetail.CartonManagementID).ToList();
            //int? total = 0;
            //foreach (var item in data)
            //{
            //    total+=item.Total;
            //}
            //var ProductSKU = data.Select(x => x.Product.ProductSKU).FirstOrDefault();
            //if (total>24)
            //{

            //    TempData["Success"] = "Carton is full!";

            //}
            //else
            //{ 
                bool status = Services.CartonManagementDetailService.Create(CartonManagementDetail);
           // }   
        
            return RedirectToAction("Index", "CartonManagement");


        }
        public ActionResult CartonDetail(int? cartonId)


        {
            var carton = Services.CartonManagementService.GetAll();
            ViewBag.TotalItems = carton.FirstOrDefault().TotalItems;
            var CartonManagement = Services.CartonManagementService.GetAll();
            ViewBag.CartonManagementID = CartonManagement.LastOrDefault().Id;
            var CartonDetails = Services.CartonManagementDetailService.GetAll();
            
            var model = CartonDetails.GroupBy(t => t.CartonManagementID).Select(g => new CartonDetailModel
            {
                CartonManagementID = g.Key,
                 Tags= g
            });
            var list = model.ToList();
           
            foreach (var item in list)
            {
                int? total = 0;
                foreach (var tag in item.Tags)
                {
                    total += tag.Total;
                }
                ViewBag.total = total;
            }
           
            return View(model);
        }
        public ActionResult Edit(int? id, int? StockDistributionSummaryId, int? BranchId)
        {
            var CartonManagement = Services.CartonManagementService.GetAll();
            ViewBag.CartonManagementID = CartonManagement.LastOrDefault().Id;
            var ProductList = Services.StockDistributionService.GetProducts(StockDistributionSummaryId, BranchId);
            ViewBag.ProductID = new SelectList(ProductList, "ProductId", "ProductName");
            var getById = Services.CartonManagementDetailService.GetById(id);
            return View(getById);
        }
        [HttpPost]
        public ActionResult Edit(CartonManagementDetailModel CartonManagementDetail)
        {
           
            var edit = Services.CartonManagementDetailService.Edit(CartonManagementDetail);
            return RedirectToAction("Index", "CartonManagement");

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartonManagementDetailModel ModelById = Services.CartonManagementDetailService.GetById(id);

            if (ModelById == null)
            {
                return HttpNotFound();
            }
            return View(ModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(CartonManagementDetailModel carton)
        {
            if (carton.Id > 0)
            {
                CartonManagementDetailModel Delete = Services.CartonManagementDetailService.Delete(carton);
                
                return RedirectToAction("Index", "CartonManagement");
            }
            return View();
        }
    }
}