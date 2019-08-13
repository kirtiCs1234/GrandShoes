﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class ProductSizeModel
    {
        public int Id { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductSKU { get; set; }
        public string Size { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual ProductModel Product { get; set; }
    }
}
