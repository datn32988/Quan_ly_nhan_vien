using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DailyReportRepository : IDailyReportRepository
    {
        private readonly ApplicationDbContext _context;

        public DailyReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DailyReport> CreateAsync(DailyReport dailyReport)
        {
            _context.DailyReports.Add(dailyReport);
            await _context.SaveChangesAsync();
            return dailyReport;
        }

        public async Task<DailyReport?> GetByEmployeeAndDateAsync(long employeeId, DateTime date)
        {
            return await _context.DailyReports
                .Include(r => r.Employee)
                .Include(r => r.DailyReportCompletedTasks)
                    .ThenInclude(ct => ct.Task)
                .Include(r => r.DailyReportPlannedTasks)
                    .ThenInclude(pt => pt.Task)
                .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.ReportDate.Date == date.Date);
        }

        public async Task<List<DailyReport>> GetByEmployeeAndMonthAsync(long employeeId, int year, int month)
        {
            var firstDay = new DateTime(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            return await _context.DailyReports
                .Where(d => d.EmployeeId == employeeId &&
                           d.ReportDate >= firstDay &&
                           d.ReportDate <= lastDay)
                .Include(d => d.Employee)
                .Include(d => d.DailyReportCompletedTasks)
                .Include(d => d.DailyReportPlannedTasks)
                .OrderBy(d => d.ReportDate)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(long employeeId, DateTime date)
        {
            return await _context.DailyReports
                .AnyAsync(r => r.EmployeeId == employeeId && r.ReportDate.Date == date.Date);
        }
        public async Task<List<DailyReport>> GetByMonthAsync(int year, int month)
        {
            
            var firstDay = new DateTime(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            return await _context.DailyReports
                .Where(r => r.ReportDate >= firstDay && r.ReportDate <= lastDay)
                .Include(r => r.Employee)
                .Include(r => r.DailyReportCompletedTasks)
                    .ThenInclude(ct => ct.Task)
                .Include(r => r.DailyReportPlannedTasks)
                    .ThenInclude(pt => pt.Task)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
