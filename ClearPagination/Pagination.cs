using System;

namespace ClearPagination
{
    public class Pagination
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int SelectedRows { get; set; }

        public Pagination()
        {
        }

        public Pagination(int? page)
        {
            Page = page ?? 1;
        }

        public int RowsFrom => (Page - 1) * PageSize + 1;
        public int RowsTo => Page * PageSize;
        public int PagesCount => (int)Math.Ceiling((double)SelectedRows / PageSize);
    }
}
