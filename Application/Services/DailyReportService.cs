using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DailyReportService : IDailyReportService
    {
        private readonly IDailyReportRepository _dailyReportRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;

        public DailyReportService(
            IDailyReportRepository dailyReportRepository,
            IEmployeeRepository employeeRepository,
            ITaskRepository taskRepository)
        {
            _dailyReportRepository = dailyReportRepository;
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
        }

        public async Task<DailyReportDto> CreateDailyReportAsync(DailyReportCreateDto request)
        {

            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
                throw new ArgumentException("Employee not found");

            var existingReport = await _dailyReportRepository.ExistsAsync(request.EmployeeId, request.ReportDate.Date);
            if (existingReport)
                throw new InvalidOperationException("Daily report already exists for this date");

            var dailyReport = new DailyReport
            {
                EmployeeId = request.EmployeeId,
                ReportDate = request.ReportDate.Date,
                GeneralNotes = request.GeneralNotes,
                FinalizedTime = DateTime.UtcNow
            };

            var createdReport = await _dailyReportRepository.CreateAsync(dailyReport);


            foreach (var completedTask in request.CompletedTasks)
            {
                createdReport.DailyReportCompletedTasks.Add(new DailyReportCompletedTask
                {
                    ReportId = createdReport.ReportId,
                    TaskId = completedTask.TaskId,
                    CompletionDescription = completedTask.CompletionDescription
                });
            }

           
            foreach (var plannedTask in request.PlannedTasks)
            {
                createdReport.DailyReportPlannedTasks.Add(new DailyReportPlannedTask
                {
                    ReportId = createdReport.ReportId,
                    TaskId = plannedTask.TaskId,
                    PlannedDescription = plannedTask.PlannedDescription
                });
            }

            return await MapToDailyReportDto(createdReport);
        }

        public async Task<DailyReportDto?> GetDailyReportAsync(long employeeId, DateTime date)
        {
            var report = await _dailyReportRepository.GetByEmployeeAndDateAsync(employeeId, date.Date);
            if (report == null) return null;

            return await MapToDailyReportDto(report);
        }

        public async Task<MonthlyReportDto?> GetMonthlyReportAsync(long employeeId, int year, int month)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return null;

            var dailyReports = await _dailyReportRepository.GetByEmployeeAndMonthAsync(employeeId, year, month);

            var monthlyReport = new MonthlyReportDto
            {
                EmployeeId = employeeId,
                EmployeeName = employee.FullName,
                Year = year,
                Month = month,
                DailyReports = new List<DailyReportDto>()
            };

            foreach (var report in dailyReports)
            {
                monthlyReport.DailyReports.Add(await MapToDailyReportDto(report));
            }

            monthlyReport.TotalCompletedTasks = monthlyReport.DailyReports.Sum(r => r.CompletedTasks.Count);
            monthlyReport.TotalPlannedTasks = monthlyReport.DailyReports.Sum(r => r.PlannedTasks.Count);

            return monthlyReport;
        }

        private async Task<DailyReportDto> MapToDailyReportDto(DailyReport report)
        {
            var employee = await _employeeRepository.GetByIdAsync(report.EmployeeId);

            var dto = new DailyReportDto
            {
                ReportId = report.ReportId,
                EmployeeId = report.EmployeeId,
                EmployeeName = employee?.FullName ?? "Unknown",
                ReportDate = report.ReportDate,
                GeneralNotes = report.GeneralNotes
            };

            
            foreach (var completedTask in report.DailyReportCompletedTasks)
            {
                var task = await _taskRepository.GetByIdAsync(completedTask.TaskId);
                dto.CompletedTasks.Add(new CompletedTaskDetailDto
                {
                    TaskId = completedTask.TaskId,
                    TaskName = task?.TaskName ?? "Unknown Task",
                    CompletionDescription = completedTask.CompletionDescription
                });
            }

            
            foreach (var plannedTask in report.DailyReportPlannedTasks)
            {
                var task = await _taskRepository.GetByIdAsync(plannedTask.TaskId);
                dto.PlannedTasks.Add(new PlannedTaskDetailDto
                {
                    TaskId = plannedTask.TaskId,
                    TaskName = task?.TaskName ?? "Unknown Task",
                    PlannedDescription = plannedTask.PlannedDescription
                });
            }

            return dto;
        }
        public async Task<List<MonthlyReportDto>> GetMonthlyReportsAsync(int year, int month)
        {
            var dailyReports = await _dailyReportRepository.GetByMonthAsync(year, month);

           
            var reportsByEmployee = dailyReports
                .GroupBy(r => r.EmployeeId)
                .ToList();

            var monthlyReports = new List<MonthlyReportDto>();

            foreach (var employeeGroup in reportsByEmployee)
            {
                var employee = employeeGroup.First().Employee;
                var monthlyReport = new MonthlyReportDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.FullName,
                    Year = year,
                    Month = month,
                    DailyReports = new List<DailyReportDto>()
                };

                foreach (var report in employeeGroup)
                {
                    monthlyReport.DailyReports.Add(await MapToDailyReportDto(report));
                }

                monthlyReport.TotalCompletedTasks = monthlyReport.DailyReports
                    .Sum(r => r.CompletedTasks.Count);
                monthlyReport.TotalPlannedTasks = monthlyReport.DailyReports
                    .Sum(r => r.PlannedTasks.Count);

                monthlyReports.Add(monthlyReport);
            }

            return monthlyReports;
        }
    }
}
