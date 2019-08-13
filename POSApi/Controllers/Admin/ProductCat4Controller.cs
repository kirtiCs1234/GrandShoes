﻿using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/ProductCat4")]
    public class ProductCat4Controller : ApiController
    {
        GrandShoesEntities db = new GrandShoesEntities();
        public ProductCat4Controller()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [Route("getDetails")]
        public IHttpActionResult GetDetails()
        {
            var list = db.ProductCat4.Where(x => x.IsActive == true).ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("getDetail")]
        public IHttpActionResult GetDetail(int id)
        {
            var data = db.ProductCat4.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            return Ok(data);
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(ProductCat4 cat)
        {
            ProductCat4 model = new ProductCat4();
            model.CateName = cat.CateName;
            model.Code = cat.Code;
            model.CreatedOn = System.DateTime.UtcNow;
            model.UpdatedOn = System.DateTime.UtcNow;
            model.IsActive = true;
            db.ProductCat4.Add(model);
            db.SaveChanges();
            return Ok(true);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getCat4Paging")]
        public ServiceResult<List<ProductCat4>> GetArea([FromUri]Paging paging)
        {
            ServiceResult<List<ProductCat4>> model = new ServiceResult<List<ProductCat4>>();
            var source = db.ProductCat4.Where(x => x.IsActive == true)
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
        public ServiceResult<List<ProductCat4>> GetSearchData(ProductCatSearch areaSearch)
        {
            var pageSize = 10;
            ServiceResult<List<ProductCat4>> model = new ServiceResult<List<ProductCat4>>();
            var source = db.ProductCat4.Where(x => x.IsActive == true);
            if (areaSearch.Catename != null)
            {
                if (!string.IsNullOrEmpty(areaSearch.Catename))
                    source = source.Where(m => m.CateName.Contains(areaSearch.Catename.ToLower()));
                //  var items = source.OrderBy(m => m.Id).Skip((areaSearch.Page ?? 1 - 1) * pageSize).Take(pageSize).ToList();
            }
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((areaSearch.Page ?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            model.data = items.Select(x => new ProductCat4
            {
                Id = x.Id,
                Code = x.Code,
                CateName = x.CateName,
                IsActive = x.IsActive,
            }).ToList();
            model.TotalCount = count;
            return model;
        }
        [HttpPost]
        [Route("edit")]
        public IHttpActionResult Edit(int id, ProductCat4 cat)
        {
            var data = db.ProductCat4.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            data.CateName = cat.CateName;
            data.Code = cat.Code;
            data.UpdatedOn = System.DateTime.UtcNow;
            data.IsActive = true;
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            var data = db.ProductCat4.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            data.IsActive = false;
            data.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(true);
        }
    }
}
