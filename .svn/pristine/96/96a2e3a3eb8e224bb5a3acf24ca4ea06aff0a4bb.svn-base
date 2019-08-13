using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public partial class BranchModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Branch Code")]
        [Remote("CheckBranchCode","Branch","Admin",ErrorMessage="This code is already uses",AdditionalFields ="Id")]
        public string BranchCode { get; set; }
        [Required(ErrorMessage = "Please Enter Branch Code")]
        [Remote("CheckBranchName", "Branch", "Admin", ErrorMessage = "This code is already uses", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter Telephone number")]
        
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid telephone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter valid telephone number.")]

        public string Telephone { get; set; }
        
        public Nullable<int> ManagerId { get; set; }
        public string DateOpen { get; set; }
        public string DateOpen1 { get; set; }
        public string DateClosed1 { get; set; }
        public string DateClosed { get; set; }
        [Required(ErrorMessage = "Please Enter Address Line1")]
        public string AddressLine1 { get; set; }
        [Required(ErrorMessage = "Please Enter Address Line2")]
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please Enter Address Line3")]
        public string AddressLine3 { get; set; }
        [Required(ErrorMessage = "Please Enter Postal Code")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Please Enter Area Code")]
        public string AreaCode { get; set; }
        public Nullable<bool> IsSendStock { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public Nullable<bool> IsHeadOffice { get; set; }
        public string StoreSize { get; set; }
        public Nullable<int> LogId { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public virtual LogModel Log { get; set; }
        public virtual StockDistributionModel StockDistributionModel { get; set; }
        public virtual UserModel User { get; set; }
        public List<SMIBranchDefaultModel> BranchDestination { get; set; }
        public List<AddItemModel> AddItemModelList { get; set; }
        public BranchModel()
        {
            AddItemModelList = new List<AddItemModel>();
            BranchDestination = new List<SMIBranchDefaultModel>();
        }
       
    }
}
