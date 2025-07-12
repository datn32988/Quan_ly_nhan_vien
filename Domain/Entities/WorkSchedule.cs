using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class WorkSchedule
{
    public long WorkScheduleId { get; set; }

    public long EmployeeId { get; set; }

    public DateTime ScheduleDate { get; set; }

    public string ScheduleType { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
