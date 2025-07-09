using EmployeeManagementModuleAPI.Core.Entities;
using EmployeeManagementModuleAPI.Core.Interfaces;

namespace EmployeeManagementModuleAPI.Application.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var emp = await _employeeRepository.GetByIdAsync(id);
            if (emp != null)
            {
                _employeeRepository.Delete(emp);
                await _employeeRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<EmployeeResponseDto>> SearchEmployeesByNameAsync(string name)
        {
            var employees = await _employeeRepository.GetAllAsync();

            var result = employees
                .Where(e => !string.IsNullOrEmpty(e.Name) &&
                            e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Gender = e.Gender,
                    DOB = e.DOB,
                    DepartmentName = e.Department?.Name ?? "N/A"
                });

            return result;
        }


    }
}
