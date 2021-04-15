using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Models;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [ValidationAspect(typeof(CountryValidator))]
    [SecurityAspect(PersonType.Admin)]
    public class CountryService : ServiceRepository<Country>, ICountryService
    {
        private readonly IRepository<Country> _repository;

        public CountryService(IRepository<Country> repository) : base(repository)
        {
            _repository = repository;
        }


        public async Task<List<Country>> GetAllAsync() => await _repository.GetAllAsync();


        [SecurityAspect(PersonType.Null)]
        public async Task<List<TreeList>> SelectListAsync()
        {
            return await _repository.TableNoTracking.Include(x => x.Cities).ThenInclude(x => x.Districts).Select(x =>
                new TreeList
                {
                    Id = x.Id,
                    Description = x.Description,
                    Sub = x.Cities.Select(y => new TreeList
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Sub = y.Districts.Select(z => new TreeList
                        {
                            Id = x.Id,
                            Description = z.Description
                        }).ToList()
                    }).ToList()
                }).ToListAsync();
        }
    }
}