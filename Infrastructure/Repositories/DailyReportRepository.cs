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

        public async Task<DailyReport?> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _context.DailyReports
                .Include(r => r.Employee)
                .Include(r => r.DailyReportCompletedTasks)
                    .ThenInclude(ct => ct.Task)
                .Include(r => r.DailyReportPlannedTasks)
                    .ThenInclude(pt => pt.Task)
                .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.ReportDate.Date == date.Date);
        }

        public async Task<List<DailyReport>> GetByEmployeeAndMonthAsync(int employeeId, int year, int month)
        {
            return await _context.DailyReports
                .Include(r => r.Employee)
                .Include(r => r.DailyReportCompletedTasks)
                    .ThenInclude(ct => ct.Task)
                .Include(r => r.DailyReportPlannedTasks)
                    .ThenInclude(pt => pt.Task)
                .Where(r => r.EmployeeId == employeeId &&
                           r.ReportDate.Year == year &&
                           r.ReportDate.Month == month)
                .OrderBy(r => r.ReportDate)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int employeeId, DateTime date)
        {
            return await _context.DailyReports
                .AnyAsync(r => r.EmployeeId == employeeId && r.ReportDate.Date == date.Date);
        }
        public async Task<List<DailyReport>> GetByMonthAsync(int year, int month)
        {
            return await _context.DailyReports
                .Where(r => r.ReportDate.Year == year && r.ReportDate.Month == month)
                .Include(r => r.Employee)
                .Include(r => r.DailyReportCompletedTasks)
                    .ThenInclude(ct => ct.Task)
                .Include(r => r.DailyReportPlannedTasks)
                    .ThenInclude(pt => pt.Task)
                .ToListAsync();
        }
    }
}
