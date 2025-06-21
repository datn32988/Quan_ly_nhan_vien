using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.LoginAsync(request);

                if (!result.Success)
                    return Unauthorized(result);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new LoginResponseDto
                {
                    Success = false,
                    Message = "Internal server error"
                });
            }
        }
        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult<LoginResponseDto>> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (employeeIdClaim == null || !int.TryParse(employeeIdClaim.Value, out int employeeId))
                    return Unauthorized(new LoginResponseDto { Success = false, Message = "Invalid token" });

                var result = await _authService.ChangePasswordAsync(employeeId, request);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new LoginResponseDto
                {
                    Success = false,
                    Message = "Internal server error"
                });
            }
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult<LoginResponseDto>> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.ResetPasswordAsync(request);
                return Ok(result);
            }
            catch (Exception )
            {
                return StatusCode(500, new LoginResponseDto
                {
                    Success = false,
                    Message = "Internal server error"
                });
            }
        }
        [HttpPost("set-password")]
        public async Task<ActionResult<LoginResponseDto>> SetPassword([FromBody] SetPasswordRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.SetPasswordAsync(request);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new LoginResponseDto
                {
                    Success = false,
                    Message = "Internal server error"
                });
            }
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<EmployeeInfoDto>> GetCurrentEmployee()
        {
            try
            {
                var employeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (employeeIdClaim == null || !int.TryParse(employeeIdClaim.Value, out int employeeId))
                    return Unauthorized(new { message = "Invalid token" });

                var employee = await _authService.GetEmployeeInfoAsync(employeeId);
                if (employee == null)
                    return NotFound(new { message = "Employee not found" });

                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            return Ok(new { success = true, message = "Logged out successfully" });
        }
    }
}
