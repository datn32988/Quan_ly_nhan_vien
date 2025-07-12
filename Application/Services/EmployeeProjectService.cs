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
    public class EmployeeProjectService : IEmployeeProjectService
    {
        private readonly IEmployeeProjectRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeProjectService(IEmployeeProjectRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EmployeeProjectDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return _mapper.Map<List<EmployeeProjectDto>>(data);
        }

        public async Task AddAsync(EmployeeProjectDto dto)
        {
            var entity = _mapper.Map<EmployeeProject>(dto);
            await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(long employeeId, long projectId)
        {
            await _repo.DeleteAsync(employeeId, projectId);
        }
    }
}
