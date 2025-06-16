using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IPositionRepository
    {
        Task<Position?> GetByIdAsync(int id);
        Task<List<Position>> GetAllAsync();
        Task AddAsync(Position position);
        Task UpdateAsync(Position position);
        Task DeleteAsync(Position position);
    }
}
