using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearPagination
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Paginates collection
        /// </summary>
        /// <param name="collection">Collection to be paginated</param>
        /// <param name="pagination">Pagination object</param>
        /// <returns>Model containing subcollection and pagination object</returns>
        public static PaginationResult<T> Paginate<T>(this IEnumerable<T> collection, Pagination pagination)
        {
            pagination.SelectedRows = collection.Count();
            return new PaginationResult<T>(
                collection.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToList(),
                pagination
            );
        }

        /// <summary>
        /// Change collection type of List in PaginationResults
        /// </summary>
        /// <param name="oldList">Pagination result</param>
        /// <param name="func">Projection old element to new element</param>
        /// <returns></returns>
        public static PaginationResult<TNew> Cast<TNew, TOld>(this PaginationResult<TOld> oldList,
            Func<TOld, TNew> func)
        {
            return new PaginationResult<TNew>(oldList.List.Select(func), oldList.Pagination);
        }
    }
}