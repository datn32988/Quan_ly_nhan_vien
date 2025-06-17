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

        public async Task<EmployeesList?> GetByIdAsync(long taskId)
        {
            return await _context.EmployeesLists
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<EmployeesList?> GetByIdWithEmployeeAsync(long taskId)
        {
            return await _context.EmployeesLists
                .Include(t => t.AssignedToEmployee)
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<List<EmployeesList>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.EmployeesLists
                .Where(t => t.AssignedToEmployeeId == employeeId)
                .Include(t => t.AssignedToEmployee)
                .ToListAsync();
        }

        public async Task<List<EmployeesList>> GetByStatusAsync(string status)
        {
            return await _context.EmployeesLists
                .Where(t => t.Status == status)
                .Include(t => t.AssignedToEmployee)
                .ToListAsync();
        }

        public async Task<List<EmployeesList>> GetUpcomingDeadlineTasksAsync(int days = 3)
        {
            var deadline = DateTime.Now.AddDays(days);
            return await _context.EmployeesLists
                .Where(t => t.CompletionDate != null &&
                            t.CompletionDate <= deadline &&
                            t.Status != "Completed")
                .Include(t => t.AssignedToEmployee)
                .ToListAsync();
        }

        public async Task AddAsync(EmployeesList task)
        {
            await _context.EmployeesLists.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeesList task)
        {
            _context.EmployeesLists.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmployeesList task)
        {
            _context.EmployeesLists.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long taskId)
        {
            return await _context.EmployeesLists
                .AnyAsync(t => t.TaskId == taskId);
        }

        public async Task<List<EmployeesList>> GetByProjectAsync(string projectName)
        {
            return await _context.EmployeesLists
                .Where(t => t.RelatedProject == projectName)
                .Include(t => t.AssignedToEmployee)
                .ToListAsync();
        }

        public async Task<List<EmployeesList>> GetCompletedTasksAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.EmployeesLists
                .Where(t => t.Status == "Completed" &&
                            t.CompletionDate >= fromDate &&
                            t.CompletionDate <= toDate)
                .Include(t => t.AssignedToEmployee)
                .ToListAsync();
        }
    }
}

