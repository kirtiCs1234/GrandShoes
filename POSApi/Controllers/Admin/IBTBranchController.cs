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
    [RoutePrefix("api/ibtBranch")]
    public class IBTBranchController : ApiController
    {
        GrandShoesEntities db = new GrandShoesEntities();
        public IBTBranchController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpPost]
        [Route("getDetails")]
        public IHttpActionResult GetDetails(WinnerReportModel winner)
        {
            var list = db.IBTDetails.Where(x => x.IsActive == true).Include(x=>x.IBTBranch).Include(x=>x.Product).ToList();
            if (list.Count > 0)
            {
                if (winner.FromReportDate != null && winner.ToReportDate != null)
                {
                    var FromDate = Convert.ToDateTime(winner.FromReportDate).Date;
                    var ToDate = Convert.ToDateTime(winner.ToReportDate).AddDays(1).Date;
                    list = list.Where(k => k.IBTBranch.DateReceive >= FromDate && k.IBTBranch.DateReceive < ToDate).ToList();
                }
                if (winner.FromDistributionDate != null && winner.ToDistributionDate != null)
                {
                    var FromDate = Convert.ToDateTime(winner.FromDistributionDate).Date;
                    var ToDate = Convert.ToDateTime(winner.ToDistributionDate).AddDays(1).Date;
                    list = list.Where(k => k.IBTBranch.DateReceive >= FromDate && k.IBTBranch.DateReceive < ToDate).ToList();
                }
            }
            return Ok(list);
        }
    }
}
