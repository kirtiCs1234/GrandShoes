using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class DesignationController : BaseController
    {
        // GET: Admin/Designation
        public ActionResult Index()
        {
            List<DesignationModel> DesignationModelList = Services.DesignationService.GetAll();
            return View(DesignationModelList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationModel DesignationModelById = Services.DesignationService.GetById(id);
            return View(DesignationModelById);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DesignationModel designation)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                // user.IsActive = true;
                DesignationModel DesignationCreate = Services.DesignationService.Create(designation);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Designation");
            }
            return View(designation);

        }
        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationModel DesignationModelById = Services.DesignationService.GetById(id);
            return View(DesignationModelById);
        }

        [HttpPost]
        public ActionResult Edit(DesignationModel designation)
        {
            if (ModelState.IsValid)
            {
                DesignationModel DesignationEdit = Services.DesignationService.Edit(designation);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Designation");
            }
            return View(designation);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DesignationModel DesignationModelById = Services.DesignationService.GetById(id);

            if (DesignationModelById == null)
            {
                return HttpNotFound();
            }
            return View(DesignationModelById);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(DesignationModel designation)
        {
            if (designation.Id > 0)
            {
                DesignationModel DesignationDelete = Services.DesignationService.Delete(designation);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Designation");
            }
            return View(designation);
        }
        public ActionResult CheckDesignationName(DesignationModel designationModel)
        {
            var iExist = Services.DesignationService.CheckDesignationName(designationModel);
            return Json(!iExist, JsonRequestBehavior.AllowGet);
        }
    }
}