using DayCareApp.Data.DAL;
using DayCareApp.Web.Helpers;
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

    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Employee = new Employee();
        }

        public Employee Employee { get; set; }
        public SelectList InstitutionList { get; set; }
        public SelectList DepartmentList { get; set; }
    }

    public class ParentViewModel
    {
        public ParentViewModel()
        {
            Parent = new Parent();
        }

        public Parent Parent { get; set; }
        public FileUploadPacket FileUploadPacket { get; set; }
        public SelectList InstitutionList { get; set; }
    }

    public class ChildViewModel
    {
        public ChildViewModel()
        {
            Child = new Child();
        }

        public Child Child { get; set; }
        public FileUploadPacket FileUploadPacket { get; set; }
        public SelectList InstitutionList { get; set; }
        public SelectList DepartmentList { get; set; }

    }

    public class DayRegistrationViewModel
    {

    }

}