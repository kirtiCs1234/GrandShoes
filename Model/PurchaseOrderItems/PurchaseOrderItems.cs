﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class PurchaseOrderItemModel
    {
        public int ID { get; set; }
        public int PurchaseOrderId { get; set; }
        [Remote("CheckProduct", "PurchaseOrder", "Admin", ErrorMessage = "This product is already uses", AdditionalFields = "autoCompleteProductName")]
        public int? ProductId { get; set; }
        public int? autoCompleteProductId { get; set; }
        public string ProductSKU { get; set; }
		public string StyleSKU { get; set; }
        public Nullable<int> SizeGridId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public string SuplierStyle { get; set; }
        public decimal? Amount { get; set; }
        public string ItemSize1 { get; set; }
        public string ItemSize2 { get; set; }
        public string ItemSize3 { get; set; }
        public string ItemSize4 { get; set; }
        public string ItemSize5 { get; set; }
        public string ItemSize6 { get; set; }
        public string ItemSize7 { get; set; }
        public string ItemSize8 { get; set; }
        public string ItemSize9 { get; set; }
        public string ItemSize10 { get; set; }
        public string ItemSize11 { get; set; }
        public string ItemSize12 { get; set; }
        public string ItemSize13 { get; set; }
        public string ItemSize14 { get; set; }
        public string ItemSize15 { get; set; }
        public string ItemSize16 { get; set; }
        public string ItemSize17 { get; set; }
        public string ItemSize18 { get; set; }
        public string ItemSize19 { get; set; }
        public string ItemSize20 { get; set; }
        public string ItemSize21 { get; set; }
        public string ItemSize22 { get; set; }
        public string ItemSize23 { get; set; }
        public string ItemSize24 { get; set; }
        public string ItemSize25 { get; set; }
        public string ItemSize26 { get; set; }
        public string ItemSize27 { get; set; }
        public string ItemSize28 { get; set; }
        public string ItemSize29 { get; set; }
        public string ItemSize30 { get; set; }
        public Nullable<int> QuantitySize1 { get; set; }
        public Nullable<int> QuantitySize2 { get; set; }
        public Nullable<int> QuantitySize3 { get; set; }
        public Nullable<int> QuantitySize4 { get; set; }
        public Nullable<int> QuantitySize5 { get; set; }
        public Nullable<int> QuantitySize6 { get; set; }
        public Nullable<int> QuantitySize7 { get; set; }
        public Nullable<int> QuantitySize8 { get; set; }
        public Nullable<int> QuantitySize9 { get; set; }
        public Nullable<int> QuantitySize10 { get; set; }
        public Nullable<int> QuantitySize11 { get; set; }
        public Nullable<int> QuantitySize12 { get; set; }
        public Nullable<int> QuantitySize13 { get; set; }
        public Nullable<int> QuantitySize14 { get; set; }
        public Nullable<int> QuantitySize15 { get; set; }
        public Nullable<int> QuantitySize16 { get; set; }
        public Nullable<int> QuantitySize17 { get; set; }
        public Nullable<int> QuantitySize18 { get; set; }
        public Nullable<int> QuantitySize19 { get; set; }
        public Nullable<int> QuantitySize20 { get; set; }
        public Nullable<int> QuantitySize21 { get; set; }
        public Nullable<int> QuantitySize22 { get; set; }
        public Nullable<int> QuantitySize23 { get; set; }
        public Nullable<int> QuantitySize24 { get; set; }
        public Nullable<int> QuantitySize25 { get; set; }
        public Nullable<int> QuantitySize26 { get; set; }
        public Nullable<int> QuantitySize27 { get; set; }
        public Nullable<int> QuantitySize28 { get; set; }
        public Nullable<int> QuantitySize29 { get; set; }
        public Nullable<int> QuantitySize30 { get; set; }
        public Nullable<decimal> CostSize1 { get; set; }
        public Nullable<decimal> CostSize2 { get; set; }
        public Nullable<decimal> CostSize3 { get; set; }
        public Nullable<decimal> CostSize4 { get; set; }
        public Nullable<decimal> CostSize5 { get; set; }
        public Nullable<decimal> CostSize6 { get; set; }
        public Nullable<decimal> CostSize7 { get; set; }
        public Nullable<decimal> CostSize8 { get; set; }
        public Nullable<decimal> CostSize9 { get; set; }
        public Nullable<decimal> CostSize10 { get; set; }
        public Nullable<decimal> CostSize11 { get; set; }
        public Nullable<decimal> CostSize12 { get; set; }
        public Nullable<decimal> CostSize13 { get; set; }
        public Nullable<decimal> CostSize14 { get; set; }
        public Nullable<decimal> CostSize15 { get; set; }
        public Nullable<decimal> CostSize16 { get; set; }
        public Nullable<decimal> CostSize17 { get; set; }
        public Nullable<decimal> CostSize18 { get; set; }
        public Nullable<decimal> CostSize19 { get; set; }
        public Nullable<decimal> CostSize20 { get; set; }
        public Nullable<decimal> CostSize21 { get; set; }
        public Nullable<decimal> CostSize22 { get; set; }
        public Nullable<decimal> CostSize23 { get; set; }
        public Nullable<decimal> CostSize24 { get; set; }
        public Nullable<decimal> CostSize25 { get; set; }
        public Nullable<decimal> CostSize26 { get; set; }
        public Nullable<decimal> CostSize27 { get; set; }
        public Nullable<decimal> CostSize28 { get; set; }
        public Nullable<decimal> CostSize29 { get; set; }
        public Nullable<decimal> CostSize30 { get; set; }
		public Nullable<decimal> TotalCost { get; set; }
		public Nullable<int> TotalQuantity { get; set; }
		public bool IsActive { get; set; }
        public string OrderItemDate{ get; set; }
        [Required(ErrorMessage ="Enter Product style")]
        [Remote("CheckProduct", "PurchaseOrder", "Admin", ErrorMessage = "This product is already uses.", AdditionalFields = "autoCompleteProductName")]
        public string autoCompleteProductStyleName { get; set; }
        [Required(ErrorMessage = "Enter Product name")]
        [Remote("CheckProduct", "PurchaseOrder", "Admin", ErrorMessage = "This product is already uses.", AdditionalFields = "autoCompleteProductName")]
        public string autoCompleteProductName { get; set; }
        [Required(ErrorMessage = "Enter Grid size")]
        public string autoCompleteGridName { get; set; }
        [Required(ErrorMessage = "Choose Color")]
        public string autoCompleteColorName { get; set; }
        public virtual ColorModel Color { get; set; }
        public virtual LogModel Log { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual ProductStyleModel ProductStyle { get; set; }
        public virtual PurchaseOrderModel PurchaseOrder { get; set; }
       
        public virtual SizeGridModel SizeGrid { get; set; }
		private int _ItemCost = 0;
		public int ItemCost
		{
			get
			{
				_ItemCost = Convert.ToInt32((QuantitySize1 ?? 0) * (CostSize1 ?? 0) + (QuantitySize2 ?? 0) * (CostSize2 ?? 0) + (QuantitySize3 ?? 0) * (CostSize3 ?? 0) + (QuantitySize4 ?? 0) * (CostSize4 ?? 0) + (QuantitySize5 ?? 0) * (CostSize5 ?? 0) + (QuantitySize6 ?? 0) * (CostSize6 ?? 0) + (QuantitySize7 ?? 0) * (CostSize8 ?? 0) + (QuantitySize9 ?? 0) * (CostSize9 ?? 0) + (QuantitySize10 ?? 0) * (CostSize10 ?? 0) + (QuantitySize11 ?? 0) * (CostSize11 ?? 0) + (QuantitySize12 ?? 0) * (CostSize12 ?? 0) + (QuantitySize13 ?? 0) * (CostSize13 ?? 0) + (QuantitySize14 ?? 0) * (CostSize14 ?? 0) + (QuantitySize15 ?? 0) * (CostSize15 ?? 0) + (QuantitySize16 ?? 0) * (CostSize16 ?? 0) + (QuantitySize17 ?? 0) * (CostSize17 ?? 0) + (QuantitySize18 ?? 0) * (CostSize18 ?? 0) + (QuantitySize19 ?? 0) * (CostSize19 ?? 0) + (QuantitySize20 ?? 0) * (CostSize20 ?? 0) + (QuantitySize21 ?? 0) * (CostSize21 ?? 0) + (QuantitySize22 ?? 0) * (CostSize22 ?? 0) + (QuantitySize23 ?? 0) * (CostSize23 ?? 0) + (QuantitySize24 ?? 0) * (CostSize24 ?? 0) + (QuantitySize25 ?? 0) * (CostSize25 ?? 0) + (QuantitySize26 ?? 0) * (CostSize26 ?? 0) + (QuantitySize27 ?? 0) * (CostSize27 ?? 0) + (QuantitySize28 ?? 0) * (CostSize28 ?? 0) + (QuantitySize29 ?? 0) * (CostSize29 ?? 0) + (QuantitySize30 ?? 0) * (CostSize30 ?? 0));
				return _ItemCost;
			}
		}
        private int _ItemCount = 0;
        public int ItemCount
        {
            get
            {
                _ItemCount =
                    Convert.ToInt32((QuantitySize1 ?? 0) + (QuantitySize2 ?? 0) + (QuantitySize3 ?? 0 ) + (QuantitySize4 ?? 0) + (QuantitySize5 ?? 0) + (QuantitySize6 ?? 0) + (QuantitySize7 ?? 0) + (QuantitySize8 ?? 0 )+( QuantitySize9 ?? 0 )+( QuantitySize10 ?? 0 )+
                    (QuantitySize11 ?? 0 )+( QuantitySize12 ?? 0 )+ (QuantitySize13 ?? 0 )+( QuantitySize14 ?? 0 )+( QuantitySize15 ?? 0 )+ (QuantitySize16 ?? 0 )+ (QuantitySize17 ?? 0 )+ (QuantitySize18 ?? 0 )+(QuantitySize19 ?? 0)+ (QuantitySize20 ?? 0) +
                    (QuantitySize21 ?? 0 )+( QuantitySize22 ?? 0 )+ (QuantitySize23 ?? 0 )+( QuantitySize24 ?? 0 )+( QuantitySize25 ?? 0 )+ (QuantitySize26 ?? 0 )+ (QuantitySize27 ?? 0 )+ (QuantitySize28 ?? 0 )+ (QuantitySize29 ?? 0)+ (QuantitySize30 ?? 0)
                    );

                return _ItemCount;
            }
        }
        
    }
}