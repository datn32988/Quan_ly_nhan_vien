using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MonthlyReportDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DailyReportDto> DailyReports { get; set; } = new();
        public int TotalCompletedTasks { get; set; }
        public int TotalPlannedTasks { get; set; }
    }
}
