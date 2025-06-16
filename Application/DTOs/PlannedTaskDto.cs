using System;
using System.Collections.Generic;
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
}
