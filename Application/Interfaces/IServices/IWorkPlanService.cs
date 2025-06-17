using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IWorkPlanService
    {
        Task<WorkPlanDto> GetByIdAsync(long id);
        Task<List<WorkPlanDto>> GetWorkPlansAsync(WorkPlanFilterDto filter);
        Task<WorkPlanDto> CreateWorkPlanAsync(CreateWorkPlanDto dto);
        Task<WorkPlanDto> UpdateWorkPlanAsync(long id, UpdateWorkPlanDto dto);
        Task DeleteWorkPlanAsync(long id);
        Task<WorkPlanDto> MarkAsCompletedAsync(long id);
    }
}
