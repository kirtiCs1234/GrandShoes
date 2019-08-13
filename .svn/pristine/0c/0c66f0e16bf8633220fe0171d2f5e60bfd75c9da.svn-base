using DAL;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace POSApi.Controllers
{
    [RoutePrefix("api/SMIBranchDefault")]
    public class SMIBranchDefaultControllerController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public SMIBranchDefaultControllerController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<SMIBranchDefault> GetBranch()
        {
            SMIBranchDefaultModel sbmBranchList = new SMIBranchDefaultModel();
            var smi = db.SMIBranchDefaults.Where(x => x.IsActive == true).ToList();
            return smi;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("getById")]
        public List<SMIBranchDefault> GetBranchById(int id)
        {
            SMIBranchDefaultModel sbmBranchList = new SMIBranchDefaultModel();
            var branches = db.Branches.Where(x => x.IsActive == true).ToList();
            var smi = db.SMIBranchDefaults.Where(x => x.IsActive == true && x.FromBranchId==id ).ToList();
            foreach(var branch in branches)
            {
                if(!smi.Any( x => x.ToBranchId == branch.Id))
                {
                    if(branch.Id != id)
                    {
                        SMIBranchDefault branchDefault = new DAL.SMIBranchDefault();
                        branchDefault.FromBranchId = id;
                        branchDefault.ToBranchId = branch.Id;
                        branchDefault.IsPreferredRoute = false;
                        branchDefault.IsActive = true;
                        branchDefault.Branch1 = branch;
                        smi.Add(branchDefault);
                    }
                }
            }
            //var smi = db.SMIBranchDefaults.Include("Branch").Where(x => x.IsActive == true && x.FromBranchId == id).ToList();
            return smi;
        }
        [HttpPost]
        [Route("smiPut")]
        [AllowAnonymous]
        [ResponseType(typeof(SMIBranchDefault))]
        public IHttpActionResult SMIBranchDefault(SMIBranchDefaultModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            foreach (var item in model.BranchDestination)
            {
                var SMIBranch = new SMIBranchDefault();
                SMIBranch.Id = item.Id;
                SMIBranch.FromBranchId = item.FromBranchId;
                SMIBranch.ToBranchId = item.ToBranchId;
                SMIBranch.Distance = Convert.ToDecimal(item.Distance);
                SMIBranch.IsPreferredRoute = true;
                SMIBranch.IsActive = true;

               // SMIBranch.LogId = CommonFunction.LogMethods("DSdf", "1", 1);
                if (item.Id == 0)
                {
                    if (item.Distance != null)
                    {
                        //model.LogId = CommonFunction.LogMethods("DSdf", 1, 1);
                        db.SMIBranchDefaults.Add(SMIBranch);
                        db.SaveChanges();
                    }
                }

                if (item.Id != 0)
                {
                    db.Entry(SMIBranch).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Ok(model);
            //return StatusCode(HttpStatusCode.NoContent);
        }
       
    }
}
