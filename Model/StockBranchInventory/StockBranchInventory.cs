using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class StockBranchInventoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> Quantity01 { get; set; }
        public Nullable<int> Quantity02 { get; set; }
        public Nullable<int> Quantity03 { get; set; }
        public Nullable<int> Quantity04 { get; set; }
        public Nullable<int> Quantity05 { get; set; }
        public Nullable<int> Quantity06 { get; set; }
        public Nullable<int> Quantity07 { get; set; }
        public Nullable<int> Quantity08 { get; set; }
        public Nullable<int> Quantity09 { get; set; }
        public Nullable<int> Quantity10 { get; set; }
        public Nullable<int> Quantity11 { get; set; }
        public Nullable<int> Quantity12 { get; set; }
        public Nullable<int> Quantity13 { get; set; }
        public Nullable<int> Quantity14 { get; set; }
        public Nullable<int> Quantity15 { get; set; }
        public Nullable<int> Quantity16 { get; set; }
        public Nullable<int> Quantity17 { get; set; }
        public Nullable<int> Quantity18 { get; set; }
        public Nullable<int> Quantity19 { get; set; }
        public Nullable<int> Quantity20 { get; set; }
        public Nullable<int> Quantity21 { get; set; }
        public Nullable<int> Quantity22 { get; set; }
        public Nullable<int> Quantity23 { get; set; }
        public Nullable<int> Quantity24 { get; set; }
        public Nullable<int> Quantity25 { get; set; }
        public Nullable<int> Quantity26 { get; set; }
        public Nullable<int> Quantity27 { get; set; }
        public Nullable<int> Quantity28 { get; set; }
        public Nullable<int> Quantity29 { get; set; }
        public Nullable<int> Quantity30 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LogId { get; set; }
        public int? Quantity { get; set; }
        public string LastSale { get; set; }
        public string UpdateTime { get; set; }
        public virtual BranchModel Branch { get; set; }
        public virtual ProductModel Product { get; set; }
        private int _ItemCount = 0;
        public int ItemCount
        {
            get
            {
                _ItemCount =
                    Convert.ToInt32((Quantity01 ?? 0) + (Quantity02 ?? 0) + (Quantity03 ?? 0) + (Quantity04 ?? 0) + (Quantity05 ?? 0) + (Quantity06 ?? 0) + (Quantity07 ?? 0) + (Quantity08 ?? 0) + (Quantity09 ?? 0) + (Quantity10 ?? 0) +
                    (Quantity11 ?? 0) + (Quantity12 ?? 0) + (Quantity13 ?? 0) + (Quantity14 ?? 0) + (Quantity15 ?? 0) + (Quantity16 ?? 0) + (Quantity17 ?? 0) + (Quantity18 ?? 0) + (Quantity19 ?? 0) + (Quantity20 ?? 0) +
                    (Quantity21 ?? 0) + (Quantity22 ?? 0) + (Quantity23 ?? 0) + (Quantity24 ?? 0) + (Quantity25 ?? 0) + (Quantity26 ?? 0) + (Quantity27 ?? 0) + (Quantity28 ?? 0) + (Quantity29 ?? 0) + (Quantity30 ?? 0)
                    );

                return _ItemCount;
            }
        }
    }
}
