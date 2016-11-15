using System.Collections.Generic;

namespace ClearPagination
{
    public class PaginationResult<TItemType>
    {
        public PaginationResult()
        {

        }
        public PaginationResult(IEnumerable<TItemType> list, Pagination pagination)
        {
            List = list;
            Pagination = pagination;
        }

        public Pagination Pagination { get; set; }
        public IEnumerable<TItemType> List { get; set; }
    }
}