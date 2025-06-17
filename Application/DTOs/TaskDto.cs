using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskDto
    {
        public long TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RelatedProject { get; set; }
        public string Priority { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

    public class CreateTaskDto
    {
        public string TaskName { get; set; } = null!;
        public string? Description { get; set; }
        public int AssignedToEmployeeId { get; set; }
        public string? RelatedProject { get; set; }
        public string Priority { get; set; } = "Medium";
    }

    public class UpdateTaskDto
    { 
        public string? TaskName { get; set; }

        public string? Description { get; set; }

        public string? RelatedProject { get; set; }

        public string? Priority { get; set; }
        public string? Status { get; set; }
    }
}
