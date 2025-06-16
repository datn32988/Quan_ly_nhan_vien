using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CheckinDto
    {
        public long CheckinId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CheckinTime { get; set; }
        public string? Notes { get; set; }
        public string? EmployeeName { get; set; }
    }
}
