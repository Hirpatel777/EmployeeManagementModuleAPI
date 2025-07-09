using EmployeeManagementModuleAPI.Application.Services;
using EmployeeManagementModuleAPI.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementModuleAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _service;

        public CityController(CityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var city = await _service.GetByIdAsync(id);
            return city == null ? NotFound() : Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Post(City c)
        {
            await _service.AddAsync(c);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, City c)
        {
            if (id != c.CityId) return BadRequest();
            await _service.UpdateAsync(c);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
