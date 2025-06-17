using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface ITaskRepository
    {
        Task<EmployeesList?> GetByIdAsync(long taskId);
        Task<EmployeesList?> GetByIdWithEmployeeAsync(long taskId);
        Task<List<EmployeesList>> GetByEmployeeAsync(int employeeId);
        Task<List<EmployeesList>> GetByStatusAsync(string status);
        Task<List<EmployeesList>> GetUpcomingDeadlineTasksAsync(int days = 3);
        Task AddAsync(EmployeesList task);
        Task UpdateAsync(EmployeesList task);
        Task DeleteAsync(EmployeesList task);
        Task<bool> ExistsAsync(long taskId);
        Task<List<EmployeesList>> GetByProjectAsync(string projectName);
        Task<List<EmployeesList>> GetCompletedTasksAsync(DateTime fromDate, DateTime toDate);
    }
}
