using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DailyReportCompletedTask
{
    public long ReportCompletedTaskId { get; set; }

    public long ReportId { get; set; }

    public long TaskId { get; set; }

    public string? CompletionDescription { get; set; }

    public virtual DailyReport Report { get; set; } = null!;

    public virtual EmployeesList Task { get; set; } = null!;
}
