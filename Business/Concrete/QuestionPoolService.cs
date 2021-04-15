using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
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
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(QuestionValidator))]
    public class QuestionPoolService : ServiceRepository<QuestionPool>, IQuestionPoolService
    {
        private readonly IRepository<QuestionPool> _questionRepo;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IUserService _userService;
        
        
        public QuestionPoolService(IRepository<QuestionPool> repository, IRepository<Doctor> doctorRepo, IUserService userService) : base(repository)
        {
            _doctorRepo = doctorRepo;
            _userService = userService;
            _questionRepo = repository;
        }
        

        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<QuestionPool>> GetAllByDept(int deptId)
        {
            return await _questionRepo.GetAllAsync(x => x.DepartmentId == deptId);
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
            return await base.InsertAsync(entity);
        }
    }
}