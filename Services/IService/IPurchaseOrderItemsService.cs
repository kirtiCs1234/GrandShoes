﻿using Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IPurchaseOrderItemsService
    {
        List<PurchaseOrderItemModel> GetAllPurchaseOrderItems();
        int AddPurchaseOrderItems(PurchaseOrderItemModel orderItems);
		int DeletePurchaseOrderItems(int id);
        int UpdatePurchaseOrderItems(PurchaseOrderItemModel orderItems);
        PurchaseOrderItemModel GetPurchaseOrderItemsById(int id);
        List<PurchaseOrderItemModel> GetItemsByPurchaseOrderId(int PurchaseOrderId);
        PurchaseOrderItemModel GetValue(PurchaseOrderSearch model);
        List<PurchaseOrderItemModel> GetItemByOrderId(int id);
		List<PurchaseOrderItemModel> GetItemsByPurchase(int PurchaseOrderId);
		PurchaseOrderItemModel GetById(int id);
		bool CheckProduct(PurchaseOrderItemModel purchaseOrderItem);
		bool CheckProductByOrder(PurchaseOrderItemModel model);
        List<PurchaseOrderItemModel> GetByProductId(int? id);
        List<DictModel> GetDictList(int? id);
    }
}
