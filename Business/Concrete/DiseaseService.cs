using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Admin)]
    [ValidationAspect(typeof(DiseaseService))]
    public class DiseaseService : ServiceRepository<Disease>, IDiseaseService
    {
        private readonly IRepository<Disease> _repository;
        
        public DiseaseService(IRepository<Disease> repository) : base(repository)
        {
            _repository = repository;
        }
        
        public async Task<List<Disease>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<Disease>> GetAllAsyncByDept(int deptId)
        {
            return await _repository.GetAllAsync(d => d.DepartmentId == deptId);
        }
    }
}