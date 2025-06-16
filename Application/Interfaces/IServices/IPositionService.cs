using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IPositionService
    {
        Task<PositionDto?> GetPositionByIdAsync(int id);
        Task<List<PositionDto>> GetAllPositionsAsync();
        Task<PositionDto> CreatePositionAsync(PositionDto positionDto);
        Task UpdatePositionAsync(int id, PositionDto positionDto);
        Task DeletePositionAsync(int id);
    }
}
