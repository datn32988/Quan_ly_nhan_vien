using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkPlansController : ControllerBase
    {
        private readonly IWorkPlanService _workPlanService;

        public WorkPlansController(IWorkPlanService workPlanService)
        {
            _workPlanService = workPlanService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkPlanDto>> GetById(long id)
        {
            var workPlan = await _workPlanService.GetByIdAsync(id);
            return Ok(workPlan);
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkPlanDto>>> GetWorkPlans(
            [FromQuery] WorkPlanFilterDto filter)
        {
            var workPlans = await _workPlanService.GetWorkPlansAsync(filter);
            return Ok(workPlans);
        }

        [HttpPost]
        public async Task<ActionResult<WorkPlanDto>> CreateWorkPlan(
            [FromBody] CreateWorkPlanDto dto)
        {
            var createdWorkPlan = await _workPlanService.CreateWorkPlanAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdWorkPlan.WorkPlanId }, createdWorkPlan);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkPlanDto>> UpdateWorkPlan(
            long id, [FromBody] UpdateWorkPlanDto dto)
        {
            var updatedWorkPlan = await _workPlanService.UpdateWorkPlanAsync(id, dto);
            return Ok(updatedWorkPlan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkPlan(long id)
        {
            await _workPlanService.DeleteWorkPlanAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public async Task<ActionResult<WorkPlanDto>> MarkAsCompleted(long id)
        {
            var workPlan = await _workPlanService.MarkAsCompletedAsync(id);
            return Ok(workPlan);
        }
    }
}
