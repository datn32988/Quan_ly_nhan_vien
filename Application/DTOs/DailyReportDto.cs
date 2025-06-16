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
        public int EmployeeId { get; set; }
        public DateTime ReportDate { get; set; }
        public decimal? TotalWorkHours { get; set; }
        public string? WorkStatus { get; set; }
        public string? GeneralNotes { get; set; }
        public bool IsFinalized { get; set; }
        public DateTime? FinalizedTime { get; set; }
        public List<CompletedTaskDto> CompletedTasks { get; set; } = new();
        public List<PlannedTaskDto> PlannedTasks { get; set; } = new();
    }
}
