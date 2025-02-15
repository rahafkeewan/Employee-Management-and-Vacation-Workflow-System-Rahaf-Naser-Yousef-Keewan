using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Entites
{
    public class Employee
    {
        public Employee()
        {
            VacationDaysLeft = 24;
        }
        [Key]
        [MaxLength(6)]
        public string EmployeeNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string EmployeeName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        [Required]
        [MaxLength(1)]
        public string GenderCode { get; set; }

        [MaxLength(6)]
        public string? ReportedToEmployeeNumber { get; set; }
        public Employee ReportedToEmployee { get; set; }

        [Range(0, 24)]
        public int VacationDaysLeft { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

    }
}