using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAppointmentService : IServiceRepository<Appointment>
    {
        Task<List<GetAppointmentDto>> GetMyAppointments(int personId);
        Task<List<GetAppointmentDto>> GetAllActivesByPatient(int patientId);
        Task<List<Appointment>> GetAllExpiredByPatient(int patientId);

        Task<List<GetAppointmentDto>> GetAllByDoctor(int doctorId);
        Task<List<GetAppointmentDto>> GetAllExpiredByDoctor(int doctorId);

        Task<GetAppointmentDto> GetByPatientAndDoctor(int patientId,int doctorId);

        Task<GetAppointmentDto> GetClosestAppointment(int personId);
    }
}