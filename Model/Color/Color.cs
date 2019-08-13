﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
   public partial class ColorModel
    {
        public int Id { get; set; }
        [Required]
        [Remote("CheckExistingColor", "Color", "Admin", ErrorMessage = "There is an color Code that already uses.", AdditionalFields = "Id")]
        [RegularExpression("\\d{3}", ErrorMessage = "Please Enter Three Digits Only.")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Color Long is required")]
        public string ColorLong { get; set; }
        [Required(ErrorMessage = "Color Short is required")]
        public string ColorShort { get; set; }
		public string ExcelFile { get; set; }
        public string FullColor { get { return Code + "#" + ColorLong; } }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<SearchData> SearchDatas { get; set; }
        public virtual ICollection<ProductModel> Products { get; set; }
        public virtual ICollection<StockInventoryModel> StockInventories { get; set; }
    }
}
