using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IWorkScheduleRepository
    {
        Task<WorkSchedule?> GetByIdAsync(long id);
        Task<WorkSchedule?> GetByIdWithEmployeeAsync(long id);
        Task<List<WorkSchedule>> GetFilteredAsync(
            int? employeeId,
            DateTime? fromDate,
            DateTime? toDate,
            string? scheduleType);
        Task AddAsync(WorkSchedule schedule);
        Task UpdateAsync(WorkSchedule schedule);
        Task DeleteAsync(WorkSchedule schedule);

    }
}
