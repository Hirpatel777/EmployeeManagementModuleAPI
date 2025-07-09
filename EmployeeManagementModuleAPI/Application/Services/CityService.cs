using EmployeeManagementModuleAPI.Core.Entities;
using EmployeeManagementModuleAPI.Core.Interfaces;

namespace EmployeeManagementModuleAPI.Application.Services
{
    public class CityService
    {
        private readonly IRepository<City> _repo;

        public CityService(IRepository<City> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<City>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<City> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task AddAsync(City c) { await _repo.AddAsync(c); await _repo.SaveAsync(); }
        public async Task UpdateAsync(City c) { _repo.Update(c); await _repo.SaveAsync(); }
        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null) { _repo.Delete(entity); await _repo.SaveAsync(); }
        }

    }
}
