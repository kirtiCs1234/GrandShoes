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
using System.Data.Entity.Validation;

namespace POSApi.Controllers
{
    [RoutePrefix("api/supplier")]
    public class SuppliersController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public SuppliersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Suppliers
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Supplier> GetSuppliers()
        {
            var data= db.Suppliers.Where(x=>x.IsActive==true).ToList();
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSupplierId")]
        public Supplier GetCode(string sku)
        {
            Supplier model = new Supplier();
            bool data = db.Suppliers.Any(x => x.Code== sku && x.IsActive == true);
            if (data == true)
            {
                model = db.Suppliers.Where(x => x.IsActive == true && x.Code == sku).FirstOrDefault();
            }
            else
            {
                model.Code = sku;
                model.IsActive = true;
                db.Suppliers.Add(model);
                db.SaveChanges();
                var list= db.Suppliers.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getSupplierPaging")]
        public ServiceResult<List<Supplier>> GetSupplier([FromUri]Paging paging)

        {
            ServiceResult<List<Supplier>> model = new ServiceResult<List<Supplier>>();
            var source = db.Suppliers.Where(x => x.IsActive == true).OrderByDescending(x =>x.Id).ToList();
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
        [Route("SupplierAutocomplete")]
        public IHttpActionResult SupplierAutocomplete(string name)
        {
            var data = db.Suppliers.Where(x => x.IsActive == true && x.Name.Contains(name)).ToList().Select(m => new SupplierModel
            {
                Name = m.Name,
                Id = m.Id,
            }).ToList();
            return Ok(data);
        }
        [HttpGet]
        [Route("issupplierexist")]
        public IHttpActionResult isColorExist(int Id, string Code)
        {
            var color = db.Suppliers.Any(s => s.IsActive == true && s.Id != Id && s.Code == Code);
            if (color)
                return Ok(true);
            else
                return Ok(false);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<Supplier>> GetSearchData(SupplierSearch supplierSearch)

        {
            ServiceResult<List<Supplier>> model = new ServiceResult<List<Supplier>>();
            var source = db.Suppliers.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (supplierSearch != null)
            {
                if (supplierSearch.IsActive == false)
                {
                    source = db.Suppliers.Where(n => n.IsActive == false);
                }
                if (!string.IsNullOrEmpty(supplierSearch.Name))
                {
                    source = source.Where(x => x.Name.ToLower().Contains(supplierSearch.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(supplierSearch.ContactNumber))
                {
                    source = source.Where(x => x.ContactNumber.ToLower().Contains(supplierSearch.ContactNumber.ToLower()));
                }
                if (!string.IsNullOrEmpty(supplierSearch.Code))
                {
                    source = source.Where(x => x.Code.ToLower().Contains(supplierSearch.Code.ToLower()));
                }
                if (!string.IsNullOrEmpty(supplierSearch.PermanentPostalCode))
                {
                    source = source.Where(x => x.PermanentPostalCode.ToLower().Contains(supplierSearch.PermanentPostalCode.ToLower()));
                }
            }
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((supplierSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new Supplier
            {
                Id = x.Id,
                Code=x.Code,
                Name=x.Name,
                PermanentAddress1=x.PermanentAddress1,
                PermanentAddress2=x.PermanentAddress2,
                PermanentAddress3=x.PermanentAddress3,
                PermanentCity=x.PermanentCity,
                PermanentCountry=x.PermanentCountry,
                PermanentPostalCode=x.PermanentPostalCode,
                ContactNumber=x.ContactNumber,
                CorrespondanceAddress1=x.CorrespondanceAddress1,
                CorrespondanceAddress2=x.CorrespondanceAddress2,
                CorrespondanceAddress3=x.CorrespondanceAddress3,
                CorrespondanceCity=x.CorrespondanceCity,
                CorrespondanceCountry=x.CorrespondanceCountry,
                CorrespondancePostalCode=x.CorrespondancePostalCode,
                Limit=x.Limit,
                LogId=x.LogId,
                FaxNumber=x.FaxNumber,
                RegistrationDate=x.RegistrationDate,
                IsActive=x.IsActive
                
            }).ToList();
            model.TotalCount = count;

            return model; 
        }
        // GET: api/Suppliers/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult GetSupplier(int id)
        {
            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getSupplierName")]
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult GetSupplierName(int id)
        {
            var supplier = db.Suppliers.Where(x => x.IsActive == true && x.Id==id).ToList();
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }
        // PUT: api/Suppliers/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSupplier(int id, Supplier supplier)
        {
            DAL.Supplier model = new DAL.Supplier();
            model= db.Suppliers.Where(s => s.Id == supplier.Id).FirstOrDefault();
            model.Code = supplier.Code;
            model.ContactNumber = supplier.ContactNumber;
            model.CorrespondanceAddress1= supplier.CorrespondanceAddress1;
            model.CorrespondanceAddress2 = supplier.CorrespondanceAddress2;
            model.CorrespondanceAddress3 = supplier.CorrespondanceAddress3;
            model.CorrespondanceCity = supplier.CorrespondanceCity;
            model.CorrespondanceCountry = supplier.CorrespondanceCountry;
            model.CorrespondancePostalCode = supplier.CorrespondancePostalCode;
            model.FaxNumber = supplier.FaxNumber;
            model.Id = supplier.Id;
            model.Limit = supplier.Limit;
            model.LogId = supplier.LogId;
            model.Name = supplier.Name;
            model.PermanentAddress1 = supplier.PermanentAddress1;
            model.PermanentAddress2 = supplier.PermanentAddress2;
            model.PermanentAddress3= supplier.PermanentAddress3;
            model.PermanentCity = supplier.PermanentCity;
            model.PermanentCountry = supplier.PermanentCountry;
            model.PermanentPostalCode = supplier.PermanentPostalCode;
            model.RegistrationDate = supplier.RegistrationDate;
            model.UpdatedOn = System.DateTime.UtcNow;
            model.IsActive = true;
           // db.Suppliers.Add()
            db.SaveChanges();
            return Ok(true);
        }

        // POST: api/Suppliers
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult PostSupplier(Supplier supplier)
        {
            DAL.Supplier model = new DAL.Supplier();
           // model = db.Suppliers.Where(s => s.Id == supplier.Id).FirstOrDefault();
            model.Code = supplier.Code;
            model.ContactNumber = supplier.ContactNumber;
            model.CorrespondanceAddress1 = supplier.CorrespondanceAddress1;
            model.CorrespondanceAddress2 = supplier.CorrespondanceAddress2;
            model.CorrespondanceAddress3 = supplier.CorrespondanceAddress3;
            model.CorrespondanceCity = supplier.CorrespondanceCity;
            model.CorrespondanceCountry = supplier.CorrespondanceCountry;
            model.CorrespondancePostalCode = supplier.CorrespondancePostalCode;
            model.FaxNumber = supplier.FaxNumber;
            model.Id = supplier.Id;
            model.Limit = supplier.Limit;
            model.LogId = supplier.LogId;
            model.Name = supplier.Name;
            model.PermanentAddress1 = supplier.PermanentAddress1;
            model.PermanentAddress2 = supplier.PermanentAddress2;
            model.PermanentAddress3 = supplier.PermanentAddress3;
            model.PermanentCity = supplier.PermanentCity;
            model.PermanentCountry = supplier.PermanentCountry;
            model.PermanentPostalCode = supplier.PermanentPostalCode;
            model.RegistrationDate = supplier.RegistrationDate;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
            model.IsActive = true;
            db.Suppliers.Add(model);
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkSupplierCode")]
        public bool GetCodeId(string chk)
        {
            
            var data = db.Suppliers.Any(x => x.Code == chk && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("supplierExcelData")]
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult PostExcelSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Suppliers.Add(supplier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplier.Id }, supplier);
        }
		[HttpPost]
		[Route("createList")]
		public IHttpActionResult CreateList(Dictionary<int,Supplier> list)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
			{
                item.Value.CreatedOn = System.DateTime.UtcNow;
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.Code, "");
                db.Suppliers.Add(item.Value);
                try
                {
                    result[item.Key + "#" + item.Value.Code] = "Add";
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.Code] = ex.Message;
                    }
                }
            }
			return Ok(result);
		}
        [HttpPost]
        [Route("updateList")]
        public IHttpActionResult UpdateList(Dictionary<int, Supplier> list)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.Code, "");
                if(item.Value.Code != null)
                {
                    var obj = db.Suppliers.Where(x => x.Code == item.Value.Code).FirstOrDefault();
                    obj.Code = item.Value.Code;
                    obj.ContactNumber = item.Value.ContactNumber;
                    obj.CorrespondanceAddress1 = item.Value.CorrespondanceAddress1;
                    obj.CorrespondanceAddress2 = item.Value.CorrespondanceAddress2;
                    obj.CorrespondanceAddress3 = item.Value.CorrespondanceAddress3;
                    obj.CorrespondanceCity = item.Value.CorrespondanceCity;
                    obj.CorrespondanceCountry = item.Value.CorrespondanceCountry;
                    obj.CorrespondancePostalCode = item.Value.CorrespondancePostalCode;
                    obj.FaxNumber = item.Value.FaxNumber;
                    obj.IsActive = item.Value.IsActive;
                    obj.Limit = item.Value.Limit;
                    obj.LogId = item.Value.LogId;
                    obj.Name = item.Value.Name;
                    obj.PermanentAddress1 = item.Value.PermanentAddress1;
                    obj.PermanentAddress2 = item.Value.PermanentAddress2;
                    obj.PermanentAddress3 = item.Value.PermanentAddress3;
                    obj.PermanentCity = item.Value.PermanentCity;
                    obj.PermanentCountry = item.Value.PermanentCountry;
                    obj.UpdatedOn = System.DateTime.UtcNow;
                    obj.PermanentPostalCode = item.Value.PermanentPostalCode;
                    obj.RegistrationDate = item.Value.RegistrationDate;
                    try
                    {
                        result[item.Key + "#" + item.Value.Code] = "Update";
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null)
                        {
                            result[item.Key + "#" + item.Value.Code] = ex.Message;
                        }
                    }
                }
            }
            return Ok(result);
        }
        // DELETE: api/Suppliers/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(Supplier))]
        public IHttpActionResult DeleteSupplier(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            supplier.IsActive = false;
            supplier.UpdatedOn = System.DateTime.UtcNow;
            //db.Suppliers.Remove(supplier);
            db.SaveChanges();

            return Ok(supplier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierExists(int id)
        {
            return db.Suppliers.Count(e => e.Id == id) > 0;
        }

    }
}