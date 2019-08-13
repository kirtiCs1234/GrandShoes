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
using PagedList;
using System.Text.RegularExpressions;
using Helper.ExtensionMethod;
using Helper;
using Newtonsoft.Json;
using POSApi.ExtensionMethod;

namespace POSApi.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductsController : ApiController
    {
        private GrandShoesEntities db = new GrandShoesEntities();
        public ProductsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }
        [HttpGet]
        [Route("getByProductSKU")]
        public IHttpActionResult GetList(string productSku)
        {
            var list = db.Products.Where(x => x.IsActive == true && x.ProductSKU == productSku).ToList().RemoveReferences();
            return Ok(list);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getFreeGift")]
        public List<Product> GetFreeGift()
        {
            var list = db.Products.Where(x => x.IsActive == true && x.IsAllowZero == true).OrderByDescending(x => x.Id).Include(x => x.Buyer).Include(x => x.Template).Include(x => x.ProductCat1).Include(x=>x.ProductCat2).Include(x=>x.ProductCat3).Include(x=>x.ProductCat4).Include(x => x.ProductSource).Include(x => x.Season).Include(x => x.SizeGrid).Include(x => x.Supplier).Include(x => x.Year).ToList();
            return list.RemoveReferences();
        }
       
        [HttpPost]
        [Route("getProduct")]
        public Product GetProduct(string ProductSKU,string StyleSKU)
        {
            var list = db.Products.Where(x => x.IsActive == true && x.ProductSKU.Contains(ProductSKU)).ToList().RemoveReferences();
            var product = list.Where(x => x.StyleSKU == StyleSKU).FirstOrDefault();
            return product;
        }
        [HttpGet]
        [Route("checkValidation")]
        public IHttpActionResult CheckValidation(string id)
        {
            
            string sku = id.Substring(0, 3);
            string style = id.Substring(3, id.Length-3);
            var product = db.Products.Where(x => x.IsActive == true && x.StyleSKU == style && x.ProductSKU == sku).FirstOrDefault();
			if (product == null)
			{
				return Ok(false);
			}
            return Ok(true);
        }
        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetails")]
        public List<Product> GetProducts()
        {
            var list = db.Products.Where(x => x.IsActive == true && x.IsAllowZero==false).OrderByDescending(x => x.Id).Include(x=>x.Buyer).Include(x=>x.Template).Include(x=>x.ProductCat1).Include(x => x.ProductCat2).Include(x => x.ProductCat3).Include(x => x.ProductCat4).Include(x=>x.ProductSource).Include(x=>x.Season).Include(x=>x.SizeGrid).Include(x=>x.Supplier).Include(x=>x.Year).ToList();
            return list.RemoveReferences();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("checkProductSKU")]
        public bool GetCode(string chk1,string chk2)
        {
            var data = db.Products.Any(x => x.ProductSKU.Contains(chk1) && x.IsActive == true && x.StyleSKU.Contains(chk2));
            return data;
        }
        [HttpPost]
        [Route("getProductId")]
        public IHttpActionResult ProductId(MarkDownAddModel model)
        {
            var productList = db.Products.Where(x => x.IsActive == true && x.ProductSKU == model.MarkDown.ProductSKU.ToString()).ToList().RemoveReferences();
            var product = productList.Where(x => x.StyleSKU == model.MarkDown.StyleSKU).FirstOrDefault();
            return Ok(product);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("checkBarcode")]
        public bool GetBarCode(string chk)
        {
            var data = db.Products.Any(x =>x.IsActive == true);
            return data;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getProductDetails")]
        public List<Product> GetAllProducts()
        {
            var list = db.Products.Where(x => x.IsActive == true).Include(x => x.Buyer).Include(x => x.Template).Include(x => x.ProductCat1).Include(x => x.ProductCat2).Include(x => x.ProductCat3).Include(x => x.ProductCat4).Include(x => x.ProductSource).Include(x => x.Season).Include(x => x.SizeGrid).Include(x => x.Supplier).Include(x => x.Year).ToList();
            return list.RemoveReferences();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getDetail")]
        [ResponseType(typeof(DAL.Product))]
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.Where(x=>x.Id==id).Include(x=>x.Color).Include(x=>x.Supplier).
                Include(x=>x.Buyer).Include(x => x.ProductCat1).Include(x => x.ProductCat2).Include(x => x.ProductCat3).Include(x => x.ProductCat4).Include(x=>x.ProductSource).Include(x=>x.Supplier).Include(x=>x.Color).Include(x => x.ProductCat1).Include(x => x.ProductCat2).Include(x => x.ProductCat3).Include(x => x.ProductCat4)
                .Include(x=>x.Season).Include(x=>x.SizeGrid).Include(x=>x.Template).Include(x=>x.ProductCat1).Include(x => x.ProductCat2).Include(x => x.ProductCat3).Include(x => x.ProductCat4).FirstOrDefault().RemoveReferences();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getProductPaging")]
        public ServiceResult<List<Product>> GetProductPaging([FromUri]Paging paging)
        {
            ServiceResult<List<Product>> model = new ServiceResult<List<Product>>();
            var source = db.Products.Where(x => x.IsActive == true)
                        .OrderByDescending(x =>x.Id).ToList().RemoveReferences();
            int count = source.Count();
            int CurrentPage = paging.pageNumber;
            int PageSize = 6;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            model.TotalCount = count;
            model.data = items;
            return model;
        }
        [HttpGet]
        [Route("ProductsAutocomplete")]
        public IHttpActionResult ProductsAutocomplete(string name)
        {
            var data = db.Products.Include(s => s.SizeGrid).Where(x => x.IsActive == true && x.ProductSKU.Contains(name)).ToList().Select(m => new ProductModel
            {
                ProductSKU = m.ProductSKU,
                Id = m.Id,
                SupplierStyle = m.SupplierStyle,
                SizeGridID = m.SizeGridID,
                autoCompleteSizeGrid = m.SizeGrid?.GridNumber,
                AvailableSize = m.AvailableSize,
				CostPrice = m.CostPrice.ToString(),
			}).ToList();
			var list = data.Select(x=>x.ProductSKU).Distinct();
            return Ok(list);
        }
		[HttpGet]
		[Route("StyleAutocomplete")]
		public IHttpActionResult StylesAutocomplete(string name)
		{
			var data = db.Products.Include(s => s.SizeGrid).Where(x => x.IsActive == true && x.StyleSKU.Contains(name)).ToList().Select(m => new ProductModel
			{
				StyleSKU = m.StyleSKU,
				Id = m.Id,
			}).ToList();
			var list = data.Select(x => x.StyleSKU).Distinct();
			return Ok(list);
		}
		[HttpGet]
        [Route("ProductStyleAutocomplete")]
        public IHttpActionResult ProductStyleAutocomplete(string name,string id)
        {
            var data=db.Products.Include(c=>c.Color).Include(c => c.SizeGrid).Where(x=>x.IsActive==true && x.StyleSKU.Contains(name) && x.ProductSKU==id).ToList().Select(m=>new ProductModel
            {
                StyleSKU = m.StyleSKU,
                Id = m.Id,
                AutoCompleteColorCode=m.Color?.Code,
                SupplierStyle = m.SupplierStyle,
                SizeGridID = m.SizeGridID,
                autoCompleteSizeGrid = m.SizeGrid?.GridNumber,
                AvailableSize = m.AvailableSize,
				ActualSellingPrice=m.ActualSellingPrice.ToString(),
                CostPrice=m.CostPrice.ToString(),
            }).ToList();
            return Ok(data);
        }
       [HttpGet]
        [Route("ProductsAutocompleteSelected")]
        public IHttpActionResult ProductsAutocompleteSelected(string name,int? id)
        {
            var data = db.Products.Include(s => s.SizeGrid).Where(x => x.IsActive == true && x.ProductSKU.Contains(name)).ToList().Select(m => new ProductModel
            {
                ProductSKU = m.ProductSKU,
                Id = m.Id,
                SupplierStyle = m.SupplierStyle,
                SizeGridID = m.SizeGridID,
                autoCompleteSizeGrid = m.SizeGrid?.GridNumber,
                AvailableSize = m.AvailableSize,
				CostPrice = m.CostPrice.ToString(),
			}).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("Check")]
        public IHttpActionResult Check(ValidationProduct model)
        {
            var data = db.Products.Any(x => x.IsActive == true && x.ColorID == model.ColorID && x.SupplierID == model.SupplierID && x.SupplierStyle == model.SupplierStyle);
            return Ok(data);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("getProducts")]
        public ServiceResult<List<Product>> GetProducts(Model.ProdSearch prodSearch)
        {
            var pageSize = 6;
            ServiceResult<List<Product>> model = new ServiceResult<List<Product>>();

            var list = db.Products.Where(x => x.IsActive == true && x.IsAllowZero==false)
                       .Include(x=>x.Season);
			List<Product> Temp = list.ToList();
			if (prodSearch != null)
            {
                if (prodSearch.IsActive == false)
                {
                    list = db.Products.Where(n => n.IsActive == false);
                }
				
				if (!string.IsNullOrEmpty(prodSearch.Colors))
                {
                    string colors = prodSearch.Colors;
                    if (colors != "null")
                    {
                        string[] values = colors.Split(',');
                        var l = values.Count();
                        for (int i = 0; i < l; i++)
                        {
                            var k = 0;
                            var vals = values[i];
                            k = Convert.ToInt32(vals);
                            if (vals != "")
                            {
                                list = list.Where(x => x.ColorID == k);
                                Temp.AddRange(list.Where(x => x.ColorID == k));
                                Temp.AddRange(list.Where(x => x.Color.Code == vals));
                            }
                        }
					
                    }
                }
                if (!string.IsNullOrEmpty(prodSearch.AutocompleteProductSKU))
                {
                   // Temp.AddRange(list.Where(x => x.ProductSKU == prodSearch.AutocompleteProductSKU));
                    list = list.Where(x => x.ProductSKU.Contains(prodSearch.AutocompleteProductSKU));
                }
				if(!string.IsNullOrEmpty(prodSearch.AutocompleteStyleSKU))
				{
					if (!string.IsNullOrEmpty(prodSearch.AutocompleteProductSKU))
					{
						list=list.Where(x => x.ProductSKU.Contains(prodSearch.AutocompleteProductSKU) && x.StyleSKU.Contains(prodSearch.AutocompleteStyleSKU));
					}
					else
					{
						list.Where(x => x.StyleSKU.Contains(prodSearch.AutocompleteStyleSKU));
					}
				}
                if (!string.IsNullOrEmpty(prodSearch.AutocompleteSeason))
                {
                   // Temp.AddRange(list.Where(x => x.Season.Code == prodSearch.AutocompleteSeason));
                   list = list.Where(x => x.Season.Code.Contains(prodSearch.AutocompleteSeason));
                }
                if (prodSearch.MaxPrice<=999 && prodSearch.MinPrice>=30)
                {
                    //Temp.AddRange(list.Where(x => x.CostPrice >= prodSearch.MinPrice && x.CostPrice <= prodSearch.MaxPrice));
                   list = list.Where(x => x.CostPrice >= prodSearch.MinPrice  && x.CostPrice<=prodSearch.MaxPrice);
                }
                if (prodSearch.ProdCat1ID > 0)
                {
                    list = list.Where(x => x.ProdCat1ID == prodSearch.ProdCat1ID);
                }
                if (prodSearch.ProdCat2ID > 0)
                {
                    list = list.Where(x => x.ProdCat2ID == prodSearch.ProdCat2ID);
                }
                if (prodSearch.ProdCat3ID > 0)
                {
                    list = list.Where(x => x.ProdCat3ID == prodSearch.ProdCat3ID);
                }
                if (prodSearch.ProdCat4ID > 0)
                {
                    list = list.Where(x => x.ProdCat4ID == prodSearch.ProdCat4ID);
                }
            }
           var result =list.ToList();
            int count = result.Count();
            var items = result.OrderByDescending(x => x.Id).Skip(((prodSearch.Page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            model.data = items.Select(x => new Product
            {
                Id = x.Id,
                PrimaryImage = x.PrimaryImage,
                StyleSKU=x.StyleSKU,
                CostPrice = x.CostPrice,
                ProductSKU = x.ProductSKU,
                Season = x.Season
            }).ToList().RemoveReferences();
            model.TotalCount = count;
            return model;
        }
        [HttpGet]
        [Route("issupplierStylexist")]
        public IHttpActionResult isColorExist(int Id, string Style)
        {
            var color = db.Products.Any(s => s.IsActive == true && s.Id != Id && s.SupplierStyle == Style);
            if (color)
                return Ok(true);
            else
                return Ok(false);
        }
        [HttpPost]
        [Route("checkProductSKU")]
        public bool GetEmail(Product model)
        {
            if (model.Id > 0)
            {
                var code = db.Products.Where(x => x.Id == model.Id && x.IsActive == true).FirstOrDefault();
                if (code.ProductSKU.Equals(model.ProductSKU))
                {
                    return false;
                }

            }
            var data = db.Products.Any(x => x.ProductSKU == model.ProductSKU && x.IsActive == true);
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ProductSearch")]
        public List<Product> GetProductSearch(Model.ProdSearch prodSearch)
        {
            var list = db.Products.Where(n => n.IsActive == true).OrderByDescending(x => x.Id).ToList();
            if (prodSearch != null)
            {
                if (!string.IsNullOrEmpty(prodSearch.ProductSKU))
                {
                    list = list.Where(x => x.ProductSKU.ToLower().Equals(prodSearch.ProductSKU.ToLower())&& x.IsActive == true).ToList();
                }
                if (!string.IsNullOrEmpty(prodSearch.StyleSKU))
                {
                    list = list.Where(x => x.StyleSKU.ToLower().Contains(prodSearch.StyleSKU.ToLower()) && x.IsActive == true).ToList();
                }
            }
            return list.RemoveReferences();
        }
		[HttpPost]
		[Route("addList")]
		public IHttpActionResult CreateList(Dictionary<int,Product> list)
		{
            Dictionary<string, string> result = new Dictionary<string, string>();
			string Message = string.Empty;
			foreach(var item in list)
			{
                item.Value.CreatedOn = System.DateTime.UtcNow.ToString();
                item.Value.UpdatedOn = System.DateTime.UtcNow;
                result.Add(item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU, "");
                try
				{
					db.Products.Add(item.Value);
                    result[item.Key+"#"+item.Value.ProductSKU + "#" + item.Value.StyleSKU] = "Add";
                    db.SaveChanges();
				}catch(Exception ex)
				{
                    if(ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU] = ex.Message;
                    }
				}
			}
			return Ok(result);
		}
        [HttpPost]
        [Route("updateList")]
        public IHttpActionResult UpdateList(Dictionary<int, Product> list)
        {
            var pSKU = list.Where(s => s.Value.ProductSKU != null).Select(x => x.Value.ProductSKU).ToList();
            var sSKU = list.Where(s => s.Value.StyleSKU != null).Select(x => x.Value.StyleSKU).ToList();
            var products = db.Products.Where(x => x.IsActive == true);
            if (pSKU != null && pSKU.Count > 0)
            {
                products.Where(x => pSKU.Contains(x.ProductSKU));
            }
            if (sSKU != null && sSKU.Count > 0)
            {
                products.Where(x => sSKU.Contains(x.StyleSKU));
            }
            var Products = products.ToList();
            Dictionary<string, string> result = new Dictionary<string, string>();
            string Message = string.Empty;
            foreach (var item in list)
            {
                result.Add(item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU, "");
                Product obj = Products.Where(x => x.ProductSKU == item.Value.ProductSKU && x.StyleSKU == item.Value.StyleSKU).FirstOrDefault();
                obj.UpdatedOn = System.DateTime.UtcNow;
                obj.ActualSellingPrice = item.Value.ActualSellingPrice ?? 0;
                obj.AvailableSize = item.Value.AvailableSize;
                obj.Barcode = item.Value.Barcode;
                obj.BuyerID = item.Value.BuyerID;
                obj.ColorID = item.Value.ColorID;
                obj.CostPrice = item.Value.CostPrice;
                obj.CostPriceUSD = item.Value.CostPriceUSD;
                obj.CreatedOn = item.Value.CreatedOn;
                obj.DefaultTemplateID = item.Value.DefaultTemplateID;
                obj.Image1 = item.Value.Image1;
                obj.CostPriceUSD = item.Value.CostPriceUSD;
                obj.Image2 = item.Value.Image2;
                obj.Image3 = item.Value.Image3;
                obj.Image4 = item.Value.Image4;
                obj.IsActive = item.Value.IsActive;
                obj.IsAllowZero = item.Value.IsAllowZero;
                obj.IsConsignment = item.Value.IsConsignment;
                obj.IsDiscontinue = item.Value.IsDiscontinue;
                obj.IsFreeGift = item.Value.IsFreeGift;
                obj.IsMarkDown = item.Value.IsMarkDown;
                obj.IsVPI = item.Value.IsVPI;
                obj.LogId = item.Value.LogId;
                obj.LongDescription = item.Value.LongDescription;
                obj.MarkDownTemplateID = item.Value.MarkDownTemplateID;
                obj.PrimaryImage = item.Value.PrimaryImage;
                obj.ProdCat1ID = item.Value.ProdCat1ID;
                obj.ProdCat2ID = item.Value.ProdCat2ID;
                obj.ProdCat3ID = item.Value.ProdCat3ID;
                obj.ProdCat4ID = item.Value.ProdCat4ID;
                obj.ProductSKU = item.Value.ProductSKU;
                obj.ProductSourceID = item.Value.ProductSourceID;
                obj.StyleSKU = item.Value.StyleSKU;
                obj.RecommendedSellingPrice = item.Value.RecommendedSellingPrice;
                obj.SeasonID = item.Value.SeasonID;
                obj.ShortDescription = item.Value.ShortDescription;
                obj.SizeGridID = item.Value.SizeGridID;
                obj.SupplierID = item.Value.SupplierID;
                obj.SupplierStyle = item.Value.SupplierStyle;
                obj.UpdatedOn = item.Value.UpdatedOn;
                obj.CostPriceUSD = item.Value.CostPriceUSD;
                obj.YearID = item.Value.YearID;
                try
                {
                    result[item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU] = "Update";
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.Message != null)
                    {
                        result[item.Key + "#" + item.Value.ProductSKU + "#" + item.Value.StyleSKU] = ex.Message;
                    }
                }
            }
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getCode")]
        public IQueryable<string> GetProductCode()
        {
            var list = from s in db.Products.Where(x => x.IsActive == true)
                       select s.ProductSKU;
            return list;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("edit")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(DAL.Product product)
        {
			var list = new List<Product>();
			var model = db.Products.Where(x => x.IsActive == true && x.Id ==product.Id).FirstOrDefault();
            model.Id = product.Id;
            model.Image1 = product.Image1;
            model.Image2 = product.Image2;
            model.Image3 = product.Image3;
            model.Image4 = product.Image4;
            model.IsActive = true;
            model.ActualSellingPrice = product.ActualSellingPrice;
            model.IsAllowZero = product.IsAllowZero;
            model.IsConsignment = product.IsConsignment;
            model.IsDiscontinue = product.IsDiscontinue;
            model.IsFreeGift = product.IsFreeGift;
            model.IsMarkDown = product.IsMarkDown;
            model.IsVPI = product.IsVPI;
            model.LogId = product.LogId;
			model.CostPrice = product.CostPrice;
            // model.Log = db.Logs.Where(x => x.IsActive == true).FirstOrDefault();
            model.LongDescription = product.LongDescription;
            model.MarkDownTemplateID = product.MarkDownTemplateID;
            model.PrimaryImage = product.PrimaryImage;
            model.ProdCat1ID = product.ProdCat1ID;
            model.ProdCat2ID = product.ProdCat2ID;
            model.ProdCat3ID = product.ProdCat3ID;
            model.ProdCat4ID = product.ProdCat4ID;
            model.ColorID = product.ColorID;
            model.CostPriceUSD = product.CostPriceUSD;
            model.Color = db.Colors.Where(x => x.IsActive == true && x.Id == model.ColorID).FirstOrDefault();
            //model.ProductGroupID = product.ProductGroupID;
            //model.ProductGrp = db.ProductGrps.Where(x => x.IsActive == true && x.ID== product.ProductGroupID).FirstOrDefault();
            //model.ProductSizes = product.ProductSizes;
            model.ProductSKU = product.ProductSKU;
            model.ProductSourceID = product.ProductSourceID;
            model.ProductSource = db.ProductSources.Where(x => x.IsActive == true && x.Id == product.ProductSourceID).FirstOrDefault();
			model.StyleSKU = product.StyleSKU;
          //  model.ProductStyle = db.ProductStyles.Where(x => x.IsActive == true && x.Id == product.ProductStyleId).FirstOrDefault();
            model.RecommendedSellingPrice = product.RecommendedSellingPrice;
            model.SeasonID = product.SeasonID;
            model.Season = db.Seasons.Where(x => x.IsActive == true && x.Id == product.SeasonID).FirstOrDefault();
            model.ShortDescription = product.ShortDescription;
            model.SizeGridID = product.SizeGridID;
            model.SizeGrid = db.SizeGrids.Where(x => x.IsActive == true && x.Id == product.SizeGridID).FirstOrDefault();
            model.SupplierID = product.SupplierID;
            model.Supplier = db.Suppliers.Where(x => x.IsActive == true && x.Id == product.SupplierID).FirstOrDefault();
            model.SupplierStyle = product.SupplierStyle;
			//   model.UpdatedOn = product.UpdatedOn;
			model.MarkDownTemplateID = product.MarkDownTemplateID;
			model.DefaultTemplateID = product.DefaultTemplateID;
			model.Barcode = model.StyleSKU + model.ProductSKU + model.Color.Code;
            model.YearID = product.YearID;
			model.CreatedOn = product.CreatedOn;
            model.UpdatedOn = System.DateTime.UtcNow;
            model.CostPriceUSD = product.CostPriceUSD;
			model.AvailableSize = product.AvailableSize;
			model.SupplierStyle = product.SupplierStyle;
            model.Year = db.Years.Where(x => x.IsActive == true && x.Id == product.YearID).FirstOrDefault();
			db.SaveChanges();
			return Ok(true);
        }
        [HttpPost]
        [Route("editByMark")]
        public IHttpActionResult EditByMark(MarkDown mark)
        {
            var model = db.Products.Where(x => x.IsActive == true && x.ProductSKU==mark.ProductSKU && x.StyleSKU==mark.StyleSKU).FirstOrDefault();
            model.ActualSellingPrice = mark.NewSellingPrice;
            model.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        [ResponseType(typeof(DAL.Product))]
        public IHttpActionResult PostProduct(Product product)
        {
			var pageName = Request.RequestUri.LocalPath.getRouteName();
			Object obj = null;
			var UserId = 0;
			if (Request.Headers.Contains("Email"))
			{
				var email = ((string[])(Request.Headers.GetValues("Email")))[0].ToString();
				UserId = db.Users.Where(x => x.IsActive == true && x.Email.Contains(email)).FirstOrDefault().Id;
			}
			Product model = new Product();
            model.ActualSellingPrice = Convert.ToDecimal(product.ActualSellingPrice);
            model.AvailableSize = product.AvailableSize;
            model.Barcode = product.Barcode;
            model.BuyerID = product.BuyerID;
            model.ColorID = product.ColorID;
            model.CostPrice = product.CostPrice;
            model.CostPriceUSD = product.CostPriceUSD;
			model.CreatedOn = product.CreatedOn;
            model.DefaultTemplateID = product.DefaultTemplateID;
            model.Image1 = product.Image1;
            model.Image2 = product.Image2;
            model.Image3 = product.Image3;
            model.Image4 = product.Image4;
            model.IsActive = true;
            model.SupplierStyle = product.SupplierStyle;
            model.IsAllowZero = product.IsAllowZero;
            model.IsConsignment = product.IsConsignment;
            model.IsDiscontinue = product.IsDiscontinue;
            model.IsFreeGift = product.IsFreeGift;
            model.IsMarkDown = product.IsMarkDown;
            model.PrimaryImage = product.PrimaryImage;
            model.ProdCat1ID = product.ProdCat1ID;
            model.ProdCat2ID = product.ProdCat2ID;
            model.ProdCat3ID = product.ProdCat3ID;
            model.ProdCat4ID = product.ProdCat4ID;
            model.ProductSKU = product.ProductSKU;
            model.ProductSourceID = product.ProductSourceID;
           model.StyleSKU = product.StyleSKU;
            model.SeasonID = product.SeasonID;
            model.SizeGridID = product.SizeGridID;
            model.ShortDescription = product.ShortDescription;
            model.SupplierID = product.SupplierID;
            model.SupplierStyle = product.SupplierStyle;
            model.UpdatedOn = System.DateTime.UtcNow; ;
            model.YearID = product.YearID;
			model.MarkDownTemplateID = product.MarkDownTemplateID;
			model.DefaultTemplateID = product.DefaultTemplateID;
            model.RecommendedSellingPrice = Convert.ToDecimal(product.RecommendedSellingPrice);
            //model.LogId = product.LogId;
            model.LongDescription = product.LongDescription;
            db.Products.Add(model);
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex) { obj = ex; }
			finally
			{
				if (obj == null)
				{

					var logTable = Newtonsoft.Json.JsonConvert.SerializeObject(product, new JsonSerializerSettings()
					{
						PreserveReferencesHandling = PreserveReferencesHandling.Objects,
						Formatting = Formatting.Indented
					});
					var flag = product.CreateLog(pageName, logTable, UserId);
				}
			}
			return Ok(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("delete")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsActive = false;
            product.UpdatedOn = System.DateTime.UtcNow;
            db.SaveChanges();
            return Ok(product);
        }
		[HttpPost]
		[Route("checkColorName")]
		public IHttpActionResult CheckColorName(int? id,int ColorId)
		{
			var status = db.Products.Any(x => x.IsActive == true && x.Id != id && x.ColorID==ColorId);
            if (status)
                return Ok(true);
            else
                return Ok(false);
        }
		[HttpPost]
		[Route("CheckSupplier")]
		public IHttpActionResult CheckSupplier(int? id,int SupplierId)
		{
			var status = db.Products.Any(x => x.IsActive == true && x.Id != id && x.SupplierID==SupplierId);
            if (status)
                return Ok(true);
            else
                return Ok(false);
        }
		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
        //getProductImportFilter
        [HttpPost]
        [Route("getProductImportFilter")]
        public IHttpActionResult getProductImportFilter(Dictionary<int, ProductImport> model)
        {
            if (model != null && model.Count > 0)
            {
                var Buyers = model.Where(x => x.Value.Buyer != null).Select(s => s.Value.Buyer).Distinct().ToList();
                var Colors = model.Where(x => x.Value.Color != null).Select(s => s.Value.Color).Distinct().ToList();
                var DefaultTemplates = model.Where(x => x.Value.DefaultTemplate != null).Select(s => s.Value.DefaultTemplate).Distinct().ToList();
                var MarkdownTemplates = model.Where(x => x.Value.MarkdownTemplate != null).Select(s => s.Value.MarkdownTemplate).Distinct().ToList();
                var ProductCat1s = model.Where(x => x.Value.ProductCat1 != null).Select(s => s.Value.ProductCat1).Distinct().ToList();
                var ProductCat2s = model.Where(x => x.Value.ProductCat2 != null).Select(s => s.Value.ProductCat2).Distinct().ToList();
                var ProductCat3s = model.Where(x => x.Value.ProductCat3 != null).Select(s => s.Value.ProductCat3).Distinct().ToList();
                var ProductCat4s = model.Where(x => x.Value.ProductCat4 != null).Select(s => s.Value.ProductCat4).Distinct().ToList();
                var ProductSources = model.Where(x => x.Value.ProductSource != null).Select(s => s.Value.ProductSource).Distinct().ToList();
                var Seasons = model.Where(x => x.Value.Season != null).Select(s => s.Value.Season).Distinct().ToList();
                var SizeGrids = model.Where(x => x.Value.SizeGridNo != null).Select(s => s.Value.SizeGridNo).Distinct().ToList();
                var Suppliers = model.Where(x => x.Value.Supplier != null).Select(s => s.Value.Supplier).Distinct().ToList();
                var Year = model.Where(x => x.Value.Year != null).Select(s => s.Value.Year).Distinct().ToList();
                var buyers = db.Buyers.Where(x => Buyers.Contains(x.Name)).ToList();
                var colors = db.Colors.Where(x => Colors.Contains(x.Code)).ToList();
                var defaultTemplates = db.Templates.Where(x => DefaultTemplates.Contains(x.Name)).ToList();
                var markdownTemplates = db.Templates.Where(x => MarkdownTemplates.Contains(x.Name)).ToList();
                var productCat1s = db.ProductCat1.Where(x => ProductCat1s.Contains(x.CateName)).ToList();
                var productCat2s = db.ProductCat2.Where(x => ProductCat2s.Contains(x.CateName)).ToList();
                var productCat3s = db.ProductCat3.Where(x => ProductCat3s.Contains(x.CateName)).ToList();
                var productCat4s = db.ProductCat4.Where(x => ProductCat4s.Contains(x.CateName)).ToList();
                var productSources = db.ProductSources.Where(x => ProductSources.Contains(x.Source)).ToList();
                var seasons = db.Seasons.Where(x => Seasons.Contains(x.Code)).ToList();
                var sizeGrids = db.SizeGrids.Where(x => SizeGrids.Contains(x.GridNumber)).ToList();
                var suppliers = db.Suppliers.Where(x => Suppliers.Contains(x.Code)).ToList();
                var years = db.Years.Where(x => Year.Contains(x.Year1)).ToList();
                foreach (var item in model)
                {
                    item.Value.BuyerId = buyers.Where(x => x.Name == item.Value.Buyer).FirstOrDefault()?.Id;
                    if (item.Value.Color.Contains("\r"))
                    {
                        item.Value.Color = item.Value.Color.Replace("\r", "");
                    }
                    item.Value.ColorID = colors.Where(x => x.Code == item.Value.Color).FirstOrDefault()?.Id;
                    item.Value.DefaultTemplateId = defaultTemplates.Where(x => x.Name == item.Value.DefaultTemplate).FirstOrDefault()?.Id;
                    item.Value.MarkdownTemplateId = markdownTemplates.Where(x => x.Name == item.Value.MarkdownTemplate).FirstOrDefault()?.Id;
                    item.Value.ProductCat1ID = productCat1s.Where(x => x.CateName == item.Value.ProductCat1).FirstOrDefault()?.Id;
                    item.Value.ProductCat2ID = productCat2s.Where(x => x.CateName == item.Value.ProductCat2).FirstOrDefault()?.Id;
                    item.Value.ProductCat3ID = productCat3s.Where(x => x.CateName == item.Value.ProductCat3).FirstOrDefault()?.Id;
                    item.Value.ProductCat4ID = productCat4s.Where(x => x.CateName == item.Value.ProductCat4).FirstOrDefault()?.Id;
                    item.Value.ProductSourceId = productSources.Where(x => x.Source == item.Value.ProductSource).FirstOrDefault()?.Id;
                    item.Value.SeasonId = seasons.Where(x => x.Code == item.Value.Season).FirstOrDefault()?.Id;
                    item.Value.SizeGridId = sizeGrids.Where(x => x.GridNumber == item.Value.SizeGridNo).FirstOrDefault()?.Id;
                    item.Value.SupplierId = suppliers.Where(x => x.Code == item.Value.Supplier).FirstOrDefault()?.Id;
                    item.Value.YearId = years.Where(x => x.Year1 == item.Value.Year).FirstOrDefault()?.Id;
                }
            }
            return Ok(model);
        }

        //getProductCheckFilter
        [HttpPost]
        [Route("getProductCheckFilter")]
        public IHttpActionResult getProductCheckFilter(Dictionary<int, string> model)
        {
            var obj = new Dictionary<int, bool>();
            if (model != null && model.Count > 0)
            {
                var products = db.Products.Where(x => x.IsActive == true);// = new IQueryable<Product>();
                //var modelsplit = model.Select(x => x.Value.Split('#'))
                var PSku = model.Select(x => x.Value.Split('#')).Select(s => s[0]).Distinct().ToList();
                var SSku = model.Select(x => x.Value.Split('#')).Select(s => s[1]).Distinct().ToList();
                if (PSku != null && PSku.Count > 0)
                {
                    products = products.Where(x => PSku.Contains(x.ProductSKU));
                }
                if (SSku != null && SSku.Count > 0)
                {
                    products = products.Where(x => SSku.Contains(x.StyleSKU));
                }
                var Products = products.ToList();

                // db.Products.Where(x=> list.Select(i=>i).ToList().Contains(x.ProductSKU)).
                foreach (var item in model)
                {
                    var sku = item.Value.Split('#');
                    var result = Products.Any(x => x.ProductSKU == sku[0] && x.StyleSKU == sku[1]);
                    obj.Add(item.Key, result);
                }
            }
            return Ok(obj);
        }
        [HttpGet]
        [Route("getDropDownList")]
        public IHttpActionResult GetDropDownList()
        {
            DropDownProductListModel model = new DropDownProductListModel();
            model.BuyerModelList = db.Buyers.Where(x => x.IsActive == true).ToList().ToModelConverter<List<BuyerModel>>();
            model.Cat1List = db.ProductCat1.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ProductCat1Model>>();
            model.Cat2List = db.ProductCat2.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ProductCat2Model>>();
            model.Cat3List = db.ProductCat3.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ProductCat3Model>>();
            model.Cat4List = db.ProductCat4.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ProductCat4Model>>();
            model.ColorModelList = db.Colors.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ColorModel>>();
            model.freeGiftList = db.Products.Where(x => x.IsActive == true && x.IsAllowZero == true).ToList().RemoveReferences().ToModelConverter<List<ProductModel>>();
            model.ProductGroupModelList = db.ProductGrps.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ProductGrpModel>>();
            model.ProductSourceModelList = db.ProductSources.Where(x => x.IsActive == true).ToList().ToModelConverter<List<ProductSourceModel>>();
            model.SeasonModelList = db.Seasons.Where(x => x.IsActive == true).ToList().ToModelConverter<List<SeasonModel>>();
            model.SizeGridModelList = db.SizeGrids.Where(x => x.IsActive == true).ToModelConverter<List<SizeGridModel>>();
            model.SupplierModelList = db.Suppliers.Where(x => x.IsActive == true).ToList().ToModelConverter<List<SupplierModel>>();
            model.TemplateModelList = db.Templates.Where(x => x.IsActive == true).ToList().ToModelConverter<List<TemplateModel>>();
            return Ok(model);
        }
    }
}