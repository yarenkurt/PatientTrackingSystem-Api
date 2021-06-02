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
        public async Task<List<GetAppointmentDto>> GetMyAppointments(int personId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Patient).ThenInclude(x => x.Person)
                .Include(x => x.Doctor).ThenInclude(x => x.Person)
                .Include(x => x.Doctor).ThenInclude(x => x.Department)
                .Where(x => x.Patient.PersonId == personId)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    PatientName = $"{x.Patient.Person.FirstName} {x.Patient.Person.LastName}",
                    DoctorName = $"{x.Doctor.Person.FirstName} {x.Doctor.Person.LastName}",
                    DepartmentName = x.Doctor.Department.Description,
                    IsExpired = x.IsActive && x.Date < DateTime.Now
                })
                .OrderBy(x => x.IsExpired ? 1 : 0).ThenBy(x => x.Date)
                .ToListAsync();
        }

        [SecurityAspect(PersonType.Null)]
        public async Task<List<GetAppointmentDto>> GetAllActivesByPatient(int patientId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .Where(x => x.PatientId == patientId && x.IsActive == true)
                .OrderByDescending(x => x.Date)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    DoctorName = x.Doctor.Person.FirstName + " " + x.Doctor.Person.LastName,
                    PatientName = x.Patient.Person.FirstName + " " + x.Patient.Person.LastName,
                    Date = x.Date,
                    DepartmentName = x.Doctor.Department.Description
                }).ToListAsync();
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
                .OrderBy(x => x.Date)
                .Where(x => x.IsActive == true)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    DoctorName = x.Doctor.Person.FirstName + " " + x.Doctor.Person.LastName,
                    PatientName = x.Patient.Person.FirstName + " " + x.Patient.Person.LastName,
                    Date = x.Date,
                }).ToListAsync();
        }

        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<GetAppointmentDto>> GetAllExpiredByDoctor(int doctorId)
        {
            return await _repository.TableNoTracking.Where((x => x.DoctorId == doctorId))
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .OrderBy(x => x.Date)
                .Where(x => x.IsActive == false)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    DoctorName = x.Doctor.Person.FirstName + " " + x.Doctor.Person.LastName,
                    PatientName = x.Patient.Person.FirstName + " " + x.Patient.Person.LastName,
                    Date = x.Date,
                }).ToListAsync();
        }

        //Return appointments that belongs to specific patient and doctor
        [SecurityAspect(PersonType.Doctor)]
        public async Task<GetAppointmentDto> GetByPatientAndDoctor(int patientId, int doctorId)
        {
            return await _repository.TableNoTracking.Where(x => x.DoctorId == doctorId && x.PatientId == patientId)
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .Where(x => x.IsActive == true)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    DoctorName = x.Doctor.Person.FirstName + " " + x.Doctor.Person.LastName,
                    PatientName = x.Patient.Person.FirstName + " " + x.Patient.Person.LastName,
                    Date = x.Date,
                    DepartmentName = x.Doctor.Department.Description
                }).FirstOrDefaultAsync();
        }


        [SecurityAspect(PersonType.Patient)]
        public async Task<GetAppointmentDto> GetClosestAppointment(int personId)
        {
            return await _repository.TableNoTracking
                .Include(x => x.Doctor).ThenInclude(x => x.Person)
                .Include(x => x.Doctor).ThenInclude(x => x.Department)
                .Include(x => x.Patient).ThenInclude(x => x.Person)
                .Where(x => x.Patient.PersonId == personId && x.IsActive && x.Date >= DateTime.Now.Date)
                .Select(x => new GetAppointmentDto
                {
                    Id = x.Id,
                    DoctorName = $"{x.Doctor.Person.FirstName} {x.Doctor.Person.LastName}",
                    PatientName = $"{x.Patient.Person.FirstName} {x.Patient.Person.LastName}",
                    Date = x.Date,
                    DepartmentName = x.Doctor.Department.Description
                })
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();
        }


        [SecurityAspect(PersonType.Doctor)]
        public override async Task<Result> DeleteAsync(int id)
        {
            var appointment = await _repository.GetAsync(id);

            appointment.IsActive = false;

            return await _repository.UpdateAsync(appointment);
        }


        [SecurityAspect(PersonType.Doctor)]
        public override async Task<DataResult<Appointment>> InsertAsync(Appointment entity)
        {
            entity.CreatedAt = DateTime.Now;
            return await base.InsertAsync(entity);
        }
    }
}