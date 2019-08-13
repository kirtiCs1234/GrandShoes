﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace POSApi.Controllers
{
    [RoutePrefix("api/staffStatus")]
    public class StaffStatusController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public StaffStatusController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<StaffStatu> GetStaffStatus()
        {
            var staffStatus = db.StaffStatus.Where(x => x.IsActive == true).ToList();
            return staffStatus;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(StaffStatu))]
        public IHttpActionResult GetStaffStatus(int id)
        {
            var staffStuss = db.StaffStatus.Find(id);
            if(staffStuss == null)
            {
                return NotFound();
            }
            return Ok(staffStuss);
        }
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(StaffStatu staff)
        {
            StaffStatu model = new StaffStatu();
            return Ok(true);
        }
    }
}
