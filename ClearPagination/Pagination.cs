using System;

namespace ClearPagination
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int SelectedRows { get; set; }

        public Pagination()
        {
            Page = 1;
            PageSize = 20;
        }

        public Pagination(int page, int pageSize = 20)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int RowsFrom => (Page - 1) * PageSize + 1;
        public int RowsTo => Page * PageSize;
        public int PagesCount => (int)Math.Ceiling((double)SelectedRows / PageSize);
    }
}
