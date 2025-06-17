using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository,
                         IEmployeeRepository employeeRepository,
                         IMapper mapper)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<TaskDto> GetTaskByIdAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdWithEmployeeAsync(taskId);
            
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<List<TaskDto>> GetTasksByEmployeeAsync(int employeeId)
        {

            var tasks = await _taskRepository.GetByEmployeeAsync(employeeId);
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto)
        { 

            var task = _mapper.Map<EmployeesList>(taskDto);
            task.CreationDate = DateTime.Now;
            task.Status = "Pending";

            await _taskRepository.AddAsync(task);
            return await GetTaskByIdAsync(task.TaskId);
        }

        public async Task<TaskDto> UpdateTaskAsync(long taskId, UpdateTaskDto taskDto)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
          

            _mapper.Map(taskDto, task);
            await _taskRepository.UpdateAsync(task);

            return await GetTaskByIdAsync(taskId);
        }

        public async Task DeleteTaskAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
          

            await _taskRepository.DeleteAsync(task);
        }

        public async Task<TaskDto> UpdateTaskStatusAsync(long taskId, string status)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            task.Status = status;
            if (status == "Completed") task.CompletionDate = DateTime.Now;

            await _taskRepository.UpdateAsync(task);
            return await GetTaskByIdAsync(taskId);
        }
    }
}
