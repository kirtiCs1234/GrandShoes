using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class DictIBTModel
	{
		public int? ProductId { get; set; }
		public Dictionary<string, string> QuantitySize { get; set; }
		public Dictionary<string, string> ItemSize { get; set; }
		public ProductModel Product { get; set; }
	}
}
