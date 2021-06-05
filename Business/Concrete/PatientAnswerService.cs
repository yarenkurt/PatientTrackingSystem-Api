using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Validation;
using Core.Results;
using Core.Token;
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
        private readonly IRepository<Patient> _patientRepo;
        private IUserService _userService;
        
        public PatientAnswerService(IRepository<PatientAnswer> repository, IRepository<QuestionPool> questionRepo, IRepository<AnswerPool> answerRepo, IUserService userService, IRepository<Patient> patientRepo) : base(repository)
        {
            _repository = repository;
            _questionRepo = questionRepo;
            _answerRepo = answerRepo;
            _userService = userService;
            _patientRepo = patientRepo;
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

        public async Task<decimal> GetMyTotalScore(int personId)
        {
            return await _repository.TableNoTracking.Include(x=>x.Patient).Where(x => x.Patient.PersonId == personId)
                .SumAsync(x => x.Score);
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

        public async Task<List<PatientAnswer>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Result> InsertPatientAnswer(InsertPatientAnswerDto dto, int personId)
        {
            var patientId = (await _patientRepo.GetAsync(x => x.PersonId == personId))?.Id ?? 0;
            if (patientId == 0)
            {
                return new ErrorResult("Patient not found!");
            }

            var entity = new PatientAnswer
            {
                Score = dto.Score,
                QuestionPoolId = dto.QuestionId,
                AnswerDescription = dto.AnswerDescription,
                PatientId = patientId
            };
            return await _repository.InsertAsync(entity);
        }
    }
}