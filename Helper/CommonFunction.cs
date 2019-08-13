
using DAL;
using Model;
using Model.ForStockTransfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class CommonFunction
    {
        private static GrandShoesEntities db = new GrandShoesEntities();
		public static DataTable ToDataTables<T>(this List<T> items)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);
			//Get all the properties
			PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo prop in Props)
			{
				//Setting column names as Property names
				dataTable.Columns.Add(prop.Name);
			}
			foreach (T item in items)
			{
				var values = new object[Props.Length];
				for (int i = 0; i < Props.Length; i++)
				{
					//inserting property values to datatable rows
					values[i] = Props[i].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}
			//put a breakpoint here and check datatable
			return dataTable;
		}
		public static List<Change> getChangedRecords(this DataTable DtInput)
		{
			var indexing = 1;
			DataTable dtTest = new DataTable("Change");
			dtTest.Columns.Add("Fieldname", typeof(String));
			dtTest.Columns.Add("RowNo", typeof(String));
			dtTest.Columns.Add("FieldValue", typeof(String));
			var change = new List<Change>();
			foreach (DataRow item in DtInput.Rows)
			{
				int index = DtInput.Rows.IndexOf(item);
				int iCount = item.ItemArray.Count();

				//if (index != DtInput.Rows.Count - 1)
				if (index < DtInput.Rows.Count)
				{
					for (int i = 0; i < iCount; i++)
					{
						//                                         0
						//if (item[i].ToString() != DtInput.Rows[index + 1][i].ToString())
						try
						{
							if (item[i].ToString() != DtInput.Rows[indexing][i].ToString())
							{
								DataRow drTest = dtTest.NewRow();
								var ch = new Change();

								ch.Fieldname = DtInput.Columns[i].Caption;
								ch.RowNo = index;
								ch.FieldValue = item[i].ToString();
								change.Add(ch);

								drTest["Fieldname"] = DtInput.Columns[i].Caption;
								drTest["RowNo"] = index;
								drTest["FieldValue"] = item[i].ToString();

								dtTest.Rows.Add(drTest);
							}
						}
						catch (Exception ex) { }
					}
					indexing = 0;
				}
			}
			return change;
		}
		public static int LogMethods(Object obj, string action, int UserId)
        {
            var name = obj.GetType().Name;//"";
            var b = obj.GetType();
            var a = b.GenericTypeArguments;
            //var c = a.Count;
            var pages = db.Pages.Where(x => x.page1.ToLower().Equals(name.ToLower())).FirstOrDefault();
            Log log = new Log();
            if (action.Equals("Insert"))
            {
                //var act = db.ActionLogs.Where(x => x.ActionLogType.ToLower().Equals(action.ToLower())).FirstOrDefault();
                //log.ActionLogId = act.Id;
                log.UserId = UserId;
                //log.Comment = Comment;
                log.LogDate = DateTime.Now;
                log.IsActive = true;
                log.PageId = pages.Id;
                log = db.Logs.Add(log);
               // try
              //  {
                    db.SaveChanges();
              //  }
              //  catch (Exception ex) { }
                return log.Id;
            }
            if (action.Equals("Update"))
            {
                //var act = db.ActionLogs.Where(x => x.ActionLogType.ToLower().Equals(action.ToLower())).FirstOrDefault();
                //log.ActionLogId = act.Id;
                log.UserId = UserId;
                //log.Comment = Comment;
                log.LogDate = DateTime.Now;
                log.IsActive = true;
                log.PageId = pages.Id;
                log = db.Logs.Add(log);
               // try
               // {
                    db.SaveChanges();
               // }
               // catch (Exception ex) { }
                return log.Id;
                // var body = JsonConvert.SerializeObject(log);
                // var log2 = ServerResponse.Invoke<Log>("api/log/edit?id=" + log.Id, body, "POST");
            }

            if (action.Equals("Print"))
            {
                var act = db.ActionLogs.Where(x => x.ActionLogType.ToLower().Equals(action.ToLower())).FirstOrDefault();
                log.ActionLogId = act.Id;
                log.UserId = UserId;
                //log.Comment = Comment;
                log.LogDate = DateTime.Now;
                log.IsActive = true;
                log.PageId = pages.Id;
                log = db.Logs.Add(log);
              //  try
               // {
                    db.SaveChanges();
              //  }
              //  catch (Exception ex) { }
                return log.Id;
                // var body = JsonConvert.SerializeObject(log);
                // var log2 = ServerResponse.Invoke<Log>("api/log/edit?id=" + log.Id, body, "POST");
            }
            if (action.Equals("Delete"))
            {
                var actionLog = db.ActionLogs.Where(x => x.ActionLogType.ToLower().Equals(action.ToLower())).FirstOrDefault();
                var act = db.ActionLogs.Where(x => x.ActionLogType.ToLower().Equals(action.ToLower())).FirstOrDefault();
                log.ActionLogId = act.Id;
                log.UserId = UserId;
                //log.Comment = Comment;
                log.LogDate = DateTime.Now;
                log.IsActive = true;
                log.PageId = pages.Id;
                log = db.Logs.Add(log);
               // try
               // {
                    db.SaveChanges();
              //  }
              //  catch (Exception ex) { }
                return log.Id;
            }
            if (action.Equals("View"))
            {
                //  var logView = db.Logs.Where(x => x.User.Id == UserId).FirstOrDefault();
                var act = db.ActionLogs.Where(x => x.ActionLogType.ToLower().Equals(action.ToLower())).FirstOrDefault();
           
                log.UserId = UserId;
                //log.Comment = Comment;
                log.LogDate = DateTime.Now;
                log.IsActive = true;
                log.PageId = pages.Id;
                //var body = JsonConvert.SerializeObject(log);           
                //var StaffCreate = ServerResponse.Invoke<Log>("api/log/create", body, "Post");
                log = db.Logs.Add(log);
              //  try
              //  {
                    db.SaveChanges();
              //  }
               // catch (Exception ex) { }
                return log.Id;
            }
            return log.Id;
        }

        public static List<int> GetPages(int TotalCount, int PageSize, int CurrentPage, int PagesToShow = 10)
        {
            int totalPages = (TotalCount / PageSize) + (TotalCount % PageSize == 0 ? 0 : 1);
            var UpperBound = CurrentPage + PagesToShow;
            if (totalPages > PagesToShow)
            {
                if (UpperBound > totalPages)
                {
                    return Enumerable.Range(totalPages - PagesToShow + 1, PagesToShow).ToList();
                }
                else
                {
                    return Enumerable.Range(CurrentPage, PagesToShow).ToList();
                }
            }
            else
            {
                return Enumerable.Range(1, totalPages).ToList();
            }
        

    }
		public static string OrderNo(List<PurchaseOrderModel> list)
		{
			try
			{
				var lastNo = "";
				var Data = list;
				if (Data.Count == 0)
				{
					lastNo = "0000000001";
				}
				else
				{
					lastNo = Data.FirstOrDefault().OrderNumber;
					var NewNumber = Convert.ToInt32(lastNo.Remove(0, 1)) + 1;
					var length = NewNumber.ToString().Length;
					var oldval = lastNo.Substring(lastNo.Length - length);
					lastNo = lastNo.Replace(oldval, NewNumber.ToString());

					//Checking for maxx limit 999999
				 //  if (lastNo.Remove(1).ToUpper() != "Q")
					//{
					//    lastNo = "Q" + lastNo;
					//}
				}
				return lastNo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public static string IBtNo(List<CartonManagementModel> list)
		{
			try
			{
				var lastNo = "";
				var Data = list;
				if (Data.Count == 0)
				{
					lastNo = "0000000001";
				}
				else
				{
					lastNo = Data.LastOrDefault().IBTNumber;
					var NewNumber = Convert.ToInt32(lastNo.Remove(0, 1)) + 1;
					var length = NewNumber.ToString().Length;
					var oldval = lastNo.Substring(lastNo.Length - length);
					lastNo = lastNo.Replace(oldval, NewNumber.ToString());

					//Checking for maxx limit 999999
					//if (lastNo.Remove(1).ToUpper() != "B")
					//{
					//	lastNo = "B" + lastNo;
					//}
				}
				return lastNo;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public static string ReceiptNo(List<ReceiveOrderModel> list)
		{
			try
			{
				var lastNo = "";
				var Data = list;
				if (Data.Count == 0)
				{
					lastNo = "0000000001";
				}
				else
				{
					lastNo = Data.FirstOrDefault().ReceiptNumber;
					var NewNumber = Convert.ToInt32(lastNo.Remove(0, 1)) + 1;
					var length = NewNumber.ToString().Length;
					var oldval = lastNo.Substring(lastNo.Length - length);
					lastNo = lastNo.Replace(oldval, NewNumber.ToString());

					//Checking for maxx limit 999999
					//if (lastNo.Remove(1).ToUpper() != "R")
					//{
					//	lastNo = "R" + lastNo;
					//}
				}
				return lastNo;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public static string GenerateRandomCode(int digits)
        {
            Random randrom = new Random();
            var min = 1;
            for (int i = 0; i < digits - 1; i++)
            {
                min *= 10;
            }
            var max = min * 10;

            return randrom.Next(min, max).ToString();
        }
        public static bool ValidateUser(byte[] passwordHash, byte[] passwordSalt, string password)
        {
            var hashAlgorithm = new SHA512HashAlgorithm();
            return CompareByteArrays(passwordHash, hashAlgorithm.GenerateSaltedHash(GetBytes(password), passwordSalt));
        }
        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            return array1.Length == array2.Length && !array1.Where((t, i) => t != array2[i]).Any();
        }
		
		public static byte[] CreateSalt(int size)
        {

            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return buff;
        }
        public static byte[] GetBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
	
		public class SHA512HashAlgorithm
        {
            private readonly SHA512Managed _algorithm = new SHA512Managed();

            public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
            {
                var plainTextWithSaltBytes =
                    new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
                }

                return _algorithm.ComputeHash(plainTextWithSaltBytes);
            }

            public string Type { get { return "SHA512"; } }

        }

		public static string IBtNoForStock(List<CartonManagementForStockTransferModel> list)
		{
			try
			{
				var lastNo = "";
				var Data = list;
				if (Data.Count == 0)
				{
					lastNo = "0000000001";
				}
				else
				{
					lastNo = Data.LastOrDefault().IBTNumber;
					var NewNumber = Convert.ToInt32(lastNo.Remove(0, 1)) + 1;
					var length = NewNumber.ToString().Length;
					var oldval = lastNo.Substring(lastNo.Length - length);
					lastNo = lastNo.Replace(oldval, NewNumber.ToString());

					//Checking for maxx limit 999999
					//if (lastNo.Remove(1).ToUpper() != "B")
					//{
					//	lastNo = "B" + lastNo;
					//}
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


