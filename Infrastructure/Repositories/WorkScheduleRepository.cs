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
    public class WorkScheduleRepository : IWorkScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkSchedule?> GetByIdAsync(long id)
        {
            return await _context.WorkSchedules.FindAsync(id);
        }

        public async Task<WorkSchedule?> GetByIdWithEmployeeAsync(long id)
        {
            return await _context.WorkSchedules
                .Include(ws => ws.Employee)
                .FirstOrDefaultAsync(ws => ws.WorkScheduleId == id);
        }

        public async Task<List<WorkSchedule>> GetFilteredAsync(
            long? employeeId,
            DateTime? fromDate,
            DateTime? toDate,
            string? scheduleType)
        {
            var query = _context.WorkSchedules
                .Include(ws => ws.Employee)
                .AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(ws => ws.EmployeeId == employeeId);

            if (fromDate.HasValue)
                query = query.Where(ws => ws.ScheduleDate >= fromDate.Value.Date);

            if (toDate.HasValue)
                query = query.Where(ws => ws.ScheduleDate <= toDate.Value.Date);

            if (!string.IsNullOrEmpty(scheduleType))
                query = query.Where(ws => ws.ScheduleType == scheduleType);

            return await query.ToListAsync();
        }

        public async Task AddAsync(WorkSchedule schedule)
        {
            await _context.WorkSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkSchedule schedule)
        {
            _context.WorkSchedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(WorkSchedule schedule)
        {
            _context.WorkSchedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }
}
