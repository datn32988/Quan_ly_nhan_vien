using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface ICheckinRepository
    {
        Task<Checkin?> GetByIdAsync(long id);
        Task<List<Checkin>> GetByEmployeeIdAsync(int employeeId);
        Task<List<Checkin>> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<List<Checkin>> GetByEmployeeAndMonthAsync(int employeeId, int year, int month);
        Task AddAsync(Checkin checkin);
        Task UpdateAsync(Checkin checkin);
        Task DeleteAsync(Checkin checkin);
    }
}
