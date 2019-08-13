
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AreaPaginationModel
    {

       // public IPagedList<AreaModel> Items { get; set; }

        public Pager1 Pager1 { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ManagerID { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual UserModel User { get; set; }
        public List<AreaPaginationModel> ListItems { get; set; }

    }

    public class Pager1
    {
        public Pager1(int totalItems, int? page, int pageSize = 3)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

    }


}
