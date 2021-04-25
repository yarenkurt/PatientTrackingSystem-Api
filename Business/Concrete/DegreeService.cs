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
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Admin)]
    [ValidationAspect(typeof(DegreeValidator))]
    public class DegreeService : ServiceRepository<Degree>, IDegreeService
    {
        private readonly IRepository<Degree> _repository;
        
        public DegreeService(IRepository<Degree> repository) : base(repository)
        {
            _repository = repository;
        }
        
 
        public async Task<List<Degree>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
    }
}