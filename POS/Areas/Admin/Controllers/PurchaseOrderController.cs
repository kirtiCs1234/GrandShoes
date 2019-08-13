using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Helper;
using Services;
using System.Security.Cryptography;
using PagedList;
using System.IO;
using IronPdf;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.PurchaseOrder)]
    public class PurchaseOrderController : Controller
    {
        protected IUnitOfService Services;
        public PurchaseOrderController()
        {
            this.Services = new UnitOfService();
        }
        public ActionResult Index(int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;

            ViewBag.PageSize = pageSize;
            var PurchaseOrderList = Services.PurchaseOrderService.GetPaging(page, out TotalCount);
			foreach(var item in PurchaseOrderList)
			{
				item.OrderDate = item.OrderDate.Replace("T00:00:00", "");
			}
            ViewBag.TotalCount = TotalCount;

            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;

            ViewBag.endPage = endPage;
            return View(PurchaseOrderList);
        }
        [HttpPost]
        public ActionResult _Index(PurchaseOrderSearchModel search,int? page)
        {
            int TotalCount = 0;
            var pageSize = 10;
            var pageNumber = page ?? 1;
            int CurrentPage = pageNumber;
            var endPage = CurrentPage + 4;
            int PagesToShow = 10;
            ViewBag.PageSize = pageSize;
            var PurchaseOrderList = Services.PurchaseOrderService.GetPurchaseOrderSearchData1(search,page, out TotalCount);
			foreach (var item in PurchaseOrderList)
			{
				item.OrderDate = item.OrderDate.Replace("T00:00:00", "");
			}
			ViewBag.TotalCount = TotalCount;
            var result = Helper.CommonFunction.GetPages(TotalCount, pageSize, CurrentPage, PagesToShow);
            int totalPages = (TotalCount / pageSize) + (TotalCount % pageSize == 0 ? 0 : 1);
            ViewBag.result = result;
            ViewBag.totalPages = totalPages;
            ViewBag.CurrentPage = CurrentPage;
            var pageCount = result.Count();
            ViewBag.pageCount = pageCount;
            ViewBag.endPage = endPage;
            return View(PurchaseOrderList);
        }
        // GET: Admin/PurchaseOrder
        public ActionResult CreatePurchaseOrder()
        {
            PurchaseOrderModel PO = new PurchaseOrderModel();
			var list = Services.PurchaseOrderService.GetAll();
            PO.OrderNumber = Helper.CommonFunction.OrderNo(list);
            Session["PurchaseOrder"] = PO.OrderNumber;
            ViewBag.PurchaseOrderID = PO.OrderNumber;
            List<BuyerModel> BuyerLists = Services.BuyerService.GetAll();
            ViewBag.BuyerId = new SelectList(BuyerLists, "Id", "Name");
            var OrderStatus = Services.PurchaseOrderService.GetPurchaseOrderStatus();
            ViewBag.OrderStatusList = new SelectList(OrderStatus, "Id", "OrderStatus");
            return View(PO);
        }
        public ActionResult EditPurchaseOrder(int id)
		{
            PurchaseOrderModel PO = new PurchaseOrderModel();
            var PurchaseOrderById = Services.PurchaseOrderService.GetPurchaseOrderById(id);
            ViewBag.Amount = PurchaseOrderById.Amount;
            ViewBag.Quantity = PurchaseOrderById.Quantity;
            ViewBag.SupplierName = PurchaseOrderById.Supplier.Name;
            ViewBag.OrderNumber = PurchaseOrderById.OrderNumber;
            ViewBag.ClientInvoiceNumber = PurchaseOrderById.ClientInvoiceNumber;
            ViewBag.OrderDate = PurchaseOrderById.OrderDate.Replace("T00:00:00","");
            if (PurchaseOrderById.FirstDeliveryDate.HasValue)
            {
                ViewBag.FirstDelieveryDate = string.Format("{0:yyyy-M-dd}", PurchaseOrderById.FirstDeliveryDate);

            }
            if (PurchaseOrderById.OrderCompletionDate.HasValue)
            {
                ViewBag.OrderCompletionDate = string.Format("{0:yyyy-M-dd}", PurchaseOrderById.OrderCompletionDate);
            }
                ViewBag.BuyerName = PurchaseOrderById.Buyer.Name;
            ViewBag.PurchaseOrderID = PO.OrderNumber;
            PO.SupplierId = PurchaseOrderById.SupplierId;
            ViewBag.SupplierId = PurchaseOrderById.SupplierId;
            ViewBag.ID = id;
			PO.IsFinalize = PurchaseOrderById.IsFinalize;
            PO.BuyerId = PurchaseOrderById.BuyerId;
            PO.ClientInvoiceNumber = PurchaseOrderById.ClientInvoiceNumber;
            if (PurchaseOrderById.ExpectedDeliveryDate.HasValue)
            {
                ViewBag.ExpectedDeliveryDate = string.Format("{0:yyyy-M-dd}", PurchaseOrderById.ExpectedDeliveryDate);
            }
            PO.FirstDeliveryDate = PurchaseOrderById.FirstDeliveryDate;
            PO.FromDate = PurchaseOrderById.FromDate;
            PO.OrderCompletionDate = PurchaseOrderById.OrderCompletionDate;
            PO.OrderDate = PurchaseOrderById.OrderDate.Replace("T00:00:00","");
            PO.OrderNumber = PurchaseOrderById.OrderNumber;
            PO.PurchaseOrderStatusId = PurchaseOrderById.PurchaseOrderStatusId;
            PO.Quantity = PurchaseOrderById.Quantity;
            PO.SupplierId = PurchaseOrderById.SupplierId;
            var PurchaseOrderItemListByOrderId = Services.PurchaseOrderItemsService.GetItemByOrderId(id);
            PO.OrderedItems = PurchaseOrderItemListByOrderId;
            List<BuyerModel> BuyerLists = Services.BuyerService.GetAll();
            ViewBag.BuyerList = new SelectList(BuyerLists, "Id", "Name");
            var OrderStatus = Services.PurchaseOrderService.GetPurchaseOrderStatus();
            ViewBag.OrderStatusList = new SelectList(OrderStatus, "Id", "OrderStatus");
            return View(PO);

		}
        public ActionResult Create(int? id)
        {
			var purchaseOrder = Services.PurchaseOrderService.GetPurchaseOrderById(id);
			ViewBag.IsFinalize = purchaseOrder.IsFinalize;
            return View();
        }
        public ActionResult DeleteOrderItem(int id)
        {
            var purchaseOrderItem = Services.PurchaseOrderItemsService.GetById(id);
			ViewBag.Status = purchaseOrderItem.PurchaseOrder.IsFinalize;
			return View();
			//return RedirectToAction("EditPurchaseOrder", new { id = purchaseId});
		}
		[HttpPost,ActionName("DeleteOrderItem")]
		public ActionResult DeleteOrder(int id)
		{
			int purchaseId = Services.PurchaseOrderItemsService.DeletePurchaseOrderItems(id);
			return RedirectToAction("EditPurchaseOrder", new { id = purchaseId }); ;
			//return RedirectToAction("EditPurchaseOrder", new { id = purchaseId});
		}
		//get item by item Id(Edit)
		public ActionResult EditItem(int id)
        {
            PurchaseOrderItemModel item = Services.PurchaseOrderItemsService.GetPurchaseOrderItemsById(id);
			EditItems Data = new EditItems();
			if (item.PurchaseOrder.IsFinalize == true)
			{
				Data.IsFinalize = true;
			}
			else
			{
				 Data = GetItemsData(item);
				Data.ItemsCount = Data.Details.Count;
			}
            return PartialView(Data);
        }

        //Get A particular product items
        private EditItems GetItemsData(PurchaseOrderItemModel item)
        {
            EditItems record = new EditItems();

            record.OrderItems.autoCompleteProductStyleName = item.autoCompleteProductStyleName;
            record.OrderItems.StyleSKU = item.StyleSKU;

            record.OrderItems.autoCompleteProductName = item.autoCompleteProductName;
            record.OrderItems.ProductId = item.ProductId;

            record.OrderItems.autoCompleteGridName = item.autoCompleteGridName;
            record.OrderItems.SizeGridId = item.SizeGridId;

            record.OrderItems.autoCompleteColorName = item.autoCompleteColorName;
            record.OrderItems.ColorId = item.ColorId;

            record.OrderItems.SuplierStyle = item.SuplierStyle;
            record.OrderItems.Amount = item.Amount;

            record.OrderItems.PurchaseOrderId = item.PurchaseOrderId;
            record.OrderItems.ID = item.ID;

            if (item.ItemSize1 != null)
            {
                ItemsDetails data = new ItemsDetails();
                data.Size = item.ItemSize1 ?? 0;
                data.Cost = item.CostSize1 ?? 0;
                data.Quantity = item.QuantitySize1 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize2 != null)
            {
                ItemsDetails data = new ItemsDetails();
                data.Size = item.ItemSize2 ?? 0;
                data.Cost = item.CostSize2 ?? 0;
                data.Quantity = item.QuantitySize2 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize3 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize3 ?? 0;
                data.Cost = item.CostSize3 ?? 0;
                data.Quantity = item.QuantitySize3 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize4 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize4 ?? 0;
                data.Cost = item.CostSize4 ?? 0;
                data.Quantity = item.QuantitySize4 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize5 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize5 ?? 0;
                data.Cost = item.CostSize5 ?? 0;
                data.Quantity = item.QuantitySize5 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize6 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize6 ?? 0;
                data.Cost = item.CostSize6 ?? 0;
                data.Quantity = item.QuantitySize6 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize7 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize7 ?? 0;
                data.Cost = item.CostSize7 ?? 0;
                data.Quantity = item.QuantitySize7 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize8 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize8 ?? 0;
                data.Cost = item.CostSize8 ?? 0;
                data.Quantity = item.QuantitySize8 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize9 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize9 ?? 0;
                data.Cost = item.CostSize9 ?? 0;
                data.Quantity = item.QuantitySize9 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize10 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize10 ?? 0;
                data.Cost = item.CostSize10 ?? 0;
                data.Quantity = item.QuantitySize10 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize11 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize11 ?? 0;
                data.Cost = item.CostSize11 ?? 0;
                data.Quantity = item.QuantitySize11 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize12 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize12 ?? 0;
                data.Cost = item.CostSize12 ?? 0;
                data.Quantity = item.QuantitySize12 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize13 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize13 ?? 0;
                data.Cost = item.CostSize13 ?? 0;
                data.Quantity = item.QuantitySize13 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize14 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize14 ?? 0;
                data.Cost = item.CostSize14 ?? 0;
                data.Quantity = item.QuantitySize14 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize15 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize15 ?? 0;
                data.Cost = item.CostSize15 ?? 0;
                data.Quantity = item.QuantitySize15 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize16 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize16 ?? 0;
                data.Cost = item.CostSize16 ?? 0;
                data.Quantity = item.QuantitySize16 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize17 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize17 ?? 0;
                data.Cost = item.CostSize17 ?? 0;
                data.Quantity = item.QuantitySize17 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize18 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize18 ?? 0;
                data.Cost = item.CostSize18 ?? 0;
                data.Quantity = item.QuantitySize18 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize19 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize19 ?? 0;
                data.Cost = item.CostSize19 ?? 0;
                data.Quantity = item.QuantitySize19 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize20 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize20 ?? 0;
                data.Cost = item.CostSize20 ?? 0;
                data.Quantity = item.QuantitySize20 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize21 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize21 ?? 0;
                data.Cost = item.CostSize21 ?? 0;
                data.Quantity = item.QuantitySize21 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize22 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize22 ?? 0;
                data.Cost = item.CostSize22 ?? 0;
                data.Quantity = item.QuantitySize22 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize23 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize23 ?? 0;
                data.Cost = item.CostSize23 ?? 0;
                data.Quantity = item.QuantitySize23 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize24 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize24 ?? 0;
                data.Cost = item.CostSize24 ?? 0;
                data.Quantity = item.QuantitySize24 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize25 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize25 ?? 0;
                data.Cost = item.CostSize25 ?? 0;
                data.Quantity = item.QuantitySize25 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize26 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize26 ?? 0;
                data.Cost = item.CostSize26 ?? 0;
                data.Quantity = item.QuantitySize26 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize27 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize27 ?? 0;
                data.Cost = item.CostSize27 ?? 0;
                data.Quantity = item.QuantitySize27 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize28 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize28 ?? 0;
                data.Cost = item.CostSize28 ?? 0;
                data.Quantity = item.QuantitySize28 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize29 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize29 ?? 0;
                data.Cost = item.CostSize29 ?? 0;
                data.Quantity = item.QuantitySize29 ?? 0;
                record.Details.Add(data);
            }
            if (item.ItemSize30 != null)
            {
                ItemsDetails data = new ItemsDetails();

                data.Size = item.ItemSize30 ?? 0;
                data.Cost = item.CostSize30 ?? 0;
                data.Quantity = item.QuantitySize30 ?? 0;
                record.Details.Add(data);
            }
            return record;
        }

        //updatepurchase order
        [HttpPost]
        public JsonResult UpdatePurchaseOrderItem(PurchaseOrderItemModel PurchaseOrderItem)
        {
            int id = Services.PurchaseOrderItemsService.UpdatePurchaseOrderItems(PurchaseOrderItem);
            ViewBag.PurchaseOrderId = id;

            return Json(id);
        }


        //delete item from lists of purchase order
        public ActionResult deleteItem(int id)
        {
            PurchaseOrderItemModel Item = Services.PurchaseOrderItemsService.GetPurchaseOrderItemsById(id);
            return View(Item);
        }

        //confirm Delete Item
        [HttpPost, ActionName("deleteItem")]
        public ActionResult deleteItem(PurchaseOrderItemModel item)
        {
            Services.PurchaseOrderItemsService.DeletePurchaseOrderItems(item.ID);
            return Json(true);
        }

        //Add PurchaseOrder
        [HttpPost]
        public JsonResult AddPurchaseOrder(PurchaseOrderModel purchareOrder)
        {
            int id = 0;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                bool chechk = CheckExistingOrder(purchareOrder.OrderNumber);
                if (chechk)
                {
                    return Json("exist");
                }
                else
                {
                    id = Services.PurchaseOrderService.AddPurchaseOrder(purchareOrder);
                    ViewBag.PurchaseOrderId = id;
                }
            }          
            return Json(id);
        }
        [HttpPost]
        public JsonResult EditPurchaseOrderValues(PurchaseOrderModel purchareOrder,int Id)
        {
            purchareOrder.Id = Id;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                bool value = Services.PurchaseOrderService.UpdatePurchaseOrder(purchareOrder);
                ViewBag.PurchaseOrderId = Id;
            }
            return Json(Id);
        }
        private bool CheckExistingOrder(string OrderNumber)
        {
            bool ifOrderExist = Services.PurchaseOrderService.IsOrderExist(OrderNumber);
            return (ifOrderExist);
        }

        //auto complete supplier
        [HttpPost]
        public JsonResult AutoCompleteSupplier(string name)
        {
            var SupplierLists = Services.SupplierService.SupplierAutocomplete(name);
            return Json(SupplierLists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePurchaseOrder(PurchaseOrderModel purchareOrder)
        {
            bool status = Services.PurchaseOrderService.UpdatePurchaseOrder(purchareOrder);
            return Json(status);
        }

        //auto complete ProductStyle
        [HttpGet]
        public JsonResult AutoCompleteProductStyle(string name,string id)
        {
            var StylesLists = Services.ProductService.ProductStyleAutocomplete(name,id);
            return Json(StylesLists, JsonRequestBehavior.AllowGet);
        }

        //auto complete ProductList
        [HttpGet]
        public JsonResult AutoCompleteProductList(string name)
        {
            var StylesLists = Services.ProductService.ProductAutocomplete(name);
            return Json(StylesLists, JsonRequestBehavior.AllowGet);
        }

        //auto Complete ColorName
        [HttpGet]
        public JsonResult AutoCompleteColorList(string name,int? id)
        {
            var ColorLists = Services.ColorService.ColorAutocompleteSelected(name,id);
            return Json(ColorLists, JsonRequestBehavior.AllowGet);
        }
		public JsonResult Finalize(int id)
		{
			//var order = Services.PurchaseOrderService.GetPurchaseOrderById(id);
			bool status = Services.PurchaseOrderService.Finalize(id);
			return Json(status,JsonRequestBehavior.AllowGet);
		}
		//[HttpPost,ActionName("Finalize")]
		//public ActionResult Finalizea(int? id)
		//{
		//	bool status = Services.PurchaseOrderService.Finalize(id);
		//	return View(status);
		//}
		//Auto Complete GridNumberList
		[HttpGet]
        public JsonResult AutoCompleteGridNumberList(string name)
        {
            var GridNumberList = Services.SizeGridService.SizeGridAutocomplete(name);
            return Json(GridNumberList, JsonRequestBehavior.AllowGet);
        }
	
		[HttpPost]
        public JsonResult AddPurchaseOrderItem(PurchaseOrderItemModel PurchaseOrderItem)
        {
            int OrderNumber = PurchaseOrderItem.PurchaseOrderId;
		    int id = Services.PurchaseOrderItemsService.AddPurchaseOrderItems(PurchaseOrderItem);
			return Json(id);
        }
		[HttpPost]
		public JsonResult CheckProduct(PurchaseOrderItemModel model)
		{
			bool status = Services.PurchaseOrderItemsService.CheckProduct(model);
			return Json(status);
		}
        public ActionResult ViewPDF(int id)
        {
            var model = new Dictionary<string, string>();
           
            var data = Services.PurchaseOrderItemsService.GetDictList(id);
            var poOrder= data.Select(x => x.PurchaseOrder).FirstOrDefault();
            model.Add("OrderNumber", poOrder.OrderNumber);
            model.Add("ClientInvoiceNumber", poOrder.ClientInvoiceNumber);
            model.Add("Buyer", poOrder.Buyer?.Name);
            model.Add("OrderDate", poOrder.OrderDate?.Substring(0, poOrder.OrderDate.Length - 9));
            //if (poOrder.OrderCompletionDate != null)
            //{
            //    model.Add("OrderCompletionDate", poOrder.OrderCompletionDate.Value.ToShortDateString());
            //}
            ViewData["model"] = model;
            return View(data);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreatePdfInvoice(string html)
        {
            bool flag = false;
            TempData["html"] = html;
            if(!string.IsNullOrEmpty(html))
            {
                flag = true;
            }
            return Json(flag,JsonRequestBehavior.AllowGet);
        }

        public FileResult downloadPDF()
        {
            var html = TempData["html"] as string;
            var pdfPrintOptions = new PdfPrintOptions()
            {
                MarginTop = 10,
                MarginBottom = 10,
                MarginLeft = 2,
                MarginRight=2,
                Header = new SimpleHeaderFooter()
                {
                   // CenterText = "{pdf-title}",
                    DrawDividerLine = true,
                    FontSize = 8
                },
                Footer = new SimpleHeaderFooter()
                {
                    LeftText = "{date} {time}",
                    RightText = "Page {page} of {total-pages}",
                    DrawDividerLine = true,
                    FontSize = 8
                },
                CssMediaType = PdfPrintOptions.PdfCssMediaType.Print
            };
            var Renderer = new IronPdf.HtmlToPdf(pdfPrintOptions);
            var PDF = Renderer.RenderHtmlAsPdf(html);
            var OutputPath = Server.MapPath("/File/HtmlToPDF.pdf");//"HtmlToPDF.pdf";E:\Project\GrandShoesLatest latest\POS\PDFDesign\
            //PDF.SaveAs(OutputPath);
             using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;  filename=PurchaseOrder.pdf");
                
                PDF.Stream.WriteTo(Response.OutputStream);
                Response.Flush();
                //Response.End();
            }
            //System.Diagnostics.Process.Start(OutputPath);
            var downloadFile = new FileStreamResult(PDF.Stream, "application/pdf") { FileDownloadName = "PurchaseOrder.pdf" };
            return downloadFile;
        }
        //show PurchaseOrder Item
        [HttpPost]
		public JsonResult CheckProductByOrderId(PurchaseOrderItemModel model)
		{
			bool status = Services.PurchaseOrderItemsService.CheckProductByOrder(model);
			return Json(status,JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
        public ActionResult showPurchaseOrderItem(int id)
        {
            var Item = Services.PurchaseOrderItemsService.GetItemByOrderId(id);
            ShowPurchaseOrderDetail model = new ShowPurchaseOrderDetail();
            model.Quantity = Item.Sum(x => x.ItemCount);
            model.Amount = Item.Sum(x => x.Amount)??0;
            model.Items = Item;
            model.VatAmount =0;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public class ShowPurchaseOrderDetail
        {
            public ShowPurchaseOrderDetail()
            {
                Items = new List<PurchaseOrderItemModel>();
            }
            public string Product { get; set; }
            public decimal Amount { get; set; }
            public decimal VatAmount { get; set; }
            public decimal Quantity { get; set; }
            public List<PurchaseOrderItemModel> Items { get; set; }
        }

        public class EditItems
        {
            public EditItems()
            {
                Details = new List<ItemsDetails>();
                OrderItems = new PurchaseOrderItemModel();
            }
			public bool IsFinalize { get; set; }
            public PurchaseOrderItemModel OrderItems { get; set; }
            public List<ItemsDetails> Details { get; set; }
            public int ItemsCount { get; set; }
        }

    }
}