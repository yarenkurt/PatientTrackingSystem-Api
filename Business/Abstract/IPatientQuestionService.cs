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
        Task<List<PatientQuestion>> GetAll();
        Task<int> GetId(PatientQuestion patientQuestion);
        Task<int> GetIdByQuestion(int questionId);

        //Task<List<MyQuestionDto>> MyQuestions(int personId);
        Task<List<QuestionsDto>> MyQuestions(int personId);
        Task<QuestionDto> Questions(int id,int personId);

    }
}