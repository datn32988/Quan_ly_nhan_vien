using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
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
        private readonly IDailyReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public DailyReportService(IDailyReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<DailyReportDto?> GetReportByIdAsync(long id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            return _mapper.Map<DailyReportDto>(report);
        }

        public async Task<DailyReportDto?> GetDailyReportAsync(int employeeId, DateTime date)
        {
            var report = await _reportRepository.GetByEmployeeAndDateAsync(employeeId, date);
            return _mapper.Map<DailyReportDto>(report);
        }

        public async Task<List<DailyReportDto>> GetEmployeeReportsAsync(int employeeId)
        {
            var reports = await _reportRepository.GetByEmployeeAsync(employeeId);
            return _mapper.Map<List<DailyReportDto>>(reports);
        }

        public async Task<DailyReportDto> CreateOrUpdateDailyReportAsync(DailyReportDto reportDto)
        {
            var existingReport = await _reportRepository.GetByEmployeeAndDateAsync(
                reportDto.EmployeeId,
                reportDto.ReportDate);

            if (existingReport == null)
            {
                var newReport = _mapper.Map<DailyReport>(reportDto);
                await _reportRepository.AddAsync(newReport);
                return _mapper.Map<DailyReportDto>(newReport);
            }

            // Chỉ cập nhật các trường có thể thay đổi
            existingReport.TotalWorkHours = reportDto.TotalWorkHours;
            existingReport.WorkStatus = reportDto.WorkStatus;
            existingReport.GeneralNotes = reportDto.GeneralNotes;

            // Xử lý các công việc đã hoàn thành
            existingReport.DailyReportCompletedTasks = reportDto.CompletedTasks
                .Select(ct => new DailyReportCompletedTask
                {
                    ReportId = existingReport.ReportId,
                    TaskId = ct.TaskId,
                    CompletionDescription = ct.CompletionDescription
                }).ToList();

            
            existingReport.DailyReportPlannedTasks = reportDto.PlannedTasks
                .Select(pt => new DailyReportPlannedTask
                {
                    ReportId = existingReport.ReportId,
                    TaskId = pt.TaskId,
                    PlannedDescription = pt.PlannedDescription
                }).ToList();

            await _reportRepository.UpdateAsync(existingReport);
            return _mapper.Map<DailyReportDto>(existingReport);
        }

        public async Task FinalizeReportAsync(long reportId)
        {
            await _reportRepository.FinalizeReportAsync(reportId);
        }

        public async Task<List<TaskDto>> GetAvailableTasksForEmployeeAsync(int employeeId)
        {
           
            throw new NotImplementedException();
        }
    }
}
