using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS.Areas
{
    public class ServiceClass
    {
        public Dictionary<string,string> getFilterData(Dictionary<string,string> addList, Dictionary<string, string> updateList, Dictionary<string, string> process)
        {
            var dictionaryFrom = new Dictionary<string, string>();
            if (addList != null && updateList != null)
                process.Where(x => !updateList.Keys.Contains(x.Key) && !addList.Keys.Contains(x.Key))
                    .ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));

            addList.ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));

            updateList.ToList().ForEach(x => dictionaryFrom.Add(x.Key, x.Value));

            return dictionaryFrom;
        }
    }
    public static class POSPaging
    {
        public static IPagedList<T> ToCustomPagedList<T>(this IEnumerable<T> superset, int pageNumber, int pageSize, int TotalItemCount)
        {
            var page = new StaticPagedList<T>(superset, pageNumber, pageSize, TotalItemCount);
            return page;
        }

        public static IPagedList<T> ToCustomPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize, int TotalItemCount)
        {
            var page = new StaticPagedList<T>(superset, pageNumber, pageSize, TotalItemCount);
            return page;
        }
    }
    }