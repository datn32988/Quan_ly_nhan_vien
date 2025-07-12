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
    public class EmployeeProjectRepository : IEmployeeProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeProject>> GetAllAsync()
        {
            return await _context.EmployeeProjects.ToListAsync();
        }

        public async Task AddAsync(EmployeeProject entity)
        {
            _context.EmployeeProjects.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long employeeId, long projectId)
        {
            var entity = await _context.EmployeeProjects
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.ProjectId == projectId);

            if (entity != null)
            {
                _context.EmployeeProjects.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
