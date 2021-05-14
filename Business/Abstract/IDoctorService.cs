using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IDoctorService 
    {
        Task<List<GetDoctorDto>> GetAllAsync();
        Task<UpdateDoctorDto> GetAsync(int id);
        Task<DataResult<GetDoctorDto>> InsertAsync(InsertDoctorDto insertAdminDto);
        Task<Result> UpdateAsync( int doctorId,  InsertDoctorDto doctorDto);
        Task<Result> DeleteAsync(int id);
        Task<List<GetDoctorDto>> GetAllByDeptAsync(int deptId);
        Task<List<GetDoctorDto>> GetAllByDegreeAsync(int degreeId);

        Task<int> CountAsync(int hospitalId);
        Task<List<GetDoctorDto>> GetAllByHospitalAsync(int hospitalId);
        Task<Doctor> GetByPersonIdAsync(int personId);



    }
}