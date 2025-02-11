using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HydroSpark.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee Employee { get; set; }
    }

}