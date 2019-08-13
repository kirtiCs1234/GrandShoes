using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class TemplateModel
    {
		public int Id { get; set; }
        [Required]
		public string Name { get; set; }
		public string TemplateHtml { get; set; }
        [Required]
		public Nullable<decimal> Width { get; set; }
		public Nullable<bool> IsActive { get; set; }
        [Required]
		public Nullable<decimal> Height { get; set; }
		public Nullable<int> paramId { get; set; }
		public Nullable<int> LengthId { get; set; }
		public virtual LengthMeasureModel LengthMeasure { get; set; }
		public virtual ICollection<ProductModel> Products { get; set; }
		public virtual SizeParameterModel SizeParameter { get; set; }
	}
	public class TemplateValue
	{
		public int Id { get; set; }
		public string Barcode { get; set; } = "0123456789";
		public string Name { get; set; } = "template";
		public string BranchName { get; set; } = "Technocodz";
		public string BranchEmail { get; set; } = "tech@technocodz.com";
		public string ProductName { get; set; } = "item";
		public string ProductId { get; set; } = "1";
		public string Unit { get; set; } = "unit";
		public string Quantity { get; set; } = "1";
		public string Discount { get; set; } = "20";
		public string Price { get; set; } = "500";
		public string PriceAfterDiscount { get; set; } = "400";
		public string BranchLogo { get; set; } = "";
		public bool? IsActive { get; set; } = true;
	}
	public class TemplateVariable
	{
		public string barcode { get; private set; } = "##barcode##";
		public string name { get; private set; } = "##name##";
		public string branchname { get; private set; } = "##companyname##";
		public string branchemail { get; private set; } = "##companyemail##";
		public string productname { get; private set; } = "##productname##";
		public string productid { get; private set; } = "##productid##";
		public string unit { get; private set; } = "##unit##";
		public string quantity { get; private set; } = "##quantity##";
		public string discount { get; private set; } = "##discount##";
		public string price { get; private set; } = "##price##";
		public string priceafterdiscount { get; private set; } = "##priceafterdiscount##";
		public string branchlogo { get; private set; } = "##companylogo##";
	}
}
