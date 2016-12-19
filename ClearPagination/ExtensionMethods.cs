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
        /// <param name="materializeFunction">Function than will be applied to collection</param>
        /// <returns>Model containing subcollection and pagination object</returns>
        public static PaginationResult<T> Paginate<T>(this IEnumerable<T> collection, Pagination pagination,
            Func<IEnumerable<T>, IEnumerable<T>> materializeFunction = null)
        {
            pagination.SelectedRows = collection.Count();
            var taken = collection.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize);
            if (materializeFunction != null)
            {
                taken = materializeFunction(taken);
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
        /// <param name="materializeFunction">Function than will be applied to collection</param>
        /// <returns>Model containing subcollection and pagination object</returns>
        public static PaginationResult<T> Paginate<T>(this IEnumerable<T> collection, int page, int pageSize = 20,
            Func<IEnumerable<T>, IEnumerable<T>> materializeFunction = null)
            => collection.Paginate(new Pagination(page, pageSize), materializeFunction);

        /// <summary>
        /// Paginates collection
        /// </summary>
        /// <param name="collection">Collection to be paginated</param>
        /// <param name="page">Page number</param>
        /// <param name="materializeFunction">Function than will be applied to collection</param>
        /// <returns>Model containing subcollection and pagination object</returns>
        public static PaginationResult<T> Paginate<T>(this IEnumerable<T> collection, int page, 
            Func<IEnumerable<T>, IEnumerable<T>> materializeFunction = null)
            => collection.Paginate(new Pagination(page, 20));

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