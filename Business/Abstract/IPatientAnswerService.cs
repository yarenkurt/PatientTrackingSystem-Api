using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPatientAnswerService : IServiceRepository<PatientAnswer>
    {
        Task<List<PatientAnswer>> GetAllAnswers(int patientId);
        decimal GetTotalScoreOfPatient(int patientId);
    }
}