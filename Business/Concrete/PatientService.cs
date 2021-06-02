using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Helpers;
using Core.Results;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _repository;
        private readonly IRepository<Person> _personRepo;
        private readonly SmsHelper _smsHelper;
        private readonly IUserService _userService;
        private readonly IRepository<DoctorPatient> _doctorPatientRepo;
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IPatientAnswerService _patientAnswerService;


        public PatientService(IRepository<Patient> repository, IUserService userService, SmsHelper smsHelper,
            IRepository<DoctorPatient> doctorPatientRepo, IRepository<Doctor> doctorRepo,
            IPatientAnswerService patientAnswerService, IRepository<Person> personRepo)
        {
            _repository = repository;
            _userService = userService;
            _smsHelper = smsHelper;
            _doctorPatientRepo = doctorPatientRepo;
            _doctorRepo = doctorRepo;
            _patientAnswerService = patientAnswerService;
            _personRepo = personRepo;
        }


        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<GetPatientDto>> GetAllActivesAsync()
        {
            return await _repository.TableNoTracking
                .Where(x => x.IsActive == true)
                .Select(p => new GetPatientDto
                {
                    Id = p.Id,
                    IdentityNumber = p.IdentityNumber,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Email = p.Email,
                    Gsm = p.Person.Gsm,
                    Age = p.Age,
                    Weight = p.Weight,
                    Height = p.Height,
                    HealthScore = _patientAnswerService.GetTotalScoreOfPatient(p.Id),
                    Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList(),
                    Danger = _patientAnswerService.CountRiskyAnswers(p.Id),
                    DepartmentId = p.PatientDiseases.Select(x => x.Disease.DepartmentId).FirstOrDefault()
                }).ToListAsync();
        }

        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<GetPatientDto>> GetAllAsync()
        {
            return await _repository.TableNoTracking
                .Select(p => new GetPatientDto
                {
                    Id = p.Id,
                    IdentityNumber = p.IdentityNumber,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Email = p.Email,
                    Gsm = p.Person.Gsm,
                    Age = p.Age,
                    Weight = p.Weight,
                    Height = p.Height,
                    HealthScore = _patientAnswerService.GetTotalScoreOfPatient(p.Id),
                    Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList(),
                    Danger = _patientAnswerService.CountRiskyAnswers(p.Id),
                    DepartmentId = p.PatientDiseases.Select(x => x.Disease.DepartmentId).FirstOrDefault()
                }).ToListAsync();
        }


        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<GetPatientDto>> GetAllPassivesAsync()
        {
            return await _repository.TableNoTracking
                .Where(x => x.IsActive == false)
                .Select(p => new GetPatientDto
                {
                    Id = p.Id,
                    IdentityNumber = p.IdentityNumber,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Email = p.Email,
                    Gsm = p.Person.Gsm,
                    Age = p.Age,
                    Weight = p.Weight,
                    Height = p.Height,
                    HealthScore = _patientAnswerService.GetTotalScoreOfPatient(p.Id),
                    Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList(),
                    Danger = _patientAnswerService.CountRiskyAnswers(p.Id),
                    DepartmentId = p.PatientDiseases.Select(x => x.Disease.DepartmentId).FirstOrDefault()
                }).ToListAsync();
        }


        [SecurityAspect(PersonType.Doctor)]
        public async Task<GetPatientDto> GetAsync(int patientId)
        {
            var doctor = await _doctorRepo.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.PersonId == _userService.PersonId)
                .FirstOrDefaultAsync();

            return await _repository.TableNoTracking.Where(p => p.Id == patientId)
                .Select(p => new GetPatientDto
                {
                    Id = p.Id,
                    IdentityNumber = p.IdentityNumber,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Email = p.Email,
                    Gsm = p.Person.Gsm,
                    Age = p.Age,
                    Weight = p.Weight,
                    Height = p.Height,
                    HealthScore = _patientAnswerService.GetTotalScoreOfPatient(p.Id),
                    Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList(),
                    Danger = _patientAnswerService.CountRiskyAnswers(p.Id),
                    DepartmentId = p.PatientDiseases.Select(x => x.Disease.DepartmentId).FirstOrDefault(),
                    HospitalId = doctor.Department.HospitalId
                }).FirstOrDefaultAsync();
        }


        [SecurityAspect(PersonType.Patient)]
        public async Task<GetPatientDto> GetByPersonIdAsync(int personId)
        {
            return await _repository.TableNoTracking.Include(p => p.Person)
                .Where(p => p.PersonId == personId)
                .Select(p => new GetPatientDto
                {
                    Id = p.Id,
                    IdentityNumber = p.IdentityNumber,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Email = p.Email,
                    Gsm = p.Person.Gsm,
                    Age = p.Age,
                    Weight = p.Weight,
                    Height = p.Height,
                    HealthScore = _patientAnswerService.GetTotalScoreOfPatient(p.Id),
                    Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList(),
                    Danger = _patientAnswerService.CountRiskyAnswers(p.Id),
                    DepartmentId = p.PatientDiseases.Select(x => x.Disease.DepartmentId).FirstOrDefault(),
                }).FirstOrDefaultAsync();
        }

        public async Task<Result> SOSAsync(int personId, SOSDto sos)
        {
            var relatives = await _repository.TableNoTracking.Where(x=>x.PersonId==personId).Include(x => x.PatientRelatives).Select(x=>x.PatientRelatives).FirstOrDefaultAsync();
            var url = $"http://maps.google.com/?q={sos.Latitude},{sos.Longitude}";
            var phones = relatives.Select(x => x.Gsm).ToList();
            return await _smsHelper.SendAsync(phones, "Acil bana yardın edin. Bu konumdayım. " + url);
        }

        [ValidationAspect(typeof(PatientInsertValidator))]
        [SecurityAspect(PersonType.Doctor)]
        public async Task<DataResult<GetPatientDto>> InsertAsync(InsertPatientDto insertPatientDto)
        {
            //System will automatically create a random password with size 6.
            var randomPass = RandomHelper.Mixed(6);
            HashingHelper.CreatePasswordHash(randomPass, out var passwordHash, out var passwordSalt);

            var patient = new Patient
            {
                IdentityNumber = insertPatientDto.IdentityNumber,
                Email = insertPatientDto.Email,
                PatientDiseases = new List<PatientDisease>(),
                IsActive = true,
                Age = insertPatientDto.Age,
                Weight = insertPatientDto.Weight,
                Height = insertPatientDto.Height,

                Person = new Person
                {
                    FirstName = insertPatientDto.FirstName,
                    LastName = insertPatientDto.LastName,
                    UserName = insertPatientDto.IdentityNumber,
                    Gsm = insertPatientDto.Gsm,
                    PersonType = PersonType.Patient,
                    CreatedAt = DateTime.Now,
                    CreatedUserName = "",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RefreshToken = RandomHelper.Mixed(32)
                },
            };

            var doctor = await _doctorRepo.TableNoTracking.Where(x => x.PersonId == _userService.PersonId)
                .FirstOrDefaultAsync();

            await _repository.InsertAsync(patient);
            await _doctorPatientRepo.InsertAsync(new DoctorPatient
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id
            });

            await _smsHelper.SendAsync(new List<string> {patient.Person.Gsm},
                "Welcome to the YEDITEPE HOSPITAL \nYou are registered to patientTracker.net as Patient by " +
                _userService.FullName + " \nLogin to the system with your ID \nYour password is " + randomPass);


            var result = await _repository.TableNoTracking.Where(x => x.PersonId == patient.Person.Id)
                .Include(x => x.Person)
                .FirstOrDefaultAsync();

            return new SuccessDataResult<GetPatientDto>(new GetPatientDto
            {
                IdentityNumber = result.IdentityNumber,
                FirstName = result.Person.FirstName,
                LastName = result.Person.LastName,
                Email = result.Email,
                Gsm = result.Person.Gsm,
                Age = result.Age,
                Weight = result.Weight,
                Height = result.Height,
                HealthScore = _patientAnswerService.GetTotalScoreOfPatient(result.Id),
                Diseases = new List<string>()
            });
        }


        [SecurityAspect(PersonType.Doctor)]
        public async Task<Result> UpdateAsync(int patientId, InsertPatientDto insertPatientDto)
        {
            var patient = await _repository.TableNoTracking
                .Where(x => x.Id == patientId)
                .FirstOrDefaultAsync();


            patient.Email = insertPatientDto.Email;
            patient.IdentityNumber = insertPatientDto.IdentityNumber;

            var result = await _repository.UpdateAsync(patient);
            if (!result.Success)
            {
                return new ErrorResult();
            }

            var person = await _personRepo.GetAsync(x => x.Id == patient.PersonId);
            person.FirstName = insertPatientDto.FirstName;
            person.LastName = insertPatientDto.LastName;
            person.Gsm = insertPatientDto.Gsm;

            return await _personRepo.UpdateAsync(person);
        }


        [SecurityAspect(PersonType.Doctor)]
        public async Task<Result> DeleteAsync(int id)
        {
            var patient = await _repository.TableNoTracking
                .Where(x => x.Id == id)
                .Include(x => x.Person).FirstOrDefaultAsync();
            patient.IsActive = false;

            return await _repository.UpdateAsync(patient);
        }
    }
}