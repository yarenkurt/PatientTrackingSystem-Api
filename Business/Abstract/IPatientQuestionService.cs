using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPatientQuestionService : IServiceRepository<PatientQuestion>
    {
        Task<List<PatientQuestion>> GetAllQuestions(int patientId);
    }
}