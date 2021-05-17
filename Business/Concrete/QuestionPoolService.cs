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
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(QuestionValidator))]
    public class QuestionPoolService : ServiceRepository<QuestionPool>, IQuestionPoolService
    {
        private readonly IRepository<QuestionPool> _questionRepo;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<AnswerPool> _answerRepo;
        private readonly IPatientAnswerService _patAnswerService;
        private readonly IPatientQuestionService _patQuestionService;
        private readonly IUserService _userService;
        
        
        public QuestionPoolService(IRepository<QuestionPool> repository, IRepository<Doctor> doctorRepo, IUserService userService, IRepository<AnswerPool> answerRepo, IPatientAnswerService patAnswerService, IPatientQuestionService patQuestionService) : base(repository)
        {
            _doctorRepo = doctorRepo;
            _userService = userService;
            _answerRepo = answerRepo;
            _patAnswerService = patAnswerService;
            _patQuestionService = patQuestionService;
            _questionRepo = repository;
        }
        

        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<QuestionPool>> GetAllByDept(int deptId)
        {
            return await _questionRepo.GetAllAsync(x => x.DepartmentId == deptId && x.IsActive == true);
        }

        
        public async Task<List<QuestionPool>> GetOnlyMCQ()
        {
            return await _questionRepo.GetAllAsync(x => x.QuestionType == QuestionType.MultipleChoice);
        }

        public async Task<List<QuestionPool>> GetOnlyNumQ()
        {
            return await _questionRepo.GetAllAsync(x => x.QuestionType == QuestionType.NumericInput);
        }

        public override async Task<DataResult<QuestionPool>> InsertAsync(QuestionPool entity)
        {
            var doctor = await _doctorRepo.GetAsync(x => x.PersonId == _userService.PersonId);

            entity.DepartmentId = doctor.DepartmentId;
            entity.IsActive = true;
            return await base.InsertAsync(entity);
        }

        public override async Task<Result> DeleteAsync(int id)
        {
            var question = await _questionRepo.GetAsync(x => x.Id == id);
            question.IsActive = false;

            var answerList = await _answerRepo.TableNoTracking.Where(x => x.QuestionPoolId == question.Id).ToListAsync();

            if (answerList.Count > 0)
            {
                foreach (var answerPool in answerList)
                {
                    answerPool.IsActive = false;
                    await _answerRepo.UpdateAsync(answerPool);
                }
            };

           var patAns = await _patAnswerService.GetAll();
           foreach (var patientAnswer in patAns)
           {
               if (patientAnswer.QuestionPoolId == question.Id)
               {
                   await _patAnswerService.DeleteAsync(await _patAnswerService.GetId(question.Id));
               }
           }

           var patQuestion = await _patQuestionService.GetAll();
           foreach (var patientQuestion in patQuestion)
           {
               if (patientQuestion.QuestionPoolId == question.Id)
               {
                   await _patQuestionService.DeleteAsync(await _patQuestionService.GetIdByQuestion(question.Id));
               }
           }

            return await _questionRepo.UpdateAsync(question);
        }
    }
}