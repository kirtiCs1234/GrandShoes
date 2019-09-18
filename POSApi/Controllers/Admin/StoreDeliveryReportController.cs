using DAL;
using Helper.ExtensionMethod;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers
{
    [RoutePrefix("api/StoreDeliveryReport")]
    public class StoreDeliveryReportController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StoreDeliveryReportController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        //Searching
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<StockDistribution>> GetSearchData(Model.StockDistributionSearch order)
        {
            var pageSize = 10;

            ServiceResult<List<StockDistribution>> model = new ServiceResult<List<StockDistribution>>();
            var source = db.StockDistributions.Where(x => x.IsActive == true).Include(x => x.Branch).Include(x=>x.Product).Include(x=>x.StockDistributionStatu).Include(x=>x.StockDistributionSummary).Include(x=>x.Product);
            if (order!= null)
            {
                if (!string.IsNullOrEmpty(order.ProductSKU))
                {
                    source = source.Where(m => m.Product.Barcode==order.ProductSKU);
                }
                if (order.BranchId>0)
                    source = source.Where(m => m.BranchId==order.BranchId);
                //  var items = source.OrderBy(m => m.Id).Skip((areaSearch.Page ?? 1 - 1) * pageSize).Take(pageSize).ToList();
                if (order.FromDate != null && order.ToDateS!= null)
                {
                    source = db.StockDistributions.Where(entry => entry.DistributionDate >=order.FromDate
                  && entry.DistributionDate <= order.ToDateS);
                }
            }
            int count = source.Count();
            var items = source.OrderByDescending(m => m.Id).Skip(((order.Page ?? 1) - 1) * pageSize)
                        .Take(pageSize).ToList();
            model.data=items.RemoveReferences();
            model.TotalCount = count;
            return model;
        }
        //Pagging
        [HttpGet]
        [AllowAnonymous]
        [Route("getPaging")]
        public ServiceResult<List<StockDistribution>> GetPaging([FromUri]Paging paging)
        {
            ServiceResult<List<StockDistribution>> model = new ServiceResult<List<StockDistribution>>();
            var source = db.StockDistributions
                        .Include(x => x.Branch).Include(x=>x.Product).AsNoTracking()
                        .AsNoTracking()                      
                        .Where(x => x.IsActive == true)
                        .ToList().RemoveReferences();
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
    }
}
