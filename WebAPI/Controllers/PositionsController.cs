using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PositionDto>>> GetAll()
        {
            var positions = await _positionService.GetAllPositionsAsync();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionDto>> GetById(int id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null)
                return NotFound();

            return Ok(position);
        }

        [HttpPost]
        public async Task<ActionResult<PositionDto>> Create(PositionDto positionDto)
        {
            var createdPosition = await _positionService.CreatePositionAsync(positionDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPosition.PositionId }, createdPosition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PositionDto positionDto)
        {
            if (id != positionDto.PositionId)
                return BadRequest();

            await _positionService.UpdatePositionAsync(id, positionDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _positionService.DeletePositionAsync(id);
            return NoContent();
        }
    }
}
