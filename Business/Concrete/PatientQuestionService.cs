using System.Collections.Generic;
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

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(PatientQuestionValidator))]
    public class PatientQuestionService : ServiceRepository<PatientQuestion>, IPatientQuestionService
    {
        private readonly IRepository<PatientQuestion> _repository;

        public PatientQuestionService(IRepository<PatientQuestion> repository) : base(repository)
        {
            _repository = repository;
        }


        [SecurityAspect(PersonType.Patient)]
        public async Task<List<PatientQuestion>> GetAllQuestions(int patientId)
        {
            return await _repository.GetAllAsync(x => x.PatientId == patientId);
        }

        [SecurityAspect(PersonType.Doctor)]

        public async Task<Result> RemoveQuestionFromPatient(PatientQuestion patientQuestion)
        {
            return await _repository.DeleteAsync(patientQuestion);
        }
        
        
    }
}