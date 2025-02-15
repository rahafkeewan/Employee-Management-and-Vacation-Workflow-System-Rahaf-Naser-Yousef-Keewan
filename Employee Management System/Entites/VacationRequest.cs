using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Entites
{
    public class VacationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        [Required]
        public DateTime RequestSubmissionDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [MaxLength(6)]
        public string EmployeeNumber { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [MaxLength(1)]
        public string VacationTypeCode { get; set; }
        public VacationType VacationType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TotalVacationDays { get; set; }

        [Required]
        public int RequestStateId { get; set; }
        public RequestState RequestState { get; set; }

        [MaxLength(6)]
        public string? ApprovedByEmployeeNumber { get; set; }
        public Employee ApprovedByEmployee { get; set; }

        [MaxLength(6)]
        public string? DeclinedByEmployeeNumber { get; set; }
        public Employee DeclinedByEmployee { get; set; }

    }
}
