using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDepartmentService : IServiceRepository<Department>
    {
        Task<List<Department>> GetAllByHospAsync(int hospitalId);
        Task<List<Department>> GetAllAsync();


        Task<int> CountAsync(int hospitalId);
    }
}