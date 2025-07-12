using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IEmployeeProjectService
    {
        Task<List<EmployeeProjectDto>> GetAllAsync();
        Task AddAsync(EmployeeProjectDto dto);
        Task DeleteAsync(long employeeId, long projectId);
    }
}
