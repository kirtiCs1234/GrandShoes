﻿using System;
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
    [RoutePrefix("api/stockTape")]
    public class StockTapesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StockTapesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        // GET: api/StockTapes
        public List<StockTape> GetStockTapes()
        {
          var list= db.StockTapes.Where(x=>x.IsActive==true);
            return list.ToList();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getByBranchId")]
        // GET: api/StockTapes/5
        [ResponseType(typeof(StockTape))]
        public List<StockTape> GetStockTape(int? BranchId)
        {
            var data = db.StockTapes.Where(x =>x.IsActive==true && x.BranchId == BranchId).ToList();
            return data;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        // PUT: api/StockTapes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockTape(int id, StockTape stockTape)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockTape.Id)
            {
                return BadRequest();
            }

            db.Entry(stockTape).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockTapeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        // POST: api/StockTapes
        [ResponseType(typeof(StockTape))]
        public IHttpActionResult PostStockTape(StockTape stockTape)
        {
            StockTape model = new StockTape();
            model.ProductID = stockTape.ProductID;
            model.Quantity01 = stockTape.Quantity01;
            model.Quantity02 = stockTape.Quantity02;
            model.Quantity03 = stockTape.Quantity03;
            model.Quantity04 = stockTape.Quantity04;
            model.Quantity05 = stockTape.Quantity05;
            model.Quantity06 = stockTape.Quantity06;
            model.Quantity07 = stockTape.Quantity07;
            model.Quantity08 = stockTape.Quantity08;
            model.Quantity09 = stockTape.Quantity09;
            model.Quantity10 = stockTape.Quantity10;
            model.Quantity11 = stockTape.Quantity11;
            model.Quantity12 = stockTape.Quantity12;
            model.Quantity13 = stockTape.Quantity13;
            model.Quantity14 = stockTape.Quantity14;
            model.Quantity15 = stockTape.Quantity15;
            model.Quantity16 = stockTape.Quantity16;
            model.Quantity17 = stockTape.Quantity17;
            model.Quantity18 = stockTape.Quantity18;
            model.Quantity19 = stockTape.Quantity19;
            model.Quantity20 = stockTape.Quantity20;
            model.Quantity21 = stockTape.Quantity21;
            model.Quantity22 = stockTape.Quantity22;
            model.Quantity23 = stockTape.Quantity23;
            model.Quantity24 = stockTape.Quantity24;
            model.Quantity25 = stockTape.Quantity25;
            model.Quantity26 = stockTape.Quantity26;
            model.Quantity27 = stockTape.Quantity27;
            model.Quantity28 = stockTape.Quantity28;
            model.Quantity29 = stockTape.Quantity29;
            model.Quantity30 = stockTape.Quantity30;
            model.Barcode = stockTape.Barcode;
            model.BranchId = stockTape.BranchId;
            model.IsActive = stockTape.IsActive;
            model.LogId = stockTape.LogId;
            db.StockTapes.Add(model);
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("stockTapeExcelData")]
        [ResponseType(typeof(StockTape))]
        public IHttpActionResult PostExcelStockTape(StockTape stockTape)
        {
            StockTape model = new StockTape();
            model.ProductID = stockTape.ProductID;
            model.Quantity01 = stockTape.Quantity01;
            model.Quantity02 = stockTape.Quantity02;
            model.Quantity03 = stockTape.Quantity03;
            model.Quantity04 = stockTape.Quantity04;
            model.Quantity05 = stockTape.Quantity05;
            model.Quantity06 = stockTape.Quantity06;
            model.Quantity07 = stockTape.Quantity07;
            model.Quantity08 = stockTape.Quantity08;
            model.Quantity09 = stockTape.Quantity09;
            model.Quantity10 = stockTape.Quantity10;
            model.Quantity11 = stockTape.Quantity11;
            model.Quantity12 = stockTape.Quantity12;
            model.Quantity13 = stockTape.Quantity13;
            model.Quantity14 = stockTape.Quantity14;
            model.Quantity15 = stockTape.Quantity15;
            model.Quantity16 = stockTape.Quantity16;
            model.Quantity17 = stockTape.Quantity17;
            model.Quantity18 = stockTape.Quantity18;
            model.Quantity19 = stockTape.Quantity19;
            model.Quantity20 = stockTape.Quantity20;
            model.Quantity21 = stockTape.Quantity21;
            model.Quantity22 = stockTape.Quantity22;
            model.Quantity23 = stockTape.Quantity23;
            model.Quantity24 = stockTape.Quantity24;
            model.Quantity25 = stockTape.Quantity25;
            model.Quantity26 = stockTape.Quantity26;
            model.Quantity27 = stockTape.Quantity27;
            model.Quantity28 = stockTape.Quantity28;
            model.Quantity29 = stockTape.Quantity29;
            model.Quantity30 = stockTape.Quantity30;
            model.Barcode = stockTape.Barcode;
            model.BranchId = stockTape.BranchId;
            model.IsActive = stockTape.IsActive;
            model.LogId = stockTape.LogId;
            db.StockTapes.Add(model);
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        // DELETE: api/StockTapes/5
        [ResponseType(typeof(StockTape))]
        public IHttpActionResult DeleteStockTape(List<StockTape> list)
        {
			foreach (var item in list)
			{
				var model = db.StockTapes.Where(x => x.IsActive == true && x.Id == item.Id).FirstOrDefault();
				db.StockTapes.Remove(model);
			}
            db.SaveChanges();
            return Ok(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockTapeExists(int id)
        {
            return db.StockTapes.Count(e => e.Id == id) > 0;
        }
    }
}