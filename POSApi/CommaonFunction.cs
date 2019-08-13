using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
	public class CommaonFunction
	{
		private static GrandShoesEntities db = new GrandShoesEntities();
		public static string OrderNo()
		{
			try
			{
				var lastNo = "";

				var Data = db.PurchaseOrders.Where(x => x.IsActive == true).ToList();
				if (Data.Count == 0)
				{
					lastNo = "0000000001";
				}
				else
				{
					lastNo = Data.OrderByDescending(c => c.OrderDate).FirstOrDefault().OrderNumber;
					var NewNumber = Convert.ToInt32(lastNo.Remove(0, 1)) + 1;
					var length = NewNumber.ToString().Length;
					var oldval = lastNo.Substring(lastNo.Length - length);
					lastNo = lastNo.Replace(oldval, NewNumber.ToString());

					//Checking for maxx limit 999999
					if (lastNo.Remove(1).ToUpper() != "Q")
					{
						lastNo = "Q" + lastNo;
					}
				}
				return lastNo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public static string IBtNo()
		{
			try
			{
				var lastNo = "";

				var Data = db.CartonManagements.Where(x => x.IsActive == true).ToList();
				if (Data.Count == 0)
				{
					lastNo = "0000000001";
				}
				else
				{
					lastNo = Data.OrderByDescending(c => c.PackDate).FirstOrDefault().IBTNumber;
					var NewNumber = Convert.ToInt32(lastNo.Remove(0, 1)) + 1;
					var length = NewNumber.ToString().Length;
					var oldval = lastNo.Substring(lastNo.Length - length);
					lastNo = lastNo.Replace(oldval, NewNumber.ToString());

					//Checking for maxx limit 999999
					if (lastNo.Remove(1).ToUpper() != "B")
					{
						lastNo = "B" + lastNo;
					}
				}
				return lastNo;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}