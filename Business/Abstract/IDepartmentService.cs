using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDepartmentService : IServiceRepository<Department>
    {
        Task<List<Department>> GetAllAsync(int hospitalId);
    }
}