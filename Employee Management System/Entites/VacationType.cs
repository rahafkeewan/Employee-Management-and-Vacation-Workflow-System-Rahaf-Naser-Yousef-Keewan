﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Entites
{
    public class VacationType
    {
        [Key]
        [MaxLength(1)]
        public string VacationTypeCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string VacationTypeName { get; set; }
    }
}
