using System.Text.Json.Serialization;
using Core.Enums;

namespace Web.Models
{
    public class QuestionPool
    {
        public int Id { get; set; }
    
        public string Description { get; set; }
        public int UpperLimit { get; set; }
        public int LowerLimit { get; set; }
        public QuestionType QuestionType { get; set; }

    }
}