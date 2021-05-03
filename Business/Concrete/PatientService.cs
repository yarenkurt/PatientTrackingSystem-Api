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
    [SecurityAspect(PersonType.Doctor)]
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _repository;
        private readonly SmsHelper _smsHelper;
        private readonly IUserService _userService;
        private readonly IRepository<DoctorPatient> _doctorPatientRepo;
        private readonly IRepository<Doctor> _doctorRepo;


        public PatientService(IRepository<Patient> repository,IUserService userService, SmsHelper smsHelper, IRepository<DoctorPatient> doctorPatientRepo, IRepository<Doctor> doctorRepo)
        {
            _repository = repository;
            _userService = userService;
            _smsHelper = smsHelper;
            _doctorPatientRepo = doctorPatientRepo;
            _doctorRepo = doctorRepo;
        }


        public async Task<List<GetPatientDto>> GetAllAsync()
        {
            return await _repository.TableNoTracking
                .Select(p => new GetPatientDto
            {
                IdentityNumber = p.IdentityNumber,
                FirstName = p.Person.FirstName,
                LastName = p.Person.LastName,
                Email = p.Email,
                Gsm = p.Person.Gsm,
                Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList()

            }).ToListAsync();
        }

        
        public async Task<GetPatientDto> GetAsync(int patientId)
        {
            return await _repository.TableNoTracking.Where(p => p.Id == patientId)
                .Select(p => new GetPatientDto
                {
                    IdentityNumber = p.IdentityNumber,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Email = p.Email,
                    Gsm = p.Person.Gsm,
                    Diseases = p.PatientDiseases.Select(x => x.Disease.Description).ToList()

                }).FirstOrDefaultAsync();
        }

        
        [ValidationAspect(typeof(PatientInsertValidator))]
        public async Task<DataResult<GetPatientDto>> InsertAsync(InsertPatientDto insertPatientDto)
        {
            //System will automatically create a random password with size 6.
            var randomPass = RandomHelper.Mixed(6);
            HashingHelper.CreatePasswordHash(randomPass,out var passwordHash, out var passwordSalt);

            var patient = new Patient
            {
                IdentityNumber = insertPatientDto.IdentityNumber,
                Email = insertPatientDto.Email,
                PatientDiseases = new List<PatientDisease>(),
                
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
                "Welcome to the YEDITEPE HOSPITAL \nYou are registered to patientTracker.net as Patient by "+_userService.FullName+" \nLogin to the system with your ID \nYour password is " + randomPass);

            
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
                Diseases = new List<string>()
            });
            
        }

        
        public async Task<Result> UpdateAsync(Patient entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        
        public async Task<Result> DeleteAsync(int id)
        {
            var patient = await _repository.GetAsync(id);
            if (patient != null)
            {
                return await _repository.DeleteAsync(patient);
            }

            return null;
        }
        
        
    }
}