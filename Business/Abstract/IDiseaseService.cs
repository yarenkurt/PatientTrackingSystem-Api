using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDiseaseService : IServiceRepository<Disease>
    {
        Task<List<GetDiseaseDto>> GetAllAsync();
        Task<List<Disease>> GetAllAsyncByDept(int deptId);
        Task<int> CountAsync(int hospitalId);
        Task<List<GetDiseaseDto>> GetAllAsyncByHospital(int hospitalId);

    }
}