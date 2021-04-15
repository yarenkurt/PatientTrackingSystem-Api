using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAppointmentService : IServiceRepository<Appointment>
    {
        Task<List<Appointment>> GetAllByPatient(int patientId);
        Task<List<Appointment>> GetAllByDoctor(int doctorId);
        Task<List<Appointment>> GetAllByPatientAndDoctor(int patientId,int doctorId);

    }
}