using System.Collections.Generic;
using System.Linq;
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
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [ValidationAspect(typeof(PatientQuestionValidator))]
    public class PatientQuestionService : ServiceRepository<PatientQuestion>, IPatientQuestionService
    {
        private readonly IRepository<PatientQuestion> _repository;

        public PatientQuestionService(IRepository<PatientQuestion> repository) : base(repository)
        {
            _repository = repository;
        }


        [SecurityAspect(PersonType.Patient)]

        public async Task<List<GetPatientQuestionDto>> GetAllQuestions(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Include(x => x.QuestionPool)
                .Include(x => x.Patient)
                .Where(x => x.IsActive == true)
                .Select(x => new GetPatientQuestionDto
                {
                    PatientName = x.Patient.Person.FirstName + " " +  x.Patient.Person.LastName,
                    QuestionDesc = x.QuestionPool.Description,
                    QuestionType = x.QuestionPool.QuestionType
                }).ToListAsync();
        }

        
        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<PatientQuestion>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        
        [SecurityAspect(PersonType.Doctor)]
        public async Task<int> GetId(PatientQuestion patientQuestion)
        {
            PatientQuestion question = await _repository.TableNoTracking
                .Where(x => x.PatientId == patientQuestion.PatientId &&
                            x.QuestionPoolId == patientQuestion.QuestionPoolId &&
                            x.IsActive == true)
                .FirstOrDefaultAsync();

            return question.Id;
        }
        
        public async Task<int> GetIdByQuestion(int questionId)
        {
            PatientQuestion question = await _repository.TableNoTracking
                .Where(x => x.QuestionPoolId == questionId)
                .FirstOrDefaultAsync();

            return question.Id;
        }

        
        [SecurityAspect(PersonType.Doctor)]

        public override async Task<Result> DeleteAsync(int id)
        {
            var patQuestion = await _repository.GetAsync(id);
            patQuestion.IsActive = false;

            return await _repository.UpdateAsync(patQuestion);
        }
    }
}