using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IEmployeeProjectRepository
    {
        Task<List<EmployeeProject>> GetAllAsync();
        Task AddAsync(EmployeeProject entity);
        Task DeleteAsync(long employeeId, long projectId);
    }
}
