using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IAuthRepository authRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var employee = await _authRepository.GetEmployeeByEmailAsync(request.Email);
                if (employee == null)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };
                }
                if (string.IsNullOrEmpty(employee.Password))
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Password not set. Please contact administrator"
                    };
                }

                bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, employee.Password);
                if (!isPasswordValid)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };
                }
                var token = _jwtTokenGenerator.GenerateToken(
                    employee.EmployeeId,
                    employee.Email!,
                    employee.FullName,
                    employee.Position?.PositionName
                );
                var employeeInfo = await MapToEmployeeInfoDto(employee);

                return new LoginResponseDto
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    Employee = employeeInfo
                };
            }
            catch (Exception)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "An error occurred during login"
                };
            }
        }

        public async Task<LoginResponseDto> ChangePasswordAsync(int employeeId, ChangePasswordRequestDto request)
        {
            try
            {
                var employee = await _authRepository.GetEmployeeByIdWithDetailsAsync(employeeId);
                if (employee == null)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Employee not found"
                    };
                }
                if (string.IsNullOrEmpty(employee.Password))
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Current password not set"
                    };
                }

                bool isCurrentPasswordValid = _passwordHasher.VerifyPassword(request.CurrentPassword, employee.Password);
                if (!isCurrentPasswordValid)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Current password is incorrect"
                    };
                }
                var hashedNewPassword = _passwordHasher.HashPassword(request.NewPassword);
                bool updateResult = await _authRepository.UpdatePasswordAsync(employeeId, hashedNewPassword);
                if (!updateResult)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Failed to update password"
                    };
                }

                return new LoginResponseDto
                {
                    Success = true,
                    Message = "Password changed successfully"
                };
            }
            catch (Exception)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "An error occurred while changing password"
                };
            }
        }

        public async Task<LoginResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            try
            {
                var employee = await _authRepository.GetEmployeeByEmailAsync(request.Email);
                var tempPassword = GenerateTemporaryPassword();
                var hashedTempPassword = _passwordHasher.HashPassword(tempPassword);

                bool updateResult = await _authRepository.UpdatePasswordAsync(employee.EmployeeId, hashedTempPassword);
                return new LoginResponseDto
                {
                    Success = true,
                    Message = "Temporary password has been sent to your email"
                };
            }
            catch (Exception)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "An error occurred during password reset"
                };
            }
        }

        public async Task<LoginResponseDto> SetPasswordAsync(SetPasswordRequestDto request)
        {
            try
            {
                var employee = await _authRepository.GetEmployeeByEmailAsync(request.Email);
                if (employee == null)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Employee not found"
                    };
                }

                var hashedPassword = _passwordHasher.HashPassword(request.NewPassword);
                bool updateResult = await _authRepository.UpdatePasswordAsync(employee.EmployeeId, hashedPassword);

                if (!updateResult)
                {
                    return new LoginResponseDto
                    {
                        Success = false,
                        Message = "Failed to set password"
                    };
                }

                return new LoginResponseDto
                {
                    Success = true,
                    Message = "Password set successfully"
                };
            }
            catch (Exception)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "An error occurred while setting password"
                };
            }
        }

        public async Task<EmployeeInfoDto?> GetEmployeeInfoAsync(int employeeId)
        {
            var employee = await _authRepository.GetEmployeeByIdWithDetailsAsync(employeeId);
            if (employee == null) return null;

            return await MapToEmployeeInfoDto(employee);
        }

        private async Task<EmployeeInfoDto> MapToEmployeeInfoDto(Employee employee)
        {
            return new EmployeeInfoDto
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Email = employee.Email!,
                Position = employee.Position?.PositionName,
                Department = employee.Position?.Description,
                EmploymentStatus = employee.EmploymentStatus,
                ManagerId = employee.ManagerId,
                ManagerName = employee.Manager?.FullName
            };
        }

        private string GenerateTemporaryPassword()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
