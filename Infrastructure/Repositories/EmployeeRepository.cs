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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(long id)
        {
                return await _context.Employees
                    .Include(e => e.Position)
                    .Include(e => e.Manager)
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);    
        }

        public async Task<List<Employee>> GetAllAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Employees
                    .Include(e => e.Position)
                    .Include(e => e.Manager)
                    .ToListAsync();
            }

            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesByManagerAsync(long managerId)
        {
            return await _context.Employees
                .Where(e => e.ManagerId == managerId)
                .Include(e => e.Position)
                .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }
        public async Task<List<Employee>> GetAllWithPositionAndDepartmentAsync()
        {
            return await _context.Employees
                .Include(e => e.Position)
                    .ThenInclude(p => p.Description)
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }
        public async Task<Employee?> GetById(long id)
        {
            return await _context.Employees
                .Include(e => e.Position)
                    .ThenInclude(p => p.Description)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }
    }
}
