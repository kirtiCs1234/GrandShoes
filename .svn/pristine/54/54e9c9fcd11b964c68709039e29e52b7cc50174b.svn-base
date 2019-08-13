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
    [RoutePrefix("api/year")]
    public class YearsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public YearsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Years
        public IQueryable<Year> GetYears()
        {
            return db.Years;
        }

        // GET: api/Years/5
        [ResponseType(typeof(Year))]
        public IHttpActionResult GetYear(int id)
        {
            Year year = db.Years.Find(id);
            if (year == null)
            {
                return NotFound();
            }

            return Ok(year);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getYearId")]
        public Year GetCode(string sku)
        {
            Year model = new Year();
            bool data = db.Years.Any(x => x.Year1 == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.Years.Where(x => x.IsActive == true && x.Year1== sku).FirstOrDefault();
            }
            else
            {
                model.Year1= sku;
                model.IsActive = true;
                db.Years.Add(model);
                db.SaveChanges();
                var list = db.Years.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        // PUT: api/Years/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutYear(int id, Year year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != year.Id)
            {
                return BadRequest();
            }

            db.Entry(year).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearExists(id))
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

        // POST: api/Years
        [ResponseType(typeof(Year))]
        public IHttpActionResult PostYear(Year year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Years.Add(year);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = year.Id }, year);
        }

        // DELETE: api/Years/5
        [ResponseType(typeof(Year))]
        public IHttpActionResult DeleteYear(int id)
        {
            Year year = db.Years.Find(id);
            if (year == null)
            {
                return NotFound();
            }

            db.Years.Remove(year);
            db.SaveChanges();

            return Ok(year);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool YearExists(int id)
        {
            return db.Years.Count(e => e.Id == id) > 0;
        }
    }
}