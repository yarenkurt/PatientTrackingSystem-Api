using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDiseaseService : IServiceRepository<Disease>
    {
        Task<List<Disease>> GetAllAsync();
        Task<List<Disease>> GetAllAsyncByDept(int deptId);
    }
}