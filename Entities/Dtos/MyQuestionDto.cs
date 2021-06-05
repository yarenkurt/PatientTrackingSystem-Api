using System.Collections.Generic;
using Core.Enums;

namespace Entities.Dtos
{
    public class QuestionsDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    
    public class QuestionDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public int UpperLimit { get; set; }
        public int LowerLimit { get; set; }
        public string QuestionType { get; set; }
        public List<AnswerPoolDto> AnswerPools { get; set; }
        public class AnswerPoolDto
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public decimal Score { get; set; }
        }
    }
}