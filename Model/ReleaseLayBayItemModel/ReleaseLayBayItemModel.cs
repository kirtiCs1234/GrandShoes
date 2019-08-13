using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReleaseLayBayItemModel
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<decimal> ProductSize { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> LayBayId { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> SalesOrderId { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public virtual LayBaySaleModel LayBaySale { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual SalesOrderModel SalesOrder { get; set; }
    }
}
