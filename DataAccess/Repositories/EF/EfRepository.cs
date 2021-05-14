using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Results;
using Core.Signatures;
using DataAccess.Contexts.EF;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EF
{
    public class EfRepository<TEntity> : IRepository<TEntity>
            where TEntity : class, IBaseEntity, new()
    {
        
        private DbSet<TEntity> _entity;
        private readonly PatientTrackingContext _context;

        public EfRepository(PatientTrackingContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public virtual DbSet<TEntity> Entity => _entity ??= _context.Set<TEntity>();

        public IQueryable<TEntity> Table => Entity;
        public IQueryable<TEntity> TableNoTracking => Entity.AsNoTracking();
        

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await TableNoTracking.ToListAsync() : await TableNoTracking.Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await Entity.FindAsync(id);
            if (entity != null) _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await TableNoTracking.FirstOrDefaultAsync(filter);
        }

        public async Task<DataResult<TEntity>> InsertAsync(TEntity entity)
        {
            if (entity == null) return new ErrorDataResult<TEntity>("Data not found");

            try
            {
                await Entity.AddAsync(entity);
                await SaveChangesAsync();
                return new SuccessDataResult<TEntity>(entity);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<TEntity>($"{e.Message}\n{e.InnerException}");
            }
        }

        public async Task<Result> UpdateAsync(TEntity entity)
        {
            if (entity == null) return new ErrorDataResult<TEntity>("Data not found!");

            try
            {
                foreach (var local in _context.Set<TEntity>().Local)
                    _context.Entry(local).State = EntityState.Detached;
                
                _context.Entry(entity).State = EntityState.Detached;
                Entity.Update(entity);
                await SaveChangesAsync();
               
                return new SuccessDataResult<TEntity>(entity);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<TEntity>($"{e.Message}\n{e.InnerException}");
            }
        }

        public async Task<Result> DeleteAsync(TEntity entity)
        {
            if (entity == null) return new ErrorDataResult<TEntity>("Data not found!");

            try
            {
                foreach (var local in _context.Set<TEntity>().Local)
                    _context.Entry(local).State = EntityState.Detached;
                
                Entity.Remove(entity);
                await SaveChangesAsync();
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorDataResult<TEntity>($"{e.Message}\n{e.InnerException}");
            }
        }
        

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(GetFullError(e));
            }
        }

        private string GetFullError(DbUpdateException e)
        {
            var entries = _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in entries)
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                        break;
                }

            return e.ToString();
        }
    }
}