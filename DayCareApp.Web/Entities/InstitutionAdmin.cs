using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class InstitutionAdmin
    {
        [Key]
        public int InstitutionAdminId { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Institution Institution { get; set; }
        public int InstitutionId { get; set; }
    }
}