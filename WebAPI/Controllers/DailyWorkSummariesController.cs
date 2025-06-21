using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyWorkSummariesController : ControllerBase
    {
        private readonly IDailyWorkSummaryService _summaryService;

        public DailyWorkSummariesController(IDailyWorkSummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DailyWorkSummaryDto>> GetById(long id)
        {
            var summary = await _summaryService.GetSummaryByIdAsync(id);
            if (summary == null) return NotFound();
            return Ok(summary);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<DailyWorkSummaryDto>>> GetByEmployee(int employeeId)
        {
            var summaries = await _summaryService.GetSummariesByEmployeeAsync(employeeId);
            return Ok(summaries);
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<List<WorkSummaryMonthlyDto>>> GetMonthlySummary(
        [FromQuery] int year, [FromQuery] int month)
        {
            var result = await _summaryService.GetMonthlyWorkSummaries(year, month);
            return Ok(result);
        }

    }
}
