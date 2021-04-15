using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Repositories;
using Business.Validations;
using Core.Aspects.Validation;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    [ValidationAspect(typeof(PatientAnswerValidator))]
    public class PatientAnswerService : ServiceRepository<PatientAnswer>, IPatientAnswerService
    {
        private readonly IRepository<PatientAnswer> _repository;
        
        public PatientAnswerService(IRepository<PatientAnswer> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<PatientAnswer>> GetAllAnswers(int patientId)
        {
            return await _repository.GetAllAsync(x => x.PatientId == patientId);
        }

        
        public async Task<decimal> GetTotalScoreOfPatient(int patientId)
        {
            return await _repository.TableNoTracking.Where(x => x.PatientId == patientId)
                .SumAsync(x => x.Score);
        }
    }
}