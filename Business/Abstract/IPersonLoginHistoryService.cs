using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPersonLoginHistoryService : IServiceRepository<PersonLoginHistory>
    {
        Task<List<PersonLoginHistory>> GetPersonHistory(int personId);
    }
}