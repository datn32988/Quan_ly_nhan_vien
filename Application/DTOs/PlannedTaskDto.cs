using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PlannedTaskDto
    {
        public long TaskId { get; set; }
        public string? PlannedDescription { get; set; }
    }
    public class PlannedTaskDetailDto
    {
        public long TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? PlannedDescription { get; set; }
    }
}
