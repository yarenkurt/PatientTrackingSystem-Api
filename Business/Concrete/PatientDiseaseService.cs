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
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(PatientDiseaseValidator))]
    public class PatientDiseaseService : ServiceRepository<PatientDisease>, IPatientDiseaseService
    {
        private readonly IRepository<PatientDisease> _repository;
        
        public PatientDiseaseService(IRepository<PatientDisease> repository) : base(repository)
        {
            _repository = repository;
        }
        
        
        [SecurityAspect(PersonType.Patient)]
        public async Task<List<PatientDisease>> GetAllByPatient(int patientId)
        {
            return await _repository.GetAllAsync(d => d.PatientId == patientId);
        }

        //Doctor ve admin görebilmeli
        public async Task<List<PatientDisease>> GetAllByDepartment(int deptId)
        {
            return await _repository.GetAllAsync(d => d.Disease.DepartmentId == deptId);
        }

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
    }
}