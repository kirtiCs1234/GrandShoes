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

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/productSize")]
    public class ProductSizesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ProductSizesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ProductSizes
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<ProductSize> GetProductSizes()
        {
           var list=db.ProductSizes.Where(x=>x.IsActive==true).OrderByDescending(x=>x.Id).ToList();
            return list;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getAreaPaging")]
        public ServiceResult<List<ProductSize>> GetArea([FromUri]Paging paging)

        {
            ServiceResult<List<ProductSize>> model = new ServiceResult<List<ProductSize>>();
            var source = db.ProductSizes.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = paging.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = paging.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            model.TotalCount = count;
            model.data = items;

            return model;

        }
        // GET: api/ProductSizes/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(ProductSize))]
        public IHttpActionResult GetProductSize(int id)
        {
            ProductSize productSize = db.ProductSizes.Find(id);
            if (productSize == null)
            {
                return NotFound();
            }

            return Ok(productSize);
        }

        // PUT: api/ProductSizes/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductSize(int id, ProductSize productSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productSize.Id)
            {
                return BadRequest();
            }

            db.Entry(productSize).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSizeExists(id))
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

        // POST: api/ProductSizes
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(ProductSize))]
        public IHttpActionResult PostProductSize(ProductSize productSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductSizes.Add(productSize);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productSize.Id }, productSize);
        }

        // DELETE: api/ProductSizes/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(ProductSize))]
        public IHttpActionResult DeleteProductSize(int id)
        {
            ProductSize productSize = db.ProductSizes.Find(id);
            if (productSize == null)
            {
                return NotFound();
            }

            db.ProductSizes.Remove(productSize);
            db.SaveChanges();

            return Ok(productSize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductSizeExists(int id)
        {
            return db.ProductSizes.Count(e => e.Id == id) > 0;
        }
    }
}