using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Concrete;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPersonLoginHistoryService : IServiceRepository<PersonLoginHistory>
    {
        Task<List<PersonLoginHistory>> GetPersonHistory(int personId);
    }
}