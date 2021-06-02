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
        Task<List<GetAnswerDto>> GetAnswerHistory(int patientId);
        Task<List<PatientAnswer>> GetAll();

        decimal GetTotalScoreOfPatient(int patientId);
        Task<decimal> GetMyTotalScore(int personId);
        int CountRiskyAnswers(int patientId);
        Task<int> GetId(int questionId);


    }
}