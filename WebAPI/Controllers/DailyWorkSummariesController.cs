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

        [HttpGet("employee/{employeeId}/date/{date}")]
        public async Task<ActionResult<DailyWorkSummaryDto>> GetDailySummary(int employeeId, DateTime date)
        {
            var summary = await _summaryService.GetDailySummaryAsync(employeeId, date);
            if (summary == null) return NotFound();
            return Ok(summary);
        }

        [HttpGet("employee/{employeeId}/month/{year}/{month}")]
        public async Task<ActionResult<List<DailyWorkSummaryDto>>> GetMonthlySummaries(int employeeId, int year, int month)
        {
            var summaries = await _summaryService.GetMonthlySummariesAsync(employeeId, year, month);
            return Ok(summaries);
        }
    }
}
