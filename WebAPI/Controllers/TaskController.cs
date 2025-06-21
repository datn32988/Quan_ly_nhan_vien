using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("tasks/available-for-planning")]
        public async Task<ActionResult<List<AvailableTaskDto>>> GetAvailableTasksForPlanning()
        {
            try
            {
                var tasks = await _taskService.GetAvailableTasksForPlanningAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("gettasksby{id}", Name = "GetTask")]
        public async Task<ActionResult<AvailableTaskDto>> GetTask(long id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }
        [HttpPost("CreateTask")]
        public async Task<ActionResult<AvailableTaskDto>> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            try
            {
                var createdTask = await _taskService.CreateTaskAsync(createTaskDto);
                return CreatedAtRoute("GetTask", new { id = createdTask.TaskId }, createdTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Internal server error",
                    detail = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
        [HttpPut("UpdateTask/{id}")]
        public async Task<IActionResult> UpdateTask(long id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            try
            {
                if (id != updateTaskDto.TaskId)
                {
                    return BadRequest("ID mismatch");
                }

                var updatedTask = await _taskService.UpdateTaskAsync(updateTaskDto);
                return Ok(updatedTask);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(long id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
