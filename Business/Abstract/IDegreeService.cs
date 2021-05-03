using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDegreeService : IServiceRepository<Degree>
    {
        Task<List<Degree>> GetAll();
        Task<int> CountAsync();
    }
}