
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductImport
    {
        public string SizeGridNo { get; set; }
        public string Season { get; set; }
        public string Year { get; set; }
        public string ProductSource { get; set; }
        public string Supplier { get; set; }
        public string Buyer { get; set; }
        public string DefaultTemplate { get; set; }
        public string MarkdownTemplate { get; set; }
        public string ProductCat1 { get; set; }
        public string ProductCat2 { get; set; }
        public string ProductCat3 { get; set; }
        public string ProductCat4 { get; set; }
        public string Color { get; set; }

        public int? SizeGridId { get; set; }
        public int? SeasonId { get; set; }
        public int? YearId { get; set; }
        public int? ProductSourceId { get; set; }
        public int? SupplierId { get; set; }
        public int? BuyerId { get; set; }
        public int? DefaultTemplateId { get; set; }
        public int? MarkdownTemplateId { get; set; }
        public int? ProductCat1ID { get; set; }
        public int? ProductCat2ID { get; set; }
        public int? ProductCat3ID { get; set; }
        public int? ProductCat4ID { get; set; }
        public int? ColorID { get; set; }
    }
}
