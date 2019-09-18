using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Helper;
using Newtonsoft.Json;
using Model;

namespace Services.Service
{
   public class PurchaseOrderItemsService : IPurchaseOrderItemsService
    {
        public int AddPurchaseOrderItems(PurchaseOrderItemModel orderItems)
        {
            return ServerResponse.Invoke<int>("/api/PurchaseOrderItems/AddPurchaseOrderItems", JsonConvert.SerializeObject(orderItems), "post");
        }

        public int DeletePurchaseOrderItems(int id)
        {
            return ServerResponse.Invoke<int>("/api/PurchaseOrderItems/DeletePurchaseOrderItems?id=" + id, "", "post");
        }
		public bool CheckProductByOrder(PurchaseOrderItemModel model)
		{
			return ServerResponse.Invoke<bool>("api/PurchaseOrderItems/checkProductByOrder", JsonConvert.SerializeObject(model), "POST");
		}
		public bool CheckProduct(PurchaseOrderItemModel purchaseOrderItem)
		{
			return ServerResponse.Invoke<bool>("api/PurchaseOrderItems/checkProduct", JsonConvert.SerializeObject(purchaseOrderItem), "POST");
		}

		public List<PurchaseOrderItemModel> GetAllPurchaseOrderItems()
        {
            return ServerResponse.Invoke<List<PurchaseOrderItemModel>>("/api/PurchaseOrderItems/GetAllPurchaseOrderItems", "", "get");
        }

        public List<PurchaseOrderItemModel> GetItemsByPurchaseOrderId(int PurchaseOrderId)
        {
            return ServerResponse.Invoke<List<PurchaseOrderItemModel>>("/api/PurchaseOrderItems/getByOrderId?Id =" + PurchaseOrderId, "", "get");
        }

		public List<PurchaseOrderItemModel> GetItemsByPurchase(int PurchaseOrderId)
		{
			return ServerResponse.Invoke<List<PurchaseOrderItemModel>>("/api/PurchaseOrderItems/getByOrderIdReceipt?id=" + PurchaseOrderId, "", "get");
		}
		public PurchaseOrderItemModel GetPurchaseOrderItemsById(int id)
        {
            return ServerResponse.Invoke<PurchaseOrderItemModel>("/api/PurchaseOrderItems/GetPurchaseOrderItemsById?id=" + id, "", "get");
        }
		public PurchaseOrderItemModel GetById(int id)
		{
			return ServerResponse.Invoke<PurchaseOrderItemModel>("/api/PurchaseOrderItems/GetItemById?id=" + id, "", "get");
		}
		public List<PurchaseOrderItemModel> GetItemByOrderId(int id)
        {
            var list= ServerResponse.Invoke<List<PurchaseOrderItemModel>>("/api/PurchaseOrderItems/getByOrderId?Id=" + id, "", "get");
            return list;
        }
        public int UpdatePurchaseOrderItems(PurchaseOrderItemModel orderItems)
        {
            return ServerResponse.Invoke<int>("/api/PurchaseOrderItems/UpdatePurchaseOrderItems", JsonConvert.SerializeObject(orderItems), "post");
        }
        public PurchaseOrderItemModel GetValue(PurchaseOrderSearch model)
        {
            
           var purchaseOrderItemList= ServerResponse.Invoke<List<PurchaseOrderItemModel>>("/api/PurchaseOrderItems/getValue", JsonConvert.SerializeObject(model), "Post");
            var purchaseOrderItem = purchaseOrderItemList.FirstOrDefault();
            
            var reciptOrderList = ServerResponse.Invoke<List<ReceiveOrderModel>>("/api/receiptOrder/GetAll", "", "GET");
            var receiptOrderByPurchaseOrderId = reciptOrderList.Where(x => x.PurchaseOrderId == model.PurchaseOrderId);
            
            var receiptOrderItemList = ServerResponse.Invoke<List<ReceiptOrderItemModel>>("/api/receiptOrderItems/getDetails", "", "GET");
            var receiptOrderItemList1 = receiptOrderItemList.Where(x => x.IsActive==true && x.Product.ProductSKU == model.autoCompleteProductName && x.Product.StyleSKU == model.autoCompleteProductStyleName).ToList();
            PurchaseOrderItemModel purchase = new PurchaseOrderItemModel();

            int? Quantity01 = 0;
            int? Quantity02 = 0;
            int? Quantity03 = 0;
            int? Quantity04 = 0;
            int? Quantity05 = 0;
            int? Quantity06 = 0;
            int? Quantity07 = 0;
            int? Quantity08 = 0;
            int? Quantity09 = 0;
            int? Quantity10 = 0;
            int? Quantity11 = 0;
            int? Quantity12 = 0;
            int? Quantity13 = 0;
            int? Quantity14 = 0;
            int? Quantity15 = 0;
            int? Quantity16 = 0;
            int? Quantity17 = 0;
            int? Quantity18 = 0;
            int? Quantity19 = 0;
            int? Quantity20 = 0;
            int? Quantity21 = 0;
            int? Quantity22 = 0;
            int? Quantity23 = 0;
            int? Quantity24 = 0;
            int? Quantity25 = 0;
            int? Quantity26 = 0;
            int? Quantity27 = 0;
            int? Quantity28 = 0;
            int? Quantity29 = 0;
            int? Quantity30 = 0;
            if (purchaseOrderItem != null)
            {
                foreach (var b in receiptOrderByPurchaseOrderId)
                {

                    var receiptOrderItemListById = receiptOrderItemList1.Where(x => x.ReceiptOrderId == b.Id).ToList();
                    if (receiptOrderItemListById.Count != 0)
                    {
                        foreach (var a in receiptOrderItemListById)
                        {
                            Quantity01 += a.Quantity01;
                            Quantity02 += a.Quantity02;
                            Quantity03 += a.Quantity03;
                            Quantity04 += a.Quantity04;
                            Quantity05 += a.Quantity05;
                            Quantity06 += a.Quantity06;
                            Quantity07 += a.Quantity07;
                            Quantity08 += a.Quantity08;
                            Quantity09 += a.Quantity09;
                            Quantity10 += a.Quantity10;
                            Quantity11 += a.Quantity11;
                            Quantity12 += a.Quantity12;
                            Quantity13 += a.Quantity13;
                            Quantity14 += a.Quantity14;
                            Quantity15 += a.Quantity15;
                            Quantity16 += a.Quantity16;
                            Quantity17 += a.Quantity17;
                            Quantity18 += a.Quantity18;
                            Quantity19 += a.Quantity19;
                            Quantity20 += a.Quantity20;
                            Quantity21 += a.Quantity21;
                            Quantity22 += a.Quantity22;
                            Quantity23 += a.Quantity23;
                            Quantity24 += a.Quantity24;
                            Quantity25 += a.Quantity25;
                            Quantity26 += a.Quantity26;
                            Quantity27 += a.Quantity27;
                            Quantity28 += a.Quantity28;
                            Quantity29 += a.Quantity29;
                            Quantity30 += a.Quantity30;
                        }
                    }
                    purchase.QuantitySize1 = purchaseOrderItem.QuantitySize1 - Quantity01;
                    purchase.QuantitySize2 = purchaseOrderItem.QuantitySize2 - Quantity02;
                    purchase.QuantitySize3 = purchaseOrderItem.QuantitySize3 - Quantity03;
                    purchase.QuantitySize4 = purchaseOrderItem.QuantitySize4 - Quantity04;
                    purchase.QuantitySize5 = purchaseOrderItem.QuantitySize5 - Quantity05;
                    purchase.QuantitySize6 = purchaseOrderItem.QuantitySize6 - Quantity06;
                    purchase.QuantitySize7 = purchaseOrderItem.QuantitySize7 - Quantity07;
                    purchase.QuantitySize8 = purchaseOrderItem.QuantitySize8 - Quantity08;
                    purchase.QuantitySize9 = purchaseOrderItem.QuantitySize9 - Quantity09;
                    purchase.QuantitySize10 = purchaseOrderItem.QuantitySize10 - Quantity10;
                    purchase.QuantitySize11 = purchaseOrderItem.QuantitySize11 - Quantity11;
                    purchase.QuantitySize12 = purchaseOrderItem.QuantitySize12 - Quantity12;
                    purchase.QuantitySize13 = purchaseOrderItem.QuantitySize13 - Quantity13;
                    purchase.QuantitySize14 = purchaseOrderItem.QuantitySize14 - Quantity14;
                    purchase.QuantitySize15 = purchaseOrderItem.QuantitySize15 - Quantity15;
                    purchase.QuantitySize16 = purchaseOrderItem.QuantitySize16 - Quantity16;
                    purchase.QuantitySize17 = purchaseOrderItem.QuantitySize17 - Quantity17;
                    purchase.QuantitySize18 = purchaseOrderItem.QuantitySize18 - Quantity18;
                    purchase.QuantitySize19 = purchaseOrderItem.QuantitySize19 - Quantity19;
                    purchase.QuantitySize20 = purchaseOrderItem.QuantitySize20 - Quantity20;
                    purchase.QuantitySize21 = purchaseOrderItem.QuantitySize21 - Quantity21;
                    purchase.QuantitySize22 = purchaseOrderItem.QuantitySize22 - Quantity22;
                    purchase.QuantitySize23 = purchaseOrderItem.QuantitySize23 - Quantity23;
                    purchase.QuantitySize24 = purchaseOrderItem.QuantitySize24 - Quantity24;
                    purchase.QuantitySize25 = purchaseOrderItem.QuantitySize25 - Quantity25;
                    purchase.QuantitySize26 = purchaseOrderItem.QuantitySize26 - Quantity26;
                    purchase.QuantitySize27 = purchaseOrderItem.QuantitySize27 - Quantity27;
                    purchase.QuantitySize28 = purchaseOrderItem.QuantitySize28 - Quantity28;
                    purchase.QuantitySize29 = purchaseOrderItem.QuantitySize29 - Quantity29;
                    purchase.QuantitySize30 = purchaseOrderItem.QuantitySize30 - Quantity30;
                    purchase.Amount = purchaseOrderItem.Amount;
                    purchase.ColorId = purchaseOrderItem.ColorId;
                    purchase.CostSize1 = purchaseOrderItem.CostSize1;
                    purchase.CostSize2 = purchaseOrderItem.CostSize2;
                    purchase.CostSize3 = purchaseOrderItem.CostSize3;
                    purchase.CostSize4 = purchaseOrderItem.CostSize4;
                    purchase.CostSize5 = purchaseOrderItem.CostSize5;
                    purchase.CostSize6 = purchaseOrderItem.CostSize6;
                    purchase.CostSize7 = purchaseOrderItem.CostSize7;
                    purchase.CostSize8 = purchaseOrderItem.CostSize8;
                    purchase.CostSize9 = purchaseOrderItem.CostSize9;
                    purchase.CostSize10 = purchaseOrderItem.CostSize10;
                    purchase.CostSize11 = purchaseOrderItem.CostSize11;
                    purchase.CostSize12 = purchaseOrderItem.CostSize12;
                    purchase.CostSize13 = purchaseOrderItem.CostSize13;
                    purchase.CostSize14 = purchaseOrderItem.CostSize14;
                    purchase.CostSize15 = purchaseOrderItem.CostSize15;
                    purchase.CostSize16 = purchaseOrderItem.CostSize16;
                    purchase.CostSize17 = purchaseOrderItem.CostSize17;
                    purchase.CostSize18 = purchaseOrderItem.CostSize18;
                    purchase.CostSize19 = purchaseOrderItem.CostSize19;
                    purchase.CostSize20 = purchaseOrderItem.CostSize20;
                    purchase.CostSize21 = purchaseOrderItem.CostSize21;
                    purchase.CostSize22 = purchaseOrderItem.CostSize22;
                    purchase.CostSize23 = purchaseOrderItem.CostSize23;
                    purchase.CostSize24 = purchaseOrderItem.CostSize24;
                    purchase.CostSize25 = purchaseOrderItem.CostSize25;
                    purchase.CostSize26 = purchaseOrderItem.CostSize26;
                    purchase.CostSize27 = purchaseOrderItem.CostSize27;
                    purchase.CostSize28 = purchaseOrderItem.CostSize28;
                    purchase.CostSize29 = purchaseOrderItem.CostSize29;
                    purchase.CostSize30 = purchaseOrderItem.CostSize30;
                    purchase.ID = purchaseOrderItem.ID;
                    purchase.IsActive = purchaseOrderItem.IsActive;
                    purchase.ItemSize1 = purchaseOrderItem.ItemSize1;
                }
            }
            return purchase;
        }
        public List<PurchaseOrderItemModel> GetByProductId(int? id)
        {
            return ServerResponse.Invoke<List<PurchaseOrderItemModel>>("api/PurchaseOrderItems/getByProductId?ProductID=" + id, "", "GET");
        }
        public List<DictModel> GetDictList(int? id)
        {
            return ServerResponse.Invoke<List<DictModel>>("api/PurchaseOrderItems/GetItemList?PurchaseOrderId=" + id, "", "GET");
        }
    }
}
