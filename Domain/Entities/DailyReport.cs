using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DailyReport
{
    public long ReportId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime ReportDate { get; set; }

    public decimal? TotalWorkHours { get; set; }

    public string? WorkStatus { get; set; }

    public string? GeneralNotes { get; set; }

    public bool IsFinalized { get; set; }

    public DateTime? FinalizedTime { get; set; }

    public virtual ICollection<DailyReportCompletedTask> DailyReportCompletedTasks { get; set; } = new List<DailyReportCompletedTask>();

    public virtual ICollection<DailyReportPlannedTask> DailyReportPlannedTasks { get; set; } = new List<DailyReportPlannedTask>();

    public virtual Employee Employee { get; set; } = null!;
}
