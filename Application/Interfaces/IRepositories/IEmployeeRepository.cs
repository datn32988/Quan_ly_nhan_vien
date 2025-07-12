using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(long id);
        Task<List<Employee>> GetAllAsync(bool includeDetails = false);
        Task<List<Employee>> GetEmployeesByManagerAsync(long managerId);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<bool> ExistsAsync(long id);
        Task<List<Employee>> GetAllWithPositionAndDepartmentAsync();
    }
}
