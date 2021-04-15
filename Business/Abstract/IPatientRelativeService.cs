using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPatientRelativeService : IServiceRepository<PatientRelative>
    {
        // Get all Relatives of specific patient
        Task<List<PatientRelative>> GetAllAsync(int patientId);
        
        
    }
}