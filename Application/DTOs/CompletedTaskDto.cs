using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CompletedTaskDto
    {
        public long TaskId { get; set; }
        public string? CompletionDescription { get; set; }
    }
    public class CompletedTaskDetailDto
    {
        public long TaskId { get; set; }
        public string TaskName { get; set; } = null!;
        public string? CompletionDescription { get; set; }
    }

}
