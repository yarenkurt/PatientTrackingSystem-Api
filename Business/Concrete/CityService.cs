using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using DataAccess.Repositories;
using Entities.Concrete;
using Entities.Dtos;
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
        

        public async Task<List<GetCityDto>> GetAllAsync(int countryId)
        {
            return await _repository.TableNoTracking.Where(x => x.CountryId == countryId)
                .Include(x => x.Country)
                .Select(x => new GetCityDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    CountryName = x.Country.Description
                }).ToListAsync();
        }
        
        
        
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<List<City>> SearchCity(string searchKey)
        {
            return await _repository.GetAllAsync(c => c.Description.Contains(searchKey));
        }


        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync()
        {
            return await _repository.TableNoTracking.CountAsync();
        }

        [SecurityAspect(PersonType.Admin)]
        public async Task<List<GetCityDto>> GetAll()
        {
            return await  _repository.TableNoTracking
                .Include(x => x.Country)
                .Select(x => new GetCityDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    CountryName = x.Country.Description
                }).ToListAsync();
        }

    }
}