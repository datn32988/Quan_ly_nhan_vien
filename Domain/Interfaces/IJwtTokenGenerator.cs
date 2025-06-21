using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int employeeId, string email, string fullName, string? position);
        bool ValidateToken(string token);
        int? GetEmployeeIdFromToken(string token);
    }
}
