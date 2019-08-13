using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EditReceiptOrderModel
    {
        public EditReceiptOrderModel()
        {
            ReceiptOrderItems = new List<ReceiptOrderItemModel>();
            ReceiptOrder = new ReceiveOrderModel();
        }
        public ReceiveOrderModel ReceiptOrder { get; set; }
        public List<ReceiptOrderItemModel> ReceiptOrderItems { get; set; }
    }
}
