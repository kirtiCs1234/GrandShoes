﻿using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POS.Areas;
using Helper;

namespace POS.Areas.Admin.Controllers
{
    [CustomAuth(PageSession.Template)]
	public class TemplateController : BaseController
	{
		// GET: Admin/Template
		public readonly int PageSize = 10;
		public ActionResult Index(int? id)
		{

			var measure = Services.TemplateService.LengthMeasure();
			ViewBag.LengthId = new SelectList(measure, "Id", "LengthCode");
			if (id == 0 || id == null)
			{
				TemplateModel ckEditor = new TemplateModel();
				return View(ckEditor);
			}
			else
			{
				var result = Services.TemplateService.GetById(id);
				return View(result);
			}
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Index(TemplateModel ckEditor)
		{
			var model = new TemplateModel();
			if (ckEditor.Id == 0)
			{
				model.Name = ckEditor.Name;
				model.TemplateHtml = ckEditor.TemplateHtml;
				model.Width = ckEditor.Width;
				model.Height = ckEditor.Height;
				model.LengthId = ckEditor.LengthId;
				model.IsActive = true;

				//var result = db.Result<TemplateModel>("api/template/create", model, db.post);
				var result = Services.TemplateService.Create(model);
			}
			else
			{
				model.Id = ckEditor.Id;
				model.Name = ckEditor.Name;
				model.TemplateHtml = ckEditor.TemplateHtml;
				model.Width = ckEditor.Width;
				model.Height = ckEditor.Height;
				model.LengthId = ckEditor.LengthId;
				model.IsActive = true;
				//var result = db.Result<TemplateModel>("api/template/edit", model, db.post);
				var result = Services.TemplateService.Edit(model);

			}
			return RedirectToAction("list", "template");
			//return View(model);
		}
		[HttpGet]
		[AllowAnonymous]
		[ValidateInput(false)]
		public ActionResult list(TemplateModel filter, int? page)
		{
			var pageCount = Services.TemplateService.PageCount();
			//var model = db.Result<List<TemplateModel>>("api/template/company/getAll?CompanyId=" + user.CompanyId, db.body, db.get);
			var model = Services.TemplateService.GetAllWithPaging(page ?? 1, PageSize);
			ViewBag.model = model;
			int PageNumber = page ?? 1;
			return View(model.ToCustomPagedList(PageNumber, PageSize, pageCount));
		}
		public ActionResult getLength(decimal len, int size)
		{
			var result = Calculate(len, size);
			return Json(result, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult ShowTemplate(TemplateModel model)
		{
			TemplateVariable tVariable = new TemplateVariable();
			TemplateValue tValue = new TemplateValue();
			string Barcode = @"<svg id = 'barcode'></svg><script>JsBarcode('#barcode', '" + tValue.Barcode + "', {width: 2,height: 40});</script>";
			//        //format: "pharmacode",
			//        //lineColor: "#0aa",
			//+ 'width: 2,'
			//+ 'height: 40,'
			//        // displayValue: false
			//+ '});</script>';
			string updateHtml = "";
			if (model.TemplateHtml != null)
			{
				string html = model.TemplateHtml;
				updateHtml = html.Replace(tVariable.barcode, Barcode);
				updateHtml = updateHtml.Replace(tVariable.name, tValue.Name);
				updateHtml = updateHtml.Replace(tVariable.branchname, tValue.BranchName);
				updateHtml = updateHtml.Replace(tVariable.branchemail, tValue.BranchEmail);
				updateHtml = updateHtml.Replace(tVariable.productname, tValue.ProductName);
				updateHtml = updateHtml.Replace(tVariable.productid, tValue.ProductId);
				updateHtml = updateHtml.Replace(tVariable.unit, tValue.Unit);
				updateHtml = updateHtml.Replace(tVariable.quantity, tValue.Quantity);
				updateHtml = updateHtml.Replace(tVariable.discount, tValue.Discount);
				updateHtml = updateHtml.Replace(tVariable.price, tValue.Price);
				updateHtml = updateHtml.Replace(tVariable.priceafterdiscount, tValue.PriceAfterDiscount);
				updateHtml = updateHtml.Replace(tVariable.branchlogo, tValue.BranchLogo);
			}

			model.TemplateHtml = updateHtml;
			return View(model);
		}
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var delete = Services.TemplateService.GetById(id);
			return View(delete);
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Delete(TemplateModel model)
		{
			var delete = Services.TemplateService.Delete(model);


			return RedirectToAction("list", "Template");
		}

		public ActionResult CheckTemplate(TemplateModel check)
		{
			return Json(Services.TemplateService.CheckTemplate(check), JsonRequestBehavior.AllowGet);
		}
		public decimal Calculate(decimal value, int i)
		{
			//i=0 px
			//i=1 cm
			//i=2 inch
			//inch = 100px;
			//inch = 2.54cm;
			//cm = 39.4px
			//cm = 
			var inch = 0.0;
			var cm = 0.0;
			var px = 0.0;
			var resolution = 100.0;
			if (i == 0)
			{
				return value;
			}
			if (i == 1)
			{
				var re = value * (decimal)(resolution / 2.54);
				return re;
			}
			if (i == 2)
			{
				return (value * (decimal)resolution);
			}
			return 0;
		}
	}
}