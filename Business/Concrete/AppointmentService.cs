using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Security;
using Core.Aspects.Validation;
using Core.Enums;
using Core.Results;
using DataAccess.Repositories;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

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
        [SecurityAspect(PersonType.Null)]

        public async Task<List<Appointment>> GetAllActivesByPatient(int patientId)
        {
            return await _repository.GetAllAsync(a => a.PatientId == patientId && a.IsActive == true);
        }
        
        public async Task<List<Appointment>> GetAllExpiredByPatient(int patientId)
        {
            return await _repository.GetAllAsync(a => a.PatientId == patientId && a.IsActive == false);
        }
        

        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<GetAppointmentDto>> GetAllByDoctor(int doctorId)
        {
            return await _repository.TableNoTracking.Where((x => x.DoctorId == doctorId))
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .Where(x => x.IsActive == true)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    DoctorName = x.Doctor.Person.FirstName + " " + x.Doctor.Person.LastName,
                    PatientName = x.Patient.Person.FirstName + " " + x.Patient.Person.LastName,
                    Date = x.Date,
                    Time = x.Time
                }).ToListAsync();
        }

        //Return appointments that belongs to specific patient and doctor
        [SecurityAspect(PersonType.Doctor)]

        public async Task<List<Appointment>> GetAllByPatientAndDoctor(int patientId, int doctorId)
        {
            return await _repository.GetAllAsync(a => a.PatientId == patientId && a.DoctorId == doctorId);
        }

        
        [SecurityAspect(PersonType.Null)]
        public async Task<Appointment> GetClosestAppointment(int patientId)
        {
            var appointmentList = await _repository.GetAllAsync(x => x.PatientId == patientId && x.IsActive == true);

            var closestAppointment = new Appointment();
            var closestDate = new DateTime(9999, 12, 25);

            foreach (var appointment in appointmentList)
            {
                if (DateTime.Compare(appointment.Date, closestDate) < 0)
                {
                    closestDate = appointment.Date;
                    closestAppointment = appointment;
                }
            }

            return closestAppointment;
        }


        public override async Task<Result> DeleteAsync(int id)
        {
            var appointment = await _repository.GetAsync(id);

            appointment.IsActive = false;

            return await _repository.UpdateAsync(appointment);
        }
        
    }
}