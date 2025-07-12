using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<ProjectDto?> GetByIdAsync(long id);
        Task<List<ProjectDto>> GetAllAsync();
        Task<ProjectDto> CreateAsync(ProjectDto dto);
        Task UpdateAsync(long id, ProjectDto dto);
        Task DeleteAsync(long id);
    }
}
