using Helper;
using Model;

using POS.Controllers;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.StockDistribution)]
    public class StockDistributionController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //public ActionResult CrateTree()
        //{
        //    var StockInventoryModelList = Services.StockInventoryService.GetAll();
        //    return View(StockInventoryModelList);
        //}
        public ActionResult Index()

        {
            StockDistributionViewModel model = new StockDistributionViewModel();
            StockDistributionSummaryModel model1 = new StockDistributionSummaryModel();
            var StockList = Services.StockDistributionSummaryService.GetAll().Where(x=>x.IsActive==true).ToList();
            //    model.Id = StockList.OrderBy(x=>x.Id).LastOrDefault().Id;
            if (StockList.Count != 0)
            {
                model1.Id = StockList.LastOrDefault().Id;
               ViewBag.Id = model1.Id;
            }
            List<StockInventoryModel> ProductModelList = Services.StockInventoryService.GetProduct();
           // List<AllProductDetailModel> ProductModelList = Services.ProductService.GetAllProduct();
            ViewBag.ProductId = new SelectList(ProductModelList, "ProductId", "ProductSKU");
            return View();
        }
        //public ActionResult StockDistribution()
        //{
        //    var Distribution = Services.StockDistributionService.GetAll();

        //}
        public ActionResult StockInventoryForProduct(int? productId)
         {
            if (productId == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var StockList = Services.StockDistributionSummaryService.GetAll();
            var summaryId = StockList.Where(x => x.IsActive == true).FirstOrDefault().Id;
            
            ViewBag.StockDistributionSummaryId = summaryId;
            var Model = Services.StockInventoryService.GetByProductId(productId, summaryId);
            return PartialView("_StockInventory", Model);
         }
       
        public JsonResult StartDistribution( StockDistributionSummaryModel stockDistributionSummary)
        {
            // Session["PurchaseOrder"] = PO.OrderNumber;
            bool status = Services.StockDistributionSummaryService.Create(stockDistributionSummary);
            
                var SummeryId = Services.StockDistributionSummaryService.GetAll().LastOrDefault().Id;
           
            return Json(SummeryId, JsonRequestBehavior.AllowGet);
            // return RedirectToAction("Index", "StockInventory");
        }
        [HttpPost]
        public ActionResult Confirm(StockDistributionSummaryModel stockDistributionSummary)
        {
            bool status = Services.StockDistributionSummaryService.Edit(stockDistributionSummary);
            return RedirectToAction("Index", "StockDistribution");
        }
        public ActionResult GetStockSummary()
        {
            StockDistributionSummaryModel model = new StockDistributionSummaryModel();
            var StockList= Services.StockDistributionSummaryService.GetAll();
            //    model.Id = StockList.OrderBy(x=>x.Id).LastOrDefault().Id;
            model.Id = StockList.Where(x => x.IsActive == true).FirstOrDefault().Id;
            ViewBag.Id = model.Id;
            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveData(StockDistributionViewModel model)
        {
           
            StockDistributionSummaryModel SummaryId = new StockDistributionSummaryModel();
            var StockList = Services.StockDistributionSummaryService.GetAll();
            SummaryId.Id = StockList.OrderBy(x => x.Id).LastOrDefault().Id;
            ViewBag.StockDistributionSummaryId = SummaryId.Id;
            bool Model = Services.StockInventoryService.SaveStock(model);

            //   return RedirectToAction("StockInventoryForProduct","StockInventory", new { DisributionSummaryId = 1, model.ProductInventory.ProductId });
            // return View(Model);
            TempData["Success"] = "Data saved successfully!";
            return RedirectToAction("Index", "StockDistribution");
        }

        public ActionResult CheckQuantity(StockDistributionModel Quantity01)
        { 
            var IsExists = Services.StockInventoryService.CheckQuantity(Quantity01);
            return Json(!IsExists, JsonRequestBehavior.AllowGet);
        }
    }
}
