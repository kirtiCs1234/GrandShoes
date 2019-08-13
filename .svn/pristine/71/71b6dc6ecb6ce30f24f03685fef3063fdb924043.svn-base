using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS
{
    public static class ProductExtensionClass
    {
        public static string GetCellValue(this ExcelWorksheet sheet, int row, int column)
        {
            try
            {
                return sheet.Cells[row, column].Value.ToString();
            }
            catch(Exception)
            {
                return "";
            }
        }
    }
}