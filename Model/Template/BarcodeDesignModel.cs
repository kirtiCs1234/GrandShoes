using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BarcodeDesignModel
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Unit { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public string Price { get; set; }
        public string PriceAfterDiscount { get; set; }
        public string CompanyLogo { get; set; }
        public bool? IsActive { get; set; }
    }
}
