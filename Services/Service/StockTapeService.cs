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
   public class StockTapeService: IStockTapeService
    {
        public List<StockTapeModel> GetByBranchId(int? BranchId)
        {
            var StockTapeModelList = ServerResponse.Invoke<List<StockTapeModel>>("api/stockTape/getByBranchId?BranchId=" + BranchId, "", "GET");
            return StockTapeModelList;
        }
        public bool Create(StockTapeModel stock)
        {
            bool CreateStokeTape = ServerResponse.Invoke<bool>("api/stockTape/create", JsonConvert.SerializeObject(stock), "POST");
            return CreateStokeTape;
        }
        public bool ExcelUpload(StockTapeModel stockTape)
        {
            var body = JsonConvert.SerializeObject(stockTape);
            bool StockTapeCreate = ServerResponse.Invoke<bool>("api/stockTape/stockTapeExcelData", body, "Post");
            return StockTapeCreate;
        }
        public bool DeleteRecord(List<StockTapeModel> stockTapeList)
        {
            var body = JsonConvert.SerializeObject(stockTapeList);
           return ServerResponse.Invoke<bool>("api/stockTape/delete", body, "POST");
           
        }
        public List<StockVarianceModel> ShowVariance(int? BranchId)
        {
            var ShowStockVarianceList = new List<StockVarianceModel>();
            var StockTapeList = ServerResponse.Invoke<List<StockTapeModel>>("api/stockTape/getByBranchId?BranchId=" + BranchId, "", "GET");
            var StockBranchInventoryList = ServerResponse.Invoke<List<StockBranchInventoryModel>>("api/stockBranchInventory/getByBranchId?BranchId=" + BranchId, "", "GET");
            foreach(var a in StockTapeList)
            {
                var StockTape = new StockVarianceModel();
                StockTape.ProductId = a.ProductId;
                StockTape.Quantity01 = a.Quantity01;
                StockTape.Quantity02 = a.Quantity02;
                StockTape.Quantity03 = a.Quantity03;
                StockTape.Quantity04 = a.Quantity04;
                StockTape.Quantity05 = a.Quantity05;
                StockTape.Quantity06 = a.Quantity06;
                StockTape.Quantity07= a.Quantity07;
                StockTape.Quantity08 = a.Quantity08;
                StockTape.Quantity09 = a.Quantity09;
                StockTape.Quantity10 = a.Quantity10;
                StockTape.Quantity11 = a.Quantity11;
                StockTape.Quantity12 = a.Quantity12;
                StockTape.Quantity13 = a.Quantity13;
                StockTape.Quantity14 = a.Quantity14;
                StockTape.Quantity15 = a.Quantity15;
                StockTape.Quantity16 = a.Quantity16;
                StockTape.Quantity17 = a.Quantity17;
                StockTape.Quantity18 = a.Quantity18;
                StockTape.Quantity19 = a.Quantity19;
                StockTape.Quantity20 = a.Quantity20;
                StockTape.Quantity21 = a.Quantity21;
                StockTape.Quantity22 = a.Quantity22;
                StockTape.Quantity23 = a.Quantity23;
                StockTape.Quantity24 = a.Quantity24;
                StockTape.Quantity25 = a.Quantity25;
                StockTape.Quantity26 = a.Quantity26;
                StockTape.Quantity27 = a.Quantity27;
                StockTape.Quantity28 = a.Quantity28;
                StockTape.Quantity29 = a.Quantity29;
                StockTape.Quantity30 = a.Quantity30;
                StockTape.BranchId = a.BranchId;
                StockTape.Date = a.Date;
                StockTape.Barcode = a.Barcode;
                StockTape.IsActive = true;
                foreach(var b in StockBranchInventoryList)
                {
                    if(b.ProductId==StockTape.ProductId)
                    {
                        StockTape.Quantity01 = StockTape.Quantity01 - b.Quantity01;
                        StockTape.Quantity02 = StockTape.Quantity02 - b.Quantity02;
                        StockTape.Quantity03 = StockTape.Quantity03 - b.Quantity03;
                        StockTape.Quantity04 = StockTape.Quantity04 - b.Quantity04;
                        StockTape.Quantity05 = StockTape.Quantity05 - b.Quantity05;
                        StockTape.Quantity06 = StockTape.Quantity06 - b.Quantity06;
                        StockTape.Quantity07 = StockTape.Quantity07 - b.Quantity07;
                        StockTape.Quantity08 = StockTape.Quantity08 - b.Quantity08;
                        StockTape.Quantity09 = StockTape.Quantity09 - b.Quantity09;
                        StockTape.Quantity10 = StockTape.Quantity10 - b.Quantity10;
                        StockTape.Quantity11 = StockTape.Quantity11 - b.Quantity11;
                        StockTape.Quantity12 = StockTape.Quantity12 - b.Quantity12;
                        StockTape.Quantity13 = StockTape.Quantity13 - b.Quantity13;
                        StockTape.Quantity14 = StockTape.Quantity14 - b.Quantity14;
                        StockTape.Quantity15 = StockTape.Quantity15 - b.Quantity15;
                        StockTape.Quantity16 = StockTape.Quantity16 - b.Quantity16;
                        StockTape.Quantity17 = StockTape.Quantity17 - b.Quantity17;
                        StockTape.Quantity18 = StockTape.Quantity18 - b.Quantity18;
                        StockTape.Quantity19 = StockTape.Quantity19 - b.Quantity19;
                        StockTape.Quantity20 = StockTape.Quantity20 - b.Quantity20;
                        StockTape.Quantity21 = StockTape.Quantity21 - b.Quantity21;
                        StockTape.Quantity22 = StockTape.Quantity22 - b.Quantity22;
                        StockTape.Quantity23 = StockTape.Quantity23 - b.Quantity23;
                        StockTape.Quantity24 = StockTape.Quantity24 - b.Quantity24;
                        StockTape.Quantity25 = StockTape.Quantity25 - b.Quantity25;
                        StockTape.Quantity26 = StockTape.Quantity26 - b.Quantity26;
                        StockTape.Quantity27 = StockTape.Quantity27 - b.Quantity27;
                        StockTape.Quantity28 = StockTape.Quantity28 - b.Quantity28;
                        StockTape.Quantity29 = StockTape.Quantity29 - b.Quantity29;
                        StockTape.Quantity30 = StockTape.Quantity30 - b.Quantity30;

                    }else
                    {
                        StockTape.Quantity01 = StockTape.Quantity01??0 - b.Quantity01;
                        StockTape.Quantity02 = StockTape.Quantity02??0 - b.Quantity02;
                        StockTape.Quantity03 = StockTape.Quantity03??0 - b.Quantity03;
                        StockTape.Quantity04 = StockTape.Quantity04??0 - b.Quantity04;
                        StockTape.Quantity05 = StockTape.Quantity05 - b.Quantity05;
                        StockTape.Quantity06 = StockTape.Quantity06 - b.Quantity06;
                        StockTape.Quantity07 = StockTape.Quantity07 - b.Quantity07;
                        StockTape.Quantity08 = StockTape.Quantity08 - b.Quantity08;
                        StockTape.Quantity09 = StockTape.Quantity09 - b.Quantity09;
                        StockTape.Quantity10 = StockTape.Quantity10 - b.Quantity10;
                        StockTape.Quantity11 = StockTape.Quantity11 - b.Quantity11;
                        StockTape.Quantity12 = StockTape.Quantity12 - b.Quantity12;
                        StockTape.Quantity13 = StockTape.Quantity13 - b.Quantity13;
                        StockTape.Quantity14 = StockTape.Quantity14 - b.Quantity14;
                        StockTape.Quantity15 = StockTape.Quantity15 - b.Quantity15;
                        StockTape.Quantity16 = StockTape.Quantity16 - b.Quantity16;
                        StockTape.Quantity17 = StockTape.Quantity17 - b.Quantity17;
                        StockTape.Quantity18 = StockTape.Quantity18 - b.Quantity18;
                        StockTape.Quantity19 = StockTape.Quantity19 - b.Quantity19;
                        StockTape.Quantity20 = StockTape.Quantity20 - b.Quantity20;
                        StockTape.Quantity21 = StockTape.Quantity21 - b.Quantity21;
                        StockTape.Quantity22 = StockTape.Quantity22 - b.Quantity22;
                        StockTape.Quantity23 = StockTape.Quantity23 - b.Quantity23;
                        StockTape.Quantity24 = StockTape.Quantity24 - b.Quantity24;
                        StockTape.Quantity25 = StockTape.Quantity25 - b.Quantity25;
                        StockTape.Quantity26 = StockTape.Quantity26 - b.Quantity26;
                        StockTape.Quantity27 = StockTape.Quantity27 - b.Quantity27;
                        StockTape.Quantity28 = StockTape.Quantity28 - b.Quantity28;
                        StockTape.Quantity29 = StockTape.Quantity29 - b.Quantity29;
                        StockTape.Quantity30 = StockTape.Quantity30 - b.Quantity30;
                    }

                }
                ShowStockVarianceList.Add(StockTape);

            }
            return ShowStockVarianceList;

        }
    }
}
