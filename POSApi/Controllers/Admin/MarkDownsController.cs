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
using Model;
using Helper.ExtensionMethod;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/markDown")]
    public class MarkDownsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public MarkDownsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/MarkDowns
		[HttpGet]
		[Route("GetByDate")]
		public IHttpActionResult GetByDate()
		{
			var todateDate= DateTime.Now.ToString("yyyy-MM-dd");
			var list = db.MarkDowns.Where(x => x.IsActive == true && x.EffectiveDate.ToString()==todateDate).OrderByDescending(x => x.Id).ToList().RemoveReferences();
			return Ok(list);
		}
        [HttpGet]
        [Route("getDetails")]
        public IHttpActionResult GetMarkDowns()
        {
           var list= db.MarkDowns.Where(x=>x.IsActive==true).OrderByDescending(x=>x.Id).ToList().RemoveReferences();
			
            return Ok(list);
        }
		[HttpGet]
		[Route("getByProduct")]
		public IHttpActionResult GetByProduct(string ProductSKU,string StyleSKU)
		{
			var list = db.MarkDowns.Where(x => x.IsActive == true && x.ProductSKU == ProductSKU && x.StyleSKU == StyleSKU).ToList().RemoveReferences();
			return Ok(list);
		}
        [HttpGet]
        [Route("getDetail")]
        // GET: api/MarkDowns/5
        [ResponseType(typeof(MarkDown))]
        public IHttpActionResult GetMarkDown(int id)
        {
            MarkDown markDown = db.MarkDowns.Find(id).RemoveReferences();
            if (markDown == null)
            {
                return NotFound();
            }
            return Ok(markDown);
        }
        [HttpPost]
        [Route("edit")]
        // PUT: api/MarkDowns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarkDown(int id, MarkDown markDown)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != markDown.Id)
            {
                return BadRequest();
            }
            db.Entry(markDown).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkDownExists(id))
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
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("getMarkDownId")]
        //public MarkDown GetCode(string sku)
        //{
        //    MarkDown model = new MarkDown();
        //    bool data = db.MarkDowns.Any(x => x.== sku && x.IsActive == true);
        //    if (data == true)
        //    {
        //        model = db.MarkDowns.Where(x => x.IsActive == true && x.Name == sku).FirstOrDefault();
        //    }
        //    else
        //    {
        //        model.Name = sku;
        //        model.IsActive = true;
        //        db.Templates.Add(model);
        //        db.SaveChanges();
        //        model = db.Templates.Where(x => x.IsActive == true).LastOrDefault();
        //    }
        //    return model;
        //}
        [HttpPost]
        [Route("create")]
        // POST: api/MarkDowns
        [ResponseType(typeof(MarkDown))]
        public IHttpActionResult PostMarkDown(MarkDownAddModel markDown)
        {
            MarkDown model = new MarkDown();
            model.MarkDownBranches = markDown.BranchList.Select(x => new MarkDownBranch
            {
                BranchID = Convert.ToInt16(x),
                IsActive = true,
                UpdatedOn = System.DateTime.UtcNow,
                CreatedOn = System.DateTime.UtcNow,
                }).ToList();
                model.EffectiveDate = Convert.ToDateTime(markDown.MarkDown.EffectiveDate);
                model.IsPercentageOriginalPrice = markDown.MarkDown.IsPercentageOriginalPrice;
                model.NewCashPrice = markDown.MarkDown.NewCashPrice;
                model.NewSellingPrice = markDown.MarkDown.NewSellingPrice;
                model.OriginalSellingPrice = markDown.MarkDown.OriginalSellingPrice;
                model.PercentageDecrease = markDown.MarkDown.PercentageDecrease;
                model.ProductSKU = markDown.MarkDown.ProductSKU.ToString();
                model.StyleSKU = markDown.MarkDown.StyleSKU;
                model.IsActive = true;
                model.UpdatedOn = System.DateTime.UtcNow;
                model.CreatedOn = System.DateTime.UtcNow;
                db.MarkDowns.Add(model);
                db.SaveChanges();
                return Ok(true);
        }
        [HttpPost]
        [Route("delete")]
        // DELETE: api/MarkDowns/5
        [ResponseType(typeof(MarkDown))]
        public IHttpActionResult DeleteMarkDown(int id)
        {
            MarkDown markDown = db.MarkDowns.Find(id);
            if (markDown == null)
            {
                return NotFound();
            }
            markDown.UpdatedOn = System.DateTime.UtcNow;
            db.MarkDowns.Remove(markDown);
            db.SaveChanges();
            return Ok(markDown);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool MarkDownExists(int id)
        {
            return db.MarkDowns.Count(e => e.Id == id) > 0;
        }
    }
}