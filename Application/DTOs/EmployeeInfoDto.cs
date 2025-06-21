using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeInfoDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string EmploymentStatus { get; set; } = null!;
        public int? ManagerId { get; set; }
        public string? ManagerName { get; set; }
    }
}
