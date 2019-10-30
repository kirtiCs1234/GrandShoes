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
using Newtonsoft.Json;
namespace POSApi.Controllers
{
    [RoutePrefix("api/productSource")]
    public class ProductSourcesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ProductSourcesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ProductSources
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<ProductSource> GetProductSources()
        {
           var list= db.ProductSources.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList();
            return list;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getProductSourcePaging")]
        public ServiceResult<List<ProductSource>> GetArea([FromUri]Paging paging)
        {
            ServiceResult<List<ProductSource>> model = new ServiceResult<List<ProductSource>>();
            var source = db.ProductSources.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            int count = source.Count();
            int CurrentPage = paging.pageNumber;
            int PageSize = paging.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            model.TotalCount = count;
            model.data = items;
            return model;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<ProductSource>> GetSearchData(ProductSourceSearch productSourceSearch)
        {
            ServiceResult<List<ProductSource>> model = new ServiceResult<List<ProductSource>>();
            var source = db.ProductSources.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (productSourceSearch != null)
            {
                if (!string.IsNullOrEmpty(productSourceSearch.Source))
                    source = source.Where(m => m.Source.Contains(productSourceSearch.Source));
            }
            var result = source.ToList();
            int count = result.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((productSourceSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new ProductSource
            {
                Id = x.Id,
                Source = x.Source,
            }).ToList();
            model.TotalCount = count;
            return model; 
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getProductSourceId")]
        public ProductSource GetCode(string sku)
        {
            ProductSource model = new ProductSource();
            bool data = db.ProductSources.Any(x => x.Source == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.ProductSources.Where(x => x.IsActive == true && x.Source== sku).FirstOrDefault();
            }
            else
            {
                model.Source= sku;
                model.IsActive = true;
                db.ProductSources.Add(model);
                db.SaveChanges();
                var list = db.ProductSources.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(ProductSource))]
        public IHttpActionResult GetProductSource(int id)
        {
            ProductSource productSource = db.ProductSources.Find(id);
            if (productSource == null)
            {
                return NotFound();
            }
            return Ok(productSource);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkSource")]
        public bool GetSource(ProductSource user)
        {
            if (user.Id > 0)
            {
                var source = db.ProductSources.Where(x => x.Id == user.Id && x.IsActive == true).FirstOrDefault();
                if (source.Source.Equals(user.Source))
                {
                    return false;
                }

            }
            var data = db.ProductSources.Any(x => x.Source == user.Source&& x.IsActive == true);
            return data;
        }
        // PUT: api/ProductSources/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductSource(int id, ProductSource productSource)
        {
			var model= db.ProductSources.Where(x=>x.IsActive==true && x.Id==id).FirstOrDefault();
			model.Source = productSource.Source;
			model.IsActive = true;
            model.UpdatedOn = System.DateTime.UtcNow;
			db.SaveChanges();
			return Ok(true);
        }

        // POST: api/ProductSources
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(ProductSource))]
        public IHttpActionResult PostProductSource(ProductSource productSource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			var model = new ProductSource();
			model.IsActive = true;
			model.Source = productSource.Source;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
			//var pageName = Request.RequestUri.LocalPath.getRouteName();
			//Object obj = null;
			//var UserId = 0;
			//if (Request.Headers.Contains("Email"))
			//{
			//	var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
			//	UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			//}
			db.ProductSources.Add(model);
			//try
			//{
				db.SaveChanges();
			//}
			//catch (Exception ex) { obj = ex; }
			//finally
			//{
			//	if (obj == null)
			//	{

			//		var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(productSource, new JsonSerializerSettings()
			//		{
			//			PreserveReferencesHandling = PreserveReferencesHandling.Objects,
			//			Formatting = Formatting.Indented
			//		});
			//		var flag = productSource.CreateLog(pageName, logTable, UserId);
			//	}
			//}

			return Ok(true);
        }
        // DELETE: api/ProductSources/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(ProductSource))]
        public IHttpActionResult DeleteProductSource(int id)
        {
           var productSource = db.ProductSources.Where(x=>x.IsActive==true && x.Id==id).FirstOrDefault();
            if (productSource == null)
            {
                return NotFound();
            }
            productSource.IsActive = false;
            productSource.UpdatedOn = System.DateTime.UtcNow;
           // db.ProductSources.Remove(productSource);
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
        private bool ProductSourceExists(int id)
        {
            return db.ProductSources.Count(e => e.Id == id) > 0;
        }
    }
}