using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Report
{
    public class TransactionEnquiryModel
    {
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public int ProductId { get; set; }
        public string ProductSKU { get; set; }
        public string StyleSKU { get; set; }
        public int ColorId { get; set; }
        public string Color { get; set; }
        public string Date { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }
}
