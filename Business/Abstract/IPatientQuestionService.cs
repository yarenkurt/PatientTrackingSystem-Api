using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPatientQuestionService : IServiceRepository<PatientQuestion>
    {
        Task<List<GetPatientQuestionDto>> GetAllQuestions(int patientId);
        Task<int> GetId(PatientQuestion patientQuestion);
        Task<int> GetIdByQuestion(int questionId);

    }
}