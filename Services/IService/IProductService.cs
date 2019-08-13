﻿using Model;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IProductService
    {
        List<ProductModel> GetListByProductSKU(string productSKU);
        List<ProductModel> GetAll();
        OffersModel GetPaging(int? page, out int TotalCount);
        bool Create(ProductModel productModel);
        ProductModel GetById(int? id);
        bool Edit(ProductModel productModel);
        List<ProductModel> GetProduct(ProdSearch prodSearch, int? page, out int TotalCount);
        List<AllProductDetailModel> SearchProduct(ProdSearch prodSearch);
        List<string> GetCode();
        List<string> ProductAutocomplete(string name);
        List<ProductModel> ProductAutocompleteSelected(string name);
        bool CheckProductSKU(ProductModel model);
        List<AllProductDetailModel> GetAllProduct();
        ProductModel Delete(ProductModel productModel);
        List<ProductModel> GetFreeGift();
        List<AllProductDetailModel> GetFreeGiftList();
        bool CheckProductSKU(string chk1,string chk2);
        YearModel GetYearId(string sku);
        ProductModel GetValues(string ProductSKU, string StyleSKU);
        List<MarkDownProduct> GetAllProductMarkDown();
        MarkDownProduct GetByIdMarkDown(int? id);
        bool EditByMark(MarkDownModel model);
        MarkDownProduct GetProductID(MarkDownAddModel model);
        List<ReceiptOrderItemModel> ReceiptByProduct(int? id);
        List<ProductModel> ProductStyleAutocomplete(string name, string id);
        bool CheckValidation(string id);
        Dictionary<string,string> CreateList(Dictionary<int,ProductModel> list);
        Dictionary<string, string> UpdateList(Dictionary<int,ProductModel> list);
		ManageTemplate ShowTemplate(int TemplateId, int ProductId);
		List<string> StyleAutocomplete(string name);
		bool CheckSupplier(int? id,int SupplierId);
		bool CheckColorCode(int? id,int ColorId);
        bool CheckExistingSupplier(int Id, string Style);
        bool Check(ValidationProduct model);
        Dictionary<int, bool> ProductCheckFilter(Dictionary<int, string> list);
        Dictionary<int, ProductImport> ProductImportFilter(Dictionary<int, ProductImport> list);
        DropDownProductListModel GetDropDownProductList();
    }
}
