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
    public class WorkPlanRepository : IWorkPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkPlan?> GetByIdAsync(long id)
        {
            return await _context.WorkPlans.FindAsync(id);
        }

        public async Task<WorkPlan?> GetByIdWithEmployeeAsync(long id)
        {
            return await _context.WorkPlans
                .Include(wp => wp.Employee)
                .FirstOrDefaultAsync(wp => wp.WorkPlanId == id);
        }

        public async Task<List<WorkPlan>> GetFilteredAsync(
            int? employeeId,
            DateTime? fromDate,
            DateTime? toDate,
            bool? isCompleted)
        {
            var query = _context.WorkPlans
                .Include(wp => wp.Employee)
                .AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(wp => wp.EmployeeId == employeeId.Value);

            if (fromDate.HasValue)
                query = query.Where(wp => wp.PlannedDate >= fromDate.Value.Date);

            if (toDate.HasValue)
                query = query.Where(wp => wp.PlannedDate <= toDate.Value.Date);

            if (isCompleted.HasValue)
                query = query.Where(wp => wp.IsCompleted == isCompleted.Value);

            return await query.OrderBy(wp => wp.PlannedDate).ToListAsync();
        }

        public async Task AddAsync(WorkPlan workPlan)
        {
            await _context.WorkPlans.AddAsync(workPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkPlan workPlan)
        {
            _context.WorkPlans.Update(workPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(WorkPlan workPlan)
        {
            _context.WorkPlans.Remove(workPlan);
            await _context.SaveChangesAsync();
        }
    }
}
