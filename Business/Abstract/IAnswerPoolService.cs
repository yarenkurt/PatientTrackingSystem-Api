using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAnswerPoolService : IServiceRepository<AnswerPool>
    {
        Task<List<AnswerPool>> GetAnswerOfQuestion(int questionId);
    }
}