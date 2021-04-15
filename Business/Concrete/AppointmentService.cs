using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Token;
using DataAccess.Repositories;
using Entities.Concrete;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(AppointmentValidator))]
    public class AppointmentService : ServiceRepository<Appointment>, IAppointmentService
    {
        private readonly IRepository<Appointment> _repository;
        
        public AppointmentService(IRepository<Appointment> repository) : base(repository)
        {
            _repository = repository;
        }
        
        
        //Return appointments that belongs to specific patient
        [SecurityAspect(PersonType.Patient)]

        public async Task<List<Appointment>> GetAllByPatient(int patientId)
        {
            return await _repository.GetAllAsync(a => a.PatientId == patientId);
        }

        //Return appointments that belongs to specific doctor
        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<Appointment>> GetAllByDoctor(int doctorId)
        {
            return await _repository.GetAllAsync(a => a.DoctorId == doctorId);
        }

        //Return appointments that belongs to specific patient and doctor
        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<Appointment>> GetAllByPatientAndDoctor(int patientId, int doctorId)
        {
            return await _repository.GetAllAsync(a => a.PatientId == patientId && a.DoctorId == doctorId);
        }
    }
}