using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Model;
using Helper.ExtensionMethod;
using Helper;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/receiptOrder")]
    public class ReceiveOrdersController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ReceiveOrdersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ReceiveOrders
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public IHttpActionResult GetReceiveOrders(int? id)
        {
            var list= db.ReceiveOrders.Where(x=>x.IsActive==true && x.PurchaseOrderId==id).Include(x=>x.PurchaseOrder).OrderByDescending(x => x.Id).ToList().Select(x=>new ReceiveOrderModel {
                Id=x.Id,
                ReceiptNumber=x.ReceiptNumber,
                PurchaseOrderId=x.PurchaseOrderId,
                TotalQuantity=x.TotalQuantity,
                TotalCost=x.TotalCost,
                TotalVAT=x.TotalVAT,
				IsFinalize=x.IsFinalize,
                SupplierInvoice=x.SupplierInvoice,
                ReceiptDate=x.ReceiptDate,
                IsActive=x.IsActive
            }).ToList();
            return Ok(list);
        }
		
        //Search
        [HttpPost]
        [Route("search")]
        public List<ReceiveOrder>Serach(ReceiptSearch search)
        {
            var list = db.ReceiveOrders.Where(x => x.IsActive == true).Include(x => x.PurchaseOrder).ToList();
            if(search.Name !=null )
            {
                list.Where(x => x.PurchaseOrder.OrderNumber.ToLower().Equals(search.Name.ToLower()));
            }
            return list;
        }
		[HttpGet]
		[Route("getByPurchaseOrderId")]
		public IHttpActionResult GetBuPurchaseOrder(int id)
		{
			var result = db.ReceiveOrders.Where(x => x.IsActive == true && x.PurchaseOrderId == id && x.TotalQuantity != 0).FirstOrDefault();
			return Ok(result);
		}
        [HttpGet]
        [Route("getAll")]
        public List<ReceiveOrder> GetAll()
        {
            var list = db.ReceiveOrders.Where(x => x.IsActive == true).OrderByDescending(x=>x.Id).Include(x=>x.PurchaseOrder).ToList().RemoveReferences();
            return list;
        }
        // GET: api/ReceiveOrders/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getById")]
        [ResponseType(typeof(ReceiveOrder))]
        public IHttpActionResult GetReceiveOrder(int id)
        {
            var receiveOrder = db.ReceiveOrders.Where(x=>x.IsActive==true &&x.Id==id).Include(x => x.ReceiptOrderItems).FirstOrDefault();
            if (receiveOrder == null)
            {
                return NotFound();
            }

            return Ok(receiveOrder);
        }
     
        [Route("receiveOrderStatus")]
        public IHttpActionResult GetAllPurchaseOrderStatus()
        {
            var OrderStatusList = db.PurchaseOrderStatus.Where(s => s.IsActive == true).ToList().Select(m => new Model.ReceiveOrderStatusModel
            {
                Id = m.Id,
                OrderStatus = m.OrderStatus,
                IsActive = m.IsActive ?? true,
            }).ToList();
            return Ok(OrderStatusList);
        }
       
        // PUT: api/ReceiveOrders/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReceiveOrder(int Id, ReceiveOrder receiveOrder)
        {
			var receipt = db.ReceiveOrders.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault();
			receipt.IsActive = true;
			receiveOrder.ReceiptOrderItems = receiveOrder.ReceiptOrderItems;

			return Ok(true);
        }

        // POST: api/ReceiveOrders
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(ReceiveOrder))]
        public IHttpActionResult PostReceiveOrder(ReceiveOrder receiveOrder)
        {
            ReceiveOrder model = new ReceiveOrder();
            model.PurchaseOrderId = receiveOrder.PurchaseOrderId;
            model.ReceiptNumber = receiveOrder.ReceiptNumber;
            DateTime dNow = DateTime.Now;
            model.ReceiptDate = dNow;
            model.LogId = 2;
            model.TotalQuantity = 0;
            model.TotalCost = 0;
            model.TotalVAT = 0;
            model.IsActive = true;
            db.ReceiveOrders.Add(model);
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("AddReceiptOrder")]
        [ResponseType(typeof(ReceiveOrder))]
        public IHttpActionResult PostReceiveOrder(int Id, ReceiveOrder receiveOrder)
        {
            var pageName = Request.RequestUri.LocalPath.getRouteName();
            var kkk = pageName.Substring(3, 7);
            var receipt = db.ReceiveOrders.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault();
            
			receipt.SupplierInvoice = receiveOrder.SupplierInvoice;
			receipt.IsActive = true;
			//receipt.ReceiptOrderItems = receiveOrder.ReceiptOrderItems;
			int? TotalQuantity = 0;
                decimal? TotalCost = 0;
                foreach (var a in receiveOrder.ReceiptOrderItems)
                {
				if(a.ProductId!=null)
				{
					TotalQuantity += a.Quantity01 + a.Quantity02 + a.Quantity03 + a.Quantity04 + a.Quantity05 + a.Quantity06 + a.Quantity07 + a.Quantity08 + a.Quantity09 + a.Quantity10 + a.Quantity11 + a.Quantity12 + a.Quantity13 + a.Quantity14 + a.Quantity15 + a.Quantity16 + a.Quantity17 +
						a.Quantity18 + a.Quantity19 + a.Quantity20 + a.Quantity21 + a.Quantity22 + a.Quantity23 + a.Quantity24 + a.Quantity25 + a.Quantity26 + a.Quantity27 + a.Quantity28 + a.Quantity29 + a.Quantity30;

					a.SalesCost = (a.Quantity01 ?? 0) * (a.Cost01 ?? 0) + (a.Quantity02 ?? 0) * (a.Cost02 ?? 0) + (a.Quantity03 ?? 0) * (a.Cost03 ?? 0) +
						   (a.Quantity04 ?? 0) * (a.Cost04 ?? 0) + (a.Quantity05 ?? 0) * (a.Cost05 ?? 0) + (a.Quantity06 ?? 0) * (a.Cost06 ?? 0) +
						   (a.Quantity07 ?? 0) * (a.Cost07 ?? 0) + (a.Quantity08 ?? 0) * (a.Cost08 ?? 0) + (a.Quantity09 ?? 0) * (a.Cost09 ?? 0) + (a.Quantity10 ?? 0) * (a.Cost10 ?? 0) +
						   (a.Quantity11 ?? 0) * (a.Cost11 ?? 0) + (a.Quantity12 ?? 0) * (a.Cost12 ?? 0) + (a.Quantity13 ?? 0) * (a.Cost13 ?? 0) + (a.Quantity14 ?? 0) * (a.Cost15 ?? 0) +
						   (a.Quantity16 ?? 0) * (a.Cost16 ?? 0) + (a.Quantity17 ?? 0) * (a.Cost17 ?? 0) + (a.Quantity18 ?? 0) * (a.Cost18 ?? 0) + (a.Quantity19 ?? 0) * (a.Cost19 ?? 0) +
						   (a.Quantity20 ?? 0) * (a.Cost20 ?? 0) + (a.Quantity21 ?? 0) * (a.Cost21 ?? 0) + (a.Quantity22 ?? 0) * (a.Cost22 ?? 0) + (a.Quantity23 ?? 0) * (a.Cost23 ?? 0) +
						   (a.Quantity24 ?? 0) * (a.Cost24 ?? 0) + (a.Quantity25 ?? 0) * (a.Cost25 ?? 0) + (a.Quantity26 ?? 0) * (a.Cost26 ?? 0) + (a.Quantity27 ?? 0) * (a.Cost27 ?? 0) +
						   (a.Quantity28 ?? 0) * (a.Cost28 ?? 0) + (a.Quantity29 ?? 0) * (a.Cost29 ?? 0) + (a.Quantity30 ?? 0) * (a.Cost30 ?? 0);
					TotalCost += a.SalesCost;
					a.IsActive = true;
					db.ReceiptOrderItems.Add(a);
                    db.SaveChanges();
                    StockWarehouseTransaction transaction = new StockWarehouseTransaction();
                    transaction.Quantity01 = a.Quantity01;
                    transaction.Quantity02 = a.Quantity02;
                    transaction.Quantity03 = a.Quantity03;
                    transaction.Quantity04 = a.Quantity04;
                    transaction.Quantity05 = a.Quantity05;
                    transaction.Quantity06 = a.Quantity06;
                    transaction.Quantity07 = a.Quantity07;
                    transaction.Quantity08 = a.Quantity08;
                    transaction.Quantity09 = a.Quantity09;
                    transaction.Quantity10 = a.Quantity10;
                    transaction.Quantity11 = a.Quantity11;
                    transaction.Quantity12 = a.Quantity12;
                    transaction.Quantity13 = a.Quantity13;
                    transaction.Quantity14 = a.Quantity14;
                    transaction.Quantity15 = a.Quantity15;
                    transaction.Quantity16 = a.Quantity16;
                    transaction.Quantity17 = a.Quantity17;
                    transaction.Quantity18 = a.Quantity18;
                    transaction.Quantity19 = a.Quantity19;
                    transaction.Quantity20 = a.Quantity20;
                    transaction.Quantity21 = a.Quantity21;
                    transaction.Quantity22 = a.Quantity22;
                    transaction.Quantity23 = a.Quantity23;
                    transaction.Quantity24 = a.Quantity24;
                    transaction.Quantity25 = a.Quantity25;
                    transaction.Quantity26 = a.Quantity26;
                    transaction.Quantity27 = a.Quantity27;
                    transaction.Quantity28 = a.Quantity28;
                    transaction.Quantity29 = a.Quantity29;
                    transaction.Quantity30 = a.Quantity30;
                    transaction.PrimaryID = a.Id;
                    transaction.StockTransactionTypeId = 1;
                    transaction.TransactionReferenceID = db.TrasactionReferences.Where(x => x.Task == kkk).FirstOrDefault().Id;
                    transaction.ProductID = a.ProductId;
                    transaction.IsActive = true;
                    db.StockWarehouseTransactions.Add(transaction);
                    var stock = db.StockInventories.Where(x => x.IsActive == true && x.ProductID == a.ProductId).FirstOrDefault();
                    if (stock != null)
                    {
                        stock.Quantity01 = stock.Quantity01 + a.Quantity01 ?? 0;
                        stock.Quantity02 = stock.Quantity02 + a.Quantity02 ?? 0;
                        stock.Quantity03 = stock.Quantity03 + a.Quantity03 ?? 0;
                        stock.Quantity04 = stock.Quantity04 + a.Quantity04 ?? 0;
                        stock.Quantity05 = stock.Quantity05 + a.Quantity05 ?? 0;
                        stock.Quantity06 = stock.Quantity06 + a.Quantity06 ?? 0;
                        stock.Quantity07 = stock.Quantity07 + a.Quantity07 ?? 0;
                        stock.Quantity08 = stock.Quantity08 + a.Quantity08 ?? 0;
                        stock.Quantity09 = stock.Quantity09 + a.Quantity09 ?? 0;
                        stock.Quantity10 = stock.Quantity10 + a.Quantity10 ?? 0;
                        stock.Quantity11 = stock.Quantity11 + a.Quantity11 ?? 0;
                        stock.Quantity12 = stock.Quantity12 + a.Quantity12 ?? 0;
                        stock.Quantity13 = stock.Quantity13 + a.Quantity13 ?? 0;
                        stock.Quantity14 = stock.Quantity14 + a.Quantity14 ?? 0;
                        stock.Quantity15 = stock.Quantity15 + a.Quantity15 ?? 0;
                        stock.Quantity16 = stock.Quantity16 + a.Quantity16 ?? 0;
                        stock.Quantity17 = stock.Quantity17 + a.Quantity17 ?? 0;
                        stock.Quantity18 = stock.Quantity18 + a.Quantity18 ?? 0;
                        stock.Quantity19 = stock.Quantity19 + a.Quantity19 ?? 0;
                        stock.Quantity20 = stock.Quantity20 + a.Quantity20 ?? 0;
                        stock.Quantity21 = stock.Quantity21 + a.Quantity21 ?? 0;
                        stock.Quantity22 = stock.Quantity01 + a.Quantity22 ?? 0;
                        stock.Quantity23 = stock.Quantity01 + a.Quantity23 ?? 0;
                        stock.Quantity24 = stock.Quantity01 + a.Quantity24 ?? 0;
                        stock.Quantity25 = stock.Quantity01 + a.Quantity25 ?? 0;
                        stock.Quantity26 = stock.Quantity01 + a.Quantity26 ?? 0;
                        stock.Quantity27 = stock.Quantity01 + a.Quantity27 ?? 0;
                        stock.Quantity28 = stock.Quantity01 + a.Quantity28 ?? 0;
                        stock.Quantity29 = stock.Quantity01 + a.Quantity29 ?? 0;
                        stock.Quantity30 = stock.Quantity01 + a.Quantity30 ?? 0;
                        db.SaveChanges();
                    }
                    else
                    {
                        var model = new StockInventory();
                        model.Quantity01 = a.Quantity01 ?? 0;
                        model.Quantity02 = a.Quantity02 ?? 0;
                        model.Quantity03 = a.Quantity03 ?? 0;
                        model.Quantity04 = a.Quantity04 ?? 0;
                        model.Quantity05 = a.Quantity05 ?? 0;
                        model.Quantity06 = a.Quantity06 ?? 0;
                        model.Quantity07 = a.Quantity07 ?? 0;
                        model.Quantity08 = a.Quantity08 ?? 0;
                        model.Quantity09 = a.Quantity09 ?? 0;
                        model.Quantity10 = a.Quantity10 ?? 0;
                        model.Quantity11 = a.Quantity11 ?? 0;
                        model.Quantity12 = a.Quantity12 ?? 0;
                        model.Quantity13 = a.Quantity13 ?? 0;
                        model.Quantity14 = a.Quantity14 ?? 0;
                        model.Quantity15 = a.Quantity15 ?? 0;
                        model.Quantity16 = a.Quantity16 ?? 0;
                        model.Quantity17 = a.Quantity17 ?? 0;
                        model.Quantity18 = a.Quantity18 ?? 0;
                        model.Quantity19 = a.Quantity19 ?? 0;
                        model.Quantity20 = a.Quantity20 ?? 0;
                        model.Quantity21 = a.Quantity21 ?? 0;
                        model.Quantity22 = a.Quantity22 ?? 0;
                        model.Quantity23 = a.Quantity23 ?? 0;
                        model.Quantity24 = a.Quantity24 ?? 0;
                        model.Quantity25 = a.Quantity25 ?? 0;
                        model.Quantity26 = a.Quantity26 ?? 0;
                        model.Quantity27 = a.Quantity27 ?? 0;
                        model.Quantity28 = a.Quantity28 ?? 0;
                        model.Quantity29 = a.Quantity29 ?? 0;
                        model.Quantity30 = a.Quantity30 ?? 0;
                        model.IsActive = true;
                        model.ProductID = a.ProductId;
                        model.LogId = 2;
                        model.ColorID = 1;
                        model.BracketNumber = 1;
                        db.StockInventories.Add(model);
                    }
                    }
                }
			receipt.TotalQuantity = TotalQuantity;
			receipt.TotalCost = TotalCost;
			receipt.IsActive = true;
                db.SaveChanges();
            return Ok(true);
        }
		[HttpPost]
		[AllowAnonymous]
		[Route("EditReceiptOrder")]
		[ResponseType(typeof(ReceiveOrder))]
		public IHttpActionResult EditReceiveOrder1(int Id, ReceiveOrder receiveOrder)
		{
			var receipt = db.ReceiveOrders.Where(x => x.IsActive == true && x.Id == Id).Include(x=>x.ReceiptOrderItems).FirstOrDefault();
			receipt.SupplierInvoice = receiveOrder.SupplierInvoice;
			receipt.IsActive = true;
			int? TotalQuantity = 0;
			decimal? TotalCost = 0;
			foreach (var a in receiveOrder.ReceiptOrderItems)
			{
				if (a.ProductId != null)
				{
					var receiptUpdate = receipt.ReceiptOrderItems.Where(x => x.IsActive == true && x.ProductId == a.ProductId && x.Id == a.Id).FirstOrDefault();
			receiptUpdate.Quantity01 = a.Quantity01; receiptUpdate.Quantity02 = a.Quantity02; receiptUpdate.Quantity03 = a.Quantity03; receiptUpdate.Quantity04 = a.Quantity04; receiptUpdate.Quantity05 = a.Quantity05; receiptUpdate.Quantity06 = a.Quantity06; receiptUpdate.Quantity07 = a.Quantity07; receiptUpdate.Quantity08 = a.Quantity08; receiptUpdate.Quantity09 = a.Quantity09; receiptUpdate.Quantity10 = a.Quantity10; receiptUpdate.Quantity11 = a.Quantity11; receiptUpdate.Quantity12 = a.Quantity12; receiptUpdate.Quantity13 = a.Quantity13; receiptUpdate.Quantity14 = a.Quantity14; receiptUpdate.Quantity15 = a.Quantity15; receiptUpdate.Quantity16 = a.Quantity16; receiptUpdate.Quantity17 = a.Quantity17; receiptUpdate.Quantity18 = a.Quantity18; receiptUpdate.Quantity19 = a.Quantity19; receiptUpdate.Quantity20 = a.Quantity20; receiptUpdate.Quantity21 = a.Quantity21; receiptUpdate.Quantity22 = a.Quantity22; receiptUpdate.Quantity23 = a.Quantity23; receiptUpdate.Quantity24 = a.Quantity24; receiptUpdate.Quantity25 = a.Quantity25; receiptUpdate.Quantity26= a.Quantity26; receiptUpdate.Quantity27 = a.Quantity27; receiptUpdate.Quantity28 = a.Quantity28; receiptUpdate.Quantity29 = a.Quantity29; receiptUpdate.Quantity30 = a.Quantity30;
					 
					TotalQuantity += a.Quantity01 + a.Quantity02 + a.Quantity03 + a.Quantity04 + a.Quantity05 + a.Quantity06 + a.Quantity07 + a.Quantity08 + a.Quantity09 + a.Quantity10 + a.Quantity11 + a.Quantity12 + a.Quantity13 + a.Quantity14 + a.Quantity15 + a.Quantity16 + a.Quantity17 +
						a.Quantity18 + a.Quantity19 + a.Quantity20 + a.Quantity21 + a.Quantity22 + a.Quantity23 + a.Quantity24 + a.Quantity25 + a.Quantity26 + a.Quantity27 + a.Quantity28 + a.Quantity29 + a.Quantity30;

					a.SalesCost = (a.Quantity01 ?? 0) * (a.Cost01 ?? 0) + (a.Quantity02 ?? 0) * (a.Cost02 ?? 0) + (a.Quantity03 ?? 0) * (a.Cost03 ?? 0) +
						   (a.Quantity04 ?? 0) * (a.Cost04 ?? 0) + (a.Quantity05 ?? 0) * (a.Cost05 ?? 0) + (a.Quantity06 ?? 0) * (a.Cost06 ?? 0) +
						   (a.Quantity07 ?? 0) * (a.Cost07 ?? 0) + (a.Quantity08 ?? 0) * (a.Cost08 ?? 0) + (a.Quantity09 ?? 0) * (a.Cost09 ?? 0) + (a.Quantity10 ?? 0) * (a.Cost10 ?? 0) +
						   (a.Quantity11 ?? 0) * (a.Cost11 ?? 0) + (a.Quantity12 ?? 0) * (a.Cost12 ?? 0) + (a.Quantity13 ?? 0) * (a.Cost13 ?? 0) + (a.Quantity14 ?? 0) * (a.Cost15 ?? 0) +
						   (a.Quantity16 ?? 0) * (a.Cost16 ?? 0) + (a.Quantity17 ?? 0) * (a.Cost17 ?? 0) + (a.Quantity18 ?? 0) * (a.Cost18 ?? 0) + (a.Quantity19 ?? 0) * (a.Cost19 ?? 0) +
						   (a.Quantity20 ?? 0) * (a.Cost20 ?? 0) + (a.Quantity21 ?? 0) * (a.Cost21 ?? 0) + (a.Quantity22 ?? 0) * (a.Cost22 ?? 0) + (a.Quantity23 ?? 0) * (a.Cost23 ?? 0) +
						   (a.Quantity24 ?? 0) * (a.Cost24 ?? 0) + (a.Quantity25 ?? 0) * (a.Cost25 ?? 0) + (a.Quantity26 ?? 0) * (a.Cost26 ?? 0) + (a.Quantity27 ?? 0) * (a.Cost27 ?? 0) +
						   (a.Quantity28 ?? 0) * (a.Cost28 ?? 0) + (a.Quantity29 ?? 0) * (a.Cost29 ?? 0) + (a.Quantity30 ?? 0) * (a.Cost30 ?? 0);
					receiptUpdate.SalesCost = a.SalesCost;
					TotalCost += a.SalesCost;
					a.IsActive = true;
                    db.SaveChanges();
                }
			}
			receipt.TotalQuantity = TotalQuantity;
			receipt.TotalCost = TotalCost;
			receipt.IsActive = true;
			var receiptOrderItemList = receiveOrder.ReceiptOrderItems;
			foreach (var item in receiptOrderItemList)
			{
                if (item.ProductId != null)
                {
                    var stock = db.StockInventories.Where(x => x.IsActive == true && x.ProductID == item.ProductId).FirstOrDefault();
                    if (stock != null)
                    {
                        stock.Quantity01 = item.Quantity01 ?? 0;
                        stock.Quantity02 = item.Quantity02 ?? 0;
                        stock.Quantity03 = item.Quantity03 ?? 0;
                        stock.Quantity04 = item.Quantity04 ?? 0;
                        stock.Quantity05 = item.Quantity05 ?? 0;
                        stock.Quantity06 = item.Quantity06 ?? 0;
                        stock.Quantity07 = item.Quantity07 ?? 0;
                        stock.Quantity08 = item.Quantity08 ?? 0;
                        stock.Quantity09 = item.Quantity09 ?? 0;
                        stock.Quantity10 = item.Quantity10 ?? 0;
                        stock.Quantity11 = item.Quantity11 ?? 0;
                        stock.Quantity12 = item.Quantity12 ?? 0;
                        stock.Quantity13 = item.Quantity13 ?? 0;
                        stock.Quantity14 = item.Quantity14 ?? 0;
                        stock.Quantity15 = item.Quantity15 ?? 0;
                        stock.Quantity16 = item.Quantity16 ?? 0;
                        stock.Quantity17 = item.Quantity17 ?? 0;
                        stock.Quantity18 = item.Quantity18 ?? 0;
                        stock.Quantity19 = item.Quantity19 ?? 0;
                        stock.Quantity20 = item.Quantity20 ?? 0;
                        stock.Quantity21 = item.Quantity21 ?? 0;
                        stock.Quantity22 = item.Quantity22 ?? 0;
                        stock.Quantity23 = item.Quantity23 ?? 0;
                        stock.Quantity24 = item.Quantity24 ?? 0;
                        stock.Quantity25 = item.Quantity25 ?? 0;
                        stock.Quantity26 = item.Quantity26 ?? 0;
                        stock.Quantity27 = item.Quantity27 ?? 0;
                        stock.Quantity28 = item.Quantity28 ?? 0;
                        stock.Quantity29 = item.Quantity29 ?? 0;
                        stock.Quantity30 = item.Quantity30 ?? 0;
                        db.SaveChanges();
                    }
                    else
                    {
                        var model = new StockInventory();
                        model.Quantity01 = item.Quantity01 ?? 0;
                        model.Quantity02 = item.Quantity02 ?? 0;
                        model.Quantity03 = item.Quantity03 ?? 0;
                        model.Quantity04 = item.Quantity04 ?? 0;
                        model.Quantity05 = item.Quantity05 ?? 0;
                        model.Quantity06 = item.Quantity06 ?? 0;
                        model.Quantity07 = item.Quantity07 ?? 0;
                        model.Quantity08 = item.Quantity08 ?? 0;
                        model.Quantity09 = item.Quantity09 ?? 0;
                        model.Quantity10 = item.Quantity10 ?? 0;
                        model.Quantity11 = item.Quantity11 ?? 0;
                        model.Quantity12 = item.Quantity12 ?? 0;
                        model.Quantity13 = item.Quantity13 ?? 0;
                        model.Quantity14 = item.Quantity14 ?? 0;
                        model.Quantity15 = item.Quantity15 ?? 0;
                        model.Quantity16 = item.Quantity16 ?? 0;
                        model.Quantity17 = item.Quantity17 ?? 0;
                        model.Quantity18 = item.Quantity18 ?? 0;
                        model.Quantity19 = item.Quantity19 ?? 0;
                        model.Quantity20 = item.Quantity20 ?? 0;
                        model.Quantity21 = item.Quantity21 ?? 0;
                        model.Quantity22 = item.Quantity22 ?? 0;
                        model.Quantity23 = item.Quantity23 ?? 0;
                        model.Quantity24 = item.Quantity24 ?? 0;
                        model.Quantity25 = item.Quantity25 ?? 0;
                        model.Quantity26 = item.Quantity26 ?? 0;
                        model.Quantity27 = item.Quantity27 ?? 0;
                        model.Quantity28 = item.Quantity28 ?? 0;
                        model.Quantity29 = item.Quantity29 ?? 0;
                        model.Quantity30 = item.Quantity30 ?? 0;
                        model.IsActive = true;
                        model.ProductID = item.ProductId;
                        model.LogId = 2;
                        model.ColorID = 1;
                        model.BracketNumber = 1;
                        db.StockInventories.Add(model);
                        db.SaveChanges();
                    }
                }
			}
			
			return Ok(true);
		}
		[HttpPost]
        [AllowAnonymous]
        [Route("EditReceiptOrder22")]
        [ResponseType(typeof(ReceiveOrder))]
        public IHttpActionResult PostReceiveOrder1(int? Id)
        {
            var ReceiptOrderItemById = db.ReceiptOrderItems.Where(x => x.ReceiptOrderId == Id).ToList();
            int? TotalQuantity = 0;
            decimal? TotalCost = 0;
            foreach (var a in ReceiptOrderItemById)
            {
                TotalQuantity += a.Quantity01 + a.Quantity02 + a.Quantity03 + a.Quantity04 + a.Quantity05 + a.Quantity06 + a.Quantity07 + a.Quantity08 + a.Quantity09 + a.Quantity10 + a.Quantity11 + a.Quantity12 + a.Quantity13 + a.Quantity14 + a.Quantity15 + a.Quantity16 + a.Quantity17 +
                    a.Quantity18 + a.Quantity19 + a.Quantity20 + a.Quantity21 + a.Quantity22 + a.Quantity23 + a.Quantity24 + a.Quantity25 + a.Quantity26 + a.Quantity27 + a.Quantity28 + a.Quantity29 + a.Quantity30;

                TotalCost += a.Cost01 + a.Cost02 + a.Cost03 + a.Cost04 + a.Cost05 + a.Cost06 + a.Cost07 + a.Cost08 + a.Cost09 + a.Cost10 + a.Cost11 + a.Cost12 + a.Cost13 + a.Cost14 + a.Cost15 + a.Cost16 + a.Cost17 + a.Cost18 + a.Cost19 + a.Cost20 + a.Cost21 + a.Cost22 + a.Cost23 + a.Cost24 + a.Cost25 + a.Cost26 + a.Cost27 + a.Cost28 + a.Cost29 + a.Cost30; ;
            }
            var ReceiptOrderById = db.ReceiveOrders.Where(x => x.Id == Id).FirstOrDefault();
            ReceiptOrderById.TotalQuantity = TotalQuantity;
            ReceiptOrderById.TotalCost = TotalCost;
            ReceiptOrderById.IsActive = true;

            db.SaveChanges();
            return Ok(true);
        }
        // DELETE: api/ReceiveOrders/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(ReceiveOrder))]
        public IHttpActionResult DeleteReceiveOrder(int id)
        {
            ReceiveOrder receiveOrder = new ReceiveOrder();
			var status = false;
            receiveOrder = db.ReceiveOrders.Where(x =>x.IsActive==true && x.Id == id).FirstOrDefault();
			if (receiveOrder.IsFinalize == true)
			{
				status = false;
			}
			else
			{
				receiveOrder.IsActive = false;
				receiveOrder.ReceiptOrderItems = db.ReceiptOrderItems.Where(x => x.IsActive == true && x.ReceiptOrderId == id).ToList();
				foreach (var a in receiveOrder.ReceiptOrderItems)
				{
					a.IsActive = false;
                    var data = db.StockWarehouseTransactions.Where(x => x.IsActive == true && x.TransactionReferenceID == 1).FirstOrDefault();
                    data.StockTransactionTypeId = 3;
                    db.SaveChanges();
                }

			}
            
            return Ok(status);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceiveOrderExists(int id)
        {
            return db.ReceiveOrders.Count(e => e.Id == id) > 0;
        }
    }
}