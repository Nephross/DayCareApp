using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class Child
    {
        [Key]
        public int ChildId { get; set; }

        [Required]
        public string Name { get; set; }

        
        public Parent Parent1 { get; set; }
        public int Parent1Id { get; set; }

        public Parent Parent2 { get; set; }
        public int Parent2Id { get; set; }

        public Institution Institution { get; set; }
        public int InstitutionId { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public bool CurrentlyCheckedIn { get; set; }

        [Required]
        public string SpecialNeeds { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<DayRegistration> DayRegistrations { get; set; }
    }
}