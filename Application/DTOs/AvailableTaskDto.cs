using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AvailableTaskDto
    {
        public long TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RelatedProject { get; set; }
        public string Priority { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
    public class CreateTaskDto
    {
        public string TaskName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RelatedProject { get; set; }
        public string Priority { get; set; } = null!;
        public long? AssignedEmployeeId { get; set; }
    }
    public class UpdateTaskDto : CreateTaskDto
    {
        public long TaskId { get; set; }
        public string? Status { get; set; }
    }
}
