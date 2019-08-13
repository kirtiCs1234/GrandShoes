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
using Model;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/stockBranchInventory")]
    public class StockBranchInventoriesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StockBranchInventoriesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        // GET: api/StockBranchInventories
        public List<StockBranchInventory> GetStockBranchInventories()
        {
            var list= db.StockBranchInventories.Where(x=>x.IsActive==true);
            return list.ToList();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getByBranchId")]
        // GET: api/StockBranchInventories/5
        [ResponseType(typeof(StockBranchInventory))]
        public List<StockBranchInventory> GetStockBranchInventory(int? BranchId)
        {
            var data = db.StockBranchInventories.Where(x =>x.IsActive==true && x.BranchId == BranchId).ToList();
            return data;
        }
        [HttpGet]
        [Route("getByProduct")]
        public IHttpActionResult GetByProduct(int? ProductId)
        {
            var data = db.StockBranchInventories.Where(x => x.IsActive == true && x.ProductId == ProductId).ToList();
            return Ok(data);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("update")]
        // PUT: api/StockBranchInventories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockBranchInventory(int? BranchId, List<StockBranchInventoryModel> stockBranchInventoryList)
        {
			foreach (var stockBranchInventory in stockBranchInventoryList)
			{
				var data = db.StockBranchInventories.Where(x => x.Id == stockBranchInventory.Id && x.BranchId == BranchId).FirstOrDefault();
				data.Quantity01 = stockBranchInventory.Quantity01;
				data.Quantity02 = stockBranchInventory.Quantity02;
				data.Quantity03 = stockBranchInventory.Quantity03;
				data.Quantity04 = stockBranchInventory.Quantity04;
				data.Quantity05 = stockBranchInventory.Quantity05;
				data.Quantity06 = stockBranchInventory.Quantity06;
				data.Quantity07 = stockBranchInventory.Quantity07;
				data.Quantity08 = stockBranchInventory.Quantity08;
				data.Quantity09 = stockBranchInventory.Quantity09;
				data.Quantity10 = stockBranchInventory.Quantity10;
				data.Quantity11 = stockBranchInventory.Quantity11;
				data.Quantity12 = stockBranchInventory.Quantity12;
				data.Quantity13 = stockBranchInventory.Quantity13;
				data.Quantity14 = stockBranchInventory.Quantity14;
				data.Quantity15 = stockBranchInventory.Quantity15;
				data.Quantity16 = stockBranchInventory.Quantity16;
				data.Quantity17 = stockBranchInventory.Quantity17;
				data.Quantity18 = stockBranchInventory.Quantity18;
				data.Quantity19 = stockBranchInventory.Quantity19;
				data.Quantity20 = stockBranchInventory.Quantity20;
				data.Quantity21 = stockBranchInventory.Quantity21;
				data.Quantity22 = stockBranchInventory.Quantity22;
				data.Quantity23 = stockBranchInventory.Quantity23;
				data.Quantity24 = stockBranchInventory.Quantity24;
				data.Quantity25 = stockBranchInventory.Quantity25;
				data.Quantity26 = stockBranchInventory.Quantity26;
				data.Quantity27 = stockBranchInventory.Quantity27;
				data.Quantity28 = stockBranchInventory.Quantity28;
				data.Quantity29 = stockBranchInventory.Quantity29;
				data.Quantity30 = stockBranchInventory.Quantity30;
				data.IsActive = stockBranchInventory.IsActive;
			}
            db.SaveChanges();
            return Ok(true);

        }

        // POST: api/StockBranchInventories
        [ResponseType(typeof(StockBranchInventory))]
        public IHttpActionResult PostStockBranchInventory(StockBranchInventory stockBranchInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StockBranchInventories.Add(stockBranchInventory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stockBranchInventory.Id }, stockBranchInventory);
        }

        // DELETE: api/StockBranchInventories/5
        [ResponseType(typeof(StockBranchInventory))]
        public IHttpActionResult DeleteStockBranchInventory(int id)
        {
            StockBranchInventory stockBranchInventory = db.StockBranchInventories.Find(id);
            if (stockBranchInventory == null)
            {
                return NotFound();
            }

            db.StockBranchInventories.Remove(stockBranchInventory);
            db.SaveChanges();

            return Ok(stockBranchInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockBranchInventoryExists(int id)
        {
            return db.StockBranchInventories.Count(e => e.Id == id) > 0;
        }
    }
}