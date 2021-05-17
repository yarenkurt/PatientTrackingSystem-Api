using Core.Signatures;

namespace Entities.Concrete
{
    public class Admin : IBaseEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        
        public Person Person { get; set; }
    }
}