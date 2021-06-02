using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPatientService 
    {
       Task<List<GetPatientDto>> GetAllActivesAsync();
       Task<List<GetPatientDto>> GetAllPassivesAsync();
       Task<List<GetPatientDto>> GetAllAsync();

        Task<GetPatientDto> GetAsync(int id);
        Task<DataResult<GetPatientDto>> InsertAsync(InsertPatientDto insertPatientDto);
        Task<Result> UpdateAsync(int patientId,InsertPatientDto entity);
        Task<Result> DeleteAsync(int id);
        Task<GetPatientDto> GetByPersonIdAsync(int personId);
        
        Task<Result> SOSAsync(int personId,SOSDto sos);

    }
}