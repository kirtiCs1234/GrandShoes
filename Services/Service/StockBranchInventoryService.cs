﻿using Helper;
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
   public class StockBranchInventoryService: IStockBranchInventoryService
    {
        public List<StockBranchInventoryModel> GetAll()
        {
            var StockBranchModelList = ServerResponse.Invoke<List<StockBranchInventoryModel>>("api/stockBranchInventory/getDetails", "", "GET");
            return StockBranchModelList;
        }
        public List<StockBranchInventoryModel> GetByBranch(int? BranchId)
        {
            var StockBranchInventoryByBranch = ServerResponse.Invoke<List<StockBranchInventoryModel>>("api/stockBranchInventory/getByBranchId?BranchId=" + BranchId, "", "GET");
            return StockBranchInventoryByBranch;
        }
        public bool Update(int? BranchId,List<StockBranchInventoryModel> stockList)
        {
            var body = JsonConvert.SerializeObject(stockList);
            return ServerResponse.Invoke<bool>("api/stockBranchInventory/update?BranchId="+BranchId, body, "POST");
        }
        public List<StockBranchInventoryModel> GetByProduct(int? id)
        {
            return ServerResponse.Invoke<List<StockBranchInventoryModel>>("api/stockBranchInventory/getByProduct?ProductId=" + id, "", "GET");
        }
        public List<StockBranchInventoryModel> GetBranchSales(int? id)
        {
            return ServerResponse.Invoke<List<StockBranchInventoryModel>>("api/stockBranchInventory/getBranchSales?id=" + id, "", "GET");
        }
    }
}
