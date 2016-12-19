using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public string Name { get; set; }

        //Should this be a seperate entity? an Enum perhaps?
        
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        
        public Institution Institution { get; set; }
        public int InstitutionId { get; set; }


    }
}