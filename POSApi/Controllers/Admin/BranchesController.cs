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
using System.Data.Entity.Validation;
using Newtonsoft.Json;
using Helper;

namespace POSApi.Controllers
{
    [RoutePrefix("api/branch")]
    public class BranchesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public BranchesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Branches
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Branch> GetBranches()
        {
            var list= db.Branches.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList();
            return list;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getBranchPaging")]
        public ServiceResult<List<Branch>> GetArea([FromUri]Paging paging)

        {
            ServiceResult<List<Branch>> model = new ServiceResult<List<Branch>>();
            var source = db.Branches.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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
		[Route("getByName")]
		public IHttpActionResult GetByName(string name)
		{
			var data = db.Branches.Where(x => x.IsActive == true && x.Name.Contains(name)).FirstOrDefault();
			return Ok(data);
		}
        [HttpPost]
		[Route("createList")]
		public IHttpActionResult CreateList(Dictionary<int,Branch> list)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
			{
                item.Value.CreatedOn= System.DateTime.UtcNow;
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key+"#"+item.Value.BranchCode, "");
                db.Branches.Add(item.Value);
                try
                {
                    result[item.Key + "#" + item.Value.BranchCode] = "Add";
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.BranchCode] = ex.Message;
                    }
                }
            }
			
			return Ok(result);
		}

        [HttpPost]
        [Route("updateList")]
        public IHttpActionResult UpdateList(Dictionary<int, Branch> list)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.BranchCode, "");
                if(item.Value.BranchCode != null)
                {
                    try
                    {
                        result[item.Key + "#" + item.Value.BranchCode] = "Update";
                        var obj = db.Branches.Where(x => x.BranchCode == item.Value.BranchCode).FirstOrDefault();
                        obj.BranchCode = item.Value.BranchCode;
                        obj.AddressLine1 = item.Value.AddressLine1;
                        obj.AddressLine2 = item.Value.AddressLine2;
                        obj.AddressLine3 = item.Value.AddressLine3;
                        obj.AreaCode = item.Value.AreaCode;
                        obj.DateClosed = item.Value.DateClosed;
                        obj.DateOpen = item.Value.DateOpen;
                        obj.IsActive = item.Value.IsActive;
                        obj.IsClosed = item.Value.IsClosed;
                        obj.IsHeadOffice = item.Value.IsHeadOffice;
                        obj.IsSendStock = item.Value.IsSendStock;
                        obj.LogId = item.Value.LogId;
                        obj.Name = item.Value.Name;
                        obj.UpdatedOn = System.DateTime.UtcNow;
                        obj.PostalCode = item.Value.PostalCode;
                        obj.StoreSize = item.Value.StoreSize;
                        obj.Telephone = item.Value.Telephone;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null)
                        {
                            result[item.Key + "#" + item.Value.BranchCode] = ex.Message;
                        }
                    }
                }
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("getBranchAddress")]
        public Branch GetBranchAddress(string name)
        {
            var data = db.Branches.Where(x => x.IsActive == true && x.Name == name).FirstOrDefault();
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<Branch>> GetSearchData(BranchSearch branchSearch)
        {
            ServiceResult<List<Branch>> model = new ServiceResult<List<Branch>>();
            var source = db.Branches.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (branchSearch != null)
            {

                if (!string.IsNullOrEmpty(branchSearch.BranchCode))

                    source = db.Branches.Where(m => m.BranchCode.Contains(branchSearch.BranchCode.ToLower()));
                if(!string.IsNullOrEmpty(branchSearch.Name))
                    source = db.Branches.Where(m => m.Name.Contains(branchSearch.Name.ToLower()));
            }
 
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((branchSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new Branch
            {
                Id = x.Id,
                BranchCode = x.BranchCode,
                AddressLine1 = x.AddressLine1,
                AddressLine2 = x.AddressLine2,
                AddressLine3 = x.AddressLine3,
                AreaCode = x.AreaCode,
                DateClosed = x.DateClosed,
                DateOpen = x.DateOpen,
                IsActive = true,
                IsClosed = x.IsClosed,
                IsHeadOffice = x.IsHeadOffice,
                IsSendStock = x.IsSendStock,
                LogId = x.LogId,
                PostalCode = x.PostalCode,
                StoreSize = x.StoreSize,
                Telephone = x.Telephone,
                Name = x.Name
            }).ToList();
            model.TotalCount = count;
           
            return model; ;
        }

        // GET: api/Branches/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Branch))]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }
        [HttpPost]
        [Route("BranchAutocomplete")]
        public IHttpActionResult BranchAutocomplete(string name)
        {
           // var list = db.StockDistributions.Where(x => x.IsActive == true).ToList();
            var data = db.Branches.Where(x => x.Name.StartsWith(name) && x.IsActive==true).ToList().Select(m => new BranchModel
            {
                Id = m.Id,
                Name=m.Name
            }).ToList();
            return Ok(data);
        }
        // PUT: api/Branches/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranch(int id, Branch branch)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			var list = new List<Branch>();
			Branch model = new Branch();
            model = db.Branches.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            model.AddressLine1 = branch.AddressLine1;
            model.AddressLine2 = branch.AddressLine2;
            model.AddressLine3 = branch.AddressLine3;
            model.AreaCode = branch.AreaCode;
            model.BranchCode = branch.BranchCode;
            model.DateClosed = branch.DateClosed;
            model.DateOpen = branch.DateOpen;
            model.IsActive = true;
            model.IsClosed = branch.IsClosed;
            model.IsHeadOffice = branch.IsHeadOffice;
            model.IsSendStock = branch.IsSendStock;
            model.LogId = branch.LogId;
            model.Name = branch.Name;
            model.PostalCode = branch.PostalCode;
            model.StoreSize = branch.StoreSize;
            model.Telephone = branch.Telephone;
            model.UpdatedOn = System.DateTime.UtcNow;
			try
			{
				db.SaveChanges();
				list.Add(model);
				return Ok(true);
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
					var flag = branch.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkBranchCode")]
        public bool GetCode(Branch branch)
        {
            if (branch.Id > 0)
            {
                var code = db.Branches.Where(x => x.Id == branch.Id && x.IsActive == true).FirstOrDefault();
                if (code.BranchCode.Equals(branch.BranchCode))
                {
                    return false;
                }

            }
            var data = db.Branches.Any(x => x.BranchCode == branch.BranchCode && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkBranchName")]
        public bool GetName(Branch branch)
        {
            if (branch.Id > 0)
            {
                var name = db.Branches.Where(x => x.Id == branch.Id && x.IsActive == true).FirstOrDefault();
                if (name.Name.Equals(branch.Name))
                {
                    return false;
                }

            }
            var data = db.Branches.Any(x => x.Name == branch.Name && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkBranchCode")]
        public bool GetCode(string chk)
        {

            var data = db.Branches.Any(x => x.BranchCode == chk && x.IsActive == true);
            return data;
        }
      
        // POST: api/Branches
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Branch))]
        public IHttpActionResult PostBranch(BranchModel branch)
        {
                if (branch.Id == 0)
                {
                    Branch model = new Branch();
                    model.AddressLine1 = branch.AddressLine1;
                    model.AddressLine2 = branch.AddressLine2;
                    model.AddressLine3 = branch.AddressLine3;
                    model.AreaCode = branch.AreaCode;
                    model.BranchCode = branch.BranchCode;
                    model.DateClosed = Convert.ToDateTime(branch.DateClosed);
                    model.DateOpen = Convert.ToDateTime(branch.DateOpen);
                    model.IsActive = true;
                    model.IsClosed = branch.IsClosed;
                    model.IsHeadOffice = branch.IsHeadOffice;
                    model.IsSendStock = branch.IsSendStock;
                    model.LogId = branch.LogId;
                    model.Name = branch.Name;
                    model.CreatedOn = System.DateTime.UtcNow;
                    model.UpdatedOn = System.DateTime.UtcNow;
                    model.PostalCode = branch.PostalCode;
                    model.StoreSize = branch.StoreSize;
                    model.Telephone = branch.Telephone;
                    db.Branches.Add(model);
                }
                else
                    db.Entry(branch).State = EntityState.Modified;
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(branch, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = branch.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
           
        }
        [HttpPost]
        [Route("checkBranch")]
        public IHttpActionResult CheckBranch(Dictionary<int, string> list)
        {
            var obj = new Dictionary<int, bool>();
            if (list != null && list.Count > 0)
            {
                var branchList = db.Branches.Where(x => x.IsActive == true);
                var code = list.Select(x => x.Value).Distinct().ToList();
                if (code != null && code.Count > 0)
                {
                    branchList = branchList.Where(x => code.Contains(x.BranchCode));
                }
                var Branch = branchList.ToList();
                foreach (var item in list)
                {
                    var result = Branch.Any(x => x.BranchCode == item.Value);
                    obj.Add(item.Key, result);
                }
            }
            return Ok(obj);
        }
        // DELETE: api/Branches/5
        [HttpPost]
        [AllowAnonymous]
        [Route("Delete")]
        [ResponseType(typeof(Branch))]
        public IHttpActionResult DeleteBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            branch.IsActive = false;
            branch.UpdatedOn = System.DateTime.UtcNow;
            //db.Branches.Remove(branch);
            db.SaveChanges();

            return Ok(branch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchExists(int id)
        {
            return db.Branches.Count(e => e.Id == id) > 0;
        }
    }
}