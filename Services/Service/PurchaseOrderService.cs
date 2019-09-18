using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Helper;
using Newtonsoft.Json;
using Model;
using PagedList;

namespace Services.Service
{
    class PurchaseOrderService : IPurchaseOrderService
    {
        public List<PurchaseOrderModel> GetAll()
        {
            return ServerResponse.Invoke<List<PurchaseOrderModel>>("api/PurchaseOrder/getDetails", "", "GET");
        }
        public List<PurchaseOrderModel> GetPaging(int? page, out int TotalCount)
        {
            var body = "";

            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<PurchaseOrderModel>>>("api/PurchaseOrder/getAreaPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }
		
		public List<PurchaseOrderModel> GetSearchData1(PurchaseOrderSearchModel search, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(search);
            var result = ServerResponse.Invoke<ServiceResult<List<PurchaseOrderModel>>>("api/PurchaseOrder/getPurchaseSearchDataForReport", body, "Post");
            
            TotalCount = result.TotalCount;

            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {
            }
            return result.data.ToList();
        }
		public bool Finalize(int? id)
		{
			return ServerResponse.Invoke<bool>("api/PurchaseOrder/finalizeOrder?id=" + id, "", "GET");
		}


		public List<PurchaseOrderModel> GetAll(int? page)
        {
            return ServerResponse.Invoke<List<PurchaseOrderModel>>("api/PurchaseOrder/getDetails", "", "GET");
        }
        public int AddPurchaseOrder(PurchaseOrderModel order)
        {
            return ServerResponse.Invoke<int>("/api/PurchaseOrder/create", JsonConvert.SerializeObject(order), "post");
        }

        public bool DeletePurchaseOrder(int id)
        {
            return ServerResponse.Invoke<bool>("/api/PurchaseOrder/DeletePurchaseOrder?id=" + id, "", "post");
        }

        public List<PurchaseOrderModel> GetAllPurchaseOrder()
        {
            return ServerResponse.Invoke<List<PurchaseOrderModel>>("/api/PurchaseOrder/GetAllPurchaseOrder", "", "get");
        }

        public PurchaseOrderModel GetPurchaseOrderById(int? id)
        {
            return ServerResponse.Invoke<Model.PurchaseOrderModel>("/api/PurchaseOrder/GetPurchaseOrderById?id=" + id, "", "get");
        }

        public List<PurchaseOrderStatus> GetPurchaseOrderStatus()

        {
            return ServerResponse.Invoke<List<PurchaseOrderStatus>>("/api/PurchaseOrder/GetAllPurchaseOrderStatus", "", "get");
        }

        public bool UpdatePurchaseOrder(PurchaseOrderModel order)
        {
            return ServerResponse.Invoke<bool>("/api/PurchaseOrder/edit", JsonConvert.SerializeObject(order), "post");
        }

        public bool IsOrderExist(string OrderNumber)
        {
            OrderNumber = System.Web.HttpUtility.UrlEncode(OrderNumber);
            return ServerResponse.Invoke<bool>("/api/PurchaseOrder/isOrderNumberexist?OrderNumber=" + OrderNumber, "", "Get");
        }
        public List<PurchaseOrderModel> GetPaging1(int? page, out int TotalCount)
        {
            var body = "";
            int pageNumber = (page ?? 1);
            var result = ServerResponse.Invoke<ServiceResult<List<PurchaseOrderModel>>>("api/PurchaseOrder/getAreaPaging?pageNumber=" + page, body, "GET");
            TotalCount = result.TotalCount;
            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {

            }
            return result.data.ToList();
        }

        public List<PurchaseOrderModel> GetPurchaseOrderSearchData1(PurchaseOrderSearchModel areaSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(areaSearch);
            var result = ServerResponse.Invoke<ServiceResult<List<PurchaseOrderModel>>>("api/PurchaseOrder/getPurchaseSearchDataForReport", body, "Post");
            TotalCount = result.TotalCount;

            if (result.data != null)
            {
                var model = result.data.ToList();
                return model;
            }
            else
            {
            }
            return result.data.ToList();
        }
        public List<PurchaseOrderModel> GetPurchaseSearchData(PurchaseOrderSearchModel search, int? page)
        {
            
            var body = JsonConvert.SerializeObject(search);
            var result = ServerResponse.Invoke<List<PurchaseOrderModel>>("api/PurchaseOrder/getPurchaseSearchData", body, "POST");
            
            return result;
        }
        public List<PurchaseOrderModel> GetByReceiptOrder()
        {
            List<PurchaseOrderModel> PurchaseOrderUpdateList = new List<PurchaseOrderModel>();
           // IPagedList<PurchaseOrderModel> purchaseList=null;
            var puchaseOrderList=ServerResponse.Invoke<List<PurchaseOrderModel>>("api/PurchaseOrder/getDetailsByFinal", "", "GET");
			if (puchaseOrderList.Count > 0)
			{
				var receiveOrderList = ServerResponse.Invoke<List<ReceiveOrderModel>>("api/receiptOrder/getAll", "", "GET");
				
					foreach (var a in puchaseOrderList)
					{
						PurchaseOrderModel model = new PurchaseOrderModel();
						var receiptByPruchaseOrder = receiveOrderList.Where(x => x.PurchaseOrderId == a.Id).ToList();
						if (receiptByPruchaseOrder.Count != 0)
						{
							decimal? TotalCost = 0;
							int? TotalQuantity = 0;
							foreach (var b in receiptByPruchaseOrder)
							{
								TotalQuantity += b.TotalQuantity;
								TotalCost += b.TotalCost;
							}
							model.Id = a.Id;
							model.Amount = a.Amount - TotalCost;
							model.BuyerId = a.BuyerId;
							model.ClientInvoiceNumber = a.ClientInvoiceNumber;
							model.ExpectedDeliveryDate = a.ExpectedDeliveryDate;
							model.FirstDeliveryDate = a.FirstDeliveryDate;
							model.FromDate = a.FromDate;
							model.IsActive = a.IsActive;
							model.OrderCompletionDate = a.OrderCompletionDate;
							model.OrderDate = a.OrderDate;
							model.OrderedItems = a.OrderedItems;
							model.OrderNumber = a.OrderNumber;
							model.PurchaseOrderStatusId = a.PurchaseOrderStatusId;
							model.Quantity = a.Quantity - TotalQuantity;
							model.SupplierId = a.SupplierId;
							// model.SupplierName = a.SupplierName;
							model.ToDate = a.ToDate;
							model.VatAmount = a.VatAmount;
							model.SupplierName = a.Supplier.Name;
                        model.PurchaseOrderStatu = puchaseOrderList.Where(x => x.Id == a.Id).FirstOrDefault().PurchaseOrderStatu;

                        }
						else
						{
							model.Id = a.Id;
							model.Amount = a.Amount;
							model.BuyerId = a.BuyerId;
							model.ClientInvoiceNumber = a.ClientInvoiceNumber;
							model.ExpectedDeliveryDate = a.ExpectedDeliveryDate;
							model.FirstDeliveryDate = a.FirstDeliveryDate;
							model.FromDate = a.FromDate;
							model.IsActive = a.IsActive;
							model.OrderCompletionDate = a.OrderCompletionDate;
							model.OrderDate = a.OrderDate;
							model.OrderedItems = a.OrderedItems;
							model.OrderNumber = a.OrderNumber;
							model.PurchaseOrderStatusId = a.PurchaseOrderStatusId;
							model.Quantity = a.Quantity;
							model.SupplierId = a.SupplierId;
							model.SupplierName = a.SupplierName;
							model.ToDate = a.ToDate;
							model.VatAmount = a.VatAmount;
							model.SupplierName = a.Supplier.Name;
                        model.PurchaseOrderStatu = puchaseOrderList.Where(x => x.Id == a.Id).FirstOrDefault().PurchaseOrderStatu;
                    }
						PurchaseOrderUpdateList.Add(model);
					
				}
			}
            return PurchaseOrderUpdateList;
        }
        public List<PurchaseOrderItemModel> ProductAutocomplete(string name)
        {
            return ServerResponse.Invoke<List<PurchaseOrderItemModel>>("api/PurchaseOrderItems/ProductsAutocomplete?name=" + name, "", "get");
        }
        public List<PurchaseOrderItemModel> ProductStyleAutocomplete(string name)
        {
            name = System.Web.HttpUtility.UrlEncode(name);
            return ServerResponse.Invoke<List<PurchaseOrderItemModel>>("api/PurchaseOrderItems/ProductsStyleAutocomplete?name=" + name, "", "get");
        }
       public PurchaseOrderModel Cancelled(int? id)
        {
            return ServerResponse.Invoke<PurchaseOrderModel>("api/PurchaseOrder/Cancelled?id=" + id, "", "GET");
        }
    }
}


