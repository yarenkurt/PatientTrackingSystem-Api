﻿using System.Collections.Generic;
using System.Linq;
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
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [SecurityAspect(PersonType.Doctor)]
    [ValidationAspect(typeof(DoctorPatientValidator))]
    public class DoctorPatientService : ServiceRepository<DoctorPatient>,IDoctorPatientService
    {
        private readonly IRepository<DoctorPatient> _repository;
        
        public DoctorPatientService(IRepository<DoctorPatient> repository,IUserService userService) : base(repository)
        {
            _repository = repository;
            userService.Check(new List<PersonType> {PersonType.Admin,PersonType.Doctor});
        }

        public async Task<int> CountPatientOfDoctor(int doctorId)
        {
            return await _repository.TableNoTracking.CountAsync(x => x.DoctorId == doctorId);
        }

        [SecurityAspect(PersonType.Doctor)]
        public async Task<List<GetPatientDto>> GetPatientListOfDoctor(int doctorId)
        {
            return await _repository.TableNoTracking.Where(x => x.DoctorId == doctorId)
                .Include(x => x.Patient)
                .Select(x => new GetPatientDto
                {
                    IdentityNumber = x.Patient.IdentityNumber,
                    Email = x.Patient.Email,
                    FirstName = x.Patient.Person.FirstName,
                    LastName = x.Patient.Person.LastName,
                    Gsm = x.Patient.Person.Gsm
                }).ToListAsync();
        }

        [SecurityAspect(PersonType.Patient)]
        public async Task<List<GetDoctorDto>> GetDoctorListOfPatient(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .Include(x => x.Doctor)
                .Select(x => new GetDoctorDto
                {
                    Email = x.Doctor.Email,
                    FirstName = x.Doctor.Person.FirstName,
                    LastName = x.Doctor.Person.LastName,
                    Gsm = x.Doctor.Person.Gsm,
                    DepartmentName = x.Doctor.Department.Description,
                    DegreeName = x.Doctor.Degree.Description
                }).ToListAsync();
        }
    }
}