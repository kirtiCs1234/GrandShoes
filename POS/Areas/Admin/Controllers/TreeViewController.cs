﻿using Helper;
using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.TreeView)]
    public class TreeViewController : BaseController
    {
        // GET: Admin/TreeView
        public ActionResult Index(int id)
        {
            List<ViewDistribution> data = new List<ViewDistribution>();
            // var StockSummary = Services.StockDistributionService.GetByProductId(id);
            var StockSummary = Services.StockDistributionSummaryService.GetAllid();
           foreach(var stock in StockSummary)
            {
                ViewDistribution viewDistribution = new ViewDistribution();
                viewDistribution.ID = stock.Id;
                viewDistribution.Name = stock.Id.ToString();
                viewDistribution.Pid = 0;
               // viewDistribution.TransactionDate = stock.DateOpen.ToString();
                data.Add(viewDistribution);
            }
            var StockList = Services.StockDistributionService.GetByProductId(id);
           //var StockList = StockDistribution.Where(x => x.ProductId == id).ToList();
           //var StockListBySummary=StockList.Where(x=>x.StockDistributionSummaryId==data.)
            foreach(var item in data)
            {
                
                var StockListByParent = StockList.Where(x => x.StockDistributionSummaryId == item.ID).ToList();
            foreach(var item1 in StockListByParent)
              {
                    ViewDistribution viewDistribution = new ViewDistribution();
                    viewDistribution.ID = item1.BranchId ?? default(int); 
                   viewDistribution.Name = item1.Branch.Name.ToString();
                    viewDistribution.Pid = item1.StockDistributionSummaryId;
                    viewDistribution.TransactionDate = item1.DistributionDate.ToString();
                  item.Childs.Add(viewDistribution);
             }
           }
            return View(data);
        }
      public JsonResult GetDetail(int? id)
        {
            var StockDistributionByBranch = Services.StockDistributionService.GetByBranchId(id);
            return Json(StockDistributionByBranch,JsonRequestBehavior.AllowGet);
        }
    }
}