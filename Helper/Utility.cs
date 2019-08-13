using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper
{
	public static class Utility
	{
			
        public static Dictionary<string, string> getKeyVaue<T>(T result)
        {
            var keyvalue = new Dictionary<string, string>();
            Type type = result.GetType();
            PropertyInfo[] props = type.GetProperties();

            string value = "";
            foreach (var prop in props)
            {
                if (prop.GetValue(result) != null)
                {
                    value = prop.GetValue(result).ToString();
                }
                else { value = ""; }
                keyvalue.Add(prop.Name, value);
            }
            return keyvalue;
        }
        public static string RemoveWhiteSpace(this string input)
        {
            var str = Regex.Replace(input, @"\s+", "");
            return str;
        }
        public static List<string> getRangeList(int start, int end, int size)
        {
            if (end <= 0)
            {
                return new List<string>();
            }
            int x = 1;
            var str = "";
            int value = start;
            while (true)
            {
                x = x * 10;
                value = (value / 10);
                size = size - 1;
                if (value == 0)
                {
                    break;
                }
            }

            for (int j = 0; j < size; j++)
            {
                str += "0";
            }
            var list = new List<string>();

            if (start <= end)
            {
                for (int i = start; i <= end; i++)
                {
                    if ((i / x) == 1)
                    {
                        x = 10 * x;
                        var len = str.Length;
                        try
                        {
                            str = str.Remove(len - 1);
                        }
                        catch (Exception) { }
                    }
                    if (i < x)
                    {
                        list.Add(str + i);
                    }
                }
            }
            return list;
        }
    }
}
