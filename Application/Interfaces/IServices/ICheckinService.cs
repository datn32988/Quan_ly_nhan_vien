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
        Task<List<CheckinDto>> GetCheckinsByEmployeeAsync(long employeeId);
        Task<List<CheckinDto>> GetCheckinsByEmployeeAndDateAsync(long employeeId, DateTime date);
        Task<List<CheckinDto>> GetCheckinsByEmployeeAndMonthAsync(long employeeId, int year, int month);
        Task<CheckinDto> CreateCheckinAsync(CheckinDto checkinDto);
        Task DeleteCheckinAsync(long id);
    }
}
