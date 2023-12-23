using InterseptorSample.Models;
using InterseptorSample.ServiceLog;
using Microsoft.AspNetCore.Mvc;

namespace InterseptorSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CarsController : ControllerBase
    {
        private readonly CrudService _crudService;

        public CarsController(CrudService crudService)
        {
            _crudService = crudService;
        }
        [HttpPost]
        public IActionResult Create(Cars cars)
        {
             var result=_crudService.Create(cars);
            return Ok(result);
        }

    }
}
