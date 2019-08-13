using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class CartonManagementForEdit
    {
        public CartonManagementForEdit()
        {
            CartonManagementDetailList = new List<CartonManagementDetailModel>();
            StockDictributionList = new List<AddItemModel>();

    }
       public List<CartonManagementDetailModel> CartonManagementDetailList { get; set; }
        public List<AddItemModel> StockDictributionList { get; set; }
        public CartonManagementModel CartonManagement { get; set; }
    }
}
