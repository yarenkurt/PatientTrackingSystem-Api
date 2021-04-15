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
    [SecurityAspect(PersonType.Admin)]
    [ValidationAspect(typeof(DegreeValidator))]
    public class DegreeService : ServiceRepository<Degree>, IDegreeService
    {
        private readonly IRepository<Degree> _repository;
        
        public DegreeService(IRepository<Degree> repository, IUserService userService) : base(repository)
        {
            _repository = repository;
            
            userService.Check(new List<PersonType> {PersonType.Admin});
        }
        
 
        public async Task<List<Degree>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
    }
}