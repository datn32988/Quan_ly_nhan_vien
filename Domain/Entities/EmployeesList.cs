using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EmployeesList
{
    public long TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public string? RelatedProject { get; set; }

    public int AssignedToEmployeeId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Employee AssignedToEmployee { get; set; } = null!;

    public virtual ICollection<DailyReportCompletedTask> DailyReportCompletedTasks { get; set; } = new List<DailyReportCompletedTask>();

    public virtual ICollection<DailyReportPlannedTask> DailyReportPlannedTasks { get; set; } = new List<DailyReportPlannedTask>();
}
