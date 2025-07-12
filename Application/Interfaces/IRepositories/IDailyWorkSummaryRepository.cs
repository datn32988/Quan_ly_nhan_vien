using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IDailyWorkSummaryRepository
    {
        Task<DailyWorkSummary?> GetByIdAsync(long id);
        Task<DailyWorkSummary?> GetByEmployeeAndDateAsync(long employeeId, DateTime date);
        Task<List<DailyWorkSummary>> GetByEmployeeAsync(long employeeId);
        Task<List<DailyWorkSummary>> GetByEmployeeAndMonthAsync(long employeeId, int year, int month);
        Task AddAsync(DailyWorkSummary summary);
        Task UpdateAsync(DailyWorkSummary summary);
        Task DeleteAsync(DailyWorkSummary summary);
        Task<List<DailyWorkSummary>> GetByDateRangeWithEmployeeAsync(DateTime startDate, DateTime endDate);
        Task<List<DailyWorkSummary>> GetByEmployeeAndDateRangeAsync(long employeeId, DateTime startDate, DateTime endDate);
        Task<List<DailyWorkSummary>> GetSummariesByDateRangeAsync(DateTime start, DateTime end);

    }
}
