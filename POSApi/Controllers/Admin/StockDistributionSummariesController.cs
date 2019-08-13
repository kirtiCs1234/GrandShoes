using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/stockDistributionSummaries")]
    public class StockDistributionSummariesController : ApiController
    {

        private GrandShoesEntities db = new GrandShoesEntities();
        public StockDistributionSummariesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/StockDistributionSummaries
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<StockDistributionSummary> GetStockDistributionSummaries()
        {
            var list = db.StockDistributionSummaries.Where(x=>x.IsActive==true);
            return list.ToList();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getAll")]
        public List<StockDistributionSummary> GetStockDistributionSummariesAll()
        {
            var list = db.StockDistributionSummaries.ToList();
            return list;
        }
        
        // GET: api/StockDistributionSummaries/5
        [ResponseType(typeof(StockDistributionSummary))]
        public IHttpActionResult GetStockDistributionSummary(int id)
        {
            StockDistributionSummary stockDistributionSummary = db.StockDistributionSummaries.Find(id);
            if (stockDistributionSummary == null)
            {
                return NotFound();
            }

            return Ok(stockDistributionSummary);
        }

        // PUT: api/StockDistributionSummaries/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockDistributionSummary(int id, StockDistributionSummary stock1)
        {
            var stockDistribution = db.StockDistributionSummaries.Where(s => s.Id == stock1.Id).FirstOrDefault();
            DateTime dNow = DateTime.Now;
            stockDistribution.DistributionNumber = 1;
            stockDistribution.DateClose = dNow;
            stockDistribution.UpdatedOn = System.DateTime.UtcNow;
            stockDistribution.IsActive = false;
            db.SaveChanges();
            return Ok(true);
        }

        // POST: api/StockDistributionSummaries
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(StockDistributionSummary))]
        public IHttpActionResult PostStockDistributionSummary(StockDistributionSummary stock)
        {
            StockDistributionSummary stockDistributionSummary = new StockDistributionSummary();
            stockDistributionSummary.Id = stock.Id;
            DateTime dNow = DateTime.Now;
            stockDistributionSummary.DateOpen =dNow;
            stockDistributionSummary.IsActive = true;
           
            stockDistributionSummary.LogId = 2;
            stockDistributionSummary.DistributionNumber = 1;
            stockDistributionSummary.CreatedOn = System.DateTime.UtcNow;
            stockDistributionSummary.UpdatedOn = System.DateTime.UtcNow;
          //  stockDistributionSummary.DistributionNumber = stock.DistributionNumber;
            db.StockDistributionSummaries.Add(stockDistributionSummary);
            db.SaveChanges();
            return Ok(true);
            
        }
       

        // DELETE: api/StockDistributionSummaries/5
        [ResponseType(typeof(StockDistributionSummary))]
        public IHttpActionResult DeleteStockDistributionSummary(int id)
        {
            StockDistributionSummary stockDistributionSummary = db.StockDistributionSummaries.Find(id);
            if (stockDistributionSummary == null)
            {
                return NotFound();
            }

            db.StockDistributionSummaries.Remove(stockDistributionSummary);
            db.SaveChanges();

            return Ok(stockDistributionSummary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockDistributionSummaryExists(int id)
        {
            return db.StockDistributionSummaries.Count(e => e.Id == id) > 0;
        }
    }
}