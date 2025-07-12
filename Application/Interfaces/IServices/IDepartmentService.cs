using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IDepartmentService
    {
        Task<DepartmentDto?> GetByIdAsync(long id);
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> CreateAsync(DepartmentDto dto);
        Task UpdateAsync(long id, DepartmentDto dto);
        Task DeleteAsync(long id);
    }

}
