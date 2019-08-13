using Model;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            ProductModel productModel = new ProductModel();
            // productModel.StyleSKU = Helper.RandomNumberGenerator.RandomStyleSKUGeneration();
            List<ColorModel> ColorModelList = Services.ColorService.GetAll();
            List<TemplateModel> TemplateModelList = Services.TemplateService.GetAll();
            List<SupplierModel> SupplierModelList = Services.SupplierService.GetAll();
            List<SizeGridModel> SizeGridModelList = Services.SizeGridService.GetAll();

            List<ProductCategoryModel> ProductCategoryModelList = Services.ProductCategoryService.GetAll();
            List<ProductGrpModel> ProductGroupModelList = Services.ProductGroupService.GetAll();
            List<ProductSourceModel> ProductSourceModelList = Services.ProductSourceService.GetAll();
            List<BuyerModel> BuyerModelList = Services.BuyerService.GetAll();
            List<SeasonModel> SeasonModelList = Services.SeasonService.GetAll();
            List<ProductStyleModel> ProductStyleModelList = Services.ProductStyleService.GetAll();
            List<ProductModel> freeGiftList = Services.ProductService.GetFreeGift();
            List<ProductCat1Model> Cat1List = Services.ProductCategoryService.GetCat1List();
            List<ProductCat2Model> Cat2List = Services.ProductCategoryService.GetCat2List();
            List<ProductCat3Model> Cat3List = Services.ProductCategoryService.GetCat3List();
            List<ProductCat4Model> Cat4List = Services.ProductCategoryService.GetCat4List();
            ViewBag.ProdCat1ID = new SelectList(Cat1List, "Id", "CateName");
            ViewBag.ProdCat2ID = new SelectList(Cat2List, "Id", "CateName");
            ViewBag.ProdCat3ID = new SelectList(Cat3List, "Id", "CateName");
            ViewBag.ProdCat4ID = new SelectList(Cat4List, "Id", "CateName");
            ViewBag.IsFreeGift = new SelectList(freeGiftList, "Id", "ProductSKU");
            ViewBag.ProductStyleId = new SelectList(ProductStyleModelList, "Id", "StyleSKU");

            ViewBag.SeasonID = new SelectList(SeasonModelList, "Id", "Description");
            //sizeGridModel.Z01 = SizeGridModelList.FirstOrDefault(x => x.Id == 1).Z01;
            ViewBag.BuyerID = new SelectList(BuyerModelList, "Id", "Name");
            ViewBag.SizeGridID = new SelectList(SizeGridModelList, "Id", "GridNumber");
            ViewBag.ProductCategoryID = new SelectList(ProductCategoryModelList, "Id", "Code");
            ViewBag.DefaultTemplateID = new SelectList(TemplateModelList, "Id", "Name");
            ViewBag.ProductGroupID = new SelectList(ProductGroupModelList, "Id", "GroupName");
            ViewBag.ProductSourceID = new SelectList(ProductSourceModelList, "Id", "Source");
            ViewBag.SupplierID = new SelectList(SupplierModelList, "Id", "Code");
            // ViewBag.SupplierID = new SelectList(SupplierModelList, "Id", "Name");
            ViewBag.ColorID = new SelectList(ColorModelList, "Id", "FullColor");
            ViewBag.MarkDownTemplateID = new SelectList(TemplateModelList, "Id", "Name");

            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult ValidationCheck(ValidationProduct model)
        {
            bool status = Services.ProductService.Check(model);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}