using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class DailyReportController : ControllerBase
    {
        private readonly IDailyReportService _dailyReportService;

        public DailyReportController(IDailyReportService dailyReportService)
        {
            _dailyReportService = dailyReportService;
        }

        [HttpPost("daily-report")]
        public async Task<ActionResult<DailyReportDto>> CreateDailyReport([FromBody] DailyReportCreateDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _dailyReportService.CreateDailyReportAsync(request);
                return CreatedAtAction(nameof(GetDailyReport),
                    new { employeeId = result.EmployeeId, date = result.ReportDate.ToString("yyyy-MM-dd") },
                    result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("daily-reportbyId")]
        public async Task<ActionResult<DailyReportDto>> GetDailyReport([FromQuery] long employeeId, [FromQuery] DateTime date)
        {
            try
            {
                var report = await _dailyReportService.GetDailyReportAsync(employeeId, date);
                if (report == null)
                    return NotFound(new { message = "Daily report not found" });

                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
   
        [HttpGet("daily-report-month-by-Id")]
        public async Task<ActionResult<MonthlyReportDto>> GetMonthlyReport([FromQuery] long employeeId, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                if (month < 1 || month > 12)
                    return BadRequest(new { message = "Invalid month. Month must be between 1 and 12." });

                var report = await _dailyReportService.GetMonthlyReportAsync(employeeId, year, month);
                if (report == null)
                    return NotFound(new { message = "Employee not found or no reports for the specified month" });

                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("dailyReport-monthly-All")]
        public async Task<ActionResult<List<MonthlyReportDto>>> GetMonthlyReports(
            [FromQuery] int year, 
            [FromQuery] int month)
         {
            var reports = await _dailyReportService.GetMonthlyReportsAsync(year, month);
            return Ok(reports);
         }
    }
}

