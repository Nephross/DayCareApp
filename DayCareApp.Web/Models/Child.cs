using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Models
{
    public class Child
    {
        public int Id { get; set; }

      
        public string Name { get; set; }
        public string Parents { get; set; }
        public string Institution { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public bool CurrentlyCheckedIn { get; set; }
        public string ImagePath { get; set; }

        

        
    }
}