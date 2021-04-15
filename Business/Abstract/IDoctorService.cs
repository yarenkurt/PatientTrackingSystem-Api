using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDoctorService 
    {
        Task<List<GetDoctorDto>> GetAllAsync();
        Task<GetDoctorDto> GetAsync(int id);
        Task<DataResult<GetDoctorDto>> InsertAsync(InsertDoctorDto insertAdminDto);
        Task<Result> UpdateAsync(Doctor entity);
        Task<Result> DeleteAsync(int id);
        Task<List<GetDoctorDto>> GetAllByDeptAsync(int deptId);
        Task<List<GetDoctorDto>> GetAllByDegreeAsync(int degreeId);



    }
}