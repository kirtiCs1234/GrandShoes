using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/salesOrderItems")]
    public class SalesOrderItemsController : ApiController
    {
        GrandShoesEntities db = new GrandShoesEntities();
        public SalesOrderItemsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpPost]
        [Route("getWinnerList")]
        public IHttpActionResult GetWinnerList(WinnerReportModel winner)
        {
            var list = db.SalesOrderItems.Where(m => m.IsActive == true).Include(m => m.SalesOrder).Include(m => m.Product);
            if (string.IsNullOrEmpty(winner.BranchCode))
            {
                list = list.Where(x => x.SalesOrder.Branch.BranchCode.Contains(winner.BranchCode));
            }
            if (winner.IsMarkDown == true)
            {
                list = list.Where(x => x.Product.IsMarkDown == true);
            }
            if (winner.FromReportDate != null && winner.ToReportDate != null)
            {
                var FromDate = Convert.ToDateTime(winner.FromReportDate).Date;
                var ToDate = Convert.ToDateTime(winner.ToReportDate).AddDays(1).Date;
                list = list.Where(k => k.SalesOrder.TransactionDate >= FromDate && k.SalesOrder.TransactionDate < ToDate);
            }
            if(winner.FromDistributionDate!=null && winner.ToDistributionDate != null)
            {
                var FromDate = Convert.ToDateTime(winner.FromDistributionDate).Date;
                var ToDate = Convert.ToDateTime(winner.ToDistributionDate).AddDays(1).Date;
                list = list.Where(k => k.SalesOrder.TransactionDate >= FromDate && k.SalesOrder.TransactionDate < ToDate);
            }
            return Ok(list.ToList());
        }
    }
}
