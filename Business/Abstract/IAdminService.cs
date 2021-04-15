using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAdminService
    {

        Task<List<GetAdminDto>> GetAllAsync();
        Task<Admin> GetAsync(int id);
        Task<DataResult<GetAdminDto>> InsertAsync(InsertAdminDto insertAdminDto);
        Task<Result> UpdateAsync(Admin entity);
        Task<Result> DeleteAsync(int id);
        
    }
}