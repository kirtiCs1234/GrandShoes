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
    [RoutePrefix("api/designation")]
    public class DesignationsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public DesignationsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Designations
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Designation> GetDesignations()
        {
            var list= db.Designations.Where(x=>x.IsActive==true).ToList();
            return list;
        }

        // GET: api/Designations/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Designation))]
        public IHttpActionResult GetDesignation(int id)
        {
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return NotFound();
            }

            return Ok(designation);
        }

        // PUT: api/Designations/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDesignation(int id, Designation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != designation.Id)
            {
                return BadRequest();
            }

            db.Entry(designation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignationExists(id))
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

        // POST: api/Designations
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Designation))]
        public IHttpActionResult PostDesignation(Designation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Designations.Add(designation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = designation.Id }, designation);
        }

        // DELETE: api/Designations/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(Designation))]
        public IHttpActionResult DeleteDesignation(int id)
        {
            Designation designation = db.Designations.Find(id);
            if (designation == null)
            {
                return NotFound();
            }
            designation.IsActive = false;
           // db.Designations.Remove(designation);
            db.SaveChanges();

            return Ok(designation);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkDesignationName")]
        public bool GetPhoneNumber(Designation designation)
        {
            if (designation.Id > 0)
            {
                var name = db.Designations.Where(x => x.Id == designation.Id && x.IsActive == true).FirstOrDefault();
                if (name.DesignationName.Equals(designation.DesignationName))
                {
                    return false;
                }

            }
            var data = db.Designations.Any(x => x.DesignationName == designation.DesignationName && x.IsActive == true);
            return data;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesignationExists(int id)
        {
            return db.Designations.Count(e => e.Id == id) > 0;
        }
    }
}