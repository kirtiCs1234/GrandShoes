using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
   public class ReceiptOrderService: IReceiptOrderService
    {
        public ReceiptOrderViewModel GetAll()
        {
            var list = ServerResponse.Invoke<ReceiptOrderViewModel>("api/PurchaseOrder/GetAllPurchaseOrder","","GET");
            return list;
        }
        public ReceiptOrderItemModel GetReceiptItemById(int? id)
        {
            return ServerResponse.Invoke<ReceiptOrderItemModel>("api/receiptOrderItems/getById?id=" + id, "", "GET");
        }
     //   public List<ReceiptOrderItemModel> Create()
      //  {
            //List<StockInventoryModel> result = new List<StockInventoryModel>();
            //var list = ServerResponse.Invoke<List<ReceiptOrderItemModel>>("api/receiptOrderItems/getDetails","","GET");
            //var group = list.ToList().GroupBy(m => new { m.ProductId });
            //foreach(var item in group)
            //{
            //    StockInventoryModel model = new StockInventoryModel();
            //    model.Quantity01 = item.Sum(x => x.Quantity01);
            //    model.Quantity02 = item.Sum(x => x.Quantity02);
            //    model.Quantity03 = item.Sum(x => x.Quantity03);
            //    model.Quantity04 = item.Sum(x => x.Quantity04);
            //    model.Quantity05 = item.Sum(x => x.Quantity05);
            //    model.Quantity06 = item.Sum(x=>x.Quantity06);
            //    model.Quantity07 = item.Sum(x => x.Quantity07);
            //    model.Quantity08 = item.Sum(x => x.Quantity08);
            //    model.Quantity09 = item.Sum(x => x.Quantity09);
            //    model.Quantity10 = item.Sum(x => x.Quantity10);
            //    model.Quantity11 = item.Sum(x => x.Quantity11);
            //    model.Quantity12 = item.Sum(x => x.Quantity12);
            //    model.Quantity13 = item.Sum(x => x.Quantity13);
            //    model.Quantity14 = item.Sum(x => x.Quantity14);
            //    model.Quantity15= item.Sum(x => x.Quantity15);
            //    model.Quantity16 = item.Sum(x => x.Quantity16);
            //    model.Quantity17 = item.Sum(x => x.Quantity17);
            //    model.Quantity18 = item.Sum(x => x.Quantity18);
            //    model.Quantity19 = item.Sum(x => x.Quantity19);
            //    model.Quantity20 = item.Sum(x => x.Quantity20);
            //    model.Quantity21 = item.Sum(x => x.Quantity21);
            //    model.Quantity22 = item.Sum(x => x.Quantity22);
            //    model.Quantity23 = item.Sum(x => x.Quantity23);
            //    model.Quantity24 = item.Sum(x => x.Quantity24);
            //    model.Quantity25 = item.Sum(x => x.Quantity25);
            //    model.Quantity26 = item.Sum(x => x.Quantity26);
            //    model.Quantity27 = item.Sum(x => x.Quantity27);
            //    model.Quantity28 = item.Sum(x => x.Quantity28);
            //    model.Quantity29 = item.Sum(x => x.Quantity29);
            //    model.Quantity30 = item.Sum(x => x.Quantity30);
            //    model.IsActive = true;
                
            //    model.ProductId = item.Select(x => x.ProductId).FirstOrDefault();
            //    model.ProductStyleId = item.Select(x => x.ProductStyleId).FirstOrDefault();
            //    result.Add(model);
        //        bool create = ServerResponse.Invoke<bool>("api/stockInventory/create", JsonConvert.SerializeObject(model), "POST");
            
        //    return list;
        //}
        public List<ReceiveOrderModel> GetAllReceiptOrder(int? id)
        {
            var list = ServerResponse.Invoke<List<ReceiveOrderModel>>("api/receiptOrder/getDetails?Id="+id, "", "GET");

            return list;
        }
        public List<ReceiptOrderViewModel> SearchData(ReceiptSearch model)
        {
            var list = ServerResponse.Invoke<List<ReceiptOrderViewModel>>("api/receiptOrder/search" ,JsonConvert.SerializeObject(model), "POST");

            return list;
        }
        public bool Create(ReceiveOrderModel model)
        {
           return ServerResponse.Invoke<bool>("/api/receiptOrder/create", JsonConvert.SerializeObject(model), "POST");
           
        }
        public bool AddReceiptOrderItem(ReceiptOrderItemModel model)
        {
            return ServerResponse.Invoke<bool>("/api/receiptOrderItems/create", JsonConvert.SerializeObject(model), "Post");
        }
        public bool EditReceiptOrderItem(ReceiptOrderItemModel model)
        {
            return ServerResponse.Invoke<bool>("/api/receiptOrderItems/edit?id="+model.Id, JsonConvert.SerializeObject(model), "Post");
        }
        public bool AddReceiptOrder(ReceiveOrderModel model)
        {
            return ServerResponse.Invoke<bool>("/api/receiptOrder/AddReceiptOrder?Id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
        }
        public bool EditReceiptOrder(ReceiveOrderModel model)
        {
			return ServerResponse.Invoke<bool>("/api/receiptOrder/EditReceiptOrder?Id=" + model.Id, JsonConvert.SerializeObject(model), "POST");
		}
        public List<ReceiptOrderItemModel> GetByReceiptOrderId(int? Id)
        {
            var list = ServerResponse.Invoke<List<ReceiptOrderItemModel>>("/api/receiptOrderItems/getDetailByReceiptOrderId?ReceiptOrderId=" + Id, "", "GET");
            return list;
        }
        public ReceiveOrderModel GetById(int? Id)
        {
            var GetById = ServerResponse.Invoke<ReceiveOrderModel>("api/receiptOrder/getById?id=" + Id, "", "GET");
            return GetById;
        }
        public bool Delete(int? Id)
        {
            var ReceiptOrdervalue = ServerResponse.Invoke<bool>("/api/receiptOrder/delete?id=" +Id, "", "Post");
            // var ReceiptOrderItemById=
            return ReceiptOrdervalue;
        }
		public ReceiveOrderModel ReceiptByPurchaseOrder(int PurchaseId)
		{
			var result = ServerResponse.Invoke<ReceiveOrderModel>("api/receiptOrder/getByPurchaseOrderId?id=" + PurchaseId, "", "GET");
			return result;
		}
        public ReceiptOrderItemModel GetUpdateValues(int? Id,int? productId)
        {
            var data = ServerResponse.Invoke<List<ReceiptOrderItemModel>>("/api/receiptOrderItems/getDetailById?ProductId=" + productId, "", "GET");
            //var data2 = data.FirstOrDefault();
            var ID = data[0].ReceiveOrder.PurchaseOrder.Id;
            var productID = data[0].Product.Id;
            //  var purchaseOrderList = ServerResponse.Invoke<List<PurchaseOrderItemModel>>("api/PurchaseOrderItems/getDetails","","GET");
            var data1 = ServerResponse.Invoke<PurchaseOrderItemModel>("api/PurchaseOrderItems/getData?Id=" + ID + "&&ProductId=" + productId, "", "GET");
           // PurchaseOrderItemModel model = new PurchaseOrderItemModel();
            ReceiptOrderItemModel data2 = new ReceiptOrderItemModel();
           
            data2.Quantity01 = data.Sum(x => x.Quantity01);
            data2.Quantity02 = data.Sum(x => x.Quantity02);
            data2.Quantity03 = data.Sum(x => x.Quantity03);
            data2.Quantity04 = data.Sum(x => x.Quantity04);
            data2.Quantity05 = data.Sum(x => x.Quantity05);
            data2.Quantity06 = data.Sum(x => x.Quantity06);
            data2.Quantity07 = data.Sum(x => x.Quantity07);
            data2.Quantity08 = data.Sum(x => x.Quantity08);
            data2.Quantity09 = data.Sum(x => x.Quantity09);
            data2.Quantity10 = data.Sum(x => x.Quantity10);
            data2.Quantity11 = data.Sum(x => x.Quantity11);
            data2.Quantity12 = data.Sum(x => x.Quantity12);
            data2.Quantity13 = data.Sum(x => x.Quantity13);
            data2.Quantity14 = data.Sum(x => x.Quantity14);
            data2.Quantity15 = data.Sum(x => x.Quantity15);
            data2.Quantity16 = data.Sum(x => x.Quantity16);
            data2.Quantity17 = data.Sum(x => x.Quantity17);
            data2.Quantity18 = data.Sum(x => x.Quantity18);
            data2.Quantity19 = data.Sum(x => x.Quantity19);
            data2.Quantity20 = data.Sum(x => x.Quantity20);
            data2.Quantity21 = data.Sum(x => x.Quantity21);
            data2.Quantity22 = data.Sum(x => x.Quantity22);
            data2.Quantity23 = data.Sum(x => x.Quantity23);
            data2.Quantity24 = data.Sum(x => x.Quantity24);
            data2.Quantity25 = data.Sum(x => x.Quantity25);
            data2.Quantity26 = data.Sum(x => x.Quantity26);
            data2.Quantity27 = data.Sum(x => x.Quantity27);
            data2.Quantity28 = data.Sum(x => x.Quantity28);
            data2.Quantity29 = data.Sum(x => x.Quantity29);
            data2.Quantity30 = data.Sum(x => x.Quantity30);
            
            data2.Quantity01 = data1.QuantitySize1 - data2.Quantity01;
            data2.Quantity02 = data1.QuantitySize2 - data2.Quantity02;
            data2.Quantity03 = data1.QuantitySize3 - data2.Quantity03;
            data2.Quantity04 = data1.QuantitySize4 - data2.Quantity04;
            data2.Quantity05 = data1.QuantitySize5 - data2.Quantity05;
            data2.Quantity06 = data1.QuantitySize6 - data2.Quantity06;
            data2.Quantity07 = data1.QuantitySize7 - data2.Quantity07;
            data2.Quantity08 = data1.QuantitySize8 - data2.Quantity08;
            data2.Quantity09 = data1.QuantitySize9 - data2.Quantity09;
            data2.Quantity10 = data1.QuantitySize10 - data2.Quantity10;
            data2.Quantity11 = data1.QuantitySize11 - data2.Quantity11;
            data2.Quantity12 = data1.QuantitySize12 - data2.Quantity12;
            data2.Quantity13 = data1.QuantitySize13 - data2.Quantity13;
            data2.Quantity14 = data1.QuantitySize14 - data2.Quantity14;
            data2.Quantity15 = data1.QuantitySize15 - data2.Quantity15;
            data2.Quantity16 = data1.QuantitySize16 - data2.Quantity16;
            data2.Quantity17 = data1.QuantitySize17 - data2.Quantity17;
            data2.Quantity18 = data1.QuantitySize18 - data2.Quantity18;
            data2.Quantity19 = data1.QuantitySize19 - data2.Quantity19;
            data2.Quantity20 = data1.QuantitySize20 - data2.Quantity20;
            data2.Quantity21 = data1.QuantitySize21 - data2.Quantity21;
            data2.Quantity22 = data1.QuantitySize22 - data2.Quantity22;
            data2.Quantity23 = data1.QuantitySize23 - data2.Quantity23;
            data2.Quantity24 = data1.QuantitySize24 - data2.Quantity24;
            data2.Quantity25 = data1.QuantitySize25 - data2.Quantity25;
            data2.Quantity26 = data1.QuantitySize26 - data2.Quantity26;
            data2.Quantity27 = data1.QuantitySize27 - data2.Quantity27;
            data2.Quantity28 = data1.QuantitySize28 - data2.Quantity28;
            data2.Quantity29 = data1.QuantitySize29 - data2.Quantity29;
            data2.Quantity30 = data1.QuantitySize30 - data2.Quantity30;
            data2.Cost01 = data1.CostSize1;
            data2.Cost02= data1.CostSize2;
            data2.Cost03 = data1.CostSize3;
            data2.Cost04 = data1.CostSize4;
            data2.Cost05 = data1.CostSize5;
            data2.Cost06 = data1.CostSize6;
            data2.Cost07 = data1.CostSize7;
            data2.Cost08 = data1.CostSize8;
            data2.Cost09 = data1.CostSize9;
            data2.Cost10 = data1.CostSize10;
            data2.Cost11 = data1.CostSize11;
            data2.Cost12 = data1.CostSize12;
            data2.Cost13 = data1.CostSize13;
            data2.Cost14 = data1.CostSize14;
            data2.Cost15 = data1.CostSize15;
            data2.Cost16 = data1.CostSize16;
            data2.Cost17 = data1.CostSize17;
            data2.Cost18 = data1.CostSize18;
            data2.Cost19 = data1.CostSize19;
            data2.Cost20 = data1.CostSize20;
            data2.Cost21 = data1.CostSize21;
            data2.Cost22 = data1.CostSize22;
            data2.Cost23 = data1.CostSize23;
            data2.Cost24 = data1.CostSize24;
            data2.Cost25 = data1.CostSize25;
            data2.Cost26 = data1.CostSize26;
            data2.Cost27 = data1.CostSize27;
            data2.Cost28 = data1.CostSize28;
            data2.Cost29 = data1.CostSize29;
            data2.Cost30 = data1.CostSize30;
            data2.autoCompleteProductName = data1.Product.ProductSKU;
            data2.autoCompleteProductStyleName = data1.Product.StyleSKU;
            return data2;
        }

           public List<ReceiveOrderModel> GetAllReceipt()
		{
			return ServerResponse.Invoke<List<ReceiveOrderModel>>("api/receiptOrder/getAll", "", "Get");
		}
        public List<ReceiptOrderItemModel> ReceiptByProduct(int? id)
        {
            return ServerResponse.Invoke<List<ReceiptOrderItemModel>>("api/receiptOrderItems/getByProduct?ProductID=" + id, "", "Get");
        }
    }
}
