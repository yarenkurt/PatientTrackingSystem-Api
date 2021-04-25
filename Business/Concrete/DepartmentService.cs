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
    [ValidationAspect(typeof(DepartmentValidator))]
    public class DepartmentService : ServiceRepository<Department>, IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        
        public DepartmentService(IRepository<Department> repository) : base(repository)
        {
            _repository = repository;
        }
        
    
        public async Task<List<Department>> GetAllByHospAsync(int hospitalId)
        {
            return await _repository.GetAllAsync(d => d.HospitalId == hospitalId);
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync(int hospitalId)
        {
            return await _repository.TableNoTracking
                .Where(x => x.HospitalId == hospitalId)
                .CountAsync();
        }
    }
}