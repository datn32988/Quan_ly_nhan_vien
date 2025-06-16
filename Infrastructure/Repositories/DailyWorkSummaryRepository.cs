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
    public class DailyWorkSummaryRepository : IDailyWorkSummaryRepository
    {
        private readonly ApplicationDbContext _context;

        public DailyWorkSummaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DailyWorkSummary?> GetByIdAsync(long id)
        {
            return await _context.DailyWorkSummaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<DailyWorkSummary?> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _context.DailyWorkSummaries
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.WorkDate.Date == date.Date);
        }

        public async Task<List<DailyWorkSummary>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.DailyWorkSummaries
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.WorkDate)
                .ToListAsync();
        }

        public async Task<List<DailyWorkSummary>> GetByEmployeeAndMonthAsync(int employeeId, int year, int month)
        {
            return await _context.DailyWorkSummaries
                .Where(s => s.EmployeeId == employeeId &&
                           s.WorkDate.Year == year &&
                           s.WorkDate.Month == month)
                .OrderBy(s => s.WorkDate)
                .ToListAsync();
        }

        public async Task AddAsync(DailyWorkSummary summary)
        {
            await _context.DailyWorkSummaries.AddAsync(summary);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DailyWorkSummary summary)
        {
            _context.DailyWorkSummaries.Update(summary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DailyWorkSummary summary)
        {
            _context.DailyWorkSummaries.Remove(summary);
            await _context.SaveChangesAsync();
        }
    }
}
