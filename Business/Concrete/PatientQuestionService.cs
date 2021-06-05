using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Helpers;
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


        [SecurityAspect(PersonType.Null)]
        public async Task<List<GetPatientQuestionDto>> GetAllQuestions(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Include(x => x.QuestionPool)
                .Include(x => x.Patient)
                .Where(x => x.IsActive == true)
                .Select(x => new GetPatientQuestionDto
                {
                    PatientName = x.Patient.Person.FirstName + " " + x.Patient.Person.LastName,
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

        public async Task<List<QuestionsDto>> MyQuestions(int personId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.QuestionPool)
                .Include(x => x.Patient).ThenInclude(x => x.Answers)
                .Where(x => x.Patient.PersonId == personId && x.IsActive && x.QuestionPool.IsActive)
                .Select(x => new QuestionsDto
                {
                    Id = x.Id,
                    QuestionId = x.QuestionPoolId,
                    Question = x.QuestionPool.Description,
                    Answer = x.Patient.Answers == null ? "" :
                             x.Patient.Answers.FirstOrDefault(y=>y.QuestionPoolId==x.QuestionPoolId).AnswerDescription ?? ""
                }).ToListAsync();
        }

        public async Task<QuestionDto> Questions(int id, int personId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.QuestionPool).ThenInclude(x=>x.Answers)
                .Where(x => x.QuestionPoolId == id && x.Patient.PersonId == personId && x.IsActive && x.QuestionPool.IsActive)
                .Select(x => new QuestionDto
                {
                    Id = x.Id,
                    QuestionId = x.QuestionPoolId,
                    Description = x.QuestionPool.Description,
                    UpperLimit = x.QuestionPool.UpperLimit,
                    LowerLimit = x.QuestionPool.LowerLimit,
                    QuestionType = EnumHelper.GetDisplayValue(x.QuestionPool.QuestionType),
                    AnswerPools = x.QuestionPool.Answers.Select(y=> new QuestionDto.AnswerPoolDto
                    {
                        Description = y.Description,
                        Id = y.Id,
                        Score = y.Score
                        
                    } ).ToList()
                    
                }).FirstOrDefaultAsync();
        }

       

        /*public async Task<List<MyQuestionDto>> MyQuestions(int personId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Patient).ThenInclude(x=>x.Answers)
                .Include(x => x.QuestionPool).ThenInclude(x => x.Answers)
                .Where(x => x.Patient.PersonId == personId)
                .Select(x => new MyQuestionDto
                {
                    Id = x.Id,
                    QuestionId = x.QuestionPoolId,
                    Description = x.QuestionPool.Description,
                    LowerLimit = x.QuestionPool.LowerLimit,
                    UpperLimit = x.QuestionPool.UpperLimit,
                    QuestionType = EnumHelper.GetDisplayValue(x.QuestionPool.QuestionType),
                    Answers = x.QuestionPool.Answers.Select(y => new MyQuestionDto.AnswerPoolDto
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Score = y.Score
                    }).ToList(),
                    PatientAnswers = x.Patient.Answers.Select(y => new MyQuestionDto.AnswerPoolDto
                    {
                        Id = y.Id,
                        Description = y.AnswerDescription,
                        Score = y.Score
                    }).ToList()
                }).ToListAsync();
        }
*/

        [SecurityAspect(PersonType.Doctor)]
        public override async Task<Result> DeleteAsync(int id)
        {
            var patQuestion = await _repository.GetAsync(id);
            patQuestion.IsActive = false;

            return await _repository.UpdateAsync(patQuestion);
        }
    }
}