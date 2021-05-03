using System.Threading.Tasks;
using Core.Results;
using Core.Signatures;
using DataAccess.Repositories;

namespace Business.Repositories
{
    public class ServiceRepository<TEntity> : IServiceRepository<TEntity> 
        where TEntity : class, IBaseEntity, new()
    {

        private readonly IRepository<TEntity> _repository;

        public ServiceRepository(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public virtual async Task<DataResult<TEntity>> InsertAsync(TEntity entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public virtual async Task<Result> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<Result> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null) return new ErrorResult("Data not found");
            return await _repository.DeleteAsync(entity);
        }
    }
}