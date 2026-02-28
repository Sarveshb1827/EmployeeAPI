using EmployeeApi.Models;

namespace EmployeeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
    }
}
