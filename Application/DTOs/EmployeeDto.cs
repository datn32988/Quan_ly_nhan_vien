using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime HireDate { get; set; }
        public string EmploymentStatus { get; set; } = null!;
        public int PositionId { get; set; }
        public int? ManagerId { get; set; }
        public string? Password {  get; set; }
        public string? PositionName { get; set; }
        public string? ManagerName { get; set; }
    }
}
