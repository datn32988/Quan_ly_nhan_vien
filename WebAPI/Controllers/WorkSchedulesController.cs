using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkSchedulesController : ControllerBase
    {
        private readonly IWorkScheduleService _workScheduleService;

        public WorkSchedulesController(IWorkScheduleService workScheduleService)
        {
            _workScheduleService = workScheduleService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkScheduleDto>> GetById(long id)
        {
            var schedule = await _workScheduleService.GetByIdAsync(id);
            return Ok(schedule);
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkScheduleDto>>> GetSchedules(
            [FromQuery] WorkScheduleFilterDto filter)
        {
            var schedules = await _workScheduleService.GetSchedulesAsync(filter);
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<ActionResult<WorkScheduleDto>> CreateSchedule(
            [FromBody] CreateWorkScheduleDto dto)
        {
            var createdSchedule = await _workScheduleService.CreateScheduleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdSchedule.WorkScheduleId }, createdSchedule);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkScheduleDto>> UpdateSchedule(
            long id, [FromBody] UpdateWorkScheduleDto dto)
        {
            var updatedSchedule = await _workScheduleService.UpdateScheduleAsync(id, dto);
            return Ok(updatedSchedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(long id)
        {
            await _workScheduleService.DeleteScheduleAsync(id);
            return NoContent();
        }

        [HttpGet("employee/{employeeId}/monthly")]
        public async Task<ActionResult<List<WorkScheduleDto>>> GetEmployeeMonthlySchedule(
            int employeeId, [FromQuery] int year, [FromQuery] int month)
        {
            var schedules = await _workScheduleService.GetEmployeeMonthlySchedule(employeeId, year, month);
            return Ok(schedules);
        }

        [HttpGet("employee/{employeeId}/weekly")]
        public async Task<ActionResult<List<WorkScheduleDto>>> GetEmployeeWeeklySchedule(
            int employeeId, [FromQuery] DateTime startDate)
        {
            var schedules = await _workScheduleService.GetEmployeeWeeklySchedule(employeeId, startDate);
            return Ok(schedules);
        }
        [HttpGet("monthly")]
        public async Task<ActionResult<List<WorkScheduleMonthlyDto>>> GetMonthlySchedules(
            [FromQuery] int year,
            [FromQuery] int month)
        {
            var schedules = await _workScheduleService.GetMonthlyWorkSchedules(year, month);
            return Ok(schedules);
        }
        [HttpPost("send-email-reminders")]
        public async Task<IActionResult> SendEmailReminders()
        {
            try
            {
                await _workScheduleService.CheckAndSendEmailReminders();
                return Ok(new { Message = "Email reminders sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Failed to send email reminders: {ex.Message}" });
            }
        }
    }
}
