﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class InstitutionAdmin
    {
        [Key]
        public int ÍnstitutionAdminId { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Institution Institution { get; set; }
    }
}