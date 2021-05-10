using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Core.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPatientDiseaseService : IServiceRepository<PatientDisease>
    {
        Task<List<PatientDisease>> GetAllByPatient(int patientId);
        Task<List<PatientDisease>> GetAllByDepartment(int deptId);
        Task<List<PatientDisease>> GetAll();
        
        Task<Result> AddDiseaseToPatient(PatientDisease newDisease);
        Task<int> GetId(PatientDisease patDisease);
    }
}