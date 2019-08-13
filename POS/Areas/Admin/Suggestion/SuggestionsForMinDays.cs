using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace POS.Areas.Admin.Suggestion
{
	public class SuggestionsForMinDays
	{
		private double totalStock;
		private double totalAvlStock;
		private static DateTime fromDate;
		private static DateTime toDate;
		public SuggestionsForMinDays()
		{
			totalStock = 0;
			totalAvlStock = 0;
		}

		public static wareHouse GetSuggestion(DateTime Date, int productId, string connectionString)
		{
			toDate = Date;
			fromDate = toDate.AddDays(-30);
			DataTable data = getDataFromServer(fromDate, toDate, productId, connectionString);
			wareHouse w = new wareHouse();
			if (data.Rows.Count == 0)
			{
				w.Response = "404";
				return w;
			}
			else
			{
				SuggestionsForMinDays sh = new SuggestionsForMinDays();
				var dataList = Helper.ConvertToList<Suggestion>(data);
                w.Data = dataList.GroupBy(c => new { c.ProductID, c.BranchID }).Select(b => new Suggestion
                {
                    BranchName = b.FirstOrDefault().BranchName,
                    BranchID = b.FirstOrDefault().BranchID,
                    ProductID = b.FirstOrDefault().ProductID,
                    SoldTotal = b.FirstOrDefault().SoldTotal,
                    ReceivedDate = b.FirstOrDefault().ReceivedDate,
                    RemainingTotal = b.Sum(c => c.RemainingTotal),
                }).ToList();
                w.Data = sh.getSaleRate(w.Data);
				w = sh.getSuggestion(w);
				w = stockToTransfer(w);
				DataSet ds = new DataSet();
				w.MaxStockSaleDays = Math.Round((w.Data.OrderByDescending(c => c.MaxDaysToStockSold).FirstOrDefault().MaxDaysToStockSold));
				w.Response = "200";
				return w;
			}

			//return Helper.ToDataTable(w.Data.Select(c => new Suggestion
			//{
			//	FromDate = fromDate,
			//	ToDate = toDate,
			//	BranchID = c.BranchID,
			//	ProductID = c.ProductID,
			//	SoldTotal = c.SoldTotal,
			//	RemainingTotal = c.RemainingTotal,
			//	ReceivedDate = c.ReceivedDate,
			//	DaysMinStockSold = w.TotalStockSaleDays,
			//	SaleRate = c.SaleRate,
			//	SalePerDay = c.SalePerDay,
			//	AvgSalePerMonth = c.AvgSalePerMonth,
			//	StockToTransfer = c.StockToTransfer,
			//	EffectiveDays = c.EffectiveDays
			//}).OrderByDescending(c => c.SaleRate).ToList());
		}


		//Get Data from sql server
		private static DataTable getDataFromServer(DateTime fromDate, DateTime toDate, int productId, string connectionString)
		{
			var connection = ConfigurationManager.ConnectionStrings["MovieDBContext"].ConnectionString;
			DataTable dt = new DataTable();
			using (var conn = new SqlConnection(connection))
			{
				using (SqlCommand cmd = new SqlCommand("SPGet_Data_For_Suggestions", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("dateFrom", fromDate);
					cmd.Parameters.AddWithValue("dateTo", toDate);
					cmd.Parameters.AddWithValue("productId", productId);
					conn.Open();
					dt.Load(cmd.ExecuteReader());
				}
			}

			return dt;

			//temporary data access
			//return Helper.ToDataTable(tempDB.ReadExcelFile().Select(c => new Suggestion
			//{
			//	FromDate = fromDate,
			//	ToDate = toDate,
			//	BranchID = c.BranchID,
			//	ProductID = c.ProductID,
			//	SoldTotal = c.SoldTotal,
			//	RemainingTotal = c.RemainingTotal,
			//	ReceivedDate = c.ReceivedDate
			//}).ToList());
		}




		//----------------------calculation of stock to transfer(receiving/sending)
		static wareHouse stockToTransfer(wareHouse result)
		{
			foreach (var item in result.Data)
				item.StockToTransfer = (item.AvgSalePerMonth - (item.RemainingTotal));
			return result;
		}

		private wareHouse getSuggestion(wareHouse warehouse)
		{
			warehouse.AvgSalePerDay = warehouse.Data.Sum(c => c.SalePerDay);
			warehouse.TotalStockSaleDays = Math.Round((totalAvlStock) / warehouse.AvgSalePerDay);
			warehouse = getAvgDays(warehouse);
			if (warehouse.Data.Sum(c => c.AvgSalePerMonth) > totalAvlStock)
			{
				warehouse.TotalStockSaleDays--;
				warehouse = getAvgDays(warehouse);
				warehouse = checkIfStockSold(warehouse);
			}
			else
				warehouse = checkIfStockSold(warehouse);
			return warehouse;
		}


		//----------------------check untill all the items distributed
		private wareHouse checkIfStockSold(wareHouse wh)
		{
			var avlStock = totalAvlStock - wh.Data.Sum(c => c.AvgSalePerMonth);
			if (avlStock > 0)
				wh = AddToStock(wh);
			else if (avlStock < 0)
				wh = removeFromStock(wh);
			var soldStock = wh.Data.Sum(c => c.AvgSalePerMonth);
			if (!(totalAvlStock == soldStock))
				wh = checkIfStockSold(wh);
			return wh;
		}


		//----------------------remove items if greater then available stock
		private wareHouse removeFromStock(wareHouse wh)
		{
			var stockToReduce = wh.Data.Sum(c => c.AvgSalePerMonth) - totalAvlStock;
			var lowStockRate = wh.Data.OrderBy(c => c.SaleRate).FirstOrDefault();
			lowStockRate.AvgSalePerMonth -= stockToReduce;
			wh.Data.Remove(wh.Data.OrderBy(c => c.SaleRate).FirstOrDefault());
			wh.Data.Add(lowStockRate);
			return wh;
		}


		//----------------------adding divide and add on distribution
		private wareHouse AddToStock(wareHouse wh)
		{
			var avlStock = totalAvlStock - wh.Data.Sum(c => c.AvgSalePerMonth);
			var individual = Math.Round(avlStock / wh.Data.Count);
			if (individual == 0 && avlStock != 0)
			{
				foreach (var item in wh.Data.OrderByDescending(c => c.SoldTotal))
				{
					item.AvgSalePerMonth += avlStock;
					break;
				}
			}
			foreach (var item in wh.Data)
				item.AvgSalePerMonth += individual;
			return wh;
		}


		//----------------------get avg days
		private wareHouse getAvgDays(wareHouse wh)
		{
			foreach (var item in wh.Data)
				item.AvgSalePerMonth = wh.TotalStockSaleDays * item.SalePerDay;
			return wh;
		}


		//----------------------Get sale Rate
		private List<Suggestion> getSaleRate(List<Suggestion> data)
		{
			totalStock = data.Sum(c => c.SoldTotal);
			totalAvlStock = data.Sum(c => c.RemainingTotal);
			foreach (var item in data)
			{
				if (fromDate < item.ReceivedDate)
				{
					item.EffectiveDays = Convert.ToInt32(toDate.Subtract(item.ReceivedDate).Days);
					item.SaleRate = Convert.ToDouble(((item.SoldTotal * 100) / totalStock).ToString("#.0000"));
					item.SalePerDay = Math.Round(item.SoldTotal / item.EffectiveDays);
					item.MaxDaysToStockSold = item.RemainingTotal / item.SalePerDay;
				}
				else
				{
					item.EffectiveDays = Convert.ToInt32(toDate.Subtract(fromDate).Days);
					item.SaleRate = Convert.ToDouble(((item.SoldTotal * 100) / totalStock).ToString("#.0000"));
					item.SalePerDay = Math.Round(item.SoldTotal / item.EffectiveDays);
					item.MaxDaysToStockSold = item.RemainingTotal / item.SalePerDay;
				}
			}
			return data;
		}


	}

}