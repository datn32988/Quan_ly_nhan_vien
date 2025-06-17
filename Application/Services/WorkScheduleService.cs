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
    public class WorkScheduleService : IWorkScheduleService
    {
        private readonly IWorkScheduleRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public WorkScheduleService(
            IWorkScheduleRepository repository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<WorkScheduleDto> GetByIdAsync(long id)
        {
            var schedule = await _repository.GetByIdWithEmployeeAsync(id);
            return _mapper.Map<WorkScheduleDto>(schedule);
        }

        public async Task<List<WorkScheduleDto>> GetSchedulesAsync(WorkScheduleFilterDto filter)
        {
            var schedules = await _repository.GetFilteredAsync(
                filter.EmployeeId,
                filter.FromDate,
                filter.ToDate,
                filter.ScheduleType);

            return _mapper.Map<List<WorkScheduleDto>>(schedules);
        }

        public async Task<WorkScheduleDto> CreateScheduleAsync(CreateWorkScheduleDto dto)
        {
            var schedule = _mapper.Map<WorkSchedule>(dto);
            await _repository.AddAsync(schedule);
            return await GetByIdAsync(schedule.WorkScheduleId);
        }

        public async Task<WorkScheduleDto> UpdateScheduleAsync(long id, UpdateWorkScheduleDto dto)
        {
            var schedule = await _repository.GetByIdAsync(id);
            _mapper.Map(dto, schedule);
            await _repository.UpdateAsync(schedule);
            return await GetByIdAsync(id);
        }

        public async Task DeleteScheduleAsync(long id)
        {
            var schedule = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(schedule);
        }

        public async Task<List<WorkScheduleDto>> GetEmployeeMonthlySchedule(int employeeId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var schedules = await _repository.GetFilteredAsync(
                employeeId,
                startDate,
                endDate,
                null);

            return _mapper.Map<List<WorkScheduleDto>>(schedules);
        }

        public async Task<List<WorkScheduleDto>> GetEmployeeWeeklySchedule(int employeeId, DateTime startDate)
        {
            var endDate = startDate.AddDays(6);

            var schedules = await _repository.GetFilteredAsync(
                employeeId,
                startDate,
                endDate,
                null);

            return _mapper.Map<List<WorkScheduleDto>>(schedules);
        }
        public async Task<List<WorkScheduleMonthlyDto>> GetMonthlyWorkSchedules(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Lấy tất cả nhân viên
            var employees = await _employeeRepository.GetAllAsync();

            // Lấy tất cả lịch làm việc trong tháng
            var schedules = await _repository.GetFilteredAsync(
                null,
                startDate,
                endDate,
                null);

            var result = new List<WorkScheduleMonthlyDto>();

            foreach (var employee in employees)
            {
                var employeeSchedules = schedules
                    .Where(s => s.EmployeeId == employee.EmployeeId)
                    .OrderBy(s => s.ScheduleDate)
                    .ToList();

                var monthlyDto = new WorkScheduleMonthlyDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.FullName,
                    Position = employee.Position?.PositionName ?? "N/A",
                    Schedules = employeeSchedules.Select(s => new DailyScheduleDto
                    {
                        Date = s.ScheduleDate,
                        ScheduleType = s.ScheduleType,
                        Notes = s.Notes
                    }).ToList()
                };

                result.Add(monthlyDto);
            }

            return result;
        }
    }
}
