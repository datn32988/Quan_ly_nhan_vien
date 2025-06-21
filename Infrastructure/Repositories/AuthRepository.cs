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

    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee?> GetEmployeeByIdWithDetailsAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Position)
                    .ThenInclude(p => p.Description)
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<bool> UpdatePasswordAsync(int employeeId, string hashedPassword)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return false;

            employee.Password = hashedPassword;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Employees.AnyAsync(e => e.Email == email);
        }
    }
}