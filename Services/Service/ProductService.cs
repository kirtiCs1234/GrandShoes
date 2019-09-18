using Helper;
using Model;
using Model.Product;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ProductService:IProductService
    {
        public List<AllProductDetailModel> GetAllProduct()

        {
            var body = "";
            List<AllProductDetailModel> AllProductDetailModels = ServerResponse.Invoke<List<AllProductDetailModel>>("api/product/getDetails", body, "get");
            return AllProductDetailModels;
        }
        public List<MarkDownProduct> GetAllProductMarkDown()
        {
            return ServerResponse.Invoke<List<MarkDownProduct>>("api/product/getDetails", "", "GET");
        }
        public List<ProductModel> GetFreeGift()
        {
            var freeGiftList = ServerResponse.Invoke<List<ProductModel>>("api/product/getFreeGift", "", "GET");
            return freeGiftList;
        }
        public bool CheckValidation(string id)
        {
            return ServerResponse.Invoke<bool>("api/product/checkValidation?id=" + id, "", "GET");
        }
        public List<AllProductDetailModel> GetFreeGiftList()
        {

            var freeGiftList = ServerResponse.Invoke<List<AllProductDetailModel>>("api/product/getFreeGift", "", "GET");
            return freeGiftList;
        }
        public List<ProductModel> GetListByProductSKU(string productSKU)
        {
          return ServerResponse.Invoke<List<ProductModel>>("api/product/getByProductSKU?productSKU=" + productSKU, "", "GET");
        }
        public bool CheckExistingSupplier(int Id, string Style)
        {
            Style = System.Web.HttpUtility.UrlEncode(Style);
            return ServerResponse.Invoke<bool>("/api/product/issupplierStylexist?id=" + Id + "&style=" + Style, "", "Get");
        }
        public ProductModel GetValues(string ProductSKU,string StyleSKU)
        {
            var product = ServerResponse.Invoke<ProductModel>("api/product/getProduct?ProductSKU=" + ProductSKU + "&&StyleSKU=" + StyleSKU, "", "Post");
            return product;
        }
        public YearModel GetYearId(string sku)
        {
            var getYearId = ServerResponse.Invoke<YearModel>("api/year/getYearId?sku=" + sku, "", "POST");
            return getYearId;
        }
        public List<ReceiptOrderItemModel> ReceiptByProduct(int? id)
        {
            return ServerResponse.Invoke<List<ReceiptOrderItemModel>>("api/receiptOrderItems/getReceiptByProduct?id="+id, "", "GET");

        }
        public List<ProductModel> GetAll()

        {
            var body = "";
            List<ProductModel> ProductDetailModels = ServerResponse.Invoke<List<ProductModel>>("api/product/getProductDetails", body, "get");
            return ProductDetailModels;
        }
        public Dictionary<string, string> CreateList(Dictionary<int,ProductModel> list)
		{
			return ServerResponse.Invoke<Dictionary<string,string>>("api/product/addList", JsonConvert.SerializeObject(list), "Post");
		}
        public Dictionary<string, string> UpdateList(Dictionary<int,ProductModel> list)
        {
            return ServerResponse.Invoke<Dictionary<string, string>>("api/product/updateList", JsonConvert.SerializeObject(list), "Post");
        }

        public MarkDownProduct GetByIdMarkDown(int? id)
        {
            var body = "";
            MarkDownProduct ProductModelById = ServerResponse.Invoke<MarkDownProduct>("api/product/getDetail?id=" + id, body, "GET");
            return ProductModelById;
        }
        public ProductModel GetById(int? id)
        {
            var body = "";
            ProductModel ProductModelById = ServerResponse.Invoke<ProductModel>("api/product/getDetail?id=" + id, body, "GET");
            return ProductModelById;
        }
        public bool CheckProductSKU(string chk1,string chk2)
        {
           // var body = JsonConvert.SerializeObject(chk1,chk2);
            var CheckBranchCode = ServerResponse.Invoke<bool>("api/product/checkProductSKU?chk1=" + chk1+"&chk2="+chk2," ", "GET");
            return CheckBranchCode;
        }
        public bool Create(ProductModel productModel)
        {
            var body = JsonConvert.SerializeObject(productModel);
            bool ProductCreate = ServerResponse.Invoke<bool>("api/product/create", body, "Post");
            return ProductCreate;
        }
        public bool Edit(ProductModel productModel)
        {
            var body = JsonConvert.SerializeObject(productModel);
            bool ProductEdit = ServerResponse.Invoke<bool>("api/product/edit", body, "POST");
            return ProductEdit;
        }
       public MarkDownProduct GetProductID(MarkDownAddModel model)
        {
            return ServerResponse.Invoke<MarkDownProduct>("api/product/getProductId", JsonConvert.SerializeObject(model), "POST");
        }
        public bool EditByMark(MarkDownModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            bool ProductEdit = ServerResponse.Invoke<bool>("api/product/editByMark", body, "POST");
            return ProductEdit;
        }
        public OffersModel GetPaging(int? page, out int TotalCount)

        {
            var body = "";
            // int pageSize = 10;
            int pageNumber = (page ?? 1);
            var model1 = new OffersModel();
            var productList = ServerResponse.Invoke<ServiceResult<List<ProductModel>>>("api/product/getProductPaging?pageNumber=" + page, body, "GET");
            TotalCount = productList.TotalCount;

            if (productList.data != null)
            {
                model1.ProductList = productList.data.ToList();
                return model1;
            }
            else
            {

            }
            return model1;
        }

        public List<ProductModel> GetProduct(ProdSearch prodSearch, int? page, out int TotalCount)
        {
            int pageNumber = (page ?? 1);
            var body = JsonConvert.SerializeObject(prodSearch);
           // var model1 = new OffersModel();
            var productList = ServerResponse.Invoke<ServiceResult<List<ProductModel>>>("api/product/getProducts", body, "POST");
            TotalCount = productList.TotalCount;

            if (productList.data != null)
            {
                var model = productList.data.ToList();
                return model;
            }
            else
            {

            }
            return productList.data.ToList();

        }
        public List<ProductModel> ProductStyleAutocomplete(string name, string id)
        {
            return ServerResponse.Invoke<List<ProductModel>>("api/Product/ProductStyleAutocomplete?name=" + name + "&&id=" + id, "", "GET");
        }
        public List<ProductModel> ProductAutocompleteSelected(string name)
        {
            return ServerResponse.Invoke<List<ProductModel>>("api/Product/ProductsAutocompleteSelected?name=" + name, "", "get");
        }
        public List<string> ProductAutocomplete(string name)
        {
            return ServerResponse.Invoke<List<string>>("api/Product/ProductsAutocomplete?name=" + name, "", "get");
        }
		public List<string> StyleAutocomplete(string name)
		{
			return ServerResponse.Invoke<List<string>>("api/Product/StyleAutocomplete?name=" + name, "", "get");
		}
		public List<AllProductDetailModel> SearchProduct(ProdSearch prodSearch)
        {
            var body = JsonConvert.SerializeObject(prodSearch);
            List<AllProductDetailModel> productModelList = ServerResponse.Invoke<List<AllProductDetailModel>>("api/product/ProductSearch", body, "Post");
            return productModelList;
        }
       
        public List<string> GetCode()
        {
            var body = "";
            List<string> ProductCodeList = ServerResponse.Invoke<List<string>>("api/product/getCode", body, "get");
            return ProductCodeList;
        }
        public bool CheckProductSKU(ProductModel model)
        {
            var body = JsonConvert.SerializeObject(model);
            var CheckProductSKU = ServerResponse.Invoke<bool>("api/product/checkProductSKU", body, "POST");
            return CheckProductSKU;
        }
        public ProductModel Delete(ProductModel productModel)
        {
            var body = JsonConvert.SerializeObject(productModel);
            var ProductDelete = ServerResponse.Invoke<ProductModel>("api/product/Delete?id=" + productModel.Id, body, "POST");
            return ProductDelete;
        }
		public ManageTemplate ShowTemplate(int TemplateId, int ProductId)
		{
			ManageTemplate mt = new ManageTemplate();

			var product = GetById(ProductId);
			TemplateModel DefaultTemplate = ServerResponse.Invoke<TemplateModel>("api/template/getDetail?id=" + product.DefaultTemplateID, "", "GET");
			TemplateModel MarkDownTemplate = ServerResponse.Invoke<TemplateModel>("api/template/getDetail?id=" + product.MarkDownTemplateID, "", "GET");
            TemplateModel CommanTemplate = ServerResponse.Invoke<TemplateModel>("api/template/getDetail?id=" + product.DefaultTemplateID, "", "GET");

            DefaultTemplate.TemplateHtml = ShowTemplate(DefaultTemplate, ProductId,"def");
            CommanTemplate.TemplateHtml = ShowTemplate(CommanTemplate, ProductId, "def1");
            MarkDownTemplate.TemplateHtml = ShowTemplate(MarkDownTemplate, ProductId,"mark");
			mt.DefaultTemplate = DefaultTemplate;
			mt.MarkDownTemplate = MarkDownTemplate;
            mt.CommanTemplate = CommanTemplate;
			return mt;
		}
		private string ShowTemplate(TemplateModel model, int ProductId, string tmpl)
		{
			TemplateVariable tVariable = new TemplateVariable();
			BarcodeDesignModel tValue = getBarcodeData(ProductId);
           // var size1 = size;
			var barcode = "barcode"+tmpl;
			var Barcode = @"<input type='hidden' value='" + tValue.Barcode + "' id='hiddenbarcode"+tmpl+"' class='abc'><svg id = 'barcode"+tmpl+"'></svg><script>JsBarcode('#barcode"+tmpl+"', '" + tValue.Barcode+ "', {width: 2,height: 40});</script>";
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
				updateHtml = updateHtml.Replace(tVariable.productname, tValue.ProductName);
				updateHtml = updateHtml.Replace(tVariable.productid, tValue.ProductId);
				updateHtml = updateHtml.Replace(tVariable.unit, tValue.Unit);
				updateHtml = updateHtml.Replace(tVariable.quantity, tValue.Quantity);
				updateHtml = updateHtml.Replace(tVariable.discount, tValue.Discount);
				updateHtml = updateHtml.Replace(tVariable.price, tValue.Price);
				updateHtml = updateHtml.Replace(tVariable.priceafterdiscount, tValue.PriceAfterDiscount);
			}

			model.TemplateHtml = updateHtml;
			return updateHtml;
		}
		private BarcodeDesignModel getBarcodeData(int ProductId)
		{
			BarcodeDesignModel model = new BarcodeDesignModel();
			ProductModel product = GetById(ProductId);
			//var product = ServerResponse.Invoke<ProductModel>(api + "getProduct?id=" + ProductId, body, get);

			model.Name = product.ProductSKU;
			model.Barcode = product.Barcode;
			model.Price = product.ActualSellingPrice.ToString();
			//  model.PriceAfterDiscount = product.PriceAfterDiscount.ToString();
			//model.Discount = product.Discount.ToString();
			model.ProductId = product.ProductSKU;
			//if (product.Quantity == null || product.Quantity == 0)
			//{
			//    model.Quantity = "1";
			//}
			//else
			//{
			//    model.Quantity = product.Quantity.ToString();
			//}
			return model;
		}
		public bool CheckColorCode(int? id,int ColorId)
		{
			var body = JsonConvert.SerializeObject(ColorId);
			var CheckColorName = ServerResponse.Invoke<bool>("api/product/checkColorName?id="+id+ "&ColorId=" + ColorId, body, "POST");
			return CheckColorName;
		}
		public bool CheckSupplier(int? id,int SupplierId)
		{
			var body = JsonConvert.SerializeObject(SupplierId);
			var CheckSupplier = ServerResponse.Invoke<bool>("api/product/checkSupplier?id="+id+ "&SupplierId=" + SupplierId, body, "POST");
			return CheckSupplier;
		}
		public bool Check(ValidationProduct model)
        {
            return ServerResponse.Invoke<bool>("api/product/Check", JsonConvert.SerializeObject(model), "POST");
        }


        public Dictionary<int, bool> ProductCheckFilter(Dictionary<int, string> list)
        {

            var body = JsonConvert.SerializeObject(list);
            var obj = ServerResponse.Invoke<Dictionary<int, bool>>("api/product/getProductCheckFilter", body, "POST");
            return obj;
        }
        public Dictionary<int, ProductImport> ProductImportFilter(Dictionary<int, ProductImport> list)
        {

            var body = JsonConvert.SerializeObject(list);
            var obj = ServerResponse.Invoke<Dictionary<int, ProductImport>>("api/product/getProductImportFilter", body, "POST");
            return obj;
        }
        public DropDownProductListModel GetDropDownProductList()
        {
            var data= ServerResponse.Invoke<DropDownProductListModel>("api/product/getDropDownList", "", "GET");
            return data;
        }
       
    }
}
