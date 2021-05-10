using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDistrictService : IServiceRepository<District>
    {
        Task<List<GetDistrictDto>> GetAllAsync(int cityId);
        Task<int> CountAsync();
        Task<List<GetDistrictDto>> GetAllDistricts();
    }
}