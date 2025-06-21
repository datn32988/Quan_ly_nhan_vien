using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ITaskService
    {
        Task<List<AvailableTaskDto>> GetAvailableTasksForPlanningAsync();
        Task<AvailableTaskDto> GetByIdAsync(long taskId);
        Task<AvailableTaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<AvailableTaskDto> UpdateTaskAsync(UpdateTaskDto updateTaskDto);
        Task DeleteTaskAsync(long taskId);
    }

}
