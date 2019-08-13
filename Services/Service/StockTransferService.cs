using Helper;
using Model.StockDistribution;
using Newtonsoft.Json;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
	public class StockTransferService: IStockTransferService	
	{
		public bool InsertSuggestions(List<StockTransferDetail> records)
		{
			return ServerResponse.Invoke<bool>("api/StockTransfer/create", JsonConvert.SerializeObject(records), "POST");
		}


		public List<StockTransferDetail> GetScheduledTransfers()
		{
			return ServerResponse.Invoke<List<StockTransferDetail>>("api/StockTransfer/GetScheduledTransfers", "", "GET");
		}

		public bool UpdateStockTransfer(List<StockTransferDetail> stock)
		{
			return ServerResponse.Invoke<bool>("api/StockTransfer/UpdateStockTransfer", JsonConvert.SerializeObject(stock), "POST");
		}
	}
}
