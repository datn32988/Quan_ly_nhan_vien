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
        Task<List<EmployeesList>> GetAvailableTasksForPlanningAsync();
        Task<EmployeesList?> GetByIdAsync(long taskId);
        Task<EmployeesList> AddAsync(EmployeesList task);
        Task UpdateAsync(EmployeesList task);
        Task DeleteAsync(EmployeesList task);
    }
}
