using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IDailyWorkSummaryService
    {
        Task<DailyWorkSummaryDto?> GetSummaryByIdAsync(long id);
        Task<DailyWorkSummaryDto?> GetDailySummaryAsync(int employeeId, DateTime date);
        Task<List<DailyWorkSummaryDto>> GetSummariesByEmployeeAsync(int employeeId);
        Task<List<DailyWorkSummaryDto>> GetMonthlySummariesAsync(int employeeId, int year, int month);
        Task ProcessCheckinAsync(int employeeId, DateTime checkinTime);
    }
}
