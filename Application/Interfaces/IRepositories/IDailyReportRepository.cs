using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IDailyReportRepository
    {
        Task<DailyReport?> GetByIdAsync(long id);
        Task<DailyReport?> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<List<DailyReport>> GetByEmployeeAsync(int employeeId);
        Task AddAsync(DailyReport report);
        Task UpdateAsync(DailyReport report);
        Task FinalizeReportAsync(long reportId);
    }

}


