using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime HireDate { get; set; }

    public string EmploymentStatus { get; set; } = null!;

    public long PositionId { get; set; }

    public long? ManagerId { get; set; }
    public string? Password { get; set; }
    public long? DepartmentId { get; set; }
    public virtual Department? Department { get; set; }

    public virtual ICollection<Checkin> Checkins { get; set; } = new List<Checkin>();

    public virtual ICollection<DailyReport> DailyReports { get; set; } = new List<DailyReport>();

    public virtual ICollection<DailyWorkSummary> DailyWorkSummaries { get; set; } = new List<DailyWorkSummary>();

    public virtual ICollection<EmployeesList> EmployeesLists { get; set; } = new List<EmployeesList>();

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Employee? Manager { get; set; }

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<WorkPlan> WorkPlans { get; set; } = new List<WorkPlan>();

    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

}
