﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SearchData
    {
        public int? Id { get; set; }
        public string ProductSKU { get; set; }
        public string StyleSKU { get; set; }
        public int? ColorID { get; set; }
        public int? BranchId { get; set; }
        public int? StockDistributionSummaryId { get; set; }
        public string Warehouse { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        public virtual ProductStyleModel ProductStyleModel { get; set; }
        public virtual ColorModel ColorModel { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual StockDistributionSummaryModel StockDistributionSummary { get; set; }
    }
}