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
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [ValidationAspect(typeof(PatientDiseaseValidator))]
    public class PatientDiseaseService : ServiceRepository<PatientDisease>, IPatientDiseaseService
    {
        private readonly IRepository<PatientDisease> _repository;
        
        public PatientDiseaseService(IRepository<PatientDisease> repository) : base(repository)
        {
            _repository = repository;
        }
        
        
        [SecurityAspect(PersonType.Patient)]
        public async Task<List<GetPatDiseaseDto>> GetAllByPatient(int patientId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Disease)
                .Where(d => d.PatientId == patientId)
                .Select(x => new GetPatDiseaseDto
                {
                    Id = x.Id,
                    DepartmentId = x.Disease.DepartmentId,
                    DepartmentName = x.Disease.Department.Description,
                    Description = x.Disease.Description
                }).ToListAsync();
        }

        //Doctor ve admin görebilmeli
        [SecurityAspect(PersonType.Null)]

        public async Task<List<PatientDisease>> GetAllByDepartment(int deptId)
        {
            return await _repository.GetAllAsync(d => d.Disease.DepartmentId == deptId);
        }

        [SecurityAspect(PersonType.Null)]

        public async Task<List<PatientDisease>> GetAll()
        {
            //return await _repository.TableNoTracking.GroupBy(d => d.PatientId).ToListAsync();
            return await _repository.GetAllAsync();
        }

        
        [SecurityAspect(PersonType.Doctor)]
        public async Task<Result> AddDiseaseToPatient(PatientDisease newDisease)
        {
            return await _repository.InsertAsync(newDisease);
        }
        
        [SecurityAspect(PersonType.Doctor)]
        public async Task<int> GetId(PatientDisease patDisease)
        {
            PatientDisease disease = await _repository.TableNoTracking
                .Where(x => x.PatientId == patDisease.PatientId && x.DiseaseId == patDisease.DiseaseId && x.IsActive == true)
                .FirstOrDefaultAsync();
            return disease.Id;
        }
    }
}