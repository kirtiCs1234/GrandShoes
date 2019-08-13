using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class BranchStockStatusReport
    {
        public string Product { get; set; }
        public string Color { get; set; }
        public decimal CostPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CostValue { get; set; }
        public decimal CurrentSaleValue { get; set; }
        public int TotalQuantiy { get; set; }
        public string MarkdownTemplate { get; set; }

        public Dictionary<int, int> BranchId { get; set; }
        public int WarehoustQuantity { get; set; }
    }
    public class InputBranchStockStatusReportModel
    {
        public string BramchFrom { get; set; }           
        public string BramchTo { get; set; }          
        public string ProductSKUFrom { get; set; }          
        public string ProductSKUTo { get; set; }          
        public string StyleSKUFrom { get; set; }         
        public string StyleSKUTo { get; set; }         
        public string AllwithMarkdown { get; set; }           
        public string OnlyMarkdown { get; set; }            
        public string NoMarkdown { get; set; }           
    }
}
