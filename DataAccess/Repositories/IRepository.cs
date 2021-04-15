using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Results;
using Core.Signatures;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// Get all entities by applying any filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        
        /// <summary>
        /// Inserting an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<DataResult<TEntity>> InsertAsync(TEntity entity);
        Task<Result> UpdateAsync(TEntity entity);
        Task<Result> DeleteAsync(TEntity entity);

    }
}