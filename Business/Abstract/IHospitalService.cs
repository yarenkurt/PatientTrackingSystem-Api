using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IHospitalService : IServiceRepository<Hospital>
    {
        //Task<List<Hospital>> GetAllAsync(int districtId);
        
        Task<List<GetHospitalDto>> GetAllAsync();
        Task<int> CountAsync();
        Task<GetHospitalDto> GetHospDto(int id);

    }
}