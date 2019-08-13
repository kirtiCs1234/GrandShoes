﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public partial class SeasonModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Season Code")]
        [Remote("CheckSeasonCode","Season","Admin" ,ErrorMessage ="This code is already uses." ,AdditionalFields ="Id")]
        public string Code { get; set; }
        public string Description { get; set; }
		public string ExcelFile { get; set; }
		public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
