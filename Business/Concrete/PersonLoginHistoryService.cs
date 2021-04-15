﻿using System.Collections.Generic;
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
    [SecurityAspect(PersonType.Null)]
    [ValidationAspect(typeof(LoginHistoryValidator))]
    public class PersonLoginHistoryService : ServiceRepository<PersonLoginHistory>, IPersonLoginHistoryService
    {
        private readonly IRepository<PersonLoginHistory> _repository;
        
        public PersonLoginHistoryService(IRepository<PersonLoginHistory> repository,IUserService userService) : base(repository)
        {
            _repository = repository;
            userService.Check(new List<PersonType> {PersonType.Admin,PersonType.Doctor});
        }

        public async Task<List<PersonLoginHistory>> GetPersonHistory(int personId)
        {
            return await _repository.TableNoTracking
                .Where(x => x.PersonId == personId)
                .OrderByDescending(x => x.Date)
                .Take(10).ToListAsync();
        }
    }
}