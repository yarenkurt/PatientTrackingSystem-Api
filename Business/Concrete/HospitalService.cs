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
    [ValidationAspect(typeof(HospitalValidator))]
    public class HospitalService : ServiceRepository<Hospital>, IHospitalService
    {
        private readonly IRepository<Hospital> _repository;
        
        public HospitalService(IRepository<Hospital> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<GetHospitalDto>> GetAllAsync()
        {
            return await _repository.TableNoTracking
                .Include(x => x.District)
                .ThenInclude(y => y.City)
                .ThenInclude(z => z.Country)
                .Select(t => new GetHospitalDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Phone = t.Phone,
                    Address = t.Address,
                    DistrictName = t.District.Description,
                    CityName = t.District.City.Description,
                    CountryName = t.District.City.Country.Description,
                    DistrictId = t.DistrictId
                }).ToListAsync();
        }
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync()
        {
            return await _repository.TableNoTracking.CountAsync();
        }

        public async Task<GetHospitalDto> GetHospDto(int id)
        {
            return await _repository.TableNoTracking
                .Where(h => h.Id == id)
                .Include(x => x.District)
                .ThenInclude(y => y.City)
                .ThenInclude(z => z.Country)
                .Select(t => new GetHospitalDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Phone = t.Phone,
                    Address = t.Address,
                    DistrictName = t.District.Description,
                    CityName = t.District.City.Description,
                    CountryName = t.District.City.Country.Description,
                    DistrictId = t.DistrictId,
                    CityId = t.District.CityId,
                    CountryId = t.District.City.CountryId
                }).FirstOrDefaultAsync();
        }
    }
}