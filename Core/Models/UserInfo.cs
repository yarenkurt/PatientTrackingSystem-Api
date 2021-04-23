using Core.Enums;

namespace Core.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public PersonType PersonType { get; set; }  
    }
}