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
        [HttpPut]
        public IActionResult Update(Cars cars)
        {
            var result=_crudService.Update(cars);
            return Ok(result);
        }
        [HttpDelete]
        public IActionResult Delete(Cars cars)
        {
            var result = _crudService.Delete(cars);
            return Ok(result);
        }

    }
}
