using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IHospitalService : IServiceRepository<Hospital>
    {
        //Task<List<Hospital>> GetAllAsync(int districtId);
        
        Task<List<Hospital>> GetAllAsync();
        Task<int> CountAsync();
    }
}