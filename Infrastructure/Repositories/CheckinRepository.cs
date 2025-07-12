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
    public class CheckinRepository : ICheckinRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Checkin?> GetByIdAsync(long id)
        {
            return await _context.Checkins
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(c => c.CheckinId == id);
        }

        public async Task<List<Checkin>> GetByEmployeeIdAsync(long employeeId)
        {
            return await _context.Checkins
                .Where(c => c.EmployeeId == employeeId)
                .Include(c => c.Employee)
                .OrderByDescending(c => c.CheckinTime)
                .ToListAsync();
        }

        public async Task<List<Checkin>> GetByEmployeeAndDateAsync(long employeeId, DateTime date)
        {
            return await _context.Checkins
                .Where(c => c.EmployeeId == employeeId && c.CheckinTime.Date == date.Date)
                .OrderBy(c => c.CheckinTime)
                .ToListAsync();
        }

        public async Task<List<Checkin>> GetByEmployeeAndMonthAsync(long employeeId, int year, int month)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            return await _context.Checkins
                .Where(c => c.EmployeeId == employeeId &&
                            c.CheckinTime >= start &&
                            c.CheckinTime < end)
                .OrderBy(c => c.CheckinTime)
                .ToListAsync();
        }


        public async Task AddAsync(Checkin checkin)
        {
            await _context.Checkins.AddAsync(checkin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Checkin checkin)
        {
            _context.Checkins.Update(checkin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Checkin checkin)
        {
            _context.Checkins.Remove(checkin);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Checkin>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Checkins
                .Where(c => c.CheckinTime >= start && c.CheckinTime <= end)
                .Include(c => c.Employee)
                .ToListAsync();
        }
    }
}
