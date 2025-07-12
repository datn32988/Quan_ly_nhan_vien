using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CheckinsController : ControllerBase
    {
        private readonly ICheckinService _checkinService;
        private readonly IDailyWorkSummaryService _summaryService;

        public CheckinsController(
            ICheckinService checkinService,
            IDailyWorkSummaryService summaryService)
        {
            _checkinService = checkinService;
            _summaryService = summaryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheckinDto>> GetById(long id)
        {
            var checkin = await _checkinService.GetCheckinByIdAsync(id);
            if (checkin == null) return NotFound();
            return Ok(checkin);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<List<CheckinDto>>> GetByEmployee(long employeeId)
        {
            var checkins = await _checkinService.GetCheckinsByEmployeeAsync(employeeId);
            return Ok(checkins);
        }

        [HttpGet("employee/{employeeId}/date/{date}")]
        public async Task<ActionResult<List<CheckinDto>>> GetByEmployeeAndDate(long employeeId, DateTime date)
        {
            var checkins = await _checkinService.GetCheckinsByEmployeeAndDateAsync(employeeId, date);
            return Ok(checkins);
        }

        [HttpGet("employee/{employeeId}/month/{year}/{month}")]
        public async Task<ActionResult<List<CheckinDto>>> GetByEmployeeAndMonth(long employeeId, int year, int month)
        {
            var checkins = await _checkinService.GetCheckinsByEmployeeAndMonthAsync(employeeId, year, month);
            return Ok(checkins);
        }

        [HttpPost]
        public async Task<ActionResult<CheckinDto>> Create([FromBody] CheckinDto checkinDto)
        {
            var createdCheckin = await _checkinService.CreateCheckinAsync(checkinDto);

            // Xử lý cập nhật DailyWorkSummary
            await _summaryService.ProcessCheckinAsync(checkinDto.EmployeeId, checkinDto.CheckinTime);

            return CreatedAtAction(nameof(GetById), new { id = createdCheckin.CheckinId }, createdCheckin);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _checkinService.DeleteCheckinAsync(id);
            return NoContent();
        }
    }
}
