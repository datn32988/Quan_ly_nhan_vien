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
}
