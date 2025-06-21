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
    public class WorkPlanService : IWorkPlanService
    {
        private readonly IWorkPlanRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public WorkPlanService(
            IWorkPlanRepository repository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<WorkPlanDto> GetByIdAsync(long id)
        {
            var workPlan = await _repository.GetByIdWithEmployeeAsync(id);

            return _mapper.Map<WorkPlanDto>(workPlan);
        }

        public async Task<List<WorkPlanDto>> GetWorkPlansAsync(WorkPlanFilterDto filter)
        {
            var workPlans = await _repository.GetFilteredAsync(
                filter.EmployeeId,
                filter.FromDate,
                filter.ToDate,
                filter.IsCompleted);

            return _mapper.Map<List<WorkPlanDto>>(workPlans);
        }

        public async Task<WorkPlanDto> CreateWorkPlanAsync(CreateWorkPlanDto dto)
        {
            var workPlan = _mapper.Map<WorkPlan>(dto);
            workPlan.IsCompleted = false;

            await _repository.AddAsync(workPlan);
            return await GetByIdAsync(workPlan.WorkPlanId);
        }

        public async Task<WorkPlanDto> UpdateWorkPlanAsync(long id, UpdateWorkPlanDto dto)
        {
            var workPlan = await _repository.GetByIdAsync(id);

            
            if (dto.IsCompleted.HasValue && dto.IsCompleted.Value && !workPlan.IsCompleted)
            {
                workPlan.CompletionDate = DateTime.Now;
            }
            else if (dto.IsCompleted.HasValue && !dto.IsCompleted.Value)
            {
                workPlan.CompletionDate = null;
            }

            _mapper.Map(dto, workPlan);
            await _repository.UpdateAsync(workPlan);
            return await GetByIdAsync(id);
        }

        public async Task DeleteWorkPlanAsync(long id)
        {
            var workPlan = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(workPlan);
        }

        public async Task<WorkPlanDto> MarkAsCompletedAsync(long id)
        {
            var workPlan = await _repository.GetByIdAsync(id);
            if (workPlan == null)
                throw new KeyNotFoundException($"WorkPlan with id {id} not found.");

            workPlan.IsCompleted = true;
            workPlan.CompletionDate = DateTime.Now;

            await _repository.UpdateAsync(workPlan);
            return await GetByIdAsync(id);
        }

    }
}
