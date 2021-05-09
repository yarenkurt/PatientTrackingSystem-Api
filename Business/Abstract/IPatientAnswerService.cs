using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPatientAnswerService : IServiceRepository<PatientAnswer>
    {
        Task<List<GetAnswerDto>> GetAllAnswers(int patientId);
        decimal GetTotalScoreOfPatient(int patientId);
        int CountRiskyAnswers(int patientId);

    }
}