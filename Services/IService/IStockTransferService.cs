using Model.StockDistribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
	public interface IStockTransferService
	{
		bool InsertSuggestions(List<StockTransferDetail> records);
		List<StockTransferDetail> GetScheduledTransfers();
		bool UpdateStockTransfer(List<StockTransferDetail> data);

	}
}
