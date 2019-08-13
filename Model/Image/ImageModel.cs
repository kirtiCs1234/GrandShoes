using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public partial class ImageModel
    {
        public int Id { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Image1 { get; set; }
        public Nullable<bool> IsPrimary { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
