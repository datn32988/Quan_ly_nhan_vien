using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DailyReportCreateDto
    {
        public long EmployeeId { get; set; }
        public DateTime ReportDate { get; set; }

        public string? GeneralNotes { get; set; }

        public List<CompletedTaskDto> CompletedTasks { get; set; } = new();
        public List<PlannedTaskDto> PlannedTasks { get; set; } = new();
    }
}
