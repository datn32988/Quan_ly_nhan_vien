using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<Employee?> GetEmployeeByIdWithDetailsAsync(int employeeId);
        Task<bool> UpdatePasswordAsync(int employeeId, string hashedPassword);
        Task<bool> EmailExistsAsync(string email);
    }
}
