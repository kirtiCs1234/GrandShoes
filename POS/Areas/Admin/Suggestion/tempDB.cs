using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using Excel = Microsoft.Office.Interop.Excel;

namespace POS.Areas.Admin.Suggestion
{
	static class tempDB
	{
		public static List<Suggestion> ReadExcelFile()
		{
			//string filePath = "E:/AutoSugg1.xlsx";
			List<Suggestion> Data = new List<Suggestion>();

			//Excel.Application xlApp = new Excel.Application();
			//Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath);
			//Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

			//Excel.Range xlRange = xlWorkSheet.UsedRange;
			//int totalRows = xlRange.Rows.Count;
			//int totalColumns = xlRange.Columns.Count;

			//string firstValue, secondValue, thirdValue, fourthValue, fifthvalue;
			//for (int rowCount = 1; rowCount <= totalRows; rowCount++)
			//{

			//	firstValue = Convert.ToString((xlRange.Cells[rowCount, 1] as Excel.Range).Text);
			//	secondValue = Convert.ToString((xlRange.Cells[rowCount, 2] as Excel.Range).Text);
			//	thirdValue = Convert.ToString((xlRange.Cells[rowCount, 3] as Excel.Range).Text);
			//	fourthValue = Convert.ToString((xlRange.Cells[rowCount, 4] as Excel.Range).Text);
			//	fifthvalue = Convert.ToString((xlRange.Cells[rowCount, 5] as Excel.Range).Text);
			//	Console.WriteLine(firstValue + "\t" + secondValue + "\t" + thirdValue + "\t" + fourthValue + "\t" + fifthvalue);
			//	Suggestion s = new Suggestion();
			//	if (rowCount > 1)
			//	{
			//		s.BranchID = Convert.ToInt32(firstValue);
			//		s.SoldTotal = Convert.ToDouble(thirdValue);
			//		s.RemainingTotal = Convert.ToDouble(fourthValue);
			//		s.ReceivedDate = Convert.ToDateTime(fifthvalue);
			//		Data.Add(s);
			//	}
			//}

			//xlWorkBook.Close();
			//xlApp.Quit();

			//Marshal.ReleaseComObject(xlWorkSheet);
			//Marshal.ReleaseComObject(xlWorkBook);
			//Marshal.ReleaseComObject(xlApp);
			return Data;
		}
	}
}
