using EmployeeManagementModuleAPI.Core.Entities;
using EmployeeManagementModuleAPI.Core.Interfaces;

namespace EmployeeManagementModuleAPI.Application.Services
{
    public class DepartmentService
    {
        private readonly IRepository<Department> _repo;

        public DepartmentService(IRepository<Department> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Department>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Department> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task AddAsync(Department d) { await _repo.AddAsync(d); await _repo.SaveAsync(); }
        public async Task UpdateAsync(Department d) { _repo.Update(d); await _repo.SaveAsync(); }
        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null) { _repo.Delete(entity); await _repo.SaveAsync(); }
        }
    }
}
