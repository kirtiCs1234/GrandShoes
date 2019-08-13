using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class PageNameModel
	{
        public int Id { get; set; }
        public string Page { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAdminPage { get; set; }
        public virtual ICollection<ActionExistenceModel> ActionExistences { get; set; }
        public virtual ICollection<PagePermissionModel> PagePermissions { get; set; }
    }
    public class PageListModel
    {
        public int Id { get; set; }
        public List<string> Area { get; set; }
        public List<string> BranchCartonDispach { get; set; }

        public List<string> Branch { get; set; }

        public List<string> BranchStockReport { get; set; }

        public List<string> Buyer { get; set; }

        public List<string> Color { get; set; }

        public List<string> Discount { get; set; }

        public List<string> IBTCarton { get; set; }

        public List<string> IBTCartonReport { get; set; }

        public List<string> MarkDownBranch { get; set; }

        public List<string> Offers { get; set; }

        public List<string> ProductCategory { get; set; }

        public List<string> Product { get; set; }

        public List<string> ProductSource { get; set; }

        public List<string> ProductStyle { get; set; }

        public List<string> PurchaseOrder { get; set; }

        public List<string> PurchaseOrderReport { get; set; }

        public List<string> Receipt { get; set; }

        public List<string> Season { get; set; }

        public List<string> SizeGrid { get; set; }

        public List<string> SMIBranchDefault { get; set; }

        //public List<string> Log { get; set; }
        public List<string> StaffMember { get; set; }

        public List<string> StockAudit { get; set; }

        public List<string> StockTaKe { get; set; }

        public List<string> Supplier { get; set; }

        public List<string> TreeView { get; set; }

        public List<string> StoreDelieveryReport { get; set; }

        public List<string> User { get; set; }

        public List<string> Role { get; set; }

        public List<string> Template { get; set; }

        public List<string> StockEnquiry { get; set; }

        public List<string> StockDistribution { get; set; }

        public int? roleId { get; set; }
        public bool IsAdminPage { get; set; }
        public bool IsActive { get; set; }
        public List<string> Report { get; set; }
        public List<string> CustomerPage { get; set; }
        public List<string> Log { get; set; }
        public List<string> CartonManagement { get; set; }
        public List<string> CartonManagementReport { get; set; }
    }
}
