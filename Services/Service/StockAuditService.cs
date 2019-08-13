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
    public class StockAuditService: IStockAuditService
    {
        public List<StockAuditModel> GetByBranchId(int? BranchId)
        {
            var StockAuditByBranchList = ServerResponse.Invoke<List<StockAuditModel>>("api/stockAudit/getByBranch?BranchId=" + BranchId, "", "GET");
            return StockAuditByBranchList;
        }
        public StockAuditModel Create(StockAuditModel audit)
        {
            var create = ServerResponse.Invoke<StockAuditModel>("api/stockAudit/create", JsonConvert.SerializeObject(audit), "POST");
            return create;
        }
       public StockAuditModel DeleteRecord(StockAuditModel stockAudit)
        {
            var body = JsonConvert.SerializeObject(stockAudit);
            var delete = ServerResponse.Invoke<StockAuditModel>("api/stockAudit/delete?id=" + stockAudit.Id, body, "POST");
            return delete;
        }
        public List<StockVarianceModel> ShowVariance(int? BranchId)
        {
            var ShowStockVarianceList = new List<StockVarianceModel>();
            var StockAuditList = ServerResponse.Invoke<List<StockAuditModel>>("api/stockAudit/getByBranch?BranchId=" + BranchId, "", "GET");
            var StockBranchInventoryList = ServerResponse.Invoke<List<StockBranchInventoryModel>>("api/stockBranchInventory/getByBranchId?BranchId=" + BranchId, "", "GET");
            foreach (var a in StockAuditList)
            {
                var StockAudit = new StockVarianceModel();
                StockAudit.ProductId = a.ProductId;
                StockAudit.Quantity01 = a.Quantity01;
                StockAudit.Quantity02 = a.Quantity02;
                StockAudit.Quantity03 = a.Quantity03;
                StockAudit.Quantity04 = a.Quantity04;
                StockAudit.Quantity05 = a.Quantity05;
                StockAudit.Quantity06 = a.Quantity06;
                StockAudit.Quantity07 = a.Quantity07;
                StockAudit.Quantity08 = a.Quantity08;
                StockAudit.Quantity09 = a.Quantity09;
                StockAudit.Quantity10 = a.Quantity10;
                StockAudit.Quantity11 = a.Quantity11;
                StockAudit.Quantity12 = a.Quantity12;
                StockAudit.Quantity13 = a.Quantity13;
                StockAudit.Quantity14 = a.Quantity14;
                StockAudit.Quantity15 = a.Quantity15;
                StockAudit.Quantity16 = a.Quantity16;
                StockAudit.Quantity17 = a.Quantity17;
                StockAudit.Quantity18 = a.Quantity18;
                StockAudit.Quantity19 = a.Quantity19;
                StockAudit.Quantity20 = a.Quantity20;
                StockAudit.Quantity21 = a.Quantity21;
                StockAudit.Quantity22 = a.Quantity22;
                StockAudit.Quantity23 = a.Quantity23;
                StockAudit.Quantity24 = a.Quantity24;
                StockAudit.Quantity25 = a.Quantity25;
                StockAudit.Quantity26 = a.Quantity26;
                StockAudit.Quantity27 = a.Quantity27;
                StockAudit.Quantity28 = a.Quantity28;
                StockAudit.Quantity29 = a.Quantity29;
                StockAudit.Quantity30 = a.Quantity30;
                StockAudit.BranchId = a.BranchId;
                StockAudit.Date = a.Date;
                StockAudit.Barcode = a.Barcode;
                StockAudit.IsActive = true;
                foreach (var b in StockBranchInventoryList)
                {
                    if (b.ProductId == StockAudit.ProductId)
                    {
                        StockAudit.Quantity01 = StockAudit.Quantity01 - b.Quantity01;
                        StockAudit.Quantity02 = StockAudit.Quantity02- b.Quantity02;
                        StockAudit.Quantity03 = StockAudit.Quantity03 - b.Quantity03;
                        StockAudit.Quantity04 = StockAudit.Quantity04 - b.Quantity04;
                        StockAudit.Quantity05 = StockAudit.Quantity05 - b.Quantity05;
                        StockAudit.Quantity06 = StockAudit.Quantity06 - b.Quantity06;
                        StockAudit.Quantity07 = StockAudit.Quantity07 - b.Quantity07;
                        StockAudit.Quantity08 = StockAudit.Quantity08 - b.Quantity08;
                        StockAudit.Quantity09 = StockAudit.Quantity09 - b.Quantity09;
                        StockAudit.Quantity10 = StockAudit.Quantity10 - b.Quantity10;
                        StockAudit.Quantity11 = StockAudit.Quantity11 - b.Quantity11;
                        StockAudit.Quantity12 = StockAudit.Quantity12 - b.Quantity12;
                        StockAudit.Quantity13 = StockAudit.Quantity13 - b.Quantity13;
                        StockAudit.Quantity14 = StockAudit.Quantity14 - b.Quantity14;
                        StockAudit.Quantity15 = StockAudit.Quantity15 - b.Quantity15;
                        StockAudit.Quantity16 = StockAudit.Quantity16 - b.Quantity16;
                        StockAudit.Quantity17 = StockAudit.Quantity17 - b.Quantity17;
                        StockAudit.Quantity18 = StockAudit.Quantity18 - b.Quantity18;
                        StockAudit.Quantity19 = StockAudit.Quantity19 - b.Quantity19;
                        StockAudit.Quantity20 = StockAudit.Quantity20 - b.Quantity20;
                        StockAudit.Quantity21 = StockAudit.Quantity21 - b.Quantity21;
                        StockAudit.Quantity22 = StockAudit.Quantity22 - b.Quantity22;
                        StockAudit.Quantity23 = StockAudit.Quantity23 - b.Quantity23;
                        StockAudit.Quantity24 = StockAudit.Quantity24 - b.Quantity24;
                        StockAudit.Quantity25 = StockAudit.Quantity25 - b.Quantity25;
                        StockAudit.Quantity26 = StockAudit.Quantity26 - b.Quantity26;
                        StockAudit.Quantity27 = StockAudit.Quantity27 - b.Quantity27;
                        StockAudit.Quantity28 = StockAudit.Quantity28 - b.Quantity28;
                        StockAudit.Quantity29 = StockAudit.Quantity29 - b.Quantity29;
                        StockAudit.Quantity30 = StockAudit.Quantity30 - b.Quantity30;
                       
                    }

                }
                ShowStockVarianceList.Add(StockAudit);

            }
            return ShowStockVarianceList;

        }
    }
}
