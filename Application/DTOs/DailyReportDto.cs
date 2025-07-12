using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DailyReportDto
    {
        public long ReportId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime ReportDate { get; set; }
        public string? GeneralNotes { get; set; }
        public List<CompletedTaskDetailDto> CompletedTasks { get; set; } = new();
        public List<PlannedTaskDetailDto> PlannedTasks { get; set; } = new();
    }
}
