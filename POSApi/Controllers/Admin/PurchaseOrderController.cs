﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using DAL;
using Model;
using Helper.ExtensionMethod;
using Newtonsoft.Json;
using System.Data.Entity.Infrastructure;
using Helper;

namespace POSApi.Controllers
{
    [RoutePrefix("api/PurchaseOrder")]
    public class PurchaseOrderController : ApiController
    {
        private GrandShoesEntities Entities = new GrandShoesEntities();
        public PurchaseOrderController()
        {
            Entities.Configuration.LazyLoadingEnabled = false;
            Entities.Configuration.ProxyCreationEnabled = false;
        }
        [HttpPost]
        [Route("getPurchaseSearchData")]
        public ServiceResult<List<PurchaseOrder>> GetSearchPurchaseOrder(PurchaseOrderSearchModel search)
        {
            ServiceResult<List<PurchaseOrder>> model = new ServiceResult<List<PurchaseOrder>>();

            var list = Entities.PurchaseOrders.Where(x => x.IsActive == true).OrderByDescending(x=>x.ID).Include(x=>x.Supplier).Include(x=>x.Buyer).Include(x=>x.PurchaseOrderStatu);
            if (search != null)
            {
                if (search.IsActive == false)
                {
                    list = Entities.PurchaseOrders.Where(x => x.IsActive == false).OrderByDescending(x=>x.ID);
                }
                if (!string.IsNullOrEmpty(search.OrderNumber))
                {
                    list = list.Where(x => x.OrderNumber.ToLower().Contains(search.OrderNumber.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.SupplierName))
                {
                    list = list.Where(x => x.Supplier.Name.ToLower().Contains(search.SupplierName.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.BuyerName))
                {
                    list = list.Where(x => x.Buyer.Name.ToLower().Contains(search.BuyerName.ToLower()));
                }
                
            }
            
            var pageSize = 10;
            var source = list.ToList();
            int count = source.Count();
            var items = source.OrderByDescending(m => m.ID).Skip(((search.Page ?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            model.data = items.Select(x => new PurchaseOrder
            {
                ID = x.ID,
                OrderNumber = x.OrderNumber,
                ClientInvoiceNumber = x.ClientInvoiceNumber,
                OrderDate = x.OrderDate,
                ExpectedDeliveryDate = x.ExpectedDeliveryDate,
                FirstDeliveryDate = x.FirstDeliveryDate,
                OrderCompletionDate = x.OrderCompletionDate,
                Quantity = x.Quantity,
                Amount = x.Amount,
                VatAmount = x.VatAmount,
                PurchaseOrderStatusId = x.PurchaseOrderStatusId,
                BuyerId = x.BuyerId,
                SupplierId = x.SupplierId,
                Buyer = Entities.Buyers.Where(m => m.IsActive == true && m.Id==x.BuyerId).FirstOrDefault(),
                Supplier=Entities.Suppliers.Where(m=>m.IsActive==true && m.Id==x.SupplierId).FirstOrDefault()
               // BuyerName=Entities.Buyers.Where(m=>m.Id==BuyerId)

            }).ToList();
            model.TotalCount = count;
            return model;
        }
		[HttpGet]
		[Route("finalizeOrder")]
		public IHttpActionResult FinalOrder(int id)
		{
			var order = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == id).FirstOrDefault();
			order.IsFinalize = true;
            order.PurchaseOrderStatusId= 3;
			Entities.SaveChanges();
			return Ok(true);
		}
        [HttpPost]
        [Route("getPurchaseSearchDataForReport")]
        public ServiceResult<List<PurchaseOrder>> GetSearchPurchaseOrder1(PurchaseOrderSearchModel search)
        {
            ServiceResult<List<PurchaseOrder>> model = new ServiceResult<List<PurchaseOrder>>();

            var list = Entities.PurchaseOrders.Where(x => x.IsActive == true).OrderByDescending(x => x.ID).Include(x => x.Supplier).Include(x => x.Buyer).Include(x => x.PurchaseOrderStatu);
            if (search != null)
            {
                if (search.IsActive == false)
                {
                    list = Entities.PurchaseOrders.Where(x => x.IsActive == false).OrderByDescending(x => x.ID);
                }
                if (search.OrderNumber1 != null)
                {
                    list = list.Where(x => x.OrderNumber == search.OrderNumber1);
                }
                if (search.OrderNumber != null )
                {
                    list = list.Where(x => x.OrderNumber == search.OrderNumber);
                }
                if (search.PurchaseOrderStatusId>0)
                {
                    list = list.Where(x => x.PurchaseOrderStatusId==search.PurchaseOrderStatusId);
                }
               
                if (search.FromDate != null && search.ToDateS!=null)
                {
                     list = Entities.PurchaseOrders.Where(entry => entry.OrderDate >= search.FromDate
                   && entry.OrderDate <= search.ToDateS); 
                }

            }

            var pageSize = 10;
            var source = list.ToList();
            int count = source.Count();
            var items = source.OrderByDescending(m => m.ID).Skip(((search.Page ?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList().RemoveReferences();
            //model.data = items.Select(x => new PurchaseOrder
            //{
            //    ID = x.ID,
            //    OrderNumber = x.OrderNumber,
            //    ClientInvoiceNumber = x.ClientInvoiceNumber,
            //    OrderDate = x.OrderDate,
            //    ExpectedDeliveryDate = x.ExpectedDeliveryDate,
            //    FirstDeliveryDate = x.FirstDeliveryDate,
            //    OrderCompletionDate = x.OrderCompletionDate,
            //    Quantity = x.Quantity,
            //    Amount = x.Amount,
            //    VatAmount = x.VatAmount,
            //    PurchaseOrderStatusId = x.PurchaseOrderStatusId,
            //    BuyerId = x.BuyerId,
            //    SupplierId = x.SupplierId,
            //    Buyer = Entities.Buyers.Where(m => m.IsActive == true && m.Id == x.BuyerId).FirstOrDefault(),
            //    Supplier = Entities.Suppliers.Where(m => m.IsActive == true && m.Id == x.SupplierId).FirstOrDefault(),
            //    // BuyerName=Entities.Buyers.Where(m=>m.Id==BuyerId)
            //   PurchaseOrderStatu=x.PurchaseOrderStatu,

            //}).ToList();
            model.data = items;
            model.TotalCount = count;
            return model;
        }
        // GET: All PurchaseOrder
        [Route("GetAllPurchaseOrder")]
        public IHttpActionResult GetAllPurchaseOrder()
        {
            List<Model.PurchaseOrderModel> AllOrders = new List<Model.PurchaseOrderModel>();
            var OrderList = Entities.PurchaseOrders.Where(s => s.IsActive == true).Include(x=>x.Supplier).ToList().Select(m => new PurchaseOrder
            {
                ID= m.ID,
                OrderNumber = m.OrderNumber,
                ClientInvoiceNumber = m.ClientInvoiceNumber,
                SupplierId = m.SupplierId,
                BuyerId = m.BuyerId ,
                OrderDate=m.OrderDate,
                ExpectedDeliveryDate=m.ExpectedDeliveryDate ,
                FirstDeliveryDate=m.FirstDeliveryDate,
                OrderCompletionDate=m.OrderCompletionDate,
                Quantity =m.Quantity,
                Amount =m.Amount,
                VatAmount=m.VatAmount,
                PurchaseOrderStatusId =m.PurchaseOrderStatusId,
                IsActive=m.IsActive
                
            }).ToList();
            return Ok(OrderList);
        }
        [HttpGet]
        [Route("getDetails")]
        public List<PurchaseOrder> GetAll()
        {
            var List = Entities.PurchaseOrders.Where(x => x.IsActive == true).OrderByDescending(x=>x.ID).Include(x=>x.Supplier).Include(x=>x.Buyer);
            return List.ToList();
        }
		[HttpGet]
		[Route("getDetailsByFinal")]
		public List<PurchaseOrder> GetAllByFinal()
		{
			var List = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.IsFinalize==true && x.PurchaseOrderStatusId!=4).OrderByDescending(x => x.ID).Include(x => x.Supplier).Include(x=>x.PurchaseOrderStatu).Include(x => x.Buyer);
			return List.ToList().RemoveReferences();
		}
		[HttpGet]
        [AllowAnonymous]
        [Route("getAreaPaging")]
        public ServiceResult<List<PurchaseOrder>> GetArea([FromUri]Paging paging)
        {
            ServiceResult<List<PurchaseOrder>> model = new ServiceResult<List<PurchaseOrder>>();
            var source = Entities.PurchaseOrders.Where(x => x.IsActive == true)
                        .Include(x => x.PurchaseOrderStatu).Include(x=>x.Supplier).Include(x=>x.Buyer).OrderByDescending(x => x.ID).ToList().RemoveReferences();
            int count = source.Count();
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = paging.pageNumber;
            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = paging.pageSize;
            // Display TotalCount to Records to User  
            int TotalCount = count;
            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            model.TotalCount = count;
            model.data = items;
            model.pageSize = PageSize;
            return model;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<PurchaseOrder>> GetSearchData(PurchaseOrderSearchModel search)
        {
            var pageSize = 10;
            var CurrentPage = 1;
            ServiceResult<List<PurchaseOrder>> model = new ServiceResult<List<PurchaseOrder>>();
            var source = Entities.PurchaseOrders.Where(x => x.IsActive == true).Include(x => x.Buyer).Include(x => x.PurchaseOrderStatu).Include(x=>x.Supplier);
           
            int count = source.Count();
            var items = source.OrderByDescending(m => m.ID).Skip(((search.Page?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            model.data = items.Select(x => new PurchaseOrder
            {
                ID = x.ID,
                OrderNumber = x.OrderNumber,
                SupplierId = x.SupplierId,
                BuyerId= x.BuyerId         
            }).ToList().RemoveReferences();
            model.TotalCount = count;
            return model; ;
        }
        //Add AddPurchaseOrder
        [Route("create")]
        public IHttpActionResult AddPurchaseOrder(PurchaseOrder order)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = Entities.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			DAL.PurchaseOrder Orders = new DAL.PurchaseOrder();
            Orders.OrderNumber = order.OrderNumber;
            Orders.ClientInvoiceNumber = order.ClientInvoiceNumber;
            Orders.SupplierId = order.SupplierId;
            Orders.BuyerId = order.BuyerId;
            Orders.OrderDate = order.OrderDate;
            Orders.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
            Orders.FirstDeliveryDate = order.FirstDeliveryDate;
            Orders.OrderCompletionDate = order.OrderCompletionDate;
            Orders.Quantity = 0;
            Orders.Amount = 0;
            Orders.VatAmount = order.VatAmount;
            Orders.PurchaseOrderStatusId = order.PurchaseOrderStatusId;
            Orders.IsActive =  true;
            Entities.PurchaseOrders.Add(Orders);
			try
			{
				Entities.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(Orders, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = Orders.CreateLog(pageName, logTable, UserId);
				}
			}
			int id = Orders.ID;
            return Ok(id);
        }

        //GetPurchaseOrderById
        [Route("GetPurchaseOrderById")]
        public IHttpActionResult GetPurchaseOrderById(int id)
        {
            var list = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == id).Include(x=>x.Buyer).Include(x=>x.Supplier).FirstOrDefault();   
            return Ok(list);
        }

        //Delete PurchaseOrder
        [HttpPost]
        [Route("DeletePurchaseOrder")]
        public IHttpActionResult DeletePurchaseOrder(int id)
        {
            DAL.PurchaseOrder order = new DAL.PurchaseOrder();
            order = Entities.PurchaseOrders.Where(s => s.ID == id).FirstOrDefault();
            order.IsActive = false;
            Entities.SaveChanges();
            return Ok(true);
        }

        //Update PurchaseOrder
        [HttpPost]
        [Route("edit")]
        public IHttpActionResult UpdatePurchaseOrder(PurchaseOrder order)
        {
			var list = new List<PurchaseOrder>();
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = Entities.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			DAL.PurchaseOrder Orders = new DAL.PurchaseOrder();
            Orders = Entities.PurchaseOrders.Where(s => s.ID == order.ID).FirstOrDefault();
            Orders.OrderNumber = order.OrderNumber;
            Orders.ClientInvoiceNumber = order.ClientInvoiceNumber;
            Orders.SupplierId = order.SupplierId;
            Orders.BuyerId = order.BuyerId;
            Orders.OrderDate = order.OrderDate;
            Orders.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
            Orders.FirstDeliveryDate = order.FirstDeliveryDate;
            Orders.OrderCompletionDate = order.OrderCompletionDate;
            Orders.Quantity = order.Quantity;
            Orders.Amount = order.Amount??0;
            Orders.VatAmount = order.VatAmount;
            Orders.PurchaseOrderStatusId = order.PurchaseOrderStatusId;
            Orders.IsActive = true;
			try
			{
				Entities.SaveChanges();
				list.Add(Orders);
				return Ok(true);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				obj = ex;
			}

			finally
			{
				if (obj == null)
				{
					logTable = "";
					var change = list.ToDataTables().getChangedRecords();
					var c = change.Count() / 2;
					for (var i = 0; i < c; i++)
					{
						logTable += change[i].Fieldname + " Old Value=[" + change[i].FieldValue + "] New Value=[" + change[i + c].FieldValue + "], ";
					}
					//logTable = Newtonsoft.Json.JsonConvert.SerializeObject(change);
					var flag = order.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
        }

        //Get All PurchaseOrderStatus
        [Route("GetAllPurchaseOrderStatus")]
        public IHttpActionResult GetAllPurchaseOrderStatus()
        {
            var OrderStatusList = Entities.PurchaseOrderStatus.Where(s => s.IsActive == true).ToList().Select(m => new Model.PurchaseOrderStatus
            {
                Id = m.Id,
                OrderStatus = m.OrderStatus,
                IsActive=m.IsActive ?? true,
            }).ToList();
            return Ok(OrderStatusList);
        }

        //Checking Existing order Number
        [HttpGet]
        [Route("isOrderNumberexist")]
        public IHttpActionResult isOrderNumberexist(string OrderNumber)
        {
            var order = Entities.PurchaseOrders.Any(s => s.IsActive == true && s.OrderNumber == OrderNumber);
            if (order)
                return Ok(true);
            else
                return Ok(false);
        }
        [HttpGet]
        [Route("Cancelled")]
        public IHttpActionResult Cancelled(int id)
        {
            var order = Entities.PurchaseOrders.Where(x => x.IsActive == true && x.ID == id).FirstOrDefault();
            order.PurchaseOrderStatusId = 4;
            Entities.SaveChanges();
            return Ok(order);
        }
        
    }
}