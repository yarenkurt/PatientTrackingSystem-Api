using Core.Signatures;

namespace Entities.Concrete
{
    public class RelativeDegree : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}