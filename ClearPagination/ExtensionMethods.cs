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
                collection.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize),
                pagination
            );
        }
        /// <summary>
        /// Paginates collection
        /// </summary>
        /// <param name="query">Query to be paginated</param>
        /// <param name="pagination">Pagination object</param>
        /// <param name="materializeFunction">Function than will be applied to collection</param>
        /// <returns>Model containing subcollection and pagination object</returns>
        public static PaginationResult<T> Paginate<T>(this IQueryable<T> query, Pagination pagination,
            Func<IQueryable<T>, IEnumerable<T>> materializeFunction = null)
        {
            pagination.SelectedRows = query.Count();
            var taken = query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize);
            if (materializeFunction != null)
            {
                return new PaginationResult<T>(
                    materializeFunction(taken),
                    pagination
                );
            }
            return new PaginationResult<T>(
                taken,
                pagination
            );
        }

        /// <summary>
        /// Paginates collection
        /// </summary>
        /// <param name="collection">Collection to be paginated</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Size of page</param>
        /// <returns>Model containing subcollection and pagination object</returns>
        public static PaginationResult<T> Paginate<T>(this IEnumerable<T> collection, int page, int pageSize = 20)
            => collection.Paginate(new Pagination(page, pageSize));

        /// <summary>
        /// Change collection type of List in PaginationResults
        /// </summary>
        /// <param name="oldList">Pagination result</param>
        /// <param name="func">Projection old element to new element</param>
        /// <returns></returns>
        public static PaginationResult<TNew> Cast<TNew, TOld>(
                this PaginationResult<TOld> oldList,
                Func<TOld, TNew> func)
            => new PaginationResult<TNew>(oldList.List.Select(func), oldList.Pagination);
    }
}