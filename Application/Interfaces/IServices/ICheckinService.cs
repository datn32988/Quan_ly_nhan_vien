using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ICheckinService
    {
        Task<CheckinDto?> GetCheckinByIdAsync(long id);
        Task<List<CheckinDto>> GetCheckinsByEmployeeAsync(int employeeId);
        Task<List<CheckinDto>> GetCheckinsByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<List<CheckinDto>> GetCheckinsByEmployeeAndMonthAsync(int employeeId, int year, int month);
        Task<CheckinDto> CreateCheckinAsync(CheckinDto checkinDto);
        Task DeleteCheckinAsync(long id);
    }
}
