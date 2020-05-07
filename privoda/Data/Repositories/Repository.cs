using Microsoft.EntityFrameworkCore;
using privoda.Contracts.Repositories;
using privoda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace privoda
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PrivodaDbContext _dbContext;

        /// <summary>
        /// List of navigation properties that should be included
        /// </summary>
        public List<string> Includes { get; private set; }

        /// <summary>
        /// Constructor of entity repository
        /// </summary>
        /// <param name="dbContext">db context</param>
        /// <param name="includes">list of navigation properties that should be included</param>
        public Repository(PrivodaDbContext dbContext, string[] includes = null)
        {
            _dbContext = dbContext;
            Includes = includes?.Length > 0 ? includes.ToList() : null;
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _dbContext.AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<T> CreateAsync(T item, List<string> includeEntities = null)
        {
            var localChanges = _dbContext.Set<T>().Local.ToList();
            if (localChanges?.Any() == true)
            {
                foreach (var local in localChanges)
                {
                    _dbContext.Entry(local).State = EntityState.Detached;
                }
            }

            var added = await _dbContext.AddAsync<T>(item);
            await _dbContext.SaveChangesAsync();

            var props = includeEntities ?? Includes;

            if (props?.Any() == true)
            {
                foreach (var prop in Includes)
                {
                    SetEntryLoads(item, prop);
                }
            }

            return added.Entity;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var itemsToRemove = await GetManyAsync(predicate);

            if (itemsToRemove.Any())
            {

                if (itemsToRemove.Count() > 1)
                {
                    _dbContext.RemoveRange(itemsToRemove);
                }
                else
                {
                    _dbContext.Remove(itemsToRemove.FirstOrDefault());
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, List<string> includeEntities = null)
        {
            IQueryable<T> items;

            if (predicate != null)
                items = _dbContext.Set<T>().AsNoTracking().Where(predicate);
            else
                items = _dbContext.Set<T>().AsNoTracking();

            if (items != null)
            {
                var props = includeEntities ?? Includes;

                if (props?.Any() == true)
                    foreach (var prop in props)
                    {
                        items = items.Include(prop);
                    }
            }

            return await items.FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate = null, List<string> includeEntities = null)
        {
            IQueryable<T> items;

            if (predicate != null)
                items = _dbContext.Set<T>().AsNoTracking().Where(predicate);
            else
                items = _dbContext.Set<T>().AsNoTracking();

            var props = includeEntities ?? Includes;

            if (props?.Any() == true)
            {
                foreach (var prop in props)
                {
                    items = items.Include(prop);
                }
            }

            return await items.ToListAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(T item)
        {
            var localChanges = _dbContext.Set<T>().Local.ToList();
            if (localChanges?.Any() == true)
            {
                foreach (var local in localChanges)
                {
                    _dbContext.Entry(local).State = EntityState.Detached;
                }
            }

            _dbContext.Entry<T>(item).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateRangeAsync(IEnumerable<T> items)
        {
            _dbContext.UpdateRange(items);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Sets lazy loads for one-to-many(properties) and many-to-one(collections)
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="prop">property</param>
        private void SetEntryLoads(T item, string prop)
        {
            if (TryLoadEntityCollection(item, prop))
            {
                return;
            }
            if (TryLoadEntityPropertyOrReference(item, prop))
            {
                return;
            }

            return;
        }

        /// <summary>
        /// Load entity if it collection
        /// </summary>
        /// <param name="item">entity item</param>
        /// <param name="prop">property name</param>
        /// <returns></returns>
        private bool TryLoadEntityCollection(T item, string prop)
        {
            var collection = _dbContext.Entry(item).Collections.FirstOrDefault(c => c.Metadata.Name.Equals(prop));

            if (collection is null) return false;

            if (_dbContext.Entry(item).Collection(prop).IsLoaded)
            {
                return true;
            }
            else
            {
                foreach (var colEntry in _dbContext.Entry(item).Collections)
                {
                    if (colEntry.Metadata.Name.Equals(prop))
                    {
                        _dbContext.Entry(item).Collection(prop).Load();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Load entity if it property or reference
        /// </summary>
        /// <param name="item">entity item</param>
        /// <param name="prop">property name</param>
        /// <returns></returns>
        private bool TryLoadEntityPropertyOrReference(T item, string prop)
        {
            var reference = _dbContext.Entry(item).References.FirstOrDefault(c => c.Metadata.Name.Equals(prop));

            if (reference is null) return false;

            if (_dbContext.Entry(item).Reference(prop).IsLoaded)
            {
                return true;
            }
            else
            {
                foreach (var propEntry in _dbContext.Entry(item).Properties)
                {
                    if (propEntry.Metadata.Name.Equals(prop))
                    {
                        _dbContext.Entry(item).Reference(prop).Load();
                        return true;
                    }
                }
                foreach (var propEntry in _dbContext.Entry(item).References)
                {

                    if (propEntry.Metadata.Name.Equals(prop))
                    {
                        _dbContext.Entry(item).Reference(prop).Load();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
