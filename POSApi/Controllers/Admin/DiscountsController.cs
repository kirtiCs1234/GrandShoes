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
    [RoutePrefix("api/discount")]
    public class DiscountsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public DiscountsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [Route("getDetails")]
        // GET: api/Discounts
        public List<Discount> GetDiscounts()
        {
            var list = db.Discounts.ToList();
            return list;
        }

        // GET: api/Discounts/5
        [ResponseType(typeof(Discount))]
        public IHttpActionResult GetDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);
        }

        // PUT: api/Discounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscount(int id, Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discount.Id)
            {
                return BadRequest();
            }

            db.Entry(discount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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
        // POST: api/Discounts
        [ResponseType(typeof(Discount))]
        public IHttpActionResult PostDiscount(DiscountSummaryModel discount)

        {
            DiscountSummary model = new DiscountSummary();
            model.DiscountType = discount.DiscountType;
            model.DiscountValue = discount.DiscountValue;
            model.FromDate = Convert.ToDateTime(discount.FromDate);
            model.ToDate = Convert.ToDateTime(discount.ToDate);
            model.FromProductSKU = discount.FromProductSKU;
            model.ToProductSKU = discount.ToProductSKU;
            model.FromStyleSKU = discount.FromStyleSKU;
            model.ToStyleSKU = discount.ToStyleSKU;
            model.FromPrice = discount.FromPrice;
            model.ToPrice = discount.ToPrice;
            model.IsActive = true;
            if (discount.ProductList != null)
            {
                model.PromotionalDiscounts = discount.ProductList.Select(x => new PromotionalDiscount
                {
                    ProductID = Convert.ToInt16(x),
                    IsActive = true,
                }).ToList();
            }
            try
            {
                db.DiscountSummaries.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return Ok(model);
        }

        // DELETE: api/Discounts/5
        [ResponseType(typeof(Discount))]
        public IHttpActionResult DeleteDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }

            db.Discounts.Remove(discount);
            db.SaveChanges();

            return Ok(discount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscountExists(int id)
        {
            return db.Discounts.Count(e => e.Id == id) > 0;
        }
    }
}