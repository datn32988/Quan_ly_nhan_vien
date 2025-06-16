using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DailyWorkSummary
{
    public long Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime WorkDate { get; set; }

    public DateTime? FirstCheckin { get; set; }

    public DateTime? LastCheckout { get; set; }

    public decimal TotalCalculatedHours { get; set; }

    public bool IsFullDay { get; set; }

    public bool IsHalfDay { get; set; }

    public bool IsLateArrival { get; set; }

    public bool IsEarlyDeparture { get; set; }

    public bool IsUnderHours { get; set; }

    public string? Status { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
