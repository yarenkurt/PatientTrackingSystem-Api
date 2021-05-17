using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Validation;
using Core.Results;
using DataAccess.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [ValidationAspect(typeof(PatientAnswerValidator))]
    public class PatientAnswerService : ServiceRepository<PatientAnswer>, IPatientAnswerService
    {
        private readonly IRepository<PatientAnswer> _repository;
        private readonly IRepository<QuestionPool> _questionRepo;
        private readonly IRepository<AnswerPool> _answerRepo;
        
        public PatientAnswerService(IRepository<PatientAnswer> repository, IRepository<QuestionPool> questionRepo, IRepository<AnswerPool> answerRepo) : base(repository)
        {
            _repository = repository;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
        }

        public async Task<List<GetAnswerDto>> GetAllAnswers(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Include(x => x.QuestionPool)
                .Where(x => x.IsActive == true)
                .Select(x => new GetAnswerDto
                {
                    QuestionDesc = x.QuestionPool.Description,
                    UpperLimit = x.QuestionPool.UpperLimit,
                    LowerLimit = x.QuestionPool.LowerLimit,
                    AnswerDesc = x.AnswerDescription,
                    PatientScore = x.Score,
                    Result = x.Result,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<List<GetAnswerDto>> GetAnswerHistory(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Include(x => x.QuestionPool)
                .Where(x => x.IsActive == false)
                .Select(x => new GetAnswerDto
                {
                    QuestionDesc = x.QuestionPool.Description,
                    UpperLimit = x.QuestionPool.UpperLimit,
                    LowerLimit = x.QuestionPool.LowerLimit,
                    AnswerDesc = x.AnswerDescription,
                    PatientScore = x.Score,
                    Result = x.Result
                })
                .ToListAsync();
        }
        
        public override Task<DataResult<PatientAnswer>> InsertAsync(PatientAnswer entity)
        {
            var question = _questionRepo.TableNoTracking
                .Where(x => x.Id == entity.QuestionPoolId)
                .Select(x => new QuestionPool
                    {
                        Id = x.Id,
                        DepartmentId = x.DepartmentId,
                        LowerLimit = x.LowerLimit,
                        UpperLimit = x.UpperLimit
                    }).FirstOrDefaultAsync();

            if (entity.Score <= question.Result.UpperLimit && entity.Score >= question.Result.LowerLimit)
            {
                entity.Result = true;
            }
            else
            {
                entity.Result = false;
            }

            entity.IsActive = true;
            
            return base.InsertAsync(entity);
        }

        public decimal GetTotalScoreOfPatient(int patientId)
        {
            return _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Sum(x => x.Score);
        }


        public int CountRiskyAnswers(int patientId)
        {
            return  _repository.TableNoTracking
                .Where(x => x.PatientId == patientId)
                .Count(x => x.Result == false);
        }


        public override async Task<Result> DeleteAsync(int id)
        {
            var patAnswer = await _repository.GetAsync(id);

            patAnswer.IsActive = false;
            return await _repository.UpdateAsync(patAnswer);
        }
        
        public async Task<int> GetId(int questionId)
        {
            PatientAnswer answer = await _repository.TableNoTracking
                .Where(x => x.QuestionPoolId == questionId && x.IsActive == true)
                .FirstOrDefaultAsync();

            return answer.Id;
        }
    }
}