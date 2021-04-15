using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(PatientQuestionValidator))]
    public class PatientQuestionService : ServiceRepository<PatientQuestion>, IPatientQuestionService
    {
        private readonly IRepository<PatientQuestion> _repository;

        public PatientQuestionService(IRepository<PatientQuestion> repository,IUserService userService) : base(repository)
        {
            _repository = repository;
            userService.Check(new List<PersonType> {PersonType.Doctor});
        }


        [SecurityAspect(PersonType.Patient)]
        public async Task<List<PatientQuestion>> GetAllQuestions(int patientId)
        {
            return await _repository.GetAllAsync(x => x.PatientId == patientId);
        }
        
        
    }
}