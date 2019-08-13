﻿using DAL;
using Model;
using Model.Log;
using Helper.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace POSApi.Controllers
{
    [RoutePrefix("api/log")]
    public class LogController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public LogController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Logs
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public IHttpActionResult GetLogs()
        {
            var list = db.Logs.Where(x => x.IsActive == true)
                                .Include(x => x.User)
                                .Include(x => x.ActionLog).Include(x => x.PageName)
								.OrderByDescending(x => x.Id)
                                .ToList().RemoveReferences();

            return Ok(list);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getLogPaging")]
        public ServiceResult<List<Log>> GetArea([FromUri]Paging paging)
        {
            ServiceResult<List<Log>> model = new ServiceResult<List<Log>>();
            var source = db.Logs.Include(x => x.ActionLog).AsNoTracking()
                .Include(x => x.User).AsNoTracking()
                .Include(x => x.PageName).AsNoTracking()
                .Where(x => x.IsActive == true).OrderByDescending(x =>x.Id)
                .ToList();
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
        //Search
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<Log>> GetSearchData(LogSearch search)
        {
            var pageSize = 10;
            var CurrentPage = 1;
            ServiceResult<List<Log>> model = new ServiceResult<List<Log>>();
            var source = db.Logs.Include(x => x.ActionLog).AsNoTracking()
                .Include(x => x.User).AsNoTracking()
                .Include(x => x.PageName).AsNoTracking()
                .Where(x => x.IsActive == true);
            
            int count = source.Count();
            var chaeck = source.ToList();

            var items = source.OrderByDescending(m => m.Id).Skip(((search.Page ?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            //model.data = items.Select(x => new Area
            //{
            //    Id = x.Id,
            //    ManagerID = x.ManagerID,
            //    Name = x.Name
            //}).ToList();
            model.data = items;
            model.TotalCount = count;
            return model; ;
        }


        //Paging
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("getLogPaging")]
        //public ServiceResult<List<Log>> GetLogPaging([FromUri]Paging paging)
        //{

        //    ServiceResult<List<Log>> model = new ServiceResult<List<Log>>();
        //    var source = db.Logs.Include(x => x.ActionLog).AsNoTracking()
        //        .Include(x => x.User).AsNoTracking()
        //        .Include(x => x.Page).AsNoTracking()
        //        .Where(x => x.IsActive == true)
        //        .ToList();

        //    int count = source.Count();
        //    int CurrentPage = paging.pageNumber;
        //    int PageSize = paging.pageSize;
        //    int TotalCount = count;
        //    int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        //    var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        //    var previousPage = CurrentPage > 1 ? "Yes" : "No";
        //    var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
        //    model.TotalCount = count;
        //    model.data = items;
        //    return model;
        //}
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(Log))]
        public Log GetStaff(int id)
        {
            //Log log = db.Logs.Find(id);
            var logByIddetals = db.Logs.Where(x => x.Id == id && x.IsActive == true)
                                .Include(x => x.User)
                                .Include(x => x.ActionLog)
                                .Include(x => x.PageName)
                                .FirstOrDefault();
            
            return logByIddetals;
        }
   

    }
}
