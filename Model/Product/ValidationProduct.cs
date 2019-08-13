using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ValidationProduct
    {
        public string SupplierStyle { get; set; }
        public int? ColorID { get; set; }
        public int? SupplierID { get; set; }
        public virtual SupplierModel supplier { get; set; }
        public virtual ColorModel color { get; set; }
    }
}
