using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DailyWorkSummaryService : IDailyWorkSummaryService
    {
        private readonly IDailyWorkSummaryRepository _summaryRepository;
        private readonly ICheckinRepository _checkinRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public DailyWorkSummaryService(
            IDailyWorkSummaryRepository summaryRepository,
            ICheckinRepository checkinRepository,
            IMapper mapper,
            IEmployeeRepository employeeRepository)
        {
            _summaryRepository = summaryRepository;
            _checkinRepository = checkinRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<DailyWorkSummaryDto?> GetSummaryByIdAsync(long id)
        {
            var summary = await _summaryRepository.GetByIdAsync(id);
            return _mapper.Map<DailyWorkSummaryDto>(summary);
        }

        public async Task<DailyWorkSummaryDto?> GetDailySummaryAsync(int employeeId, DateTime date)
        {
            var summary = await _summaryRepository.GetByEmployeeAndDateAsync(employeeId, date.Date);
            return _mapper.Map<DailyWorkSummaryDto>(summary);
        }

        public async Task<List<DailyWorkSummaryDto>> GetSummariesByEmployeeAsync(int employeeId)
        {
            var summaries = await _summaryRepository.GetByEmployeeAsync(employeeId);
            return _mapper.Map<List<DailyWorkSummaryDto>>(summaries);
        }

        public async Task<List<DailyWorkSummaryDto>> GetMonthlySummariesAsync(int employeeId, int year, int month)
        {
            var summaries = await _summaryRepository.GetByEmployeeAndMonthAsync(employeeId, year, month);
            return _mapper.Map<List<DailyWorkSummaryDto>>(summaries);
        }

        public async Task ProcessCheckinAsync(int employeeId, DateTime checkinTime)
        {
            var localCheckinTime = checkinTime.Kind == DateTimeKind.Utc
                ? checkinTime.ToLocalTime()
                : checkinTime;

            
            var date = localCheckinTime.Date;
            var checkins = await _checkinRepository.GetByEmployeeAndDateAsync(employeeId, date);


            var summary = await _summaryRepository.GetByEmployeeAndDateAsync(employeeId, date) ??
                          new DailyWorkSummary
                          {
                              EmployeeId = employeeId,
                              WorkDate = date,
                              Status = "Pending"
                          };

            if (!summary.FirstCheckin.HasValue || checkinTime < summary.FirstCheckin)
            {
                summary.FirstCheckin = checkinTime;
            }

            if (!summary.LastCheckout.HasValue || checkinTime > summary.LastCheckout)
            {
                summary.LastCheckout = checkinTime;
            }

            if (summary.FirstCheckin.HasValue && summary.LastCheckout.HasValue)
            {
                var timeSpan = summary.LastCheckout.Value - summary.FirstCheckin.Value;
                summary.TotalCalculatedHours = (decimal)timeSpan.TotalHours;

                
                var firstCheckinTime = summary.FirstCheckin.Value.TimeOfDay;
                var lastCheckoutTime = summary.LastCheckout.Value.TimeOfDay;

                summary.IsLateArrival = firstCheckinTime > WorkTimeConstants.LateArrivalThreshold;
                summary.IsEarlyDeparture = lastCheckoutTime < WorkTimeConstants.EarlyDepartureThreshold;

                summary.IsFullDay = summary.TotalCalculatedHours >= WorkTimeConstants.FullDayRequiredHours &&
                                   firstCheckinTime >= WorkTimeConstants.EarliestStartTime &&
                                   lastCheckoutTime <= WorkTimeConstants.LatestEndTime;

                summary.IsHalfDay = summary.TotalCalculatedHours >= WorkTimeConstants.HalfDayRequiredHours &&
                                   summary.TotalCalculatedHours < WorkTimeConstants.FullDayRequiredHours &&
                                   firstCheckinTime >= WorkTimeConstants.EarliestStartTime &&
                                   lastCheckoutTime <= WorkTimeConstants.LatestEndTime;

                summary.IsUnderHours = summary.TotalCalculatedHours < WorkTimeConstants.HalfDayRequiredHours ||
                                      firstCheckinTime < WorkTimeConstants.EarliestStartTime ||
                                      lastCheckoutTime > WorkTimeConstants.LatestEndTime;

             
                if (summary.IsFullDay) summary.Status = "Full Day";
                else if (summary.IsHalfDay) summary.Status = "Half Day";
                else summary.Status = "Under Hours";
            }

            if (summary.Id == 0)
            {
                await _summaryRepository.AddAsync(summary);
            }
            else
            {
                await _summaryRepository.UpdateAsync(summary);
            }
        }
        public async Task<List<WorkSummaryMonthlyDto>> GetMonthlyWorkSummaries(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var employees = await _employeeRepository.GetAllAsync(includeDetails: true);
            var summaries = await _summaryRepository.GetSummariesByDateRangeAsync(startDate, endDate);

            var result = new List<WorkSummaryMonthlyDto>();

            foreach (var emp in employees)
            {
                var empSummaries = summaries
                    .Where(s => s.EmployeeId == emp.EmployeeId)
                    .OrderBy(s => s.WorkDate)
                    .Select(s => new DailyWorkSummaryDto
                    {
                        WorkDate = s.WorkDate,
                        FirstCheckin = s.FirstCheckin,
                        LastCheckout = s.LastCheckout,
                        TotalCalculatedHours = s.TotalCalculatedHours,
                        Status = s.Status ?? "",
                        IsLateArrival = s.IsLateArrival,
                        IsEarlyDeparture = s.IsEarlyDeparture,
                        IsUnderHours = s.IsUnderHours,
                        IsFullDay = s.IsFullDay,
                        IsHalfDay = s.IsHalfDay
                    }).ToList();

                var monthly = new WorkSummaryMonthlyDto
                {
                    EmployeeId = emp.EmployeeId,
                    EmployeeName = emp.FullName,
                    Position = emp.Position?.PositionName ?? "N/A",
                    Summaries = empSummaries
                };

                result.Add(monthly);
            }

            return result;
        }



    }

}


