using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAppointmentService : IServiceRepository<Appointment>
    {
        Task<List<Appointment>> GetAllActivesByPatient(int patientId);
        Task<List<Appointment>> GetAllExpiredByPatient(int patientId);

        Task<List<GetAppointmentDto>> GetAllByDoctor(int doctorId);
        Task<List<Appointment>> GetAllByPatientAndDoctor(int patientId,int doctorId);


    }
}