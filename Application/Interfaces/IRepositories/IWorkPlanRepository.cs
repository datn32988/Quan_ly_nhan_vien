using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IWorkPlanRepository
    {
        Task<WorkPlan?> GetByIdAsync(long id);
        Task<WorkPlan?> GetByIdWithEmployeeAsync(long id);
        Task<List<WorkPlan>> GetFilteredAsync(long? employeeId, DateTime? fromDate, DateTime? toDate, bool? isCompleted);
        Task AddAsync(WorkPlan workPlan);
        Task UpdateAsync(WorkPlan workPlan);
        Task DeleteAsync(WorkPlan workPlan);
    }
}
