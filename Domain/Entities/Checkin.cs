using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Checkin
{
    public long CheckinId { get; set; }

    public long EmployeeId { get; set; }

    public DateTime CheckinTime { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
