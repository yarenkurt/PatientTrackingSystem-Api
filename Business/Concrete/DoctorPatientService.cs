using System.Collections.Generic;
using System.Linq;
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
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(DoctorPatientValidator))]
    public class DoctorPatientService : ServiceRepository<DoctorPatient>,IDoctorPatientService
    {
        private readonly IRepository<DoctorPatient> _repository;
        private readonly IUserService _userService;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IPatientAnswerService _patientAnswerService;
        private readonly IRepository<PatientAnswer> _answerRepo;
        
        public DoctorPatientService(IRepository<DoctorPatient> repository, IUserService userService, IRepository<Doctor> doctorRepo, IPatientAnswerService patientAnswerService, IRepository<PatientAnswer> answerRepo) : base(repository)
        {
            _repository = repository;
            _userService = userService;
            _doctorRepo = doctorRepo;
            _patientAnswerService = patientAnswerService;
            _answerRepo = answerRepo;
        }

        public async Task<int> CountPatientOfDoctor(int doctorId)
        {
            return await _repository.TableNoTracking.CountAsync(x => x.DoctorId == doctorId);
        }

        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<GetPatientDto>> GetPatientListOfDoctor(int doctorId)
        {
            var doctor = await _doctorRepo.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.PersonId == doctorId)
                .FirstOrDefaultAsync();
            
            
            return await _repository.TableNoTracking.Where(x => x.DoctorId == doctor.Id)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.Doctor.Department)
                .Select(x => new GetPatientDto
                {
                    Id = x.Patient.Id,
                    IdentityNumber = x.Patient.IdentityNumber,
                    Email = x.Patient.Email,
                    FirstName = x.Patient.Person.FirstName,
                    LastName = x.Patient.Person.LastName,
                    Gsm = x.Patient.Person.Gsm,
                    HealthScore =  _patientAnswerService.GetTotalScoreOfPatient(x.Patient.Id),
                    Danger =  _patientAnswerService.CountRiskyAnswers(x.Patient.Id),
                    DepartmentId = x.Doctor.DepartmentId,
                    HospitalId = x.Doctor.Department.HospitalId
                    
                }).ToListAsync();
        }

        [SecurityAspect(PersonType.Patient)]
        public async Task<List<GetDoctorDto>> GetDoctorListOfPatient(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Include(x => x.Doctor)
                .Select(x => new GetDoctorDto
                {
                    Email = x.Doctor.Email,
                    FirstName = x.Doctor.Person.FirstName,
                    LastName = x.Doctor.Person.LastName,
                    Gsm = x.Doctor.Person.Gsm,
                    DepartmentName = x.Doctor.Department.Description,
                    DegreeName = x.Doctor.Degree.Description
                }).ToListAsync();
        }
    }
}