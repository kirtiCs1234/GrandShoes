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
    [RoutePrefix("api/productStyle")]
    public class ProductStylesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ProductStylesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ProductStyles
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<ProductStyle> GetProductStyles()
        {
            var list = db.ProductStyles.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            return list;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getCode")]
        public IQueryable<string> GetProductStyleCode()
        {
            var list = from s in db.ProductStyles.Where(x=>x.IsActive==true)
                       select s.StyleSKU;
            return list;
        }
        // GET: api/ProductStyles/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(ProductStyle))]
        public IHttpActionResult GetProductStyle(int id)
        {
            ProductStyle productStyle = db.ProductStyles.Find(id);
            if (productStyle == null)
            {
                return NotFound();
            }

            return Ok(productStyle);
        }


        //Get productStyle for AutoComplete
        [HttpGet]
        [Route("ProductStyleAutocomplete")]
        public IHttpActionResult ProductStyleAutocomplete(string name)
        {
            var data = db.ProductStyles.Where(x => x.IsActive == true && x.StyleSKU.Contains(name)).ToList().Select(m => new Model.ProductStyleModel
            {
                StyleSKU = m.StyleSKU,
                Id = m.Id,
            }).ToList();
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getProductStylePaging")]
        public ServiceResult<List<ProductStyle>> GetPaging([FromUri]Paging paging)

        {
            ServiceResult<List<ProductStyle>> model = new ServiceResult<List<ProductStyle>>();
            var source = db.ProductStyles.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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
            model.TotalCount = count;
            model.data = items;

            return model;

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<ProductStyle>> GetSearchData(ProductStyleSearch productStyleSearch)

        {
            ServiceResult<List<ProductStyle>> model = new ServiceResult<List<ProductStyle>>();
            var source = db.ProductStyles.Where(x => x.IsActive == true);

            if (productStyleSearch != null)
            {

                if (!string.IsNullOrEmpty(productStyleSearch.StyleSKU))

                    source = source.Where(m => m.StyleSKU == productStyleSearch.StyleSKU);
            }

            var result = source.ToList();
            int count = result.Count();
            model.TotalCount = count;
            model.data = result;

            return model; ;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getStyleSKUId")]
        public ProductStyle GetCode(string sku)
        {
            ProductStyle model = new ProductStyle();
            bool data = db.ProductStyles.Any(x => x.StyleSKU== sku && x.IsActive == true);
            if (data == true)
            {
                model = db.ProductStyles.Where(x => x.IsActive == true && x.StyleSKU == sku).FirstOrDefault();
            }
            else
            {
                model.StyleSKU = sku;
                model.IsActive =true;
                db.ProductStyles.Add(model);
                db.SaveChanges();
                var list = db.ProductStyles.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }

        // PUT: api/ProductStyles/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductStyle(int id, ProductStyle productStyle)
        {
            var model = db.ProductStyles.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            model.StyleSKU = productStyle.StyleSKU;
            model.IsActive = productStyle.IsActive;
            db.SaveChanges();
            return Ok(true);
        }

        // POST: api/ProductStyles
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(ProductStyle))]
        public IHttpActionResult PostProductStyle(ProductStyle productStyle)
        {
            ProductStyle model = new ProductStyle();
            model.StyleSKU = productStyle.StyleSKU;
            model.IsActive = productStyle.IsActive;
            db.ProductStyles.Add(model);
            db.SaveChanges();
            return Ok(true);
        }

        // DELETE: api/ProductStyles/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(ProductStyle))]
        public IHttpActionResult DeleteProductStyle(int id)
        {
            ProductStyle productStyle = db.ProductStyles.Find(id);
            productStyle.IsActive = false;
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

        private bool ProductStyleExists(int id)
        {
            return db.ProductStyles.Count(e => e.Id == id) > 0;
        }
    }
}