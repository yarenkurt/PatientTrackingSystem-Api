using Core.Signatures;

namespace Entities.Concrete
{
    public class Degree : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}