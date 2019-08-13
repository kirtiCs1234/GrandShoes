using Helper;
using Model;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
	public class PendingItemReceiptService: IPendingItemReceiptService
	{
		public bool Create(PendingItemReceiptModel model)
		{
			bool create = ServerResponse.Invoke<bool>("api/pendingReceipt/create", JsonConvert.SerializeObject(model), "POST");
			return create;
		}
	}
}
