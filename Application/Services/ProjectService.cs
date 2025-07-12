

using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProjectDto> CreateAsync(ProjectDto dto)
        {
            var entity = _mapper.Map<Project>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<ProjectDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
            await _repo.DeleteAsync(entity);
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<ProjectDto>>(entities);
        }

        public async Task<ProjectDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<ProjectDto>(entity);
        }

        public async Task UpdateAsync(long id, ProjectDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
        }
    }
}
