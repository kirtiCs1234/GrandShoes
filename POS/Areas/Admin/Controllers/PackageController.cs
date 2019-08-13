using System;
using System.Collections.Generic;
 using System.Web;
using System.Web.Mvc;
using DAL;

namespace POS.Areas.Admin.Controllers
{
    public class PackageController : Controller
    {
        // GET: Admin/Package
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Package/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Package/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Package/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Package/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Package/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Package/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Package/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
