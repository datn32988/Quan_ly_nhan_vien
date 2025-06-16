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
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<List<EmployeeDto>> GetEmployeesByManagerAsync(int managerId);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);
    }
}
