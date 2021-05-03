using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using DataAccess.Repositories;
using Entities.Concrete;
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

        public async Task<List<Hospital>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync()
        {
            return await _repository.TableNoTracking.CountAsync();
        }
    }
}