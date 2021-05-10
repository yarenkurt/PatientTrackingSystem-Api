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
    [ValidationAspect(typeof(DistrictValidator))]
    public class DistrictService : ServiceRepository<District>, IDistrictService
    {
        private readonly IRepository<District> _repository;
        
        public DistrictService(IRepository<District> repository) : base(repository)
        {
            _repository = repository;
        }
        
   
        public async Task<List<GetDistrictDto>> GetAllAsync(int cityId)
        {
            return await _repository.TableNoTracking.Where(d => d.CityId == cityId)
                .Include(x => x.City)
                .Select(x => new GetDistrictDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    CityName = x.City.Description
                }).ToListAsync();
        }
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync()
        {
            return await _repository.TableNoTracking.CountAsync();
        }

        public async Task<List<GetDistrictDto>> GetAllDistricts()
        {
            return await _repository.TableNoTracking
                .Include(x => x.City)
                .Select(x => new GetDistrictDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    CityName = x.City.Description
                }).ToListAsync();
        }
    }
}