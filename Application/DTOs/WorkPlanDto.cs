using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WorkPlanDto
    {
        public long WorkPlanId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string TaskDescription { get; set; } = null!;
        public DateTime PlannedDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
    }

    public class CreateWorkPlanDto
    {
        public long EmployeeId { get; set; }
        public string TaskDescription { get; set; } = null!;
        public DateTime PlannedDate { get; set; }
    }

    public class UpdateWorkPlanDto
    {
        public string? TaskDescription { get; set; }
        public DateTime? PlannedDate { get; set; }
        public bool? IsCompleted { get; set; }
    }

    public class WorkPlanFilterDto
    {
        public long? EmployeeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
