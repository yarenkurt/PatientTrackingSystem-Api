using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repositories;
using Core.Enums;
using Core.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IQuestionPoolService : IServiceRepository<QuestionPool>
    {
        Task<List<QuestionPool>> GetAllByDept(int deptId);
        
        //Get only multiple choice questions
        Task<List<QuestionPool>> GetOnlyMCQ();
        
        //Get only numeric type questions
        Task<List<QuestionPool>> GetOnlyNumQ();
     
        
    }
}