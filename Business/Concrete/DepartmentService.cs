using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Results;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Null)]
    [ValidationAspect(typeof(DepartmentValidator))]
    public class DepartmentService : ServiceRepository<Department>, IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        
        public DepartmentService(IRepository<Department> repository) : base(repository)
        {
            _repository = repository;
        }
        
    
        public async Task<List<Department>> GetAllAsync(int hospitalId)
        {
            return await _repository.GetAllAsync(d => d.HospitalId == hospitalId && d.IsActive == true);
        }
        

        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync(int hospitalId)
        {
            return await _repository.TableNoTracking
                .Where(x => x.HospitalId == hospitalId && x.IsActive == true)
                .CountAsync();
        }


        public override async Task<Result> DeleteAsync(int id)
        {
            var dept = await _repository.GetAsync(id);
            dept.IsActive = false;

            return await _repository.UpdateAsync(dept);
        }
    }
}