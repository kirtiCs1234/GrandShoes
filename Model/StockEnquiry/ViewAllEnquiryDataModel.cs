﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ViewAllEnquiryDataModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? BranchId { get; set; }
        public int? ColorID { get; set; }
        public int? ProductStyleId { get; set; }
        public int StockDistributionSummaryId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? DistributionDate { get; set; }
        public int StockDistributionStatusId { get; set; }
        public string Name { get; set; }
        public int? Quantity01 { get; set; }
        public int? Quantity02 { get; set; }
        public int? Quantity03 { get; set; }
        public int? Quantity04 { get; set; }
        public int? Quantity05 { get; set; }
        public int? Quantity06 { get; set; }
        public int? Quantity07 { get; set; }
        public int? Quantity08 { get; set; }
        public int? Quantity09 { get; set; }
        public int? Quantity10 { get; set; }
        public int? Quantity11 { get; set; }
        public int? Quantity12 { get; set; }
        public int? Quantity13 { get; set; }
        public int? Quantity14 { get; set; }
        public int? Quantity15 { get; set; }
        public int? Quantity16 { get; set; }
        public int? Quantity17 { get; set; }
        public int? Quantity18 { get; set; }
        public int? Quantity19 { get; set; }
        public int? Quantity20 { get; set; }
        public int? Quantity21 { get; set; }
        public int? Quantity22 { get; set; }
        public int? Quantity23 { get; set; }
        public int? Quantity24 { get; set; }
        public int? Quantity25 { get; set; }
        public int? Quantity26 { get; set; }
        public int? Quantity27 { get; set; }
        public int? Quantity28 { get; set; }
        public int? Quantity29 { get; set; }
        public int? Quantity30 { get; set; }
        public bool IsActive { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual ColorModel Color { get; set; }
        public virtual ProductStyleModel ProductStyle { get; set; }

    }
}