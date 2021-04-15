using System.Collections.Generic;
using System.Security.Authentication;
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
    [ValidationAspect(typeof(AnswerValidator))]
    public class AnswerPoolService : ServiceRepository<AnswerPool>, IAnswerPoolService
    {
        private readonly IRepository<AnswerPool> _repository;
        
        public AnswerPoolService(IRepository<AnswerPool> repository, IUserService userService) : base(repository)
        {
            _repository = repository;
            
            userService.Check(new List<PersonType> {PersonType.Doctor});
        }
        
  
        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<AnswerPool>> GetAnswerOfQuestion(int questionId)
        {
            return await _repository.GetAllAsync(x => x.QuestionPoolId == questionId);
        }
    }
}