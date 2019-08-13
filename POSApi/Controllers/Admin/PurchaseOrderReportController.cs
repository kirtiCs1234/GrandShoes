﻿using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.Controllers
{
    [RoutePrefix("api/PurchaseReportOrder")]
    public class PurchaseOrderReportController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public PurchaseOrderReportController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
   
        //Searching
        [HttpPost]
        [AllowAnonymous]
        [Route("getSearchData")]
        public ServiceResult<List<PurchaseOrder>> GetSearchData(Model.PurchaseOrderModel order)
        {
            ServiceResult<List<PurchaseOrder>> model = new ServiceResult<List<PurchaseOrder>>();
            var source = db.PurchaseOrders.Include("PurchaseOrderStatu").Where(x => x.IsActive == true);
            if (order != null && order.PurchaseOrderStatusId > 0)
            {
                source = source.Where(m => m.PurchaseOrderStatusId == order.PurchaseOrderStatusId
                && (m.OrderDate >= order.FromDate && m.OrderDate <= order.ToDate));
            }
            else
            {
                source = source.Where(m => m.OrderNumber.ToLower().Equals(order.OrderNumber.ToLower()) &&
                                m.IsActive == true);
            }
            var result = source.ToList();
            int count = result.Count();
            model.TotalCount = count;
            model.data = result;
            return model;
        }

        //Pagging
        [HttpGet]
        [AllowAnonymous]
        [Route("getPurchasePaging")]
        public ServiceResult<List<PurchaseOrder>> GetStaffMemberPaging([FromUri]Paging paging)
        {
            ServiceResult<List<PurchaseOrder>> model = new ServiceResult<List<PurchaseOrder>>();
            var source = db.PurchaseOrders.Include("PurchaseOrderStatu")
                .Where(x => x.IsActive == true)
                .ToList();
            int count = source.Count();
            int CurrentPage = paging.pageNumber;
            int PageSize = paging.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            model.TotalCount = count;
            model.data = items;
            return model;
        }
    }
}
