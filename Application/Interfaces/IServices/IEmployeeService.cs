using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetEmployeeByIdAsync(long id);
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<List<EmployeeDto>> GetEmployeesByManagerAsync(long managerId);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(long id, EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(long id);
    }
}
