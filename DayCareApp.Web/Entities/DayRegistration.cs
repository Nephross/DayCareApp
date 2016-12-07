using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class DayRegistration
    {
        [Key]
        public int DayRegistrationId { get; set; }
        
        [Required]
        public Child Child { get; set; }
        public int ChildId { get; set; }

        public string Other { get; set; }

        public bool ChangedDiaper { get; set; }
        
        [Required]
        public Parent ExpectedPickupParent { get; set; }
        public int ExpectedPickupParentId { get; set; }

        [Required]
        public Parent DeliveryParent { get; set; }
        public int DeliveryParentId { get; set; }

        public Parent PickUpParent { get; set; }
        public int PickUpParentId { get; set; }

        public Employee ArrivalEmployee { get; set; }
        public int ArrivalEmployeeId { get; set; }

        public Employee DepartureEmployee { get; set; }
        public int DepartureEmployeeId { get; set; }

        public DateTime CheckInTime { get; set; }

        public DateTime CheckOutTime { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

    }
}