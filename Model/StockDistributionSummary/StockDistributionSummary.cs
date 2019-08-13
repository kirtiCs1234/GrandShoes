﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class StockDistributionSummaryModel
    {
        public int Id { get; set; }
        public int? DistributionNumber { get; set; }
        public Nullable<System.DateTime> DateOpen { get; set; }
        public Nullable<System.DateTime> DateClose { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<StockDistributionModel> StockDistributions { get; set; }
        public List<AddItemModel> AddItemList { get; set; }
        public StockDistributionSummaryModel()
        {
            AddItemList = new List<AddItemModel>();
        }
    }
}
