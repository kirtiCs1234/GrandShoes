using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Model;
using Newtonsoft.Json;
using Helper;

namespace POSApi.Controllers
{
    [RoutePrefix("api/productCategory")]
    public class ProductCategoriesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ProductCategoriesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/ProductCategories
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<ProductCategory> GetProductCategories()
        {
            var list= db.ProductCategories.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList();
            return list;
        }
        [HttpPost]
        [Route("checkCode")]
        public bool GetEmail(ProductCategory model)
        {
            if (model.Id > 0)
            {
                var code = db.ProductCategories.Where(x => x.Id == model.Id && x.IsActive == true).FirstOrDefault();
                if (code.Code.Equals(model.Code))
                {
                    return false;
                }

            }
            var data = db.ProductCategories.Any(x => x.Code == model.Code && x.IsActive == true);
            return data;
        }
        [HttpGet]
        [Route("getIdCate1")]
        public IHttpActionResult GetIDCat1(string prodCat1)
        {
            var data = db.ProductCat1.Where(x => x.IsActive == true && x.CateName == prodCat1).FirstOrDefault();
            return Ok(data);
        }
        [HttpGet]
        [Route("getIdCate2")]
        public IHttpActionResult GetIDCat2(string prodCat2)
        {
            var data = db.ProductCat2.Where(x => x.IsActive == true && x.CateName == prodCat2).FirstOrDefault();
            return Ok(data);
        }
        [HttpGet]
        [Route("getIdCate3")]
        public IHttpActionResult GetIDCat3(string prodCat3)
        {
            var data = db.ProductCat3.Where(x => x.IsActive == true && x.CateName == prodCat3).FirstOrDefault();
            return Ok(data);
        }
        [HttpGet]
        [Route("getIdCate4")]
        public IHttpActionResult GetIDCat4(string prodCat4)
        {
            var data = db.ProductCat4.Where(x => x.IsActive == true && x.CateName == prodCat4).FirstOrDefault();
            return Ok(data);
        }
        [HttpGet]
        [Route("getProdCat1")]
        public IHttpActionResult GetCat1()
        {
            var list = db.ProductCat1.Where(x => x.IsActive == true).ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("getProdCat2")]
        public IHttpActionResult GetCat2()
        {
            var list = db.ProductCat2.Where(x => x.IsActive == true).ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("getProdCat3")]
        public IHttpActionResult GetCat3()
        {
            var list = db.ProductCat3.Where(x => x.IsActive == true).ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("getProdCat4")]
        public IHttpActionResult GetCat4()
        {
            var list = db.ProductCat4.Where(x => x.IsActive == true).ToList();
            return Ok(list);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getCatagoryCodeId")]
        public ProductCategory GetCode(string sku)
        {
            ProductCategory model = new ProductCategory();
            bool data = db.ProductCategories.Any(x => x.Code == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.ProductCategories.Where(x => x.IsActive == true && x.Code== sku).FirstOrDefault();
            }
            else
            {
                model.Code= sku;
                model.IsActive = true;
                db.ProductCategories.Add(model);
                db.SaveChanges();
                var list = db.ProductCategories.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getProductCategoryPaging")]
        public ServiceResult<List<ProductCategory>> GetArea([FromUri]Paging paging)

        {
            ServiceResult<List<ProductCategory>> model = new ServiceResult<List<ProductCategory>>();
            var source = db.ProductCategories.Where(x => x.IsActive == true)
                        .OrderByDescending(x => x.Id).ToList();
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
            model.pageSize = PageSize;
            return model;

        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<ProductCategory>> GetSearchData(ProductCategorySearch productCategorySearch)

        {
            ServiceResult<List<ProductCategory>> model = new ServiceResult<List<ProductCategory>>();
            var source = db.ProductCategories.Where(x => x.IsActive == true);
            var pageSize = 10;
            var CurrentPage = 1;
            if (productCategorySearch != null)
            {

                if (!string.IsNullOrEmpty(productCategorySearch.Code))

                    source = source.Where(m => m.Code== productCategorySearch.Code);
            }

            
            int count = source.Count();
           
            var items = source.OrderByDescending(x => x.Id).Skip(((productCategorySearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new ProductCategory
            {
                Id = x.Id,
                Code=x.Code,
               // Description=x.Description,
                IsActive=x.IsActive
            }).ToList();
            model.TotalCount = count;
            return model; ;
        }

        // GET: api/ProductCategories/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult GetProductCategory(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        // PUT: api/ProductCategories/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductCategory(int id, ProductCategory productCategory)
        {
			var list = new List<ProductCategory>();
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
			data.Code = productCategory.Code;
			//data.Description = productCategory.Description;
			data.IsActive = true;
			try
			{
				db.SaveChanges();
				list.Add(productCategory);
				return Ok(data);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				obj = ex;
			}

			finally
			{
				if (obj == null)
				{
					logTable = "";
					var change = list.ToDataTables().getChangedRecords();
					var c = change.Count() / 2;
					for (var i = 0; i < c; i++)
					{
						logTable += change[i].Fieldname + " Old Value=[" + change[i].FieldValue + "] New Value=[" + change[i + c].FieldValue + "], ";
					}
					//logTable = Newtonsoft.Json.JsonConvert.SerializeObject(change);
					var flag = productCategory.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(data);
        }

        // POST: api/ProductCategories
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult PostProductCategory(ProductCategory productCategory)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			db.ProductCategories.Add(productCategory);
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(productCategory, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = productCategory.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(productCategory);
        }

        // DELETE: api/ProductCategories/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(ProductCategory))]
        public IHttpActionResult DeleteProductCategory(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            productCategory.IsActive = false;
           //db.ProductCategories.Remove(productCategory);
            db.SaveChanges();

            return Ok(productCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductCategoryExists(int id)
        {
            return db.ProductCategories.Count(e => e.Id == id) > 0;
        }
    }
}