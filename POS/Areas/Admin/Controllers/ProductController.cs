using Helper;
using Model;
using OfficeOpenXml;
using PagedList;
using POS.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace POS.Areas.Admin.Controllers
{
	[CustomAuth(PageSession.Product)]
	public class ProductController : BaseController
	{
        // GET: Admin/Product
        ServiceClass sc = new ServiceClass();
		public ActionResult Index()
		{
            var pData = TempData["ProcessData"];
            if(pData != null)
            {
                ViewBag.processData = pData;
            }

            var AllProductDetailModelList = Services.ProductService.GetAllProduct();
			return View(AllProductDetailModelList);
		}
		public ActionResult _Index(ProdSearch prodSearch)
		{

			var productModelList = Services.ProductService.SearchProduct(prodSearch);
			return View(productModelList);
		}
		public ActionResult _Index1()
		{
			var ProductList = Services.ProductService.GetFreeGiftList();
			return View(ProductList);
		}
		public ActionResult ViewReceiptByProduct(int? id)
		{
			var ReceiptByProduct = Services.ProductService.ReceiptByProduct(id);
			return View(ReceiptByProduct);
		}
		public ActionResult GetReceiptById(int? id)
		{
			var ReceiptItem = Services.ReceiptOrderService.GetReceiptItemById(id);
			return View(ReceiptItem);
		}
		public ActionResult TreeView(int id)
		{
			List<ViewDistribution> data = new List<ViewDistribution>();
			// var StockSummary = Services.StockDistributionService.GetByProductId(id);
			var StockSummary = Services.StockDistributionSummaryService.GetAllid();
			foreach (var stock in StockSummary)
			{
				ViewDistribution viewDistribution = new ViewDistribution();
				viewDistribution.ID = stock.Id;
				viewDistribution.Name = stock.Id.ToString();
				viewDistribution.Pid = 0;
				// viewDistribution.TransactionDate = stock.DateOpen.ToString();
				data.Add(viewDistribution);
			}

			var StockList = Services.StockDistributionService.GetByProductId(id);
			//var StockList = StockDistribution.Where(x => x.ProductId == id).ToList();
			//var StockListBySummary=StockList.Where(x=>x.StockDistributionSummaryId==data.)
			foreach (var item in data)
			{

				var StockListByParent = StockList.Where(x => x.StockDistributionSummaryId == item.ID).ToList();
				foreach (var item1 in StockListByParent)
				{
					ViewDistribution viewDistribution = new ViewDistribution();
					viewDistribution.ID = item1.BranchId ?? default(int);
					viewDistribution.Name = item1.Branch.Name.ToString();
					viewDistribution.Pid = item1.StockDistributionSummaryId;
					viewDistribution.TransactionDate = item1.DistributionDate.ToString();
					item.Childs.Add(viewDistribution);
				}
			}
			return View(data);
		}
		public ActionResult GetDetail(int? id)
		{
			var StockDistributionByBranch = Services.StockDistributionService.GetByProductId(id);
			return View(StockDistributionByBranch);
		}
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Dictionary<string, SelectList> seList = new Dictionary<string, SelectList>();
            ProductModel productModel = new ProductModel();
            DropDownProductListModel model = Services.ProductService.GetDropDownProductList();
            var ProdCat1ID = new SelectList(model.Cat1List, "Id", "CateName");
            var ProdCat2ID = new SelectList(model.Cat2List, "Id", "CateName");
            var ProdCat3ID = new SelectList(model.Cat3List, "Id", "CateName");
            var ProdCat4ID = new SelectList(model.Cat4List, "Id", "CateName");
            var IsFreeGift = new SelectList(model.freeGiftList, "Id", "ProductSKU");
            var SeasonID = new SelectList(model.SeasonModelList, "Id", "Description");
            var BuyerID = new SelectList(model.BuyerModelList, "Id", "Name");
            var SizeGridID = new SelectList(model.SizeGridModelList, "Id", "GridNumber");
            var ProductCategoryID = new SelectList(model.ProductCategoryModelList, "Id", "Code");
            var DefaultTemplateID = new SelectList(model.TemplateModelList, "Id", "Name");
            var ProductGroupID = new SelectList(model.ProductGroupModelList, "Id", "GroupName");
            var ProductSourceID = new SelectList(model.ProductSourceModelList, "Id", "Source");
            var SupplierID = new SelectList(model.SupplierModelList, "Id", "Code");
            var ColorID = new SelectList(model.ColorModelList, "Id", "FullColor");
            var MarkDownTemplateID = new SelectList(model.TemplateModelList, "Id", "Name");
            
            seList.Add("ProdCat1ID", ProdCat1ID);
            seList.Add("ProdCat2ID", ProdCat2ID);
            seList.Add("ProdCat3ID", ProdCat3ID);
            seList.Add("ProdCat4ID", ProdCat4ID);
            seList.Add("IsFreeGift", IsFreeGift);
            seList.Add("SeasonID", SeasonID);
            seList.Add("BuyerID", BuyerID);
            seList.Add("SizeGridID", SizeGridID);
            seList.Add("ProductCategoryID", ProductCategoryID);
            seList.Add("DefaultTemplateID", DefaultTemplateID);
            seList.Add("ProductGroupID", ProductGroupID);
            seList.Add("ProductSourceID", ProductSourceID);
            seList.Add("SupplierID", SupplierID);
            seList.Add("ColorID", ColorID);
            seList.Add("MarkDownTemplateID", MarkDownTemplateID);
            ViewData["dropDownData"] = seList;
            ProductModel ProductModelById = Services.ProductService.GetById(id);
            ProductModelById.SupplierName = ProductModelById.Supplier.Name;
			var productSku = ProductModelById.ProductSKU;
			var markDown = Services.MarkDownService.GetAll();
			var markDownPrev = markDown.Where(x => x.ProductSKU == productSku && x.StyleSKU == ProductModelById.StyleSKU).Count();
			TempData["Number"] = markDownPrev;
			var EffectiveDateLast = markDown.Where(x => x.ProductSKU.ToString() == productSku).LastOrDefault();
			if (EffectiveDateLast == null)
			{
				TempData["Date"] = "";
			}
			else
			{
				TempData["Date"] = EffectiveDateLast.EffectiveDate;
			}
			if (ProductModelById.CreatedOn != null)
			{
				ProductModelById.CreatedOn = ProductModelById.CreatedOn.ToString();
			}
			ViewBag.PrimaryImage = ProductModelById.PrimaryImage;
			if (ProductModelById.Barcode == null)
			{
				ViewBag.Barcode = ProductModelById.StyleSKU + ProductModelById.ProductSKU + ProductModelById.Color.Code;
			}
			else
			{
				ViewBag.Barcode = ProductModelById.Barcode;
			}
			//  ViewBag.slectedItems = ProductModelById.ProductVariances.Select(c => c.ColorID).ToList();

			return View(ProductModelById);
		}
		[HttpGet]
		public JsonResult GenerateStyle(string id)
		{
			var product = Services.ProductService.GetListByProductSKU(id);

			var styleSKU = "0000";
			if (product.Count == 0)
			{
				styleSKU = "0001";
			}
			else
			{
				// string formattedIncNumber = String.Format("{0:D4}", product);
				//product+1;
				int style = Convert.ToInt32(product.LastOrDefault().StyleSKU) + 1;

				int cnt = style.ToString().Length;
				if (cnt == 1)
				{
					styleSKU = "000" + style.ToString();
				}
				if (cnt == 2)
				{
					styleSKU = "00" + style.ToString();
				}
				if (cnt == 3)
				{
					styleSKU = "0" + style.ToString();
				}
				if (cnt == 4)
				{
					styleSKU = style.ToString();
				}
			}
			return Json(styleSKU, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public JsonResult Validation(string id)
		{
			bool status = Services.ProductService.CheckValidation(id);
			return Json(status, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Create()
		{
            Dictionary<string, SelectList> seList = new Dictionary<string, SelectList>();
			ProductModel productModel = new ProductModel();
            DropDownProductListModel model = Services.ProductService.GetDropDownProductList();
             var ProdCat1ID = new SelectList(model.Cat1List, "Id", "CateName");
            var ProdCat2ID = new SelectList(model.Cat2List, "Id", "CateName");
            var ProdCat3ID = new SelectList(model.Cat3List, "Id", "CateName");
            var ProdCat4ID = new SelectList(model.Cat4List, "Id", "CateName");
            var IsFreeGift = new SelectList(model.freeGiftList, "Id", "ProductSKU");
            var SeasonID = new SelectList(model.SeasonModelList, "Id", "Description");
            var BuyerID = new SelectList(model.BuyerModelList, "Id", "Name");
            var SizeGridID = new SelectList(model.SizeGridModelList, "Id", "GridNumber");
            var ProductCategoryID = new SelectList(model.ProductCategoryModelList, "Id", "Code");
            var DefaultTemplateID = new SelectList(model.TemplateModelList, "Id", "Name");
            var ProductGroupID = new SelectList(model.ProductGroupModelList, "Id", "GroupName");
            var ProductSourceID = new SelectList(model.ProductSourceModelList, "Id", "Source");
			var SupplierID = new SelectList(model.SupplierModelList, "Id", "Code");
			var ColorID = new SelectList(model.ColorModelList, "Id", "FullColor");
			var MarkDownTemplateID = new SelectList(model.TemplateModelList, "Id", "Name");
            seList.Add("ProdCat1ID", ProdCat1ID);
            seList.Add("ProdCat2ID", ProdCat2ID);
            seList.Add("ProdCat3ID", ProdCat3ID);
            seList.Add("ProdCat4ID", ProdCat4ID);
            seList.Add("IsFreeGift", IsFreeGift);
            seList.Add("SeasonID", SeasonID);
            seList.Add("BuyerID", BuyerID);
            seList.Add("SizeGridID", SizeGridID);
            seList.Add("ProductCategoryID", ProductCategoryID);
            seList.Add("DefaultTemplateID", DefaultTemplateID);
            seList.Add("ProductGroupID", ProductGroupID);
            seList.Add("ProductSourceID", ProductSourceID);
            seList.Add("SupplierID", SupplierID);
            seList.Add("ColorID", ColorID);
            seList.Add("MarkDownTemplateID", MarkDownTemplateID);
            ViewData["dropDownData"] = seList;
			return PartialView(productModel);
		}
		[HttpPost]
		public ActionResult Create(ProductModel product, HttpPostedFileBase PrimaryImage, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
		{
			List<ColorModel> ColorModelList = Services.ColorService.GetAll();
			var colorCode = ColorModelList.Where(x => x.Id == product.ColorID).FirstOrDefault().Code;

			var stylesku = product.StyleSKU;
			var sku = product.ProductSKU;
			product.Barcode = stylesku + sku + colorCode;
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			if (ModelState.IsValid)
			{
				if (PrimaryImage != null || Image1 != null || Image2 != null || Image3 != null || Image4 != null)
				{
					var allowedExtensions = new[] { ".jfif", ".Jpg", ".png", ".jpg", ".jpeg" };
					if (PrimaryImage != null)
					{
						product.PrimaryImage = PrimaryImage.ToString();
						var fileName = Path.GetFileName(PrimaryImage.FileName);
						var ext = Path.GetExtension(PrimaryImage.FileName);
						if (allowedExtensions.Contains(ext))
						{
							string name1 = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
							string myfile1 = name1 + "_" + product.Id + ext; //appending the name with id  
																			 // store the file inside ~/project folder(Img) 
							var path = Path.Combine(Server.MapPath("~/Image/product"), myfile1);

							var path1 = myfile1;
							product.PrimaryImage = path1;

							PrimaryImage.SaveAs(path);
						}
					}

					if (Image1 != null)
					{
						product.Image1 = Image1.ToString();
						var fileName1 = Path.GetFileName(Image1.FileName);

						var ext1 = Path.GetExtension(Image1.FileName);
						if (allowedExtensions.Contains(ext1))
						{
							string name2 = Path.GetFileNameWithoutExtension(fileName1); //getting file name without extension  
							string myfile2 = name2 + "_" + product.Id + ext1; //appending the name with ~/project folder(Img) 
							var path = Path.Combine(Server.MapPath("~/Image/product"), myfile2);
							var path2 = myfile2;
							product.Image1 = path2;

							Image1.SaveAs(path);
						}
					}
					if (Image2 != null)
					{
						product.Image2 = Image2.ToString();
						var fileName2 = Path.GetFileName(Image2.FileName);
						var ext2 = Path.GetExtension(Image2.FileName);
						if (allowedExtensions.Contains(ext2))
						{
							string name3 = Path.GetFileNameWithoutExtension(fileName2); //getting file name without extension  
							string myfile3 = name3 + "_" + product.Id + ext2; //appending the name with id                                                                              // store the file inside ~/project folder(Img) 
							var path = Path.Combine(Server.MapPath("~/Image/product"), myfile3);
							var path3 = myfile3;
							product.Image2 = path3;

							Image2.SaveAs(path);
						}
					}
					if (Image3 != null)
					{
						product.Image3 = Image3.ToString();
						var fileName3 = Path.GetFileName(Image3.FileName);
						var ext3 = Path.GetExtension(Image3.FileName);

						if (allowedExtensions.Contains(ext3))
						{
							string name4 = Path.GetFileNameWithoutExtension(fileName3); //getting file name without extension  
							string myfile4 = name4 + "_" + product.Id + ext3; //appending the name with id  
																			  // store the file inside ~/project folder(Img) 
							var path = Path.Combine(Server.MapPath("~/Image/product"), myfile4);

							var path4 = myfile4;
							product.Image3 = path4;

							Image3.SaveAs(path);
						}
					}
					if (Image4 != null)
					{
						product.Image4 = Image4.ToString();
						var fileName4 = Path.GetFileName(Image4.FileName);
						var ext4 = Path.GetExtension(Image4.FileName);

						if (allowedExtensions.Contains(ext4))
						{
							string name5 = Path.GetFileNameWithoutExtension(fileName4); //getting file name without extension  
							string myfile5 = name5 + "_" + product.Id + ext4; //appending the name with id  
																			  // store the file inside ~/project folder(Img) 
							var path = Path.Combine(Server.MapPath("~/Image/product"), myfile5);

							var path5 = myfile5;
							product.Image4 = path5;

							Image4.SaveAs(path);
						}
					}
				}

				product.IsActive = true;

				var ProductCreate = Services.ProductService.Create(product);
				TempData["Success"] = "Data Saved Successfully!";
				return RedirectToAction("Index", "Product/Index");

			}
			else
			{
				var errors1 = ModelState.Select(x => x.Value.Errors)
									   .Where(y => y.Count > 0)
									  .ToList();
			}
			return View();
		}
		public ActionResult ExcelUpload()
		{
			return View();
		}
		[HttpPost]
        public ActionResult ExcelUpload(HttpPostedFileBase ExcelFile)
        {
            var SKU = new Dictionary<int, string>();
            Dictionary<string, string> process = new Dictionary<string, string>();
            var msgList = new List<Message>();

            Dictionary<int, ProductModel> tempProducts = new Dictionary<int, ProductModel>();

            Dictionary<int, ProductModel> products = new Dictionary<int, ProductModel>();
            Dictionary<int, ProductModel> updateProducts = new Dictionary<int, ProductModel>();

            Dictionary<int, ProductImport> filter = new Dictionary<int, ProductImport>();
            string filePath = string.Empty;
            if (ExcelFile != null)
            {
                string path = Server.MapPath("~/File/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(ExcelFile.FileName);
                string extension = Path.GetExtension(ExcelFile.FileName);
                ExcelFile.SaveAs(filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);
                var csv = csvData.Split('\n').Length;
                var msg = new Message();
                int i = 0;
                var csvSplitData = csvData.Split('\n');
                Dictionary<int, ProductImport> productImport = new Dictionary<int, ProductImport>();
                foreach (string row in csvSplitData)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(row) && row.Split(',')[0].All(char.IsDigit) && !row.StartsWith(",,,,,"))
                            {
                                i++;
                                ProductImport pi = new ProductImport();
                                var model = new ProductModel();
                                var rowsplit = row.Split(',');
                                model.ProductSKU = rowsplit[0];
                                model.StyleSKU = rowsplit[1];
                                if (model.StyleSKU.Length < 4)
                                {
                                    var s = model.StyleSKU.Length;
                                    while (s < 4)
                                    {
                                        model.StyleSKU = "0" + model.StyleSKU;
                                        s++;
                                    }
                                }
                                process.Add(i + "#" + rowsplit[0] + "#" + model.StyleSKU, "");
                                model.LongDescription = rowsplit[2];
                                model.ShortDescription = rowsplit[3];
                                var SizeGridNo = rowsplit[4];
                                pi.SizeGridNo = rowsplit[4];
                                model.RecommendedSellingPrice = rowsplit[5];
                                model.ActualSellingPrice = rowsplit[6];
                                model.IsVPI = rowsplit[7].Equals("1") ? true : false;
                                model.IsMarkDown = rowsplit[8].Equals("1") ? true : false;
                                var SeasonCode = rowsplit[9];
                                pi.Season = rowsplit[9];
                                // model.SeasonID = Services.SeasonService.GetSeasonId(SeasonCode)?.Id;
                                var year = rowsplit[10];
                                pi.Year = rowsplit[10];
                                //  model.YearID = Services.ProductService.GetYearId(year)?.Id;
                                var productSource = rowsplit[11];
                                pi.ProductSource = rowsplit[11];
                                //model.ProductSourceID = Services.ProductSourceService.GetProductSourceId(productSource)?.Id;

                                model.SupplierStyle = rowsplit[12];
                                var supplier = rowsplit[13];
                                pi.Supplier = rowsplit[13];
                                //model.SupplierID = Services.SupplierService.GetSupplierId(supplier)?.Id;

                                var buyer = rowsplit[14];
                                pi.Buyer = rowsplit[14];
                                //    model.BuyerID = Services.BuyerService.GetBuyerId(buyer)?.Id;

                                model.CostPriceUSD = rowsplit[15];
                                model.CostPrice = rowsplit[16];
                                model.IsAllowZero = rowsplit[17].Equals("1") ? true : false;
                                var template = rowsplit[18];
                                pi.DefaultTemplate = rowsplit[18];
                                //   model.DefaultTemplateID = Services.TemplateService.GetTemplateId(template)?.Id;

                                var markdown = rowsplit[19];
                                pi.MarkdownTemplate = rowsplit[19];
                                //   model.MarkDownTemplateID = Services.TemplateService.GetTemplateId(markdown)?.Id;

                                model.IsFreeGift = rowsplit[20];
                                model.IsConsignment = rowsplit[21].Equals("1") ? true : false;
                                model.IsDiscontinue = rowsplit[22].Equals("1") ? true : false;
                                model.CreatedOn = rowsplit[23];
                                model.UpdatedOn = rowsplit[24];
                                var prodCat1 = rowsplit[25];
                                pi.ProductCat1 = rowsplit[25];
                                //  model.ProdCat1ID = Services.ProductCategoryService.GetIdCat1(prodCat1)?.Id;
                                var prodCat2 = rowsplit[26];
                                pi.ProductCat2 = rowsplit[26];
                                // model.ProdCat2ID = Services.ProductCategoryService.GetIdCat2(prodCat2)?.Id;
                                var prodCat3 = rowsplit[27];
                                pi.ProductCat3 = rowsplit[27];
                                //   model.ProdCat3ID = Services.ProductCategoryService.GetIdCat3(prodCat3)?.Id;
                                var prodCat4 = rowsplit[28];
                                pi.ProductCat4 = rowsplit[28];
                                //   model.ProdCat4ID = Services.ProductCategoryService.GetIdCat4(prodCat4)?.Id;
                                var count = rowsplit.Length;
                                for (int j = 29; j < count - 1; j++)
                                {
                                    model.AvailableSize += rowsplit[j] + ",";
                                }
                                model.AvailableSize = model.AvailableSize.Remove(0, 1);
                                model.AvailableSize = model.AvailableSize.Remove(model.AvailableSize.Length - 2, 1);

                                ////model.AvailableSize = model.AvailableSize.Remove('"',null);
                                var color = rowsplit[count - 1];
                                pi.Color = rowsplit[count - 1];
                                //  model.ColorID = Services.ColorService.GetColorId(color)?.Id;
                                //var body = Newtonsoft.Json.JsonConvert.SerializeObject(pi)
                               
                                //ServerResponse.Invoke<int>("api/product/getProductImportFilter",body,"POST");

                                model.Barcode = model.StyleSKU + model.ProductSKU + color;
                                model.Barcode = model.Barcode.Replace("\r", "");
                                model.IsActive = true;
                                string chk1 = rowsplit[0];

                                string chk2 = model.StyleSKU;

                                if (model.ProductSKU.Length == 3 && model.StyleSKU.Length == 4)
                                {
                                    tempProducts.Add(i, model);
                                    SKU.Add(i, model.ProductSKU + "#" + model.StyleSKU);
                                    productImport.Add(i, pi);
                                }
                                else
                                {
                                    process[i + "#" + rowsplit[0] + "#" + model.StyleSKU] = "Check the ProductSKU Length";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message != null)
                            {
                                process[i + "#" + row.Split(',')[0] + "#" + row.Split(',')[1]] = ex.Message;
                            }
                            else
                            {
                                process[i + "#" + row.Split(',')[0] + "#" + row.Split(',')[1]] = "Error Adding the row";
                            }
                        }
                    }
                }

                var check = Services.ProductService.ProductCheckFilter(SKU);//ServerResponse.Invoke<Dictionary<int, bool>>("api/product/getProductCheckFilter", body2, "POST");

                if (tempProducts != null && tempProducts.Count > 0)
                {
                    foreach (var item in tempProducts)
                    {
                        if (check[item.Key] == false)
                        {
                            products.Add(item.Key, item.Value);
                            process[item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU] = "Add";
                        }
                        else
                        {
                            updateProducts.Add(item.Key, item.Value);
                            process[item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU] = "Update";
                        }
                    }
                }

                filter = Services.ProductService.ProductImportFilter(productImport);//ServerResponse.Invoke<Dictionary<int, ProductImport>>("api/product/getProductImportFilter",body,"POST");
            }

            if (products != null && products.Count > 0)
            {
                foreach (var item in products)
                {
                    var fValue = filter.Where(x => x.Key == item.Key).FirstOrDefault().Value;

                    item.Value.BuyerID = fValue.BuyerId;
                    item.Value.ColorID = fValue.ColorID;
                    item.Value.DefaultTemplateID = fValue.DefaultTemplateId;
                    item.Value.MarkDownTemplateID = fValue.MarkdownTemplateId;
                    item.Value.ProdCat1ID = fValue.ProductCat1ID;
                    item.Value.ProdCat2ID = fValue.ProductCat2ID;
                    item.Value.ProdCat3ID = fValue.ProductCat3ID;
                    item.Value.ProdCat4ID = fValue.ProductCat4ID;
                    item.Value.ProductSourceID = fValue.ProductSourceId;
                    item.Value.SeasonID = fValue.SeasonId;
                    item.Value.SizeGridID = fValue.SizeGridId;
                    item.Value.SupplierID = fValue.SupplierId;
                    item.Value.YearID = fValue.YearId;
                }
            }
            if (updateProducts != null && updateProducts.Count > 0)
            {
                foreach (var item in updateProducts)
                {
                    var fValue = filter.Where(x => x.Key == item.Key).FirstOrDefault().Value;

                    item.Value.BuyerID = fValue.BuyerId;
                    item.Value.ColorID = fValue.ColorID;
                    item.Value.DefaultTemplateID = fValue.DefaultTemplateId;
                    item.Value.MarkDownTemplateID = fValue.MarkdownTemplateId;
                    item.Value.ProdCat1ID = fValue.ProductCat1ID;
                    item.Value.ProdCat2ID = fValue.ProductCat2ID;
                    item.Value.ProdCat3ID = fValue.ProductCat3ID;
                    item.Value.ProdCat4ID = fValue.ProductCat4ID;
                    item.Value.ProductSourceID = fValue.ProductSourceId;
                    item.Value.SeasonID = fValue.SeasonId;
                    item.Value.SizeGridID = fValue.SizeGridId;
                    item.Value.SupplierID = fValue.SupplierId;
                    item.Value.YearID = fValue.YearId;
                }
            }

            var addList = new Dictionary<string, string>();//Services.ProductService.CreateList(products);
            var updateList = new Dictionary<string, string>();//Services.ProductService.UpdateList(updateProducts);

            if (products.Count > 0)
            {
                addList = Services.ProductService.CreateList(products);
            }
            if (updateProducts.Count > 0)
            {
                updateList = Services.ProductService.UpdateList(updateProducts);
            }
            var dictionaryFrom = new Dictionary<string, string>();

            dictionaryFrom = sc.getFilterData(addList, updateList, process);

            //if (addList != null && updateList != null)
            //process.Where(x => !updateList.Keys.Contains(x.Key) && !addList.Keys.Contains(x.Key)).ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));
            //addList.ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));
            //updateList.ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));
            TempData["ProcessData"] = dictionaryFrom;
            TempData["Success"] = "Data Uploaded Successfully!";
            return RedirectToAction("Index", "Product/Index");
        }
        public ActionResult ExportList()
		{
			var data = Services.ProductService.GetAll();
			ExcelPackage excel = new ExcelPackage();
			var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
			workSheet.Cells[1, 2].LoadFromCollection(data, true);
			using (var memoryStream = new MemoryStream())
			{
				Response.ContentType = "application/vnd.ms-excel";
				Response.AddHeader("content-disposition", "attachment;  filename=ProductExport.xlsx");
				excel.SaveAs(memoryStream);
				memoryStream.WriteTo(Response.OutputStream);
				Response.Flush();
				//Response.End();
			}
			return View();
		}
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var ProductModelById = Services.ProductService.GetById(id);
			if (ProductModelById == null)
			{
				return HttpNotFound();
			}
			return View(ProductModelById);
		}
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(ProductModel product)
		{
			if (product.Id > 0)
			{
				var ProductDelete = Services.ProductService.Delete(product);
				TempData["Success"] = "Data Deleted Successfully!";
				return RedirectToAction("Index", "Product/Index");
			}
			return View(product);
		}
		public ActionResult Edit(int? id)

		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Dictionary<string, SelectList> seList = new Dictionary<string, SelectList>();
            ProductModel productModel = new ProductModel();
            DropDownProductListModel model = Services.ProductService.GetDropDownProductList();
            var ProdCat1ID = new SelectList(model.Cat1List, "Id", "CateName");
            var ProdCat2ID = new SelectList(model.Cat2List, "Id", "CateName");
            var ProdCat3ID = new SelectList(model.Cat3List, "Id", "CateName");
            var ProdCat4ID = new SelectList(model.Cat4List, "Id", "CateName");
            var IsFreeGift = new SelectList(model.freeGiftList, "Id", "ProductSKU");
            var SeasonID = new SelectList(model.SeasonModelList, "Id", "Description");
            var BuyerID = new SelectList(model.BuyerModelList, "Id", "Name");
            var SizeGridID = new SelectList(model.SizeGridModelList, "Id", "GridNumber");
            var ProductCategoryID = new SelectList(model.ProductCategoryModelList, "Id", "Code");
            var DefaultTemplateID = new SelectList(model.TemplateModelList, "Id", "Name");
            var ProductGroupID = new SelectList(model.ProductGroupModelList, "Id", "GroupName");
            var ProductSourceID = new SelectList(model.ProductSourceModelList, "Id", "Source");
            var SupplierID = new SelectList(model.SupplierModelList, "Id", "Code");
            var ColorID = new SelectList(model.ColorModelList, "Id", "FullColor");
            var MarkDownTemplateID = new SelectList(model.TemplateModelList, "Id", "Name");
            
            seList.Add("ProdCat1ID", ProdCat1ID);
            seList.Add("ProdCat2ID", ProdCat2ID);
            seList.Add("ProdCat3ID", ProdCat3ID);
            seList.Add("ProdCat4ID", ProdCat4ID);
            seList.Add("IsFreeGift", IsFreeGift);
            seList.Add("SeasonID", SeasonID);
            seList.Add("BuyerID", BuyerID);
            seList.Add("SizeGridID", SizeGridID);
            seList.Add("ProductCategoryID", ProductCategoryID);
            seList.Add("DefaultTemplateID", DefaultTemplateID);
            seList.Add("ProductGroupID", ProductGroupID);
            seList.Add("ProductSourceID", ProductSourceID);
            seList.Add("SupplierID", SupplierID);
            seList.Add("ColorID", ColorID);
            seList.Add("MarkDownTemplateID", MarkDownTemplateID);
            ViewData["dropDownData"] = seList;
            ProductModel ProductModelById = Services.ProductService.GetById(id);
           
            var productSku = ProductModelById.ProductSKU;
			ProductModelById.SupplierName = ProductModelById.Supplier.Name;
			var markDown = Services.MarkDownService.GetAll();
			var markDownPrev = markDown.Where(x => x.ProductSKU == productSku && x.StyleSKU == ProductModelById.StyleSKU).Count();
			TempData["Number"] = markDownPrev;
			var EffectiveDateLast = markDown.Where(x => x.ProductSKU.ToString() == productSku).LastOrDefault();
			if (EffectiveDateLast == null)
			{
				TempData["Date"] = "";
			}
			else
			{
				TempData["Date"] = EffectiveDateLast.EffectiveDate;
			}
			ViewBag.PrimaryImage = ProductModelById.PrimaryImage;
			ProductModelById.CreatedOn = ProductModelById.CreatedOn?.ToString();
			if (ProductModelById.Barcode == null)
			{
				ViewBag.Barcode = ProductModelById.StyleSKU + ProductModelById.ProductSKU + ProductModelById.Color.Code;
			}
			else
			{
				ViewBag.Barcode = ProductModelById.Barcode;
			}

			// ViewBag.slectedItems = ProductModelById.ProductVariances.Select(c => c.ColorID).ToList();
			return View(ProductModelById);
		}

		[HttpPost]
		public ActionResult Edit(ProductModel product, HttpPostedFileBase PrimaryImage, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
		{
			product.IsActive = true;
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			ModelState.Remove("PrimaryImage");
			//if (!ModelState.IsValid)
			//{
			if (PrimaryImage != null || Image1 != null || Image2 != null || Image3 != null || Image4 != null)
			{
				var allowedExtensions = new[] { ".jfif", ".Jpg", ".png", ".jpg", ".jpeg", ".PNG" };
				if (PrimaryImage != null)
				{
					product.PrimaryImage = PrimaryImage.ToString();
					var fileName = Path.GetFileName(PrimaryImage.FileName);
					var ext = Path.GetExtension(PrimaryImage.FileName);
					if (allowedExtensions.Contains(ext))
					{
						string name1 = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
						string myfile1 = name1 + "_" + product.Id + ext; //appending the name with id  
																		 // store the file inside ~/project folder(Img) 
						var path = Path.Combine(Server.MapPath("~/Image/product"), myfile1);

						var path1 = myfile1;
						product.PrimaryImage = path1;

						PrimaryImage.SaveAs(path);
					}
				}

				if (Image1 != null)
				{
					product.Image1 = Image1.ToString();
					var fileName1 = Path.GetFileName(Image1.FileName);

					var ext1 = Path.GetExtension(Image1.FileName);
					if (allowedExtensions.Contains(ext1))
					{
						string name2 = Path.GetFileNameWithoutExtension(fileName1); //getting file name without extension  
						string myfile2 = name2 + "_" + product.Id + ext1; //appending the name with id                                                                               // store the file inside ~/project folder(Img) 
						var path = Path.Combine(Server.MapPath("~/Image/product"), myfile2);
						var path2 = myfile2;
						product.Image1 = path2;

						Image1.SaveAs(path);
					}
				}
				if (Image2 != null)
				{
					product.Image2 = Image2.ToString();
					var fileName2 = Path.GetFileName(Image2.FileName);
					var ext2 = Path.GetExtension(Image2.FileName);
					if (allowedExtensions.Contains(ext2))
					{
						string name3 = Path.GetFileNameWithoutExtension(fileName2); //getting file name without extension  
						string myfile3 = name3 + "_" + product.Id + ext2; //appending the name with id                                                                              // store the file inside ~/project folder(Img) 
						var path = Path.Combine(Server.MapPath("~/Image/product"), myfile3);
						var path3 = myfile3;
						product.Image2 = path3;

						Image2.SaveAs(path);
					}
				}
				if (Image3 != null)
				{
					product.Image3 = Image3.ToString();
					var fileName3 = Path.GetFileName(Image3.FileName);
					var ext3 = Path.GetExtension(Image3.FileName);

					if (allowedExtensions.Contains(ext3))
					{
						string name4 = Path.GetFileNameWithoutExtension(fileName3); //getting file name without extension  
						string myfile4 = name4 + "_" + product.Id + ext3; //appending the name with id  
																		  // store the file inside ~/project folder(Img) 
						var path = Path.Combine(Server.MapPath("~/Image/product"), myfile4);

						var path4 = myfile4;
						product.Image3 = path4;

						Image3.SaveAs(path);
					}
				}
				if (Image4 != null)
				{
					product.Image4 = Image4.ToString();
					var fileName4 = Path.GetFileName(Image4.FileName);
					var ext4 = Path.GetExtension(Image4.FileName);

					if (allowedExtensions.Contains(ext4))
					{
						string name5 = Path.GetFileNameWithoutExtension(fileName4); //getting file name without extension  
						string myfile5 = name5 + "_" + product.Id + ext4; //appending the name with id  
																		  // store the file inside ~/project folder(Img) 
						var path = Path.Combine(Server.MapPath("~/Image/product"), myfile5);

						var path5 = myfile5;
						product.Image4 = path5;

						Image4.SaveAs(path);
					}
				}
			}
			List<ColorModel> ColorModelList = Services.ColorService.GetAll();
			List<TemplateModel> TemplateModelList = Services.TemplateService.GetAll();
			List<SupplierModel> SupplierModelList = Services.SupplierService.GetAll();
			List<SizeGridModel> SizeGridModelList = Services.SizeGridService.GetAll();
			List<ProductCategoryModel> ProductCategoryModelList = Services.ProductCategoryService.GetAll();
			List<ProductGrpModel> ProductGroupModelList = Services.ProductGroupService.GetAll();
			List<ProductSourceModel> ProductSourceModel = Services.ProductSourceService.GetAll();
			List<SeasonModel> SeasonModelList = Services.SeasonService.GetAll();
			List<BuyerModel> BuyerModelList = Services.BuyerService.GetAll();
			ViewBag.BuyerID = new SelectList(BuyerModelList, "Id", "Name");
			ViewBag.SeasonID = new SelectList(SeasonModelList, "Id", "Description");
			ViewBag.SizeGridID = new SelectList(SizeGridModelList, "Id", "GridNumber");
			ViewBag.SizeGridID = new SelectList(SizeGridModelList, "Id", "");

			ViewBag.ProductCategoryID = new SelectList(ProductCategoryModelList, "Id", "Code");
			ViewBag.ProductGroupID = new SelectList(ProductGroupModelList, "Id", "GroupName");
			ViewBag.ProductSourceID = new SelectList(ProductSourceModel, "Id", "Source");
			ViewBag.SupplierID = new SelectList(SupplierModelList, "Id", "Code");
			// List<SupplierModel> SupplierModelName = Services.SupplierService.GetSupplierName(0);
			// ViewBag.SupplierID = new SelectList(SupplierModelName, "Id", "Name");
			ViewBag.ColorID = new SelectList(ColorModelList, "Id", "Code");
			ViewBag.MarkDownTemplateID = new SelectList(TemplateModelList, "Id", "Name");

			bool ProductEdit = Services.ProductService.Edit(product);
			TempData["Success"] = "Data Saved Successfully!";
			return RedirectToAction("Index", "Product/Index");
		}
		public ActionResult getSeasonDescription(int? id)
		{
			// ProductModel ProductModelById = Services.ProductService.GetById(id);
			List<SeasonModel> SeasonModelDescription = Services.SeasonService.GetSeasonDescription(id);
			ViewBag.SeasonID = new SelectList(SeasonModelDescription, "Id", "Description");
			return View();
		}

		public ActionResult getSupplierName(int? id)
		{
			// ProductModel ProductModelById = Services.ProductService.GetById(id);
			List<SupplierModel> SupplierModelName = Services.SupplierService.GetSupplierName(id);
			ViewBag.SupplierID = new SelectList(SupplierModelName, "Id", "Name");
			return View();
		}
		[HttpGet]
		public JsonResult AutoCompleteStyleSKUList(string name)
		{
			var ColorLists = Services.ProductStyleService.ProductStyleAutocomplete(name);
			return Json(ColorLists, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public JsonResult AutoCompleteProductSKUList(string name)
		{
			var ProductSKUList = Services.ProductService.ProductAutocomplete(name);
			return Json(ProductSKUList, JsonRequestBehavior.AllowGet);
		}
		public ActionResult CheckProductSKU(ProductModel model)
		{
			var iExist = Services.ProductService.CheckProductSKU(model);
			return Json(!iExist, JsonRequestBehavior.AllowGet);
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
                 var a1=   SizeGridModelById.Z01.ToString().Replace(".0", "");
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
                    var a2=SizeGridModelById.Z02.ToString().Replace(".0", "");
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
                    var a3=SizeGridModelById.Z03.ToString().Replace(".0", "");
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
                    var a4=SizeGridModelById.Z04.ToString().Replace(".0", "");
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
                    var a5=SizeGridModelById.Z05.ToString().Replace(".0", "");
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
                    var a6=SizeGridModelById.Z06.ToString().Replace(".0", "");
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
                    var a7=SizeGridModelById.Z07.ToString().Replace(".0", "");
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
                    var a8=SizeGridModelById.Z08.ToString().Replace(".0", "");
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
                    var a9=SizeGridModelById.Z09.ToString().Replace(".0", "");
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
                    var a10=SizeGridModelById.Z10.ToString().Replace(".0", "");
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
                    var a11=SizeGridModelById.Z11.ToString().Replace(".0", "");
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
                    var a12=SizeGridModelById.Z12.ToString().Replace(".0", "");
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
                   var a13= SizeGridModelById.Z13.ToString().Replace(".0", "");
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
                    var a14=SizeGridModelById.Z14.ToString().Replace(".0", "");
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
                    var a15=SizeGridModelById.Z15.ToString().Replace(".0", "");
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
                    var a16=SizeGridModelById.Z16.ToString().Replace(".0", "");
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
                   var a17= SizeGridModelById.Z17.ToString().Replace(".0", "");
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
                    var a18=SizeGridModelById.Z18.ToString().Replace(".0", "");
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
                    var a19=SizeGridModelById.Z19.ToString().Replace(".0", "");
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
                    var a20=SizeGridModelById.Z20.ToString().Replace(".0", "");
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
                    var a21=SizeGridModelById.Z21.ToString().Replace(".0", "");
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
                    var a22=SizeGridModelById.Z22.ToString().Replace(".0", "");
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
                    var a23=SizeGridModelById.Z23.ToString().Replace(".0", "");
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
                    var a24=SizeGridModelById.Z24.ToString().Replace(".0", "");
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
                    var a25=SizeGridModelById.Z25.ToString().Replace(".0", "");
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
                    var a26=SizeGridModelById.Z26.ToString().Replace(".0", "");
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
                   
                    var a27=SizeGridModelById.Z27.ToString().Replace(".0", "");
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
                   var a28= SizeGridModelById.Z28.ToString().Replace(".0", "");
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
                   var a29=SizeGridModelById.Z29.ToString().Replace(".0", "");
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
                   var a30=SizeGridModelById.Z30.ToString().Replace(".0", "");
                    namelist.Add(a30);
                }
                else
                {
                    namelist.Add(SizeGridModelById.Z30.ToString());
                }
            }
            //TempData["SizeList"] = new SelectList(namelist, "company_name", "select");

            return Json(namelist, JsonRequestBehavior.AllowGet);

		}
		public JsonResult getCategoryName(int? id)
		{
			// ProductModel ProductModelById = Services.ProductService.GetById(id);
			var SupplierModelName = Services.ProductCategoryService.GetById(id);
			return Json(SupplierModelName, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public JsonResult AutoCompleteColorList(string name)
		{
			var ColorLists = Services.ColorService.ColoeAutocomplete(name);
			return Json(ColorLists, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public JsonResult AutocompleteSupplierCode(string name)
		{
			return Json(JsonRequestBehavior.AllowGet);
		}
		public ActionResult ShowTemplate(int TemplateId, int ProductId)
		{
            var data = Services.ProductService.GetById(ProductId);
            var sizeGrid = Services.SizeGridService.GetById(data.SizeGridID);
            var utility = Utilities.getKeyVaue(sizeGrid);

            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> gridSizes = new Dictionary<string, string>();
            var value= Utilities.getFilterDictionary(utility, "Z");
            foreach (var item in value)
            {
                if (item.Value.Contains(".0"))
                {
                    result[item.Key] = item.Value.Replace(".0", "");
                    
                }
                else
                {
                    result[item.Key] = item.Value;
                    
                }
            }
            var avalableSize = data.AvailableSize;
            string[] strArray = avalableSize.Split(',');
            foreach(var item in strArray)
            {
                var DictValue = result.Where(x => x.Value.Equals(item)).FirstOrDefault();
                if (DictValue.Value != null)
                {
                    if (Convert.ToInt16(DictValue.Key) < 10)
                    {
                        gridSizes.Add("0" + DictValue.Key, DictValue.Value);
                    }
                    else
                    {
                        gridSizes.Add(DictValue.Key, DictValue.Value);
                    }
                }
                
            }
            ViewBag.CreateSize = new SelectList(gridSizes, "Key", "Value");
            //var model = db.Result<TemplateModel>("api/template/getOne?id=" + TemplateId, "", "GET");
            var model = Services.ProductService.ShowTemplate(TemplateId, ProductId);
			//model.TemplateHtml = ShowTemplate(model, ProductId);

			return View(model);
		}
		public ActionResult CheckColorCode(int? Id,int ColorId)
		{
            int id = Id ?? 0;
			var iExist = Services.ProductService.CheckColorCode(id, ColorId);
			return Json(!iExist, JsonRequestBehavior.AllowGet);
		}
		public ActionResult CheckExistingSupplier(int? Id,string SupplierStyle)
		{
            int id = Id ?? 0;
			var iExist = Services.ProductService.CheckExistingSupplier(id,SupplierStyle);
			return Json(!iExist, JsonRequestBehavior.AllowGet);
		}
		public ActionResult CheckSupplier(int? Id,int SupplierId)
		{
            int id = Id ?? 0;
			var iExist = Services.ProductService.CheckSupplier(id, SupplierId);
			return Json(!iExist, JsonRequestBehavior.AllowGet);
		}
        public ActionResult getGridSizeA(int? id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            SizeGridModel SizeGrid = Services.SizeGridService.GetById(id);
            var SizeGrids = Utility.getKeyVaue(SizeGrid).Where(x => x.Key.Contains("Z")).ToList();
            foreach (var item in SizeGrids)
            {
                result.Add(item.Key, "");
                if (item.Value.Contains(".0"))
                {
                    result[item.Key] = item.Value.Replace(".0", "");
                }
                else
                {
                    result[item.Key] = item.Value;
                }
            }
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult pListGrid(int? id)
        {
            var list = new List<string>();
            Dictionary<string, string> result = new Dictionary<string, string>();
            SizeGridModel SizeGrid = Services.SizeGridService.GetById(id);
            var SizeGrids = Utility.getKeyVaue(SizeGrid).Where(x => x.Key.Contains("Z")).ToList();
            foreach (var item in SizeGrids)
            {
                result.Add(item.Key, "");
                if (item.Value.Contains(".0"))
                {
                    result[item.Key] = item.Value.Replace(".0", "");
                    list.Add(item.Value.Replace(".0", ""));
                }
                else
                {
                    result[item.Key] = item.Value;
                    list.Add(item.Value);
                }
            }
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewPurchaseOrder(int? id)
        {
            var list = Services.PurchaseOrderItemsService.GetByProductId(id);
            return View(list);
        }
        public ActionResult ViewReceiptOrder(int? id)
        {
            var list = Services.ReceiptOrderService.ReceiptByProduct(id);
            return View(list);
        }
        public ActionResult ViewDistribution(int? id)
        {
            var list = Services.StockDistributionService.GetByProduct(id);
            return View(list);
        }
       public ActionResult ViewStock(int? id)
        {
            var data = Services.StockInventoryService.GetByProduct(id);
            var list = Services.StockBranchInventoryService.GetByProduct(id);
            ViewBag.Warehouse = data;
            return View(list);
        }
        public ActionResult ValidationCheck(ValidationProduct model)
        {
            bool status = Services.ProductService.Check(model);
            return Json(status,JsonRequestBehavior.AllowGet);
        }
    }
}