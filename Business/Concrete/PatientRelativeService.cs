using System.Collections.Generic;
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
    [SecurityAspect(PersonType.Patient)]
    [ValidationAspect(typeof(RelativeValidator))]
    public class PatientRelativeService : ServiceRepository<PatientRelative>, IPatientRelativeService
    {
        private readonly IRepository<PatientRelative> _repository;
        
        public PatientRelativeService(IRepository<PatientRelative> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<PatientRelative>> GetAllAsync(int patientId)
        {
            return await _repository.GetAllAsync(x => x.PatientId == patientId);
        }

     
    }
}