using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDoctorPatientService : IServiceRepository<DoctorPatient>
    {
        Task<int> CountPatientOfDoctor(int doctorId);
        
        Task<List<GetPatientDto>> GetActivePatientListOfDoctor(int doctorId);
        Task<List<GetPatientDto>> GetPassivePatientListOfDoctor(int doctorId);

        Task<List<GetDoctorDto>> GetDoctorListOfPatient(int patientId);

        //Hasta ve doktor bazında gruplama?
    }
}