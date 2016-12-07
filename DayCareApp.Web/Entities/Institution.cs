using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
	public class Institution
	{

        [Key]
        public int InstitutionId { get; set; }

        public string InstitutionName { get; set; }


        //Institution leaders name
        [Required]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }


        //Adress of the the institution itself
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

        //contact information
        [Required]
        [Display(Name = "Telefonnummer")]
        [MinLength(8, ErrorMessage = "The {0} musth have a {1} digits")]
        [MaxLength(8, ErrorMessage = "The {0} musth have a {1} digits")]
        public string Phonenumber { get; set; }

        [Required]
        [Display(Name = "Mobiltelefon")]
        [MinLength(8, ErrorMessage = "The {0} musth have a {1} digits")]
        [MaxLength(8, ErrorMessage = "The {0} musth have a {1} digits")]
        public string MobilePhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

    }
}