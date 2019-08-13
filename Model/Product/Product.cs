﻿using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Model
{
    public  class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Product SKU.")]
        [RegularExpression("\\d{3}", ErrorMessage ="Please Enter Three Digits Only.")]
        public string ProductSKU { get; set; }
        public string StyleSKU { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        [Required(ErrorMessage ="Please Enter Size Grid No.")]
        public int? SizeGridID { get; set; }
        public string RecommendedSellingPrice { get; set; }
        [Required(ErrorMessage ="Please Enter Actual Selling Price.")]
        public string ActualSellingPrice { get; set; }
        public Nullable<bool> IsVPI { get; set; }
        public Nullable<bool> IsMarkDown { get; set; }
        public int? CountMarkDown { get; set; }
        public string PreDate { get; set; }
        [Required(ErrorMessage ="Please Enter Season.")]
        public int? SeasonID { get; set; }
        public int? YearID { get; set; }
        public int? ProductCategoryID { get; set; }
        public int? ProductGroupID { get; set; }
        public int? ProductSourceID { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> ProdCat1ID { get; set; }
        public Nullable<int> ProdCat2ID { get; set; }
        public Nullable<int> ProdCat3ID { get; set; }
        public Nullable<int> ProdCat4ID { get; set; }
        [Required(ErrorMessage = "Please Enter Supplier Style")]
        public string SupplierStyle { get; set; }
        [Required(ErrorMessage = "Please Enter Supplier")]
        public int? SupplierID { get; set; }
        [Required(ErrorMessage ="Please Enter Buyer.")]
        public int? BuyerID { get; set; }
        public string CostPriceUSD { get; set; }
        public string CostPrice { get; set; }
		public string ExcelFile { get; set; }
		[Required(ErrorMessage = "Please Select AllowZero.")]
		public Nullable<bool> IsAllowZero { get; set; }
        public Nullable<bool> IsActive { get; set;}
        public int? DefaultTemplateID { get; set; }
        public Nullable<int> MarkDownTemplateID { get; set; }
        public string IsFreeGift { get; set; }
        public Nullable<bool> IsConsignment { get; set; }
        public Nullable<bool> IsDiscontinue { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string CreatedOn { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string UpdatedOn { get; set; }
        [Required(ErrorMessage = "Please Enter Color")]
        public int? ColorID { get; set; }
        public string PrimaryImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public int? Start { get; set; }
        public int? End { get; set; }
        public string AutocompleteSupplierCode { get; set; }
        public string AutocompleteProductSKU { get; set; }
        [Required(ErrorMessage ="Please Enter Available Sizes.")]
        public string AvailableSize { get; set; }
        public IEnumerable<SizeGridModel> Sizes { get; set; }
        public string[] SelectedSizes { get; set; }
        public virtual BuyerModel Buyer { get; set; }
        public virtual ColorModel Color { get; set; }
        public virtual ICollection<ItemsDetails> PurchaseOrderItems { get; set; }
        public virtual ProductCategoryModel ProductCategory { get; set; }
        public virtual ProductGrpModel ProductGroup { get; set; }
        public virtual ProductSourceModel ProductSource { get; set; }
        public virtual ProductCat1Model ProductCat1 { get; set; }
        public virtual ProductCat2Model ProductCat2 { get; set; }
        public virtual ProductCat3Model ProductCat3 { get; set; }
        public virtual ProductCat4Model ProductCat4 { get; set; }
        public virtual SeasonModel Season { get; set; }
        public virtual SizeGridModel SizeGrid { get; set; }
        public virtual SupplierModel Supplier { get; set; }
        public virtual TemplateModel Template { get; set; }
        public virtual YearModel Year { get; set; }
        public virtual ICollection<SearchData> SearchDatas { get; set; }
        public virtual ICollection<ProductSizeModel> ProductSizes { get; set; }
        public virtual StockDistributionModel StockDistributionModel { get; set; }
        
        public virtual ICollection<StockInventoryModel> StockInventories { get; set; }
        //public ProductModel()
        //{
        //    AvailSize = new List<Size>();
        //}
        public ProductModel()
        {
            AddItemModelList = new List<AddItemModel>();
        }
		public Nullable<decimal> OriginalSellingPrice { get; set; }
		public Nullable<decimal> NewCashPrice { get; set; }
		public string Barcode { get; set; }
        public virtual CartonManagementDetailModel CartonManagementDetailModel { get; set; }
        public string autoCompleteSizeGrid { get; set; }
        public string AutoCompleteColorCode { get; set; }
        public List<AddItemModel> AddItemModelList { get; set; }
       
        
    }
}
