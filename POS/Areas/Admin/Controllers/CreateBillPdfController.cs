using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class CreateBillPdfController : Controller
    {
        // GET: Admin/CreateBillPdf
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreatePdfInvoice(string html)
        {
            // Render any HTML fragment or document to HTML
            //var Renderer = new IronPdf.HtmlToPdf();
            //var PDF = Renderer.RenderHtmlAsPdf(html);
            var OutputPath = Server.MapPath("/PDFDesign/HtmlToPDF.pdf");//"HtmlToPDF.pdf";E:\Project\GrandShoesLatest latest\POS\PDFDesign\
            //PDF.SaveAs(OutputPath);
            // This neat trick opens our PDF file so we can see the result in our default PDF viewer
            //System.Diagnostics.Process.Start(OutputPath);
            return Json(OutputPath,JsonRequestBehavior.AllowGet);
        }

    }
}