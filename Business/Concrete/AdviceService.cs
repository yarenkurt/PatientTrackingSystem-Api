using System.Collections.Generic;
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

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    
    public class AdviceService : ServiceRepository<DoctorAdvice>, IAdviceService
    {
        private readonly IRepository<DoctorAdvice> _repository;
        private readonly IUserService _userService;
        private readonly IRepository<Doctor> _doctorRepo;


        public AdviceService(IRepository<DoctorAdvice> repository, IUserService userService, IRepository<Doctor> doctorRepo) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _doctorRepo = doctorRepo;
        }

     
        [DoctorSecurityAspect]
        public async Task<List<DoctorAdvice>> GetAllByDept(int deptId)
        {
            return await _repository.GetAllAsync(a => a.DepartmentId == deptId);
        }

        [ValidationAspect(typeof(AdviceValidator))]
        public override async Task<DataResult<DoctorAdvice>> InsertAsync(DoctorAdvice entity)
        {
            var doctor = await _doctorRepo.GetAsync(x => x.PersonId == _userService.PersonId);
            entity.DepartmentId = doctor.DepartmentId;
            
            return await base.InsertAsync(entity);
        }
    }
}