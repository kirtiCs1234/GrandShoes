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
using Helper.ExtensionMethod;

namespace POSApi.Controllers.Admin
{
    [RoutePrefix("api/markDownBranch")]
    public class MarkDownBranchesController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public MarkDownBranchesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [Route("getDetails")]
        // GET: api/MarkDownBranches
        public List<MarkDownBranch> GetMarkDownBranches()
        {
           var list= db.MarkDownBranches.Where(x=>x.IsActive==true).Include(x=>x.Branch).OrderByDescending(x => x.Id).ToList().RemoveReferences();
            return list;
        }
        [HttpGet]
        [Route("getDetail")]
        // GET: api/MarkDownBranches/5
        [ResponseType(typeof(MarkDownBranch))]
        public IHttpActionResult GetMarkDownBranch(int id)
        {
            MarkDownBranch markDownBranch = db.MarkDownBranches.Find(id).RemoveReferences();
            if (markDownBranch == null)
            {
                return NotFound();
            }
            return Ok(markDownBranch);
        }
        [HttpPost]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarkDownBranch(int id, MarkDownBranch markDownBranch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != markDownBranch.Id)
            {
                return BadRequest();
            }
            db.Entry(markDownBranch).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkDownBranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpPost]
        [Route("create")]
        // POST: api/MarkDownBranches
        [ResponseType(typeof(MarkDownBranch))]
        public IHttpActionResult PostMarkDownBranch(MarkDownAddModel model)
        {
            List<MarkDownBranch> mark = new List<MarkDownBranch>();
            var markDownID = db.MarkDowns.Where(x => x.IsActive == true).LastOrDefault().Id;
            foreach (var item in model.BranchList)
            {
                MarkDownBranch m = new MarkDownBranch();
                m.BranchID = Convert.ToInt16(item);
                m.MarkDownID = markDownID;
                m.IsActive = true;
                m.CreatedOn = System.DateTime.UtcNow;
                m.UpdatedOn = System.DateTime.UtcNow;
                mark.Add(m);
            }
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [Route("delete")]
        // DELETE: api/MarkDownBranches/5
        [ResponseType(typeof(MarkDownBranch))]
        public IHttpActionResult DeleteMarkDownBranch(int id)
        {
            MarkDownBranch markDownBranch = db.MarkDownBranches.Find(id);
            if (markDownBranch == null)
            {
                return NotFound();
            }
            markDownBranch.UpdatedOn = System.DateTime.UtcNow;
            db.MarkDownBranches.Remove(markDownBranch);
            db.SaveChanges();
            return Ok(markDownBranch);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool MarkDownBranchExists(int id)
        {
            return db.MarkDownBranches.Count(e => e.Id == id) > 0;
        }
    }
}