﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
   public interface IReceiptOrderService
    {
        ReceiptOrderViewModel GetAll();
        List<ReceiveOrderModel> GetAllReceiptOrder(int? id);
        bool Create(ReceiveOrderModel model);
        bool AddReceiptOrderItem(ReceiptOrderItemModel model);
        bool AddReceiptOrder(ReceiveOrderModel model);
        List<ReceiptOrderItemModel> GetByReceiptOrderId(int? Id);
        bool Delete(int? Id);
        ReceiveOrderModel GetById(int? Id);
        ReceiptOrderItemModel GetUpdateValues(int? Id, int? productId);
        bool EditReceiptOrderItem(ReceiptOrderItemModel model);
        bool EditReceiptOrder(ReceiveOrderModel model);
        List<ReceiptOrderViewModel> SearchData(ReceiptSearch model);
        //List<ReceiptOrderItemModel> Create();
        ReceiptOrderItemModel GetReceiptItemById(int? id);
		ReceiveOrderModel ReceiptByPurchaseOrder(int PurchaseId);
		List<ReceiveOrderModel> GetAllReceipt();
        List<ReceiptOrderItemModel> ReceiptByProduct(int? id);
        List<ReceiptOrderItemModel> GetReceiptByProduct(int? id);
    }
}