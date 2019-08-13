﻿using System;
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
using PagedList;
using static Model.AreaPaginationModel;
using Helper;
using Helper;

namespace POSApi.Controllers
{
    [RoutePrefix("api/area")]
    public class AreasController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
		public AreasController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Areas
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Area> GetAreas()

        {
            var data = db.Areas.Where(x => x.IsActive == true).Include(x => x.User).OrderByDescending(x=>x.Id).ToList();
            return data;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getAreaPaging")]
        public ServiceResult<List<Area>> GetArea([FromUri]Paging paging)
        {
            ServiceResult<List<Area>> model = new ServiceResult<List<Area>>();
            var source = db.Areas.Where(x => x.IsActive == true)
                        .Include(x => x.User.Role).OrderByDescending(x=>x.Id).ToList();
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
        public ServiceResult<List<Area>> GetSearchData(AreaSearch areaSearch)
        {
            var pageSize = 10;
            
            ServiceResult<List<Area>> model = new ServiceResult<List<Area>>();
            var source = db.Areas.Where(x => x.IsActive == true).Include(x => x.User.Role);
            if (areaSearch.Name != null)
            {
                if (!string.IsNullOrEmpty(areaSearch.Name))
                    source = source.Where(m => m.Name.Contains(areaSearch.Name.ToLower()));
              //  var items = source.OrderBy(m => m.Id).Skip((areaSearch.Page ?? 1 - 1) * pageSize).Take(pageSize).ToList();
            }          
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((areaSearch.Page??1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            model.data = items.Select(x=>new Area {
                Id=x.Id, 
                ManagerID=x.ManagerID,
                Name=x.Name,
                User=x.User,
            }).ToList();
            model.TotalCount = count;
            return model; 
        }
        // GET: api/Areas/5
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Area))]
        public IHttpActionResult GetArea(int id)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }
        // PUT: api/Areas/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArea(int id, Area area)
        {
			var list = new List<Area>();
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			var data = db.Areas.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
			data.ManagerID = area.ManagerID;
			data.Name = area.Name;
			data.IsActive = area.IsActive;
            data.UpdatedOn = System.DateTime.UtcNow;
			//db.Entry(area).State = EntityState.Modified;
			try
			{
				db.SaveChanges();
				list.Add(area);
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
					var flag = area.CreateLog(pageName, logTable, UserId);
				}
			}
				return Ok(data);
        }
		
        // POST: api/Areas
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Area))]
        public IHttpActionResult PostArea(Area area)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			area.IsActive = true;
            area.CreatedOn = System.DateTime.UtcNow;
            area.UpdatedOn = System.DateTime.UtcNow;
			db.Areas.Add(area);
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{
					
					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(area, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = area.CreateLog(pageName,logTable, UserId);
				}
			}
			return Ok(true);

		}

        // DELETE: api/Areas/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(Area))]
        public IHttpActionResult DeleteArea(int id)
        {
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return NotFound();
            }
            area.IsActive = false;
            area.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();

            return Ok(area);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AreaExists(int id)
        {
            return db.Areas.Count(e => e.Id == id) > 0;
        }
    }
}