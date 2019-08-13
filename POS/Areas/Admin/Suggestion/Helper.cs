using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace POS.Areas.Admin.Suggestion
{
	static class Helper
	{
		//---------Converting datatable to models list
		public static List<T> ConvertToList<T>(DataTable dt)
		{
			var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
			var properties = typeof(T).GetProperties();
			return dt.AsEnumerable().Select(row =>
			{
				var objT = Activator.CreateInstance<T>();
				foreach (var pro in properties)
				{
					if (columnNames.Contains(pro.Name.ToLower()))
					{
						try
						{
							if (pro.PropertyType.Name == "Int32")
							{
								var val = Convert.ToInt32(row[pro.Name]);
								pro.SetValue(objT, val);
							}
							else if (pro.PropertyType.Name == "Double")
							{
								var val = Convert.ToDouble(row[pro.Name]);
								pro.SetValue(objT, val);
							}
							else if (pro.PropertyType.Name == "DateTime")
							{
								var val = Convert.ToDateTime(row[pro.Name]);
								pro.SetValue(objT, val);
							}
							else
								pro.SetValue(objT, row[pro.Name]);
						}
						catch (Exception ex) { throw ex; }
					}
				}
				return objT;
			}).ToList();
		}

		public static DataTable ToDataTable<T>(List<T> items)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);
			//Get all the properties by using reflection   
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
					//inserting value to table row by column
					values[i] = Props[i].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}

			return dataTable;
		}
	}

}