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
    [RoutePrefix("api/branchDiscount")]
    public class BranchDiscountsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public BranchDiscountsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/BranchDiscounts
        [HttpGet]
        [Route("getDetails")]
        public List<BranchDiscount> GetBranchDiscounts()
        {
            var list=db.BranchDiscounts.Where(x=>x.IsActive==true).Include(x=>x.Branch).OrderByDescending(x => x.Id).Include(x=>x.Discount).ToList();
            return list;
        }

        // GET: api/BranchDiscounts/5
        [ResponseType(typeof(BranchDiscount))]
        public IHttpActionResult GetBranchDiscount(int id)
        {
            BranchDiscount branchDiscount = db.BranchDiscounts.Find(id);
            if (branchDiscount == null)
            {
                return NotFound();
            }

            return Ok(branchDiscount);
        }

        // PUT: api/BranchDiscounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranchDiscount(int id, BranchDiscount branchDiscount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branchDiscount.Id)
            {
                return BadRequest();
            }

            db.Entry(branchDiscount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchDiscountExists(id))
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

        // POST: api/BranchDiscounts
        [ResponseType(typeof(BranchDiscount))]
        public IHttpActionResult PostBranchDiscount(BranchDiscount branchDiscount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BranchDiscounts.Add(branchDiscount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = branchDiscount.Id }, branchDiscount);
        }

        // DELETE: api/BranchDiscounts/5
        [ResponseType(typeof(BranchDiscount))]
        public IHttpActionResult DeleteBranchDiscount(int id)
        {
            BranchDiscount branchDiscount = db.BranchDiscounts.Find(id);
            if (branchDiscount == null)
            {
                return NotFound();
            }

            db.BranchDiscounts.Remove(branchDiscount);
            db.SaveChanges();

            return Ok(branchDiscount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchDiscountExists(int id)
        {
            return db.BranchDiscounts.Count(e => e.Id == id) > 0;
        }
    }
}