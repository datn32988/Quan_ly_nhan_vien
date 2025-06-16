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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<PositionDto?> GetPositionByIdAsync(int id)
        {
            var position = await _positionRepository.GetByIdAsync(id);
            return _mapper.Map<PositionDto>(position);
        }

        public async Task<List<PositionDto>> GetAllPositionsAsync()
        {
            var positions = await _positionRepository.GetAllAsync();
            return _mapper.Map<List<PositionDto>>(positions);
        }

        public async Task<PositionDto> CreatePositionAsync(PositionDto positionDto)
        {
            var position = _mapper.Map<Position>(positionDto);
            await _positionRepository.AddAsync(position);
            return _mapper.Map<PositionDto>(position);
        }

        public async Task UpdatePositionAsync(int id, PositionDto positionDto)
        {
            var existingPosition = await _positionRepository.GetByIdAsync(id);
            if (existingPosition == null)
                throw new KeyNotFoundException("Position not found");

            _mapper.Map(positionDto, existingPosition);
            await _positionRepository.UpdateAsync(existingPosition);
        }

        public async Task DeletePositionAsync(int id)
        {
            var position = await _positionRepository.GetByIdAsync(id);
            if (position == null)
                throw new KeyNotFoundException("Position not found");

            await _positionRepository.DeleteAsync(position);
        }
    }
}
