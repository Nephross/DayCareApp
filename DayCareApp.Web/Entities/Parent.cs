using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }
        public string ApplicationUserId { get; set; }

        //Is this needed?
        [Required]
        [Display(Name = "Institution")]
        public Institution Institution { get; set; }
        
        [Required]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postnummer")]
        [MinLength(4, ErrorMessage = "The {0} musth have a {1} digits")]
        [MaxLength(4, ErrorMessage = "The {0} musth have a {1} digits")]
        public string AreaCode { get; set; }

        [Required]
        [Display(Name = "By")]
        public string City { get; set; }
        
        [Required]
        [Display(Name = "Mobiltelefon")]
        [MinLength(8, ErrorMessage = "The {0} musth have a {1} digits")]
        [MaxLength(8, ErrorMessage = "The {0} musth have a {1} digits")]
        public string MobilePhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //ProfilePicture
        public string ImagePath { get; set; }

        public virtual ICollection<Child> Children { get; set; }

    }
}