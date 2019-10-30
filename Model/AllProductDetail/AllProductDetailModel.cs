﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AllProductDetailModel
    {
        public int Id { get; set; }
        public string ProductSKU { get; set; }
        public string StyleSKU { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
       
        public int? SizeGridID { get; set; }
        public int? ColorID { get; set; }
        public int? ProductCategoryID { get; set; }
        public int? ProductGroupID { get; set; }
        public int? ProductSourceID { get; set; }
		public int? DefaultTemplateId { get; set; }

		public string Barcode { get; set; }
        public string PrimaryImage { get; set; }
        
        public virtual ColorModel Color { get; set; }
        
    }
}