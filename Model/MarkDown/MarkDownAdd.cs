using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MarkDownAddModel
    {
        public MarkDownModel MarkDown { get; set; }
        public List<MarkDownBranchModel> MarkDownBranchList { get; set; }
        public List<BranchModel> BranchList1 { get; set; }
        public List<MarkDownModel> MarkDownList { get; set; }
        public MarkDownAddModel()
        {
            MarkDownBranchList = new List<MarkDownBranchModel>();
            BranchList1 = new List<BranchModel>();
            MarkDownList = new List<MarkDownModel>();
        }
    public string[] BranchList { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public string LastMarkDownDate { get; set; }
		public string Count { get; set; }
    public string AutocompleteProductSKU { get; set; }
        public string AutocompleteProductStyleSKU { get; set; }
        public bool IsActive { get; set; }
      
    }
}
