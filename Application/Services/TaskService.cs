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
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository,IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<List<AvailableTaskDto>> GetAvailableTasksForPlanningAsync()
        {
            var tasks = await _taskRepository.GetAvailableTasksForPlanningAsync();

            return tasks.Select(t => new AvailableTaskDto
            {
                TaskId = t.TaskId,
                TaskName = t.TaskName,
                Description = t.Description,
                RelatedProject = t.RelatedProject,
                Priority = t.Priority,
                CreationDate = t.CreationDate
            }).ToList();
        }
        public async Task<AvailableTaskDto?> GetByIdAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return null;
            return _mapper.Map<AvailableTaskDto>(task);
        }
        public async Task<AvailableTaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            if (string.IsNullOrEmpty(createTaskDto.TaskName))
                throw new ArgumentException("TaskName is required");

            if (string.IsNullOrEmpty(createTaskDto.Priority))
                createTaskDto.Priority = "Medium";

            var task = _mapper.Map<EmployeesList>(createTaskDto);
            task.CreationDate = DateTime.UtcNow;
            task.Status = "New";
            var createdTask = await _taskRepository.AddAsync(task);
            return _mapper.Map<AvailableTaskDto>(createdTask);
        }


        public async Task<AvailableTaskDto> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(updateTaskDto.TaskId);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {updateTaskDto.TaskId} not found.");
            }

            _mapper.Map(updateTaskDto, existingTask);
            await _taskRepository.UpdateAsync(existingTask);

            return _mapper.Map<AvailableTaskDto>(existingTask);
        }

        public async Task DeleteTaskAsync(long taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");
            }

            await _taskRepository.DeleteAsync(task);
        }
    }
}