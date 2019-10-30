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
using Helper;
using Newtonsoft.Json;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/season")]
    public class SeasonsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public SeasonsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Seasons
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Season> GetSeasons()
        {
            var list= db.Seasons.Where(x=>x.IsActive==true).OrderByDescending(x => x.Id).ToList();
            return list;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getSeasonPaging")]
        public ServiceResult<List<Season>> GetArea([FromUri]Paging paging)

        {
            ServiceResult<List<Season>> model = new ServiceResult<List<Season>>();
            var source = db.Seasons.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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
        public ServiceResult<List<Season>> GetSearchData(SeasonSearch seasonSearch)

        {
            ServiceResult<List<Season>> model = new ServiceResult<List<Season>>();
            var source = db.Seasons.Where(x => x.IsActive == true);
            var pageSize = 10;
            if (seasonSearch != null)
            {
                if (!string.IsNullOrEmpty(seasonSearch.Code))
                    source = source.Where(m => m.Code == seasonSearch.Code);
            }
            int count = source.Count();
            var items = source.OrderByDescending(x => x.Id).Skip(((seasonSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new Season
            {
                Id=x.Id,
                Code=x.Code,
                Description=x.Description,
                IsActive=x.IsActive
            }).ToList();
            model.TotalCount = count;
            return model; ;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        // GET: api/Seasons/5
        [ResponseType(typeof(Season))]
        public IHttpActionResult GetSeason(int id)
        {
            Season season = db.Seasons.Find(id);
            if (season == null)
            {
                return NotFound();
            }
            return Ok(season);
        }
		[HttpPost]
		[Route("CreateList")]
		public IHttpActionResult CreateList(Dictionary<int, Season> list)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
			{
                item.Value.CreatedOn = System.DateTime.UtcNow;
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.Code, "");
                try
                {
                    result[item.Key + "#" + item.Value.Code] = "Add";
                    db.Seasons.Add(item.Value);
                    db.SaveChanges();
                }
                catch(Exception ex)
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
        [Route("UpdateList")]
        public IHttpActionResult UpdateList(Dictionary<int, Season> list)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            
            foreach (var item in list)
            {
                if(item.Value.Code != null)
                {
                    item.Value.UpdatedOn = System.DateTime.UtcNow;
                    result.Add(item.Key + "#" + item.Value.Code, "");
                    var obj = db.Seasons.Where(x => x.Code == item.Value.Code).FirstOrDefault();
                    obj.Code = item.Value.Code;
                    obj.Description = item.Value.Code;
                    obj.UpdatedOn = System.DateTime.UtcNow;
                    obj.IsActive = item.Value.IsActive;
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
        [HttpGet]
        [AllowAnonymous]
        [Route("getSeasonDescription")]
        [ResponseType(typeof(Season))]
        public IHttpActionResult GetSeasonDescription(int id)
        {
            var season = db.Seasons.Where(x=>x.IsActive==true && x.Id==id).ToList();
            if (season == null)
            {
                return NotFound();
            }
            return Ok(season);
        }

        // PUT: api/Seasons/5
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeason(int id, Season season)
        {
			var list = new List<Season>();
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			string logTable = "";
			var data = db.Seasons.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
			data.Code = season.Code;
			data.Description = season.Description;
            data.UpdatedOn = System.DateTime.UtcNow;
			try
			{
				db.SaveChanges();
				list.Add(season);
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
					var flag = season.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(data);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkSeasonCode")]
        public bool GetPhoneNumber(Season season)
        {
            if (season.Id > 0)
            {
                var code = db.Seasons.Where(x => x.Id == season.Id && x.IsActive == true).FirstOrDefault();
                if (code.Code.Equals(season.Code))
                {
                    return false;
                }

            }
            var data = db.Seasons.Any(x => x.Code == season.Code && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkSeasonCode1")]
        public bool GetCode(string chk)
        {
            var data = db.Seasons.Any(x => x.Code == chk && x.IsActive == true);
            return data;
        }
        // POST: api/Seasons
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(Season))]
        public IHttpActionResult PostSeason(Season season)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			Season model = new Season();
            model.Code = season.Code;
            model.Description = season.Description;
            model.IsActive = true;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
            db.Seasons.Add(model);
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(season, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = season.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getSeasonId")]
        public Season GetSeasonCode(string sku)
        {
            Season model = new Season();
            bool data = db.Seasons.Any(x => x.Code == sku && x.IsActive == true);
            if (data == true)
            {
                model = db.Seasons.Where(x => x.IsActive == true && x.Code== sku).FirstOrDefault();
            }
            else
            {
                model.Code = sku;
                model.IsActive = true;
                db.Seasons.Add(model);
                db.SaveChanges();
                var list = db.Seasons.Where(x => x.IsActive == true).ToList();
                model = list.LastOrDefault();
            }
            return model;
        }
        [HttpGet]
        [Route("SeasonAutocomplete")]
        public IHttpActionResult ColorsAutocomplete(string name)
        {
            var data = db.Seasons.Where(x => x.IsActive == true && x.Description.Contains(name)).ToList().Select(m => new Model.SeasonModel
            {
                Description = m.Description,
                Id = m.Id,
            }).ToList();
            return Ok(data);
        }
        // DELETE: api/Seasons/5
        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(Season))]
        public IHttpActionResult DeleteSeason(int id)
        {
            Season season = db.Seasons.Find(id);
            if (season == null)
            {
                return NotFound();
            }
            season.IsActive = false;
            season.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();

            return Ok(season);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeasonExists(int id)
        {
            return db.Seasons.Count(e => e.Id == id) > 0;
        }
    }
}