using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas
{
    public class DemoController : BaseController
    {
        // GET: Demo
        public ActionResult Index()
        {
            List<SizeGridModel> SizeGridModelList = Services.SizeGridService.GetAll();
            ViewBag.SizeGridID = new SelectList(SizeGridModelList, "Id", "GridNumber");
            return View();
        }
        [HttpGet]
        public JsonResult getGridSize(int? id)
        {
            //ProductModel ProductModelById = Services.ProductService.GetById(id);
            SizeGridModel SizeGridModelById = Services.SizeGridService.GetById(id);
            List<string> namelist = new List<string>();
            if (!string.IsNullOrEmpty(SizeGridModelById.Z01.ToString()))
            {
                if (SizeGridModelById.Z01.ToString().Contains(".0"))
                {
                    var a1 = SizeGridModelById.Z01.ToString().Replace(".0", "");
                    namelist.Add(a1);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z01.ToString());
                }

            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z02.ToString()))
            {
                if (SizeGridModelById.Z02.ToString().Contains(".0"))
                {
                    var a2 = SizeGridModelById.Z02.ToString().Replace(".0", "");
                    namelist.Add(a2);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z02.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z03.ToString()))
            {
                if (SizeGridModelById.Z03.ToString().Contains(".0"))
                {
                    var a3 = SizeGridModelById.Z03.ToString().Replace(".0", "");
                    namelist.Add(a3);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z03.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z04.ToString()))
            {
                if (SizeGridModelById.Z04.ToString().Contains(".0"))
                {
                    var a4 = SizeGridModelById.Z04.ToString().Replace(".0", "");
                    namelist.Add(a4);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z04.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z05.ToString()))
            {
                if (SizeGridModelById.Z05.ToString().Contains(".0"))
                {
                    var a5 = SizeGridModelById.Z05.ToString().Replace(".0", "");
                    namelist.Add(a5);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z05.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z06.ToString()))
            {
                if (SizeGridModelById.Z06.ToString().Contains(".0"))
                {
                    var a6 = SizeGridModelById.Z06.ToString().Replace(".0", "");
                    namelist.Add(a6);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z06.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z07.ToString()))
            {
                if (SizeGridModelById.Z07.ToString().Contains(".0"))
                {
                    var a7 = SizeGridModelById.Z07.ToString().Replace(".0", "");
                    namelist.Add(a7);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z07.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z08.ToString()))
            {
                if (SizeGridModelById.Z08.ToString().Contains(".0"))
                {
                    var a8 = SizeGridModelById.Z08.ToString().Replace(".0", "");
                    namelist.Add(a8);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z08.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z09.ToString()))
            {
                if (SizeGridModelById.Z09.ToString().Contains(".0"))
                {
                    var a9 = SizeGridModelById.Z09.ToString().Replace(".0", "");
                    namelist.Add(a9);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z09.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z10.ToString()))
            {
                if (SizeGridModelById.Z10.ToString().Contains(".0"))
                {
                    var a10 = SizeGridModelById.Z10.ToString().Replace(".0", "");
                    namelist.Add(a10);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z10.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z11.ToString()))
            {
                if (SizeGridModelById.Z11.ToString().Contains(".0"))
                {
                    var a11 = SizeGridModelById.Z11.ToString().Replace(".0", "");
                    namelist.Add(a11);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z11.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z12.ToString()))
            {
                if (SizeGridModelById.Z12.ToString().Contains(".0"))
                {
                    var a12 = SizeGridModelById.Z12.ToString().Replace(".0", "");
                    namelist.Add(a12);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z12.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z13.ToString()))
            {
                if (SizeGridModelById.Z13.ToString().Contains(".0"))
                {
                    var a13 = SizeGridModelById.Z13.ToString().Replace(".0", "");
                    namelist.Add(a13);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z13.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z14.ToString()))
            {
                if (SizeGridModelById.Z14.ToString().Contains(".0"))
                {
                    var a14 = SizeGridModelById.Z14.ToString().Replace(".0", "");
                    namelist.Add(a14);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z14.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z15.ToString()))
            {
                if (SizeGridModelById.Z15.ToString().Contains(".0"))
                {
                    var a15 = SizeGridModelById.Z15.ToString().Replace(".0", "");
                    namelist.Add(a15);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z15.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z16.ToString()))
            {
                if (SizeGridModelById.Z16.ToString().Contains(".0"))
                {
                    var a16 = SizeGridModelById.Z16.ToString().Replace(".0", "");
                    namelist.Add(a16);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z16.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z17.ToString()))
            {
                if (SizeGridModelById.Z17.ToString().Contains(".0"))
                {
                    var a17 = SizeGridModelById.Z17.ToString().Replace(".0", "");
                    namelist.Add(a17);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z17.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z18.ToString()))
            {
                if (SizeGridModelById.Z18.ToString().Contains(".0"))
                {
                    var a18 = SizeGridModelById.Z18.ToString().Replace(".0", "");
                    namelist.Add(a18);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z18.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z19.ToString()))
            {
                if (SizeGridModelById.Z19.ToString().Contains(".0"))
                {
                    var a19 = SizeGridModelById.Z19.ToString().Replace(".0", "");
                    namelist.Add(a19);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z19.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z20.ToString()))
            {
                if (SizeGridModelById.Z20.ToString().Contains(".0"))
                {
                    var a20 = SizeGridModelById.Z20.ToString().Replace(".0", "");
                    namelist.Add(a20);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z20.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z21.ToString()))
            {
                if (SizeGridModelById.Z21.ToString().Contains(".0"))
                {
                    var a21 = SizeGridModelById.Z21.ToString().Replace(".0", "");
                    namelist.Add(a21);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z21.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z22.ToString()))
            {
                if (SizeGridModelById.Z22.ToString().Contains(".0"))
                {
                    var a22 = SizeGridModelById.Z22.ToString().Replace(".0", "");
                    namelist.Add(a22);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z22.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z23.ToString()))
            {
                if (SizeGridModelById.Z23.ToString().Contains(".0"))
                {
                    var a23 = SizeGridModelById.Z23.ToString().Replace(".0", "");
                    namelist.Add(a23);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z23.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z24.ToString()))
            {
                if (SizeGridModelById.Z24.ToString().Contains(".0"))
                {
                    var a24 = SizeGridModelById.Z24.ToString().Replace(".0", "");
                    namelist.Add(a24);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z24.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z25.ToString()))
            {
                if (SizeGridModelById.Z25.ToString().Contains(".0"))
                {
                    var a25 = SizeGridModelById.Z25.ToString().Replace(".0", "");
                    namelist.Add(a25);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z25.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z26.ToString()))
            {
                if (SizeGridModelById.Z26.ToString().Contains(".0"))
                {
                    var a26 = SizeGridModelById.Z26.ToString().Replace(".0", "");
                    namelist.Add(a26);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z26.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z27.ToString()))
            {
                if (SizeGridModelById.Z27.ToString().Contains(".0"))
                {

                    var a27 = SizeGridModelById.Z27.ToString().Replace(".0", "");
                    namelist.Add(a27);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z27.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z28.ToString()))
            {
                if (SizeGridModelById.Z28.ToString().Contains(".0"))
                {
                    var a28 = SizeGridModelById.Z28.ToString().Replace(".0", "");
                    namelist.Add(a28);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z28.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z29.ToString()))
            {
                if (SizeGridModelById.Z29.ToString().Contains(".0"))
                {
                    var a29 = SizeGridModelById.Z29.ToString().Replace(".0", "");
                    namelist.Add(a29);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z29.ToString());
                }
            }
            if (!string.IsNullOrEmpty(SizeGridModelById.Z30.ToString()))
            {
                if (SizeGridModelById.Z30.ToString().Contains(".0"))
                {
                    var a30 = SizeGridModelById.Z30.ToString().Replace(".0", "");
                    namelist.Add(a30);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z30.ToString());
                }
            }
            return Json(namelist, JsonRequestBehavior.AllowGet);

        }
    }
}