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
    [SecurityAspect(PersonType.Admin)]
    
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _repository;
        private readonly IRepository<Person> _personRepo;
        private readonly SmsHelper _smsHelper;
        private readonly IUserService _userService;

        public AdminService(IRepository<Admin> repository, IUserService userService, SmsHelper smsHelper, IRepository<Person> personRepo)
        {
            _repository = repository;
            _userService = userService;
            _smsHelper = smsHelper;
            _personRepo = personRepo;
        }
        
    
        public async Task<List<GetAdminDto>> GetAllAsync()
        {
           
            return await _repository.TableNoTracking.Select(x => new GetAdminDto
            {
                PersonId = x.PersonId,
                IsActive = x.IsActive,
                UserName = x.Person.UserName,
                FirstName = x.Person.FirstName,
                LastName = x.Person.LastName,
                Gsm = x.Person.Gsm
            }).ToListAsync();
        }
        
        
        [ValidationAspect(typeof(AdminInsertValidator))]
        public async Task<DataResult<GetAdminDto>> InsertAsync(InsertAdminDto insertAdminDto)
        {
            var randomPass = RandomHelper.Mixed(6);
            HashingHelper.CreatePasswordHash(randomPass,out var passwordHash, out var passwordSalt);

            var admin = new Admin
            {
                Email = insertAdminDto.Email,
                IsActive = true,
                Person = new Person
                {
                    FirstName = insertAdminDto.FirstName,
                    LastName = insertAdminDto.LastName,
                    UserName = insertAdminDto.Email,
                    Gsm = insertAdminDto.Gsm,
                    PersonType = PersonType.Admin,
                    CreatedUserName = "",
                    CreatedAt = DateTime.Now,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RefreshToken = RandomHelper.Mixed(32)
                }
            };

            await _repository.InsertAsync(admin);
            await _smsHelper.SendAsync(new List<string> {admin.Person.Gsm},
                "Welcome to the PatientTracker System. \n You are registered to patientTracker.net as Admin by "+_userService.FullName+" \n Your password is " + randomPass+"\n Wis");
            
            var result = await _repository.TableNoTracking.Where(x => x.Id == admin.Id)
                .Include(x => x.Person)
                .FirstOrDefaultAsync();
            
            var res = new SuccessDataResult<GetAdminDto>(new GetAdminDto
            {
                PersonId = result.PersonId,
                UserName = result.Person.UserName,
                FirstName = result.Person.FirstName,
                LastName = result.Person.LastName,
                Gsm = result.Person.Gsm,
                IsActive = admin.IsActive
            });

            return res;
        }
        
        

        public async Task<Admin> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }
        
        
        public async Task<Result> UpdateAsync(int adminId,InsertAdminDto insertAdminDto)
        {
            var admin = await _repository.GetAsync(adminId);

            admin.Email = insertAdminDto.Email;

            var result = await _repository.UpdateAsync(admin);
            if (!result.Success)
            {
                return new ErrorResult();
            }
            
            var person = await _personRepo.GetAsync(x => x.Id == admin.PersonId);
           
            admin.Person.FirstName = insertAdminDto.FirstName;
            admin.Person.LastName = insertAdminDto.LastName;
            admin.Person.Gsm = insertAdminDto.Gsm;
            return await _personRepo.UpdateAsync(person);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id);

            entity.IsActive = false;
            if (entity == null) return new ErrorResult("Data not found");
            return await _repository.UpdateAsync(entity);
        }
    }
}