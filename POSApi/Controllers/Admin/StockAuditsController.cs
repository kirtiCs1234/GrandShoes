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
    [RoutePrefix("api/stockAudit")]
    public class StockAuditsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StockAuditsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        // GET: api/StockAudits
        public List<StockAudit> GetStockAudits()
        {
            var list= db.StockAudits;
            return list.ToList();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getByBranch")]
        // GET: api/StockAudits
        public List<StockAudit> GetStockAudit(int? BranchId)
        {
            var list = db.StockAudits.Where(x=>x.IsActive==true && x.BranchId==BranchId);
            return list.ToList();
        }
        // GET: api/StockAudits/5
        [ResponseType(typeof(StockAudit))]
        public IHttpActionResult GetStockAudit(int id)
        {
            StockAudit stockAudit = db.StockAudits.Find(id);
            if (stockAudit == null)
            {
                return NotFound();
            }

            return Ok(stockAudit);
        }

        // PUT: api/StockAudits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockAudit(int id, StockAudit stockAudit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockAudit.Id)
            {
                return BadRequest();
            }

            db.Entry(stockAudit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockAuditExists(id))
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
        [Route("create")]
        // POST: api/StockAudits
        [ResponseType(typeof(StockAudit))]
        public IHttpActionResult PostStockAudit(StockAudit stockAudit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StockAudits.Add(stockAudit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stockAudit.Id }, stockAudit);
        }
        [HttpPost]
        [Route("delete")]
        // DELETE: api/StockAudits/5
        [ResponseType(typeof(StockAudit))]
        public IHttpActionResult DeleteStockAudit(int id)
        {
            StockAudit stockAudit = db.StockAudits.Find(id);
            if (stockAudit == null)
            {
                return NotFound();
            }

            db.StockAudits.Remove(stockAudit);
            db.SaveChanges();

            return Ok(stockAudit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockAuditExists(int id)
        {
            return db.StockAudits.Count(e => e.Id == id) > 0;
        }
    }
}