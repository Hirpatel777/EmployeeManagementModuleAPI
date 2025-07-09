using EmployeeManagementModuleAPI.Application.Services;
using EmployeeManagementModuleAPI.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementModuleAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly SalaryService _salaryService;

        public SalaryController(SalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _salaryService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var salary = await _salaryService.GetByIdAsync(id);
            return salary == null ? NotFound() : Ok(salary);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Salary salary)
        {
            await _salaryService.AddAsync(salary);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Salary salary)
        {
            if (id != salary.Id) return BadRequest("ID mismatch");
            await _salaryService.UpdateAsync(salary);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _salaryService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("department-monthly-summary")]
        public async Task<IActionResult> GetDepartmentMonthlySummary([FromQuery] int year)
        {
            if (year <= 0)
                return BadRequest("Please provide a valid year.");

            var summary = await _salaryService.GetDepartmentMonthlySummaryAsync(year);
            return Ok(summary);
        }

        [HttpGet("by-range")]
        public async Task<IActionResult> GetBySalaryRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            if (min > max || min < 0 || max <= 0)
                return BadRequest("Invalid salary range");

            var result = await _salaryService.GetEmployeesBySalaryRangeAsync(min, max);
            return Ok(result);
        }

    }
}
