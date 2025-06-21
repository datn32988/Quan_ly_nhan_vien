using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IDailyReportRepository
    {
        Task<DailyReport> CreateAsync(DailyReport dailyReport);
        Task<DailyReport?> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<List<DailyReport>> GetByEmployeeAndMonthAsync(int employeeId, int year, int month);
        Task<bool> ExistsAsync(int employeeId, DateTime date);
        Task<List<DailyReport>> GetByMonthAsync(int year, int month);

        
    }
}
