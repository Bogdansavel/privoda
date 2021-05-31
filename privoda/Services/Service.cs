using privoda.Contracts.Repositories;
using privoda.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace privoda.Services
{
    public class Service<T> : IService<T>
    {
        private readonly IRepository<T> _repository;

        /// <summary>
        /// CRUD service constructor
        /// </summary>
        /// <param name="repository">CRUD repository</param>
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _repository.AddRangeAsync(items);
        }

        /// <inheritdoc />
        public async Task<T> CreateAsync(T item, List<string> includeEntities = null)
        {
            return await _repository.CreateAsync(item, includeEntities);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                await _repository.DeleteAsync(predicate);
            }
            else
            {
                throw new ArgumentException("Predicate on delete need to be not null");
            }
        }

        /// <inheritdoc />
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, List<string> includeEntities = null)
        {
            return (predicate is null)
                ? await _repository.GetAsync(null, includeEntities)
                : await _repository.GetAsync(predicate, includeEntities);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate = null, List<string> includeEntities = null)
        {
            return (predicate is null)
                ? await _repository.GetManyAsync(null, includeEntities)
                : await _repository.GetManyAsync(predicate, includeEntities);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(T item)
        {
            await _repository.UpdateAsync(item);
        }

        /// <inheritdoc />
        public async Task UpdateRangeAsync(IEnumerable<T> items)
        {
            await _repository.UpdateRangeAsync(items);
        }
    }
}
