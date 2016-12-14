using DayCareApp.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayCareApp.Web.Models
{
    public class EntityViewModels
    {

    }

    public class InstitutionAdminViewModel
    {
        public InstitutionAdminViewModel()
        {
            institutionAdmin = new InstitutionAdmin();
        }

        public InstitutionAdmin institutionAdmin { get; set; }
        public SelectList InstitutionList { get; set; }
    }

    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
            Department = new Department();
        }

        public Department Department { get; set; }
        public SelectList InstitutionList { get; set; }
    }
}