using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductSize { get; set; }
        public int? CashSaleCount { get; set; }
        public int? LayBuySaleCount { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> SalesOrderId { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> TotalPriceAll { get; set; }
        public int? IBTStock { get; set; }
        public Nullable<decimal> Sold { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsMarkDown { get; set; }
        public int MarkDownCount { get; set; }
        public Nullable<int> RemainingQuantity { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual SalesOrderModel SalesOrder { get; set; }
    }
}
