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

namespace POSApi.Controllers.Admin
{
	[RoutePrefix("api/template")]
	public class TemplatesController : ApiController
	{
		private GrandShoesEntities db = new GrandShoesEntities();
		public TemplatesController()
		{
			db.Configuration.LazyLoadingEnabled = false;
			db.Configuration.ProxyCreationEnabled = false;
		}
        // GET: api/Templates
        //count
        [HttpGet]
        [AllowAnonymous]
        [Route("count")]
        public IHttpActionResult Count()
        {
            var list = db.Templates.Where(x => x.IsActive == true).ToList().Count;
            return Ok(list);
        }
        [HttpGet]
		[AllowAnonymous]
		[Route("getAll")]
		public List<Template> GetTemplates(int PageNumber,int PageSize)
		{
            int skipRows = (PageNumber - 1) * PageSize;
            var list = db.Templates.Where(x=>x.IsActive == true)
                                    .OrderByDescending(o=>o.Id)
                                    .Skip(skipRows)
                                    .Take(PageSize)
                                    .ToList();
			return list;
		}
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Template> GetAll()
        {
            var list = db.Templates.Where(x => x.IsActive == true)
                                    .ToList();
            return list;
        }
        [HttpPost]
		[AllowAnonymous]
		[Route("getTemplateId")]
		public Template GetCode(string sku)
		{
			Template model = new Template();
			bool data = db.Templates.Any(x => x.Name == sku && x.IsActive == true);
			if (data == true)
			{
				model = db.Templates.Where(x => x.IsActive == true && x.Name == sku).FirstOrDefault();
			}
			else
			{
				model.Name = sku;
				model.IsActive = true;
				db.Templates.Add(model);
				db.SaveChanges();
				var list = db.Templates.Where(x => x.IsActive == true).ToList();
				model = list.LastOrDefault();
			}
			return model;
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("lengthmeasure")]
		public IHttpActionResult LengthMeasure()
		{
			var all = db.LengthMeasures.Where(x => x.IsActive == true).ToList();
			return Ok(all);
		}
		// GET: api/Templates/5
		[HttpGet]
		[AllowAnonymous]
		[Route("getDetail")]
		[ResponseType(typeof(Template))]
		public IHttpActionResult GetTemplate(int id)
		{
			Template template = db.Templates.Find(id);
			if (template == null)
			{
				return NotFound();
			}

			return Ok(template);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("checkTemplate")]
		public IHttpActionResult check(TemplateModel check)
		{
			if (check.Id > 0)
			{
				var user = db.Templates.Where(x => x.Id == check.Id).ToList();
				if (user[0].Name.Equals(check.Name))
				{
					return Ok(true);
				}
			}
			var result = db.Templates.Any(m => m.Name == check.Name &&

												m.Id != check.Id);
			return Ok(!result);
		}
		[HttpPost]
		[AllowAnonymous]
		[Route("edit")]
		[ResponseType(typeof(void))]
		public IHttpActionResult PutTemplate(int id, Template template)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != template.Id)
			{
				return BadRequest();
			}
			var result = db.Templates.Find(id);
			result.Height = template.Height;
			result.IsActive = template.IsActive;
			result.LengthId = template.LengthId;
			//result.LengthMeasure = template.LengthMeasure;
			result.Name = template.Name;
			result.TemplateHtml = template.TemplateHtml;
			result.Width = template.Width;
			//db.Entry(template).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				if (!TemplateExists(id))
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

		// POST: api/Templates
		[HttpPost]
		[AllowAnonymous]
		[Route("create")]
		[ResponseType(typeof(Template))]
		public IHttpActionResult PostTemplate(Template template)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Templates.Add(template);
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { }

			return CreatedAtRoute("DefaultApi", new { id = template.Id }, template);
		}

		// DELETE: api/Templates/5
		[HttpPost]
		[AllowAnonymous]
		[Route("delete")]
		public IHttpActionResult DeleteTemplate(int id)
		{
			Template template = db.Templates.Find(id);
            
			if (template == null)
			{
				return NotFound();
			}
            template.IsActive = false;
            try
            {
                db.SaveChanges();
            }catch(Exception ex) { }
			return Ok(template);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TemplateExists(int id)
		{
			return db.Templates.Count(e => e.Id == id) > 0;
		}
	}
}