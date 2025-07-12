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
        Task<Employee?> GetEmployeeByIdWithDetailsAsync(long employeeId);
        Task<bool> UpdatePasswordAsync(long employeeId, string hashedPassword);
        Task<bool> EmailExistsAsync(string email);
    }
}
