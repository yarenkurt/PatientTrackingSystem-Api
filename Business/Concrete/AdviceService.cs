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
    public class AdviceService : ServiceRepository<DoctorAdvice>, IAdviceService
    {
        private readonly IRepository<DoctorAdvice> _repository;
        private readonly IUserService _userService;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<Patient> _patientRepo;


        public AdviceService(IRepository<DoctorAdvice> repository, IUserService userService,
            IRepository<Doctor> doctorRepo, IRepository<Patient> patientRepo) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }


        [SecurityAspect(PersonType.Null)]
        public async Task<List<DoctorAdvice>> GetAllByDept(int deptId)
        {
            return await _repository.GetAllAsync(a => a.DepartmentId == deptId);
        }

        public async Task<List<DoctorAdvice>> GetMyAdvices(int personId)
        {
            var deptId =await _patientRepo.TableNoTracking.Where(x => x.PersonId == personId)
                .Include(x => x.PatientDiseases).ThenInclude(x => x.Disease)
                .Select(x => x.PatientDiseases.FirstOrDefault().Disease.DepartmentId)
                .FirstOrDefaultAsync();
            return await _repository.GetAllAsync(a => a.DepartmentId == deptId);
        }

        [ValidationAspect(typeof(AdviceValidator))]
        [SecurityAspect(PersonType.Doctor)]
        public override async Task<DataResult<DoctorAdvice>> InsertAsync(DoctorAdvice entity)
        {
            var doctor = await _doctorRepo.GetAsync(x => x.PersonId == _userService.PersonId);
            entity.DepartmentId = doctor.DepartmentId;
            entity.CreatedUserName = _userService.FullName;

            return await base.InsertAsync(entity);
        }

        [SecurityAspect(PersonType.Doctor)]
        public override Task<Result> UpdateAsync(DoctorAdvice entity)
        {
            entity.CreatedUserName = _userService.FullName;

            return base.UpdateAsync(entity);
        }
    }
}