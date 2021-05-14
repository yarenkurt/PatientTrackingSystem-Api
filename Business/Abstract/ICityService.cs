using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICityService : IServiceRepository<City>
    {
        Task<List<GetCityDto>> GetAllAsync(int countryId);

        Task<List<City>> SearchCity(string searchKey);

        Task<int> CountAsync();
        
        Task<List<GetCityDto>> GetAll();
        
    }
}