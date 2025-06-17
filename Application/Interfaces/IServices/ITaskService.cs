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
        Task<TaskDto> GetTaskByIdAsync(long taskId);
        Task<List<TaskDto>> GetTasksByEmployeeAsync(int employeeId);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto);
        Task<TaskDto> UpdateTaskAsync(long taskId, UpdateTaskDto taskDto);
        Task DeleteTaskAsync(long taskId);
        Task<TaskDto> UpdateTaskStatusAsync(long taskId, string status);
    }
}
