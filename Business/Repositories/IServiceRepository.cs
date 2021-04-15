using System.Threading.Tasks;
using Core.Results;
using Core.Signatures;

namespace Business.Repositories
{
    public interface IServiceRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        Task<TEntity> GetAsync(int id);
        
        Task<DataResult<TEntity>> InsertAsync(TEntity entity);
        
        Task<Result> UpdateAsync(TEntity entity);

        Task<Result> DeleteAsync(int id);
    }
}