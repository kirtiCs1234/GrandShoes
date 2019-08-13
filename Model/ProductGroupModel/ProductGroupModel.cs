﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductGrpModel
	{
        public int Id { get; set; }
        public string GroupName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
