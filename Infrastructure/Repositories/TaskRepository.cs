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
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeesList>> GetAvailableTasksForPlanningAsync()
        {
            return await _context.EmployeesLists
                .Where(t => t.Status != "Completed" && t.CompletionDate == null)
                .Include(t => t.AssignedToEmployee)
                .OrderBy(t => t.Priority)
                .ThenBy(t => t.CreationDate)
                .ToListAsync();
        }

        public async Task<EmployeesList?> GetByIdAsync(long taskId)
        {
            return await _context.EmployeesLists
                .Include(t => t.AssignedToEmployee)
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }
        public async Task<EmployeesList> AddAsync(EmployeesList task)
        {
            _context.EmployeesLists.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(EmployeesList task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeesList task)
        {
            _context.EmployeesLists.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
