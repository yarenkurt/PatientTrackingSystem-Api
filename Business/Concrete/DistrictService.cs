using System.Collections.Generic;
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
        
   
        public async Task<List<District>> GetAllAsync(int cityId)
        {
            return await _repository.GetAllAsync(d => d.CityId == cityId);
        }
    }
}