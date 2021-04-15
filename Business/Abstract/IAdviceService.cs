using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAdviceService : IServiceRepository<DoctorAdvice>
    {
        Task<List<DoctorAdvice>> GetAllByDept(int deptId);
    }
}