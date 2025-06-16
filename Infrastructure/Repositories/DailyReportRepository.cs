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

        public async Task<DailyReport?> GetByIdAsync(long id)
        {
            return await _context.DailyReports
                .Include(r => r.Employee)
                .Include(r => r.DailyReportCompletedTasks)
                    .ThenInclude(ct => ct.Task)
                .Include(r => r.DailyReportPlannedTasks)
                    .ThenInclude(pt => pt.Task)
                .FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task<DailyReport?> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _context.DailyReports
                .Include(r => r.DailyReportCompletedTasks)
                .Include(r => r.DailyReportPlannedTasks)
                .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.ReportDate.Date == date.Date);
        }

        public async Task<List<DailyReport>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.DailyReports
                .Where(r => r.EmployeeId == employeeId)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
        }

        public async Task AddAsync(DailyReport report)
        {
            await _context.DailyReports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DailyReport report)
        {
           
            _context.Entry(report).State = EntityState.Modified;

            var existingCompletedTasks = await _context.DailyReportCompletedTasks
                .Where(ct => ct.ReportId == report.ReportId)
                .ToListAsync();

            _context.DailyReportCompletedTasks.RemoveRange(existingCompletedTasks);

            foreach (var task in report.DailyReportCompletedTasks)
            {
                _context.Entry(task).State = EntityState.Added;
            }

            
            var existingPlannedTasks = await _context.DailyReportPlannedTasks
                .Where(pt => pt.ReportId == report.ReportId)
                .ToListAsync();

            _context.DailyReportPlannedTasks.RemoveRange(existingPlannedTasks);

            foreach (var task in report.DailyReportPlannedTasks)
            {
                _context.Entry(task).State = EntityState.Added;
            }

            await _context.SaveChangesAsync();
        }

        public async Task FinalizeReportAsync(long reportId)
        {
            var report = await _context.DailyReports.FindAsync(reportId);
            if (report != null)
            {
                report.IsFinalized = true;
                report.FinalizedTime = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
