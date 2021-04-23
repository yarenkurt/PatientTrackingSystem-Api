using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
        private readonly SmsHelper _smsHelper;
        private readonly IUserService _userService;

        public AdminService(IRepository<Admin> repository, IUserService userService, SmsHelper smsHelper)
        {
            _repository = repository;
            _userService = userService;
            _smsHelper = smsHelper;
        }
        
    
        public async Task<List<GetAdminDto>> GetAllAsync()
        {
           
            return await _repository.TableNoTracking.Select(x => new GetAdminDto
            {
                PersonId = x.PersonId,
                IsBlocked = x.IsBlocked,
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
                IsBlocked = false,
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
                IsBlocked = admin.IsBlocked
            });

            return res;
        }
        
        

        public async Task<Admin> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public Task<Result> UpdateAsync(Admin entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}