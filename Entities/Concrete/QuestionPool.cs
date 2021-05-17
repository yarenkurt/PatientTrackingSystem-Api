using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Enums;
using Core.Signatures;

namespace Entities.Concrete
{
    public class QuestionPool : IBaseEntity
    {
        public int Id { get; set; }
        
        public int DepartmentId { get; set; }
        public string Description { get; set; }
        public int UpperLimit { get; set; }
        public int LowerLimit { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public Department Department { get; set; }

        public ICollection<AnswerPool> Answers { get; set; }
    }
}