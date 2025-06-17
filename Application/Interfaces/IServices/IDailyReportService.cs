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
        Task<DailyReportDto?> GetReportByIdAsync(long id);
        Task<DailyReportDto?> GetDailyReportAsync(int employeeId, DateTime date);
        Task<List<DailyReportDto>> GetEmployeeReportsAsync(int employeeId);
        Task<DailyReportDto> CreateOrUpdateDailyReportAsync(DailyReportDto reportDto);
        Task FinalizeReportAsync(long reportId);
        Task<List<TaskDto>> GetAvailableTasksForEmployeeAsync(int employeeId);
        Task<List<CompletedTaskDto>> GetCompletedTasksByReportIdAsync(long reportId);
        Task<List<PlannedTaskDto>> GetPlannedTasksByReportIdAsync(long reportId);
        Task<List<DateTime>> GetMissingReportDatesForEmployeeAsync(int employeeId);
    }
}
