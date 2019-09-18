﻿using Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.IService
{
    public interface IPurchaseOrderService
    {
        List<PurchaseOrderModel> GetAll(int? page);
        List<PurchaseOrderItemModel> ProductStyleAutocomplete(string name);
        List<PurchaseOrderItemModel> ProductAutocomplete(string name);
        List<PurchaseOrderModel> GetAll();
        List<PurchaseOrderModel> GetAllPurchaseOrder();
        int AddPurchaseOrder(PurchaseOrderModel order);
        bool DeletePurchaseOrder(int id);
        List<PurchaseOrderModel> GetByReceiptOrder();
        bool UpdatePurchaseOrder(PurchaseOrderModel order);
        PurchaseOrderModel GetPurchaseOrderById(int? id);
        List<PurchaseOrderStatus> GetPurchaseOrderStatus();
        bool IsOrderExist(string OrderNumber);
        List<PurchaseOrderModel> GetPurchaseSearchData(PurchaseOrderSearchModel search, int? page);
        List<PurchaseOrderModel> GetPurchaseOrderSearchData1(PurchaseOrderSearchModel areaSearch, int? page, out int TotalCount);
        List<PurchaseOrderModel> GetPaging1(int? page, out int TotalCount);
        List<PurchaseOrderModel> GetPaging(int? page, out int TotalCount);
        List<PurchaseOrderModel> GetSearchData1(PurchaseOrderSearchModel search, int? page, out int TotalCount);
		bool Finalize(int? id);
        PurchaseOrderModel Cancelled(int? id);
    }
}
