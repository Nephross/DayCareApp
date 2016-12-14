using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class Department
    {

        [Key]
        public int DepartMentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public Institution Institution { get; set; }
        public int InstitutionId { get; set; }

    }
}