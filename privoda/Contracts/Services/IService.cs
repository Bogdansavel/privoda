using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace privoda.Contracts.Services
{
    /// <summary>
    /// CRUD service
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Add range items
        /// </summary>
        /// <param name="items">items for create</param>
        Task AddRangeAsync(IEnumerable<T> items);

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="predicate">predicate on get item</param>
        /// <param name="includeEntities">list of navigation properties that should be included</param>
        /// <returns>entity item</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, List<string> includeEntities = null);

        /// <summary>
        /// Get many items
        /// </summary>
        /// <param name="predicate">predicate on get items (if predicate is null get all items)</param>
        /// <param name="includeEntities">list of navigation properties that should be included</param>
        /// <returns>entity items</returns>
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate = null, List<string> includeEntities = null);

        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="item">item for create</param>
        /// <param name="includeEntities">list of navigation properties that should be included</param>
        /// <returns>created entity item</returns>
        Task<T> CreateAsync(T item, List<string> includeEntities = null);

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="item">item for update</param>
        Task UpdateAsync(T item);

        /// <summary>
        /// Update items
        /// </summary>
        /// <param name="items">items for update</param>
        Task UpdateRangeAsync(IEnumerable<T> items);

        /// <summary>
        /// Detete item
        /// </summary>
        /// <param name="predicate">predicate for delete item</param>
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
