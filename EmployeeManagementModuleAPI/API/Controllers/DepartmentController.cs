using EmployeeManagementModuleAPI.Application.Services;
using EmployeeManagementModuleAPI.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementModuleAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _service;

        public DepartmentController(DepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dept = await _service.GetByIdAsync(id);
            return dept == null ? NotFound() : Ok(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Department d)
        {
            await _service.AddAsync(d);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Department d)
        {
            if (id != d.Id) return BadRequest();
            await _service.UpdateAsync(d);
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
