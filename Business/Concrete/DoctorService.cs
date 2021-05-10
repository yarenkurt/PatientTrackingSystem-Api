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
   
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor> _doctorRepo;
        private readonly IRepository<QuestionPool> _questionRepo;
        private readonly SmsHelper _smsHelper;
        private readonly IUserService _userService;
        
        public DoctorService(IRepository<Doctor> repository, IRepository<QuestionPool> questionRepo, IUserService userService, SmsHelper smsHelper)
        {
            _doctorRepo = repository;
            _questionRepo = questionRepo;
            _userService = userService;
            _smsHelper = smsHelper;
        }
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<List<GetDoctorDto>> GetAllAsync()
        {
            return await _doctorRepo.TableNoTracking
                .Include(x => x.Department)
                .Include(x => x.Degree)
                .Select(x => new GetDoctorDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Gsm = x.Person.Gsm,
                    DepartmentName = x.Department.Description,
                    DegreeName = x.Degree.Description
                })
                .ToListAsync();
        }
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<GetDoctorDto> GetAsync(int doctorId)
        {
            return await _doctorRepo.TableNoTracking.Where(x => x.Id == doctorId)
                .Include(x => x.Department)
                .Include(x => x.Degree)
                .Select(x => new GetDoctorDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Gsm = x.Person.Gsm,
                    DepartmentName = x.Department.Description,
                    DegreeName = x.Degree.Description
                })
                .FirstOrDefaultAsync();
        }

        [SecurityAspect(PersonType.Admin)]
        [ValidationAspect(typeof(DoctorInsertValidator))]
        public async Task<DataResult<GetDoctorDto>> InsertAsync(InsertDoctorDto insertDoctorDto)
        {
            var randomPass = RandomHelper.Mixed(6);

            HashingHelper.CreatePasswordHash(randomPass,out var passwordHash, out var passwordSalt);

            var doctor = new Doctor
            {
                Email = insertDoctorDto.Email,
                IsBlocked = false,
                DepartmentId = insertDoctorDto.DepartmentId,
                DegreeId = insertDoctorDto.DegreeId,
                Person = new Person
                {
                    FirstName = insertDoctorDto.FirstName,
                    LastName = insertDoctorDto.LastName,
                    Gsm = insertDoctorDto.Gsm,
                    CreatedAt = DateTime.Now,
                    CreatedUserName = "",
                    PersonType = PersonType.Doctor,
                    UserName = insertDoctorDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RefreshToken = RandomHelper.Mixed(32),
                    RefreshTokenExpiredDate = DateTime.Now.AddDays(-1)
                }
            };

            await _doctorRepo.InsertAsync(doctor);
            var hospital =  _doctorRepo.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.PersonId == doctor.PersonId)
                .Select(x => x.Department.Hospital.Description)
                .FirstOrDefaultAsync();
                
                
            await _smsHelper.SendAsync(new List<string> {doctor.Person.Gsm},
                "Welcome to the "+ hospital + " Hospital \n You are registered to patientTracker.net as Doctor by "+_userService.FullName+" \n Your password is " + randomPass);


            var result = await _doctorRepo.TableNoTracking.Where(x => x.PersonId == doctor.PersonId)
                .Include(x => x.Person)
                .Include(x => x.Department)
                .Include(x => x.Degree)
                .FirstOrDefaultAsync();
            
            var res = new SuccessDataResult<GetDoctorDto>(new GetDoctorDto
            {
                Id = result.Id,
                Email = result.Email,
                FirstName = result.Person.FirstName,
                LastName = result.Person.LastName,
                Gsm = result.Person.Gsm,
                DepartmentName = result.Department.Description,
                DegreeName = result.Degree.Description
            });

            return res;
        }
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<Result> UpdateAsync( int doctorId,  InsertDoctorDto insertDoctorDto)
        {
            var doctor = await _doctorRepo.GetAsync(doctorId);

            doctor.Person.FirstName = insertDoctorDto.FirstName;
            doctor.Person.LastName = insertDoctorDto.LastName;
            doctor.Person.Gsm = insertDoctorDto.Gsm;
            doctor.Email = insertDoctorDto.Email;
            doctor.DegreeId = insertDoctorDto.DegreeId;
            doctor.DepartmentId = insertDoctorDto.DepartmentId;
            
            return await _doctorRepo.UpdateAsync(doctor);
        }

        
        [SecurityAspect(PersonType.Admin)]
        public async Task<Result> DeleteAsync(int id)
        {
            var doctor = await _doctorRepo.GetAsync(id);
            if (doctor != null)
            {
                return await _doctorRepo.DeleteAsync(doctor);
            }

            return null;
        }
        
        
        [SecurityAspect(PersonType.Admin)]
        public async Task<List<GetDoctorDto>> GetAllByDeptAsync(int deptId)
        {
            return await _doctorRepo.TableNoTracking.Where(x => x.DepartmentId == deptId)
                .Include(x => x.Department)
                .Include(x => x.Degree)
                .Select(x => new GetDoctorDto
                {
                    Email = x.Email,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Gsm = x.Person.Gsm,
                    DepartmentName = x.Department.Description,
                    DegreeName = x.Degree.Description
                })
                .ToListAsync();
        }

        
        [SecurityAspect(PersonType.Admin)]
        public async Task<List<GetDoctorDto>> GetAllByDegreeAsync(int degreeId)
        {
            return await _doctorRepo.TableNoTracking.Where(x => x.DegreeId == degreeId)
                .Include(x => x.Department)
                .Include(x => x.Degree)
                .Select(x => new GetDoctorDto
                {
                    Email = x.Email,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Gsm = x.Person.Gsm,
                    DepartmentName = x.Department.Description,
                    DegreeName = x.Degree.Description
                })
                .ToListAsync();
        }

        
        [SecurityAspect(PersonType.Admin)]
        public async Task<int> CountAsync(int hospitalId)
        {
            return await _doctorRepo.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.Department.HospitalId == hospitalId)
                .CountAsync();
        }

        
        [SecurityAspect(PersonType.Admin)]
        public async Task<List<GetDoctorDto>> GetAllByHospitalAsync(int hospitalId)
        {
            return await _doctorRepo.TableNoTracking
                .Include(x => x.Department)
                .Where(x => x.Department.HospitalId == hospitalId)
                .Select(x => new GetDoctorDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Gsm = x.Person.Gsm,
                    DepartmentName = x.Department.Description,
                    DegreeName = x.Degree.Description
                })
                .ToListAsync();
        }

        
        [SecurityAspect(PersonType.Doctor)]
        public async Task<Doctor> GetByPersonIdAsync(int personId)
        {
            return await _doctorRepo.TableNoTracking
                                    .Include(x => x.Degree)
                                    .Include((x => x.Department))
                                    .Where(x => x.PersonId == personId)
                                    .FirstOrDefaultAsync();
        }
    }
}