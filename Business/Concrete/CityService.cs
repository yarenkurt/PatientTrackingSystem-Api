using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Admin)]
    [ValidationAspect(typeof(CityValidator))]
    public class CityService : ServiceRepository<City>, ICityService
    {
        private readonly IRepository<City> _repository;

        public CityService(IRepository<City> repository) : base(repository)
        {
            _repository = repository;
        }
        

        public async Task<List<City>> GetAllAsync(int countryId)
        {
            return await _repository.GetAllAsync(c => c.CountryId == countryId);
        }

        
        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountCities()
        {
            return await _repository.Table.CountAsync();
        }

        
        [SecurityAspect(PersonType.Admin)]
        public async Task<List<City>> SearchCity(string searchKey)
        {
            return await _repository.GetAllAsync(c => c.Description.Contains(searchKey));
        }
    }
}