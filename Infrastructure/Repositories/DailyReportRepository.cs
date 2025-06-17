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
        public async Task<List<EmployeesList>> GetAvailableTasksForEmployeeAsync(int employeeId)
        {
            return await _context.EmployeesLists
                .Where(t => t.AssignedToEmployeeId == employeeId && t.Status != "Completed")
                .Include(t => t.AssignedToEmployee)
                .ToListAsync();
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
        public async Task<List<DailyReportCompletedTask>> GetCompletedTasksByReportIdAsync(long reportId)
        {
            return await _context.DailyReportCompletedTasks
                .Include(ct => ct.Task)
                .Where(ct => ct.ReportId == reportId)
                .ToListAsync();
        }

        public async Task<List<DailyReportPlannedTask>> GetPlannedTasksByReportIdAsync(long reportId)
        {
            return await _context.DailyReportPlannedTasks
                .Include(pt => pt.Task)
                .Where(pt => pt.ReportId == reportId)
                .ToListAsync();
        }

        public async Task<List<DateTime>> GetMissingReportDatesForEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return new List<DateTime>();

            var startDate = employee.HireDate.Date;
            var endDate = DateTime.Today.AddDays(-1); 

            var existingReportDates = await _context.DailyReports
                .Where(r => r.EmployeeId == employeeId)
                .Select(r => r.ReportDate.Date)
                .ToListAsync();

            var missingDates = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (!existingReportDates.Contains(date))
                    {
                        missingDates.Add(date);
                    }
                }
            }

            return missingDates;
        }

    }
}
