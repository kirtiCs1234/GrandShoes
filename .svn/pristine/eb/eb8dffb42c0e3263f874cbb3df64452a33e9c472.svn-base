using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class ImageController : BaseController
    {
        // GET: Admin/Image
        public ActionResult Index()

        {
            List<ImageModel> ImageModelList = Services.ImageService.GetAllImage();
            return View(ImageModelList);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(ImageModel image, HttpPostedFileBase file)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (file != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                image.Image1 = file.ToString();
                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName);
                if (allowedExtensions.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + "_" + image.Id + ext; //appending the name with id  
                                                                   // store the file inside ~/project folder(Img) 
                    var path = Path.Combine(Server.MapPath("~/Image/product"), myfile);

                    var path1 = "/Image/product/" + myfile;
                   image.Image1 = path1;
                    file.SaveAs(path);
                }
            }
                image.IsActive = true;
                ImageModel ImageCreate = Services.ImageService.Create(image);
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index", "Image");
           
        }
    }
}