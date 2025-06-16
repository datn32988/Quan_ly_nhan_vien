using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DailyReportsController : ControllerBase
    {
        private readonly IDailyReportService _reportService;

        public DailyReportsController(IDailyReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DailyReportDto>> GetById(long id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpGet("employee/{employeeId}/date/{date}")]
        public async Task<ActionResult<DailyReportDto>> GetByEmployeeAndDate(int employeeId, DateTime date)
        {
            var report = await _reportService.GetDailyReportAsync(employeeId, date);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<DailyReportDto>>> GetByEmployee(int employeeId)
        {
            var reports = await _reportService.GetEmployeeReportsAsync(employeeId);
            return Ok(reports);
        }

        [HttpPost]
        public async Task<ActionResult<DailyReportDto>> CreateOrUpdate([FromBody] DailyReportDto reportDto)
        {
            if (reportDto.ReportId > 0 && reportDto.IsFinalized)
            {
                return BadRequest("Cannot modify a finalized report");
            }

            var result = await _reportService.CreateOrUpdateDailyReportAsync(reportDto);
            return Ok(result);
        }

        [HttpPost("{reportId}/finalize")]
        public async Task<IActionResult> FinalizeReport(long reportId)
        {
            await _reportService.FinalizeReportAsync(reportId);
            return NoContent();
        }

        [HttpGet("employee/{employeeId}/available-tasks")]
        public async Task<ActionResult<List<TaskDto>>> GetAvailableTasks(int employeeId)
        {
            var tasks = await _reportService.GetAvailableTasksForEmployeeAsync(employeeId);
            return Ok(tasks);
        }
    }
}
