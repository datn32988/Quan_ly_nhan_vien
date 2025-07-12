using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeProject
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public long ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
