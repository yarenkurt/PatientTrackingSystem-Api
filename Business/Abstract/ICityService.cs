using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICityService : IServiceRepository<City>
    {
        Task<List<City>> GetAllAsync(int countryId);

        Task<List<City>> SearchCity(string searchKey);

        Task<int> CountAsync();
        
        Task<List<City>> GetAll();

    }
}