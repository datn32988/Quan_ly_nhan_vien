using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<LoginResponseDto> ChangePasswordAsync(int employeeId, ChangePasswordRequestDto request);
        Task<LoginResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request);
        Task<LoginResponseDto> SetPasswordAsync(SetPasswordRequestDto request);
        Task<EmployeeInfoDto?> GetEmployeeInfoAsync(int employeeId);
    }
}
