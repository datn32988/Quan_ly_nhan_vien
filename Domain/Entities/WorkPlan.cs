using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class WorkPlan
{
    public long WorkPlanId { get; set; }

    public long EmployeeId { get; set; }

    public string TaskDescription { get; set; } = null!;

    public DateTime PlannedDate { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletionDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
