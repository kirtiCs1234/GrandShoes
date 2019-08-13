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

namespace POSApi.Controllers
{
    [RoutePrefix("api/productGroup")]
    public class ProductGroupController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ProductGroupController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ProductGroup
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<ProductGrp> GetProductGroups()
        {
           var list= db.ProductGrps.ToList();
            return list;
        }

        // GET: api/ProductGroup/5
        [ResponseType(typeof(ProductGrp))]
        public IHttpActionResult GetProductGroup(int id)
        {
			ProductGrp productGroup = db.ProductGrps.Find(id);
            if (productGroup == null)
            {
                return NotFound();
            }
            return Ok(productGroup);
        }
        // PUT: api/ProductGroup/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductGroup(int id, ProductGrp productGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(productGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductGroupExists(id))
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

        // POST: api/ProductGroup
        [ResponseType(typeof(ProductGrp))]
        public IHttpActionResult PostProductGroup(ProductGrp productGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductGrps.Add(productGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productGroup.ID }, productGroup);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getProductGroupId")]
        public ProductGrp GetCode(string sku)
        {
			ProductGrp model = new ProductGrp();
            bool data = db.ProductGrps.Any(x => x.GroupName == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.ProductGrps.Where(x => x.IsActive == true && x.GroupName == sku).FirstOrDefault();
            }
            else
            {
                model.GroupName = sku;
                model.IsActive = true;
                model.CreatedOn = System.DateTime.UtcNow;
                model.UpdatedOn = System.DateTime.UtcNow;
                db.ProductGrps.Add(model);
                db.SaveChanges();
                var list = db.ProductGrps.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        // DELETE: api/ProductGroup/5
        [ResponseType(typeof(ProductGrp))]
        public IHttpActionResult DeleteProductGroup(int id)
        {
			ProductGrp productGroup = db.ProductGrps.Find(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            db.ProductGrps.Remove(productGroup);
            db.SaveChanges();

            return Ok(productGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductGroupExists(int id)
        {
            return db.ProductGrps.Count(e => e.ID == id) > 0;
        }
    }
}