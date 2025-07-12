using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Application.Services
{
    public class WorkScheduleService : IWorkScheduleService
    {
        private readonly IWorkScheduleRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public WorkScheduleService(
            IWorkScheduleRepository repository,
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _configuration = configuration;
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
            if (schedule == null)
                throw new KeyNotFoundException($"Không tìm thấy lịch làm việc với ID = {id}");
            _mapper.Map(dto, schedule);
            await _repository.UpdateAsync(schedule);
            return await GetByIdAsync(id);
        }

        public async Task DeleteScheduleAsync(long id)
        {
            var schedule = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(schedule);
        }

        public async Task<List<WorkScheduleDto>> GetEmployeeMonthlySchedule(long employeeId, int year, int month)
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

        public async Task<List<WorkScheduleDto>> GetEmployeeWeeklySchedule(long employeeId, DateTime startDate)
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

            var employees = await _employeeRepository.GetAllAsync();


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
        public async Task CheckAndSendEmailReminders()
        {
            var today = DateTime.Today;
            var threeDaysAgo = today.AddDays(-3);
            var recentSchedules = await _repository.GetFilteredAsync(
                employeeId: null,
                fromDate: threeDaysAgo,
                toDate: today,
                scheduleType: null);

            var employeeIdsWithSchedules = recentSchedules.Select(s => s.EmployeeId).Distinct().ToList();

            var employees = (await _employeeRepository.GetAllAsync())
                .Where(e => !string.IsNullOrEmpty(e.Email) && !employeeIdsWithSchedules.Contains(e.EmployeeId))
                .ToList();

            foreach (var employee in employees)
            {
                await SendEmailReminder(employee);
                Console.WriteLine($"Failed to send email to {employee.Email}:");

            }
        }

        private async Task SendEmailReminder(Employee employee)
        {
            var message = new MimeMessage();
            var senderName = _configuration["SmtpSettings:SenderName"] ?? throw new InvalidOperationException("SmtpSettings:SenderName is missing in configuration");
            var senderEmail = _configuration["SmtpSettings:SenderEmail"] ?? throw new InvalidOperationException("SmtpSettings:SenderEmail is missing in configuration");
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(employee.FullName, employee.Email));
            message.Subject = "Cảnh báo: Chưa đăng ký lịch làm việc";

            message.Body = new TextPart("plain")
            {
                Text = $@"Xin chào {employee.FullName},
Bạn chưa đăng ký lịch làm việc trong 3 ngày qua. Vui lòng đăng ký lịch làm việc sớm nhất có thể.
Trân trọng,
{senderName}"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient()) 
            {
                var server = _configuration["SmtpSettings:Server"] ?? throw new InvalidOperationException("SmtpSettings:Server is missing in configuration");
                var port = int.Parse(_configuration["SmtpSettings:Port"] ?? throw new InvalidOperationException("SmtpSettings:Port is missing in configuration"));
                var password = _configuration["SmtpSettings:Password"] ?? throw new InvalidOperationException("SmtpSettings:Password is missing in configuration");

                try
                {
                    await client.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(senderEmail, password);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email to {employee.Email}: {ex.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }

        }
    }
}

