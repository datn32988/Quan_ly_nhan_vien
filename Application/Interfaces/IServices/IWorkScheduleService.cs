using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IWorkScheduleService
    {
        Task<WorkScheduleDto> GetByIdAsync(long id);
        Task<List<WorkScheduleDto>> GetSchedulesAsync(WorkScheduleFilterDto filter);
        Task<WorkScheduleDto> CreateScheduleAsync(CreateWorkScheduleDto dto);
        Task<WorkScheduleDto> UpdateScheduleAsync(long id, UpdateWorkScheduleDto dto);
        Task DeleteScheduleAsync(long id);
        Task<List<WorkScheduleDto>> GetEmployeeMonthlySchedule(long employeeId, int year, int month);
        Task<List<WorkScheduleDto>> GetEmployeeWeeklySchedule(long employeeId, DateTime startDate);
        Task<List<WorkScheduleMonthlyDto>> GetMonthlyWorkSchedules(int year, int month);
        Task CheckAndSendEmailReminders();
    }
}
