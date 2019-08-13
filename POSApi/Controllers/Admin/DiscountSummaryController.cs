using DAL;
using Helper.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/discountSummary")]
    public class DiscountSummaryController : ApiController
    {
        GrandShoesEntities db = new GrandShoesEntities();
        public DiscountSummaryController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [Route("getDetails")]
        public IHttpActionResult GetDetails()
        {
            var list = db.DiscountSummaries.Where(x=>x.IsActive==true).ToList();
            return Ok(list.RemoveReferences());
        }
        [HttpGet]
        [Route("getDetail")]
        public IHttpActionResult GetDetail(int id)
        {
            var data = db.DiscountSummaries.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            return Ok(data);
        }
        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(DiscountSummary model)
        {
            var data = db.DiscountSummaries.Where(x => x.IsActive == true && x.Id ==model.Id).FirstOrDefault();
            data.IsActive = false;
            foreach(var item in data.PromotionalDiscounts)
            {
                item.IsActive = false;
            }
            db.SaveChanges();
            return Ok(true);
        }
        [HttpGet]
        [Route("getPromotional")]
        public IHttpActionResult GetData(int? id)
        {
            var list = db.PromotionalDiscounts.Where(x => x.IsActive == true && x.DiscountSummaryID == id)
                                                .Include(x => x.Product)
                                                .Include(x => x.DiscountSummary)
                                                .Include(x => x.DiscountBranches)
                                                .Include(x => x.DiscountBranches.Select(m => m.Branch))
                                                .ToList();
                                               // .RemoveReferences();
            return Ok(list.RemoveReferences());
        }
    }
}
