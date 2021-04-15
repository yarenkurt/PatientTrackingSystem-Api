using System;
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
using Core.Helpers;
using Core.Models;
using Core.Results;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Admin)]
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
        

        public async Task<List<GetDoctorDto>> GetAllAsync()
        {
            return await _doctorRepo.TableNoTracking
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
        

        public async Task<GetDoctorDto> GetAsync(int doctorId)
        {
            return await _doctorRepo.TableNoTracking.Where(x => x.Id == doctorId)
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
                .FirstOrDefaultAsync();
        }

        
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
            await _smsHelper.SendAsync(new List<string> {doctor.Person.Gsm},
                "Welcome to the Yeditepe Hospital \n You are registered to patientTracker.net as Doctor by "+_userService.FullName+" \n Your password is " + randomPass);


            var result = await _doctorRepo.TableNoTracking.Where(x => x.Id == doctor.Id)
                .Include(x => x.Person)
                .Include(x => x.Department)
                .Include(x => x.Degree)
                .FirstOrDefaultAsync();
            
            var res = new SuccessDataResult<GetDoctorDto>(new GetDoctorDto
            {
                Email = result.Email,
                FirstName = result.Person.FirstName,
                LastName = result.Person.LastName,
                Gsm = result.Person.Gsm,
                DepartmentName = result.Department.Description,
                DegreeName = result.Degree.Description
            });

            return res;
        }
        
        
        public async Task<Result> UpdateAsync(Doctor entity)
        {
            return await _doctorRepo.UpdateAsync(entity);
        }

        
        public async Task<Result> DeleteAsync(int id)
        {
            var doctor = await _doctorRepo.GetAsync(id);
            if (doctor != null)
            {
                return await _doctorRepo.DeleteAsync(doctor);
            }

            return null;
        }
        
        
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


       
    }
}