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
using Newtonsoft.Json;
using Helper;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/buyer")]
    public class BuyersController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public BuyersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Buyers
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public IHttpActionResult GetBuyers()
        {
           var list= db.Buyers.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList().Select(m=>new Model.BuyerModel {
               Id=m.Id,
               Name=m.Name,
               IsActive=m.IsActive,
           }).ToList();
           return Ok(list);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getBuyerId")]
        public Buyer GetCode(string sku)
        {
            Buyer model = new Buyer();
            bool data = db.Buyers.Any(x => x.Name== sku && x.IsActive == true);
            if (data == true)
            {
                model = db.Buyers.Where(x => x.IsActive == true && x.Name== sku).FirstOrDefault();
            }
            else
            {
                model.Name = sku;
                model.IsActive = true;
                db.Buyers.Add(model);
                db.SaveChanges();
                var list= db.Buyers.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getBuyerPaging")]
        public ServiceResult<List<Buyer>> GetBuyer([FromUri]Paging paging)
        {
            ServiceResult<List<Buyer>> model = new ServiceResult<List<Buyer>>();
            var source = db.Buyers.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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
            model.pageSize = PageSize;
            return model;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<Buyer>> GetSearchData(BuyerSearch buyerSearch)
        {
            var pageSize = 10;
            var CurrentPage = 1;
            ServiceResult<List<Buyer>> model = new ServiceResult<List<Buyer>>();
            var source = db.Buyers.Where(x => x.IsActive == true);
            if (buyerSearch != null)
            {
                if (!string.IsNullOrEmpty(buyerSearch.Name))
                   source = source.Where(m => m.Name == buyerSearch.Name);
            }
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((buyerSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new Buyer{
                Id = x.Id, BuyLimit = x.BuyLimit, IsActive = x.IsActive, LogId = x.LogId,Name = x.Name
            }).ToList();
            model.TotalCount = count;
            return model; ;
        }      
        // GET: api/Buyers/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Buyer))]
        public IHttpActionResult GetBuyer(int id)
        {
            Buyer buyer = db.Buyers.Find(id);
            if (buyer == null)
            {
                return NotFound();
            }

            return Ok(buyer);
        }
    
        // PUT: api/Buyers/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuyer(int id, Buyer buyer)
        {
			var list = new List<Buyer>();
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			var data = db.Buyers.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
			data.IsActive = true;
			data.Name = buyer.Name;
			data.BuyLimit = buyer.BuyLimit;
			data.Id = buyer.Id;
            data.UpdatedOn = System.DateTime.UtcNow;
			try
			{
				db.SaveChanges();
				list.Add(buyer);
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
					var flag = buyer.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(data);
        }

        // POST: api/Buyers
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Buyer))]
        public IHttpActionResult PostBuyer(Buyer buyer)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
            buyer.CreatedOn = System.DateTime.UtcNow;
            buyer.UpdatedOn = System.DateTime.UtcNow;
			db.Buyers.Add(buyer);
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(buyer, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = buyer.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(buyer);
		}

        // DELETE: api/Buyers/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(Buyer))]
        public IHttpActionResult DeleteBuyer(int id)
        {
            Buyer buyer = db.Buyers.Find(id);
            if (buyer == null)
            {
                return NotFound();
            }
            buyer.IsActive = false;
            buyer.UpdatedOn = System.DateTime.UtcNow;
            //db.Buyers.Remove(buyer);
            db.SaveChanges();

            return Ok(buyer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuyerExists(int id)
        {
            return db.Buyers.Count(e => e.Id == id) > 0;
        }
    }
}