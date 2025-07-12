using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectDto
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
