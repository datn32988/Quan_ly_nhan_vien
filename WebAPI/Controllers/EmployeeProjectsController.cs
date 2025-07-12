using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProjectsController : ControllerBase
    {
        private readonly IEmployeeProjectService _service;

        public EmployeeProjectsController(IEmployeeProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeProjectDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeProjectDto dto)
        {
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] long employeeId, [FromQuery] long projectId)
        {
            await _service.DeleteAsync(employeeId, projectId);
            return NoContent();
        }
    }
}
