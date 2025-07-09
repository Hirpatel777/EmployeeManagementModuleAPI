using EmployeeManagementModuleAPI.Core.Entities;
using EmployeeManagementModuleAPI.Core.Interfaces;
using EmployeeManagementModuleAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementModuleAPI.Application.Services
{
    public class SalaryService
    {
        private readonly IRepository<Salary> _repo;
        private readonly AppDbContext _context;
        public SalaryService(IRepository<Salary> repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;

        }

        public async Task<IEnumerable<Salary>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Salary> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task AddAsync(Salary salary)
        {
            await _repo.AddAsync(salary);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(Salary salary)
        {
            _repo.Update(salary);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var salary = await _repo.GetByIdAsync(id);
            if (salary != null)
            {
                _repo.Delete(salary);
                await _repo.SaveAsync();
            }
        }

        public async Task<List<DepartmentMonthlySalaryDto>> GetDepartmentMonthlySummaryAsync(int year)
        {
            var result = await _context.Salaries
                .Where(s => s.Date.Year == year)
                .Include(s => s.Employee)
                    .ThenInclude(e => e.Department)
                .GroupBy(s => new { s.Employee.Department.Name, Month = s.Date.Month })
                .Select(g => new DepartmentMonthlySalaryDto
                {
                    DepartmentName = g.Key.Name,
                    Month = g.Key.Month,
                    TotalSalary = g.Sum(s => s.Amount)
                })
                .OrderBy(r => r.DepartmentName)
                .ThenBy(r => r.Month)
                .ToListAsync();

            return result;
        }

        public async Task<List<EmployeeSalaryRangeDto>> GetEmployeesBySalaryRangeAsync(decimal min, decimal max)
        {
            var query = await _context.Salaries
                .Where(s => s.Amount >= min && s.Amount <= max)
                .Include(s => s.Employee)
                    .ThenInclude(e => e.Department)
                .Include(s => s.Employee)
                    .ThenInclude(e => e.City)
                .Select(s => new EmployeeSalaryRangeDto
                {
                    EmployeeName = s.Employee.Name,
                    SalaryAmount = s.Amount,
                    DepartmentName = s.Employee.Department.Name,
                    CityName = s.Employee.City.CityName
                })
                .ToListAsync();

            return query;
        }


    }
}
