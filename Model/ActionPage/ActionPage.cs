using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class ActionPageModel
	{
		public int Id { get; set; }
		public Nullable<int> ActionNameId { get; set; }
		
		public string ActionPages { get; set; }
		public Nullable<bool> IsActive { get; set; }
		public Nullable<int> RoleId { get; set; }
		public virtual ICollection<ActionPageModel> ActionPage1 { get; set; }
		public virtual ActionPageModel ActionPage2 { get; set; }
	}
}
