using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDistrictService : IServiceRepository<District>
    {
        Task<List<District>> GetAllAsync(int cityId);
        Task<int> CountAsync();
        Task<List<District>> GetAllDistricts();
    }
}