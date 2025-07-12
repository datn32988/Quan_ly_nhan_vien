using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IDailyReportService
    {
        Task<DailyReportDto> CreateDailyReportAsync(DailyReportCreateDto request);
        Task<DailyReportDto?> GetDailyReportAsync(long employeeId, DateTime date);
        Task<MonthlyReportDto?> GetMonthlyReportAsync(long employeeId, int year, int month);
        Task<List<MonthlyReportDto>> GetMonthlyReportsAsync(int year, int month);
    }
}
