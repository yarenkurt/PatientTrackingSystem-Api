using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAnswerPoolService : IServiceRepository<AnswerPool>
    {
        Task<List<AnswerPool>> GetAnswerOfQuestion(int questionId);
    }
}