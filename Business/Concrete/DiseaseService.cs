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
    [SecurityAspect(PersonType.Null)]
    [ValidationAspect(typeof(DiseaseValidator))]
    public class DiseaseService : ServiceRepository<Disease>, IDiseaseService
    {
        private readonly IRepository<Disease> _repository;
        
        public DiseaseService(IRepository<Disease> repository) : base(repository)
        {
            _repository = repository;
        }
        
        public async Task<List<GetDiseaseDto>> GetAllAsync()
        {
            return await _repository.TableNoTracking
                .Include(x => x.Department)
                .Select(x => new GetDiseaseDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    DepartmentName = x.Department.Description
                }).ToListAsync();
        }

        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<Disease>> GetAllAsyncByDept(int deptId)
        {
            return await _repository.GetAllAsync(d => d.DepartmentId == deptId);
        }
        
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync(int hospitalId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.Department.HospitalId == hospitalId)
                .CountAsync();
        }

        public async Task<List<GetDiseaseDto>> GetAllAsyncByHospital(int hospitalId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.Department.HospitalId == hospitalId)

                .Select(x => new GetDiseaseDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    DepartmentName = x.Department.Description
                }).ToListAsync();
        }
    }
}