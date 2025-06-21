using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DailyWorkSummaryDto
    {
        public long Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null;
        public DateTime WorkDate { get; set; }
        public DateTime? FirstCheckin { get; set; }
        public DateTime? LastCheckout { get; set; }
        public decimal TotalCalculatedHours { get; set; }
        public bool IsFullDay { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsLateArrival { get; set; }
        public bool IsEarlyDeparture { get; set; }
        public bool IsUnderHours { get; set; }
        public string? Status { get; set; }
        public string? EmployeeName { get; set; }
    }

    public class WorkSummaryMonthlyDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public List<DailyWorkSummaryDto> Summaries { get; set; } = new();
    }

   
}
