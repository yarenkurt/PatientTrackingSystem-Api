using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;

namespace Api.Controllers
{
    public class DegreesController : ControllerRepository<IDegreeService, Degree>
    {
        public DegreesController(IDegreeService service) : base(service)
        {
        }
    }
}