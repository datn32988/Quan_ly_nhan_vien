using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WorkScheduleDto
    {
        public long WorkScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime ScheduleDate { get; set; }
        public string ScheduleType { get; set; } = null!; // "Daily", "Weekly", "Monthly"
        public string? Notes { get; set; }
    }

    public class CreateWorkScheduleDto
    {
        public long EmployeeId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string ScheduleType { get; set; } = null!;
        public string? Notes { get; set; }
    }

    public class UpdateWorkScheduleDto
    {
        public DateTime? ScheduleDate { get; set; }
        public string? ScheduleType { get; set; }
        public string? Notes { get; set; }
    }

    public class WorkScheduleFilterDto
    {
        public long? EmployeeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ScheduleType { get; set; }
    }
    public class WorkScheduleMonthlyDto
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public List<DailyScheduleDto> Schedules { get; set; } = new();
    }

    public class DailyScheduleDto
    {
        public DateTime Date { get; set; }
        public string ScheduleType { get; set; } = null!;
        public string? Notes { get; set; }
    }
}

