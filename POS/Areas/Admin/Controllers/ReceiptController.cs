using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList.Mvc;
using PagedList;
using System.Web;
using System.Web.Mvc;
using Helper;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Receipt)]
    public class ReceiptController : BaseController
    {
        // GET: Admin/Receipt
        
        public ActionResult Index(int? page)
        
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var PurchaseOrderList = Services.PurchaseOrderService.GetByReceiptOrder();

            return View(PurchaseOrderList.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
       public ActionResult _Index1(PurchaseOrderSearch search,int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var PurchaseOrderList = Services.PurchaseOrderService.GetByReceiptOrder();
            if (search != null)
            {
                if (search.IsActive == true)
                {
                    if (!String.IsNullOrEmpty(search.OrderNumber) || !String.IsNullOrEmpty(search.SupplierName))
                    {
                        PurchaseOrderList = PurchaseOrderList.Where(x => x.OrderNumber==search.OrderNumber || x.SupplierName.Contains(search.SupplierName.ToLower())).ToList();

                    }
                }
            }
            else
            {
                PurchaseOrderList = PurchaseOrderList.Where(x => x.OrderNumber.Contains(search.OrderNumber.ToLower()) || x.SupplierName.Contains(search.SupplierName.ToLower())).ToList();

            }
            return View(PurchaseOrderList.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult GetReceiptById(int? id)
        {
            var ReceiptOrderList = Services.ReceiptOrderService.GetAllReceiptOrder(id);
            var list = ReceiptOrderList.Where(x => x.TotalQuantity != 0 && x.TotalCost != 0).ToList();
            return View(list);
        }
        public ActionResult Create(int id)
        {
            // ViewBag.ReceiveOrderId = RO.ReceiptNumber;
            ReceiveOrderModel ROM = new ReceiveOrderModel();
			var list = Services.ReceiptOrderService.GetAllReceipt();
			ROM.ReceiptNumber = Helper.CommonFunction.ReceiptNo(list);
			Session["ReceiveOrder"] = ROM.ReceiptNumber;
			ROM.PurchaseOrderId = id;
			var receiptOrder = Services.ReceiptOrderService.ReceiptByPurchaseOrder(id);
			if (receiptOrder != null)
			{
				ROM.IsFinalize = receiptOrder.IsFinalize;
			}
			var create = Services.ReceiptOrderService.Create(ROM);
            var ReceiptOrder = Services.ReceiptOrderService.GetAllReceiptOrder(id);
			var purchaseOrderItemList = Services.PurchaseOrderItemsService.GetItemsByPurchase(id);
			ROM.purchaseOrderItemList = purchaseOrderItemList;
           ViewBag.ReceiptId = ReceiptOrder.FirstOrDefault().Id;
            return View(ROM);
        }
		[HttpPost]
		public JsonResult Create(int Id,ReceiveOrderModel model)
		{
			bool create = Services.ReceiptOrderService.AddReceiptOrder(model);
			return Json(create,JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult Edit(int Id, ReceiveOrderModel model)
		{
			bool edit = Services.ReceiptOrderService.EditReceiptOrder(model);
			return Json(edit, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Edit(int Id)
        {
            ReceiveOrderModel RO = new ReceiveOrderModel();
            var ReceiptOrderById = Services.ReceiptOrderService.GetById(Id);
			RO.purchaseOrderItemList = Services.PurchaseOrderItemsService.GetItemsByPurchase(ReceiptOrderById.PurchaseOrderId??0);
			RO.ReceiptNumber = ReceiptOrderById.ReceiptNumber;
            RO.SupplierInvoice = ReceiptOrderById.SupplierInvoice;
            RO.Id = Id;
			RO.IsFinalize = ReceiptOrderById.IsFinalize;
			RO.PurchaseOrderId = ReceiptOrderById.PurchaseOrderId;
            var receiptOrderItemList = Services.ReceiptOrderService.GetByReceiptOrderId(Id);
            RO.ReceiptOrderItems = ReceiptOrderById.ReceiptOrderItems;
            ViewBag.Count = receiptOrderItemList.Count();
            return View(RO);
        }
      
        public JsonResult GetPurchaseOrderForUpdate(int? Id,int? productId)
        {
            var Product = Services.ReceiptOrderService.GetUpdateValues(Id,productId);
            return Json(Product, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AutoCompleteProductSKUList(string name)
        {
            var ProductLists = Services.ProductService.ProductAutocomplete(name);
            return Json(ProductLists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AutoCompleteStyleSKUList(string name,string id)
        {
            var StylesLists = Services.ProductService.ProductStyleAutocomplete(name,id);
            return Json(StylesLists, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPurchaseOrder(PurchaseOrderSearch model)

        {
            var PurchaseOrderValue = Services.PurchaseOrderItemsService.GetValue(model);
            return Json(PurchaseOrderValue, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddReceiptOrderItem(ReceiptOrderItemModel model)
        {
            var productList = Services.ProductService.GetAll();
            model.ProductId = productList.Where(x => x.ProductSKU == model.autoCompleteProductName && x.StyleSKU==model.autoCompleteProductStyleName).FirstOrDefault().Id;
            var create = Services.ReceiptOrderService.AddReceiptOrderItem(model);
            
            return RedirectToAction("Index", "Receipt");
        }
        [HttpPost]
        public ActionResult EditReceiptOrderItem(ReceiptOrderItemModel model)


        {
            //var productList = Services.ProductService.GetAll();
           // model.ProductId = productList.Where(x => x.ProductSKU == model.autoCompleteProductName).FirstOrDefault().Id;
            var edit = Services.ReceiptOrderService.EditReceiptOrderItem(model);
            return RedirectToAction("Index", "Receipt");
        }
        //public ActionResult AddReceiptOrder(int? ReceiptOrderId)
        //{
        //    bool AddReceipt = Services.ReceiptOrderService.AddReceiptOrder(ReceiptOrderId);
        //   // var AddStockInventory = Services.ReceiptOrderService.Create();
        //    return RedirectToAction("Index", "Receipt");
        // }
        //public ActionResult EditReceiptOrder(int? Id)
        //{
        //    bool AddReceipt = Services.ReceiptOrderService.EditReceiptOrder(Id);
        //    return RedirectToAction("Index", "Receipt");
        //}
        public ActionResult Detail(int? Id)
        {
            var receiptOrderItemList = Services.ReceiptOrderService.GetByReceiptOrderId(Id);
            return View(receiptOrderItemList);
        }
        public ActionResult Delete(int? Id)
        {

            ReceiveOrderModel getById = Services.ReceiptOrderService.GetById(Id);
           // bool status = Services.ReceiptOrderService.Delete(Id);
			ViewBag.Status = getById.IsFinalize;
            return View();

        }
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int? Id)
		{

			bool status = Services.ReceiptOrderService.Delete(Id);
			TempData["Success"] = "Data Deleted Successfully!";
		    return RedirectToAction("Index", "Receipt");
			
		}
		public ActionResult ViewDistribution(int purchaseId)
		{
			var puchaseOrderItemList = Services.PurchaseOrderItemsService.GetItemsByPurchaseOrderId(purchaseId);
			return View(puchaseOrderItemList);
		}
		public ActionResult ViewPending(int id)
		{
			var purchaseOrderItemList = Services.PurchaseOrderItemsService.GetItemsByPurchase(id);
			return View(purchaseOrderItemList);
		}
		public ActionResult Finalize(PendingItemReceiptModel model)
		{
			var purchaseItem = Services.PurchaseOrderItemsService.GetItemsByPurchase(model.PurchaseOrderId);
			foreach(var a in purchaseItem)
			{
				model.TotalCost = a.Amount;
				model.TotalItem = a.ItemCount;
				model.Quantity01 = a.QuantitySize1; model.Quantity02 = a.QuantitySize2; model.Quantity03 = a.QuantitySize3; model.Quantity04 = a.QuantitySize4; model.Quantity05 = a.QuantitySize5; model.Quantity06 = a.QuantitySize6; model.Quantity07 = a.QuantitySize7; model.Quantity08 = a.QuantitySize8; model.Quantity09 = a.QuantitySize9; model.Quantity10 = a.QuantitySize10; model.Quantity11 = a.QuantitySize11; model.Quantity12 = a.QuantitySize12; model.Quantity13 = a.QuantitySize13; model.Quantity14 = a.QuantitySize14; model.Quantity15 = a.QuantitySize15; model.Quantity16 = a.QuantitySize16; model.Quantity17 = a.QuantitySize17; model.Quantity18 = a.QuantitySize18; model.Quantity19 = a.QuantitySize19; model.Quantity20 = a.QuantitySize20; model.Quantity21 = a.QuantitySize21; model.Quantity22 = a.QuantitySize22; model.Quantity23 = a.QuantitySize23; model.Quantity24 = a.QuantitySize24; model.Quantity25 = a.QuantitySize25; model.Quantity26= a.QuantitySize26; model.Quantity27 = a.QuantitySize27; model.Quantity28 = a.QuantitySize28; model.Quantity29 = a.QuantitySize29; model.Quantity30 = a.QuantitySize30;
				model.Cost01 = a.CostSize1; model.Cost02 = a.CostSize2; model.Cost03 = a.CostSize3; model.Cost04 = a.CostSize4; model.Cost05 = a.CostSize5; model.Cost06 = a.CostSize6; model.Cost07 = a.CostSize7; model.Cost08 = a.CostSize8; model.Cost09 = a.CostSize9; model.Cost10 = a.CostSize10; model.Cost11 = a.CostSize11; model.Cost12 = a.CostSize12; model.Cost13 = a.CostSize13; model.Cost14 = a.CostSize14; model.Cost15 = a.CostSize15; model.Cost16 = a.CostSize16; model.Cost17 = a.CostSize17; model.Cost18 = a.CostSize18; model.Cost19 = a.CostSize19; model.Cost20 = a.CostSize20; model.Cost21 = a.CostSize21; model.Cost22 = a.CostSize22; model.Cost21 = a.CostSize21; model.Cost24 = a.CostSize24; model.Cost25 = a.CostSize25; model.Cost26 = a.CostSize26; model.Cost27 = a.CostSize27; model.Cost28 = a.CostSize28; model.Cost29 = a.CostSize29; model.Cost30 = a.CostSize30; 	model.ProductId = a.ProductId;
				bool create = Services.PendingItemReceiptService.Create(model);

			}
			return RedirectToAction("Index");
		}
	}
}