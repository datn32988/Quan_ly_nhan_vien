using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDto>> GetTask(long id)
        { 
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
           
        }

        [HttpGet("employee/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TaskDto>>> GetTasksByEmployee(int employeeId)
        {
                var tasks = await _taskService.GetTasksByEmployeeAsync(employeeId);
                return Ok(tasks);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto taskDto)
        {
                var createdTask = await _taskService.CreateTaskAsync(taskDto);
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.TaskId }, createdTask);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDto>> UpdateTask(long id, [FromBody] UpdateTaskDto taskDto)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);
            return Ok(updatedTask);

        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDto>> UpdateTaskStatus(long id, [FromBody] string status)
        {
                var updatedTask = await _taskService.UpdateTaskStatusAsync(id, status);
                return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(long id)
        {     
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
        }
    }
}
