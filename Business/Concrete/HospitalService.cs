using System.Collections.Generic;
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
    [ValidationAspect(typeof(HospitalValidator))]
    public class HospitalService : ServiceRepository<Hospital>, IHospitalService
    {
        private readonly IRepository<Hospital> _repository;
        
        public HospitalService(IRepository<Hospital> repository,IUserService userService) : base(repository)
        {
            _repository = _repository;
            userService.Check(new List<PersonType> {PersonType.Admin});
        }

        public async Task<List<Hospital>> GetAllAsync(int districtId)
        {
            return await _repository.GetAllAsync(h => h.DistrictId == districtId);
        }
    }
}