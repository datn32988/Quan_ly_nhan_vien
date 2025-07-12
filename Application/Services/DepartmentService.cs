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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> CreateAsync(DepartmentDto dto)
        {
            var entity = _mapper.Map<Department>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<DepartmentDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
            await _repo.DeleteAsync(entity);
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<DepartmentDto>>(entities);
        }

        public async Task<DepartmentDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<DepartmentDto>(entity);
        }

        public async Task UpdateAsync(long id, DepartmentDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
        }
    }
}
